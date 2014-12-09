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
                        {
                            sdm = (StationDataMode)idm;
                            if (sdm.StationNameProperty.StartsWith("Q"))
                            {
                                return string.Format("信号机间距(km)：{0}",
                                    ((sdm.LengthProperty * sdm.ScaleProperty + 200) / 1000).ToString("F3"));
                            }
                            else
                            {
                                datas = sdm.StationNameProperty.Split(':');
                                return string.Format("信号机位置：{0}\r\n信号机类型：{1}\r\n信号机编号：{2}",
                                    string.Format("{0} {1}", sdm.HatProperty, sdm.PositionProperty.ToString("F3")),
                                  datas[0].Equals("1") ? "车站信号机" : "通过信号机",
                                datas[1]);
                            }
                        }                     
                    case DataType.Station:
                        sdm = (StationDataMode)idm;
                        if (!sdm.StationNameProperty.Equals("区间"))
                            return string.Format("车站名:{0}\r\n中心里程(km):{1}{2}\r\n路段号{3}\r\n",
                                sdm.StationNameProperty, sdm.HatProperty,
                                sdm.PositionProperty.ToString("F3"),
                                sdm.SectionNumProperty.ToString());
                        else
                            return string.Format("区间长度(km):{0}",
                                ((sdm.LengthProperty * sdm.ScaleProperty + 100) / 1000).ToString("F3"));
                    case DataType.Bridge:
                        return "未定义数据";
                    case DataType.Tunel:
                        return "未定义数据";
                    case DataType.Podu:
                        ldm = (LineDataModel)value;
                        return string.Format("起点公里标(km):{0}{1}\r\n坡长(m):{2}\r\n坡度(‰):{3}\r\n路段号{4}\r\n",
                            ldm.HatProperty, ldm.PositionProperty.ToString("F3"),
                            (ldm.LengthProperty * ldm.ScaleProperty).ToString("F3"), ldm.AngleProperty.ToString(),
                            ldm.SectionNumProperty.ToString());
                    case DataType.Quxian:
                        ldm = (LineDataModel)value;
                        return string.Format("起点公里标(km):{0}{1}\r\n曲线长(m):{2}\r\n曲线半径(m):{3}\r\n路段号{4}\r\n",
                            ldm.HatProperty, ldm.PositionProperty.ToString(),
                             (ldm.LengthProperty * ldm.ScaleProperty).ToString("F3"), ldm.RadioProperty.ToString(),
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
