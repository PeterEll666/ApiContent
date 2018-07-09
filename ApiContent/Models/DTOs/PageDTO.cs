using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ApiContent.Models.DTOs;

namespace ApiContent.Models
{
    public class PageDTO
    { 

        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Html { get; set; }

        public List<IncludedItemDTO> Contents { get; set; }

    }
}