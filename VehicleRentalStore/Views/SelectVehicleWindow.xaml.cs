using System.Collections.ObjectModel;
using System.Windows;
using VehicleRentalStore.Models;

namespace VehicleRentalStore.Views
{
    public partial class SelectVehicleWindow : Window
    {
        public Vehicle? SelectedVehicle { get; private set; }

        public SelectVehicleWindow(ObservableCollection<Vehicle> vehicles)
        {
            InitializeComponent();

            // Set the DataGrid's item source to the list passed from the ViewModel
            VehicleDataGrid.ItemsSource = vehicles;
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (VehicleDataGrid.SelectedItem is Vehicle vehicle)
            {
                SelectedVehicle = vehicle;
                DialogResult = true; // This closes the window and returns true to ShowDialog()
            }
            else
            {
                MessageBox.Show("Please select a vehicle from the list first.", "No Selection");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Closes the window and returns false
        }
    }
}