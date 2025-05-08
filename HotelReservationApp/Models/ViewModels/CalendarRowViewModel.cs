namespace HotelReservationApp.Models.ViewModels
{
    public class CalendarRowViewModel
    {
        public int RoomNumber { get; set; }
        public List<string> Days { get; set; } = new();
    }

}
