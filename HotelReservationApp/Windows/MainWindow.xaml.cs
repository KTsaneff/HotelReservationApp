using System.Windows;

namespace HotelReservationApp.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new HomeView(this);
        }        

        public void NavigateToHome()
        {
            MainContent.Content = new HomeView(this);
        }

        public void NavigateToReservationCalendar()
        {
            MainContent.Content = new ReservationCalendarView(this);
        }

        internal void NavigateToUpcomingReservations()
        {
            MainContent.Content = new UpcomingReservationsView(this);
        }
    }
}
