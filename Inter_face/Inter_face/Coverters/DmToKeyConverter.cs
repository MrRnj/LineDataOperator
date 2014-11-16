using Inter_face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    class DmToKeyConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                StationDataMode sdm = value as StationDataMode;
                if (sdm == null || string.IsNullOrEmpty(sdm.StationNameProperty) || sdm.StationNameProperty.StartsWith("Q"))
                {
                    return "无";
                }
                else
                    return sdm.StationNameProperty.Split(':')[1];
            }
            catch
            {
                return "无";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
