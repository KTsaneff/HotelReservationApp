using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HotelReservationApp.Data;

namespace HotelReservationApp.Windows
{
    /// <summary>
    /// Interaction logic for ManageRoomsView.xaml
    /// </summary>
    public partial class ManageRoomsView : UserControl
    {
        private readonly MainWindow _main;
        public event EventHandler? BackToHomeRequested;
        public ManageRoomsView(MainWindow main)
        {
            InitializeComponent();
            LoadRooms();
            _main = main;
        }

        private void BackToHome_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _main.NavigateToHome();
        }

        public void LoadRooms()
        {
            RoomGrid.Children.Clear();

            using var db = new HotelDbContextFactory().CreateDbContext(null);
            var rooms = db.Rooms.ToList();

            foreach (var room in rooms)
            {
                var border = new Border
                {
                    Width = 100,
                    Height = 100,
                    Margin = new Thickness(5),
                    CornerRadius = new CornerRadius(8),
                    BorderBrush = Brushes.Red,
                    BorderThickness = new Thickness(2),
                    Background = Brushes.White,
                    Effect = new System.Windows.Media.Effects.DropShadowEffect
                    {
                        Color = Colors.Gray,
                        Direction = 320,
                        ShadowDepth = 2,
                        Opacity = 0.3,
                        BlurRadius = 5
                    },
                    RenderTransformOrigin = new Point(0.5, 0.5),
                    RenderTransform = new ScaleTransform(1, 1),
                };

                var stack = new StackPanel
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Children =
                    {
                        new TextBlock
                        {
                            Text = $"Room {room.RoomNumber}",
                            FontWeight = FontWeights.Bold,
                            TextAlignment = TextAlignment.Center,
                            Foreground = Brushes.DarkRed,
                            Margin = new Thickness(0, 10, 0, 5),
                            TextWrapping = TextWrapping.Wrap
                        },
                        new TextBlock
                        {
                            Text = room.IsBookable ? "🟢 Available" : "🔴 Occupied",
                            FontSize = 12,
                            Foreground = Brushes.DarkSlateGray,
                            TextAlignment = TextAlignment.Center
                        }
                    }
                };

                border.Child = stack;

                // Hover effect
                border.MouseEnter += (s, e) =>
                {
                    var transform = (ScaleTransform)border.RenderTransform;
                    transform.ScaleX = 1.05;
                    transform.ScaleY = 1.05;
                    border.BorderBrush = Brushes.DarkBlue;
                    border.Background = new SolidColorBrush(Color.FromRgb(230, 240, 255));
                };

                border.MouseLeave += (s, e) =>
                {
                    var transform = (ScaleTransform)border.RenderTransform;
                    transform.ScaleX = 1;
                    transform.ScaleY = 1;
                    border.BorderBrush = Brushes.Red;
                    border.Background = Brushes.White;
                };

                RoomGrid.Children.Add(border);
            }
        }
    }
}
