using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using VehicleRentalStore.Models;
using VehicleRentalStore.Services;

namespace VehicleRentalStore.ViewModels
{
    public class DashboardViewModel : ObservableObject
    {
        private readonly IDataService _dataService;

        private int _totalVehicles;
        public int TotalVehicles { get => _totalVehicles; set => SetProperty(ref _totalVehicles, value); }

        private int _totalCustomers;
        public int TotalCustomers { get => _totalCustomers; set => SetProperty(ref _totalCustomers, value); }

        private int _activeRentalsCount;
        public int ActiveRentalsCount { get => _activeRentalsCount; set => SetProperty(ref _activeRentalsCount, value); }

        private decimal _totalLifetimeRevenue;
        public decimal TotalLifetimeRevenue { get => _totalLifetimeRevenue; set => SetProperty(ref _totalLifetimeRevenue, value); }

        private ObservableCollection<Rental> _recentRentals = new();
        public ObservableCollection<Rental> RecentRentals { get => _recentRentals; set => SetProperty(ref _recentRentals, value); }

        private ObservableCollection<Invoice> _unpaidInvoices = new();
        public ObservableCollection<Invoice> UnpaidInvoices { get => _unpaidInvoices; set => SetProperty(ref _unpaidInvoices, value); }

        public DashboardViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _ = LoadDashboardDataAsync();
        }

        private async Task LoadDashboardDataAsync()
        {
            // Fetch everything for the admin view
            var vehicles = await _dataService.GetAllAsync<Vehicle>();
            var customers = await _dataService.GetAllCustomersAsync();
            var rentals = await _dataService.GetAllRentalsAsync();
            var invoices = await _dataService.GetAllInvoicesAsync();

            // Calculate totals
            TotalVehicles = vehicles.Count;
            TotalCustomers = customers.Count;
            ActiveRentalsCount = rentals.Count(r => r.Status == RentalStatus.Active);
            TotalLifetimeRevenue = invoices.Where(i => i.Status == InvoiceStatus.Paid).Sum(i => i.Total);

            // Get the 5 most recent rentals
            RecentRentals = new ObservableCollection<Rental>(
                rentals.OrderByDescending(r => r.StartDate).Take(5)
            );

            // Get the 5 most urgent unpaid invoices
            UnpaidInvoices = new ObservableCollection<Invoice>(
                invoices.Where(i => i.Status == InvoiceStatus.Unpaid).OrderBy(i => i.DueDate).Take(5)
            );
        }
    }
}