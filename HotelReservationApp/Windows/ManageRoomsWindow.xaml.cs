using HotelReservationApp.Data;
using HotelReservationApp.Models;
using HotelReservationApp.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;


namespace HotelReservationApp.Windows
{
    public partial class ManageRoomsWindow : Window
    {
        public Action OnRoomAdded { get; set; }

        public ManageRoomsWindow()
        {
            InitializeComponent();
            RoomTypeComboBox.ItemsSource = RoomTypes.All;
        }

        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(RoomNumberTextBox.Text, out int roomNumber)
                && decimal.TryParse(PriceTextBox.Text, out decimal price)
                && RoomTypeComboBox.SelectedItem != null)
            {
                using var db = new HotelDbContextFactory().CreateDbContext(null);

                var room = new Room
                {
                    RoomNumber = roomNumber,
                    Type = RoomTypeComboBox.SelectedItem?.ToString() ?? "Unknown",
                    PricePerNight = price,
                    IsBookable = IsBookableCheckBox.IsChecked == true
                };

                db.Rooms.Add(room);
                db.SaveChanges();

                MessageBox.Show("Room added successfully!");
                RoomNumberTextBox.Clear();
                RoomTypeComboBox.SelectedIndex = 0;
                PriceTextBox.Clear();
                IsBookableCheckBox.IsChecked = true;

                OnRoomAdded?.Invoke();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter valid values.");
            }
        }
    }
}
