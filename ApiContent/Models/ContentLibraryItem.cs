using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApiContent.Models
{
    public class ContentLibraryItem
    {
        [Key]
        [Column(Order = 1)]
        public string LibraryName { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required]
        public int ContentId { get; set; }
        [ForeignKey("ContentId")]
        public Content Child { get; set; }
    }
}