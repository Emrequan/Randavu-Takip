using System;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Maui.Controls;
using System.Linq; // LINQ için

namespace RandevuApp;

public partial class RandevuOlusturPage : ContentPage
{
    private readonly HttpClient _httpClient = new HttpClient();

    public RandevuOlusturPage()
    {
        InitializeComponent();
    }
    private async void OnRandevuOlusturClicked(object sender, EventArgs e)
    {
        string baslik = baslikEntry.Text?.Trim();
        string aciklama = aciklamaEditor.Text?.Trim();
        DateTime tarih = tarihPicker.Date;
        TimeSpan saat = saatPicker.Time;

        string randevuTuru = randevuTuruPicker.SelectedItem as string;
        if (string.IsNullOrEmpty(randevuTuru))
        {
            await DisplayAlert("Hata", "Lütfen bir randevu türü seçin.", "Tamam");
            return;
        }

        bool hatirlatmaIstendi = hatirlatmaCheckbox.IsChecked;

        string tekrarlama = "Yok";
        if (tekrarGunluk.IsChecked) tekrarlama = "Günlük";
        else if (tekrarHaftalik.IsChecked) tekrarlama = "Haftalık";

        if (string.IsNullOrWhiteSpace(baslik))
        {
            await DisplayAlert("Hata", "Randevu başlığı boş olamaz.", "Tamam");
            return;
        }

        DateTime randevuZamani = tarih.Date + saat;

        var yeniRandevu = new
        {
            AdSoyad = baslik,
            Tarih = randevuZamani,
            Aciklama = aciklama,
            RandevuTuru = randevuTuru,
            Hatirlatma = hatirlatmaIstendi,
            Tekrarlama = tekrarlama
        };

        string apiUrl = "http://10.0.2.2:5237/randevular";

        try
        {
            var response = await _httpClient.PostAsJsonAsync(apiUrl, yeniRandevu);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Başarılı", $"Randevu {randevuZamani:dd.MM.yyyy HH:mm} tarihinde oluşturuldu.", "Tamam");
                await Navigation.PopAsync();
            }
            else
            {
                string hataMesaji = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Hata", $"Randevu oluşturulamadı: {hataMesaji}", "Tamam");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"İstek sırasında hata oluştu: {ex.Message}", "Tamam");
        }
    }

}
