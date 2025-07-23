using RandevuApp.Models;
using System.Net.Http.Json;
using RandevuApp.View;
using RandevuApp;  // Veya RandevuOlusturPage'in namespace'i neyse onu yaz

namespace RandevuApp.Views;

public partial class AnaSayfa : ContentPage
{
    private readonly HttpClient _httpClient;
    private List<Randevu> _tumRandevular = new();

    public AnaSayfa()
    {
        InitializeComponent();

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://10.0.2.2:5237/") // Buraya kendi API adresinizi yazın
        };
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await RandevulariYukle();
    }

    private async Task RandevulariYukle()
    {
        try
        {
            var randevular = await _httpClient.GetFromJsonAsync<List<Randevu>>("randevular");
            if (randevular != null)
            {
                _tumRandevular = randevular;
                CvRandevuListesi.ItemsSource = _tumRandevular;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "Randevular yüklenirken hata oluştu: " + ex.Message, "Tamam");
        }
    }

    private void DpFiltreTarih_DateSelected(object sender, DateChangedEventArgs e)
    {
        var secilenTarih = e.NewDate.Date;
        var filtreliListe = _tumRandevular
            .Where(r => r.Tarih.Date == secilenTarih)
            .ToList();

        CvRandevuListesi.ItemsSource = filtreliListe;

    }

    private async void BtnYeniRandevu_Clicked(object sender, EventArgs e)
    {
        // Yeni randevu sayfasına navigasyon
        await Navigation.PushAsync(new RandevuOlusturPage());

    }

    private async void BtnCikis_Clicked(object sender, EventArgs e)
    {
        // Çıkış işlemi örneği (ör: token temizle, ana sayfaya dön)
        // Burada gerçek çıkış kodunu eklemelisin
        await DisplayAlert("Çıkış", "Çıkış yapıldı.", "Tamam");
        await Navigation.PopToRootAsync();
    }

    private async void BtnRandevularim_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RandevuListesiPage());
    }
}
