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
            StationDataMode signalmode = null;

            try
            {
                if (idm.LengthProperty * idm.ScaleProperty < 300)
                    return string.Empty;
                switch (idm.Type)
                {
                    case DataType.SingleS:
                    case DataType.Single:
                        {
                            signalmode = (StationDataMode)idm;
                            if (signalmode.StationNameProperty.StartsWith("Q"))
                                return (signalmode.RealLength * idm.ScaleProperty).ToString("F0");
                            return string.Empty;
                        }
                    case DataType.Station:
                        {
                            signalmode = (StationDataMode)idm;
                            if (signalmode.StationNameProperty.Equals("区间"))
                                return (signalmode.RealLength * idm.ScaleProperty).ToString("F3");
                            return string.Empty;
                        }
                    case DataType.Bridge:
                        return string.Empty;
                    case DataType.Tunel:
                        return string.Empty;
                    case DataType.Podu:
                        return (idm.LengthProperty * idm.ScaleProperty).ToString("F3");
                    case DataType.Quxian:
                        return (idm.LengthProperty * idm.ScaleProperty).ToString("F0");
                    case DataType.Break:
                        return idm.SectionNumProperty.ToString();
                    case DataType.Position:
                        return string.Format("{0} {1}", idm.HatProperty, idm.PositionProperty.ToString("F0"));
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
