using Inter_face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Inter_face.Coverters
{
    class DmToPartsLengthConverter : IMultiValueConverter
    {        

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                StationDataMode sdm = values[0] as StationDataMode;
                int div = (int)values[1];


                if (sdm == null)
                    return 0;
                else
                {
                    if (sdm.StationNameProperty.StartsWith("3") || sdm.StationNameProperty.Equals("Q:0"))
                        return (sdm.LengthProperty * sdm.ScaleProperty / div).ToString();
                    else
                    {
                        return ((sdm.LengthProperty * sdm.ScaleProperty + 200) / div).ToString();
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
