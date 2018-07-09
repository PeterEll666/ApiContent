using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiContent.DataAccess
{
    public class Filter
    {
        public Filter(string filter)
        {
            if (filter == null)
            {
                return;
            }
            var properties = this.GetType().GetProperties();
            string[] filters = filter.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var filt in filters)
            {
                string[] items = filt.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                if (items.Count() == 2)
                {
                    foreach (var prop in properties)
                    {
                        if (items[0].ToLower() == prop.Name.ToLower())
                        {
                            prop.SetValue(this, Convert.ChangeType(items[1], prop.PropertyType));
                        }
                    }
                }
            }
        }
    }
}