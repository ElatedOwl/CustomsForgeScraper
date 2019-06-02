using BotData.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
namespace CustomScraper
{
    public class ScrapeClient
    {
        protected HttpClient client;
        private string _username;
        private string _password;
        private const string LOGIN_ACTION_URL = "http://customsforge.com/index.php?app=core&module=global&section=login&do=process";
        private const string LOGIN_URL = "http://customsforge.com/index.php?app=core&module=global&section=login";
        private const string SONG_SEARCH_URL = "http://ignition.customsforge.com/cfss?u=21575";

        /// <summary>
        /// Creates a new scrape client with the specified credentials and logs in.
        /// </summary>
        /// <param name="username">The username to log in with.</param>
        /// <param name="password">The password to log in with.</param>
        private ScrapeClient(string username, string password)
        {
            this._username = username;
            this._password = password;
            client = new HttpClient();
        }

        /// <summary>
        /// Creates and attempts to log in a new ScrapeClient with the given credentials.
        /// </summary>
        /// <param name="username">The username to log in with.</param>
        /// <param name="password">The password to log in with.</param>
        /// <returns>Returns null if login fails.</returns>
        public static ScrapeClient CreateInstance(string username, string password)
        {
            var client = new ScrapeClient(username, password);

            if(!client.Login())
            {
                return null;
            }

            return client;
        }

        /// <summary>
        /// Creates and attempts to log in a new ScrapeClient with the given credentials.
        /// </summary>
        /// <param name="username">The username to log in with.</param>
        /// <param name="password">The password to log in with.</param>
        /// <param name="client">The output ScrapeClient. Will not be set if failed.</param>
        /// <returns>Returns true if login successful, false if failed.</returns>
        public static bool TryCreateInstance(string username, string password, ref ScrapeClient client)
        {
            var newClient = CreateInstance(username, password);

            if(newClient == null)
            {
                return false;
            }
            else
            {
                client = newClient;
                return true;
            }
        }

        #region Login

        /// <summary>
        /// Performs the login to CustomsForge.
        /// </summary>
        /// <returns>True if login success, false if failed.</returns>
        public bool Login()
        {
            var loginData = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("ips_username", _username),
                new KeyValuePair<string, string>("ips_password", _password),
                new KeyValuePair<string, string>("auth_key", GetAuthKey()),
                new KeyValuePair<string, string>("rememberMe", "1")
            });
            var res = client.PostAsync(LOGIN_ACTION_URL, loginData);
            var result = res.Result;
            return res.Result.RequestMessage.RequestUri.Query == "?" && res.Result.StatusCode == System.Net.HttpStatusCode.OK;
        }

        /// <summary>
        /// Gets the auth key required to login.
        /// </summary>
        /// <returns>Returns auth key from login page.</returns>
        protected string GetAuthKey()
        {
            var html = client.GetStringAsync(LOGIN_URL);
            var dom = new HtmlAgilityPack.HtmlDocument();
            dom.LoadHtml(html.Result);

            var authNode = dom
                .DocumentNode
                .Descendants("input")
                .Where(n => n.Attributes["name"] != null && n.Attributes["name"].Value == "auth_key").FirstOrDefault();

            if (authNode == null)
            {
                return "";
            }
            Console.WriteLine("Auth key retrieved");
            return authNode.GetAttributeValue("value", "");
        }
        #endregion

        /// <summary>
        /// Performs a search for the specified terms on the song and artist field.
        /// </summary>
        /// <param name="search">Terms to search by.</param>
        /// <param name="artist">Specifies the artist to filter by.</param>
        /// <param name="songTitle">Specifies the song title to filter by.</param>
        /// <param name="resultsPerPage">Specifies the amount of results per page. This seems to be unlimited but keep low. Default is 25.</param>
        /// <param name="pageNumber">The page number to load, 0 based. Default is 0.</param>
        /// <param name="maxResults">The maximum amount of results to parse; default is 0 (unlimited). Returns soft error if exceeded and not 0.</param>
        /// <returns>Returns an object specifying success/fail with error reason on fail or list of results on success.</returns>
        public SongRequest Lookup(string search, string artist = null, string songTitle = null, int resultsPerPage = 25, int pageNumber = 0, int maxResults = 0)
        {
            var result = new SongRequest();
            string json = RequestJson(search, artist, songTitle, resultsPerPage, pageNumber);
            dynamic data = JObject.Parse(json);

            if(data.data.Count == 0)
            {
                result.Error = true;
                result.ErrorType = SongRequest.RequestError.NoResults;
            }
            else if(maxResults != 0 && data.data.Count > maxResults)
            {
                result.Error = true;
                result.ErrorType = SongRequest.RequestError.TooManyResults;
            }
            else
            {
                foreach(dynamic song in data.data)
                {
                    result.Results.Add(Parser.ParseSong(song));
                }
            }

            return result;
        }

        /// <summary>
        /// Makes a request to the search URL and returns the JSON content as string.
        /// </summary>
        /// <param name="search">The term(s) to search for.</param>
        /// <param name="relog">Flag indicating whether it should try to log in if logged out automatically; used to ensure there isn't an endless loop.</param>
        /// <returns></returns>
        protected string RequestJson(string search, string artist, string songTitle, int resultsPerPage, int pageNumber, bool relog = false)
        {
            var formData = FormData.GetFormData(search, artist, songTitle, resultsPerPage, pageNumber);
            var res = client.PostAsync(SONG_SEARCH_URL, formData).Result;
            if (res.RequestMessage != null && res.RequestMessage.RequestUri.ToString().Contains("section=login"))
            {
                if(relog)
                {
                    throw new Exception("Failed to log in, breaking endless loop.");
                }
                Login();
                return RequestJson(search, artist, songTitle, resultsPerPage, pageNumber, true);
            }
            return res.Content.ReadAsStringAsync().Result;
        }
    }
}
