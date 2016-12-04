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
using System.Windows.Shapes;

namespace Inter_face.Views
{
    /// <summary>
    /// InfoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InfoWindow : Window
    {
        private static InfoWindow staticInstance = null;

        public InfoWindow()
        {
            InitializeComponent();
            Closed += WindowOnClosed;
        }

        public static InfoWindow GetInstance()
        {
            if (staticInstance == null)
            {
                staticInstance = new InfoWindow();
            }

            return staticInstance;
        }

        private void WindowOnClosed(object sender, System.EventArgs e)
        {
            staticInstance = null;
            this.Close();
        }
    }
}
