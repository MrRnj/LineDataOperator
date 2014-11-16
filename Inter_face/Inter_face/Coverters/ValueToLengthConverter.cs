using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Inter_face.Coverters
{
    class ValueToLengthConverter : IMultiValueConverter
    {
        float startpos;
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                startpos = (float)values[0];
                double value = (double)values[1];
                return value.ToString("F0");
                //return (value - startpos * 1000).ToString("F0");
            }
            catch
            {
                return "0.0";
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object[] { startpos, (double)(int.Parse((value as string).Equals("") ? "0" : (value as string))) };
            //return new object[] { startpos, (double)(startpos * 1000 + int.Parse((value as string).Equals("") ? "0" : (value as string))) };
        }
    }
}
