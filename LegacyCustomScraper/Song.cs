using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyCustomScraper
{
    public class Song
    {
        private const string DOWNLOAD_BASE = "http://customsforge.com/process.php?id=";
        public virtual string Artist { get; set; }
        public virtual string Title { get; set; }
        public string Album { get; set; }
        public string Tuning { get; set; }
        public bool Official { get; set; }
        // This is the CustomsForge ID
        public virtual int SongId { get; set; }
        public SongPath Paths { get; set; }
        private string _pathsSerialized;
        public string PathsSerialized
        {
            get
            {
                if(_pathsSerialized == null)
                {
                    _pathsSerialized = JsonConvert.SerializeObject(Platforms);
                }
                return _pathsSerialized;
            }
            set
            {
                _pathsSerialized = value;
            }
        }
        public SongPlatform Platforms { get; set; }
        private string _platformsSerialized;
        public string PlatformsSerialized
        {
            get
            {
                if (_platformsSerialized == null)
                {
                    _platformsSerialized = JsonConvert.SerializeObject(Platforms);
                }
                return _platformsSerialized;
            }
            set
            {
                _platformsSerialized = value;
            }
        }

        public string Author { get; set; }
        public string Version { get; set; }
        public bool DynamicDifficulty { get; set; }
        public long Downloads { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Updated { get; set; }

        /// <summary>
        /// Gets a formatted song title.
        /// </summary>
        /// <returns>Artist - Title (Tuning); has a star in front if official.</returns>
        public string GetTitle()
        {
            string official = Official ? "☆ " : "";
            return string.Format($"{official}{Artist} - {Title} ({Tuning}) Charted by {Author}");
        }

        public string GetTitleShort()
        {
            string official = Official ? "☆ " : "";
            return string.Format($"{official}{Artist} - {Title}");
        }

        /// <summary>
        /// Gets a download link for the song.
        /// </summary>
        /// <returns>Returns the CustomsForge download link.</returns>
        public string GetDownloadLink()
        {
            return DOWNLOAD_BASE + SongId.ToString();
        }
    }
}
