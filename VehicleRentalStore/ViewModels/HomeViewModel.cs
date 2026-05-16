using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using VehicleRentalStore.Models;
using VehicleRentalStore.Services;

namespace VehicleRentalStore.ViewModels
{
    public class HomeViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDataService _dataService;

        // Properties for the UI bindings
        private int _availableCars;
        public int AvailableCars { get => _availableCars; set => SetProperty(ref _availableCars, value); }

        private int _availableMotorcycles;
        public int AvailableMotorcycles { get => _availableMotorcycles; set => SetProperty(ref _availableMotorcycles, value); }

        private int _activeRentals;
        public int ActiveRentals { get => _activeRentals; set => SetProperty(ref _activeRentals, value); }

        private int _overdueRentals;
        public int OverdueRentals { get => _overdueRentals; set => SetProperty(ref _overdueRentals, value); }

        private int _totalCustomers;
        public int TotalCustomers { get => _totalCustomers; set => SetProperty(ref _totalCustomers, value); }

        private decimal _revenueToday;
        public decimal RevenueToday { get => _revenueToday; set => SetProperty(ref _revenueToday, value); }

        public Action<ObservableObject> RequestNavigation { get; set; }
        public ICommand OpenRegisterCustomerCommand { get; }

        public HomeViewModel(IServiceProvider serviceProvider, IDataService dataService)
        {
            _serviceProvider = serviceProvider;
            _dataService = dataService;

            OpenRegisterCustomerCommand = new RelayCommand(ExecuteOpenRegisterCustomer);

            // Fetch the dashboard stats as soon as the view loads
            _ = LoadDashboardStatsAsync();
        }

        private async Task LoadDashboardStatsAsync()
        {
            // Fetch all necessary data concurrently
            var vehiclesTask = _dataService.GetAllAsync<Vehicle>();
            var rentalsTask = _dataService.GetAllRentalsAsync();
            var customersTask = _dataService.GetAllCustomersAsync();
            var invoicesTask = _dataService.GetAllInvoicesAsync();

            await Task.WhenAll(vehiclesTask, rentalsTask, customersTask, invoicesTask);

            var vehicles = vehiclesTask.Result;
            var rentals = rentalsTask.Result;
            var customers = customersTask.Result;
            var invoices = invoicesTask.Result;

            // Calculate the statistics
            AvailableCars = vehicles.Count(v => v is Car && v.Status == ItemStatus.Available);
            AvailableMotorcycles = vehicles.Count(v => v is Motorcycle && v.Status == ItemStatus.Available);

            ActiveRentals = rentals.Count(r => r.Status == RentalStatus.Active);

            // Overdue if the rental is still active but the expected end date has passed
            OverdueRentals = rentals.Count(r => r.Status == RentalStatus.Active && r.ExpectedEndDate < DateTime.Now);

            TotalCustomers = customers.Count;

            // Calculate today's revenue based on invoices generated today
            RevenueToday = invoices
                .Where(i => i.DateIssued.Date == DateTime.Today)
                .Sum(i => i.Total);
        }

        private void ExecuteOpenRegisterCustomer(object? parameter)
        {
            var registerVm = _serviceProvider.GetRequiredService<RegisterCustomerViewModel>();
            RequestNavigation?.Invoke(registerVm);
        }
    }
}