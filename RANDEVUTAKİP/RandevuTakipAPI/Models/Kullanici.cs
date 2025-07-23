using System.ComponentModel.DataAnnotations;

namespace RandevuTakipAPI.Models
{
    public class Kullanici
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "İsim zorunludur.")]
        public string Isim { get; set; } = null!;

        [Required(ErrorMessage = "Soyisim zorunludur.")]
        public string Soyisim { get; set; } = null!;

        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalı.")]
        public string Sifre { get; set; } = null!;

        public DateTime KayitTarihi { get; set; }
    }
}
