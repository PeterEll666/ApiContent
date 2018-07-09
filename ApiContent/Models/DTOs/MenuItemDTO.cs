using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiContent.Models.DTOs
{
    public class MenuItemDTO
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string PageUrl { get; set;}
        public string DisplayName { get; set; }

        public List<MenuItemDTO> Children { get; set; }

    }
}