using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    class PathDetailConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string pathdata = value as string;
            string part = parameter as string;

            if (pathdata != null)
            {
                string[] datas = pathdata.Split(':');

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
            else
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
