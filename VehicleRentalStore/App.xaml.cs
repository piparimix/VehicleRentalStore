using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using VehicleRentalStore.Data;
using VehicleRentalStore.Services;
using VehicleRentalStore.ViewModels;
using VehicleRentalStore.Views;

namespace VehicleRentalStore
{
    // This is the entry point of the WPF application. It sets up the Dependency Injection container and starts the main window.
    public partial class App : Application
    {
        // This static property holds the built ServiceProvider, allowing you to access registered services from anywhere in the application.
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        // The OnStartup method is overridden to set up the DI container before the main window is shown.
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);
                ServiceProvider = serviceCollection.BuildServiceProvider();

                InitializeDatabase();

                // Call the new method instead of having the logic here
                StartSession();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Startup Error: {ex.Message}\n\n{ex.InnerException?.Message}",
                                "Application Crash",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                Current.Shutdown();
            }
        }
        public void StartSession()
        {
            // Keep app alive while dialog is open
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var loginWindow = ServiceProvider.GetRequiredService<LogInWindow>();
            bool? loginSuccess = loginWindow.ShowDialog();

            if (loginSuccess == true && loginWindow.AuthenticatedUser != null)
            {
                var mainWindow = ServiceProvider.GetRequiredService<Paavalikko>();

                var mainVm = (MainViewModel)mainWindow.DataContext;
                mainVm.CurrentUser = loginWindow.AuthenticatedUser;
                Current.MainWindow = mainWindow;
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                mainWindow.Show();
            }
            else
            {
                Current.Shutdown();
            }
        }
        private void InitializeDatabase()
        {
            var factory = ServiceProvider.GetRequiredService<IDbContextFactory<VehicleRentalDbContext>>();
            using var context = factory.CreateDbContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            DatabaseSeeder.Seed(context);
        }

        // This method is responsible for registering all services, ViewModels, and Views with the DI container.
        private void ConfigureServices(IServiceCollection services)
        {
            // --- Register Services (Database, APIs, utilities)
            services.AddDbContextFactory<VehicleRentalDbContext>();
            services.AddSingleton<IDataService, SqlDataService>();

            // --- Register ViewModels ---
            // MainViewModel is a Singleton because you only ever have one main window
            services.AddSingleton<MainViewModel>();

            // Sub-pages are Transient so they get recreated fresh when you navigate to them
            services.AddTransient<HomeViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<SettingViewModel>();
            services.AddTransient<RegisterCustomerViewModel>();
            services.AddTransient<RegisterCustomerView>();
            services.AddTransient<RegisterVehicleViewModel>();
            services.AddTransient<RegisterVehicleView>();
            services.AddTransient<VehicleListViewModel>();
            services.AddTransient<VehicleListView>();
            services.AddTransient<LogInViewModel>();
            services.AddTransient<CustomerListViewModel>();
            services.AddTransient<CustomerListView>();
            services.AddTransient<CreateRentalViewModel>();
            services.AddTransient<CreateRentalView>();

            // Fixed casing to match your class name perfectly
            services.AddTransient<LogInWindow>();

            // --- Register Views ---
            // Registering the window itself allows the DI container to read its constructor 
            // and automatically pass in the MainViewModel
            services.AddTransient<Paavalikko>();
        }
    }
}