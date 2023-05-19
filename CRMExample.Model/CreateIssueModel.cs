using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRMExample.Model
{
    public class CreateIssueModel
    {
        [Display(Name = "Özet")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [StringLength(400, ErrorMessage = "{0} alanı en fazla {1} karakter olabilir.")]
        public string Summary { get; set; }

        [Display(Name = "Son Tarih")]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Atanan")]
        public int UserId { get; set; }
    }

    public class EditIssueModel
    {
        [Display(Name = "Özet")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [StringLength(400, ErrorMessage = "{0} alanı en fazla {1} karakter olabilir.")]
        public string Summary { get; set; }

        [Display(Name = "Son Tarih")]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Durumu")]
        public bool Completed { get; set; }

        [Display(Name = "Atanan")]
        public int UserId { get; set; }
    }
}

