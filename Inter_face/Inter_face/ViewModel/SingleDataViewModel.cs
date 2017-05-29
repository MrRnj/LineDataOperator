using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inter_face.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System;

namespace Inter_face.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SingleDataViewModel : ViewModelBase, ISingleDataViewModel
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
                //if (value != null)
                   // MessengerInstance.Send<DataType>((DataType)TypeNum, "SelectedChanged");
                
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
                    MessengerInstance.Send((DataType)TypeNum, "SelectedChanged");
                   
                }
                else
                {
                    SelectedIndex = -1;
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
        
        private ObservableCollection<IDataModel> selectedItems;
        public ObservableCollection<IDataModel> SelectedItems
        {
            get { return selectedItems; }
            set { selectedItems = value; }
        }
        /// <summary>
        /// The <see cref="IsShowQujian" /> property's name.
        /// </summary>
        public const string IsShowQujianPropertyName = "IsShowQujian";

        private bool _isshowqujianProperty = false;

        /// <summary>
        /// Sets and gets the IsShowQujian property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsShowQujian
        {
            get
            {
                return _isshowqujianProperty;
            }

            set
            {
                if (_isshowqujianProperty == value)
                {
                    return;
                }

                _isshowqujianProperty = value;
                RaisePropertyChanged(IsShowQujianPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsShowSignale" /> property's name.
        /// </summary>
        public const string IsShowSignalePropertyName = "IsShowSignale";

        private bool _isshowsignalProperty = false;

        /// <summary>
        /// Sets and gets the IsShowSignale property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsShowSignale
        {
            get
            {
                return _isshowsignalProperty;
            }

            set
            {
                if (_isshowsignalProperty == value)
                {
                    return;
                }

                _isshowsignalProperty = value;
                RaisePropertyChanged(IsShowSignalePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsShowDianFX" /> property's name.
        /// </summary>
        public const string IsShowDianFXPropertyName = "IsShowDianFX";

        private bool _isshowdianfxProperty = false;

        /// <summary>
        /// Sets and gets the IsShowDianFX property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsShowDianFX
        {
            get
            {
                return _isshowdianfxProperty;
            }

            set
            {
                if (_isshowdianfxProperty == value)
                {
                    return;
                }

                _isshowdianfxProperty = value;
                RaisePropertyChanged(IsShowDianFXPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="MenuVisibility" /> property's name.
        /// </summary>
        public const string MenuVisibilityPropertyName = "MenuVisibility";

        private System.Windows.Visibility _visibility = System.Windows.Visibility.Visible;

        /// <summary>
        /// Sets and gets the MenuVisibility property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public System.Windows.Visibility MenuVisibility
        {
            get
            {
                return _visibility;
            }

            set
            {
                if (_visibility == value)
                {
                    return;
                }

                _visibility = value;
                RaisePropertyChanged(MenuVisibilityPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="QjMenuVisibility" /> property's name.
        /// </summary>
        public const string QjMenuVisibilityPropertyName = "QjMenuVisibility";

        private System.Windows.Visibility _qjMenuvisibility = System.Windows.Visibility.Collapsed;

        /// <summary>
        /// Sets and gets the QjMenuVisibility property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public System.Windows.Visibility QjMenuVisibility
        {
            get
            {
                return _qjMenuvisibility;
            }

            set
            {
                if (_qjMenuvisibility == value)
                {
                    return;
                }

                _qjMenuvisibility = value;
                RaisePropertyChanged(QjMenuVisibilityPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="XhMenuVisibility" /> property's name.
        /// </summary>
        public const string XhMenuVisibilityPropertyName = "XhMenuVisibility";

        private System.Windows.Visibility _xhMenuvisibility = System.Windows.Visibility.Collapsed;

        /// <summary>
        /// Sets and gets the XhMenuVisibility property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public System.Windows.Visibility XhMenuVisibility
        {
            get
            {
                return _xhMenuvisibility;
            }

            set
            {
                if (_xhMenuvisibility == value)
                {
                    return;
                }

                _xhMenuvisibility = value;
                RaisePropertyChanged(XhMenuVisibilityPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="DfxMenuVisibility" /> property's name.
        /// </summary>
        public const string DfxMenuVisibilityPropertyName = "DfxMenuVisibility";

        private System.Windows.Visibility _dfxMenuvisibility = System.Windows.Visibility.Collapsed;

        /// <summary>
        /// Sets and gets the DfxMenuVisibility property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public System.Windows.Visibility DfxMenuVisibility
        {
            get
            {
                return _dfxMenuvisibility;
            }

            set
            {
                if (_dfxMenuvisibility == value)
                {
                    return;
                }

                _dfxMenuvisibility = value;
                RaisePropertyChanged(DfxMenuVisibilityPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="CanMoveSignal" /> property's name.
        /// </summary>
        public const string CanMoveSignalPropertyName = "CanMoveSignal";

        private bool _canMovesignal = true;

        /// <summary>
        /// Sets and gets the CanMoveSignal property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool CanMoveSignal
        {
            get
            {
                return _canMovesignal;
            }

            set
            {
                if (_canMovesignal == value)
                {
                    return;
                }

                _canMovesignal = value;
                RaisePropertyChanged(CanMoveSignalPropertyName);
            }
        }
        /// <summary>
        /// Initializes a new instance of the SingleDataViewModel class.
        /// </summary>
        public SingleDataViewModel()
        {
            _datacollection = new ObservableCollection<IDataModel>();
            selectedItems = new ObservableCollection<IDataModel>();
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
        
        /// <summary>
        /// The <see cref="IsSingleQJ" /> property's name.
        /// </summary>
        public const string IsSingleQJPropertyName = "IsSingleQJ";

        private bool _isSingleqj = true;

        /// <summary>
        /// Sets and gets the CanMoveSignal property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsSingleQJ
        {
            get
            {
                return _isSingleqj;
            }

            set
            {
                if (_isSingleqj == value)
                {
                    return;
                }

                _isSingleqj = value;
                RaisePropertyChanged(IsSingleQJPropertyName);
            }
        }

        private bool checkDianfx()
        {
            if (TypeNum == (int)DataType.SingleS || TypeNum == (int)DataType.Single)
            {
                if (SelectedItems == null)
                    return false;
                if (SelectedItems.Count == 0 || SelectedItems.Count > 1)
                    return false;
                foreach (StationDataMode item in SelectedItems)
                {
                   if (!item.StationNameProperty.StartsWith("3"))
                        return false;
                }
                return true;
            }
            return false;
        }

        private void sendSignalInfo()
        {
            if (TypeNum == (int)DataType.SingleS || TypeNum == (int)DataType.Single)
            {
                if (SelectedItems == null)
                    MessengerInstance.Send("未选择信号机^f", "GetSignalInfo");
                if (SelectedItems.Count == 0 || SelectedItems.Count > 1)
                    MessengerInstance.Send("未选择信号机或选择多个信号机^f", "GetSignalInfo");

                string[] parts;
                foreach (StationDataMode item in SelectedItems)
                {
                    parts = item.StationNameProperty.Split(':');
                    if (parts[0].StartsWith("Q") || parts[0].Equals("3"))
                    {
                        MessengerInstance.Send("未选择信号机^f", "GetSignalInfo");
                        return;
                    }                       

                    MessengerInstance.Send(string.Format("{0}^t", item.StationNameProperty), "GetSignalInfo");
                }
                return;
            }
            MessengerInstance.Send("未选择信号机^f", "GetSignalInfo"); 
        }

        private bool checkQujian()
        {
            if (TypeNum == (int)DataType.SingleS || TypeNum == (int)DataType.Single)
            {
                if (SelectedItems == null)
                    return false;
                if (SelectedItems.Count == 0 || SelectedItems.Count > 1)
                    return false;

                foreach (StationDataMode item in SelectedItems)
                {
                    if (!item.StationNameProperty.StartsWith("Q"))
                        return false;
                }

                int index = DataCollection.IndexOf(selectedItems[0]);
                if (index == 0 || index == DataCollection.Count - 1)
                    return false;
                return true;
            }
            return false;
        }

        private bool checkSignal()
        {
            if (TypeNum == (int)DataType.SingleS || TypeNum == (int)DataType.Single)
            {
                if (SelectedItems == null)
                    return false;
                if (SelectedItems.Count == 0)
                    return false;

                foreach (StationDataMode item in SelectedItems)
                {
                    if (!item.StationNameProperty.StartsWith("2"))
                        return false;
                }
                return true;
            }
            return false;
        }

        private void checkMenuFunc(ObservableCollection<IDataModel> items)
        {
            string type = string.Empty;
            int index = 0;            
            StationDataMode nextsdm;
            DfxMenuVisibility = System.Windows.Visibility.Collapsed;
            XhMenuVisibility = System.Windows.Visibility.Collapsed;
            QjMenuVisibility = System.Windows.Visibility.Collapsed;            

            foreach (IDataModel item in items)
            {                
                StationDataMode sdm = item as StationDataMode;
                index = DataCollection.IndexOf(item);

                if (sdm != null)
                {
                    type = sdm.StationNameProperty.Split('|')[0];
                    if (type.StartsWith("Q"))
                    {
                        QjMenuVisibility = System.Windows.Visibility.Visible;

                        if (items.Count > 1)
                        {
                            XhMenuVisibility = System.Windows.Visibility.Collapsed;
                            DfxMenuVisibility = System.Windows.Visibility.Collapsed;
                            IsSingleQJ = false;

                            if (TypeNum == (int)DataType.SingleS)
                            {
                                if(index == DataCollection.Count - 1)
                                {
                                    QjMenuVisibility = System.Windows.Visibility.Collapsed;
                                    break;
                                }
                                nextsdm = DataCollection[index + 1] as StationDataMode;
                                if (nextsdm.StationNameProperty.Split(':')[0].Equals("1") ||
                                    nextsdm.StationNameProperty.Split(':')[0].Equals("3"))
                                {
                                    QjMenuVisibility = System.Windows.Visibility.Collapsed;
                                    break;
                                }
                            }
                            else if (TypeNum == (int)DataType.Single)
                            {
                                if (index == 0 || index == 1)
                                {
                                    QjMenuVisibility = System.Windows.Visibility.Collapsed;
                                    break;
                                }
                                nextsdm = DataCollection[index - 1] as StationDataMode;
                                if (nextsdm.StationNameProperty.Split(':')[0].Equals("1") ||
                                    nextsdm.StationNameProperty.Split(':')[0].Equals("3") )
                                {
                                    QjMenuVisibility = System.Windows.Visibility.Collapsed;
                                    break;
                                }
                            }
                            QjMenuVisibility = System.Windows.Visibility.Visible;
                        }
                        else
                            IsSingleQJ = true;

                        XhMenuVisibility = System.Windows.Visibility.Collapsed;
                        DfxMenuVisibility = System.Windows.Visibility.Collapsed;
                    }
                    else if (type.StartsWith("3"))
                    {
                        DfxMenuVisibility = System.Windows.Visibility.Visible;
                        if (items.Count > 1)
                        {
                            DfxMenuVisibility = System.Windows.Visibility.Collapsed;
                            XhMenuVisibility = System.Windows.Visibility.Collapsed;
                            QjMenuVisibility = System.Windows.Visibility.Collapsed;
                            break;
                        }
                        XhMenuVisibility = System.Windows.Visibility.Collapsed;
                        QjMenuVisibility = System.Windows.Visibility.Collapsed;
                    }
                    else if (type.StartsWith("1"))
                    {
                        XhMenuVisibility = System.Windows.Visibility.Collapsed;
                        DfxMenuVisibility = System.Windows.Visibility.Collapsed;
                        QjMenuVisibility = System.Windows.Visibility.Collapsed;
                        break;
                    }
                    else
                    {
                        XhMenuVisibility = System.Windows.Visibility.Visible;
                        if (DfxMenuVisibility == System.Windows.Visibility.Visible ||
                            QjMenuVisibility == System.Windows.Visibility.Visible)
                        {
                            XhMenuVisibility = System.Windows.Visibility.Collapsed;
                        }
                        DfxMenuVisibility = System.Windows.Visibility.Collapsed;
                        QjMenuVisibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }
            if (XhMenuVisibility == System.Windows.Visibility.Visible)
            {
                if (items.Count > 1)
                    CanMoveSignal = false;
                else
                    CanMoveSignal = true;
            }
            
        }
        private RelayCommand _showdatachangedCommand;

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

        private RelayCommand<SelectionChangedEventArgs> _selectionChangedCommand;

        /// <summary>
        /// Gets the SelectionChangedCommand.
        /// </summary>
        public RelayCommand<SelectionChangedEventArgs> SelectionChangedCommand
        {
            get
            {
                return _selectionChangedCommand
                    ?? (_selectionChangedCommand = new RelayCommand<SelectionChangedEventArgs>(
                    p =>
                    {
                        //SelectedItems.Clear();
                        foreach (IDataModel item in p.AddedItems)
                        {
                            SelectedItems.Add(item);
                        }

                        foreach (IDataModel item in p.RemovedItems)
                        {
                            SelectedItems.Remove(item);
                        }

                        IsShowSignale = checkSignal();
                        IsShowQujian = checkQujian();
                        IsShowDianFX = checkDianfx();
                        checkMenuFunc(selectedItems);
                        sendSignalInfo();                                               
                    }));
            }
        }

        private RelayCommand _showrightdialogCommand;

        /// <summary>
        /// Gets the ShowRightDialogCommand.
        /// </summary>
        public RelayCommand ShowRightDialogCommand
        {
            get
            {
                return _showrightdialogCommand
                    ?? (_showrightdialogCommand = new RelayCommand(
                    () =>
                    {
                        MessengerInstance.Send<DataType>((DataType)TypeNum, "ShowRightDialog"); 
                    }));
            }
        }

        private RelayCommand<string> _exCommand;

        /// <summary>
        /// Gets the ExCommand.
        /// </summary>
        public RelayCommand<string> ExCommand
        {
            get
            {
                return _exCommand
                    ?? (_exCommand = new RelayCommand<string>(
                    p =>
                    {
                        MessengerInstance.Send(p, "ExCommand");
                    }));
            }
        }
           
    }
}