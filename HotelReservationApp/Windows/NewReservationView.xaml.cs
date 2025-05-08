using System.Windows;
using System.Windows.Controls;

namespace HotelReservationApp.Windows
{
    /// <summary>
    /// Interaction logic for NewReservationView.xaml
    /// </summary>
    public partial class NewReservationView : UserControl
    {
        public event EventHandler? BackToHomeRequested;
        public NewReservationView()
        {
            InitializeComponent();
        }

        private void SaveReservation_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Reservation saved!");
            // TODO: Save to database later
        }

        private void BackToHome_Click(object sender, RoutedEventArgs e)
        {
            BackToHomeRequested?.Invoke(this, EventArgs.Empty);
        }

        private void NightsBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (StartDatePicker.SelectedDate == null)
                return;

            if (int.TryParse(NightsBox.Text, out int nights) && nights > 0)
            {
                DateTime startDate = StartDatePicker.SelectedDate.Value;
                DateTime endDate = startDate.AddDays(nights);
                EndDatePicker.SelectedDate = endDate;
            }
            else
            {
                EndDatePicker.SelectedDate = null;
            }
        }

        private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            NightsBox_TextChanged(null, null); // Trigger recalculation if NightsBox has value
        }

    }
}
