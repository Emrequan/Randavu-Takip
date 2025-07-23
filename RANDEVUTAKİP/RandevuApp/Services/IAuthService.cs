using RandevuApp.Models;
using System.Threading.Tasks;

namespace RandevuApp.Services
{
    public interface IAuthService
    {
        Task<bool> GirisYapAsync(KullaniciGirisModel model);
    }
}
