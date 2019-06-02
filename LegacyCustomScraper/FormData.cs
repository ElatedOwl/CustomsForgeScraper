using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CustomScraper
{
    public static class FormData
    {
        private static KeyValuePair<string, string>[] _baseFormData = new[]{
                new KeyValuePair<string, string>("search[regex]", "false"),
                new KeyValuePair<string, string>("draw", "2"),
                new KeyValuePair<string, string>("order[0][column]", "2"),
                new KeyValuePair<string, string>("order[0][dir]", "asc"),
                new KeyValuePair<string, string>("columns[0][data][_]", ""),
                new KeyValuePair<string, string>("columns[0][data][display]", "undefined"),
                new KeyValuePair<string, string>("columns[0][name]", ""),
                new KeyValuePair<string, string>("columns[0][searchable]", "true"),
                new KeyValuePair<string, string>("columns[0][orderable]", "true"),
                new KeyValuePair<string, string>("columns[0][search][value]", ""),
                new KeyValuePair<string, string>("columns[0][search][regex]", "false"),
                new KeyValuePair<string, string>("columns[1][data][_]", "1"),
				new KeyValuePair<string, string>("columns[1][data][display]", "undefined"),
				new KeyValuePair<string, string>("columns[1][name]", ""),
				new KeyValuePair<string, string>("columns[1][searchable]", "true"),
				new KeyValuePair<string, string>("columns[1][orderable]", "true"),
				new KeyValuePair<string, string>("columns[1][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[2][data][_]", "2"),
				new KeyValuePair<string, string>("columns[2][data][display]", "undefined"),
				new KeyValuePair<string, string>("columns[2][name]", ""),
				new KeyValuePair<string, string>("columns[2][searchable]", "true"),
				new KeyValuePair<string, string>("columns[2][orderable]", "true"),
				new KeyValuePair<string, string>("columns[2][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[3][data]", "3"),
				new KeyValuePair<string, string>("columns[3][name]", ""),
				new KeyValuePair<string, string>("columns[3][searchable]", "false"),
				new KeyValuePair<string, string>("columns[3][orderable]", "true"),
				new KeyValuePair<string, string>("columns[3][search][value]", ""),
				new KeyValuePair<string, string>("columns[3][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[4][data][_]", "4"),
				new KeyValuePair<string, string>("columns[4][data][display]", "undefined"),
				new KeyValuePair<string, string>("columns[4][name]", ""),
				new KeyValuePair<string, string>("columns[4][searchable]", "true"),
				new KeyValuePair<string, string>("columns[4][orderable]", "true"),
				new KeyValuePair<string, string>("columns[4][search][value]", ""),
				new KeyValuePair<string, string>("columns[4][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[5][data]", "5"),
				new KeyValuePair<string, string>("columns[5][name]", ""),
				new KeyValuePair<string, string>("columns[5][searchable]", "true"),
				new KeyValuePair<string, string>("columns[5][orderable]", "true"),
				new KeyValuePair<string, string>("columns[5][search][value]", ""),
				new KeyValuePair<string, string>("columns[5][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[6][data]", "6"),
				new KeyValuePair<string, string>("columns[6][name]", ""),
				new KeyValuePair<string, string>("columns[6][searchable]", "true"),
				new KeyValuePair<string, string>("columns[6][orderable]", "true"),
				new KeyValuePair<string, string>("columns[6][search][value]", ""),
				new KeyValuePair<string, string>("columns[6][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[7][data][_]", "7"),
				new KeyValuePair<string, string>("columns[7][data][display]", "undefined"),
				new KeyValuePair<string, string>("columns[7][name]", ""),
				new KeyValuePair<string, string>("columns[7][searchable]", "true"),
				new KeyValuePair<string, string>("columns[7][orderable]", "true"),
				new KeyValuePair<string, string>("columns[7][search][value]", ""),
				new KeyValuePair<string, string>("columns[7][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[8][data][_]", "8"),
				new KeyValuePair<string, string>("columns[8][data][display]", "undefined"),
				new KeyValuePair<string, string>("columns[8][name]", ""),
				new KeyValuePair<string, string>("columns[8][searchable]", "true"),
				new KeyValuePair<string, string>("columns[8][orderable]", "true"),
				new KeyValuePair<string, string>("columns[8][search][value]", ""),
				new KeyValuePair<string, string>("columns[8][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[9][data]", "9"),
				new KeyValuePair<string, string>("columns[9][name]", ""),
				new KeyValuePair<string, string>("columns[9][searchable]", "true"),
				new KeyValuePair<string, string>("columns[9][orderable]", "true"),
				new KeyValuePair<string, string>("columns[9][search][value]", ""),
				new KeyValuePair<string, string>("columns[9][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[10][data][_]", "10"),
				new KeyValuePair<string, string>("columns[10][data][display]", "undefined"),
				new KeyValuePair<string, string>("columns[10][name]", ""),
				new KeyValuePair<string, string>("columns[10][searchable]", "true"),
				new KeyValuePair<string, string>("columns[10][orderable]", "true"),
				new KeyValuePair<string, string>("columns[10][search][value]", ""),
				new KeyValuePair<string, string>("columns[10][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[11][data][_]", "11"),
				new KeyValuePair<string, string>("columns[11][data][filter]", "11"),
				new KeyValuePair<string, string>("columns[11][data][display]", "undefined"),
				new KeyValuePair<string, string>("columns[11][name]", ""),
				new KeyValuePair<string, string>("columns[11][searchable]", "true"),
				new KeyValuePair<string, string>("columns[11][orderable]", "true"),
				new KeyValuePair<string, string>("columns[11][search][value]", ""),
				new KeyValuePair<string, string>("columns[11][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[12][data][_]", "12"),
				new KeyValuePair<string, string>("columns[12][data][display]", "undefined"),
				new KeyValuePair<string, string>("columns[12][name]", ""),
				new KeyValuePair<string, string>("columns[12][searchable]", "true"),
				new KeyValuePair<string, string>("columns[12][orderable]", "true"),
				new KeyValuePair<string, string>("columns[12][search][value]", ""),
				new KeyValuePair<string, string>("columns[12][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[13][data]", "13"),
				new KeyValuePair<string, string>("columns[13][name]", ""),
				new KeyValuePair<string, string>("columns[13][searchable]", "true"),
				new KeyValuePair<string, string>("columns[13][orderable]", "true"),
				new KeyValuePair<string, string>("columns[13][search][value]", ""),
				new KeyValuePair<string, string>("columns[13][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[14][data]", "14"),
				new KeyValuePair<string, string>("columns[14][name]", ""),
				new KeyValuePair<string, string>("columns[14][searchable]", "true"),
				new KeyValuePair<string, string>("columns[14][orderable]", "true"),
				new KeyValuePair<string, string>("columns[14][search][value]", ""),
				new KeyValuePair<string, string>("columns[14][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[15][data]", "15"),
				new KeyValuePair<string, string>("columns[15][name]", ""),
				new KeyValuePair<string, string>("columns[15][searchable]", "true"),
				new KeyValuePair<string, string>("columns[15][orderable]", "true"),
				new KeyValuePair<string, string>("columns[15][search][value]", ""),
				new KeyValuePair<string, string>("columns[15][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[16][data]", "16"),
				new KeyValuePair<string, string>("columns[16][name]", ""),
				new KeyValuePair<string, string>("columns[16][searchable]", "true"),
				new KeyValuePair<string, string>("columns[16][orderable]", "true"),
				new KeyValuePair<string, string>("columns[16][search][value]", ""),
				new KeyValuePair<string, string>("columns[16][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[17][data]", "17"),
				new KeyValuePair<string, string>("columns[17][name]", ""),
				new KeyValuePair<string, string>("columns[17][searchable]", "true"),
				new KeyValuePair<string, string>("columns[17][orderable]", "true"),
				new KeyValuePair<string, string>("columns[17][search][value]", ""),
				new KeyValuePair<string, string>("columns[17][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[18][data]", "18"),
				new KeyValuePair<string, string>("columns[18][name]", ""),
				new KeyValuePair<string, string>("columns[18][searchable]", "true"),
				new KeyValuePair<string, string>("columns[18][orderable]", "true"),
				new KeyValuePair<string, string>("columns[18][search][value]", ""),
				new KeyValuePair<string, string>("columns[18][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[19][data]", "19"),
				new KeyValuePair<string, string>("columns[19][name]", ""),
				new KeyValuePair<string, string>("columns[19][searchable]", "true"),
				new KeyValuePair<string, string>("columns[19][orderable]", "true"),
				new KeyValuePair<string, string>("columns[19][search][value]", ""),
				new KeyValuePair<string, string>("columns[19][search][regex]", "false"),
				new KeyValuePair<string, string>("columns[20][data]", "20"),
				new KeyValuePair<string, string>("columns[20][name]", ""),
				new KeyValuePair<string, string>("columns[20][searchable]", "true"),
				new KeyValuePair<string, string>("columns[20][orderable]", "true"),
				new KeyValuePair<string, string>("columns[20][search][value]", ""),
				new KeyValuePair<string, string>("columns[20][search][regex]", "false"),
        };

        /// <summary>
        /// Gets the form data required to perform a search by song and artist only.
        /// </summary>
        /// <param name="search">The term(s) to search for.</param>
        /// <returns>Returns a FormUrlEncodedContent for searching by song and artist.</returns>
        public static HttpContent GetFormData(string search, string artist, string song, int resultsPerPage, int pageNumber)
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("columns[2][search][value]", song));
            list.Add(new KeyValuePair<string, string>("columns[1][search][value]", artist));
            list.Add(new KeyValuePair<string, string>("search[value]", search));
            list.Add(new KeyValuePair<string, string>("start", (resultsPerPage * pageNumber).ToString()));
            list.Add(new KeyValuePair<string, string>("length", resultsPerPage.ToString()));
            list.AddRange(_baseFormData);
            return new FormUrlEncodedContent(list);
        }

        private static string GetRequestParameter(string parm, ref string search)
        {
            parm += ":";
            int index = search.IndexOf(parm);
            int adjStartIndex = index + parm.Length;
            if (index == -1 || adjStartIndex >= search.Length - 1)
            {
                return string.Empty;
            }

            int endIndex = search.IndexOf(" ", index);
            if (endIndex == -1)
            {
                endIndex = search.Length - 1;
            }

            var result = search.Substring(adjStartIndex, endIndex - adjStartIndex + 1);
            search = search.Remove(index, endIndex - index + 1);

            return result;
        }
    }
}
