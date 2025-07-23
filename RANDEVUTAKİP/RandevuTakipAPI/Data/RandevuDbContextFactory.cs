using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RandevuTakipAPI.Data
{
    public class RandevuDbContextFactory : IDesignTimeDbContextFactory<RandevuDbContext>
    {
        public RandevuDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RandevuDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=RandevuDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;");

            return new RandevuDbContext(optionsBuilder.Options);
        }
    }
}
