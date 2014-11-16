using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inter_face.Models;

namespace Inter_face.Coverters
{
    class DmToPositionConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                string position = string.Empty;
                StationDataMode sdm = value as StationDataMode;

                if (sdm != null)
                {
                    if (sdm.Type == DataType.Position)
                    {
                        position = string.Format("{0} {1}", sdm.HatProperty, sdm.PositionProperty.ToString());
                    }
                    
                }
                return position;
            }

            catch
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
