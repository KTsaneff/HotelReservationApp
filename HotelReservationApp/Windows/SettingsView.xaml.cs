using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace HotelReservationApp.Windows
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //if (LanguageComboBox.SelectedItem is ComboBoxItem selected)
            //{
            //    string culture = selected.Tag.ToString();
            //    Properties.Settings.Default.AppCulture = culture;
            //    Properties.Settings.Default.Save();

            //    MessageBox.Show("Please restart the application to apply the new language setting.", "Language Changed");
            //}
        }
    }
}
