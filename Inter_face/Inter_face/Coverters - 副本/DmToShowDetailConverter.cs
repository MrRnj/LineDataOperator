using Inter_face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    class DmToShowDetailConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IDataModel idm = (IDataModel)value;

            try
            {
                switch (idm.Type)
                {
                    case DataType.Single:
                        return idm.LengthProperty.ToString();
                    case DataType.Station:
                        return string.Empty;
                    case DataType.Bridge:
                        return string.Empty;
                    case DataType.Tunel:
                        return string.Empty;
                    case DataType.Podu:
                        return string.Empty;
                    case DataType.Quxian:
                        return string.Empty;
                    case DataType.Break:
                        return string.Empty;
                    case DataType.Position:
                        return string.Format("{0} {1}", idm.HatProperty, idm.PositionProperty.ToString());
                    default:
                        return string.Empty;
                }
            }

            catch
            {
                return "Error";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
