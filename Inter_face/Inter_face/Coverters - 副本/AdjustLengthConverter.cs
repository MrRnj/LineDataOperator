using Inter_face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    class AdjustLengthConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                IDataModel idm = (IDataModel)value;
                return idm.LengthProperty;
            }
            catch
            {
                return 0;
            }

            /*if (idm.Type == DataType.Position)
                return idm.LengthProperty;
            else
                return (float)Math.Floor(idm.LengthProperty - idm.LengthProperty / 600);*/

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
