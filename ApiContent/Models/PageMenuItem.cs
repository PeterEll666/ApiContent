using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApiContent.Models
{
    public class PageMenuItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Seq { get; set; }

        public string DisplayName { get; set; }

        [Required]
        public int? ParentMenuId { get; set; }
        [ForeignKey("ParentMenuId")]
        public PageMenuItem ParentMenu { get; set; }

        public int? PageId { get; set; }
        [ForeignKey("PageId")]
        public Page Page { get; set; }

    }
}