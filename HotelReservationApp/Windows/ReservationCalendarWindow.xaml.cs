using System.Windows;
using System.Windows.Controls;
using HotelReservationApp.Data;
using HotelReservationApp.Models.ViewModels;

namespace HotelReservationApp.Windows
{
    public partial class ReservationCalendarView : UserControl
    {
        private readonly MainWindow _main;

        public ReservationCalendarView(MainWindow main)
        {
            InitializeComponent();
            _main = main;
            LoadCalendar();
            StartDatePicker.SelectedDate = DateTime.Today;
            EndDatePicker.SelectedDate = DateTime.Today.AddDays(6);
        }
        private void LoadCalendar()
        {
            using var db = new HotelDbContextFactory().CreateDbContext(null);
            var rooms = db.Rooms.ToList();

            var today = DateTime.Today;
            var days = Enumerable.Range(0, 7).Select(i => today.AddDays(i).ToString("MM.dd")).ToList();

            // Fill the headers
            DateHeaderList.ItemsSource = days;

            var calendarData = rooms.Select(r => new CalendarRowViewModel
            {
                RoomNumber = r.RoomNumber,
                Days = days // This can later show "Booked"/"Free"
            }).ToList();

            CalendarList.ItemsSource = calendarData;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (StartDatePicker.SelectedDate.HasValue && EndDatePicker.SelectedDate.HasValue)
            {
                var start = StartDatePicker.SelectedDate.Value;
                var end = EndDatePicker.SelectedDate.Value;

                if (start > end)
                {
                    MessageBox.Show("Start date must be before end date.");
                    return;
                }

                GenerateCalendarForPeriod(start, end);
            }
            else
            {
                MessageBox.Show("Please select both start and end dates.");
            }
        }

        private void GenerateCalendarForPeriod(DateTime startDate, DateTime endDate)
        {
            using var db = new HotelDbContextFactory().CreateDbContext(null);
            var rooms = db.Rooms.ToList();

            var days = Enumerable.Range(0, (endDate - startDate).Days + 1)
                                 .Select(i => startDate.AddDays(i).ToString("MM.dd"))
                                 .ToList();

            DateHeaderList.ItemsSource = days;

            var calendarData = rooms.Select(r => new CalendarRowViewModel
            {
                RoomNumber = r.RoomNumber,
                Days = days // No reservation data yet, just the visual calendar
            }).ToList();

            CalendarList.ItemsSource = calendarData;
        }

        private void BackToHome_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _main.NavigateToHome();
        }
    }
}
