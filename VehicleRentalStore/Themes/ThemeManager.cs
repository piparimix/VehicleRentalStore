using System;
using System.Linq;
using System.Windows;

namespace VehicleRentalStore.Themes
{
    // This class is responsible for applying the selected theme by replacing the current ResourceDictionary with the new one.
    public static class ThemeManager
    {
        public static void ApplyTheme(string themeName)
        {
            var appDictionaries = Application.Current.Resources.MergedDictionaries;

            // Find the current theme dictionary (assuming it ends with "Theme.xaml")
            var currentTheme = appDictionaries.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.EndsWith("Theme.xaml"));

            if (currentTheme != null)
            {
                // Remove the old theme
                appDictionaries.Remove(currentTheme);
            }

            // Load the new theme
            var newTheme = new ResourceDictionary
            {
                Source = new Uri($"pack://application:,,,/Themes/{themeName}.xaml")
            };

            // Insert the new theme at the beginning of the merged dictionaries
            appDictionaries.Insert(0, newTheme);
        }
    }
}