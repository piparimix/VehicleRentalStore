using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using VehicleRentalStore.Models;
using VehicleRentalStore.Services;
using VehicleRentalStore.Views;

namespace VehicleRentalStore.ViewModels
{
    public class CreateRentalViewModel : ObservableObject
    {
        private readonly IDataService _dataService;


        // Collections for dropdowns/lists
        public ObservableCollection<Customer> Customers { get; } = new();
        public ObservableCollection<Vehicle> AvailableVehicles { get; } = new();
        public ObservableCollection<Location> Locations { get; } = new();
        public ObservableCollection<InsurancePlan> InsurancePlans { get; } = new();
        // Form Fields
        private Customer? _selectedCustomer;
        public Customer? SelectedCustomer
        {
            get => _selectedCustomer;
            set => SetProperty(ref _selectedCustomer, value);
        }
        private int _startOdometer;
        public int StartOdometer
        {
            get => _startOdometer;
            set => SetProperty(ref _startOdometer, value);
        }

        private int _startFuelLevel = 100; // Default to a full tank
        public int StartFuelLevel
        {
            get => _startFuelLevel;
            set => SetProperty(ref _startFuelLevel, value);
        }

        private InsurancePlan? _selectedInsurancePlan;
        public InsurancePlan? SelectedInsurancePlan
        {
            get => _selectedInsurancePlan;
            set => SetProperty(ref _selectedInsurancePlan, value);
        }

        private decimal _securityDeposit;
        public decimal SecurityDeposit
        {
            get => _securityDeposit;
            set => SetProperty(ref _securityDeposit, value);
        }

        private Vehicle? _selectedVehicle;
        public Vehicle? SelectedVehicle
        {
            get => _selectedVehicle;
            set => SetProperty(ref _selectedVehicle, value);
        }

        private DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        private DateTime _expectedEndDate = DateTime.Now.AddDays(1);
        public DateTime ExpectedEndDate
        {
            get => _expectedEndDate;
            set => SetProperty(ref _expectedEndDate, value);
        }

        private Location? _pickupLocation;
        public Location? PickupLocation
        {
            get => _pickupLocation;
            set => SetProperty(ref _pickupLocation, value);
        }

        private Location? _dropoffLocation;
        public Location? DropoffLocation
        {
            get => _dropoffLocation;
            set => SetProperty(ref _dropoffLocation, value);
        }

        public ICommand SaveRentalCommand { get; }
        public ICommand OpenSelectVehicleCommand { get; }

        public CreateRentalViewModel(IDataService dataService)
        {

            _dataService = dataService;
            SaveRentalCommand = new RelayCommand(async (parameter) => await SaveRentalAsync(), (parameter) => CanSaveRental()
);
            OpenSelectVehicleCommand = new RelayCommand(
    (parameter) => OpenVehicleDialog()
);
            // Call an initialization method to load Customers, Locations, etc.
            _ = LoadDataAsync();
        }
        private void OpenVehicleDialog()
        {
            // Create the dialog and pass the list of vehicles
            var dialog = new SelectVehicleWindow(AvailableVehicles);

            // ShowDialog pauses the code here until the window is closed
            if (dialog.ShowDialog() == true)
            {
                // If the user clicked OK, update the selected vehicle
                SelectedVehicle = dialog.SelectedVehicle;
            }
        }
        private async Task LoadDataAsync()
        {
            try
            {
                // 1. Load Customers
                var customers = await _dataService.GetAllCustomersAsync();
                Customers.Clear();
                foreach (var customer in customers)
                {
                    Customers.Add(customer);
                }

                // 2. Load Locations
                var locations = await _dataService.GetAllLocationsAsync();
                Locations.Clear();
                foreach (var location in locations)
                {
                    Locations.Add(location);
                }

                // 3. Load Available Vehicles 
                // (Using the dates currently selected in the form)
                var availableVehicles = await _dataService.GetAvailableVehiclesAsync(StartDate, ExpectedEndDate);
                AvailableVehicles.Clear();
                foreach (var vehicle in availableVehicles)
                {
                    AvailableVehicles.Add(vehicle);
                }

                // 4. Load Insurance Plans (We need to add this to your DataService next!)
                var insurancePlans = await _dataService.GetAllInsurancePlansAsync();
                InsurancePlans.Clear();
                foreach (var plan in insurancePlans)
                {
                    InsurancePlans.Add(plan);
                }
            }
            catch (Exception ex)
            {
                // Good place to log the error or show a MessageBox if the database fails to load
                System.Diagnostics.Debug.WriteLine($"Error loading rental form data: {ex.Message}");
            }
        }

        private bool CanSaveRental()
        {
            // Validation logic: ensure required fields are not null
            return SelectedCustomer != null &&
                   SelectedVehicle != null &&
                   PickupLocation != null &&
                   DropoffLocation != null &&
                   ExpectedEndDate > StartDate;
        }

        private async Task SaveRentalAsync()
        {
            // 1. Construct the new Rental object using the form properties
            // 2. Add the SelectedVehicle to the Rental's RentedItems
            // 3. Call _dataService.AddRentalAsync(newRental)
            // 4. Navigate back or clear the form
        }
    }
}