using LegacyCustomScraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LegacyCustomScraper
{
    public static class Parser
    {
        public static DateTime DateFromUnixEpoch(long timestamp)
        {
            var dte = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dte.AddSeconds(timestamp);
        }
        public static DateTime DateFromUnixEpoch(string timestamp)
        {
            return DateFromUnixEpoch(long.Parse(timestamp));
        }

        public static Song ParseSong(dynamic json)
        {
            var song = new Song()
            {
                SongId = json[0],
                Artist = WebUtility.HtmlDecode((string)json[1]),
                Title = WebUtility.HtmlDecode((string)json[2]),
                Album = WebUtility.HtmlDecode((string)json[3]),
                Tuning = WebUtility.HtmlDecode((string)json[4]),
                Paths = new SongPath()
                {
                    Lead = json[10].ToString().Contains("lead"),
                    Rhythm = json[10].ToString().Contains("rhythm"),
                    Bass = json[10].ToString().Contains("bass"),
                    Vocals = json[10].ToString().Contains("vocals"),
                },
                Platforms = new SongPlatform()
                {
                    PC = json[12].ToString().Contains("pc"),
                    Mac = json[12].ToString().Contains("mac"),
                    Xbox = json[12].ToString().Contains("xbox360"),
                    Playstation = json[12].ToString().Contains("ps3")
                },
                Official = json[15],
                Author = json[6],
                Version = json[5],
                Downloads = long.Parse((string)json[9]),
                DynamicDifficulty = json[11],
                CreateDate = DateFromUnixEpoch((string)json[7]),
                Updated = DateFromUnixEpoch((string)json[8]),
            };
            return song;
        }
    }
}
