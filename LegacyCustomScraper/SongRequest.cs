using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyCustomScraper
{
    public class SongRequest
    {
        public SongRequest()
        {
            Results = new List<Song>();
        }

        public enum RequestError
        {
            NoResults,
            TooManyResults
        }

        public bool Error { get; set; }
        public RequestError ErrorType { get; set; }
        public List<Song> Results { get; set; }
    }
}
