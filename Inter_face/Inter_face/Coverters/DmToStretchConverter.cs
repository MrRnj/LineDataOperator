using Inter_face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Inter_face.Coverters
{
    class DmToStretchConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                IDataModel idm = (IDataModel)value;

                switch (idm.Type)
                {
                    case DataType.Single:
                        return Stretch.Uniform;
                    case DataType.Station:
                        return Stretch.Fill;
                    case DataType.Bridge:
                        return Stretch.Fill;
                    case DataType.Tunel:
                        return Stretch.Fill;
                    case DataType.Podu:
                        return Stretch.Fill;
                    case DataType.Quxian:
                        return Stretch.Fill;
                    case DataType.Break:
                        return Stretch.Fill;
                    case DataType.Position:
                        return Stretch.None;
                    default:
                        return Stretch.Fill;
                }
            }

            catch
            {
                return Stretch.Fill;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
