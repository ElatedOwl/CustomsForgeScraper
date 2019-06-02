using CFScraper.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFScraper.Repository
{
    interface IWebScraper
    {
        void Login();
        string SearchSongs(SearchOptions searchOptions);
    }
}
