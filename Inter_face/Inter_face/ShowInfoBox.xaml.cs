using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inter_face
{
    /// <summary>
    /// Interaction logic for ShowInfoBox.xaml
    /// </summary>
    public partial class ShowInfoBox : UserControl
    {
        public static readonly DependencyProperty SituationProperty = DependencyProperty.Register("Situation", typeof(string), typeof(ShowInfoBox));
        /// <summary>
        /// 0 is good
        /// 1 is bad
        /// 2 is whatever
        /// 3 is processfinish
        /// </summary>
        public string Situation 
        {
            get { return (string)GetValue(SituationProperty); }
            set { SetValue(SituationProperty, value); }
        }

        string filepath;
        public string FilePath 
        {
            get { return filepath; }
            set { filepath = value; }
        }

        int rowindex;
        public int Rowindex 
        {
            get { return rowindex; }
            set { rowindex = value; }
        }

        string position;
        public string Position 
        {
            get { return position; }
            set { position = value; }
        }

        string message;
        public string Message 
        {
            get { return message; }
            set 
            { 
                message = value;
                this.contentbutton.Content = message;
            }
        }
        
        public ShowInfoBox()
        {
            InitializeComponent();
            this.Situation = "1";
            this.DataContext = this;
            SetCustomBinding(this.signgrid, Grid.BackgroundProperty, "Situation", new IntToBackgroudConverter());
            //SetCustomBinding(this.contentbutton, Button.IsEnabledProperty, "Situation",new IntToBoolenConverter());
            SetCustomBinding(this.ok, Button.IsEnabledProperty, "Situation", new FinishIntToBoolenConverter());
            SetCustomBinding(this.ig, Button.IsEnabledProperty, "Situation", new FinishIntToBoolenConverter());
            //SetCustomBinding(this.contentbutton, Button.IsEnabledProperty, "Situation", new FinishIntToBoolenConverter());
        }

        void SetCustomBinding(FrameworkElement obj, DependencyProperty p, string path,IValueConverter converter)
        {            
            Binding b = new Binding();                        
            b.Source = this;
            b.Converter = converter;
            b.Path = new PropertyPath(path);
            b.Mode = BindingMode.OneWay;
            obj.SetBinding(p, b);
        }

        private void contentbutton_Click(object sender, RoutedEventArgs e)
        {
            if (Situation != "0" && Situation != "3")
                OnContentbuttonClick(this, new ContentbuttonClickEventArgs(filepath, rowindex, position));
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            SetValue(SituationProperty,"0");
            OnOperationbuttonClick(this, new OperationbuttonClickEventArgs(OperationSituation.Ok));
        }

        private void no_Click(object sender, RoutedEventArgs e)
        {
            SetValue(SituationProperty, "0");
            OnOperationbuttonClick(this, new OperationbuttonClickEventArgs(OperationSituation.Close));
        }

        private void ig_Click(object sender, RoutedEventArgs e)
        {
            SetValue(SituationProperty, "2");
            OnOperationbuttonClick(this, new OperationbuttonClickEventArgs(OperationSituation.Ignore));
        }

        #region ContentbuttonClick OperationbuttonClick

        public class ContentbuttonClickEventArgs : EventArgs 
        {
            string filepath;
            public string FilePath
            {
                get { return filepath; }                
            }

            int rowindex;
            public int Rowindex
            {
                get { return rowindex; }                
            }

            string position;
            public string Position
            {
                get { return position; }                
            }

            public ContentbuttonClickEventArgs(string filepath, int rowindex, string position) 
            {
                this.filepath = filepath;
                this.rowindex = rowindex;
                this.position = position;
            }
        }

        public delegate void ContentbuttonClickEventhandler(object sender, ContentbuttonClickEventArgs e);
        public event ContentbuttonClickEventhandler ContentbuttonClick;

        protected virtual void OnContentbuttonClick(object sender, ContentbuttonClickEventArgs e) 
        {
            if (ContentbuttonClick != null)
                ContentbuttonClick(sender, e);
        }

        public class OperationbuttonClickEventArgs : EventArgs 
        {
            OperationSituation situation;
            public OperationSituation Situation 
            {
                get { return situation; }
            }

            public OperationbuttonClickEventArgs(OperationSituation situation) 
            {
                this.situation = situation;
            }
        }

        public delegate void OperationbuttonClickEventhandler(object sender, OperationbuttonClickEventArgs e);
        public event OperationbuttonClickEventhandler OperationbuttonClick;

        protected virtual void OnOperationbuttonClick(object sender, OperationbuttonClickEventArgs e)
        {
            if (OperationbuttonClick != null)
                OperationbuttonClick(sender, e);
        }

        #endregion       
        
    }

    public enum OperationSituation 
    {
        None=0,
        Ok,
        Close,
        Ignore
    };

    class IntToBackgroudConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string situation = value.ToString();
            switch (situation)
            {
                case "0":
                case "3":
                    return new SolidColorBrush(Colors.LightSeaGreen);                    
                case "1":
                    return new SolidColorBrush(Colors.LightPink);                    
                case "2":               
                    return new SolidColorBrush(Colors.Orange);
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    class IntToBoolenConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string situation = value.ToString();
            if (situation.Equals("0"))
                return false;
            else
                return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    class FinishIntToBoolenConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string situation = value.ToString();
            if (situation.Equals("3"))
                return false;
            else
                return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }



}
