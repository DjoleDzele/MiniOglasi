using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniOglasi.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Unesite vas email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Unesite pravilan email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Unesite vasu sifru")]
        [DataType(DataType.Password)]
        [Display(Name = "Sifra")]
        public string Password { get; set; }

        [Display(Name = "Ostani prijavljen?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Unesite vas telefon")]
        [Display(Name = "Kontakt telefon")]
        [RegularExpression(
            @"0\d{2}-\d{3}-\d{3,4}",
            ErrorMessage = "Pogresan format telefona, unesite 0XX-XXX-XXX(X) ")]
        public string KontaktTelefon { get; set; }

        [Required(ErrorMessage = "Izaberite vas grad")]
        [Display(Name = "Izaberite vas grad")]
        public int GradId { get; set; }

        public ICollection<Grad> Gradovi { get; set; }

        //MOJE DODATO <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        [Required(ErrorMessage = "Email je obavezan")]
        [EmailAddress(ErrorMessage = "Unesite validnu email adresu")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Sifra je obavezna")]
        [StringLength(100, ErrorMessage = "{0} mora imati bar {2} karaktera.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Sifra")]
        [RegularExpression(@"^(?=.*[a-z]+)(?=.*[A-Z]+)(?=.*\d)(?=.*[^\da-zA-Z]+).{6,}$",
            ErrorMessage = "Minimum 1 veliko i malo slovo, broj i simbol")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Ponovi sifru")]
        [Compare("Password", ErrorMessage = "Sifre se ne podudaraju")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}