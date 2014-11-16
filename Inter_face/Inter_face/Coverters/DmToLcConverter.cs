using Inter_face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    class DmToLcConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            StationDataMode sdm = value as StationDataMode;
            string[] parts = { };

            if (sdm == null)
                return "无";
            else
            {
                parts = sdm.PositionProperty.ToString("F3").Split('.');
                return string.Format("{0} {1}+{2}", sdm.HatProperty,
                    parts[0], Math.Round(sdm.PositionProperty - int.Parse(parts[0]), 3).ToString("F3").Substring(2));
            }                
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
