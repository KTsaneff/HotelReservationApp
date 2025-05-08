using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using HotelReservationApp.Data;
using System.Windows.Shapes;
using System.Data;
using System.Globalization;
using System.Windows.Input;

namespace HotelReservationApp.Windows
{
    public partial class HomeView : UserControl
    {
        private readonly MainWindow _main;
        private DispatcherTimer? timer;
        private string calcInput = string.Empty;

        public HomeView(MainWindow main)
        {
            InitializeComponent();
            _main = main;

            MonthText.Text = DateTime.Now.ToString("MMMM");
            DayText.Text = DateTime.Now.Day.ToString();
            WeekDayText.Text = DateTime.Now.DayOfWeek.ToString();

            var leftPanel = new HomeLeftPanel();
            leftPanel.MakeReservationRequested += HandleMakeReservation;
            leftPanel.ReservationCalendarRequested += HandleReservationCalendar;
            leftPanel.UpcomingReservationsRequested += HandleUpcomingReservations;

            LeftPanelContainer.Content = leftPanel;
        }

        private void HandleMakeReservation(object? sender, EventArgs e)
        {
            var reservationView = new NewReservationView(_main);
            reservationView.BackToHomeRequested += HandleBackToHome;
            LeftPanelContainer.Content = reservationView;
        }

        private void HandleReservationCalendar(object? sender, EventArgs e)
        {
            _main.NavigateToReservationCalendar();
        }

        private void CurrentAccommodations_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Opening Current Accommodations window...");
        }

        private void NewReservation_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Opening New Reservation form...");
        }

        private void HandleUpcomingReservations(object? sender, EventArgs e)
        {
            _main.NavigateToUpcomingReservations();
        }

        private void PastReservations_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate to Past Reservations view
            MessageBox.Show("Past Reservations clicked!");
        }


        private void SetupClock()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += UpdateClock;
            timer.Start();
        }

        private void UpdateClock(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            double secondAngle = now.Second * 6;
            double minuteAngle = now.Minute * 6 + now.Second * 0.1;
            double hourAngle = (now.Hour % 12) * 30 + now.Minute * 0.5;

            SetHand(SecondHand, secondAngle, 75);
            SetHand(MinuteHand, minuteAngle, 65);
            SetHand(HourHand, hourAngle, 45);
        }

        private void SetHand(Line hand, double angle, double length)
        {
            double rad = angle * Math.PI / 180;
            double centerX = 100;
            double centerY = 100;

            double x = centerX + length * Math.Sin(rad);
            double y = centerY - length * Math.Cos(rad);

            hand.X2 = x;
            hand.Y2 = y;
        }

        private void CalcButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string value = button.Content.ToString();

                // Add spacing if needed
                if ("÷×−+%".Contains(value))
                    CalcDisplay.Text += $" {value} ";
                else
                    CalcDisplay.Text += value;
            }
        }


        private void CalcClear_Click(object sender, RoutedEventArgs e)
        {
            CalcDisplay.Text = string.Empty;
        }

        private void CalcBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CalcDisplay.Text))
            {
                CalcDisplay.Text = CalcDisplay.Text[..^1];
            }
        }

        private void CalcEquals_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string input = CalcDisplay.Text;

                // Replace user-friendly operators with C#-compatible ones
                input = input.Replace("×", "*").Replace("÷", "/").Replace("−", "-");

                // Handle percentages: e.g., 1230 - 15% => 1230 - (1230 * 15 / 100)
                if (input.Contains('%'))
                {
                    input = ProcessPercentage(input);
                }

                // Evaluate the final expression using DataTable
                var result = new System.Data.DataTable().Compute(input, null);
                CalcDisplay.Text = result.ToString();
            }
            catch
            {
                CalcDisplay.Text = "Error";
            }
        }

        private string ProcessPercentage(string input)
        {
            int percentIndex = input.IndexOf('%');
            if (percentIndex == -1)
                return input;

            // Go backward to find the start of the number before '%'
            int end = percentIndex - 1;
            int start = end;
            while (start >= 0 && (char.IsDigit(input[start]) || input[start] == '.'))
            {
                start--;
            }
            start++; // move back to start of the number

            string percentValueStr = input.Substring(start, percentIndex - start);
            if (!double.TryParse(percentValueStr, out double percentValue))
                throw new FormatException();

            percentValue /= 100;

            // Replace the percentage portion with its decimal equivalent
            string before = input.Substring(0, start);
            string after = input.Substring(percentIndex + 1);

            return before + percentValue.ToString("G", CultureInfo.InvariantCulture) + after;
        }

        private void CalcDisplay_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true; // prevent default behavior

            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                CalcDisplay.Text += e.Key.ToString().Last();
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                CalcDisplay.Text += (char)('0' + (e.Key - Key.NumPad0));
            }
            else if (e.Key == Key.Add || e.Key == Key.OemPlus)
            {
                CalcDisplay.Text += "+";
            }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
            {
                CalcDisplay.Text += "−";
            }
            else if (e.Key == Key.Multiply)
            {
                CalcDisplay.Text += "×";
            }
            else if (e.Key == Key.Divide)
            {
                CalcDisplay.Text += "÷";
            }
            else if (e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {
                CalcDisplay.Text += ".";
            }
            else if (e.Key == Key.Back)
            {
                if (CalcDisplay.Text.Length > 0)
                    CalcDisplay.Text = CalcDisplay.Text.Substring(0, CalcDisplay.Text.Length - 1);
            }
            else if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                CalcEquals_Click(null, null);
            }
            else if (e.Key == Key.C)
            {
                CalcClear_Click(null, null);
            }
            else if (e.Key == Key.D5 && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                CalcDisplay.Text += "%";
            }
        }

        private void HandleBackToHome(object? sender, EventArgs e)
        {
            var homeLeftPanel = new HomeLeftPanel();
            homeLeftPanel.MakeReservationRequested += HandleMakeReservation;
            homeLeftPanel.ReservationCalendarRequested += HandleReservationCalendar;
            LeftPanelContainer.Content = homeLeftPanel;
        }
    }
}
