﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    class PartIntConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string raw = value.ToString();
            int result = -1;

            if (!string.IsNullOrEmpty(raw))
            {
                return raw;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string raw = value as string;
            int result = 0;

            if (!string.IsNullOrEmpty(raw))
            {
                int.TryParse(raw, out result);
                if (result < 0 || raw.Contains('.'))
                {
                    result = 0;
                }
            }
            return result;
        }
    }
}
