using System.ComponentModel.DataAnnotations;

namespace RandevuTakipAPI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        public string Sifre { get; set; } = null!;
    }
}
