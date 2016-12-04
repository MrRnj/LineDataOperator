using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    public class StringToFloatConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float range = (float)value;
            return range.ToString("#0.00");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string rawString = value as string;
            if (string.IsNullOrEmpty(rawString))
            {
                return 1;
            }
            else
            {
                float def = 1;                
                float.TryParse(rawString,out def);
                return def;
            }
        }
    }
}
