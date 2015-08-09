using Inter_face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    class DmToLengthConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            StationDataMode sdm = value as StationDataMode;
            if (sdm == null)
                return "0";
            else
            {
                if (sdm.StationNameProperty.StartsWith("3") || sdm.StationNameProperty.Equals("Q:0"))
                    return (sdm.LengthProperty * sdm.ScaleProperty).ToString();
                else
                {
                    return (sdm.RealLength * sdm.ScaleProperty).ToString();
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}

