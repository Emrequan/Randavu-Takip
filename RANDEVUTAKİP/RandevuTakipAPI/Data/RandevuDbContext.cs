using Microsoft.EntityFrameworkCore;
using RandevuTakipAPI.Models;
using RandevuTakipAPI.Data;

namespace RandevuTakipAPI.Data
{
    public class RandevuDbContext : DbContext
    {
        public RandevuDbContext(DbContextOptions<RandevuDbContext> options)
            : base(options)
        {
        }

        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }

    }
}
