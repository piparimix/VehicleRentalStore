using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VehicleRentalStore.Models;
using VehicleRentalStore.Services;

namespace VehicleRentalStore.ViewModels
{
    public class CustomerListViewModel : ObservableObject
    {
        private readonly IDataService _dataService;

        private ObservableCollection<Customer> _customers = new();

        // The property the XAML DataGrid is binding to
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value); // Notifies the UI!
        }

        public CustomerListViewModel(IDataService dataService)
        {
            _dataService = dataService;

            // Load the data as soon as the ViewModel is created
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            // Fetch the customers using the method we added to SqlDataService
            var customers = await _dataService.GetAllCustomersAsync();

            // Assign to the public property to trigger the UI update
            Customers = new ObservableCollection<Customer>(customers);
        }
    }
}
