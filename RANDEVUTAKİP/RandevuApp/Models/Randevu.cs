using System;
using System.ComponentModel.DataAnnotations;

namespace RandevuApp.Models
{
    public class Randevu
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad Soyad zorunludur.")]
        [StringLength(100, ErrorMessage = "Ad Soyad 100 karakterden uzun olamaz.")]
        public string AdSoyad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tarih zorunludur.")]
        public DateTime Tarih { get; set; }

        [StringLength(500, ErrorMessage = "Açıklama 500 karakterden uzun olamaz.")]
        public string? Aciklama { get; set; }
    }
}
