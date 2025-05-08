using HotelReservationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationApp.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        // Add more DbSets for Reservations, Guests, Bills later
    }
}
