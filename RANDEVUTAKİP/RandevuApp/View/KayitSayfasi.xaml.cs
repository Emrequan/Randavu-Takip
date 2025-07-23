using System;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Maui.Controls;

namespace RandevuApp;

public partial class KayitSayfasi : ContentPage
{
    public KayitSayfasi()
    {
        InitializeComponent();
    }

    private async void OnKayitOlClicked(object sender, EventArgs e)
    {
        string isim = isimEntry.Text?.Trim();
        string soyisim = soyisimEntry.Text?.Trim();
        string email = emailEntry.Text?.Trim();
        string sifre = sifreEntry.Text;
        string sifreTekrar = sifreTekrarEntry.Text;

        if (string.IsNullOrWhiteSpace(isim) || string.IsNullOrWhiteSpace(soyisim) ||
            string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(sifre) ||
            string.IsNullOrWhiteSpace(sifreTekrar))
        {
            mesajLabel.Text = "Lütfen tüm alanları doldurun.";
            return;
        }

        if (sifre != sifreTekrar)
        {
            mesajLabel.Text = "Şifreler eşleşmiyor.";
            return;
        }

        mesajLabel.Text = "";

        try
        {
            var kayitModel = new
            {
                Isim = isim,
                Soyisim = soyisim,
                Email = email,
                Sifre = sifre
            };

            using HttpClient client = new()
            {
                BaseAddress = new Uri("http://10.0.2.2:5237/")
            };

            var response = await client.PostAsJsonAsync("kayit", kayitModel);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Başarılı", "Kayıt işlemi tamamlandı.", "Tamam");
                await Navigation.PopAsync();
            }
            else
            {
                string hataMesaji = await response.Content.ReadAsStringAsync();
                mesajLabel.Text = $"Kayıt başarısız: {hataMesaji}";
            }
        }
        catch (Exception ex)
        {
            mesajLabel.Text = $"Hata: {ex.Message}";
        }
    }
}
