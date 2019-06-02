using System;
using System.Collections.Generic;
using System.Text;

namespace CFScraper.Contracts
{
    public interface ISearchService
    {
        List<Song> SearchSongs(SearchOptions searchOptions);
        byte[] DownloadSong(Song song);
    }
}
