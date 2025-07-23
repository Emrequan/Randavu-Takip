using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Syncfusion.Maui.DataGrid;
using RandevuApp.Models;
using Microsoft.Maui.Dispatching;
namespace RandevuApp.View;

public partial class RandevuListesiPage : ContentPage
{
    public ObservableCollection<Randevu> Randevular { get; set; } = new ObservableCollection<Randevu>();

    private readonly HttpClient _httpClient = new HttpClient();

    public RandevuListesiPage()
    {
        InitializeComponent();
        BindingContext = this;

        // Sayfa yüklendiğinde randevuları yükle
        Loaded += async (s, e) => await LoadRandevularAsync();
    }

    private async Task LoadRandevularAsync()
    {
        try
        {
            string apiUrl = "http://10.0.2.2:5237/randevular";
            var randevularFromApi = await _httpClient.GetFromJsonAsync<Randevu[]>(apiUrl);

            if (randevularFromApi != null)
            {
                Randevular.Clear();
                foreach (var randevu in randevularFromApi)
                {
                    Randevular.Add(randevu);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"Randevular yüklenirken hata oluştu: {ex.Message}", "Tamam");
        }
    }

    private async void OnRandevuSilClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is int randevuId)
        {
            bool onay = await DisplayAlert("Silme Onayı", "Bu randevuyu silmek istediğinize emin misiniz?", "Evet", "Hayır");
            if (!onay)
                return;

            try
            {
                string apiUrl = $"http://10.0.2.2:5237/randevular/{randevuId}";
                var response = await _httpClient.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var randevuToRemove = Randevular.FirstOrDefault(r => r.Id == randevuId);
                    if (randevuToRemove != null)
                    {
                        Randevular.Remove(randevuToRemove);
                    }

                    await DisplayAlert("Başarılı", "Randevu başarıyla silindi.", "Tamam");
                }
                else
                {
                    string hataMesaji = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Hata", $"Randevu silinemedi: {hataMesaji}", "Tamam");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"İstek sırasında hata oluştu: {ex.Message}", "Tamam");
            }
        }
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Randevu secilenRandevu)
        {
            string tarihStr = secilenRandevu.Tarih.ToString("dd.MM.yyyy HH:mm");
            await DisplayAlert(secilenRandevu.AdSoyad, $"{tarihStr}\n{secilenRandevu.Aciklama}", "Tamam");

            // Seçimi temizle ki tekrar seçilebilsin
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}