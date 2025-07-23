using System.Net.Http.Json;
using RandevuApp.Models;

namespace RandevuApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7250/") // API adresini buraya yaz
            };
        }

        public async Task<bool> GirisYapAsync(KullaniciGirisModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/kullanici/giris", model);
            return response.IsSuccessStatusCode;
        }
    }
}
