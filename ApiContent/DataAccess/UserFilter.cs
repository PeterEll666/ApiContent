using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiContent.DataAccess
{
    public class UserFilter : Filter
    {
        public string Email { get; set; }
        public string Name { get; set; }

        public UserFilter(string filter) : base(filter)
        {
        }

    }
}