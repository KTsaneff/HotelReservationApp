using System.Windows;
using System.Windows.Controls;

namespace HotelReservationApp.Windows
{
    public partial class UpcomingReservationsView : UserControl
    {
        private readonly MainWindow _main;

        public UpcomingReservationsView(MainWindow main)
        {
            InitializeComponent();
            _main = main;
        }

        private void BackToHome_Click(object sender, RoutedEventArgs e)
        {
            _main.NavigateToHome();
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            // Placeholder to ensure the project builds
            MessageBox.Show("Details button clicked. Feature coming soon!");
        }

    }
}
