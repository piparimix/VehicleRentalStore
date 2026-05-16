using System;
using System.Globalization;
using System.Windows.Data;

namespace VehicleRentalStore.Converters
{
    // This converter is used in the Button's Style to determine if the button should be highlighted as active.
    public class ActiveViewConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // values[0] is the CurrentViewModel from the Window
            // values[1] is the Tag (Target Type) from the Button
            if (values.Length < 2 || values[0] == null || values[1] == null)
                return false;

            return values[0].GetType() == (Type)values[1];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}