using System.DirectoryServices.ActiveDirectory;
using System.Windows;
using System.Windows.Controls;

namespace HotelReservationApp.Windows
{
    /// <summary>
    /// Interaction logic for NewReservationView.xaml
    /// </summary>
    public partial class NewReservationView : UserControl
    {
        private readonly MainWindow _main;
        public event EventHandler? BackToHomeRequested;
        public NewReservationView(MainWindow main)
        {
            _main = main;
            InitializeComponent();
        }

        private void SaveReservation_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button saveButton)
            {
                saveButton.IsEnabled = false;
                saveButton.Content = "Saved";

                try
                {
                    // TODO: Add logic to save to the database here

                    MessageBox.Show("Reservation saved!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");

                    // Re-enable the button in case of failure
                    saveButton.IsEnabled = true;
                    saveButton.Content = "💾 Save Reservation";
                }
            }
        }

        private void BackToHome_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _main.NavigateToHome();
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
