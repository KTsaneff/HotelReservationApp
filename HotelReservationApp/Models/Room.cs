using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApp.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public string Type { get; set; } = null!;
        public decimal PricePerNight { get; set; }
        public bool IsBookable { get; set; } = true;
    }
}
