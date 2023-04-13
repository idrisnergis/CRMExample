using System.ComponentModel.DataAnnotations;

namespace CRMExample.Models
{
    public class AuthenticationModel
    {
        [Display(Name = "Kullanıcı Adı")]
        [StringLength(25, ErrorMessage = "{0} alanı zorunludur.")]
        public string Username { get; set; }

        [Display(Name = "Şifre")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "{0} alanı {2} ile {1} karakter arasında olabilir.")]
        public string Password { get; set; }
    }

    public class CreateUserModel
    {
        [Display(Name = "Ad")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [StringLength(60, ErrorMessage = "{0} alanı en fazla {1} karakter olabilir.")]
        public string Name { get; set; } // John Doe or Codeove


        [Display(Name = "E-Posta")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta adresi giriniz.")]
        [StringLength(60, ErrorMessage = "{0} alanı en fazla {1} karakter olabilir.")]
        public string Email { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [StringLength(25, ErrorMessage = "{0} alanı zorunludur.")]
        public string Username { get; set; }

        [Display(Name = "Şifre")]
        [StringLength(16,MinimumLength = 6, ErrorMessage = "{0} alanı {2} ile {1} karakter arasında olabilir.")]
        public string Password { get; set; }

        [Display(Name = "Şifre (Tekrar)")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "{0} alanı {2} ile {1} karakter arasında olabilir.")]
        [Compare(nameof(Password),ErrorMessage ="{0} ile {1} alanı uyuşmuyor.")]
        public string RePassword { get; set; }

        [Display(Name = "Rol")]
        [StringLength(20, ErrorMessage = "{0} alanı zorunludur.")]

        public string Role { get; set; }

        [Display(Name = "Pasif")]
        public bool Locked { get; set; }
    }

}