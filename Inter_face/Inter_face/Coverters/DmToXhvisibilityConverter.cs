using Inter_face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    class DmToXhvisibilityConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IList<IDataModel> sdms = value as IList<IDataModel>;
            if (sdms == null)
                return System.Windows.Visibility.Collapsed;
            if (sdms.Count == 0)
                return System.Windows.Visibility.Collapsed;
           
            foreach (StationDataMode item in sdms)
            {
                if (!item.StationNameProperty.StartsWith("2"))
                    return System.Windows.Visibility.Collapsed;
            }
            return System.Windows.Visibility.Visible;            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
