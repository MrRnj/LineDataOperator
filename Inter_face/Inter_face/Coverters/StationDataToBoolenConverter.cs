using Inter_face.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Inter_face.Coverters
{
    class StationDataToBoolenConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ObservableCollection<ISingleDataViewModel> collection = value as ObservableCollection<ISingleDataViewModel>;
            bool b = false;

            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].TypeNum == (int)DataType.Station)
                {
                    b = true;
                    break;
                }
            }

            return b;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
