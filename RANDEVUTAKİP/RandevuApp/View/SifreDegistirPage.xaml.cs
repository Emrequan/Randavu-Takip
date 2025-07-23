using System;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Maui.Controls;
namespace RandevuApp;

public partial class SifreDegistirPage : ContentPage
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _email; // Kullanıcının eposta adresi



    public SifreDegistirPage(string email)
    {
        InitializeComponent();
        _email = email;
    }


    private async void OnSifreDegistirClicked(object sender, EventArgs e)
    {
        string eskiSifre = eskiSifreEntry.Text?.Trim();
        string yeniSifre = yeniSifreEntry.Text?.Trim();
        string yeniSifreTekrar = yeniSifreTekrarEntry.Text?.Trim();

        if (string.IsNullOrWhiteSpace(eskiSifre) || string.IsNullOrWhiteSpace(yeniSifre) || string.IsNullOrWhiteSpace(yeniSifreTekrar))
        {
            await DisplayAlert("Hata", "Lütfen tüm alanları doldurun.", "Tamam");
            return;
        }

        if (yeniSifre != yeniSifreTekrar)
        {
            await DisplayAlert("Hata", "Yeni şifreler eşleşmiyor.", "Tamam");
            return;
        }

        string apiUrl = "http://10.0.2.2:5237/kullanicilar/sifre-sifirla";

        var sifreSifirlaModel = new SifreSifirlaModel
        {
            Email = _email,

            YeniSifre = yeniSifre
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync(apiUrl, sifreSifirlaModel);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Başarılı", "Şifreniz başarıyla değiştirildi.", "Tamam");
                await Navigation.PopAsync();
            }
            else
            {
                string hataMesaji = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Hata", $"Şifre değiştirilemedi: {hataMesaji}", "Tamam");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"İstek sırasında hata oluştu: {ex.Message}", "Tamam");
        }
    }
}

