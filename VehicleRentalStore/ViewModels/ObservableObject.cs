using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VehicleRentalStore.ViewModels
{
    // This class serves as a base class for all ViewModels in the application.
    // It provides the implementation for the INotifyPropertyChanged interface,
    // allowing derived classes to notify the UI when a property value changes.
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
            {
                return false;
            }

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}