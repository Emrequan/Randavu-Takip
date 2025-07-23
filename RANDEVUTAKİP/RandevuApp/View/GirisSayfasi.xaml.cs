using System;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Maui.Controls;
using RandevuApp.Models;
using RandevuApp.Views;

namespace RandevuApp.View
{
    public partial class GirisSayfasi : ContentPage
    {
        public GirisSayfasi()
        {
            InitializeComponent();
        }

        private async void OnGirisYapClicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text;
            string sifre = sifreEntry.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(sifre))
            {
                await DisplayAlert("Hata", "Email ve şifre boş olamaz.", "Tamam");
                return;
            }

            var loginModel = new { Email = email, Sifre = sifre };

            try
            {
                using HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://10.0.2.2:5237/");

                // Burada API'deki endpoint ile aynı olmalı
                HttpResponseMessage response = await client.PostAsJsonAsync("giris", loginModel);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Başarılı", "Giriş başarılı!", "Tamam");
                    await Navigation.PushAsync(new AnaSayfa());
                }
                else
                {
                    string errorMsg = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Hata", "Giriş başarısız: " + errorMsg, "Tamam");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", "Bağlantı hatası: " + ex.Message, "Tamam");
            }
        }

        private async void OnSifremiUnuttumClicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert("Hata", "Lütfen e-posta adresinizi girin.", "Tamam");
                return;
            }

            await Navigation.PushAsync(new SifreDegistirPage(email));
        }


        private async void OnKayitOlTapped(object sender, EventArgs e)
        {
            // Kayıt sayfasına yönlendirme yapılıyor
            await Navigation.PushAsync(new KayitSayfasi());
        }
    }
}
