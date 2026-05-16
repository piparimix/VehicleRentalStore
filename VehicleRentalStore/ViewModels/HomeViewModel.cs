using System;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;

namespace VehicleRentalStore.ViewModels
{
    public class HomeViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;

        // This acts as a communication pipe to the MainViewModel
        public Action<ObservableObject> RequestNavigation { get; set; }

        public ICommand OpenRegisterCustomerCommand { get; }

        public HomeViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            OpenRegisterCustomerCommand = new RelayCommand(ExecuteOpenRegisterCustomer);
        }

        private void ExecuteOpenRegisterCustomer(object? parameter)
        {
            // 1. Get a fresh instance of the registration ViewModel from your DI container
            var registerVm = _serviceProvider.GetRequiredService<RegisterCustomerViewModel>();

            // 2. Fire the action to tell MainViewModel to put it on the screen!
            RequestNavigation?.Invoke(registerVm);
        }
    }
}