using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiContent.DataAccess
{
    public class ContentFilter : Filter
    {
        public string Name { get; set; }

        public ContentFilter(string filter) : base(filter)
        {
        }

    }
}