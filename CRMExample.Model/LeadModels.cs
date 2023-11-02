using CRMExample.Entities;
using System.ComponentModel.DataAnnotations;

namespace CRMExample.Model
{
    public class CreateLeadModel
    {
        [Display(Name = "Özet")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [StringLength(250, ErrorMessage = "{0} alanı en fazla {1} karakter olabilir.")]
        public string Summary { get; set; }

        [Display(Name = "Açıklama")]
        [StringLength(250, ErrorMessage = "{0} alanı en fazla {1} karakter olabilir.")]
        public string Desc { get; set; }

        [Display(Name = "Bedeli")]
        public decimal Price { get; set; }

        [Display(Name = "Atanan")]
        public int UserId { get; set; }

        [Display(Name = "Müşteri")]
        public int ClientId { get; set; }
    }

    public class EditLeadModel
    {
        [Display(Name = "Özet")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [StringLength(250, ErrorMessage = "{0} alanı en fazla {1} karakter olabilir.")]
        public string Summary { get; set; }

        [Display(Name = "Açıklama")]
        [StringLength(250, ErrorMessage = "{0} alanı en fazla {1} karakter olabilir.")]
        public string Desc { get; set; }

        [Display(Name = "Bedeli")]
        public decimal Price { get; set; }

        [Display(Name = "Atanan")]
        public int UserId { get; set; }

        [Display(Name = "Müşteri")]
        public int ClientId { get; set; }

        [Display(Name = "Durum")]
        public LeadType Type { get; set; }
    }

    public class LeadTypeModel
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
}
