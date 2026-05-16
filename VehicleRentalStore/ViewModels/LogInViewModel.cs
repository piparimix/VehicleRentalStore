using System.Windows.Controls;
using System.Windows.Input;
using VehicleRentalStore.Models;
using VehicleRentalStore.Services;
// using VehicleRentalStore.Models; // You will need this later for Employee
// using VehicleRentalStore.Services; // You will need this later for SqlDataService

namespace VehicleRentalStore.ViewModels
{
    public class LogInViewModel : ObservableObject
    {
        private readonly IDataService _dataService;
        private string _email = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _hasError;

        // Binds to the Email textbox
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        // Binds to the red error text
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (SetProperty(ref _errorMessage, value))
                {
                    // Automatically show/hide the textblock if there is a message
                    HasError = !string.IsNullOrWhiteSpace(value);
                }
            }
        }

        // Used by the BoolToVis converter
        public bool HasError
        {
            get => _hasError;
            set => SetProperty(ref _hasError, value);
        }

        // The command the button actually calls
        public ICommand LoginCommand { get; }

        public LogInViewModel(IDataService dataService)
        {
            _dataService = dataService;
            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        public Action<Employee>? OnLoginSuccess { get; set; }
        private async void ExecuteLogin(object? parameter)
        {
            if (parameter is PasswordBox passwordBox)
            {
                string securePasswordEntered = passwordBox.Password;
                ErrorMessage = string.Empty;

                // 1. Fetch the employee from the database
                var employee = await _dataService.GetEmployeeByEmailAsync(Email);

                // 2. Verify the employee exists and the password matches
                // NOTE: In a real application, you would use a hashing algorithm like BCrypt
                // e.g., BCrypt.Net.BCrypt.Verify(securePasswordEntered, employee.PasswordHash)
                if (employee != null && employee.PasswordHash == securePasswordEntered)
                {
                    // Trigger the success action and pass the user
                    OnLoginSuccess?.Invoke(employee);
                }
                else
                {
                    ErrorMessage = "Invalid email or password.";
                }
            }
        }
    }
}