using CFScraper.Contracts;
using CFScraper.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFScraper.Domain
{
    class SearchService : ISearchService
    {
        private IWebScraper _webScraper;
        public SearchService(IWebScraper webScraper)
        {
            _webScraper = webScraper;
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
