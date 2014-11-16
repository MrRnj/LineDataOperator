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
    /// Interaction logic for ProcessForm.xaml
    /// </summary>
    public partial class ProcessForm : Window
    {
        public string Labtext;

        public int Interval;    
        
        public ProcessForm()
        {
            InitializeComponent();
        }     

        public void SetLabText() 
        {
            this.label_DataSource.Content = Labtext;
        }       

        public void SetProcess()
        {
            this.progressBar.Value += Interval;
        }

        public void SetMaxValue(int maxvalue) 
        {
            this.progressBar.Maximum = maxvalue;
        }

        #region ProcessCancel

        public delegate void ProcessCancelEventHandler(object sender, EventArgs e);
        public event ProcessCancelEventHandler ProcessCancelEvent;

        protected virtual void OnProcessCancel(object sender,EventArgs e)
        {
            if (ProcessCancelEvent != null)
                ProcessCancelEvent(sender, e);
        }

        #endregion
    }
}
