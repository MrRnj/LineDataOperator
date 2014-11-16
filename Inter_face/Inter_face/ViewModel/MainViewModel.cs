using GalaSoft.MvvmLight;

namespace Inter_face.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// The <see cref="DatasCollectionProperty" /> property's name.
        /// </summary>
        public const string DatasCollectionPropertyPropertyName = "DatasCollectionProperty";

        private DataCollectionViewModel _DatasCollectionProperty = new DataCollectionViewModel();

        /// <summary>
        /// Sets and gets the DatasCollectionProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DataCollectionViewModel DatasCollectionProperty
        {
            get
            {
                return _DatasCollectionProperty;
            }

            set
            {
                if (_DatasCollectionProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(DatasCollectionPropertyPropertyName);
                _DatasCollectionProperty = value;
                RaisePropertyChanged(DatasCollectionPropertyPropertyName);
            }
        }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        private void FullfilLinedata()
        {

        }
    }
}