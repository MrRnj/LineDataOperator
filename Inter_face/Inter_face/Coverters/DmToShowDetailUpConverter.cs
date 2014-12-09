using Inter_face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    class DmToShowDetailUpConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IDataModel idm = (IDataModel)value;
            LineDataModel linemode = null;

            try
            {
                if (idm.LengthProperty * idm.ScaleProperty < 300)
                    return string.Empty;
                switch (idm.Type)
                {
                    case DataType.Single:
                        return string.Empty;
                    case DataType.Station:
                        return string.Empty;
                    case DataType.Bridge:
                        return string.Empty;
                    case DataType.Tunel:
                        return string.Empty;
                    case DataType.Podu:
                        {
                            linemode = (LineDataModel)idm;
                            return linemode.AngleProperty.ToString("F1");
                        }
                    case DataType.Quxian:
                        {
                            linemode = (LineDataModel)idm;
                            return string.Format("{0}", linemode.RadioProperty.ToString("F0"));
                        }
                    case DataType.Break:
                        return string.Empty;
                    case DataType.Position:
                        return string.Empty;
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
