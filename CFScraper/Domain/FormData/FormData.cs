using System;
using System.Collections.Generic;
using System.Text;

namespace CFScraper.Domain.FormData
{
    internal class FormData : Attribute
    {
        public FormData(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}
