using CFScraper.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFScraper.Repository
{
    internal interface ICustomForgeClient
    {
        string RequestSongJson(SearchOptions searchOptions);
    }
}
