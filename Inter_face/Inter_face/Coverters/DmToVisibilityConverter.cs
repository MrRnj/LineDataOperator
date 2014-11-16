using Inter_face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    class DmToVisibilityConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                IDataModel idm = (IDataModel)value;

                switch (idm.Type)
                {
                    case DataType.Single:
                        return 1;
                    case DataType.Station:
                        return 0;
                    case DataType.Bridge:
                        return 0;
                    case DataType.Tunel:
                        return 0;
                    case DataType.Podu:
                        return 1;
                    case DataType.Quxian:
                        return 0.6;
                    case DataType.Break:
                        return 0;
                    case DataType.Position:
                        return 0;
                    default:
                        return 1;
                }
            }

            catch
            {
                return 0.6;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
