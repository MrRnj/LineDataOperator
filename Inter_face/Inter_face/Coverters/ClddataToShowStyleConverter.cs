using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    class ClddataToShowStyleConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string content = value as string;

            if (content != null)
            {
                string[] parts = content.Split(':');
                float first = float.Parse(parts[0].Split('+')[1]) / 1000;
                float secend = float.Parse(parts[1].Split('+')[1]) / 1000;
                string result = string.Format("{0} {1}+{2} = {3} {4}+{5}", parts[0].Split('+')[0],
                    Math.Floor(first).ToString(),
                    ((first - Math.Floor(first)) * 1000).ToString("F3"),
                    parts[1].Split('+')[0],
                    Math.Floor(secend).ToString(),
                    ((secend - Math.Floor(secend)) * 1000).ToString("F3"));
                return result;
            }

            return "=";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
