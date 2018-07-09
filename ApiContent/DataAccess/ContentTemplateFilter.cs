using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiContent.DataAccess
{
    public class ContentTemplateFilter : Filter
    {
        public string Name { get; set; }

        public ContentTemplateFilter(string filter) : base(filter)
        {
        }

    }
}