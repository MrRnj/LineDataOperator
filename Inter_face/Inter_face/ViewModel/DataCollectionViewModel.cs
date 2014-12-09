﻿using ExtractData;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inter_face.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;


namespace Inter_face.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// 里程由小到大为规定下行（实际上行），由大到小为规定上行（实际下行）
    /// 0代表上行，1代表下行
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class DataCollectionViewModel : ViewModelBase
    {
        private string _currentdir = Environment.CurrentDirectory;
        private string _Pdpath;
        private string _Qxpath;
        private string _Bjpath;
        private string _XhPath;        

        private GraphyDataOper GDoper;
        List<ExtractData.ChangeToTxt.PoduOutputData> pdxlist;
        List<ExtractData.ChangeToTxt.PoduOutputData> pdslist;

        List<ChangeToTxt.QuxianOutputData> qxxlist;
        List<ChangeToTxt.QuxianOutputData> qxslist;

        List<ChangeToTxt.CheZhanOutputData> czxlist;
        List<ChangeToTxt.CheZhanOutputData> czslist;

        List<ChangeToTxt.CheZhanOutputData> xhxlist;
        List<ChangeToTxt.CheZhanOutputData> xhslist;

        List<string> cdlxlist;
        List<string> cdlslist;

        MoveXinhaoWindow mxwindow;
        ModifyQujianWindow mqjwindow;
        StationWindow Swindow;

        /// <summary>
        /// The <see cref="Direction" /> property's name.
        /// </summary>
        public const string DirectionPropertyName = "Direction";
        //0:上行  1：下行
        private int _directionProperty = 0;

        /// <summary>
        /// Sets and gets the Direction property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Direction
        {
            get
            {
                return _directionProperty;
            }

            set
            {
                if (_directionProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(DirectionPropertyName);
                _directionProperty = value;
                RaisePropertyChanged(DirectionPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="ScaleProperty" /> property's name.
        /// </summary>
        public const string ScalePropertyPropertyName = "ScaleProperty";

        private int _scaleProperty = 10;

        /// <summary>
        /// Sets and gets the ScaleProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int ScaleProperty
        {
            get
            {
                return _scaleProperty;
            }

            set
            {
                if (_scaleProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(ScalePropertyPropertyName);
                _scaleProperty = value;
                RaisePropertyChanged(ScalePropertyPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CountProperty" /> property's name.
        /// </summary>
        public const string CountPropertyPropertyName = "CountProperty";

        private int _countProperty = 0;

        /// <summary>
        /// Sets and gets the CountProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int CountProperty
        {
            get
            {
                return _countProperty;
            }

            set
            {
                if (_countProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(CountPropertyPropertyName);
                _countProperty = value;
                RaisePropertyChanged(CountPropertyPropertyName);
            }
        }

        List<ISingleDataViewModel> _DataBin;
        
        ObservableCollection<ISingleDataViewModel> _datascollection;
        public ObservableCollection<ISingleDataViewModel> DatasCollection
        {
            get
            {
                return _datascollection;
            }
            set { _datascollection = value; }
        }

        /// <summary>
        /// The <see cref="CurrentDatasProperty" /> property's name.
        /// </summary>
        public const string CurrentDatasPropertyPropertyName = "CurrentDatasProperty";

        private ISingleDataViewModel _CurrentDatasProperty = null;

        /// <summary>
        /// Sets and gets the CurrentDatasProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ISingleDataViewModel CurrentDatasProperty
        {
            get
            {
                return _CurrentDatasProperty;
            }

            set
            {
                if (_CurrentDatasProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(CurrentDatasPropertyPropertyName);

                if (value != null)
                {
                    if (value.TypeNum == (int)DataType.Single)
                        SeletedXinhaoS = false;
                    else if (value.TypeNum == (int)DataType.SingleS)
                        SeletedXinhaoS = true;
                }
                
                _CurrentDatasProperty = value;
                RaisePropertyChanged(CurrentDatasPropertyPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsPoduLoadedProperty" /> property's name.
        /// </summary>
        public const string IsPoduLoadedPropertyPropertyName = "IsPoduLoadedProperty";

        private bool _ispoduloadedProperty = false;

        /// <summary>
        /// Sets and gets the IsPoduLoadedProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsPoduLoadedProperty
        {
            get
            {
                return _ispoduloadedProperty;
            }

            set
            {
                if (_ispoduloadedProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(IsPoduLoadedPropertyPropertyName);
                _ispoduloadedProperty = value;
                RaisePropertyChanged(IsPoduLoadedPropertyPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsQuXianLoadedProperty" /> property's name.
        /// </summary>
        public const string IsQuXianLoadedPropertyName = "IsQuXianLoadedProperty";

        private bool _isquxianloadedProperty = false;

        /// <summary>
        /// Sets and gets the IsQuXianProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsQuXianLoadedProperty
        {
            get
            {
                return _isquxianloadedProperty;
            }

            set
            {
                if (_isquxianloadedProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(IsQuXianLoadedPropertyName);
                _isquxianloadedProperty = value;
                RaisePropertyChanged(IsQuXianLoadedPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsCheZhanLoadedProperty" /> property's name.
        /// </summary>
        public const string IsCheZhanLoadedPropertyName = "IsCheZhanLoadedProperty";

        private bool _ischezhanloadedProperty = false;

        /// <summary>
        /// Sets and gets the IsCheZhanProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsCheZhanLoadedProperty
        {
            get
            {
                return _ischezhanloadedProperty;
            }

            set
            {
                if (_ischezhanloadedProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(IsCheZhanLoadedPropertyName);
                _ischezhanloadedProperty = value;
                RaisePropertyChanged(IsCheZhanLoadedPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsLiChengLoadedProperty" /> property's name.
        /// </summary>
        public const string IsLiChengLoadedPropertyName = "IsLiChengLoadedProperty";

        private bool _islichengloadedProperty = false;

        /// <summary>
        /// Sets and gets the IsLiChengLoadedProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsLiChengLoadedProperty
        {
            get
            {
                return _islichengloadedProperty;
            }

            set
            {
                if (_islichengloadedProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(IsLiChengLoadedPropertyName);
                _islichengloadedProperty = value;
                RaisePropertyChanged(IsLiChengLoadedPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsXinhaoLoadedProperty" /> property's name.
        /// </summary>
        public const string IsXinhaoLoadedPropertyName = "IsXinhaoLoadedProperty";

        private bool _isxinhaoloadedProperty = false;

        /// <summary>
        /// Sets and gets the IsXinhaoLoadedProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsXinhaoLoadedProperty
        {
            get
            {
                return _isxinhaoloadedProperty;
            }

            set
            {
                if (_isxinhaoloadedProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(IsXinhaoLoadedPropertyName);
                _isxinhaoloadedProperty = value;
                RaisePropertyChanged(IsXinhaoLoadedPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsXinhaoSLoaded" /> property's name.
        /// </summary>
        public const string IsXinhaoSLoadedPropertyName = "IsXinhaoSLoaded";

        private bool _isxinhaosProperty = false;

        /// <summary>
        /// Sets and gets the IsXinhaoSLoaded property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsXinhaoSLoaded
        {
            get
            {
                return _isxinhaosProperty;
            }

            set
            {
                if (_isxinhaosProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(IsXinhaoSLoadedPropertyName);
                _isxinhaosProperty = value;
                RaisePropertyChanged(IsXinhaoSLoadedPropertyName);
            }
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
        /// The <see cref="MoveingDistence" /> property's name.
        /// </summary>
        public const string MoveingDistencePropertyName = "MoveingDistence";

        private int _movedistenceProperty = 0;

        /// <summary>
        /// Sets and gets the MoveingDistence property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int MoveingDistence
        {
            get
            {
                return _movedistenceProperty;
            }

            set
            {
                if (_movedistenceProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(MoveingDistencePropertyName);
                _movedistenceProperty = value;
                RaisePropertyChanged(MoveingDistencePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="TabItemIndex" /> property's name.
        /// </summary>
        public const string TabItemIndexPropertyName = "TabItemIndex";

        private int _tabitemProperty = 1;

        /// <summary>
        /// Sets and gets the TabItemIndex property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int TabItemIndex
        {
            get
            {
                return _tabitemProperty;
            }

            set
            {
                if (_tabitemProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(TabItemIndexPropertyName);
                _tabitemProperty = value;
                RaisePropertyChanged(TabItemIndexPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="DivParts" /> property's name.
        /// </summary>
        public const string DivPartsPropertyName = "DivParts";

        private int _divpartsProperty = 1;

        /// <summary>
        /// Sets and gets the DivParts property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int DivParts
        {
            get
            {
                return _divpartsProperty;
            }

            set
            {
                if (_divpartsProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(DivPartsPropertyName);
                _divpartsProperty = value;
                RaisePropertyChanged(DivPartsPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="SeletedXinhaoS" /> property's name.
        /// </summary>
        public const string SeletedXinhaoSPropertyName = "SeletedXinhaoS";

        private bool _seletedxinhaosProperty = false;

        /// <summary>
        /// Sets and gets the SeletedXinhaoS property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool SeletedXinhaoS
        {
            get
            {
                return _seletedxinhaosProperty;
            }

            set
            {
                if (_seletedxinhaosProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(SeletedXinhaoSPropertyName);
                _seletedxinhaosProperty = value;
                RaisePropertyChanged(SeletedXinhaoSPropertyName);
            }
        }
        /// <summary>
        /// Initializes a new instance of the DataCollectionViewModel class.
        /// </summary>
        public DataCollectionViewModel()
        {
            _datascollection = new ObservableCollection<ISingleDataViewModel>();
            _Pdpath = Path.Combine(_currentdir, @"excelmodels\接坡面数据_Single.xlsx");
            _Qxpath = Path.Combine(_currentdir, @"excelmodels\接曲线数据_single.xlsx");
            _Bjpath = Path.Combine(_currentdir, @"excelmodels\接标记数据_single.xlsx");
            _XhPath = Path.Combine(_currentdir, @"excelmodels\信号机数据_single.xlsx");
            GDoper = GraphyDataOper.CreatOper(_Pdpath, _Bjpath, _Qxpath, _XhPath);
            _DataBin = new List<ISingleDataViewModel>();

            MessengerInstance.Register<ISingleDataViewModel>(this, "DisapearData",
                p =>
                {

                    switch (p.TypeNum)
                    {
                        case (int)DataType.Podu:
                            IsPoduLoadedProperty = false;
                            break;
                        case (int)DataType.Quxian:
                            IsQuXianLoadedProperty = false;
                            break;
                        case (int)DataType.Station:
                            IsCheZhanLoadedProperty = false;
                            break;
                        case (int)DataType.Position:
                            IsLiChengLoadedProperty = false;
                            break;
                        case (int)DataType.Single:
                            IsXinhaoLoadedProperty = false;
                            break;
                        case (int)DataType.SingleS:
                            IsXinhaoSLoaded = false;
                            break;
                        default:
                            break;
                    }

                    DatasCollection.Remove(p);

                    if (!_DataBin.Contains(p))
                        _DataBin.Add(p);
                    CountProperty = DatasCollection.Count;
                }
                );

            MessengerInstance.Register<bool>(this, "CleanData",
                p =>
                {
                    DatasCollection.Clear();
                    _DataBin.Clear();
                    CountProperty = 0;
                });
            MessengerInstance.Register<DataType>(this, "SelectedChanged",
                (p) =>
                {
                    int n = 0;
                    foreach (ISingleDataViewModel item in DatasCollection)
                    {
                        if (item.TypeNum == (int)p)
                        {
                            SelectedIndex = n;
                            break;
                        }
                        n++;
                    }
                });
            /*MessengerInstance.Register<int>(this, "SelectedTabItem",
                (p) =>
                {
                    TabItemIndex = p;
                }
                );*/
            MessengerInstance.Register<double>(this, "Distence",
                (p) =>
                {
                    StationDataMode sdm = CurrentDatasProperty.CurrentDataProperty as StationDataMode;
                    int index = CurrentDatasProperty.DataCollection.IndexOf(sdm);

                    if (sdm != null)
                    {
                        CurrentDatasProperty.SelectedIndex = -1;
                        moveXinhaoX(sdm.StationNameProperty, (float)Math.Round(p));
                        CurrentDatasProperty.SelectedIndex = index;

                        string[] parts = { };
                        float offset1 = 0;
                        float offset2 = 0;

                        sdm = CurrentDatasProperty.DataCollection[index] as StationDataMode;
                        StationDataMode nextsdm = null;
                        float newposition = 0;

                        if (index == CurrentDatasProperty.DataCollection.Count - 2)
                        {
                            nextsdm = CurrentDatasProperty.DataCollection[index + 1] as StationDataMode;
                            newposition = nextsdm.PositionProperty + (nextsdm.LengthProperty + 20) * nextsdm.ScaleProperty / 1000;
                        }
                        else
                        {
                            nextsdm = CurrentDatasProperty.DataCollection[index + 2] as StationDataMode;
                            newposition = nextsdm.PositionProperty;
                        }
                        StationDataMode presdm = CurrentDatasProperty.DataCollection[index - 1] as StationDataMode;

                        for (int i = presdm.SectionNumProperty; i < sdm.SectionNumProperty; i++)
                        {
                            parts = cdlxlist[i - 1].Split(':');
                            offset1 += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                        }

                        for (int i = sdm.SectionNumProperty; i < nextsdm.SectionNumProperty; i++)
                        {
                            parts = cdlxlist[i - 1].Split(':');
                            offset2 += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                        }

                        GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string>
                                                  (string.Format("{0}:{1}:{2}:{3}:{4}",
                                                  presdm.PositionProperty, newposition,
                                                  sdm.PositionProperty, offset1.ToString(), offset2.ToString()),
                                                  "Resources");
                    }
                });
            MessengerInstance.Register<string>(this, "InsertXinhao", 
                (p) => 
                {
                    string[] parts = p.Split(':');
                    InsertXinhaoX(float.Parse(parts[0]), int.Parse(parts[1]), parts[2], null);
                });
            MessengerInstance.Register<List<StationDataMode>>(this, "UpdataStationSignal", 
                (p) => 
                {
                    UpdataStationSignals(p);
                    Swindow.Close();
                });
        }

        public void LoadPoduXData()
        {
            if (!IsPoduLoadedProperty)
            {
                removeAdataItem((int)DataType.Podu);
                return;
            }

            SingleDataViewModel sdvm = null;

            foreach (ISingleDataViewModel Singleitem in _DataBin)
            {
                if (Singleitem.TypeNum == (int)DataType.Podu)
                {
                    sdvm = (SingleDataViewModel)Singleitem;
                    sdvm.ShowDataProperty = true;
                    break;
                }
            }

            if (sdvm == null)
            {    

                sdvm = new SingleDataViewModel();
                sdvm.TypeNameProperty = "坡度";
                sdvm.ShowDataProperty = true;
                sdvm.TypeNum = (int)DataType.Podu;                

                try
                {
                    GDoper.GetPoDuData(out pdxlist, out pdslist);
                    cdlxlist = pdxlist.TakeWhile(p => p.Cdl != "+:+").Select(q => q.Cdl).ToList();

                    foreach (ExtractData.ChangeToTxt.PoduOutputData item in pdxlist)
                    {
                        sdvm.DataCollection.Add(new LineDataModel()
                        {
                            HatProperty = item.Gh,
                            LengthProperty = float.Parse(item.Pc) / ScaleProperty,
                            PositionProperty = float.Parse(item.Qdglb),
                            Type = DataType.Podu,
                            SectionNumProperty = int.Parse(item.Ldh),
                            HeightProperty = float.Parse(item.Bg),
                            AngleProperty = float.Parse(item.Pd),
                            ScaleProperty = ScaleProperty,
                            SelectedProperty = false,
                            PathDataProperty = float.Parse(item.Pd) > 0 ?
                            "3:1 0:#00DC5625:#FF35F30E:M0,500 L500,0" : float.Parse(item.Pd) == 0 ?
                            "3:1 0:#00DC5625:#FF35F30E:M0,0 L500,0" : "3:1 0:#00DC5625:#FF35F30E:M0,0 L500,500"
                        });
                    }
                }

                catch (NullReferenceException ure)
                {
                    MessengerInstance.Send<string>(ure.Message, "ReadDataError");
                }
            }

            if (_datascollection.Count == 0)
            {
                _datascollection.Add(sdvm);
            }
            else
            {
                for (int i = 0; i < _datascollection.Count; i++)
                {
                    if (sdvm.TypeNum < _datascollection[i].TypeNum)
                    {
                        _datascollection.Insert(i, sdvm);
                        break;
                    }
                    else if (i == _datascollection.Count - 1)
                    {
                        _datascollection.Add(sdvm);
                        break;
                    }
                }
            }

            IsPoduLoadedProperty = true;
            CountProperty = DatasCollection.Count;            
        }

        public void LoadQuXianXData()
        {
            if (!IsQuXianLoadedProperty)
            {
                removeAdataItem((int)DataType.Quxian);
                return;
            }
            
            SingleDataViewModel sdvm = null;

            foreach (ISingleDataViewModel Singleitem in _DataBin)
            {
                if (Singleitem.TypeNum == (int)DataType.Quxian)
                {
                    sdvm = (SingleDataViewModel)Singleitem;
                    sdvm.ShowDataProperty = true;
                    break;
                }
            }

             if (sdvm == null)
             {                 

                 try
                 {
                     GDoper.GetQuxianData(out qxxlist, out qxslist);

                     sdvm = new SingleDataViewModel();
                     sdvm.TypeNameProperty = "曲线";
                     sdvm.ShowDataProperty = true;
                     sdvm.TypeNum = (int)DataType.Quxian;

                     int n = 0;
                     string[] parts = { };                    
                     float offset = 0;

                     float len = (float.Parse(qxxlist[0].Qdglb) - float.Parse(pdxlist[0].Qdglb)) * 1000 / ScaleProperty;
                     float pos = float.Parse(pdxlist[0].Qdglb);                    
                     
                     for (int i = int.Parse(pdxlist[0].Ldh); i < int.Parse(qxxlist[0].Ldh); i++)
                     {
                         parts = cdlxlist[n].Split(':');
                         offset += float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]);                                           
                         n++;
                     }

                     len = ((float.Parse(qxxlist[0].Qdglb) -
                             float.Parse(pdxlist[0].Qdglb)) * 1000 - offset) / ScaleProperty;  

                     sdvm.DataCollection.Add(new LineDataModel()
                     {
                         HatProperty = qxxlist[0].Gh,
                         LengthProperty = len,
                         PositionProperty = pos,                        
                         RadioProperty = 0,
                         Type = DataType.Quxian,
                         SectionNumProperty = int.Parse(pdxlist[0].Ldh),
                         ScaleProperty = ScaleProperty,
                         SelectedProperty = false,
                         PathDataProperty = "3:1 0:#00DC5625:#FF35F30E:M0,0 L500,0"
                     });                     

                     foreach (ChangeToTxt.QuxianOutputData item in qxxlist)
                     {                         
                         sdvm.DataCollection.Add(new LineDataModel()
                         {
                             HatProperty = item.Gh,
                             LengthProperty = (float.Parse(item.Qxc)) / ScaleProperty,
                             PositionProperty = float.Parse(item.Qdglb),
                             RadioProperty = int.Parse(item.Qxbj),
                             Type = DataType.Quxian,
                             SectionNumProperty = int.Parse(item.Ldh),
                             ScaleProperty = ScaleProperty,
                             SelectedProperty = false,
                             PathDataProperty = int.Parse(item.Qxbj) == 0 ? "3:1 0:#00DC5625:#FF35F30E:M0,0 L500,0" :
                             int.Parse(item.Qxwq) == 1 ?
                             "3:1 0:#00DC5625:#FF35F30E:M0,500 L0,400 500,300 2000,300 2500,400 2500,500" :
                             "3:1 0:#00DC5625:#FF35F30E:M0,0 L0,150 500,300 2000,300 2500,150 2500,0"
                         });

                         if (sdvm.DataCollection.Count > 2)
                         {
                             LineDataModel preldm = (LineDataModel)sdvm.DataCollection[sdvm.DataCollection.Count - 2];
                             LineDataModel curldm = (LineDataModel)sdvm.DataCollection[sdvm.DataCollection.Count - 1];

                             int currentldh = preldm.SectionNumProperty;
                             offset = 0;

                             pos = preldm.PositionProperty +
                                     preldm.LengthProperty * preldm.ScaleProperty / 1000;
                             for (int i = currentldh - 1; i < cdlxlist.Count; i++)
                             {
                                 parts = cdlxlist[i].Split(':');
                                 if (preldm.LengthProperty * preldm.ScaleProperty + preldm.PositionProperty * 1000 + offset >
                                     float.Parse(parts[0].Split('+')[1]))
                                 {
                                     offset += (float)Math.Round(float.Parse(parts[1].Split('+')[1]) -
                                             float.Parse(parts[0].Split('+')[1]), 3);
                                     currentldh++;
                                     continue;
                                 }
                                 break;
                             }
                             pos = pos + offset / 1000;
                             len = (curldm.PositionProperty - pos) * 1000;
                             
                             if (len != 0)
                             {

                                 for (int i = currentldh - 1; i < curldm.SectionNumProperty - 1; i++)
                                 {
                                     parts = cdlxlist[i].Split(':');
                                     offset += (float)Math.Round(float.Parse(parts[1].Split('+')[1]) -
                                             float.Parse(parts[0].Split('+')[1]), 3);
                                 }

                                 len = (len - offset) / ScaleProperty;

                                 LineDataModel insertldm = new LineDataModel()
                                 {
                                     HatProperty = cdlxlist[currentldh - 1].Split(':')[0].Split('+')[0],
                                     LengthProperty = len,
                                     PositionProperty = pos,
                                     RadioProperty = 0,
                                     Type = DataType.Quxian,
                                     SectionNumProperty = currentldh,
                                     ScaleProperty = ScaleProperty,
                                     SelectedProperty = false,
                                     PathDataProperty = "3:1 0:#00DC5625:#FF35F30E:M0,0 L500,0"
                                 };

                                 sdvm.DataCollection.Insert(sdvm.DataCollection.Count - 1, insertldm);
                             }                             
                         }
                     }
                 }

                 catch (NullReferenceException ure)
                 {
                     MessengerInstance.Send<string>(ure.Message, "ReadDataError");
                 }
             }

            if (_datascollection.Count == 0)
            {
                _datascollection.Add(sdvm);
            }
            else
            {
                for (int i = 0; i < _datascollection.Count; i++)
                {
                    if (sdvm.TypeNum < _datascollection[i].TypeNum)
                    {
                        _datascollection.Insert(i, sdvm);
                        break;
                    }
                    else if (i == _datascollection.Count - 1)
                    {
                        _datascollection.Add(sdvm);
                        break;
                    }
                }
            }

            CountProperty = DatasCollection.Count;
            IsQuXianLoadedProperty = true;
        }
        public void LoadCheZhanXData()
        {
            if (!IsCheZhanLoadedProperty)
            {
                removeAdataItem((int)DataType.Station);
                return;
            }
            
            SingleDataViewModel sdvm = null;

            foreach (ISingleDataViewModel Singleitem in _DataBin)
            {
                if (Singleitem.TypeNum == (int)DataType.Station)
                {
                    sdvm = (SingleDataViewModel)Singleitem;
                    sdvm.ShowDataProperty = true;
                    break;
                }
            }

             if (sdvm == null)
             {                 

                 try
                 {
                     GDoper.GetBjData(out czxlist, out czslist);

                     sdvm = new SingleDataViewModel();
                     sdvm.TypeNameProperty = "车站";
                     sdvm.ShowDataProperty = true;
                     sdvm.TypeNum = (int)DataType.Station;

                     int n = 0;
                     int m = 0;
                     string[] parts = { };                     
                     float offset = 0;
                     float len = 0;
                     float pos = 0;
                     

                     foreach (ChangeToTxt.CheZhanOutputData item in czxlist)
                     {

                         offset = 0;
                         if (n == 0)
                         {
                             len = ((float.Parse(item.Glb) - float.Parse(pdxlist[0].Qdglb)) * 1000 - 50) / ScaleProperty > 0 ?
                                 ((float.Parse(item.Glb) - float.Parse(pdxlist[0].Qdglb)) * 1000 - 50) / ScaleProperty : 0;
                             pos = float.Parse(pdxlist[0].Qdglb);

                             for (int i = int.Parse(pdxlist[0].Ldh); i < int.Parse(czxlist[0].Ldh); i++)
                             {
                                 parts = cdlxlist[m].Split(':');
                                 offset += float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]);                                 
                                 m++;
                             }
                             len = len - offset / ScaleProperty;

                             sdvm.DataCollection.Add(new StationDataMode()
                             {
                                 HatProperty = item.Gh,
                                 LengthProperty = len,
                                 PositionProperty = pos,
                                 Type = DataType.Station,
                                 SectionNumProperty = 1,
                                 ScaleProperty = ScaleProperty,
                                 SelectedProperty = false,
                                 PathDataProperty = "5:6 2 1 2:#00DC5625:#FF000000:M0,0 L500,0",
                                 StationNameProperty = "区间"
                             });
                         }
                         else
                         {
                             ChangeToTxt.CheZhanOutputData presdm = czxlist[n - 1];                             
                             pos = float.Parse(presdm.Glb);

                             for (int i = int.Parse(presdm.Ldh); i < int.Parse(item.Ldh); i++)
                             {
                                 parts = cdlxlist[m].Split(':');
                                 offset += float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]);                                 
                                 m++;
                             }

                             len = ((float.Parse(item.Glb) - float.Parse(presdm.Glb)) * 1000 - 100 - offset) / ScaleProperty;
                             sdvm.DataCollection.Add(new StationDataMode()
                             {
                                 HatProperty = presdm.Gh,
                                 LengthProperty = len,
                                 PositionProperty = pos,
                                 Type = DataType.Station,
                                 SectionNumProperty = int.Parse(presdm.Ldh),
                                 ScaleProperty = ScaleProperty,
                                 SelectedProperty = false,
                                 PathDataProperty = "5:6 2 1 2:#00DC5625:#FF000000:M0,0 L500,0",
                                 StationNameProperty = "区间"
                             });
                         }

                         sdvm.DataCollection.Add(new StationDataMode()
                         {
                             HatProperty = item.Gh,
                             LengthProperty = 100 / ScaleProperty,
                             PositionProperty = float.Parse(item.Glb),
                             Type = DataType.Station,
                             SectionNumProperty = int.Parse(item.Ldh),
                             ScaleProperty = ScaleProperty,
                             SelectedProperty = false,
                             PathDataProperty = "1:1 0:#FFDC5625:#FF35F30E:M15,0 L30,15 L15,30 L0,15 z",
                             StationNameProperty = item.Bjsj
                         });

                         n++;
                     }
                 }

                 catch (NullReferenceException ure)
                 {
                     MessengerInstance.Send<string>(ure.Message, "ReadDataError");
                 }
             }

            if (_datascollection.Count == 0)
            {
                _datascollection.Add(sdvm);
            }
            else
            {
                for (int i = 0; i < _datascollection.Count; i++)
                {
                    if (sdvm.TypeNum < _datascollection[i].TypeNum)
                    {
                        _datascollection.Insert(i, sdvm);                        
                        break;
                    }
                    else if (i == _datascollection.Count - 1 && sdvm.TypeNum != _datascollection[i].TypeNum)
                    {
                        _datascollection.Add(sdvm);
                        break;
                    }
                }
            }

            IsCheZhanLoadedProperty = true;
            CountProperty = DatasCollection.Count;            
        }

        public void LoadXinHaoSData()
        {
            if (!IsXinhaoSLoaded)
            {
                removeAdataItem((int)DataType.SingleS);
                return;
            }

            SingleDataViewModel sdvm = null;

            foreach (ISingleDataViewModel Singleitem in _DataBin)
            {
                if (Singleitem.TypeNum == (int)DataType.SingleS)
                {
                    sdvm = (SingleDataViewModel)Singleitem;
                    sdvm.ShowDataProperty = true;
                    break;
                }
            }

            if (sdvm == null)
            {
                try
                {
                    GDoper.GetXhDataS(out xhslist);

                    sdvm = new SingleDataViewModel();
                    sdvm.TypeNameProperty = "信号机(S)";
                    sdvm.ShowDataProperty = true;
                    sdvm.TypeNum = (int)DataType.SingleS;

                    int n = 0;
                    int m = 0;
                    string[] parts = { };
                    float offset = 0;
                    float len = 0;
                    float pos = 0;

                    if (xhslist.Count == 0)
                    {
                        offset = 0;

                        pos = float.Parse(pdxlist[0].Qdglb);

                        for (int i = int.Parse(pdxlist[0].Ldh); i < int.Parse(pdxlist[pdxlist.Count - 1].Ldh); i++)
                        {
                            parts = cdlxlist[m].Split(':');
                            offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                            m++;
                        }

                        len = ((float.Parse(pdxlist[pdxlist.Count - 1].Qdglb) -
                            float.Parse(pdxlist[0].Qdglb)) * 1000 +
                            float.Parse(pdxlist[pdxlist.Count - 1].Pc) - offset) / ScaleProperty;

                        sdvm.DataCollection.Add(new StationDataMode()
                        {
                            HatProperty = pdxlist[0].Gh,
                            LengthProperty = len,
                            PositionProperty = pos,
                            Type = DataType.Single,
                            SectionNumProperty = int.Parse(pdxlist[0].Ldh),
                            ScaleProperty = ScaleProperty,
                            SelectedProperty = false,
                            PathDataProperty = "5:6 2 1 2:#00DC5625:#FF000000:M0,0 L500,0",
                            StationNameProperty = "Q" + pdxlist[0].Ldh + "+" + n.ToString()
                        });
                    }
                    else
                    {
                        foreach (ChangeToTxt.CheZhanOutputData item in xhslist)
                        {
                            offset = 0;
                            if (n == 0)
                            {
                                pos = float.Parse(pdxlist[0].Qdglb);

                                for (int i = int.Parse(pdxlist[0].Ldh); i < int.Parse(xhslist[0].Ldh); i++)
                                {
                                    parts = cdlxlist[m].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                    m++;
                                }

                                len = ((float.Parse(item.Glb) - float.Parse(pdxlist[0].Qdglb)) * 1000) / ScaleProperty > 0 ?
                                    ((float.Parse(item.Glb) - float.Parse(pdxlist[0].Qdglb)) * 1000 - offset)
                                    / ScaleProperty : 0;

                                sdvm.DataCollection.Add(new StationDataMode()
                                {
                                    HatProperty = item.Gh,
                                    LengthProperty = len,
                                    PositionProperty = pos,
                                    Type = DataType.Single,
                                    SectionNumProperty = 1,
                                    ScaleProperty = ScaleProperty,
                                    SelectedProperty = false,
                                    PathDataProperty = "5:6 2 1 2:#00DC5625:#FF000000:M0,0 L500,0",
                                    StationNameProperty = "Q" + pdxlist[0].Ldh + "+" + n.ToString()
                                });
                            }
                            else
                            {
                                ChangeToTxt.CheZhanOutputData presdm = xhslist[n - 1];
                                //len = (float)(float.Parse(item.Glb) - float.Parse(presdm.Glb) - 0.2) * 1000 / ScaleProperty;
                                pos = float.Parse(presdm.Glb);

                                for (int i = int.Parse(presdm.Ldh); i < int.Parse(item.Ldh); i++)
                                {
                                    parts = cdlxlist[m].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                    m++;
                                }

                                len = ((float.Parse(item.Glb) - float.Parse(presdm.Glb)) * 1000 - 200 - offset)
                                        / ScaleProperty;

                                sdvm.DataCollection.Add(new StationDataMode()
                                {
                                    HatProperty = presdm.Gh,
                                    LengthProperty = len,
                                    PositionProperty = pos,
                                    Type = DataType.Single,
                                    SectionNumProperty = int.Parse(presdm.Ldh),
                                    ScaleProperty = ScaleProperty,
                                    SelectedProperty = false,
                                    PathDataProperty = "5:6 2 1 2:#00DC5625:#FF000000:M0,0 L500,0",
                                    StationNameProperty = "Q" + int.Parse(presdm.Ldh).ToString() + "+" +
                                    (presdm.Bjlx.Equals("1") ?
                                        //标记类型：信号机编号：所属车站名：信号机类型：车站中心坐标
                                    string.Format("{0}:{1}:{2}", presdm.Bjlx, presdm.Bjsj, FormatStationPosition(presdm.Bjsj.Split(':')[1])) :
                                    string.Format("{0}:{1}", presdm.Bjlx, presdm.Bjsj))
                                });
                            }

                            sdvm.DataCollection.Add(new StationDataMode()
                            {
                                HatProperty = item.Gh,
                                LengthProperty = 200 / ScaleProperty,
                                PositionProperty = float.Parse(item.Glb),
                                Type = DataType.Single,
                                SectionNumProperty = int.Parse(item.Ldh),
                                ScaleProperty = ScaleProperty,
                                SelectedProperty = false,
                                PathDataProperty = "1:1 0:#FFDC5625:#FF35F30E:M-27,30 L5,30 M57,31 C57,47 45,59 30,59 C15,59 2.5,47 3,31 C3,15 15,3 30,3 C45,3 57,15 57,31 z",
                                StationNameProperty = (item.Bjlx.Equals("1") ?
                                    string.Format("{0}:{1}:{2}", item.Bjlx, item.Bjsj, FormatStationPosition(item.Bjsj.Split(':')[1])) :
                                    string.Format("{0}:{1}", item.Bjlx, item.Bjsj))
                            });

                            n++;
                        }

                        ChangeToTxt.PoduOutputData lastpd = pdxlist.Last();
                        StationDataMode lastxh = (StationDataMode)sdvm.DataCollection.Last();

                        pos = lastxh.PositionProperty;

                        for (int i = lastxh.SectionNumProperty; i < int.Parse(lastpd.Ldh); i++)
                        {
                            parts = cdlxlist[i - 1].Split(':');
                            offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                        }

                        len = ((float.Parse(lastpd.Qdglb) - lastxh.PositionProperty) * 1000 - 200 + float.Parse(lastpd.Pc) - offset)
                                / ScaleProperty;

                        sdvm.DataCollection.Add(new StationDataMode()
                        {
                            HatProperty = lastxh.HatProperty,
                            LengthProperty = len,
                            PositionProperty = pos,
                            Type = DataType.Single,
                            SectionNumProperty = lastxh.SectionNumProperty,
                            ScaleProperty = ScaleProperty,
                            SelectedProperty = false,
                            PathDataProperty = "5:6 2 1 2:#00DC5625:#FF000000:M0,0 L500,0",
                            StationNameProperty = "Q" + lastxh.SectionNumProperty.ToString() + "+" + lastxh.StationNameProperty
                        });
                    }
                }

                catch (NullReferenceException ure)
                {
                    MessengerInstance.Send<string>(ure.Message, "ReadDataError");
                }
            }

            if (_datascollection.Count == 0)
            {
                _datascollection.Add(sdvm);
            }
            else
            {
                for (int i = 0; i < _datascollection.Count; i++)
                {
                    if (sdvm.TypeNum < _datascollection[i].TypeNum)
                    {
                        _datascollection.Insert(i, sdvm);
                        break;
                    }
                    else if (i == _datascollection.Count - 1 && sdvm.TypeNum != _datascollection[i].TypeNum)
                    {
                        _datascollection.Add(sdvm);
                        break;
                    }
                }
            }

            IsXinhaoSLoaded = true;
            CountProperty = DatasCollection.Count;
        }

      

        public void LoadXinHaoData()
        {
            if (!IsXinhaoLoadedProperty)
            {
                removeAdataItem((int)DataType.Single);
                return;
            }

            SingleDataViewModel sdvm = null;

            foreach (ISingleDataViewModel Singleitem in _DataBin)
            {
                if (Singleitem.TypeNum == (int)DataType.Single)
                {
                    sdvm = (SingleDataViewModel)Singleitem;
                    sdvm.ShowDataProperty = true;
                    break;
                }
            }

            if (sdvm == null)
            {
                try
                {
                    GDoper.GetXhData(out xhxlist);

                    sdvm = new SingleDataViewModel();
                    sdvm.TypeNameProperty = "信号机(X)";
                    sdvm.ShowDataProperty = true;
                    sdvm.TypeNum = (int)DataType.Single;

                    int n = 0;
                    int m = 0;
                    string[] parts = { };
                    float offset = 0;
                    float len = 0;
                    float pos = 0;

                    if (xhxlist.Count == 0)
                    {
                        offset = 0;

                        pos = float.Parse(pdxlist[0].Qdglb);

                        for (int i = int.Parse(pdxlist[0].Ldh); i < int.Parse(pdxlist[pdxlist.Count - 1].Ldh); i++)
                        {
                            parts = cdlxlist[m].Split(':');
                            offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                            m++;
                        }

                        len = ((float.Parse(pdxlist[pdxlist.Count - 1].Qdglb) - 
                            float.Parse(pdxlist[0].Qdglb)) * 1000 +
                            float.Parse(pdxlist[pdxlist.Count - 1].Pc) - offset) / ScaleProperty;

                        sdvm.DataCollection.Add(new StationDataMode()
                        {
                            HatProperty = pdxlist[0].Gh,
                            LengthProperty = len,
                            PositionProperty = pos,
                            Type = DataType.Single,
                            SectionNumProperty =int.Parse(pdxlist[0].Ldh),
                            ScaleProperty = ScaleProperty,
                            SelectedProperty = false,
                            PathDataProperty = "5:6 2 1 2:#00DC5625:#FF000000:M0,0 L500,0",
                            StationNameProperty = "Q" + pdxlist[0].Ldh + "+" + n.ToString()
                        });
                    }
                    else
                    {
                        foreach (ChangeToTxt.CheZhanOutputData item in xhxlist)
                        {
                            offset = 0;
                            if (n == 0)
                            {
                                pos = float.Parse(pdxlist[0].Qdglb);

                                for (int i = int.Parse(pdxlist[0].Ldh); i < int.Parse(xhxlist[0].Ldh); i++)
                                {
                                    parts = cdlxlist[m].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                    m++;
                                }

                                len = ((float.Parse(item.Glb) - float.Parse(pdxlist[0].Qdglb)) * 1000) / ScaleProperty > 0 ?
                                    ((float.Parse(item.Glb) - float.Parse(pdxlist[0].Qdglb)) * 1000 - offset)
                                    / ScaleProperty : 0;

                                sdvm.DataCollection.Add(new StationDataMode()
                                {
                                    HatProperty = item.Gh,
                                    LengthProperty = len,
                                    PositionProperty = pos,
                                    Type = DataType.Single,
                                    SectionNumProperty = 1,
                                    ScaleProperty = ScaleProperty,
                                    SelectedProperty = false,
                                    PathDataProperty = "5:6 2 1 2:#00DC5625:#FF000000:M0,0 L500,0",
                                    StationNameProperty = "Q" + pdxlist[0].Ldh + "+" + n.ToString()
                                });
                            }
                            else
                            {
                                ChangeToTxt.CheZhanOutputData presdm = xhxlist[n - 1];
                                //len = (float)(float.Parse(item.Glb) - float.Parse(presdm.Glb) - 0.2) * 1000 / ScaleProperty;
                                pos = float.Parse(presdm.Glb);

                                for (int i = int.Parse(presdm.Ldh); i < int.Parse(item.Ldh); i++)
                                {
                                    parts = cdlxlist[m].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                    m++;
                                }

                                len = ((float.Parse(item.Glb) - float.Parse(presdm.Glb)) * 1000 - 200 - offset)
                                        / ScaleProperty;

                                sdvm.DataCollection.Add(new StationDataMode()
                                {
                                    HatProperty = presdm.Gh,
                                    LengthProperty = len,
                                    PositionProperty = pos,
                                    Type = DataType.Single,
                                    SectionNumProperty = int.Parse(presdm.Ldh),
                                    ScaleProperty = ScaleProperty,
                                    SelectedProperty = false,
                                    PathDataProperty = "5:6 2 1 2:#00DC5625:#FF000000:M0,0 L500,0",
                                    StationNameProperty = "Q" + int.Parse(presdm.Ldh).ToString() + "+" +
                                    (presdm.Bjlx.Equals("1") ?
                                        //标记类型：信号机编号：所属车站名：信号机类型：车站中心坐标
                                    string.Format("{0}:{1}:{2}", presdm.Bjlx, presdm.Bjsj, FormatStationPosition(presdm.Bjsj.Split(':')[1])) :
                                    string.Format("{0}:{1}", presdm.Bjlx, presdm.Bjsj))
                                });
                            }

                            sdvm.DataCollection.Add(new StationDataMode()
                            {
                                HatProperty = item.Gh,
                                LengthProperty = 200 / ScaleProperty,
                                PositionProperty = float.Parse(item.Glb),
                                Type = DataType.Single,
                                SectionNumProperty = int.Parse(item.Ldh),
                                ScaleProperty = ScaleProperty,
                                SelectedProperty = false,
                                PathDataProperty = "1:1 0:#FFDC5625:#FF35F30E:M-27,30 L5,30 M57,31 C57,47 45,59 30,59 C15,59 2.5,47 3,31 C3,15 15,3 30,3 C45,3 57,15 57,31 z",
                                StationNameProperty = (item.Bjlx.Equals("1") ?
                                    string.Format("{0}:{1}:{2}", item.Bjlx, item.Bjsj, FormatStationPosition(item.Bjsj.Split(':')[1])) :
                                    string.Format("{0}:{1}", item.Bjlx, item.Bjsj))
                            });

                            n++;
                        }

                        ChangeToTxt.PoduOutputData lastpd = pdxlist.Last();
                        StationDataMode lastxh = (StationDataMode)sdvm.DataCollection.Last();

                        pos = lastxh.PositionProperty;

                        for (int i = lastxh.SectionNumProperty; i < int.Parse(lastpd.Ldh); i++)
                        {
                            parts = cdlxlist[i - 1].Split(':');
                            offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                        }

                        len = ((float.Parse(lastpd.Qdglb) - lastxh.PositionProperty) * 1000 - 200 + float.Parse(lastpd.Pc) - offset)
                                / ScaleProperty;

                        sdvm.DataCollection.Add(new StationDataMode()
                        {
                            HatProperty = lastxh.HatProperty,
                            LengthProperty = len,
                            PositionProperty = pos,
                            Type = DataType.Single,
                            SectionNumProperty = lastxh.SectionNumProperty,
                            ScaleProperty = ScaleProperty,
                            SelectedProperty = false,
                            PathDataProperty = "5:6 2 1 2:#00DC5625:#FF000000:M0,0 L500,0",
                            StationNameProperty = "Q" + lastxh.SectionNumProperty.ToString() + "+" + lastxh.StationNameProperty
                        });
                    }
                }

                catch (NullReferenceException ure)
                {
                    MessengerInstance.Send<string>(ure.Message, "ReadDataError");
                }
            }

            if (_datascollection.Count == 0)
            {
                _datascollection.Add(sdvm);
            }
            else
            {
                for (int i = 0; i < _datascollection.Count; i++)
                {
                    if (sdvm.TypeNum < _datascollection[i].TypeNum)
                    {
                        _datascollection.Insert(i, sdvm);
                        break;
                    }
                    else if (i == _datascollection.Count - 1 && sdvm.TypeNum != _datascollection[i].TypeNum)
                    {
                        _datascollection.Add(sdvm);
                        break;
                    }
                }
            }

            IsXinhaoLoadedProperty = true;
            CountProperty = DatasCollection.Count;
        }

        private string FormatStationPosition(string stationame)
        {
            string pos;

            if (czxlist == null || czxlist.Count == 0)
            {
                throw new SatitonNotLoadedException("未载入车站数据，请先载入车站数据！");
            }
            else
            {
                foreach (ChangeToTxt.CheZhanOutputData item in czxlist)
                {
                    if(item.Bjsj.Equals(stationame))
                    {
                        pos = float.Parse(item.Glb).ToString("F3");
                        return string.Format("{0} {1}+{2}", item.Gh, pos.Split('.')[0], pos.Split('.')[1]);
                    }                    
                }
                return string.Empty;
            }
        }



        public void LoadLiChengXData()
        {
            if (!IsLiChengLoadedProperty)
            {
                removeAdataItem((int)DataType.Position);
                return;
            }

            SingleDataViewModel sdvm = null;

            foreach (ISingleDataViewModel Singleitem in _DataBin)
            {
                if (Singleitem.TypeNum == (int)DataType.Position)
                {
                    sdvm = (SingleDataViewModel)Singleitem;
                    sdvm.ShowDataProperty = true;
                    break;
                }
            }

            if (pdxlist == null || pdxlist.Count == 0) return;

            string pathes = string.Empty;
            float lc = 0;


            if (sdvm == null)
            {
                sdvm = new SingleDataViewModel();
                sdvm.TypeNameProperty = "里程";
                sdvm.ShowDataProperty = true;
                sdvm.TypeNum = (int)DataType.Position;
                try
                {
                    int starter = (int)Math.Floor(float.Parse(pdxlist[0].Qdglb));
                    int ender = (int)Math.Ceiling(float.Parse(pdxlist[pdxlist.Count - 1].Qdglb) +
                        float.Parse(pdxlist[pdxlist.Count - 1].Pc)/1000);

                    for (int i = starter; i < ender; i++)
                    {
                        lc = 0;
                        pathes = string.Empty;

                        if (i == starter)
                        {
                            int firstl = (int)Math.Round((float.Parse(pdxlist[0].Qdglb) - starter) * 1000, 0);
                            lc = firstl / ScaleProperty;
                            pathes = string.Format("M0,25 L0,40 {0},40", lc);

                            for (int j = (int)Math.Ceiling(firstl / 100d); j < 10; j++)
                            {
                                pathes += string.Format("M{0},35 L{1},40 {2},40", lc, lc, lc + 100 / ScaleProperty);
                                lc += 100 / ScaleProperty;
                            }

                            sdvm.DataCollection.Add(new StationDataMode()
                            {
                                HatProperty = pdxlist[0].Gh,
                                LengthProperty = (1000 - firstl) / ScaleProperty,
                                PositionProperty = float.Parse(pdxlist[0].Qdglb),
                                Type = DataType.Position,
                                SectionNumProperty = int.Parse(pdxlist[0].Ldh),
                                ScaleProperty = ScaleProperty,
                                SelectedProperty = false,
                                PathDataProperty = string.Format("2:1 0:#00DC5625:#FF000000:{0}", pathes),
                                StationNameProperty = string.Empty
                            });

                            continue;
                        }

                        for (int k = 0; k < 10; k++)
                        {
                            if (k == 0)
                            {
                                pathes += string.Format("M{0},25 L{1},40 {2},40", lc, lc, lc + 100 / ScaleProperty);
                                lc += 100 / ScaleProperty;
                                continue;
                            }

                            pathes += string.Format("M{0},35 L{1},40 {2},40", lc, lc, lc + 100 / ScaleProperty);
                            lc += 100 / ScaleProperty;
                        }

                        sdvm.DataCollection.Add(new StationDataMode()
                        {
                            HatProperty = pdxlist[0].Gh,
                            LengthProperty = 1000 / ScaleProperty,
                            PositionProperty = i,
                            Type = DataType.Position,
                            SectionNumProperty = int.Parse(pdxlist[0].Ldh),
                            ScaleProperty = ScaleProperty,
                            SelectedProperty = false,
                            PathDataProperty = string.Format("2:1 0:#00DC5625:#FF000000:{0}", pathes),
                            StationNameProperty = string.Empty
                        });
                    }
                }

                catch (NullReferenceException ure)
                {
                    MessengerInstance.Send<string>(ure.Message, "ReadDataError");
                }
            }

            int p = 0;                       

            foreach (string item in cdlxlist)
            {
                pathes = string.Empty;
                lc = 0;
                string[] parts = item.Split(':');
                float et = 0;
                double near = Math.Floor(float.Parse(parts[0].Split('+')[1]) / 1000);
                double far = Math.Floor(float.Parse(parts[1].Split('+')[1]) / 1000);
                float offset = (float)(far - near);                
                string farhat = parts[1].Split('+')[0];

                if (offset != 0)
                {
                    for (int i = p; i < sdvm.DataCollection.Count; i++)
                    {
                        if (sdvm.DataCollection[i].PositionProperty == near)
                        {
                            sdvm.DataCollection[i].LengthProperty = (float.Parse(parts[0].Split('+')[1]) -
                                sdvm.DataCollection[i].PositionProperty * 1000) / ScaleProperty;
                            int nearcount = (int)Math.Ceiling((float.Parse(parts[0].Split('+')[1]) / 1000
                                - sdvm.DataCollection[i].PositionProperty) * 10);

                            for (int k = 0; k < nearcount; k++)
                            {
                                /*if (float.Parse(parts[0].Split('+')[1]) / near == 1)
                                {
                                    pathes += string.Format("M{0},25 L{1},40 {2},40", lc, lc, lc + 100 / ScaleProperty);
                                    lc += 100 / ScaleProperty;
                                    continue;
                                }*/

                                if (k == nearcount - 1)
                                {
                                    et = (float)((float.Parse(parts[0].Split('+')[1]) - near * 1000 - (nearcount - 1) * 100)
                                     / ScaleProperty);
                                    pathes += string.Format("M{0},35 L{1},40 {2},40", lc, lc, lc + et
                                     );
                                    lc = lc + et;
                                    continue;
                                }

                                if (k == 0)
                                {
                                    pathes += string.Format("M{0},25 L{1},40 {2},40", lc, lc, lc + 100 / ScaleProperty);
                                    lc += 100 / ScaleProperty;
                                    continue;
                                }

                                pathes += string.Format("M{0},35 L{1},40 {2},40", lc, lc, lc + 100 / ScaleProperty);
                                lc += 100 / ScaleProperty;
                            }

                            sdvm.DataCollection[i].PathDataProperty = string.Format("2:1 0:#00DC5625:#FF000000:{0}", pathes);

                            pathes = string.Empty;
                            lc = 0;

                            for (int j = i + 1; j < sdvm.DataCollection.Count; j++)
                            {
                                sdvm.DataCollection[j].HatProperty = farhat;
                                sdvm.DataCollection[j].PositionProperty += offset;
                                sdvm.DataCollection[j].SectionNumProperty += 1;
                            }

                            nearcount = (int)Math.Floor((float.Parse(parts[1].Split('+')[1]) - far * 1000) / 100);
                            for (int l = nearcount; l < 10; l++)
                            {
                                if (l == nearcount)
                                {
                                    et = (float)(((nearcount + 1) * 100 - (float.Parse(parts[1].Split('+')[1]) - far * 1000 - nearcount * 100))
                                     / ScaleProperty);
                                    pathes += string.Format("M{0},15 L{1},40 {2},40", lc, lc, lc + et);
                                    lc = lc + et;
                                    continue;
                                }

                                pathes += string.Format("M{0},35 L{1},40 {2},40", lc, lc, lc + 100 / ScaleProperty);
                                lc += 100 / ScaleProperty;
                            }

                            StationDataMode newsdm = new StationDataMode()
                            {
                                HatProperty = farhat,
                                LengthProperty = (float)((far + 1) * 1000 - float.Parse(parts[1].Split('+')[1])) / ScaleProperty,
                                PositionProperty = (float)(float.Parse(parts[1].Split('+')[1]) / 1000),
                                Type = DataType.Position,
                                SectionNumProperty = sdvm.DataCollection[i].SectionNumProperty + 1,
                                ScaleProperty = ScaleProperty,
                                SelectedProperty = false,
                                PathDataProperty = string.Format("2:1 0:#00DC5625:#FF000000:{0}", pathes),
                                StationNameProperty = string.Empty
                            };

                            sdvm.DataCollection.Insert(i + 1, newsdm);
                            p = i;
                            break;
                        }                        
                    }
                }
            }
            
            if (_datascollection.Count == 0)
            {
                _datascollection.Add(sdvm);
            }
            else
            {
                for (int i = 0; i < _datascollection.Count; i++)
                {
                    if (sdvm.TypeNum < _datascollection[i].TypeNum)
                    {
                        _datascollection.Insert(i, sdvm);
                        break;
                    }
                    else if (i == _datascollection.Count - 1 && sdvm.TypeNum != _datascollection[i].TypeNum)
                    {
                        _datascollection.Add(sdvm);
                        break;
                    }
                }
            }

            CountProperty = DatasCollection.Count;
            IsLiChengLoadedProperty = true;
        }

        private void removeAdataItem(int typenum)
        {           
            foreach (ISingleDataViewModel item in _datascollection)
            {
                if (item.TypeNum == typenum)
                {
                    DatasCollection.Remove(item);
                    if (!_DataBin.Contains(item))
                        _DataBin.Add(item);
                    break;
                }
            }
            CountProperty = DatasCollection.Count;
        }

        private float ChangeToOdd(float data, bool odd)
        {
            data = (int)Math.Round(data * 10, 0);
            if (odd)
            {
                if (data % 2 != 0)
                    return data;
                return data + 1;
            }
            else
            {
                if (data % 2 == 0)
                    return data;
                return data + 1;
            }
        }

        private void AddXinhaoX(string taken, int numbers)
        {
            ISingleDataViewModel singledata;
            try
            {
                if (!SeletedXinhaoS)
                    singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.Single);
                else
                    singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.SingleS);
                StationDataMode sdm = (StationDataMode)singledata.DataCollection.
                    Single(q => ((StationDataMode)q).StationNameProperty.Equals(taken));

                int index = singledata.DataCollection.IndexOf(sdm);
                float offset = 0;
                string[] parts = { };

                float len = sdm.LengthProperty * ScaleProperty + 200;
                int odd = (int)(Math.Round(len) % (numbers + 1));
                float divide = (float)(Math.Round(len) - odd) / (numbers + 1);
                float pos = 0;
                int sec = sdm.SectionNumProperty;
                StationDataMode newsdmxh = null;
                StationDataMode newsdmqj = null;
                offset = 0;

                for (int i = 0; i < numbers; i++)
                {

                    if (i == 0)
                    {                       
                        newsdmqj = new StationDataMode()
                        {
                            HatProperty = sdm.HatProperty,
                            LengthProperty = (divide + odd - 200) / ScaleProperty,
                            PathDataProperty = "5:6 2 1 2:#00DC5625:#FF000000:M0,0 L500,0",
                            PositionProperty = sdm.PositionProperty,
                            ScaleProperty = ScaleProperty,
                            SectionNumProperty = sdm.SectionNumProperty,
                            SelectedProperty = false,
                            StationNameProperty = sdm.StationNameProperty
                        };

                        singledata.DataCollection.RemoveAt(index);
                        singledata.DataCollection.Insert(index, newsdmqj);

                        pos = sdm.PositionProperty;
                        parts = cdlxlist[sec - 1].Split(':');
                        if ((pos + divide / 1000) > float.Parse(parts[0].Split('+')[1]) / 1000)
                        {
                            offset = float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]);
                            sec += 1;
                        }                       
                    }

                    pos = pos + divide / 1000 + offset / 1000;

                    newsdmxh = new StationDataMode()
                    {
                        HatProperty = parts[0].Split('+')[0],
                        LengthProperty = 200 / ScaleProperty,
                        PathDataProperty = 
                        "1:1 0:#FFDC5625:#FF35F30E:M-27,30 L5,30 M57,31 C57,47 45,59 30,59 C15,59 2.5,47 3,31 C3,15 15,3 30,3 C45,3 57,15 57,31 z",
                        PositionProperty = pos,
                        ScaleProperty = ScaleProperty,
                        SectionNumProperty = sec,
                        SelectedProperty = false,
                        StationNameProperty = Direction == 0 ? 
                        string.Format("{0}:{1}", "2", ChangeToOdd(pos, false).ToString()) :
                        string.Format("{0}:{1}", "2", ChangeToOdd(pos, true).ToString()),
                        Type = DataType.Single
                    };

                    newsdmqj = new StationDataMode()
                    {
                        HatProperty = parts[0].Split('+')[0],
                        LengthProperty = (divide - 200) / ScaleProperty,
                        PathDataProperty = "5:6 2 1 2:#00DC5625:#FF000000:M0,0 L500,0",
                        PositionProperty = pos,
                        ScaleProperty = ScaleProperty,
                        SectionNumProperty = sec,
                        SelectedProperty = false,
                        StationNameProperty =
                        "Q" + newsdmxh.SectionNumProperty.ToString() + "+" + newsdmxh.StationNameProperty,
                        Type = DataType.Single
                    };

                    index += 1;
                    singledata.DataCollection.Insert(index, newsdmxh);
                    index += 1;
                    singledata.DataCollection.Insert(index, newsdmqj);

                    parts = cdlxlist[sec - 1].Split(':');
                    if ((pos + divide / 1000) > float.Parse(parts[0].Split('+')[1]) / 1000)
                    {
                        offset = float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]);
                        sec += 1;
                        continue;
                    }
                    offset = 0;
                }
            }
            catch { }
        }

        private void AddXinhaoS(string taken, int numbers)
        {
            AddXinhaoX(taken, numbers);
        }

        private void moveXinhaoX(string taken, float offset, string newSinge = "")
        {
            ISingleDataViewModel singledata;
            //sec:name
            string[] takens = taken.Split(':');
           
            if (!SeletedXinhaoS)
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.Single);
            else
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.SingleS);
            StationDataMode sdm = (StationDataMode)singledata.DataCollection.
                Single(q => ((StationDataMode)q).StationNameProperty.Equals(taken));

            int index = singledata.DataCollection.IndexOf(sdm);
            string[] parts = { };
            float pos = sdm.PositionProperty;
            int sectionum = sdm.SectionNumProperty;
            float adding = 0;

            StationDataMode preqj = (StationDataMode)singledata.DataCollection[index - 1];
            StationDataMode prexh = index != 1 ? (StationDataMode)singledata.DataCollection[index - 2] : preqj;
            StationDataMode currentqj = (StationDataMode)singledata.DataCollection[index + 1];

            if (offset >= 0)
            {

                if (offset >= (currentqj.LengthProperty * ScaleProperty + 200))
                {
                    throw new InvalidDataException("偏移过长");
                }

                int max = (index == singledata.DataCollection.Count - 2) ? index + 1 : index + 2;
                for (int i = sdm.SectionNumProperty; i < singledata.DataCollection[max].SectionNumProperty; i++)
                {
                    parts = cdlxlist[i - 1].Split(':');

                    if ((sdm.PositionProperty + offset / 1000 + adding) > float.Parse(parts[0].Split('+')[1]) / 1000)
                    {
                        adding += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                        sectionum += 1;
                    }
                }

                //改变stationname
                sdm.PositionProperty = pos + (offset + adding) / 1000;
                sdm.SectionNumProperty = sectionum;
                parts = sdm.StationNameProperty.Split(':');
                if (parts[0].Equals("1"))
                {
                    sdm.StationNameProperty = string.Format("{0}:{1}:{2}:{3}:{4}", parts[0], newSinge, parts[2], parts[3], parts[4]);
                }
                else if (parts[0].Equals("2"))
                {
                    sdm.StationNameProperty = int.Parse(sdm.StationNameProperty.Split(':')[1]) % 2 == 0 ?
                        string.Format("{0}:{1}", sdm.StationNameProperty.Split(':')[0], ChangeToOdd(sdm.PositionProperty, false)) :
                        string.Format("{0}:{1}", sdm.StationNameProperty.Split(':')[0], ChangeToOdd(sdm.PositionProperty, true));
                }
                singledata.DataCollection.RemoveAt(index);
                singledata.DataCollection.Insert(index, sdm);
                currentqj.PositionProperty = sdm.PositionProperty;
                currentqj.LengthProperty = currentqj.LengthProperty - offset / ScaleProperty;
                currentqj.StationNameProperty = "Q" + sectionum.ToString() + "+" + sdm.StationNameProperty;
                currentqj.SectionNumProperty = sectionum;
                singledata.DataCollection.RemoveAt(index + 1);
                singledata.DataCollection.Insert(index + 1, currentqj);
                preqj.LengthProperty = preqj.LengthProperty + offset / ScaleProperty;
                singledata.DataCollection.RemoveAt(index - 1);
                singledata.DataCollection.Insert(index - 1, preqj);

            }
            else
            {
                if (-offset >= (preqj.LengthProperty * ScaleProperty + 200))
                {
                    throw new InvalidDataException("偏移过长");
                }

                for (int i = sdm.SectionNumProperty; i > prexh.SectionNumProperty; i--)
                {
                    parts = cdlxlist[i - 2].Split(':');

                    if ((sdm.PositionProperty + offset / 1000) < float.Parse(parts[1].Split('+')[1]) / 1000)
                    {
                        adding += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                        sectionum -= 1;
                    }
                }

                sdm.PositionProperty = pos + (offset - adding) / 1000;
                sdm.SectionNumProperty = sectionum;
                 parts = sdm.StationNameProperty.Split(':');
                if (parts[0].Equals("1"))
                {
                    sdm.StationNameProperty = string.Format("{0}:{1}:{2}:{3}:{4}", parts[0], newSinge, parts[2], parts[3], parts[4]);
                }
                else if (parts[0].Equals("2"))
                {
                    sdm.StationNameProperty = int.Parse(sdm.StationNameProperty.Split(':')[1]) % 2 == 0 ?
                        string.Format("{0}:{1}", sdm.StationNameProperty.Split(':')[0], ChangeToOdd(sdm.PositionProperty, false)) :
                        string.Format("{0}:{1}", sdm.StationNameProperty.Split(':')[0], ChangeToOdd(sdm.PositionProperty, true));
                }
                singledata.DataCollection.RemoveAt(index);
                singledata.DataCollection.Insert(index, sdm);
                currentqj.PositionProperty = pos;
                currentqj.LengthProperty = currentqj.LengthProperty - offset / ScaleProperty;
                currentqj.StationNameProperty = "Q" + sectionum.ToString() + "+" + sdm.StationNameProperty;
                sdm.SectionNumProperty = sectionum;
                singledata.DataCollection.RemoveAt(index + 1);
                singledata.DataCollection.Insert(index + 1, currentqj);
                preqj.LengthProperty = preqj.LengthProperty + offset / ScaleProperty;
                singledata.DataCollection.RemoveAt(index - 1);
                singledata.DataCollection.Insert(index - 1, preqj);
            }
        }

        private void MoveXinhaoS(string taken, float offset, string newSinge = "")
        {
            moveXinhaoX(taken, offset, newSinge);
        }
       
        private void InsertXinhaoX(float position, int secnum, string singletype, StationDataMode stationsignalinfo)
        {
            ISingleDataViewModel singledata;
            if (!SeletedXinhaoS)
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.Single);
            else
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.SingleS);
            float offset = 0;
            float pos = position;
            float len = 0;
            string[] parts = { };
            StationDataMode newqj = null;
            StationDataMode newxh = null;
           
            if (singledata.DataCollection.Count == 0)
            {               
                
                for (int i = int.Parse(pdxlist[0].Ldh); i < secnum; i++)
                {
                    parts = cdlxlist[i - 1].Split(':');
                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                }               

                newqj = new StationDataMode()
                {
                    HatProperty = pdxlist[0].Gh,
                    LengthProperty = (pos - float.Parse(pdxlist[0].Qdglb)) * 1000 - offset,
                    PathDataProperty = "5:6 2 1 2:#00DC5625:#FF000000:M0,0 L50,0",
                    PositionProperty = float.Parse(pdxlist[0].Qdglb),
                    ScaleProperty = ScaleProperty,
                    SectionNumProperty = int.Parse(pdxlist[0].Ldh),
                    SelectedProperty = false,
                    StationNameProperty = "Q" + pdxlist[0].Ldh + "+" + "0",
                    Type = DataType.Single
                };

                singledata.DataCollection.Add(newqj);

                if (singletype.Equals("2"))
                {
                    newxh = new StationDataMode()
                    {
                        HatProperty = cdlxlist[secnum - 1].Split(':')[0].Split('+')[0],
                        LengthProperty = 200 / ScaleProperty,
                        PathDataProperty =
                        "1:1 0:#FFDC5625:#FF35F30E:M-27,30 L5,30 M57,31 C57,47 45,59 30,59 C15,59 2.5,47 3,31 C3,15 15,3 30,3 C45,3 57,15 57,31 z",
                        PositionProperty = pos,
                        ScaleProperty = ScaleProperty,
                        SectionNumProperty = secnum,
                        SelectedProperty = false,
                        StationNameProperty = Direction == 0 ?
                        string.Format("{0}:{1}", singletype, ChangeToOdd(pos, false).ToString()) :
                        string.Format("{0}:{1}", singletype, ChangeToOdd(pos, true).ToString()),
                        Type = DataType.Single
                    };
                }
                else if (singletype.Equals("1"))
                {
                    newxh = stationsignalinfo;
                }

                singledata.DataCollection.Add(newxh);

                for (int i = secnum; i < int.Parse(pdxlist[pdxlist.Count - 1].Ldh); i++)
                {
                    parts = cdlxlist[i - 1].Split(':');
                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                }

                newqj = new StationDataMode()
                {
                    HatProperty = cdlxlist[secnum - 1].Split(':')[0].Split('+')[0],
                    LengthProperty = (float.Parse(pdxlist[pdxlist.Count - 1].Qdglb) - pos) * 1000
                    + float.Parse(pdxlist[pdxlist.Count - 1].Pc) - offset,
                    PathDataProperty = "5:6 2 1 2:#00DC5625:#FF000000:M0,0 L50,0",
                    PositionProperty = newxh.PositionProperty,
                    ScaleProperty = ScaleProperty,
                    SectionNumProperty = secnum,
                    SelectedProperty = false,
                    StationNameProperty = "Q" + secnum + "+" + newxh.StationNameProperty,
                    Type = DataType.Single
                };

                singledata.DataCollection.Add(newqj);
            }
            else
            {
                offset = 0;
                parts = new string[] { };

                for (int i = 0; i < singledata.DataCollection.Count; i += 2)
                {
                    StationDataMode currentsdm = singledata.DataCollection[i] as StationDataMode;
                    StationDataMode nextsdm = null;

                    if (i == singledata.DataCollection.Count - 1)
                        nextsdm = currentsdm;
                    else
                        nextsdm = singledata.DataCollection[i + 1] as StationDataMode;

                    if (currentsdm.SectionNumProperty != nextsdm.SectionNumProperty)
                    {
                        for (int j = currentsdm.SectionNumProperty; j < nextsdm.SectionNumProperty; j++)
                        {
                            parts = cdlxlist[j - 1].Split(':');
                            offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                        }
                    }
                    else
                    {
                        parts = cdlxlist[currentsdm.SectionNumProperty - 1].Split(':');
                        offset = 0;
                    }

                    if ((currentsdm.PositionProperty +
                        (currentsdm.LengthProperty * ScaleProperty + 200) / 1000 + offset / 1000) > pos)
                    {
                        offset = 0;
                        if (pos * 1000 > float.Parse(parts[0].Split('+')[1]))
                        {
                            for (int j = currentsdm.SectionNumProperty; j < secnum; j++)
                            {
                                parts = cdlxlist[j - 1].Split(':');
                                offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                            }

                            if (i != 0)
                            {
                                len = (currentsdm.LengthProperty * ScaleProperty) / ScaleProperty -
                                 ((pos - currentsdm.PositionProperty) * 1000 - offset - 200) / ScaleProperty;
                                currentsdm.LengthProperty = ((pos - currentsdm.PositionProperty) * 1000 - offset - 200) / ScaleProperty;                                
                            }
                            else
                            {
                                len = (currentsdm.LengthProperty * ScaleProperty) / ScaleProperty -
                              ((pos - currentsdm.PositionProperty) * 1000 - offset) / ScaleProperty;
                                currentsdm.LengthProperty = ((pos - currentsdm.PositionProperty) * 1000 - offset) / ScaleProperty;
                               
                            }
                        }
                        else
                        {
                            if (i != 0)
                            {
                                len = (currentsdm.LengthProperty * ScaleProperty) / ScaleProperty -
                               ((pos - currentsdm.PositionProperty) * 1000 - 200) / ScaleProperty;
                                currentsdm.LengthProperty = ((pos - currentsdm.PositionProperty) * 1000 - 200) / ScaleProperty;
                               
                            }
                            else
                            {
                                len = (currentsdm.LengthProperty * ScaleProperty) / ScaleProperty -
                               ((pos - currentsdm.PositionProperty) * 1000) / ScaleProperty;
                                currentsdm.LengthProperty = ((pos - currentsdm.PositionProperty) * 1000) / ScaleProperty;
                               
                            }
                        }

                        if (singletype.Equals("2"))
                        {
                            newxh = new StationDataMode()
                            {
                                HatProperty = cdlxlist[secnum - 1].Split(':')[0].Split('+')[0],
                                LengthProperty = 200 / ScaleProperty,
                                PathDataProperty = "1:1 0:#FFDC5625:#FF35F30E:M-27,30 L5,30 M57,31 C57,47 45,59 30,59 C15,59 2.5,47 3,31 C3,15 15,3 30,3 C45,3 57,15 57,31 z",
                                PositionProperty = pos,
                                ScaleProperty = ScaleProperty,
                                SectionNumProperty = secnum,
                                SelectedProperty = false,
                                StationNameProperty = Direction == 0 ?
                                string.Format("{0}:{1}", singletype, ChangeToOdd(pos, false).ToString()) :
                                string.Format("{0}:{1}", singletype, ChangeToOdd(pos, true).ToString()),
                                Type = DataType.Single
                            };
                        }
                        else if (singletype.Equals("1"))
                        {
                            newxh = stationsignalinfo;
                        }

                        newqj = new StationDataMode()
                        {
                            HatProperty = newxh.HatProperty,
                            LengthProperty = len - 200 / ScaleProperty,
                            PathDataProperty = "5:6 2 1 2:#00DC5625:#FF000000:M0,0 L50,0",
                            PositionProperty = newxh.PositionProperty,
                            ScaleProperty = ScaleProperty,
                            SectionNumProperty = newxh.SectionNumProperty,
                            SelectedProperty = false,
                            StationNameProperty = "Q" + secnum + "+" + newxh.StationNameProperty,
                            Type = DataType.Single
                        };

                        singledata.DataCollection.RemoveAt(i);

                        singledata.DataCollection.Insert(i, currentsdm);

                       
                        
                        singledata.DataCollection.Insert(i + 1, newxh);

                        singledata.DataCollection.Insert(i + 2, newqj);

                        break;
                    }
                }
            }
        }

        private void InsertXinhaoS(float position, int secnum, string singletype,StationDataMode stationsignalinfo)
        {
            InsertXinhaoX(position, secnum, singletype, stationsignalinfo);
        }

        private void DeleteXinhao(string taken)
        {
            ISingleDataViewModel singledata;
            if (!SeletedXinhaoS)
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.Single);
            else
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.SingleS);
            StationDataMode sdm = (StationDataMode)singledata.DataCollection.
                Single(q => ((StationDataMode)q).StationNameProperty.Equals(taken));

            int index = singledata.DataCollection.IndexOf(sdm);

            StationDataMode preqj = singledata.DataCollection[index - 1] as StationDataMode;
            StationDataMode currentqj = singledata.DataCollection[index + 1] as StationDataMode;

            preqj.LengthProperty = preqj.LengthProperty + currentqj.LengthProperty + 200 / ScaleProperty;

            singledata.DataCollection.RemoveAt(index - 1);
            singledata.DataCollection.Insert(index - 1, preqj);
            singledata.DataCollection.RemoveAt(index);
            singledata.DataCollection.RemoveAt(index);
        }

        private void ModifyStationSignal()
        {
            ISingleDataViewModel singledata;
            if (!SeletedXinhaoS)
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.Single);
            else
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.SingleS);
            var stationsignals = singledata.DataCollection.
                Where(p => (!((StationDataMode)p).StationNameProperty.StartsWith("Q")
                    && ((StationDataMode)p).StationNameProperty.Split(':')[0].Equals("1"))).
                Select(q => (StationDataMode)q);
            List<StationDataMode> stationSignallist = new List<StationDataMode>();
            string mark = string.Empty;

            if (czxlist == null || czxlist.Count == 0)
            {
                throw new SatitonNotLoadedException("未载入车站数据，请先载入车站数据！");
            }
            else
            {
                foreach (ChangeToTxt.CheZhanOutputData item in czxlist)
                {
                    mark = string.Empty;
                    foreach (StationDataMode s in stationsignals)
                    {
                        if (s.StationNameProperty.Split(':')[2].Equals(item.Bjsj))
                        {
                            if (s.StationNameProperty.Split(':')[3].Equals("J"))
                            {
                                stationSignallist.Add(new StationDataMode()
                                {
                                    HatProperty = s.HatProperty,
                                    StationNameProperty = s.StationNameProperty,
                                    LengthProperty = s.LengthProperty,
                                    PathDataProperty = s.PathDataProperty,
                                    PositionProperty = s.PositionProperty,
                                    ScaleProperty = s.ScaleProperty,
                                    SectionNumProperty = s.SectionNumProperty,
                                    SelectedProperty = s.SelectedProperty,
                                    Type = s.Type
                                });
                                mark += "J";
                            }
                            else if (s.StationNameProperty.Split(':')[3].Equals("C"))
                            {
                                stationSignallist.Add(new StationDataMode()
                                {
                                    HatProperty = s.HatProperty,
                                    StationNameProperty = s.StationNameProperty,
                                    LengthProperty = s.LengthProperty,
                                    PathDataProperty = s.PathDataProperty,
                                    PositionProperty = s.PositionProperty,
                                    ScaleProperty = s.ScaleProperty,
                                    SectionNumProperty = s.SectionNumProperty,
                                    SelectedProperty = s.SelectedProperty,
                                    Type = s.Type
                                });
                                mark += "C";
                            }
                        }
                    }

                    switch (mark)
                    {
                        case "":
                            //进站
                            stationSignallist.Add(new StationDataMode()
                            {
                                HatProperty = item.Gh,
                                StationNameProperty = string.Format("{0}:{1}:{2}:{3}:{4}",
                                "1", string.Empty, item.Bjsj, "J", FormatStationPosition(item.Bjsj)),
                                LengthProperty = 200 / ScaleProperty,
                                PathDataProperty =
                                "1:1 0:#FFDC5625:#FF35F30E:M-27,30 L5,30 M57,31 C57,47 45,59 30,59 C15,59 2.5,47 3,31 C3,15 15,3 30,3 C45,3 57,15 57,31 z",
                                PositionProperty = float.Parse(item.Glb),
                                ScaleProperty = ScaleProperty,
                                SectionNumProperty = int.Parse(item.Ldh),
                                SelectedProperty = false,
                                Type = DataType.Single
                            });
                            //出站
                            stationSignallist.Add(new StationDataMode()
                            {
                                HatProperty = item.Gh,
                                StationNameProperty = string.Format("{0}:{1}:{2}:{3}:{4}",
                                "1", string.Empty, item.Bjsj, "C", FormatStationPosition(item.Bjsj)),
                                LengthProperty = 200 / ScaleProperty,
                                PathDataProperty =
                                "1:1 0:#FFDC5625:#FF35F30E:M-27,30 L5,30 M57,31 C57,47 45,59 30,59 C15,59 2.5,47 3,31 C3,15 15,3 30,3 C45,3 57,15 57,31 z",
                                PositionProperty = float.Parse(item.Glb),
                                ScaleProperty = ScaleProperty,
                                SectionNumProperty = int.Parse(item.Ldh),
                                SelectedProperty = false,
                                Type = DataType.Single
                            });
                            break;
                        case "J":
                            //出站
                            stationSignallist.Add(new StationDataMode()
                            {
                                HatProperty = item.Gh,
                                StationNameProperty = string.Format("{0}:{1}:{2}:{3}:{4}",
                                "1", string.Empty, item.Bjsj, "C", FormatStationPosition(item.Bjsj)),
                                LengthProperty = 200 / ScaleProperty,
                                PathDataProperty =
                                "1:1 0:#FFDC5625:#FF35F30E:M-27,30 L5,30 M57,31 C57,47 45,59 30,59 C15,59 2.5,47 3,31 C3,15 15,3 30,3 C45,3 57,15 57,31 z",
                                PositionProperty = float.Parse(item.Glb),
                                ScaleProperty = ScaleProperty,
                                SectionNumProperty = int.Parse(item.Ldh),
                                SelectedProperty = false,
                                Type = DataType.Single
                            });
                            break;
                        case "C":
                            //进站
                            stationSignallist.Add(new StationDataMode()
                            {
                                HatProperty = item.Gh,
                                StationNameProperty = string.Format("{0}:{1}:{2}:{3}:{4}",
                                "1", string.Empty, item.Bjsj, "J", FormatStationPosition(item.Bjsj)),
                                LengthProperty = 200 / ScaleProperty,
                                PathDataProperty =
                                "1:1 0:#FFDC5625:#FF35F30E:M-27,30 L5,30 M57,31 C57,47 45,59 30,59 C15,59 2.5,47 3,31 C3,15 15,3 30,3 C45,3 57,15 57,31 z",
                                PositionProperty = float.Parse(item.Glb),
                                ScaleProperty = ScaleProperty,
                                SectionNumProperty = int.Parse(item.Ldh),
                                SelectedProperty = false,
                                Type = DataType.Single
                            });
                            break;
                        default:
                            break;
                    }
                }

                Swindow = new StationWindow();
                MessengerInstance.Send<System.Collections.Generic.List<string>>(cdlxlist, "cdl");
                MessengerInstance.Send<List<StationDataMode>>(stationSignallist, "stationsignal");
                Swindow.ShowDialog();
            }

        }

        private void UpdataStationSignals(List<StationDataMode> stationsignalcollection)
        {
            ISingleDataViewModel singledata;
            if (!SeletedXinhaoS)
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.Single);
            else
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.SingleS);
            string[] parts;
            StationDataMode matchedmode;

            foreach (StationDataMode item in stationsignalcollection)
            {
                if (item.PositionProperty == -1)
                    continue;
                parts = item.StationNameProperty.Split(':');
                try
                {
                    matchedmode = singledata.DataCollection.Single
                        (p => (!((StationDataMode)p).StationNameProperty.StartsWith("Q") &&
                            ((StationDataMode)p).StationNameProperty.Split(':').Length > 2 &&
                            ((StationDataMode)p).StationNameProperty.Split(':')[2].Equals(parts[2]) &&
                            ((StationDataMode)p).StationNameProperty.Split(':')[3].Equals(parts[3]))) as StationDataMode;
                    float offset = (item.PositionProperty - matchedmode.PositionProperty) * 1000;

                    if (item.SectionNumProperty != matchedmode.SectionNumProperty)
                    {
                        for (int i = matchedmode.SectionNumProperty; i < item.SectionNumProperty; i++)
                        {
                            parts = cdlxlist[i - 1].Split(':');
                            offset -= (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                        }
                    }

                    if (offset > 1)
                        moveXinhaoX(matchedmode.StationNameProperty, offset, parts[1]);
                }
                catch(System.InvalidOperationException ioe)
                {
                    InsertXinhaoX(item.PositionProperty, item.SectionNumProperty, "1", item);
                }
            }
        }
        #region Commands
       
        private RelayCommand _showdataChangedCommand;

        /// <summary>
        /// Gets the ShowDataChangedCommand.
        /// </summary>
        public RelayCommand ShowDataChangedCommand
        {
            get
            {
                return _showdataChangedCommand
                    ?? (_showdataChangedCommand = new RelayCommand(
                                          () =>
                                          {
                                              ISingleDataViewModel matchone = null;
                                              foreach (ISingleDataViewModel item in _datascollection)
                                              {
                                                  if (item.ShowDataProperty == false)
                                                  {
                                                      matchone = item;
                                                      switch (item.TypeNum)
                                                      {
                                                          case (int)DataType.Podu:
                                                              IsPoduLoadedProperty = false;
                                                              break;
                                                          case (int)DataType.Quxian:
                                                              IsQuXianLoadedProperty = false;
                                                              break;
                                                          case (int)DataType.Station:
                                                              IsCheZhanLoadedProperty = false;
                                                              break;
                                                          case (int)DataType.Position:
                                                              IsLiChengLoadedProperty = false;
                                                              break;
                                                          case (int)DataType.Single:
                                                              IsXinhaoLoadedProperty = false;
                                                              break;
                                                          case (int)DataType.SingleS:
                                                              IsXinhaoSLoaded = false;
                                                              break;
                                                          default:
                                                              break;
                                                      }
                                                  }
                                              }

                                              DatasCollection.Remove(matchone);

                                              if (!_DataBin.Contains(matchone))
                                                  _DataBin.Add(matchone);
                                          }));
            }
        }

        private RelayCommand _loadPoduDataCommand;

        /// <summary>
        /// Gets the LoadPoduDataCommand.
        /// </summary>
        public RelayCommand LoadPoduDataCommand
        {
            get
            {
                return _loadPoduDataCommand
                    ?? (_loadPoduDataCommand = new RelayCommand(
                                          () =>
                                          {
                                              LoadPoduXData();
                                          }));
            }
        }

        private RelayCommand _loadquxiandataCommand;

        /// <summary>
        /// Gets the LoadQuXianDataCommand.
        /// </summary>
        public RelayCommand LoadQuXianDataCommand
        {
            get
            {
                return _loadquxiandataCommand
                    ?? (_loadquxiandataCommand = new RelayCommand(
                                          () =>
                                          {
                                              LoadQuXianXData();
                                          }));
            }
        }

        private RelayCommand _loadstationCommand;

        /// <summary>
        /// Gets the LoadStationCommand.
        /// </summary>
        public RelayCommand LoadStationCommand
        {
            get
            {
                return _loadstationCommand
                    ?? (_loadstationCommand = new RelayCommand(
                                          () =>
                                          {
                                              LoadCheZhanXData();
                                          }));
            }
        }

        private RelayCommand _loadpositionCommand;

        /// <summary>
        /// Gets the LoadPositionCommand.
        /// </summary>
        public RelayCommand LoadPositionCommand
        {
            get
            {
                return _loadpositionCommand
                    ?? (_loadpositionCommand = new RelayCommand(
                                          () =>
                                          {
                                              LoadLiChengXData();
                                          }));
            }
        }

        private RelayCommand _loadxhCommand;

        /// <summary>
        /// Gets the LoadXinhaoCommand.
        /// </summary>
        public RelayCommand LoadXinhaoCommand
        {
            get
            {
                return _loadxhCommand
                    ?? (_loadxhCommand = new RelayCommand(
                                          () =>
                                          {
                                              LoadXinHaoData();
                                          }));
            }
        }
        private RelayCommand _loadxinhaosCommand;

        /// <summary>
        /// Gets the LoadXinHaoSCommand.
        /// </summary>
        public RelayCommand LoadXinHaoSCommand
        {
            get
            {
                return _loadxinhaosCommand
                    ?? (_loadxinhaosCommand = new RelayCommand(
                                          () =>
                                          {
                                              LoadXinHaoSData();
                                          }));
            }
        }
        private RelayCommand _InsertXinhaoCommand;

        /// <summary>
        /// Gets the InsertXinhaoCommand.
        /// </summary>
        public RelayCommand InsertXinhaoCommand
        {
            get
            {
                return _InsertXinhaoCommand
                    ?? (_InsertXinhaoCommand = new RelayCommand(
                                          () =>
                                          {
                                              string[] parts = { };
                                              string divinfos = string.Empty;
                                              float offset2 = 0;
                                              StationDataMode sdm = CurrentDatasProperty.CurrentDataProperty as StationDataMode;
                                              StationDataMode nextsdm = null;
                                              float newposition = 0;

                                              int index = CurrentDatasProperty.DataCollection.IndexOf(sdm);
                                              if (index == CurrentDatasProperty.DataCollection.Count - 1)
                                              {
                                                  nextsdm = sdm;
                                                  newposition = nextsdm.PositionProperty + (nextsdm.LengthProperty + 20) * sdm.ScaleProperty / 1000;
                                              }
                                              else
                                              {
                                                  nextsdm = CurrentDatasProperty.DataCollection[index + 1] as StationDataMode;
                                                  newposition = nextsdm.PositionProperty;
                                              }

                                              if (sdm.SectionNumProperty == nextsdm.SectionNumProperty)
                                              {
                                                  divinfos = divinfos + "," + string.Format("{0}:{1}", 
                                                      cdlxlist[sdm.SectionNumProperty - 1].Split(':')[0], 
                                                      cdlxlist[sdm.SectionNumProperty - 1].Split(':')[0]);
                                              }
                                              else
                                              {
                                                  for (int i = sdm.SectionNumProperty; i < nextsdm.SectionNumProperty; i++)
                                                  {
                                                      divinfos = divinfos + "," + cdlxlist[i - 1];
                                                      parts = cdlxlist[i - 1].Split(':');
                                                      offset2 += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                                  }
                                              }

                                              mqjwindow = new ModifyQujianWindow();
                                              GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string>
                                                  (string.Format("{0};{1};{2};{3};{4};{5}",
                                                  sdm.PositionProperty, newposition,
                                                  sdm.PositionProperty, divinfos, offset2.ToString(),
                                                  sdm.SectionNumProperty.ToString()),
                                                  "Resources");
                                              mqjwindow.ShowDialog();                                             
                                          }));
            }
        }
        private RelayCommand _AddXinhaoCommand;

        /// <summary>
        /// Gets the AddXinhaoCommand.
        /// </summary>
        public RelayCommand AddXinhaoCommand
        {
            get
            {
                return _AddXinhaoCommand
                    ?? (_AddXinhaoCommand = new RelayCommand(
                                          () =>
                                          {
                                              StationDataMode sdm = CurrentDatasProperty.CurrentDataProperty as StationDataMode;
                                              if (sdm != null)
                                                  AddXinhaoX(sdm.StationNameProperty, DivParts - 1);
                                          }));
            }
        }

        private RelayCommand _movexinhaoCommand;

        /// <summary>
        /// Gets the MoveXinHaoCommand.
        /// </summary>
        public RelayCommand MoveXinHaoCommand
        {
            get
            {
                return _movexinhaoCommand
                    ?? (_movexinhaoCommand = new RelayCommand(
                                          () =>
                                          {
                                              string[] parts = { };
                                              float offset1 = 0;
                                              float offset2 = 0;
                                              StationDataMode sdm = CurrentDatasProperty.CurrentDataProperty as StationDataMode;
                                              StationDataMode nextsdm = null;
                                              float newposition = 0;
                                              int index = CurrentDatasProperty.DataCollection.IndexOf(sdm);
                                              if (index == CurrentDatasProperty.DataCollection.Count - 2)
                                              {
                                                  nextsdm = sdm;
                                                  newposition = nextsdm.PositionProperty + (CurrentDatasProperty.DataCollection[index + 1].LengthProperty + 20) * sdm.ScaleProperty / 1000;
                                              }
                                              else
                                              {
                                                  nextsdm = CurrentDatasProperty.DataCollection[index + 2] as StationDataMode;
                                                  newposition = nextsdm.PositionProperty;
                                              }
                                              StationDataMode presdm = CurrentDatasProperty.DataCollection[index - 1] as StationDataMode;

                                              for (int i = presdm.SectionNumProperty; i < sdm.SectionNumProperty; i++)
                                              {
                                                  parts = cdlxlist[i - 1].Split(':');
                                                  offset1 += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                              }

                                              for (int i = sdm.SectionNumProperty; i < nextsdm.SectionNumProperty; i++)
                                              {
                                                  parts = cdlxlist[i - 1].Split(':');
                                                  offset2 += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                              }

                                              mxwindow = new MoveXinhaoWindow();

                                              GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string>
                                                  (string.Format("{0}:{1}:{2}:{3}:{4}",
                                                  presdm.PositionProperty,
                                                 newposition,
                                                  sdm.PositionProperty, offset1.ToString(), offset2.ToString()),
                                                  "Resources");
                                              mxwindow.ShowDialog();
                                          }));
            }
        }
        private RelayCommand _removexinhaoCommand;

        /// <summary>
        /// Gets the RemoveXinHaoCommand.
        /// </summary>
        public RelayCommand RemoveXinHaoCommand
        {
            get
            {
                return _removexinhaoCommand
                    ?? (_removexinhaoCommand = new RelayCommand(
                                          () =>
                                          {
                                              StationDataMode sdm = CurrentDatasProperty.CurrentDataProperty as StationDataMode;
                                              if (sdm != null)
                                                  DeleteXinhao(sdm.StationNameProperty);
                                          }));
            }
        }
        private RelayCommand _modifystationsignalCommand;

        /// <summary>
        /// Gets the ModifyStationSignalCommand.
        /// </summary>
        public RelayCommand ModifyStationSignalCommand
        {
            get
            {
                return _modifystationsignalCommand
                    ?? (_modifystationsignalCommand = new RelayCommand(
                                          () =>
                                          {
                                              ModifyStationSignal();
                                          }));
            }
        }
       
        #endregion

        [Serializable]
        public class SatitonNotLoadedException : Exception
        {
            public SatitonNotLoadedException() { }
            public SatitonNotLoadedException(string message) : base(message) { }
            public SatitonNotLoadedException(string message, Exception inner) : base(message, inner) { }
            protected SatitonNotLoadedException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context)
                : base(info, context) { }
        }
    }
}