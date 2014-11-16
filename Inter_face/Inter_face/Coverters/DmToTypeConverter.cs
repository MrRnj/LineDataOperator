using Inter_face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    class DmToTypeConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            StationDataMode sdm = value as StationDataMode;
            if (sdm == null)
                return "无";
            else
            {
                switch (sdm.StationNameProperty.Split(':')[0])
                {
                    case "1":
                        return "进出站信号机";
                    case "2":
                        return "通过信号机";
                    case "3":
                        return "电分相";
                    default:
                        return "区间";                       
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
