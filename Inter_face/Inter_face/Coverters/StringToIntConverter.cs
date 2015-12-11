using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    class StringToIntConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string raw = value.ToString();
            int result = -1;

            if (!string.IsNullOrEmpty(raw))
            {
                return raw;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string raw = value.ToString();
            int result = -1;

            if (!string.IsNullOrEmpty(raw))
            {
                raw = raw.TrimStart('0');
                int.TryParse(raw, out result);
                if (result < 0 || raw.Contains('.'))
                {
                    result = -1;
                }
            }
            return result;
        }
    }
}
