using System.Windows;
using System.Windows.Controls.Primitives;
using VehicleRentalStore.ViewModels;


namespace VehicleRentalStore
{
    public partial class Paavalikko : Window
    {
        public Paavalikko(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            // Listen for the logout trigger from the ViewModel
            viewModel.OnLogout = () =>
            {
                // 1. Prevent the app from exiting when we close this window
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

                // 2. Cast the current Application back to our App class and restart the flow
                if (Application.Current is App app)
                {
                    app.StartSession();
                }

                // 3. Destroy the current main window session
                this.Close();
            };
        }

        public Paavalikko()
        {
            InitializeComponent();
        }
    }
}