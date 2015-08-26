using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    class MovingDistenceStringToIntConverter: System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string raw = value.ToString();
            int result = 0;

            if (!string.IsNullOrEmpty(raw))
            {
                return int.Parse(raw);
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {            
            string raw = value.ToString();
            int result = 0;

            if (!string.IsNullOrEmpty(raw))
            {
                return int.Parse(raw);
            }
            return result;
        }
    }
}
