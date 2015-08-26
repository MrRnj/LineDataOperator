using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Inter_face.Coverters
{
    class ISDToSelectionModeConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                int typenum = (int)value;

                if (typenum == (int)DataType.Single || typenum == (int)DataType.SingleS)
                {
                    return SelectionMode.Multiple;
                }
                else
                    return SelectionMode.Single;
            }
            catch
            {
                return SelectionMode.Single;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
