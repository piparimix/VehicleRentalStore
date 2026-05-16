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
        }

        public Paavalikko()
        {
            InitializeComponent();
        }
    }
}