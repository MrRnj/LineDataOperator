using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Inter_face.Coverters
{
    class PosToPart1LenConverter : IMultiValueConverter
    {
       
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                int startpos = (int)values[0];
                int endpos = (int)values[1];
                float currentpos = float.Parse(((double)values[2]).ToString());

                return ((int)Math.Round(currentpos - startpos, 0)).ToString();
            }
            catch
            {
                return "0";
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
