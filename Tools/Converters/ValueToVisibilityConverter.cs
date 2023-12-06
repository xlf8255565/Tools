using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Tools.Converters
{
    public class ValueToVisibilityConverter : ValueToBoolConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)base.Convert(value, targetType, parameter, culture) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
