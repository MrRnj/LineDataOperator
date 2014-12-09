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

namespace Inter_face
{
    /// <summary>
    /// ModifyQujianWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ModifyQujianWindow : Window
    {
        public ModifyQujianWindow()
        {
            InitializeComponent();
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<bool?>(this, "status",
               (p) =>
               {
                   if (p == true)
                       this.Close();
               });
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<bool?>(this, "status");
        }
    }
}
