using Inter_face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    class IDatamodelToAliConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IDataModel idm = (IDataModel)value;            

            try
            {
                switch (idm.Type)
                {
                    case DataType.Single:
                        return System.Windows.HorizontalAlignment.Center;
                    case DataType.Station:
                        return System.Windows.HorizontalAlignment.Center;
                    case DataType.Bridge:
                        return System.Windows.HorizontalAlignment.Center;
                    case DataType.Tunel:
                        return System.Windows.HorizontalAlignment.Center;
                    case DataType.Podu:
                        return System.Windows.HorizontalAlignment.Center;
                    case DataType.Quxian:
                        return System.Windows.HorizontalAlignment.Center;
                    case DataType.Break:
                        return System.Windows.HorizontalAlignment.Center;
                    case DataType.Position:
                        return System.Windows.HorizontalAlignment.Left;
                    default:
                        return System.Windows.HorizontalAlignment.Center;
                }
            }

            catch
            {
                return System.Windows.HorizontalAlignment.Center;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
