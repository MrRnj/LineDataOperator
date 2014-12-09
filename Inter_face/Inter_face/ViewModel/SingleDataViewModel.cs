using GalaSoft.MvvmLight;
using Inter_face.Models;
using System.Collections.ObjectModel;


namespace Inter_face.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SingleDataViewModel : ViewModelBase, Inter_face.ViewModel.ISingleDataViewModel
    {
        /// <summary>
        /// The <see cref="ShowDataProperty" /> property's name.
        /// </summary>
        public const string ShowDataPropertyPropertyName = "ShowDataProperty";

        private bool _ShowDataProperty = true;

        /// <summary>
        /// Sets and gets the ShowDataProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool ShowDataProperty
        {
            get
            {
                return _ShowDataProperty;
            }

            set
            {
                if (_ShowDataProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(ShowDataPropertyPropertyName);
                _ShowDataProperty = value;
                RaisePropertyChanged(ShowDataPropertyPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="TypeNameProperty" /> property's name.
        /// </summary>
        public const string TypeNamePropertyPropertyName = "TypeNameProperty";

        private string _TypeNameProperty = string.Empty;

        /// <summary>
        /// Sets and gets the TypeNameProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string TypeNameProperty
        {
            get
            {
                return _TypeNameProperty;
            }

            set
            {
                if (_TypeNameProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(TypeNamePropertyPropertyName);
                _TypeNameProperty = value;
                RaisePropertyChanged(TypeNamePropertyPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CurrentDataProperty" /> property's name.
        /// </summary>
        public const string CurrentDataPropertyPropertyName = "CurrentDataProperty";

        private IDataModel _CurrentDataProperty = null;

        /// <summary>
        /// Sets and gets the CurrentDataProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public IDataModel CurrentDataProperty
        {
            get
            {
                return _CurrentDataProperty;
            }

            set
            {
                if (value != null)
                    MessengerInstance.Send<DataType>((DataType)TypeNum, "SelectedChanged");

                if (_CurrentDataProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(CurrentDataPropertyPropertyName);

                if (value != null)
                {
                    foreach (IDataModel item in DataCollection)
                    {
                        item.SelectedProperty = false;
                        if (item.Type == value.Type)
                            item.SelectedProperty = true;
                    }

                    value.SelectedProperty = true;
                    
                   /* if (value.Type != DataType.Single)
                        MessengerInstance.Send<int>(0, "SelectedTabItem");
                    else
                    {
                        StationDataMode sdm = value as StationDataMode;
                        if (sdm.StationNameProperty.StartsWith("Q"))
                            MessengerInstance.Send<int>(0, "SelectedTabItem");
                        else
                            MessengerInstance.Send<int>(2, "SelectedTabItem");
                    }*/
                }
                _CurrentDataProperty = value;
                RaisePropertyChanged(CurrentDataPropertyPropertyName);
            }
        }
       
        ObservableCollection<IDataModel> _datacollection;
        public ObservableCollection<IDataModel> DataCollection
        {
            get
            {
                return _datacollection;
            }
            set { _datacollection = value; }
        }
        /// <summary>
        /// The <see cref="SelectedIndex" /> property's name.
        /// </summary>
        public const string SelectedIndexPropertyName = "SelectedIndex";

        private int _selectedindexProperty = 0;

        /// <summary>
        /// Sets and gets the SelectedIndex property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                return _selectedindexProperty;
            }

            set
            {
                if (_selectedindexProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(SelectedIndexPropertyName);
                _selectedindexProperty = value;
                RaisePropertyChanged(SelectedIndexPropertyName);
            }
        }
        /// <summary>
        /// Initializes a new instance of the SingleDataViewModel class.
        /// </summary>
        public SingleDataViewModel()
        {
            _datacollection = new ObservableCollection<IDataModel>();
        }

        private int _typenum;
        public int TypeNum
        {
            get
            {
                return _typenum;
            }
            set
            {
                _typenum = value;
            }
        }

        private GalaSoft.MvvmLight.Command.RelayCommand _showdatachangedCommand;

        /// <summary>
        /// Gets the ShowDataChangedCommand.
        /// </summary>
        public GalaSoft.MvvmLight.Command.RelayCommand ShowDataChangedCommand
        {
            get
            {
                return _showdatachangedCommand
                    ?? (_showdatachangedCommand = new GalaSoft.MvvmLight.Command.RelayCommand(
                                          () =>
                                          {
                                              MessengerInstance.Send<ISingleDataViewModel>(this, "DisapearData");
                                          }));
            }
        }        
    }
}