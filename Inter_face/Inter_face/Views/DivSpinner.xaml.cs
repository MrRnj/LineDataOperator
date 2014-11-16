using System;
using System.Collections.Generic;
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
	/// DivSpinner.xaml 的交互逻辑
	/// </summary>
	public partial class DivSpinner : UserControl
	{
        
        public DivSpinner()
		{
			this.InitializeComponent();
			
			// 在此点之下插入创建对象所需的代码。            
		}



        public float StartPosition
        {
            get
            {                
                return (float)GetValue(StartPositionProperty);
            }
            set
            {               
                SetValue(StartPositionProperty, value);
            }
        }
        
        // Using a DependencyProperty as the backing store for StartPosition.  This enables animation, styling, binding, etc...
              public static readonly DependencyProperty StartPositionProperty =
            DependencyProperty.Register("StartPosition", typeof(float), typeof(DivSpinner), new PropertyMetadata((float)0, new PropertyChangedCallback(OnStartPositionChangede)));

              private static void OnStartPositionChangede(DependencyObject d, DependencyPropertyChangedEventArgs e)
              {
                  DivSpinnerModel viewmodel = (d as DivSpinner).DataContext as DivSpinnerModel;
                  viewmodel.StartPosition = (float)e.NewValue;
              }

        /// <summary>
        /// The <see cref="EndPosition" /> dependency property's name.
        /// </summary>
        public const string EndPositionPropertyName = "EndPosition";

        /// <summary>
        /// Gets or sets the value of the <see cref="EndPosition" />
        /// property. This is a dependency property.
        /// </summary>
        public float EndPosition
        {
            get
            {                
                return (float)GetValue(EndPositionProperty);
            }
            set
            {                
                SetValue(EndPositionProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="EndPosition" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty EndPositionProperty = DependencyProperty.Register(
            EndPositionPropertyName,
            typeof(float),
            typeof(DivSpinner),
            new UIPropertyMetadata((float)0));

        /// <summary>
        /// The <see cref="CurrentPosition" /> dependency property's name.
        /// </summary>
        public const string CurrentPositionPropertyName = "CurrentPosition";

        /// <summary>
        /// Gets or sets the value of the <see cref="CurrentPosition" />
        /// property. This is a dependency property.
        /// </summary>
        public float CurrentPosition
        {
            get
            {
                //SetValue(CurrentPositionProperty, viewmodel.CurrentPosition);
                return (float)GetValue(CurrentPositionProperty);
            }
            set
            {
                //viewmodel.CurrentPosition = value;
                SetValue(CurrentPositionProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="CurrentPosition" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CurrentPositionProperty = DependencyProperty.Register(
            CurrentPositionPropertyName,
            typeof(float),
            typeof(DivSpinner),
            new UIPropertyMetadata((float)0));
	}
}