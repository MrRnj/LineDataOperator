using Inter_face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    class IDatamodelToToolTipConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                IDataModel idm = (IDataModel)value;
                string part = parameter as string;
                string[] datas;

                StationDataMode sdm;
                LineDataModel ldm;

                switch (idm.Type)
                {
                    case DataType.Single:
                        return "未定义数据";
                    case DataType.Station:
                        sdm = (StationDataMode)idm;
                        return string.Format("车站名:{0}\r\n中心里程(km):{1}{2}\r\n路段号{3}\r\n",
                            sdm.StationNameProperty, sdm.HatProperty,
                            sdm.PositionProperty.ToString(), 
                            sdm.SectionNumProperty.ToString());
                    case DataType.Bridge:
                        return "未定义数据";
                    case DataType.Tunel:
                        return "未定义数据";
                    case DataType.Podu:
                        ldm = (LineDataModel)value;
                        return string.Format("起点公里标(km):{0}{1}\r\n坡长(m):{2}\r\n坡度(‰):{3}\r\n路段号{4}\r\n",
                            ldm.HatProperty, ldm.PositionProperty.ToString(),
                            (ldm.LengthProperty * ldm.ScaleProperty).ToString(), ldm.AngleProperty.ToString(),
                            ldm.SectionNumProperty.ToString());
                    case DataType.Quxian:
                        ldm = (LineDataModel)value;
                        return string.Format("起点公里标(km):{0}{1}\r\n曲线长(m):{2}\r\n曲线半径(m):{3}\r\n路段号{4}\r\n",
                            ldm.HatProperty, ldm.PositionProperty.ToString(),
                             (ldm.LengthProperty * ldm.ScaleProperty).ToString(), ldm.RadioProperty.ToString(),
                            ldm.SectionNumProperty.ToString());
                    case DataType.Break:
                        return "未定义数据";
                    case DataType.Position:
                        return "";
                    default:
                        return "未定义数据";
                }
               
            }

            catch
            {
                return "未定义数据";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
