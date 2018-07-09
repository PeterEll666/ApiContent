using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApiContent.Models
{
    public class TextLang
    {
        [Key]
        [Column(Order = 1)]
        [Required]
        [ForeignKey("Text")]
        public int Id { get; set; }
        public Text Text { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required]
        public string Language { get; set; }

        public string Html { get; set; }
    }
}