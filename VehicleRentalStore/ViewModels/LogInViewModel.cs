using System.Windows.Controls;
using System.Windows.Input;
using VehicleRentalStore.Models;
// using VehicleRentalStore.Models; // You will need this later for Employee
// using VehicleRentalStore.Services; // You will need this later for SqlDataService

namespace VehicleRentalStore.ViewModels
{
    public class LogInViewModel : ObservableObject
    {
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

        public LogInViewModel()
        {
            // Initialize the command inside the constructor
            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        public Action<Employee>? OnLoginSuccess { get; set; }
        private void ExecuteLogin(object? parameter)
        {
            if (parameter is PasswordBox passwordBox)
            {
                string securePasswordEntered = passwordBox.Password;
                ErrorMessage = string.Empty;

                // TODO: Your actual database check goes here.
                // For now, let's fake a successful login for testing!
                if (Email == "admin" && securePasswordEntered == "admin")
                {
                    var loggedInUser = new Employee
                    {
                        FirstName = "Jesse",
                        LastName = "Mikkonen",
                        Role = EmployeeRole.Staff, // Try changing this to Staff to test the dynamic menu!
                        Email = "admin@rentalstore.com",
                        PhoneNumber = "555-0199",
                        Address = "123 Admin Street",
                    };

                    // 2. Trigger the success action and pass the user
                    OnLoginSuccess?.Invoke(loggedInUser);
                }
                else
                {
                    ErrorMessage = "Invalid email or password.";
                }
            }
        }
    }
}