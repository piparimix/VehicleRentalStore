using System.Windows;
using VehicleRentalStore.Models;
using VehicleRentalStore.ViewModels;

namespace VehicleRentalStore.Views
{
    public partial class LogInWindow : Window
    {
        public Employee? AuthenticatedUser { get; private set; }

        public LogInWindow(LogInViewModel viewModel)
        {
            InitializeComponent();

            // Set the DataContext to the injected ViewModel
            DataContext = viewModel;

            // Listen for the success trigger!
            viewModel.OnLoginSuccess = (employee) =>
            {
                AuthenticatedUser = employee;
                DialogResult = true; // This automatically closes the window and returns true
            };
        }
    }
}