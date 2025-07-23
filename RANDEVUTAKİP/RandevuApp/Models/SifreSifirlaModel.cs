using System.ComponentModel.DataAnnotations;
public class SifreSifirlaModel
{

    [Required(ErrorMessage = "Email zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
    public string Email { get; set; }

    [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
    public string YeniSifre { get; set; }
}