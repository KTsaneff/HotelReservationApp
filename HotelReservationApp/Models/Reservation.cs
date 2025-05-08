using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationApp.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int? RoomId { get; set; }

        [ForeignKey(nameof(RoomId))]
        public Room? Room { get; set; }

        [Required]
        public string GuestName { get; set; } = null!;

        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PricePerNight { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Deposit { get; set; }

        [NotMapped]
        public decimal TotalSum
        {
            get
            {
                int totalNights = (EndDate - StartDate).Days;
                decimal gross = totalNights > 0 ? PricePerNight * totalNights : 0;
                return gross - (Deposit ?? 0);
            }
        }

        [Required]
        [Range(1, 10)]
        public int NumberOfAdults { get; set; }

        [Required]
        [Range(0, 10)]
        public int NumberOfChildren { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ReservationDate { get; set; } = DateTime.Now;

        public bool IsConfirmed { get; set; } = false;

        public bool HasCheckedOut { get; set; } = false;

        public bool HasCheckedIn { get; set;} = false;
    }
}
