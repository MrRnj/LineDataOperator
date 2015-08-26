using Inter_face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    class DmToPathDataConverter : System.Windows.Data.IValueConverter
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
                    case DataType.SingleS:
                    case DataType.Single:
                        sdm = (StationDataMode)idm;
                        datas = sdm.PathDataProperty.Split(':');
                        break;
                    case DataType.Station:
                        sdm = (StationDataMode)idm;
                        datas = sdm.PathDataProperty.Split(':');
                        break;                         
                    case DataType.Bridge:
                        datas = "1:1 0:#00DC5625:#FF35F30E:M0,0 L500,0".Split(':');
                        break;
                    case DataType.Tunel:
                        datas = "1:1 0:#00DC5625:#FF35F30E:M0,0 L500,0".Split(':');
                        break;
                    case DataType.Podu:
                         ldm = (LineDataModel)value;
                         datas = ldm.PathDataProperty.Split(':');
                        break;                                                
                    case DataType.Quxian:
                         ldm = (LineDataModel)value;
                         datas = ldm.PathDataProperty.Split(':');
                        break; 
                    case DataType.Break:
                        sdm = (StationDataMode)idm;
                        datas = sdm.PathDataProperty.Split(':');
                        break;
                    case DataType.Position:
                        sdm = (StationDataMode)idm;
                        datas = sdm.PathDataProperty.Split(':');
                        break;
                    default:
                         datas = "1:1 0:#00DC5625:#FF35F30E:M0,0 L500,0".Split(':');
                        break;
                }
                //strokethickness:strokedasharray:fill:strock:data
                switch (part)
                {
                    case "0":
                        return datas[0];
                    case "1":
                        return datas[1];
                    case "2":
                        return datas[2];
                    case "3":
                        return datas[3];
                    case "4":
                        return datas[4];
                    default:
                        return string.Empty;
                }
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
