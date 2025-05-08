using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HotelReservationApp.Data
{
    public class HotelDbContextFactory : IDesignTimeDbContextFactory<HotelDbContext>
    {
        public HotelDbContext CreateDbContext(string[] args)
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hotel.db");

            var optionsBuilder = new DbContextOptionsBuilder<HotelDbContext>();
            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            return new HotelDbContext(optionsBuilder.Options);
        }
    }
}
