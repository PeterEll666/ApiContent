using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ApiContent.Models.DTOs;

namespace ApiContent.Models
{
    public class ContentDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int TemplateId { get; set; }

        public string Html { get; set; }

        public List<IncludedItemDTO> ContentTexts { get; set; }
        
    }
}