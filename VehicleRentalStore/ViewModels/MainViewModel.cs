using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VehicleRentalStore.Models;
using VehicleRentalStore.Themes;

namespace VehicleRentalStore.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private bool _isDarkMode = true;
        private object? _currentViewModel;
        private Employee? _currentUser;

        // Properties for the "INFO" section
        public string CompanyName { get; } = "App Name";

        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                if (SetProperty(ref _isDarkMode, value))
                {
                    string newTheme = _isDarkMode ? "DarkTheme" : "LightTheme";
                    ThemeManager.ApplyTheme(newTheme);
                }
            }
        }

        public class NavigationItem : ObservableObject
        {
            public required string Title { get; set; }
            public required string IconPath { get; set; }
            public Type? TargetViewModel { get; set; }

            // 1. Add this action so the item can talk back to the MainViewModel
            public Action<NavigationItem>? OnExpanded { get; set; }

            private bool _isExpanded;
            public bool IsExpanded
            {
                get => _isExpanded;
                set
                {
                    // 2. If this item is being opened (value is true), trigger the callback
                    if (SetProperty(ref _isExpanded, value) && value)
                    {
                        OnExpanded?.Invoke(this);
                    }
                }
            }

            public ObservableCollection<NavigationItem> Children { get; } = new();
            public bool HasChildren => Children.Count > 0;
        }
        private void CloseOtherMenus(NavigationItem expandedItem)
        {
            foreach (var item in MenuItems)
            {
                // If the item has children, is currently expanded, and isn't the one we just clicked...
                if (item.HasChildren && item.IsExpanded && item != expandedItem)
                {
                    item.IsExpanded = false; // Close it!
                }
            }
        }

        // The dynamic list of menus bound to the UI
        public ObservableCollection<NavigationItem> MenuItems { get; } = new();

        public object? CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public Employee? CurrentUser
        {
            get => _currentUser;
            set
            {
                if (SetProperty(ref _currentUser, value) && value != null)
                {
                    OnPropertyChanged(nameof(UserName));
                    BuildMenuForRole(value.Role);
                }
            }
        }

        public string UserName => CurrentUser != null ? CurrentUser.FullName : "Not Logged In";

        public ICommand ToggleThemeCommand { get; }
        public ICommand NavigateCommand { get; }
        public ICommand LogoutCommand { get; }
        public Action? OnLogout { get; set; }

        public MainViewModel()
        {
            ToggleThemeCommand = new RelayCommand(_ => IsDarkMode = !IsDarkMode);

            NavigateCommand = new RelayCommand(type =>
            {
                if (type is Type viewModelType)
                {
                    var nextViewModel = App.ServiceProvider.GetRequiredService(viewModelType);

                    // Wire up the navigation pipe before showing it
                    WireUpNavigation(nextViewModel);

                    CurrentViewModel = nextViewModel;
                }
            });

            ThemeManager.ApplyTheme("DarkTheme");
            LogoutCommand = new RelayCommand(_ => OnLogout?.Invoke());
        }

        private void WireUpNavigation(object viewModel)
        {
            if (viewModel is HomeViewModel homeVm)
            {
                homeVm.RequestNavigation = (viewModelToOpen) =>
                {
                    CurrentViewModel = viewModelToOpen;
                };
            }
        }

        private void BuildMenuForRole(EmployeeRole role)
        {
            MenuItems.Clear();

            // Standalone buttons
            MenuItems.Add(new NavigationItem { Title = "Home", IconPath = "\xE80F", TargetViewModel = typeof(HomeViewModel) });

            if (role == EmployeeRole.Staff || role == EmployeeRole.Manager)
            {
                MenuItems.Add(new NavigationItem { Title = "Dashboard", IconPath = "\xE7C6", TargetViewModel = typeof(DashboardViewModel) });

                // --- CUSTOMERS GROUP ---
                var customersMenu = new NavigationItem
                {
                    Title = "Customers",
                    IconPath = "\xE71D",
                    OnExpanded = CloseOtherMenus
                };
                // UPDATE THIS LINE:
                customersMenu.Children.Add(new NavigationItem { Title = "Customer List", IconPath = "\xE710", TargetViewModel = typeof(CustomerListViewModel) });
                customersMenu.Children.Add(new NavigationItem { Title = "Register New", IconPath = "\xE710", TargetViewModel = typeof(RegisterCustomerViewModel) });
                MenuItems.Add(customersMenu);

                // --- VEHICLES GROUP ---
                var vehiclesMenu = new NavigationItem
                {
                    Title = "Vehicles",
                    IconPath = "\xE71D",
                    OnExpanded = CloseOtherMenus // <--- And here!
                };
                vehiclesMenu.Children.Add(new NavigationItem { Title = "Vehicle List", IconPath = "\xE710", TargetViewModel = typeof(VehicleListViewModel) });
                vehiclesMenu.Children.Add(new NavigationItem { Title = "Register New", IconPath = "\xE710", TargetViewModel = typeof(RegisterVehicleViewModel) });
                MenuItems.Add(vehiclesMenu);
            }

            if (role == EmployeeRole.Manager)
            {
                MenuItems.Add(new NavigationItem { Title = "Settings", IconPath = "\xE713", TargetViewModel = typeof(SettingViewModel) });
            }

            if (MenuItems.Count > 0)
            {
                NavigateCommand.Execute(MenuItems[0].TargetViewModel);
            }
        }
    }
}