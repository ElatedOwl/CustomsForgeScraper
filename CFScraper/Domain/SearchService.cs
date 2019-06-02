using CFScraper.Contracts;
using CFScraper.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFScraper.Domain
{
    class SearchService : ISearchService
    {
        private ICustomForgeClient _customForgeClient;
        public SearchService(ICustomForgeClient customForgeClient)
        {
            _customForgeClient = customForgeClient;
        }

        public byte[] DownloadSong(Song song)
        {
            throw new NotImplementedException();
        }

        public List<Song> SearchSongs(SearchOptions searchOptions)
        {
            throw new NotImplementedException();
        }
    }
}
