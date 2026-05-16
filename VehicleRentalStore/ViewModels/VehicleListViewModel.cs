using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VehicleRentalStore.Models;
using VehicleRentalStore.Services;

namespace VehicleRentalStore.ViewModels
{
    public class VehicleListViewModel : ObservableObject
    {
        private readonly IDataService _dataService;
        private ObservableCollection<Vehicle> _fleet = new();
        public ObservableCollection<Vehicle> Fleet => _fleet;

        // Inject the data service through the constructor
        public VehicleListViewModel(IDataService dataService)
        {
            _dataService = dataService;

            // Load the data as soon as the ViewModel is created
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var vehicles = await _dataService.GetAllAsync<Vehicle>();
            _fleet = new ObservableCollection<Vehicle>(vehicles);
        }
    }
}
