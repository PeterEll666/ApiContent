using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApiContent.Models
{
    public class ContentTemplateLibraryItem
    {
        [Key]
        [Column(Order = 1)]
        public string LibraryName { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required]
        public int ContentTemplateId { get; set; }
        [ForeignKey("ContentTemplateId")]
        public ContentTemplate Child { get; set; }
    }
}