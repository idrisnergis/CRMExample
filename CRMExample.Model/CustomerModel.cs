using System;
using System.ComponentModel.DataAnnotations;

namespace CRMExample.Models
{
    public abstract class CustomerBaseModel
    {
        [Display(Name = "Ad Soyad / Şirket Adı")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [StringLength(60, ErrorMessage = "{0} alanı en fazla {1} karakter olabilir.")]
        public string Name { get; set; } // John Doe or Codeove


        [Display(Name = "E-Posta")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta adresi giriniz.")]
        [StringLength(60, ErrorMessage = "{0} alanı en fazla {1} karakter olabilir.")]
        public string Email { get; set; }

        [Display(Name = "Telefon")]
        [StringLength(25, ErrorMessage = "{0} alanı en fazla {1} karakter olabilir.")]
        public string Phone { get; set; }

        [Display(Name = "Açıklama")]
        [StringLength(500, ErrorMessage = "{0} alanı en fazla {1} karakter olabilir.")]
        public string Description { get; set; }

        [Display(Name = "Kurumsal")]
        public bool IsCorporate { get; set; }

        [Display(Name = "Pasif")]
        public bool Locked { get; set; }
    }

    public class CreateCustomerModel : CustomerBaseModel
    {

    }

    public class EditCustomerModel : CustomerBaseModel
    {

    }

}