using System.ComponentModel.DataAnnotations;

namespace RandevuTakip.Models
{
    public class KullaniciKayitModel
    {
        [Required(ErrorMessage = "İsim zorunludur.")]
        public string Isim { get; set; } = null!;

        [Required(ErrorMessage = "Soyisim zorunludur.")]
        public string Soyisim { get; set; } = null!;

        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        public string Sifre { get; set; } = null!;
    }
}
