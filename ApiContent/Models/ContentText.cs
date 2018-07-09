using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApiContent.Models
{
    public class ContentText
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Seq { get; set; }

        [Required]
        public int ParentContentId { get; set; }
        [ForeignKey("ParentContentId")]
        public Content ParentContent { get; set; }

        public int ContentTextId { get; set; }
        [ForeignKey("ContentTextId")]
        public Text Text { get; set; }
    }
}