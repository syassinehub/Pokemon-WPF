using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PokemonWpf.ViewModel
{
    public class HealthToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int health = (int)value;
            if (health > 70) return Brushes.Green;
            if (health > 30) return Brushes.Orange;
            return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
