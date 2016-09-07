using ExtractData;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inter_face.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using Microsoft.Win32;
using System.Threading;
using System.Windows;
using Inter_face.Views;
using LocomotiveSim;

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
        private string _XHEPath;
        private string _prePdpath;
        private string _preQxpath;
        private string _preBjpath;
        private string _preQspath;
        private string _takenOfcurrentDFX;
        private float _startPos;
        private float _endPos;
        private string _xhdataPath;
        private string _xhsavefilePath;
        private string[] lineparts = null;
        private ControlCenter _contrCen;

        private GraphyDataOper GDoper;
        private SignalDataExportor SDexportor;
        private AutoBuildOperator ABoper;
        private GraphyDataOper gdo;
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
        string[] colors = 
        { "#990033",
            "#FF3399", 
            "#660099", 
            "#0099FF", 
            "#CC6600", 
            "#66FF00", 
            "#CCFF00",
            "#FF0000", 
            "#996666", 
            "#99FF66" };

        MoveXinhaoWindow mxwindow;
        ModifyQujianWindow mqjwindow;
        StationWindow Swindow;
        SaveFileDialog sfwindow;
        ModifyCdldataWindow mcdwindown;
        ShowProcessBar showprocesbar;
        DianFXWindow dianfxwindow;
        AdjustSignalDisWindow asdwindow;
        ProcessForm processform;
        TrainInfoWindow tiWindow;
        GetBreakDisWindow gbdWindow;

        System.Windows.Threading.Dispatcher dispatcher;

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
        /// The <see cref="IsLdhMapLoadedProperty" /> property's name.
        /// </summary>
        public const string IsLdhMapLoadedPropertyPropertyName = "IsLdhMapLoadedProperty";

        private bool _isldhmaploadedProperty = false;

        /// <summary>
        /// Sets and gets the IsLdhMapLoadedProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsLdhMapLoadedProperty
        {
            get
            {
                return _isldhmaploadedProperty;
            }

            set
            {
                if (_isldhmaploadedProperty == value)
                {
                    return;
                }

                _isldhmaploadedProperty = value;
                RaisePropertyChanged(IsLdhMapLoadedPropertyPropertyName);
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
                Direction = 0;
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
                Direction = 1;
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
                if (_seletedxinhaosProperty)
                    Direction = 1;
                else
                    Direction = 0;
                RaisePropertyChanged(SeletedXinhaoSPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="containpddata" /> property's name.
        /// </summary>
        public const string containpddataPropertyName = "containpddata";

        private bool _ContainProperty = false;

        /// <summary>
        /// Sets and gets the containpddata property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool containpddata
        {
            get
            {
                return _ContainProperty;
            }

            set
            {
                if (_ContainProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(containpddataPropertyName);
                _ContainProperty = value;
                RaisePropertyChanged(containpddataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Cursor" /> property's name.
        /// </summary>
        public const string CursorPropertyName = "Cursor";

        private int _cursorProperty = 0;

        /// <summary>
        /// Sets and gets the Cursor property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Cursor
        {
            get
            {
                return _cursorProperty;
            }

            set
            {
                if (_cursorProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(CursorPropertyName);
                _cursorProperty = value;
                RaisePropertyChanged(CursorPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsXinhaoChanged" /> property's name.
        /// </summary>
        public const string IsXinhaoChangedPropertyName = "IsXinhaoChanged";

        private bool _isxinhaochangedProperty = false;
        Thread QuikSaveXhdataThread = null;

        /// <summary>
        /// Sets and gets the IsXinhaoChanged property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsXinhaoChanged
        {
            get
            {
                return _isxinhaochangedProperty;
            }

            set
            {
                if (value)
                {
                    
                    if (GDoper != null)
                    {
                        if (QuikSaveXhdataThread != null && QuikSaveXhdataThread.IsAlive)
                        {
                            QuikSaveXhdataThread.Abort();
                            QuikSaveXhdataThread = null;
                        }

                        if (!string.IsNullOrEmpty(_xhdataPath))
                        {
                            QuikSaveXhdataThread = new Thread(() =>
                            {
                                ISingleDataViewModel singledata = null;
                                List<ChangeToTxt.CheZhanOutputData> xhs = new List<ChangeToTxt.CheZhanOutputData>();
                                List<ChangeToTxt.CheZhanOutputData> xhx = new List<ChangeToTxt.CheZhanOutputData>();
                                string[] parts;
                                string bjData = string.Empty;

                                try
                                {

                                    singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.SingleS);
                                }
                                catch
                                {
                                    singledata = _DataBin.SingleOrDefault(p => p.TypeNum == (int)DataType.SingleS);
                                }
                                if (singledata != null)
                                {
                                    foreach (StationDataMode item in singledata.DataCollection.ToArray())
                                    {
                                        parts = item.StationNameProperty.Split(':');
                                        if (!parts[0].StartsWith("Q"))
                                        {
                                            switch (parts[0])
                                            {
                                                case "1":
                                                    bjData = string.Format("{0}:{1}:{2}", parts[1], parts[2], parts[3]);
                                                    break;
                                                case "2":
                                                    bjData = parts[1];
                                                    break;
                                                case "3":
                                                    //电分相名称：无电区左边缘（路段号+里程）：无电区中心（路段号+里程）：无电区右边缘（路段号+里程）：无电区长度
                                                    bjData = string.Format("{0}:{1}:{2}:{3}:{4}", parts[1], parts[2], parts[3], parts[4], parts[5]);
                                                    break;
                                                default:
                                                    break;
                                            }

                                            xhs.Add(new ChangeToTxt.CheZhanOutputData()
                                            {
                                                Bjlx = parts[0],
                                                Bjsj = bjData,
                                                Gh = item.HatProperty,
                                                Glb = item.PositionProperty.ToString("F3"),
                                                Index = string.Empty,
                                                Ldh = item.SectionNumProperty.ToString(),
                                                Zjfx = "1"
                                            });
                                        }
                                    }
                                }

                                bjData = string.Empty;
                                try
                                {

                                    singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.Single);
                                }
                                catch
                                {
                                    singledata = _DataBin.SingleOrDefault(p => p.TypeNum == (int)DataType.Single);
                                }

                                if (singledata != null)
                                {
                                    foreach (StationDataMode item in singledata.DataCollection.ToArray())
                                    {
                                        parts = item.StationNameProperty.Split(':');
                                        if (!parts[0].StartsWith("Q"))
                                        {
                                            switch (parts[0])
                                            {
                                                case "1":
                                                    bjData = string.Format("{0}:{1}:{2}", parts[1], parts[2], parts[3]);
                                                    break;
                                                case "2":
                                                    bjData = parts[1];
                                                    break;
                                                case "3":
                                                    bjData = string.Format("{0}:{1}:{2}:{3}:{4}", parts[1], parts[2], parts[3], parts[4], parts[5]);
                                                    break;
                                                default:
                                                    break;
                                            }

                                            xhx.Add(new ChangeToTxt.CheZhanOutputData()
                                            {
                                                Bjlx = parts[0],
                                                Bjsj = bjData,
                                                Gh = item.HatProperty,
                                                Glb = item.PositionProperty.ToString("F3"),
                                                Index = string.Empty,
                                                Ldh = item.SectionNumProperty.ToString(),
                                                Zjfx = "1"
                                            });
                                        }
                                    }
                                }

                                string backupfile = Path.Combine(Path.GetDirectoryName(_xhdataPath),
                                                                 Path.GetFileNameWithoutExtension(_xhdataPath) +
                                                                 ".xhbackup");
                                if (!_xhdataPath.Equals(string.Empty))
                                {
                                    GDoper.XhDataQuikSave(xhs, xhx, backupfile);
                                }
                            });

                            QuikSaveXhdataThread.Start();
                        }                        
                    }
                }

                if (_isxinhaochangedProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(IsXinhaoChangedPropertyName);
                _isxinhaochangedProperty = value;
                RaisePropertyChanged(IsXinhaoChangedPropertyName);                
            }
        }

        /// <summary>
        /// The <see cref="exPortTypeIndex" /> property's name.
        /// </summary>
        public const string exPortTypeIndexPropertyName = "exPortTypeIndex";

        private int _exporttypeindexProperty = 0;

        /// <summary>
        /// Sets and gets the exPortTypeIndex property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int exPortTypeIndex
        {
            get
            {
                return _exporttypeindexProperty;
            }

            set
            {
                if (_exporttypeindexProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(exPortTypeIndexPropertyName);
                _exporttypeindexProperty = value;
                if (_exporttypeindexProperty == 0)
                    ExportXhText = "分页输出";
                else
                    ExportXhText = "单页输出";
                RaisePropertyChanged(exPortTypeIndexPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ExportXhText" /> property's name.
        /// </summary>
        public const string ExportXhTextPropertyName = "ExportXhText";

        private string _exportxhtextProperty = "";

        /// <summary>
        /// Sets and gets the ExportXhText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ExportXhText
        {
            get
            {
                return _exportxhtextProperty;
            }

            set
            {
                if (_exportxhtextProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(ExportXhTextPropertyName);
                _exportxhtextProperty = value;
                RaisePropertyChanged(ExportXhTextPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="CanloadxhProperty" /> property's name.
        /// </summary>
        public const string CanloadxhPropertyPropertyName = "CanloadxhProperty";

        private bool _canloadxhProperty = false;

        /// <summary>
        /// Sets and gets the CanloadxhProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool CanloadxhProperty
        {
            get
            {
                return _canloadxhProperty;
            }

            set
            {
                if (_canloadxhProperty == value)
                {
                    return;
                }

                _canloadxhProperty = value;
                RaisePropertyChanged(CanloadxhPropertyPropertyName);
            }
        }
        /// <summary>
        /// Initializes a new instance of the DataCollectionViewModel class.
        /// </summary>
        public DataCollectionViewModel()
        {
            _datascollection = new ObservableCollection<ISingleDataViewModel>();
            //_datascollection.CollectionChanged += _datascollection_CollectionChanged;            
            _Pdpath = Path.Combine(_currentdir, @"excelmodels\接坡面数据_Single.xlsx");
            _Qxpath = Path.Combine(_currentdir, @"excelmodels\接曲线数据_single.xlsx");
            _Bjpath = Path.Combine(_currentdir, @"excelmodels\接标记数据_single.xlsx");
            _XhPath = Path.Combine(_currentdir, @"excelmodels\信号机数据_single.xlsx");
            _XHEPath = Path.Combine(_currentdir, @"excelmodels\信号机位置输出.xlsx");
            _prePdpath = Path.Combine(_currentdir, @"excelmodels\接坡面数据.xlsx");
            _preQxpath = Path.Combine(_currentdir, @"excelmodels\接曲线数据.xlsx");
            _preBjpath = Path.Combine(_currentdir, @"excelmodels\接标记数据.xlsx");
            GDoper = GraphyDataOper.CreatOper(_Pdpath, _Bjpath, _Qxpath, _XhPath);
            ABoper = AutoBuildOperator.CreatOper(_prePdpath, _preBjpath, _preQxpath);
            SDexportor = SignalDataExportor.CreatOper(_XHEPath);
            _DataBin = new List<ISingleDataViewModel>();
            _takenOfcurrentDFX = string.Empty;
            _xhdataPath = string.Empty;
            _startPos = -1;
            _endPos = -1;
            ExportXhText = "分页输出";
            _contrCen = new ControlCenter();

            MessengerInstance.Register<GraphyDataOper>(this, "gdo", p => { gdo = p; });
            MessengerInstance.Register<System.Windows.Threading.Dispatcher>(this, "Dispatcher", p => { dispatcher = p; });

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
                        case (int)DataType.Break:
                            IsLdhMapLoadedProperty = false;
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
                    containpddata = false;
                    lineparts = null;
                    _startPos = -1;
                    _endPos = -1;
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
                            //break;
                        }
                        else
                        {
                            item.CurrentDataProperty = null;
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
                        if (mxwindow != null)
                        {
                            mxwindow.Close();
                            mxwindow = null;
                        }
                        /*CurrentDatasProperty.SelectedIndex = index;

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
                                                  "Resources");*/
                    }
                });
            MessengerInstance.Register<string>(this, "InsertXinhao",
                (p) =>
                {
                    string[] parts = p.Split(':');
                    int sec = int.Parse(parts[1]) == cdlxlist.Count() + 2 ? cdlxlist.Count() + 1 : int.Parse(parts[1]);
                    InsertXinhaoX(float.Parse(parts[0]), sec, parts[2], null);
                });

            MessengerInstance.Register<List<StationDataMode>>(this, "UpdataStationSignal",
                (p) =>
                {
                    if (!SeletedXinhaoS)
                        p.Reverse();
                    UpdataStationSignals(p);
                    Swindow.Close();
                });

            MessengerInstance.Register<float>(this, "AdjustSignalsDis",
                (p) =>
                {
                    AdjustSignalsTosameDistence(p);
                    if (asdwindow != null)
                    {
                        asdwindow.Close();
                        asdwindow = null;
                    }
                });

            MessengerInstance.Register<StationDataMode>(this, "InsertDianfx",
                p =>
                {
                    if (dianfxwindow != null)
                    {
                        dianfxwindow.Close();
                        dianfxwindow = null;
                    }

                    if (p != null)
                    {
                        string[] infos = p.StationNameProperty.Split(':');
                        InsertDianFXx(string.Format("{0}:{1}:{2}",
                            infos[2].Split('+')[1], infos[3].Split('+')[1], infos[4].Split('+')[1]), p);
                    }

                });
            MessengerInstance.Register<StationDataMode>(this, "UpdataDianfx",
               p =>
               {
                   if (dianfxwindow != null)
                   {
                       dianfxwindow.Close();
                       dianfxwindow = null;
                   }

                   if (p != null)
                   {
                       string[] infos = p.StationNameProperty.Split(':');
                       UpdataDianfx(string.Format("{0}:{1}:{2}",
                           infos[2].Split('+')[1], infos[3].Split('+')[1], infos[4].Split('+')[1]), p);
                   }

               });
            MessengerInstance.Register<DataType>(this, "ShowRightDialog", p =>
            {
                showRightDialog();
            });

            MessengerInstance.Register<string>(this, "ExCommand",
                p => { CommitMenuCommand(p); });

            MessengerInstance.Register<string>(this, "FixPosError",
                p =>
                {
                    string[] fixinfo = p.Split('?');
                    if (!ABoper.AdjustLength(fixinfo[2], int.Parse(fixinfo[0]), float.Parse(fixinfo[1])))
                    {
                        MessageBox.Show("高程自动调整失败，请手动调整！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            MessengerInstance.Register<string>(this, "formatMu",
                p =>
                {
                    //trainame:filepath:protectdis:tk
                    if (p != string.Empty)
                    {
                        string[] infos = p.Split('|');
                        _contrCen = formatMu(infos[1], 
                            float.Parse(infos[2]), int.Parse(infos[3]), int.Parse(infos[4]));
                    }

                });

            MessengerInstance.Register<string>(this, "calculeteDis", p => { calculetedis(p); });        
        }

        private void ShowProcessWindow()
        {
            processform = new ProcessForm();
            processform.ShowDialog();
        }

        private void CloseProcessWindow()
        {
            dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
               new Action(() =>
               {
                   if (processform != null)
                   {
                       processform.Close();
                       processform = null;
                   }
               }));            
        }

        private void CommitMenuCommand(string cmd)
        {
            switch (cmd)
            {
                case "InsertXH":
                    InsertXinhaoCommand.Execute(null);
                    break;
                case "InsertDFX":
                    beginInsertDianfx();
                    break;
                case "DeleteXH":
                    RemoveXinHaoCommand.Execute(null);
                    break;
                case "MoveXH":
                    MoveSignal();
                    break;
                case "DeleteDFX":
                    DeleteDFXCommand.Execute(null);
                    break;
                case "ModifyDFX":
                    beginModifyDianfx();
                    break;
                case "AdjustQJ":
                    BeginAdjustSignalsDis();
                    break;
                default:
                    break;
            }
        }
        void _datascollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IsXinhaoChanged = true;
        }
        public void LoadLdhMap()
        {
            if (!IsLdhMapLoadedProperty)
            {
                foreach (ISingleDataViewModel item in _datascollection)
                {
                    if (item.TypeNum == (int)DataType.Break)
                    {
                        DatasCollection.Remove(item);                       
                        break;
                    }
                }
                CountProperty = DatasCollection.Count;
                return;
            }

            SingleDataViewModel sdvm = null;
            
            if (sdvm == null)
            {

                sdvm = new SingleDataViewModel();
                sdvm.TypeNameProperty = "路段号";
                sdvm.ShowDataProperty = true;
                sdvm.TypeNum = (int)DataType.Break;

                try
                {
                    Cursor = 1;
                    cdlxlist = GDoper.GetCdlData(
                        Path.Combine(Environment.CurrentDirectory, @"excelmodels\接坡面数据.xlsx")).ToList();

                    if (cdlxlist.Count != 0)
                    {
                        int n = 0;
                        string[] parts;
                        string[] nextparts;

                        for (int i = 0; i < cdlxlist.Count; i++)
                        {
                            parts = cdlxlist[i].Split(':');

                            if (i == 0)
                            {
                                sdvm.DataCollection.Add(new StationDataMode()
                                {
                                    HatProperty = parts[0].Split('+')[0],
                                    LengthProperty = !isAnyDatashowed() ? 1000 / ScaleProperty :
                                    (float.Parse(parts[0].Split('+')[1]) - _startPos * 1000) / ScaleProperty,
                                    RealLength = !isAnyDatashowed() ? 1000 / ScaleProperty :
                                    (float.Parse(parts[0].Split('+')[1]) - _startPos * 1000) / ScaleProperty,
                                    PositionProperty = _startPos == -1 ? float.Parse(parts[0].Split('+')[1]) / 1000 - 1 : _startPos,
                                    Type = DataType.Break,
                                    SectionNumProperty = ++n,
                                    ScaleProperty = ScaleProperty,
                                    SelectedProperty = false,
                                    PathDataProperty = string.Format("20:1 0:#00DC5625:{0}:M0,0 L500,0", colors[n % 9]),
                                    StationNameProperty = string.Format("小于{0} {1}", parts[0].Split('+')[0],
                                        (float.Parse(parts[0].Split('+')[1]) / 1000).ToString("F3")),
                                });
                                //continue;
                            }

                            if (i == cdlxlist.Count - 1)
                            {
                                sdvm.DataCollection.Add(new StationDataMode()
                                {
                                    HatProperty = parts[1].Split('+')[0],
                                    LengthProperty = !isAnyDatashowed() ? 1000 / ScaleProperty :
                                    (_endPos * 1000 - float.Parse(parts[1].Split('+')[1])) / ScaleProperty,
                                    RealLength = !isAnyDatashowed() ? 1000 / ScaleProperty : 
                                    (_endPos * 1000 - float.Parse(parts[1].Split('+')[1])) / ScaleProperty,
                                    PositionProperty = float.Parse(parts[1].Split('+')[1]) / 1000,
                                    Type = DataType.Break,
                                    SectionNumProperty = ++n,
                                    ScaleProperty = ScaleProperty,
                                    SelectedProperty = false,
                                    PathDataProperty = string.Format("20:1 0:#00DC5625:{0}:M0,0 L500,0", colors[n % 9]),
                                    StationNameProperty = string.Format("大于{0} {1}", parts[1].Split('+')[0],
                                    (float.Parse(parts[1].Split('+')[1]) / 1000).ToString("F3")),
                                });
                                continue;
                            }

                            nextparts = cdlxlist[i + 1].Split(':');
                            sdvm.DataCollection.Add(new StationDataMode()
                            {
                                HatProperty = parts[1].Split('+')[0],
                                LengthProperty = !isAnyDatashowed() ? 1000 / ScaleProperty : 
                                (float.Parse(nextparts[0].Split('+')[1]) - float.Parse(parts[1].Split('+')[1])) / ScaleProperty,
                                RealLength = !isAnyDatashowed() ? 1000 / ScaleProperty : 
                                (float.Parse(nextparts[0].Split('+')[1]) - float.Parse(parts[1].Split('+')[1])) / ScaleProperty,
                                PositionProperty = float.Parse(parts[1].Split('+')[1]) / 1000,
                                Type = DataType.Break,
                                SectionNumProperty = ++n,
                                ScaleProperty = ScaleProperty,
                                SelectedProperty = false,
                                PathDataProperty = string.Format("20:1 0:#00DC5625:{0}:M0,0 L500,0", colors[n % 9]),
                                StationNameProperty = string.Format("{0} {1}--{2} {3}", parts[1].Split('+')[0],
                                (float.Parse(parts[1].Split('+')[1]) / 1000).ToString("F3"),
                                nextparts[0].Split('+')[0],
                                (float.Parse(nextparts[0].Split('+')[1]) / 1000).ToString("F3")),
                            });
                        }
                        Cursor = 0;
                    }                   
                }

                catch (NullReferenceException ure)
                {
                    MessengerInstance.Send<string>(ure.Message, "ReadDataError");
                }

                finally
                {
                    Cursor = 0;
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

            IsLdhMapLoadedProperty = true;
            CountProperty = DatasCollection.Count;
        }
        private bool isAnyDatashowed()
        {
            return IsPoduLoadedProperty || IsQuXianLoadedProperty || IsCheZhanLoadedProperty || IsLiChengLoadedProperty
                   || IsXinhaoSLoaded || IsXinhaoSLoaded;
        }
        public void LoadPoduXData()
        {
            if (!IsPoduLoadedProperty)
            {
                removeAdataItem((int)DataType.Podu);
                return;
            }

            SingleDataViewModel sdvm = null;
            int part = 0;

            foreach (ISingleDataViewModel Singleitem in _DataBin)
            {
                if (Singleitem.TypeNum == (int)DataType.Podu)
                {
                    sdvm = (SingleDataViewModel)Singleitem;
                    sdvm.ShowDataProperty = true;
                    break;
                }
            }

            foreach (ISingleDataViewModel item in DatasCollection)
            {
                if (item.TypeNum == (int)DataType.Break)
                {
                    DatasCollection.Remove(item);
                    break;
                }
            }
            IsLdhMapLoadedProperty = false;

            if (sdvm == null)
            {

                sdvm = new SingleDataViewModel();
                sdvm.TypeNameProperty = "坡度";
                sdvm.ShowDataProperty = true;
                sdvm.TypeNum = (int)DataType.Podu;
                //showprocesbar = new ShowProcessBar();

                try
                {
                    //Thread procesthread = new Thread(() => { showprocesbar.show(); });
                    //procesthread.Start();

                    //MessengerInstance.Send<string>("正在读取数据", "msg");
                    Cursor = 1;
                    GDoper.GetPoDuData(out pdxlist, out pdslist);
                    //MessengerInstance.Send<int>(50, "processes");
                    //MessengerInstance.Send<string>("开始生成坡度图形", "msg");
                    cdlxlist = pdxlist.TakeWhile(p => p.Cdl != "+:+").Select(q => q.Cdl).ToList();
                    if (cdlxlist.Count == 0)
                    {
                        cdlxlist.Add("0+0:0+0");
                    }
                    part = (int)(pdxlist.Count() / 5);
                    _startPos = float.Parse(pdxlist[0].Qdglb);
                    _endPos = float.Parse(pdxlist[pdxlist.Count - 1].Qdglb);

                    foreach (ChangeToTxt.PoduOutputData item in pdxlist)
                    {
                        if (!item.Pc.Equals("0"))
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
                    Cursor = 0;
                    //MessengerInstance.Send<int>(100, "processes");
                    //MessengerInstance.Send<string>("坡度图形生成完毕", "msg");

                }

                catch (NullReferenceException ure)
                {
                    MessengerInstance.Send<string>(ure.Message, "ReadDataError");
                }

                finally
                {
                    Cursor = 0;
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
            containpddata = true;
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
                                     HatProperty = currentldh - 1 == cdlxlist.Count ? 
                                                                     cdlxlist[currentldh - 2].Split(':')[1].Split('+')[0] :
                                                                     cdlxlist[currentldh - 1].Split(':')[0].Split('+')[0],
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
                                 RealLength = len + 50 / ScaleProperty,
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
                                 RealLength = len + 100 / ScaleProperty,
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
                             RealLength = 100 / ScaleProperty,
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
                    Cursor = 1;
                    GDoper.GetXhDataS(out xhslist);

                    sdvm = new SingleDataViewModel();
                    sdvm.TypeNameProperty = "信号机(X)";
                    sdvm.ShowDataProperty = true;
                    sdvm.TypeNum = (int)DataType.SingleS;

                    int n = 0;
                    int m = 0;
                    string[] parts = { };
                    float offset = 0;
                    float len = 0;
                    float pos = 0;
                    float addingpart = 200;

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
                            RealLength = len,
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
                                    ((float.Parse(item.Glb) - float.Parse(pdxlist[0].Qdglb)) * 1000 - offset) / ScaleProperty : 0;

                                sdvm.DataCollection.Add(new StationDataMode()
                                {
                                    HatProperty = item.Gh,
                                    LengthProperty = len,
                                    RealLength = len,
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
                                addingpart = float.Parse(presdm.Bjlx.Equals("3") ? presdm.Bjsj.Split(':')[4] : "200");
                                //len = (float)(float.Parse(item.Glb) - float.Parse(presdm.Glb) - 0.2) * 1000 / ScaleProperty;
                                pos = float.Parse(presdm.Glb);

                                for (int i = int.Parse(presdm.Ldh); i < int.Parse(item.Ldh); i++)
                                {
                                    parts = cdlxlist[m].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                    m++;
                                }

                                len = ((float.Parse(item.Glb) - float.Parse(presdm.Glb)) * 1000 - offset - addingpart)
                                        / ScaleProperty;

                                sdvm.DataCollection.Add(new StationDataMode()
                                {
                                    HatProperty = presdm.Gh,
                                    LengthProperty = len,
                                    RealLength = len + addingpart / ScaleProperty,
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
                            //M-27,30 L5,30 M57,31 C57,47 45,59 30,59 C15,59 2.5,47 3,31 C3,15 15,3 30,3 C45,3 57,15 57,31 z
                            float currentlen = float.Parse(item.Bjlx.Equals("3") ? item.Bjsj.Split(':')[4] : "200");
                            sdvm.DataCollection.Add(new StationDataMode()
                            {
                                HatProperty = item.Gh,
                                LengthProperty = currentlen / ScaleProperty,
                                RealLength = currentlen / ScaleProperty,
                                PositionProperty = float.Parse(item.Glb),
                                Type = DataType.Single,
                                SectionNumProperty = int.Parse(item.Ldh),
                                ScaleProperty = ScaleProperty,
                                SelectedProperty = false,
                                PathDataProperty = (item.Bjlx.Equals("3") ? "1:1 0:#00DC5625:#FF4500:M334,361 L436,410 M334,410 L436,361 " : item.Bjlx.Equals("2") ?
                                "1:1 0:#FFDC5625:#FF000000:M388,269 C394,269,399,264,399,258 C399,251,394,247,388,247 C381,247,376,251,376,258 C376,264,381,269,388,269 M347,242 L347,258 L376,258 M347,257 L347,270 M412,269 C418,269,423,264,423,258 C423,252,418,247,412,247 C405,247,400,252,400,258 C400,264,405,269,412,267 z" :
                                "1:1 0:#FFDC5625:#FF000000:M400,208 C409,208,418,200,418,191 C418,181,409,174,400,174 C390,174,382,181,382,191 C382,200,390,208,400,208 M353,191 L383,191 M353,171 L353,212"),
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
                            RealLength = len + 200 / ScaleProperty,
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
                    MessengerInstance.Send<string>("读入信号数据出错", "ReadDataError");
                    return;
                }

                catch
                {
                    MessengerInstance.Send<string>("读入信号数据出错", "ReadDataError");
                    return;
                }

                finally
                {
                    Cursor = 0;
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
                    Cursor = 1;
                    GDoper.GetXhData(out xhxlist);

                    sdvm = new SingleDataViewModel();
                    sdvm.TypeNameProperty = "信号机(S)";
                    sdvm.ShowDataProperty = true;
                    sdvm.TypeNum = (int)DataType.Single;

                    int n = 0;
                    int m = 0;
                    string[] parts = { };
                    //string moreinfos;
                    float offset = 0;
                    float len = 0;
                    float pos = 0;
                    float addingpart = 200;                    

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
                            RealLength = len,
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
                                    ((float.Parse(item.Glb) - float.Parse(pdxlist[0].Qdglb)) * 1000 - offset) / ScaleProperty 
                                    : 0;

                                sdvm.DataCollection.Add(new StationDataMode()
                                {
                                    HatProperty = item.Gh,
                                    LengthProperty = len,
                                    RealLength = len,
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
                                addingpart = float.Parse(presdm.Bjlx.Equals("3") ? presdm.Bjsj.Split(':')[4] : "200");

                                //len = (float)(float.Parse(item.Glb) - float.Parse(presdm.Glb) - 0.2) * 1000 / ScaleProperty;
                                pos = float.Parse(presdm.Glb);

                                for (int i = int.Parse(presdm.Ldh); i < int.Parse(item.Ldh); i++)
                                {
                                    parts = cdlxlist[m].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                    m++;
                                }

                               /* if (presdm.Bjlx.Equals("3"))
                                {
                                    len = ((float.Parse(item.Glb) - float.Parse(presdm.Bjsj.Split(':')[1].Split('+')[0]) * 1000 -
                                            offset - addingpart)) / ScaleProperty;
                                }
                                else*/
                                    len = ((float.Parse(item.Glb) - float.Parse(presdm.Glb)) * 1000
                                          - offset - addingpart) / ScaleProperty;

                                sdvm.DataCollection.Add(new StationDataMode()
                                {
                                    HatProperty = presdm.Gh,
                                    LengthProperty = len,
                                    RealLength = len + addingpart / ScaleProperty,
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

                            float currentlen = float.Parse(item.Bjlx.Equals("3") ? item.Bjsj.Split(':')[4] : "200");
                           
                            sdvm.DataCollection.Add(new StationDataMode()
                            {
                                HatProperty = item.Gh,
                                LengthProperty = currentlen / ScaleProperty,
                                RealLength = currentlen / ScaleProperty,
                                PositionProperty = float.Parse(item.Glb),
                                Type = DataType.Single,
                                SectionNumProperty = int.Parse(item.Ldh),
                                ScaleProperty = ScaleProperty,
                                SelectedProperty = false,
                                PathDataProperty = (item.Bjlx.Equals("3") ? "1:1 0:#00DC5625:#FF4500:M334,361 L436,410 M334,410 L436,361" : item.Bjlx.Equals("2") ?
                                "1:1 0:#FFDC5625:#FF000000:M383,333 C377,333,371,328,371,322 C371,316,376,311,383,311 C389,310,394,315,394,321 C395,328,389,333,383,333 M424,337 L423,321 L394,322 M423,321 L423,308 M359,333 C352,333,347,328,347,322 C347,316,352,311,358,311 C365,311,370,316,370,322 C370,328,365,333,359,333 z" :
                                "1:1 0:#FFDC5625:#FF000000:M371,143 C361,143,353,136,353,126 C353,117,361,109,371,109 C381,109,389,117,389,126 C389,136,381,143,371,143 M418,127 L388,127 M418,149 L418,105"),
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
                            RealLength = len + 200 / ScaleProperty,
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
                    MessengerInstance.Send<string>("读入信号数据出错", "ReadDataError");
                    return;
                }

                catch
                {
                    MessengerInstance.Send<string>("读入信号数据出错", "ReadDataError");
                    return;
                }

                finally
                {
                    Cursor = 0;
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
                    
                    float offset = 0;
                    string[] parts = null;

                    foreach (string cdl in cdlxlist)
                    {
                        parts = cdl.Split(':');
                        offset += (float.Parse(parts[0].Split('+')[1]) - float.Parse(parts[1].Split('+')[1]));
                    }

                    float rawender = float.Parse(pdxlist[pdxlist.Count - 1].Qdglb) +
                        float.Parse(pdxlist[pdxlist.Count - 1].Pc) / 1000;
                    int ender = (int)Math.Ceiling(rawender + offset / 1000);               
                    int step = 1;
                    int p = 0;

                    for (int i = starter; i < ender; i+=step)
                    {
                        lc = 0;
                        pathes = string.Empty;                        
                        
                        float le = 0;
                        float et = 0;                        
                        int nearcount = 0;
                        int index = 0;
                        int firstl = 0;

                        if (p < cdlxlist.Count)
                        {
                            parts = cdlxlist[p].Split(':');
                            if (i == starter)
                            {
                                firstl = (int)Math.Round((float.Parse(pdxlist[0].Qdglb) - starter) * 1000, 0);
                                //pathes = string.Format("M0,25 L0,40 {0},40", lc);
                                index = (int)Math.Ceiling(firstl / 100d);
                            }
                            else
                            {
                                firstl = 0;
                                index = 0;
                            }

                            if (Math.Floor(float.Parse(parts[0].Split('+')[1]) / 1000) == i)
                            {
                                offset -= (float.Parse(parts[0].Split('+')[1]) - float.Parse(parts[1].Split('+')[1]));
                                ender = (int)Math.Ceiling(rawender + offset / 1000);

                                le = (float.Parse(parts[0].Split('+')[1]) - (i * 1000 + firstl)) / ScaleProperty;
                                nearcount = (int)Math.Ceiling((float.Parse(parts[0].Split('+')[1]) / 1000
                                - i) * 10);

                                for (int k = index; k < nearcount; k++)
                                {

                                    if (k == 0)
                                    {
                                        pathes += string.Format("M{0},25 L{1},40 {2},40", lc, lc, lc + 100 / ScaleProperty);
                                        lc += 100 / ScaleProperty;
                                        continue;
                                    }

                                    if (k == nearcount - 1)
                                    {
                                        et = (float)((float.Parse(parts[0].Split('+')[1]) - i * 1000 - (nearcount - 1) * 100)
                                         / ScaleProperty);
                                        pathes += string.Format("M{0},35 L{1},40 {2},40", lc, lc, lc + et
                                         );
                                        lc = lc + et;
                                        continue;
                                    }

                                    pathes += string.Format("M{0},35 L{1},40 {2},40", lc, lc, lc + 100 / ScaleProperty);
                                    lc += 100 / ScaleProperty;
                                }

                                if (nearcount != 0)
                                {
                                    sdvm.DataCollection.Add(new StationDataMode()
                                    {
                                        HatProperty = parts[0].Split('+')[0],
                                        LengthProperty = le,
                                        PositionProperty = i + firstl / 1000,
                                        Type = DataType.Position,
                                        SectionNumProperty = p + 1,
                                        ScaleProperty = ScaleProperty,
                                        SelectedProperty = false,
                                        PathDataProperty = string.Format("2:1 0:#00DC5625:#FF000000:{0}", pathes),
                                        StationNameProperty = string.Empty
                                    });
                                }

                                pathes = string.Empty;
                                lc = 0;

                                string far = parts[1].Split('+')[1].Contains('.') ?
                                   parts[1].Split('+')[1] : parts[1].Split('+')[1] + ".000";
                                int temp = int.Parse((float.Parse(far) / 1000).ToString("F3").Split('.')[1]);
                                if (temp > 0 && temp < 100) temp = 100;
                                nearcount = (int)(10 - Math.Floor((double)(temp / 100)));
                                le = (float)((Math.Ceiling(float.Parse(far) / 1000) * 1000
                                    - float.Parse(far)) / ScaleProperty);

                                for (int k = 10; k > nearcount; k--)
                                {
                                    if (k == nearcount + 1)
                                    {
                                        et = (float)(((nearcount + 1) * 100 - float.Parse(far.Split('.')[1]) * 1000)
                                         / ScaleProperty);
                                        pathes += string.Format("M{0},15 L{1},40 {2},40", lc, lc, lc + et);
                                        //lc = lc + et;
                                        continue;
                                    }

                                    pathes += string.Format("M{0},35 L{1},40 {2},40", lc, lc, lc + 100 / ScaleProperty);
                                    lc += 100 / ScaleProperty;
                                }

                                if (nearcount != 10)
                                {
                                    sdvm.DataCollection.Add(new StationDataMode()
                                    {
                                        HatProperty = parts[1].Split('+')[0],
                                        LengthProperty = le,
                                        PositionProperty = (float)(float.Parse(parts[1].Split('+')[1]) / 1000),
                                        Type = DataType.Position,
                                        SectionNumProperty = p + 2,
                                        ScaleProperty = ScaleProperty,
                                        SelectedProperty = false,
                                        PathDataProperty = string.Format("2:1 0:#00DC5625:#FF000000:{0}", pathes),
                                        StationNameProperty = string.Empty
                                    });

                                    int sec = (int)Math.Floor(float.Parse(parts[1].Split('+')[1]) / 1000) + 1;
                                    int fst = (int)Math.Floor(float.Parse(parts[0].Split('+')[1]) / 1000);
                                    step = sec - fst;
                                }
                                else
                                {
                                    int sec = (int)Math.Floor(float.Parse(parts[1].Split('+')[1]) / 1000);
                                    int fst = (int)Math.Floor(float.Parse(parts[0].Split('+')[1]) / 1000);
                                    step = sec - fst;

                                }
                                p += 1;
                            }
                            else
                            {
                                step = 1;
                            }

                            if (parts != null && Math.Floor(float.Parse(parts[0].Split('+')[1]) / 1000) == i) continue;
                        }

                       /* if (i == starter)
                        {
                            int firstl = (int)Math.Round((float.Parse(pdxlist[0].Qdglb) - starter) * 1000, 0);
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
                                PositionProperty = starter + firstl / 1000,
                                Type = DataType.Position,
                                SectionNumProperty = int.Parse(pdxlist[0].Ldh),
                                ScaleProperty = ScaleProperty,
                                SelectedProperty = false,
                                PathDataProperty = string.Format("2:1 0:#00DC5625:#FF000000:{0}", pathes),
                                StationNameProperty = string.Empty
                            });

                            step = 1;
                            starter = -1;
                            continue;
                        }*/

                        lc = 0;
                        pathes = string.Empty;

                        for (int k = index; k < 10; k++)
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
                            LengthProperty = (1000 - firstl) / ScaleProperty,
                            PositionProperty = i,
                            Type = DataType.Position,
                            SectionNumProperty = int.Parse(pdxlist[0].Ldh),
                            ScaleProperty = ScaleProperty,
                            SelectedProperty = false,
                            PathDataProperty = string.Format("2:1 0:#00DC5625:#FF000000:{0}", pathes),
                            StationNameProperty = string.Empty
                        });

                        step = 1;
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
            //odd false为下行
            if (!odd)
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
            float addtionallength = 0;

            try
            {
                if (!SeletedXinhaoS)
                    singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.Single);
                else
                    singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.SingleS);

                StationDataMode sdm = (StationDataMode)singledata.DataCollection.
                    Single(q => ((StationDataMode)q).StationNameProperty.Equals(taken));

                int index = singledata.DataCollection.IndexOf(sdm);
                if (index != 0)
                {
                    StationDataMode prisdm = singledata.DataCollection[index - 1] as StationDataMode;
                    addtionallength = prisdm.RealLength;
                }

                float offset = 0;
                string[] parts = { };

                float len = sdm.RealLength;
                int odd = 0;
                float divide = 0;
                float pos = 0;
                int sec = sdm.SectionNumProperty;
                StationDataMode newsdmxh = null;
                StationDataMode newsdmqj = null;
                offset = 0;

                for (int i = 0; i < numbers; i++)
                {
                    odd = 0;
                    if (i == 0)
                    {
                        odd = (int)(Math.Round(len) % (numbers + 1));
                        divide = (float)(Math.Round(len) - odd) / (numbers + 1);

                        newsdmqj = new StationDataMode()
                        {
                            HatProperty = sdm.HatProperty,
                            LengthProperty = divide + odd - addtionallength,
                            RealLength = divide + odd,
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
                        if (sec != cdlxlist.Count() + 1)
                        {
                            parts = cdlxlist[sec - 1].Split(':');
                            if ((pos + divide * ScaleProperty / 1000 + odd / 1000) > float.Parse(parts[0].Split('+')[1]) / 1000)
                            {
                                offset = float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]);
                                sec += 1;
                            }
                        }
                        else
                            parts = cdlxlist[sec - 2].Split(':');                        
                    }

                    pos = pos + (divide + odd) * ScaleProperty / 1000 + offset / 1000;

                    newsdmxh = new StationDataMode()
                    {
                        HatProperty = parts[0].Split('+')[0],
                        LengthProperty = 200 / ScaleProperty,
                        RealLength = 200 / ScaleProperty,
                        PathDataProperty =
                        SeletedXinhaoS ?
                         "1:1 0:#FFDC5625:#FF000000:M388,269 C394,269,399,264,399,258 C399,251,394,247,388,247 C381,247,376,251,376,258 C376,264,381,269,388,269 M347,242 L347,258 L376,258 M347,257 L347,270 M412,269 C418,269,423,264,423,258 C423,252,418,247,412,247 C405,247,400,252,400,258 C400,264,405,269,412,267 z" :
                        "1:1 0:#FFDC5625:#FF000000:M383,333 C377,333,371,328,371,322 C371,316,376,311,383,311 C389,310,394,315,394,321 C395,328,389,333,383,333 M424,337 L423,321 L394,322 M423,321 L423,308 M359,333 C352,333,347,328,347,322 C347,316,352,311,358,311 C365,311,370,316,370,322 C370,328,365,333,359,333 z",
                        PositionProperty = pos,
                        ScaleProperty = ScaleProperty,
                        SectionNumProperty = sec,
                        SelectedProperty = false,
                        StationNameProperty = SeletedXinhaoS ?
                        string.Format("{0}:{1}", "2", ChangeToOdd(pos, false).ToString()) :
                        string.Format("{0}:{1}", "2", ChangeToOdd(pos, true).ToString()),
                        Type = DataType.Single
                    };

                    newsdmqj = new StationDataMode()
                    {
                        HatProperty = parts[0].Split('+')[0],
                        LengthProperty = divide - 200 / ScaleProperty,
                        RealLength = divide,
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

                    if (sec != cdlxlist.Count() + 1)
                    {
                        parts = cdlxlist[sec - 1].Split(':');
                        if ((pos + divide * ScaleProperty / 1000) > float.Parse(parts[0].Split('+')[1]) / 1000)
                        {
                            offset = float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]);
                            sec += 1;
                            continue;
                        }
                    }
                    else
                        parts = cdlxlist[sec - 2].Split(':');


                    offset = 0;
                }
                _datascollection_CollectionChanged(null, null);
            }
            catch { }
        }

        private void AddXinhaoS(string taken, int numbers)
        {
            AddXinhaoX(taken, numbers);
        }

        private void moveXinhaoX(string taken, float offset, string newSignal = "")
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

                if (offset >= (currentqj.RealLength * ScaleProperty))
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
                    sdm.StationNameProperty = string.Format("{0}:{1}:{2}:{3}:{4}", parts[0], newSignal, parts[2], parts[3], parts[4]);
                }
                else if (parts[0].Equals("2"))
                {
                    sdm.StationNameProperty = SeletedXinhaoS ?
                        string.Format("{0}:{1}", sdm.StationNameProperty.Split(':')[0], ChangeToOdd(sdm.PositionProperty, false)) :
                        string.Format("{0}:{1}", sdm.StationNameProperty.Split(':')[0], ChangeToOdd(sdm.PositionProperty, true));
                }
                singledata.DataCollection.RemoveAt(index);
                singledata.DataCollection.Insert(index, sdm);
                currentqj.PositionProperty = sdm.PositionProperty;
                currentqj.LengthProperty = currentqj.LengthProperty - offset / ScaleProperty;
                currentqj.RealLength = currentqj.RealLength - offset / ScaleProperty;
                currentqj.StationNameProperty = "Q" + sectionum.ToString() + "+" + sdm.StationNameProperty;
                currentqj.SectionNumProperty = sectionum;
                singledata.DataCollection.RemoveAt(index + 1);
                singledata.DataCollection.Insert(index + 1, currentqj);
                preqj.LengthProperty = preqj.LengthProperty + offset / ScaleProperty;
                preqj.RealLength = preqj.RealLength + offset / ScaleProperty;
                //preqj.PositionProperty = sdm.PositionProperty;
                singledata.DataCollection.RemoveAt(index - 1);
                singledata.DataCollection.Insert(index - 1, preqj);

            }
            else
            {
                float additionlen = 200;
                if (prexh.StationNameProperty.Split(':')[0].Equals("3"))
                {
                    additionlen = prexh.RealLength;
                }
                if (-offset >= (preqj.LengthProperty * ScaleProperty + additionlen))
                {
                    throw new InvalidDataException("偏移过长");
                }

                for (int i = sdm.SectionNumProperty; i > preqj.SectionNumProperty; i--)
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
                    sdm.StationNameProperty = string.Format("{0}:{1}:{2}:{3}:{4}", parts[0], newSignal, parts[2], parts[3], parts[4]);
                }
                else if (parts[0].Equals("2"))
                {
                    sdm.StationNameProperty = SeletedXinhaoS ?
                        string.Format("{0}:{1}", sdm.StationNameProperty.Split(':')[0], ChangeToOdd(sdm.PositionProperty, false)) :
                        string.Format("{0}:{1}", sdm.StationNameProperty.Split(':')[0], ChangeToOdd(sdm.PositionProperty, true));
                }

                singledata.DataCollection.RemoveAt(index);
                singledata.DataCollection.Insert(index, sdm);
                currentqj.PositionProperty = sdm.PositionProperty;
                currentqj.LengthProperty = currentqj.LengthProperty - offset / ScaleProperty;
                currentqj.RealLength = currentqj.RealLength - offset / ScaleProperty;
                currentqj.StationNameProperty = "Q" + sectionum.ToString() + "+" + sdm.StationNameProperty;
                sdm.SectionNumProperty = sectionum;
                singledata.DataCollection.RemoveAt(index + 1);
                singledata.DataCollection.Insert(index + 1, currentqj);
                preqj.LengthProperty = preqj.LengthProperty + offset / ScaleProperty;
                preqj.RealLength = preqj.RealLength + offset / ScaleProperty;
                //preqj.PositionProperty = sdm.PositionProperty;
                singledata.DataCollection.RemoveAt(index - 1);
                singledata.DataCollection.Insert(index - 1, preqj);
            }
            _datascollection_CollectionChanged(null, null);
        }
        private void MoveSignals(float offset)
        {
            List<StationDataMode> signals = new List<StationDataMode>();
            List<IDataModel> SeletedSignals = CurrentDatasProperty.SelectedItems.ToList();

            if (offset < 0)
            {
                signals = SeletedSignals.Select(p => p as StationDataMode).ToList();                
            }
            else
            {
                SeletedSignals.Reverse();
                signals = SeletedSignals.Select(p => p as StationDataMode).ToList();
            }

            int error = signals.Count(p =>
            {
                string[] parts = p.StationNameProperty.Split(':');
                return parts[0].Equals("Q") || parts[0].Equals("1") || parts[0].Equals("3");
            });

            if (error > 0)
                return;

            foreach (StationDataMode item in signals)
            {
                moveXinhaoX(item.StationNameProperty, offset);
            }
        }

        private void MoveSignal()
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
                if (nextsdm.StationNameProperty.StartsWith("3"))
                {
                    newposition = float.Parse(nextsdm.StationNameProperty.Split(':')[2].Split('+')[0]);
                }
                else
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
        }

        private void MoveXinhaoS(string taken, float offset, string newSinge = "")
        {
            moveXinhaoX(taken, offset, newSinge);
        }

        private void BeginAdjustSignalsDis()
        {
            asdwindow = new AdjustSignalDisWindow();
            asdwindow.ShowDialog();
        }

        private void AdjustSignalsTosameDistence(float dis)
        {
            ISingleDataViewModel signaldatas;
            int index = 0;
            decimal gap = 0;
            StationDataMode nextsdm;
            StationDataMode currentQj;
            List<StationDataMode> adjustSignals = new List<StationDataMode>();
            List<IDataModel> SeletedQj = CurrentDatasProperty.SelectedItems.ToList();

            try
            {
                //选择上行
                if (!SeletedXinhaoS)
                {
                    signaldatas = DatasCollection.Single(p => p.TypeNum == (int)DataType.Single);
                    foreach (IDataModel item in SeletedQj)
                    {
                        index = signaldatas.DataCollection.IndexOf(item);
                        nextsdm = signaldatas.DataCollection[index - 1] as StationDataMode;
                        adjustSignals.Add(nextsdm as StationDataMode);
                    }

                    //adjustSignals.Reverse();
                    //SeletedQj.Reverse();
                    adjustSignals = adjustSignals.OrderByDescending(p => int.Parse(p.StationNameProperty.Split(':')[1])).ToList();
                    SeletedQj = SeletedQj.OrderByDescending(p => int.Parse((p as StationDataMode).StationNameProperty.Split(':')[1])).ToList();

                    for (int i = 0; i < adjustSignals.Count; i++)
                    {
                        currentQj = SeletedQj[i] as StationDataMode;
                        gap = decimal.Parse(currentQj.RealLength.ToString("#0.0")) * currentQj.ScaleProperty
                            - decimal.Parse(dis.ToString());
                        moveXinhaoX(adjustSignals[i].StationNameProperty, float.Parse(gap.ToString()));
                    }
                }
                else
                {
                    signaldatas = DatasCollection.Single(p => p.TypeNum == (int)DataType.SingleS);
                    foreach (IDataModel item in SeletedQj)
                    {
                        index = signaldatas.DataCollection.IndexOf(item);
                        nextsdm = signaldatas.DataCollection[index + 1] as StationDataMode;
                        adjustSignals.Add(nextsdm as StationDataMode);
                    }

                    adjustSignals = adjustSignals.OrderBy(p => int.Parse(p.StationNameProperty.Split(':')[1])).ToList();
                    SeletedQj = SeletedQj.OrderBy(p => int.Parse((p as StationDataMode).StationNameProperty.Split(':')[1])).ToList();

                    for (int i = 0; i < adjustSignals.Count; i++)
                    {
                        currentQj = SeletedQj[i] as StationDataMode;
                        gap = decimal.Parse(dis.ToString())
                            - decimal.Parse(currentQj.RealLength.ToString("#0.0")) * currentQj.ScaleProperty;
                        MoveXinhaoS(adjustSignals[i].StationNameProperty, float.Parse(gap.ToString()));
                    }
                }
                _datascollection_CollectionChanged(null, null);
            }
            catch (InvalidDataException ide)
            {
                MessageBox.Show(ide.Message, "错误", MessageBoxButton.OK);
            }
            catch
            {
                MessageBox.Show("调整错误", "错误", MessageBoxButton.OK);
            }
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
                    //长度不包括信号机所占位置(表现长度）
                    LengthProperty = ((pos - float.Parse(pdxlist[0].Qdglb)) * 1000 - offset)/ScaleProperty,
                    RealLength = ((pos - float.Parse(pdxlist[0].Qdglb)) * 1000 - offset) / ScaleProperty,
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
                        HatProperty = secnum == 1 ?
                                cdlxlist[0].Split(':')[0].Split('+')[0] :
                                secnum == cdlxlist.Count() + 1 ?
                                cdlxlist[secnum - 2].Split(':')[1].Split('+')[0] :
                                cdlxlist[secnum - 1].Split(':')[0].Split('+')[0],
                        LengthProperty = 200 / ScaleProperty,
                        RealLength = 200 / ScaleProperty,
                        PathDataProperty =
                          SeletedXinhaoS ?
                        "1:1 0:#FFDC5625:#FF000000:M388,269 C394,269,399,264,399,258 C399,251,394,247,388,247 C381,247,376,251,376,258 C376,264,381,269,388,269 M347,242 L347,258 L376,258 M347,257 L347,270 M412,269 C418,269,423,264,423,258 C423,252,418,247,412,247 C405,247,400,252,400,258 C400,264,405,269,412,267 z" :
                        "1:1 0:#FFDC5625:#FF000000:M383,333 C377,333,371,328,371,322 C371,316,376,311,383,311 C389,310,394,315,394,321 C395,328,389,333,383,333 M424,337 L423,321 L394,322 M423,321 L423,308 M359,333 C352,333,347,328,347,322 C347,316,352,311,358,311 C365,311,370,316,370,322 C370,328,365,333,359,333 z",
                        PositionProperty = pos,
                        ScaleProperty = ScaleProperty,
                        SectionNumProperty = secnum,
                        SelectedProperty = false,
                        StationNameProperty = SeletedXinhaoS ?
                        string.Format("{0}:{1}", singletype, ChangeToOdd(pos, false).ToString()) :
                        string.Format("{0}:{1}", singletype, ChangeToOdd(pos, true).ToString()),
                        Type = DataType.Single
                    };
                }
                else if (singletype.Equals("1"))
                {
                    stationsignalinfo.RealLength = stationsignalinfo.LengthProperty;
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
                    LengthProperty = ((float.Parse(pdxlist[pdxlist.Count - 1].Qdglb) - pos) * 1000
                    + float.Parse(pdxlist[pdxlist.Count - 1].Pc) - 200 - offset) / ScaleProperty,
                    RealLength = ((float.Parse(pdxlist[pdxlist.Count - 1].Qdglb) - pos) * 1000
                    + float.Parse(pdxlist[pdxlist.Count - 1].Pc) - offset) / ScaleProperty,
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
                    offset = 0;
                    //区间
                    StationDataMode currentsdm = singledata.DataCollection[i] as StationDataMode;
                    //下一架信号机
                    StationDataMode nextsdm = null;
                    //上一架信号机
                    StationDataMode prisdm = null;

                    if (i == singledata.DataCollection.Count - 1)
                        nextsdm = currentsdm;
                    else
                        nextsdm = singledata.DataCollection[i + 1] as StationDataMode;

                    if (currentsdm.SectionNumProperty != secnum)
                    {
                        for (int j = currentsdm.SectionNumProperty; j < secnum; j++)
                        {
                            parts = cdlxlist[j - 1].Split(':');
                            offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                        }
                    }
                    else
                    {
                        /*if (currentsdm.SectionNumProperty > cdlxlist.Count())
                            parts = cdlxlist[currentsdm.SectionNumProperty - 2].Split(':');
                        else
                            parts = cdlxlist[currentsdm.SectionNumProperty - 1].Split(':');*/
                        offset = 0;
                    }

                    if ((currentsdm.PositionProperty +
                        (currentsdm.RealLength * ScaleProperty) / 1000 + offset / 1000) > pos)
                    {
                        offset = 0;
                        if (currentsdm.SectionNumProperty != secnum)
                        {
                            for (int j = currentsdm.SectionNumProperty; j < secnum; j++)
                            {
                                parts = cdlxlist[j - 1].Split(':');
                                offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                            }

                            if (i != 0)
                            {
                                prisdm = singledata.DataCollection[i - 1] as StationDataMode;
                                len = currentsdm.RealLength -
                                 ((pos - currentsdm.PositionProperty) * 1000 - offset) / ScaleProperty;
                                currentsdm.LengthProperty = ((pos - currentsdm.PositionProperty) * 1000 - offset - prisdm.RealLength * ScaleProperty) / ScaleProperty;
                                currentsdm.RealLength = ((pos - currentsdm.PositionProperty) * 1000 - offset) / ScaleProperty;
                            }
                            else
                            {
                                len = currentsdm.LengthProperty -
                              ((pos - currentsdm.PositionProperty) * 1000 - offset) / ScaleProperty;
                                currentsdm.LengthProperty = ((pos - currentsdm.PositionProperty) * 1000 - offset) / ScaleProperty;
                                currentsdm.RealLength = ((pos - currentsdm.PositionProperty) * 1000 - offset) / ScaleProperty;

                            }
                        }
                        else
                        {
                            if (i != 0)
                            {
                                prisdm = singledata.DataCollection[i - 1] as StationDataMode;
                                len = currentsdm.LengthProperty + prisdm.RealLength - ((pos - currentsdm.PositionProperty) * 1000) / ScaleProperty;
                                currentsdm.LengthProperty = (pos - currentsdm.PositionProperty) * 1000 / ScaleProperty - prisdm.RealLength;
                                currentsdm.RealLength = ((pos - currentsdm.PositionProperty) * 1000) / ScaleProperty;
                            }
                            else
                            {
                                len = currentsdm.LengthProperty - ((pos - currentsdm.PositionProperty) * 1000) / ScaleProperty;
                                currentsdm.LengthProperty = ((pos - currentsdm.PositionProperty) * 1000) / ScaleProperty;
                                currentsdm.RealLength = ((pos - currentsdm.PositionProperty) * 1000) / ScaleProperty;
                            }
                        }

                        if (singletype.Equals("2"))
                        {
                            newxh = new StationDataMode()
                            {
                                HatProperty = secnum == 1 ?
                                cdlxlist[0].Split(':')[0].Split('+')[0] :
                                secnum == cdlxlist.Count() + 1 ?
                                cdlxlist[secnum - 2].Split(':')[1].Split('+')[0] :
                                cdlxlist[secnum - 1].Split(':')[0].Split('+')[0],
                                LengthProperty = 200 / ScaleProperty,
                                RealLength = 200 / ScaleProperty,
                                PathDataProperty = 
                                SeletedXinhaoS ?
                         "1:1 0:#FFDC5625:#FF000000:M388,269 C394,269,399,264,399,258 C399,251,394,247,388,247 C381,247,376,251,376,258 C376,264,381,269,388,269 M347,242 L347,258 L376,258 M347,257 L347,270 M412,269 C418,269,423,264,423,258 C423,252,418,247,412,247 C405,247,400,252,400,258 C400,264,405,269,412,267 z" :
                        "1:1 0:#FFDC5625:#FF000000:M383,333 C377,333,371,328,371,322 C371,316,376,311,383,311 C389,310,394,315,394,321 C395,328,389,333,383,333 M424,337 L423,321 L394,322 M423,321 L423,308 M359,333 C352,333,347,328,347,322 C347,316,352,311,358,311 C365,311,370,316,370,322 C370,328,365,333,359,333 z",
                                PositionProperty = pos,
                                ScaleProperty = ScaleProperty,
                                SectionNumProperty = secnum,
                                SelectedProperty = false,
                                StationNameProperty = SeletedXinhaoS ?
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
                            RealLength = len,
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
            _datascollection_CollectionChanged(null, null);
        }

        /// <summary>
        /// type:dfxname:leftsecnum + pos:middlesecnum + pos:rightsecnum + pos:len
        /// </summary>       
        private void InsertDianFXx(string ghs, StationDataMode DianFXinfo)
        {
            ISingleDataViewModel singledata;
            DianFXinfo.ScaleProperty = ScaleProperty;
            DianFXinfo.RealLength = DianFXinfo.LengthProperty = DianFXinfo.LengthProperty / ScaleProperty;

            if (!SeletedXinhaoS)
            {
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.Single);
                DianFXinfo.Type = DataType.Single;
            }
            else
            {
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.SingleS);
                DianFXinfo.Type = DataType.SingleS;
            }
                
            float offset = 0;            
            float len = 0;
            string[] parts = { };
            string[] dfxinfos = DianFXinfo.StationNameProperty.Split(':');

            StationDataMode newqj = null;
            StationDataMode currentsdm = null;
            StationDataMode nextsdm = null;
            StationDataMode prisdm = null;

            string[] ghlist = ghs.Split(':');
            int secnum = int.Parse(ghlist[0]);
            int currentsdmidex = 0;
            string identy = (CurrentDatasProperty.CurrentDataProperty as StationDataMode).StationNameProperty;
            float pos = float.Parse(dfxinfos[2].Split('+')[0]);

            if (singledata.DataCollection.Count != 0)
            {
                for (int i = 0; i < singledata.DataCollection.Count; i++)
                {
                    if ((singledata.DataCollection[i] as StationDataMode).StationNameProperty.Equals(identy))
                    {
                        currentsdm = singledata.DataCollection[i] as StationDataMode;
                        if (i == singledata.DataCollection.Count - 1)
                            nextsdm = currentsdm;
                        else
                            nextsdm = singledata.DataCollection[i + 1] as StationDataMode;
                        prisdm = singledata.DataCollection[i - 1] as StationDataMode;
                        currentsdmidex = i;
                        break;
                    }
                }

                if (currentsdm.SectionNumProperty != int.Parse(ghlist[0]))
                {
                    for (int j = currentsdm.SectionNumProperty; j < int.Parse(ghlist[0]); j++)
                    {
                        parts = cdlxlist[j - 1].Split(':');
                        offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                    }
                }
                else
                {
                    if (currentsdm.SectionNumProperty > cdlxlist.Count())
                        parts = cdlxlist[currentsdm.SectionNumProperty - 2].Split(':');
                    else
                        parts = cdlxlist[currentsdm.SectionNumProperty - 1].Split(':');
                    offset = 0;
                }
                //错误位置
                if ((currentsdm.PositionProperty +
                        (currentsdm.RealLength * ScaleProperty) / 1000 + offset / 1000) > pos)
                {
                    if (offset != 0)
                    {
                        len = currentsdm.LengthProperty + prisdm.RealLength -
                            ((pos - currentsdm.PositionProperty) * 1000 - offset) / ScaleProperty;

                        currentsdm.LengthProperty = ((pos - currentsdm.PositionProperty) * 1000 - offset) / ScaleProperty - prisdm.RealLength;
                        currentsdm.RealLength = ((pos - currentsdm.PositionProperty) * 1000 - offset) / ScaleProperty;
                    }
                    else
                    {
                        len = currentsdm.LengthProperty + prisdm.RealLength -
                       ((pos - currentsdm.PositionProperty) * 1000) / ScaleProperty;
                        currentsdm.LengthProperty = ((pos - currentsdm.PositionProperty) * 1000) / ScaleProperty - prisdm.RealLength;
                        currentsdm.RealLength = ((pos - currentsdm.PositionProperty) * 1000) / ScaleProperty;
                    }

                    newqj = new StationDataMode()
                    {
                        HatProperty = DianFXinfo.HatProperty,
                        LengthProperty = len - float.Parse(dfxinfos[5]) / ScaleProperty,
                        RealLength = len,
                        PathDataProperty = "5:6 2 1 2:#00DC5625:#FF000000:M0,0 L50,0",
                        PositionProperty = float.Parse(dfxinfos[2].Split('+')[0]),
                        ScaleProperty = ScaleProperty,
                        SectionNumProperty = int.Parse(dfxinfos[2].Split('+')[1]),
                        SelectedProperty = false,
                        StationNameProperty = "Q" + int.Parse(dfxinfos[4].Split('+')[1]) + "+" + DianFXinfo.StationNameProperty,
                        Type = DataType.Single
                    };

                    singledata.DataCollection.RemoveAt(currentsdmidex);
                    singledata.DataCollection.Insert(currentsdmidex, currentsdm);

                    singledata.DataCollection.Insert(currentsdmidex + 1, DianFXinfo);
                    singledata.DataCollection.Insert(currentsdmidex + 2, newqj);
                }
                _datascollection_CollectionChanged(null, null);
            }
        }

        private void beginInsertDianfx()
        {
            StationDataMode sdm = CurrentDatasProperty.CurrentDataProperty as StationDataMode;
            int index = CurrentDatasProperty.DataCollection.IndexOf(sdm);

            float startpos = 0;
            int startsecnum = 0;
            float endpos = 0;
            int endsecnum = 0;

            if (index != 0 && index != CurrentDatasProperty.DataCollection.Count - 1)
            {
                StationDataMode presdm = CurrentDatasProperty.DataCollection[index - 1] as StationDataMode;
                if (presdm.StationNameProperty.StartsWith("3"))
                {
                    startpos = float.Parse(presdm.StationNameProperty.Split(':')[4].Split('+')[0]);
                    startsecnum = int.Parse(presdm.StationNameProperty.Split(':')[4].Split('+')[1]);
                }
                else
                {
                    startpos = presdm.PositionProperty;
                    startsecnum = presdm.SectionNumProperty;
                }

                StationDataMode nextsdm = CurrentDatasProperty.DataCollection[index + 1] as StationDataMode;
                endpos = nextsdm.PositionProperty;
                endsecnum = nextsdm.SectionNumProperty;


                dianfxwindow = new DianFXWindow();
                //类型：电分相名称：无电区左边缘（路段号+里程）：无电区中心（路段号+里程）：无电区右边缘（路段号+里程）：无电区长度
                string dfxinfosstring = string.Format("{0}:{1}:{2}+{3}:{4}+{5}:{6}+{7}:{8}", "3", "无名",
                    startpos.ToString("F3"), startsecnum.ToString(),
                    startpos.ToString("F3"), startsecnum.ToString(),
                    startpos.ToString("F3"), startsecnum.ToString(),
                    sdm.LengthProperty.ToString("F3")
                    );

       
        MessengerInstance.Send<DianFXneededInfosMode>(new DianFXneededInfosMode()
                {
                    CdlListProperty = cdlxlist,
                    DfxInfosProperty = dfxinfosstring,
                    //（路段号+里程）
                    LeftPosProperty = string.Format("{0}+{1}", startsecnum.ToString(), startpos.ToString("F3")),
                    RightPosProperty = string.Format("{0}+{1}", endsecnum.ToString(), endpos.ToString("F3")),
                    IsUpdataProperty = false,
                    DerictionProperty = SeletedXinhaoS ? 1 : 0
                },
                "DfxInputInfos");

                dianfxwindow.ShowDialog();
            }
        }

        private void beginModifyDianfx()
        {
            try
            {
                StationDataMode sdm = CurrentDatasProperty.CurrentDataProperty as StationDataMode;
                int index = CurrentDatasProperty.DataCollection.IndexOf(sdm);
                if (index == -1)
                {
                    MessageBox.Show("未选中对象！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }               
                _takenOfcurrentDFX = sdm.StationNameProperty;

                StationDataMode presdm = CurrentDatasProperty.DataCollection[index - 2] as StationDataMode;
                float startpos = 0;
                int startsecnum = 0;
                if (presdm.StationNameProperty.StartsWith("3"))
                {
                    startpos = float.Parse(presdm.StationNameProperty.Split(':')[4].Split('+')[0]);
                    startsecnum = int.Parse(presdm.StationNameProperty.Split(':')[4].Split('+')[1]);
                }
                else
                {
                    startpos = presdm.PositionProperty;
                    startsecnum = presdm.SectionNumProperty;
                }

                StationDataMode nextsdm = CurrentDatasProperty.DataCollection[index + 2] as StationDataMode;
                float endpos = nextsdm.PositionProperty;
                int endsecnum = nextsdm.SectionNumProperty;

                dianfxwindow = new DianFXWindow();
                string[] parts = sdm.StationNameProperty.Split(':');
                string dfxinfosstring = string.Format("{0}:{1}:{2}+{3}:{4}+{5}:{6}+{7}:{8}", "3", parts[1],
                   parts[2].Split('+')[0], parts[2].Split('+')[1],
                   parts[3].Split('+')[0], parts[3].Split('+')[1],
                   parts[4].Split('+')[0], parts[4].Split('+')[1],
                   sdm.LengthProperty.ToString("F3")
                   );
                MessengerInstance.Send<DianFXneededInfosMode>(new DianFXneededInfosMode()
                {
                    CdlListProperty = cdlxlist,
                    DfxInfosProperty = dfxinfosstring,
                    LeftPosProperty = string.Format("{0}+{1}", startsecnum.ToString(), startpos.ToString("F3")),
                    RightPosProperty = string.Format("{0}+{1}", endsecnum.ToString(), endpos.ToString("F3")),
                    IsUpdataProperty = true,
                    DerictionProperty = SeletedXinhaoS ? 1 : 0
                },
                "DfxInputInfos");

                dianfxwindow.ShowDialog();
            }
            catch
            {
                MessageBox.Show("操作出错！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdataDianfx(string ghs, StationDataMode DianFXinfo)
        {
            int idx = 0;
            ISingleDataViewModel singledata;

            if (!SeletedXinhaoS)
            {
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.Single);
                DianFXinfo.Type = DataType.Single;
            }
            else
            {
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.SingleS);
                DianFXinfo.Type = DataType.SingleS;
            }

            if (!string.IsNullOrEmpty(_takenOfcurrentDFX))
            {
                idx = DeleteDianFXx(_takenOfcurrentDFX);
                CurrentDatasProperty.CurrentDataProperty = singledata.DataCollection[idx];
                InsertDianFXx(ghs, DianFXinfo);
            }
        }
        
        private int DeleteDianFXx(string taken) 
        {
            ISingleDataViewModel singledata;
            string[] dfxinfos = taken.Split(':');

            if (!SeletedXinhaoS)
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.Single);
            else
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.SingleS);

            StationDataMode sdm = (StationDataMode)singledata.DataCollection.
                Single(q => ((StationDataMode)q).StationNameProperty.Equals(taken));

            int index = singledata.DataCollection.IndexOf(sdm);

            StationDataMode preqj = singledata.DataCollection[index - 1] as StationDataMode;
            StationDataMode currentqj = singledata.DataCollection[index + 1] as StationDataMode;

            preqj.LengthProperty = preqj.LengthProperty + currentqj.LengthProperty + float.Parse(dfxinfos[5]) / ScaleProperty;
            preqj.RealLength = preqj.RealLength + currentqj.RealLength;

            singledata.DataCollection.RemoveAt(index - 1);
            singledata.DataCollection.Insert(index - 1, preqj);
            singledata.DataCollection.RemoveAt(index);
            singledata.DataCollection.RemoveAt(index);
            _takenOfcurrentDFX = string.Empty;
            _datascollection_CollectionChanged(null, null);
            return index - 1;
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
            preqj.RealLength = preqj.RealLength + currentqj.RealLength;

            singledata.DataCollection.RemoveAt(index - 1);
            singledata.DataCollection.Insert(index - 1, preqj);
            singledata.DataCollection.RemoveAt(index);
            singledata.DataCollection.RemoveAt(index);
            _datascollection_CollectionChanged(null, null);
        }

        private void ModifyStationSignal()
        {
            ISingleDataViewModel singledata;

            try
            {
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
                                        RealLength = s.RealLength,
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
                                        RealLength = s.RealLength,
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
                                    //"1:1 0:#FFDC5625:#FF000000:M371,143 C361,143,353,136,353,126 C353,117,361,109,371,109 C381,109,389,117,389,126 C389,136,381,143,371,143 M418,127 L388,127 M418,149 L418,105"
                                    //"1:1 0:#FFDC5625:#FF000000:M400,208 C409,208,418,200,418,191 C418,181,409,174,400,174 C390,174,382,181,382,191 C382,200,390,208,400,208 M353,191 L383,191 M353,171 L353,212
                                    HatProperty = item.Gh,
                                    StationNameProperty = string.Format("{0}:{1}:{2}:{3}:{4}",
                                    "1", string.Empty, item.Bjsj, "J", FormatStationPosition(item.Bjsj)),
                                    LengthProperty = 200 / ScaleProperty,
                                    RealLength = 200 / ScaleProperty,
                                    PathDataProperty =
                                    SeletedXinhaoS ?
                        "1:1 0:#FFDC5625:#FF000000:M400,208 C409,208,418,200,418,191 C418,181,409,174,400,174 C390,174,382,181,382,191 C382,200,390,208,400,208 M353,191 L383,191 M353,171 L353,212" :
                        "1:1 0:#FFDC5625:#FF000000:M371,143 C361,143,353,136,353,126 C353,117,361,109,371,109 C381,109,389,117,389,126 C389,136,381,143,371,143 M418,127 L388,127 M418,149 L418,105",
                                    PositionProperty = -1.0f,
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
                                    RealLength = 200 / ScaleProperty,
                                    PathDataProperty =
                                    SeletedXinhaoS ?
                        "1:1 0:#FFDC5625:#FF000000:M400,208 C409,208,418,200,418,191 C418,181,409,174,400,174 C390,174,382,181,382,191 C382,200,390,208,400,208 M353,191 L383,191 M353,171 L353,212" :
                        "1:1 0:#FFDC5625:#FF000000:M371,143 C361,143,353,136,353,126 C353,117,361,109,371,109 C381,109,389,117,389,126 C389,136,381,143,371,143 M418,127 L388,127 M418,149 L418,105",
                                    PositionProperty = -1.0f,
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
                                    RealLength = 200 / ScaleProperty,
                                    PathDataProperty =
                                    SeletedXinhaoS ?
                        "1:1 0:#FFDC5625:#FF000000:M400,208 C409,208,418,200,418,191 C418,181,409,174,400,174 C390,174,382,181,382,191 C382,200,390,208,400,208 M353,191 L383,191 M353,171 L353,212" :
                        "1:1 0:#FFDC5625:#FF000000:M371,143 C361,143,353,136,353,126 C353,117,361,109,371,109 C381,109,389,117,389,126 C389,136,381,143,371,143 M418,127 L388,127 M418,149 L418,105",
                                    PositionProperty = -1.0f,
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
                                    RealLength = 200 / ScaleProperty,
                                    PathDataProperty =
                                    SeletedXinhaoS ?
                       "1:1 0:#FFDC5625:#FF000000:M400,208 C409,208,418,200,418,191 C418,181,409,174,400,174 C390,174,382,181,382,191 C382,200,390,208,400,208 M353,191 L383,191 M353,171 L353,212" :
                        "1:1 0:#FFDC5625:#FF000000:M371,143 C361,143,353,136,353,126 C353,117,361,109,371,109 C381,109,389,117,389,126 C389,136,381,143,371,143 M418,127 L388,127 M418,149 L418,105",
                                    PositionProperty = -1.0f,
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
                    if (!SeletedXinhaoS)
                        stationSignallist.Reverse();

                    MessengerInstance.Send<List<StationDataMode>>(stationSignallist, "stationsignal");
                    Swindow.ShowDialog();
                }
            }

            catch(System.InvalidOperationException ioe)
            {
                MessengerInstance.Send<string>("读入信号数据出错", "ReadDataError");
            }

            catch (SatitonNotLoadedException sne)
            {
                MessengerInstance.Send<string>(sne.Message, "ReadDataError");
            }

            catch
            {
                MessengerInstance.Send<string>("读入信号数据出错", "ReadDataError");
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

                    if (Math.Abs(offset) > 1)
                        moveXinhaoX(matchedmode.StationNameProperty, offset, parts[1]);
                }
                catch(System.InvalidOperationException ioe)
                {
                    InsertXinhaoX(item.PositionProperty, item.SectionNumProperty, "1", item);
                }
            }
        }

        private void ExportSignalData()
        {
            SignalExportData sig_data_s = new SignalExportData();
            SignalExportData sig_data_x = new SignalExportData();
            ISingleDataViewModel singledata;
            StationDataMode currentxinhao = null;
            StationDataMode currentqj = null;
            StationDataMode nextxinhao = null;
            StationDataMode next2xinhao = null;
            StationDataMode nextqj = null;
            float offset = 0;
            string[] parts;
            string dfxinfo;
            Thread exportAsoneThread;

            try
            {
                //输出上行
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.Single);

                if (singledata.DataCollection.Count > 1)
                {
                    

                    for (int i = 1; i < singledata.DataCollection.Count - 1; i += 2)
                    {
                        currentxinhao = singledata.DataCollection[i] as StationDataMode;
                        if (currentxinhao.StationNameProperty.Split(':')[0].Equals("3"))
                            continue;

                        offset = 0;
                        sig_data_s.IDProperty.Add(currentxinhao.StationNameProperty.Split(':')[1]);
                        sig_data_s.HatProperty.Add(currentxinhao.HatProperty);

                        if (currentxinhao.StationNameProperty.Split(':')[0].Equals("1"))
                        {
                            sig_data_s.StationNameProperty.Add(
                                currentxinhao.StationNameProperty.Split(':')[2]);
                        }
                        else
                        {
                            sig_data_s.StationNameProperty.Add(string.Empty);
                        }

                        if (i + 1 == singledata.DataCollection.Count - 1)
                        {
                            sig_data_s.StartPosProperty = singledata.DataCollection[i].PositionProperty * 1000;
                            break;
                        }                            

                        currentqj = singledata.DataCollection[i + 1] as StationDataMode;
                        nextxinhao = singledata.DataCollection[i + 2] as StationDataMode;

                        if (!nextxinhao.StationNameProperty.Split(':')[0].Equals("3"))
                        {
                            sig_data_s.DistenceProperty.Add((currentqj.RealLength) * currentqj.ScaleProperty);

                            if (currentxinhao.SectionNumProperty == nextxinhao.SectionNumProperty)
                            {
                                sig_data_s.GapProperty.Add(0);
                            }
                            else
                            {
                                for (int j = currentxinhao.SectionNumProperty; j < nextxinhao.SectionNumProperty; j++)
                                {
                                    parts = cdlxlist[j - 1].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                }
                                sig_data_s.GapProperty.Add(offset);
                            }

                            sig_data_s.DianFxInfoProperty.Add(string.Empty);
                        }
                        else
                        {
                            nextqj =singledata.DataCollection[i + 3] as StationDataMode;
                            sig_data_s.DistenceProperty.Add((currentqj.RealLength + nextqj.RealLength) * currentqj.ScaleProperty);
                            dfxinfo = nextxinhao.StationNameProperty;

                            if (currentxinhao.SectionNumProperty.ToString() == dfxinfo.Split(':')[2].Split('+')[1])
                            {
                                dfxinfo += ":0";
                            }
                            else
                            {
                                for (int j = currentxinhao.SectionNumProperty; j < int.Parse(dfxinfo.Split(':')[2].Split('+')[1]); j++)
                                {
                                    parts = cdlxlist[j - 1].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                }
                                dfxinfo += (":" + (offset).ToString());
                            }

                            next2xinhao = singledata.DataCollection[i + 4] as StationDataMode;
                            if (next2xinhao.SectionNumProperty.ToString() == dfxinfo.Split(':')[2].Split('+')[1])
                            {
                                dfxinfo += ":0";
                            }
                            else
                            {
                                for (int j = int.Parse(dfxinfo.Split(':')[2].Split('+')[1]); j < next2xinhao.SectionNumProperty; j++)
                                {
                                    parts = cdlxlist[j - 1].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                }
                                dfxinfo += (":" + (offset).ToString());
                            }

                            offset = 0;
                            if (currentxinhao.SectionNumProperty == next2xinhao.SectionNumProperty)
                            {
                                sig_data_s.GapProperty.Add(0);
                            }
                            else
                            {
                                for (int j = currentxinhao.SectionNumProperty; j < next2xinhao.SectionNumProperty; j++)
                                {
                                    parts = cdlxlist[j - 1].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                }
                                sig_data_s.GapProperty.Add(offset);
                            }

                            sig_data_s.DianFxInfoProperty.Add(dfxinfo);
                        }                        
                    }
                }

                //输出下行
                offset = 0;
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.SingleS);

                if (singledata.DataCollection.Count > 1)
                {
                    sig_data_x.StartPosProperty = singledata.DataCollection[1].PositionProperty * 1000;

                    for (int i = 1; i < singledata.DataCollection.Count - 1; i += 2)
                    {
                        currentxinhao = singledata.DataCollection[i] as StationDataMode;
                        if (currentxinhao.StationNameProperty.Split(':')[0].Equals("3"))
                            continue;

                        offset = 0;
                        sig_data_x.IDProperty.Add(currentxinhao.StationNameProperty.Split(':')[1]);
                        sig_data_x.HatProperty.Add(currentxinhao.HatProperty);

                        if (currentxinhao.StationNameProperty.Split(':')[0].Equals("1"))
                        {
                            sig_data_x.StationNameProperty.Add(
                                currentxinhao.StationNameProperty.Split(':')[2]);
                        }
                        else
                        {
                            sig_data_x.StationNameProperty.Add(string.Empty);
                        }

                        if (i + 1 == singledata.DataCollection.Count - 1)
                            break;

                        currentqj = singledata.DataCollection[i + 1] as StationDataMode;
                        nextxinhao = singledata.DataCollection[i + 2] as StationDataMode;

                        if (!nextxinhao.StationNameProperty.Split(':')[0].Equals("3"))
                        {
                            sig_data_x.DistenceProperty.Add((currentqj.RealLength) * currentqj.ScaleProperty);
                            if (currentxinhao.SectionNumProperty == nextxinhao.SectionNumProperty)
                            {
                                sig_data_x.GapProperty.Add(0);
                            }
                            else
                            {
                                for (int j = currentxinhao.SectionNumProperty; j < nextxinhao.SectionNumProperty; j++)
                                {
                                    parts = cdlxlist[j - 1].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                }
                                sig_data_x.GapProperty.Add(offset);
                            }

                            sig_data_x.DianFxInfoProperty.Add(string.Empty);
                        }
                        else
                        {
                            nextqj = singledata.DataCollection[i + 3] as StationDataMode;
                            sig_data_x.DistenceProperty.Add((currentqj.RealLength + nextqj.RealLength) * currentqj.ScaleProperty);
                            dfxinfo = nextxinhao.StationNameProperty;

                            if (currentxinhao.SectionNumProperty.ToString() == dfxinfo.Split(':')[2].Split('+')[1])
                            {
                                dfxinfo += ":0";
                            }
                            else
                            {
                                for (int j = currentxinhao.SectionNumProperty; j < int.Parse(dfxinfo.Split(':')[2].Split('+')[1]); j++)
                                {
                                    parts = cdlxlist[j - 1].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                }
                                dfxinfo += (":" + offset.ToString());
                            }

                            next2xinhao = singledata.DataCollection[i + 4] as StationDataMode;
                            if (next2xinhao.SectionNumProperty.ToString() == dfxinfo.Split(':')[2].Split('+')[1])
                            {
                                dfxinfo += ":0";
                            }
                            else
                            {
                                for (int j = int.Parse(dfxinfo.Split(':')[2].Split('+')[1]); j < next2xinhao.SectionNumProperty; j++)
                                {
                                    parts = cdlxlist[j - 1].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                }
                                dfxinfo += (":" + offset.ToString());
                            }

                            offset = 0;
                            if (currentxinhao.SectionNumProperty == next2xinhao.SectionNumProperty)
                            {
                                sig_data_x.GapProperty.Add(0);
                            }
                            else
                            {
                                for (int j = currentxinhao.SectionNumProperty; j < next2xinhao.SectionNumProperty; j++)
                                {
                                    parts = cdlxlist[j - 1].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                }
                                sig_data_x.GapProperty.Add(offset);
                            }

                            sig_data_x.DianFxInfoProperty.Add(dfxinfo);
                        }                       
                    }
                }

                sfwindow = new SaveFileDialog();
                //sfwindow.CheckFileExists = true;
                sfwindow.AddExtension = true;
                sfwindow.Title = "保存文件";
                sfwindow.Filter = "xls files (*.xls)|*.xls|All files (*.*)|*.*";
                if (sfwindow.ShowDialog() == true)
                {
                    exportAsoneThread = new Thread(() =>
                    {
                        SDexportor.ExportDataAsOne(sig_data_s, sig_data_x, sfwindow.FileName);
                        CloseProcessWindow();
                    });

                    exportAsoneThread.Start();
                    ShowProcessWindow();                   
                }
            }

            catch (System.InvalidOperationException ioe)
            {                
                MessengerInstance.Send<string>("上下行信号机数据不完整", "ReadDataError");
            }

            catch
            {                
                MessengerInstance.Send<string>("输出信号机数据出错", "ReadDataError");
            }
        }

        private void ExportSignalDataSparately()
        {
            SignalExportData sig_data_s = null;
            SignalExportData sig_data_x = new SignalExportData();
            List<SignalExportData> datas_s = new List<SignalExportData>();
            List<SignalExportData> datas_x = new List<SignalExportData>();

            ISingleDataViewModel singledata;
            StationDataMode currentxinhao = null;
            StationDataMode currentqj = null;
            StationDataMode nextxinhao = null;
            StationDataMode next2xinhao = null;
            StationDataMode nextqj = null;
            string stationame = string.Empty;
            float offset = 0;
            string[] parts;
            string dfxinfo = string.Empty;
            Thread exportsparatelyThread;

            try
            {
                //输出上行
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.Single);

                if (singledata.DataCollection.Count > 1)
                {
                    for (int i = singledata.DataCollection.IndexOf(
                        singledata.DataCollection.First(
                        p => (p as StationDataMode).StationNameProperty.Split(':')[0].Equals("1")));
                        i < singledata.DataCollection.Count - 1; i += 2)
                    {
                        currentxinhao = singledata.DataCollection[i] as StationDataMode;
                        parts = currentxinhao.StationNameProperty.Split(':');

                        if (parts[0].Equals("3"))
                            continue;

                        offset = 0;                        

                        if (parts[0].Equals("1"))
                        {
                            if (!parts[2].Equals(stationame))
                            {
                                if (stationame.Equals(string.Empty))
                                {
                                    sig_data_s = new SignalExportData();
                                    stationame = parts[2];
                                    //sig_data_s.StartPosProperty = currentxinhao.PositionProperty * 1000;
                                }
                                else
                                {
                                    stationame = string.Empty;
                                    sig_data_s.StartPosProperty= currentxinhao.PositionProperty * 1000;
                                    sig_data_s.IDProperty.Add(parts[1]);
                                    sig_data_s.HatProperty.Add(currentxinhao.HatProperty);
                                    sig_data_s.StationNameProperty.Add(parts[2]);
                                    datas_s.Add(sig_data_s);
                                    continue;
                                }
                            }
                        }

                        sig_data_s.IDProperty.Add(parts[1]);
                        sig_data_s.HatProperty.Add(currentxinhao.HatProperty);
                        if (parts[0].Equals("1"))
                        {                            
                            sig_data_s.StationNameProperty.Add(parts[2]);
                        }
                        else
                        {
                            sig_data_s.StationNameProperty.Add(string.Empty);
                        }
                        
                        if (i + 1 == singledata.DataCollection.Count - 1)
                            break;

                        currentqj = singledata.DataCollection[i + 1] as StationDataMode;
                        nextxinhao = singledata.DataCollection[i + 2] as StationDataMode;

                        if (!nextxinhao.StationNameProperty.Split(':')[0].Equals("3"))
                        {
                            sig_data_s.DistenceProperty.Add((currentqj.RealLength) * currentqj.ScaleProperty);

                            if (currentxinhao.SectionNumProperty == nextxinhao.SectionNumProperty)
                            {
                                sig_data_s.GapProperty.Add(0);
                            }
                            else
                            {
                                for (int j = currentxinhao.SectionNumProperty; j < nextxinhao.SectionNumProperty; j++)
                                {
                                    parts = cdlxlist[j - 1].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                }
                                sig_data_s.GapProperty.Add(offset);
                            }

                            sig_data_s.DianFxInfoProperty.Add(string.Empty);
                        }
                        else
                        {
                            nextqj = singledata.DataCollection[i + 3] as StationDataMode;
                            sig_data_s.DistenceProperty.Add((currentqj.RealLength + nextqj.RealLength) * currentqj.ScaleProperty);
                            dfxinfo = nextxinhao.StationNameProperty;

                            if (currentxinhao.SectionNumProperty.ToString() == dfxinfo.Split(':')[2].Split('+')[1])
                            {
                                dfxinfo += ":0";
                            }
                            else
                            {
                                for (int j = currentxinhao.SectionNumProperty; j < int.Parse(dfxinfo.Split(':')[2].Split('+')[1]); j++)
                                {
                                    parts = cdlxlist[j - 1].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                }
                                dfxinfo += (":" + offset.ToString());
                            }

                            next2xinhao = singledata.DataCollection[i + 4] as StationDataMode;
                            if (next2xinhao.SectionNumProperty.ToString() == dfxinfo.Split(':')[2].Split('+')[1])
                            {
                                dfxinfo += ":0";
                            }
                            else
                            {
                                for (int j = int.Parse(dfxinfo.Split(':')[2].Split('+')[1]); j < next2xinhao.SectionNumProperty; j++)
                                {
                                    parts = cdlxlist[j - 1].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                }
                                dfxinfo += (":" + offset.ToString());
                            }

                            offset = 0;
                            if (currentxinhao.SectionNumProperty == next2xinhao.SectionNumProperty)
                            {
                                sig_data_s.GapProperty.Add(0);
                            }
                            else
                            {
                                for (int j = currentxinhao.SectionNumProperty; j < next2xinhao.SectionNumProperty; j++)
                                {
                                    parts = cdlxlist[j - 1].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                }
                                sig_data_s.GapProperty.Add(offset);
                            }

                            sig_data_s.DianFxInfoProperty.Add(dfxinfo);
                        }                        
                    }
                }

                offset = 0;
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.SingleS);

                if (singledata.DataCollection.Count > 1)
                {
                    for (int i = singledata.DataCollection.IndexOf(
                        singledata.DataCollection.First(
                        p => (p as StationDataMode).StationNameProperty.Split(':')[0].Equals("1")));
                        i < singledata.DataCollection.Count - 1; i += 2)
                    {
                        currentxinhao = singledata.DataCollection[i] as StationDataMode;
                        parts = currentxinhao.StationNameProperty.Split(':');
                        if (parts[0].Equals("3"))
                            continue;

                        offset = 0;
                       
                        if (parts[0].Equals("1"))
                        {
                            if (!parts[2].Equals(stationame))
                            {
                                if (stationame.Equals(string.Empty))
                                {
                                    sig_data_x = new SignalExportData();
                                    stationame = parts[2];
                                    sig_data_x.StartPosProperty = currentxinhao.PositionProperty * 1000;
                                }
                                else
                                {
                                    stationame = string.Empty;
                                    sig_data_x.IDProperty.Add(parts[1]);
                                    sig_data_x.HatProperty.Add(currentxinhao.HatProperty);
                                    sig_data_x.StationNameProperty.Add(parts[2]);
                                    datas_x.Add(sig_data_x);
                                    continue;
                                }
                            }
                        }

                        sig_data_x.IDProperty.Add(parts[1]);
                        sig_data_x.HatProperty.Add(currentxinhao.HatProperty);

                        if (parts[0].Equals("1"))
                        {                            
                            sig_data_x.StationNameProperty.Add(parts[2]);
                        }
                        else
                        {
                            sig_data_x.StationNameProperty.Add(string.Empty);
                        }

                        if (i + 1 == singledata.DataCollection.Count - 1)
                            break;

                        currentqj = singledata.DataCollection[i + 1] as StationDataMode;
                        nextxinhao = singledata.DataCollection[i + 2] as StationDataMode;

                        if (!nextxinhao.StationNameProperty.Split(':')[0].Equals("3"))
                        {
                            sig_data_x.DistenceProperty.Add((currentqj.RealLength) * currentqj.ScaleProperty);

                            if (currentxinhao.SectionNumProperty == nextxinhao.SectionNumProperty)
                            {
                                sig_data_x.GapProperty.Add(0);
                            }
                            else
                            {
                                for (int j = currentxinhao.SectionNumProperty; j < nextxinhao.SectionNumProperty; j++)
                                {
                                    parts = cdlxlist[j - 1].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                }
                                sig_data_x.GapProperty.Add(offset);
                            }

                            sig_data_x.DianFxInfoProperty.Add(string.Empty);
                        }
                        else
                        {
                            nextqj = singledata.DataCollection[i + 3] as StationDataMode;
                            sig_data_x.DistenceProperty.Add((currentqj.RealLength + nextqj.RealLength) * currentqj.ScaleProperty);
                            dfxinfo = nextxinhao.StationNameProperty;

                            if (currentxinhao.SectionNumProperty.ToString() == dfxinfo.Split(':')[2].Split('+')[1])
                            {
                                dfxinfo += ":0";
                            }
                            else
                            {
                                for (int j = currentxinhao.SectionNumProperty; j < int.Parse(dfxinfo.Split(':')[2].Split('+')[1]); j++)
                                {
                                    parts = cdlxlist[j - 1].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                }
                                dfxinfo += (":" + offset.ToString());
                            }

                            next2xinhao = singledata.DataCollection[i + 4] as StationDataMode;
                            if (next2xinhao.SectionNumProperty.ToString() == dfxinfo.Split(':')[2].Split('+')[1])
                            {
                                dfxinfo += ":0";
                            }
                            else
                            {
                                for (int j = int.Parse(dfxinfo.Split(':')[2].Split('+')[1]); j < next2xinhao.SectionNumProperty; j++)
                                {
                                    parts = cdlxlist[j - 1].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                }
                                dfxinfo += (":" + offset.ToString());
                            }

                            next2xinhao = singledata.DataCollection[i + 4] as StationDataMode;
                            if (next2xinhao.SectionNumProperty.ToString() == dfxinfo.Split(':')[2].Split('+')[1])
                            {
                                dfxinfo += ":0";
                            }
                            else
                            {
                                for (int j = int.Parse(dfxinfo.Split(':')[2].Split('+')[1]); j < next2xinhao.SectionNumProperty; j++)
                                {
                                    parts = cdlxlist[j - 1].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                }
                                dfxinfo += (":" + offset.ToString());
                            }

                            offset = 0;
                            if (currentxinhao.SectionNumProperty == next2xinhao.SectionNumProperty)
                            {
                                sig_data_x.GapProperty.Add(0);
                            }
                            else
                            {
                                for (int j = currentxinhao.SectionNumProperty; j < next2xinhao.SectionNumProperty; j++)
                                {
                                    parts = cdlxlist[j - 1].Split(':');
                                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                                }
                                sig_data_x.GapProperty.Add(offset);
                            }

                            sig_data_x.DianFxInfoProperty.Add(dfxinfo);
                        }                        
                    }
                }

                sfwindow = new SaveFileDialog();
                //sfwindow.CheckFileExists = true;
                sfwindow.AddExtension = true;
                sfwindow.Title = "保存文件";
                sfwindow.Filter = "xls files (*.xls)|*.xls|All files (*.*)|*.*";
                if (sfwindow.ShowDialog() == true)
                {
                    exportsparatelyThread = new Thread(() =>
                    {
                        SDexportor.ExportDataSeparately(datas_s, datas_x, sfwindow.FileName);
                        CloseProcessWindow();
                    });
                    exportsparatelyThread.Start();
                    ShowProcessWindow();                   
                }
            }

            catch (System.InvalidOperationException ioe)
            {
                MessengerInstance.Send<string>("上下行信号机数据不完整", "ReadDataError");
            }

            catch
            {
                MessengerInstance.Send<string>("输出信号机数据出错", "ReadDataError");
            }
        }

        private void SaveXhData()
        {
            ISingleDataViewModel singledata;
            List<ChangeToTxt.CheZhanOutputData> xhs = new List<ChangeToTxt.CheZhanOutputData>();
            List<ChangeToTxt.CheZhanOutputData> xhx = new List<ChangeToTxt.CheZhanOutputData>();
            string[] parts;
            string bjData = string.Empty;
            Cursor = 1;

            try
            {
                
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.SingleS);
                foreach (StationDataMode item in singledata.DataCollection.ToArray())
                {
                    parts = item.StationNameProperty.Split(':');
                    if (!parts[0].StartsWith("Q"))
                    {
                        switch (parts[0])
                        {
                            case "1":
                                bjData = string.Format("{0}:{1}:{2}", parts[1], parts[2], parts[3]);
                                break;
                            case "2":
                                bjData = parts[1];
                                break;
                            case "3":
                                //电分相名称：无电区左边缘（路段号+里程）：无电区中心（路段号+里程）：无电区右边缘（路段号+里程）：无电区长度
                                bjData = string.Format("{0}:{1}:{2}:{3}:{4}", parts[1], parts[2], parts[3], parts[4], parts[5]);
                                break;
                            default:
                                break;
                        }

                        xhs.Add(new ChangeToTxt.CheZhanOutputData()
                        {
                            Bjlx = parts[0],
                            Bjsj = bjData,
                            Gh = item.HatProperty,
                            Glb = item.PositionProperty.ToString("F3"),
                            Index = string.Empty,
                            Ldh = item.SectionNumProperty.ToString(),
                            Zjfx = "1"
                        });
                    }
                }
                //Doper.SaveXhData(xhs, xhx);
                IsXinhaoChanged = false;
            }

            catch
            {
                MessengerInstance.Send<string>("下行信号机数据不可见，保存出错", "ReadDataError");
                return;
            }

            finally
            {
                Cursor = 0;
            }

            bjData = string.Empty;
            try
            {
                
                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.Single);
                foreach (StationDataMode item in singledata.DataCollection.ToArray())
                {
                    parts = item.StationNameProperty.Split(':');
                    if (!parts[0].StartsWith("Q"))
                    {
                        switch (parts[0])
                        {
                            case "1":
                                bjData = string.Format("{0}:{1}:{2}", parts[1], parts[2], parts[3]);
                                break;
                            case "2":
                                bjData = parts[1];
                                break;
                            case "3":
                                bjData = string.Format("{0}:{1}:{2}:{3}:{4}", parts[1], parts[2], parts[3], parts[4], parts[5]);
                                break;
                            default:
                                break;
                        }
                        
                        xhx.Add(new ChangeToTxt.CheZhanOutputData()
                        {
                            Bjlx = parts[0],
                            Bjsj = bjData,
                            Gh = item.HatProperty,
                            Glb = item.PositionProperty.ToString("F3"),
                            Index = string.Empty,
                            Ldh = item.SectionNumProperty.ToString(),
                            Zjfx = "1"
                        });
                    }
                }

                GDoper.SaveXhdataCompleted += GDoper_SaveXhdataCompleted;
                if (_xhdataPath.Equals(string.Empty))
                {
                    SaveFileDialog savexhdataDialog = new SaveFileDialog();
                    savexhdataDialog.Title = "保存信号数据";
                    savexhdataDialog.Filter = "信号数据|*.xh";

                    if (savexhdataDialog.ShowDialog() == true)
                    {
                        _xhdataPath = savexhdataDialog.FileName;
                        Thread savexhThread = new Thread(() =>
                        {
                            GDoper.SaveXhData(xhs, xhx, _xhdataPath);
                            IsXinhaoChanged = false;                            
                        });
                        savexhThread.Start();
                        ShowProcessWindow();
                    }
                }
                else
                {
                    string backupfile = Path.Combine(Path.GetDirectoryName(_xhdataPath),
                                                             Path.GetFileNameWithoutExtension(_xhdataPath) +
                                                             ".xhbackup");
                    File.Delete(backupfile);
                    Thread savexhThread = new Thread(() =>
                        {
                            GDoper.SaveXhData(xhs, xhx, _xhdataPath);
                            IsXinhaoChanged = false;                            
                        });
                    savexhThread.Start();
                    ShowProcessWindow();
                }
            }

            catch 
            {
                MessengerInstance.Send<string>("上行信号机数据不可见，保存出错", "ReadDataError");
            }

            Cursor = 0;
        }

        private void GDoper_SaveXhdataCompleted(object sender, EventArgs e)
        {
            CloseProcessWindow();
        }

        private void saveOtherXhData()
        {
            ISingleDataViewModel singledata;
            List<ChangeToTxt.CheZhanOutputData> xhs = new List<ChangeToTxt.CheZhanOutputData>();
            List<ChangeToTxt.CheZhanOutputData> xhx = new List<ChangeToTxt.CheZhanOutputData>();
            string[] parts;
            string bjData = string.Empty;
            Cursor = 1;
            Thread saveOtherxhdataTread;

            try
            {

                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.SingleS);
                foreach (StationDataMode item in singledata.DataCollection.ToArray())
                {
                    parts = item.StationNameProperty.Split(':');
                    if (!parts[0].StartsWith("Q"))
                    {
                        switch (parts[0])
                        {
                            case "1":
                                bjData = string.Format("{0}:{1}:{2}", parts[1], parts[2], parts[3]);
                                break;
                            case "2":
                                bjData = parts[1];
                                break;
                            case "3":
                                //电分相名称：无电区左边缘（路段号+里程）：无电区中心（路段号+里程）：无电区右边缘（路段号+里程）：无电区长度
                                bjData = string.Format("{0}:{1}:{2}:{3}:{4}", parts[1], parts[2], parts[3], parts[4], parts[5]);
                                break;
                            default:
                                break;
                        }

                        xhs.Add(new ChangeToTxt.CheZhanOutputData()
                        {
                            Bjlx = parts[0],
                            Bjsj = bjData,
                            Gh = item.HatProperty,
                            Glb = item.PositionProperty.ToString("F3"),
                            Index = string.Empty,
                            Ldh = item.SectionNumProperty.ToString(),
                            Zjfx = "1"
                        });
                    }
                }
                //Doper.SaveXhData(xhs, xhx);
                IsXinhaoChanged = false;
            }

            catch
            {
                MessengerInstance.Send<string>("下行信号机数据不可见，保存出错", "ReadDataError");
                return;
            }

            bjData = string.Empty;
            try
            {

                singledata = DatasCollection.Single(p => p.TypeNum == (int)DataType.Single);
                foreach (StationDataMode item in singledata.DataCollection.ToArray())
                {
                    parts = item.StationNameProperty.Split(':');
                    if (!parts[0].StartsWith("Q"))
                    {
                        switch (parts[0])
                        {
                            case "1":
                                bjData = string.Format("{0}:{1}:{2}", parts[1], parts[2], parts[3]);
                                break;
                            case "2":
                                bjData = parts[1];
                                break;
                            case "3":
                                bjData = string.Format("{0}:{1}:{2}:{3}:{4}", parts[1], parts[2], parts[3], parts[4], parts[5]);
                                break;
                            default:
                                break;
                        }

                        xhx.Add(new ChangeToTxt.CheZhanOutputData()
                        {
                            Bjlx = parts[0],
                            Bjsj = bjData,
                            Gh = item.HatProperty,
                            Glb = item.PositionProperty.ToString("F3"),
                            Index = string.Empty,
                            Ldh = item.SectionNumProperty.ToString(),
                            Zjfx = "1"
                        });
                    }
                }

                if (!_xhdataPath.Equals(string.Empty))
                {
                    string backupfile = Path.Combine(Path.GetDirectoryName(_xhdataPath),
                                                            Path.GetFileNameWithoutExtension(_xhdataPath) +
                                                            "_backup" + Path.GetExtension(_xhdataPath));
                    File.Delete(backupfile);
                }

                SaveFileDialog savexhdataDialog = new SaveFileDialog();
                savexhdataDialog.Title = "保存信号数据";
                savexhdataDialog.Filter = "信号数据|*.xh";

                if (savexhdataDialog.ShowDialog() == true)
                {
                    _xhdataPath = savexhdataDialog.FileName;
                    saveOtherxhdataTread = new Thread(() =>
                    {
                        GDoper.SaveXhData(xhs, xhx, _xhdataPath);
                        IsXinhaoChanged = false;
                        CloseProcessWindow();
                    });
                    saveOtherxhdataTread.Start();
                    ShowProcessWindow();
                }
            }

            catch
            {
                MessengerInstance.Send<string>("上行信号机数据不可见，保存出错", "ReadDataError");
            }

            Cursor = 0;
        }

        private void openXhdata()
        {
            OpenFileDialog openxhdataDialog;
            Thread openxhThread;

            if (hadanyXhdata())
            {
                if (MessageBox.Show("未保存信号数据将会丢失，是否继续？", "提示",
                       MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                {
                    cleanXhdata();
                    openxhdataDialog = new OpenFileDialog();
                    openxhdataDialog.Title = "打开信号数据";
                    openxhdataDialog.Filter = "信号数据文件|*.xh|备放文件|*.xhbackup|所有文件|*.*";
                    openxhdataDialog.Multiselect = false;

                    if (openxhdataDialog.ShowDialog() == true)
                    {
                        try
                        {
                            Cursor = 1;
                            if (Path.GetExtension(openxhdataDialog.FileName).Equals(".xh"))
                            {
                                _xhdataPath = openxhdataDialog.FileName;
                            }
                            else if (Path.GetExtension(openxhdataDialog.FileName).Equals(".xhbackup"))
                            {
                                _xhdataPath = Path.Combine(Path.GetDirectoryName(openxhdataDialog.FileName),
                                                      Path.GetFileNameWithoutExtension(openxhdataDialog.FileName) + ".xh");
                                _xhdataPath = _xhdataPath.TrimEnd(' ');
                                File.Delete(_xhdataPath);
                                File.Copy(openxhdataDialog.FileName, _xhdataPath);
                                File.Delete(openxhdataDialog.FileName);

                            }

                            openxhThread = new Thread(() =>
                            {
                                GDoper.OpenXhData(_xhdataPath);
                                CanloadxhProperty = true;
                                CloseProcessWindow();
                            });

                            openxhThread.Start();
                            ShowProcessWindow();
                        }
                        catch (ExtractData.WorkSheetBase.WorksheetNotOnlyException wne)
                        {
                            MessengerInstance.Send<string>("读入信号机数据出错", "ReadDataError");
                        }
                        finally
                        {
                            Cursor = 0;
                        }
                    }
                    return;
                }
                return;
            }

            openxhdataDialog = new OpenFileDialog();
            openxhdataDialog.Title = "打开信号数据";
            openxhdataDialog.Filter = "信号数据文件|*.xh|备放文件|*.xhbackup|所有文件|*.*";
            openxhdataDialog.Multiselect = false;

            if (openxhdataDialog.ShowDialog() == true)
            {
                try
                {
                    Cursor = 1;
                    if (Path.GetExtension(openxhdataDialog.FileName).Equals(".xh"))
                    {
                        _xhdataPath = openxhdataDialog.FileName;
                    }
                    else if (Path.GetExtension(openxhdataDialog.FileName).Equals(".xhbackup"))
                    {
                        _xhdataPath = Path.Combine(Path.GetDirectoryName(openxhdataDialog.FileName),
                                              Path.GetFileNameWithoutExtension(openxhdataDialog.FileName) + ".xh");
                        _xhdataPath= _xhdataPath.TrimEnd(' ');
                        File.Delete(_xhdataPath);
                        File.Copy(openxhdataDialog.FileName, _xhdataPath);
                        File.Delete(openxhdataDialog.FileName);

                    }

                    openxhThread = new Thread(() =>
                    {
                        GDoper.OpenXhData(_xhdataPath);
                        CanloadxhProperty = true;
                        CloseProcessWindow();
                    });

                    openxhThread.Start();
                    ShowProcessWindow();
                }
                catch (ExtractData.WorkSheetBase.WorksheetNotOnlyException wne)
                {
                    CanloadxhProperty = false;
                    MessengerInstance.Send<string>("读入信号机数据出错", "ReadDataError");
                }
                finally
                {
                    Cursor = 0;
                }
            }           

        }

        private void creatNewxhdata()
        {
            try
            {
                if (hadanyXhdata())
                {
                    if (MessageBox.Show("未保存信号数据将会丢失，是否继续？", "提示", 
                        MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                    {
                        cleanXhdata();
                        CanloadxhProperty = true;
                        return;
                    }                    
                }
                CanloadxhProperty = true;
            }
            catch
            {
                CanloadxhProperty = false;
            }
        }

        private void ModifyCdldata()
        {
            mcdwindown = new ModifyCdldataWindow();
            MessengerInstance.Send<GraphyDataOper>(gdo, "gdoInWindow");
            mcdwindown.ShowDialog();
        }

        private void Autofit()
        {
            Cursor = 1;

            if (gdo.WorkbookCountProperty == 0)
            {
                try
                {
                    string[] cdldata = GDoper.GetCdlData(Path.Combine(Environment.CurrentDirectory, @"excelmodels\接坡面数据.xlsx"));
                    System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
                    ABoper.Autofit(cdldata);
                    watch.Stop();
                    MessengerInstance.Send<string>(string.Format("自动调整完成！\r\n共用时{0}秒",
                        watch.Elapsed.Seconds.ToString()), "ReadDataRight");
                }

                catch (ExtractData.AutoBuildOperator.ErrorPosException epe)
                {                   
                    MessengerInstance.Send(
                       string.Format("{0}|{1}", epe.Message, epe.ErrorPosMessageProperty), "FixErrorWithOperate");
                }

                catch (ExtractData.AutoBuildOperator.ErrorMatchException eme)
                {
                    MessengerInstance.Send<string>(
                        string.Format("{0}|{1}", eme.Message, eme.ErrorPosMessageProperty), "ReadDataErrorWithOperate");
                }

                catch (ExtractData.AutoBuildOperator.ErrorDistencException ede)
                {
                    MessengerInstance.Send<string>(
                        string.Format("{0}|{1}", ede.Message, ede.ErrorPosMessageProperty), "ReadDataErrorWithOperate");
                }

                catch(System.Exception ex)
                {
                    MessengerInstance.Send<string>(
                        string.Format("{0}|{1}", ex.Message, ex.Source), "ReadDataErrorWithOperate");
                }
                Cursor = 0;
            }
            else
            {
                Cursor = 0;
                MessengerInstance.Send<string>("请关闭所有相关工作表再进行自动调整！", "ReadDataError");
            }
        }

        private void SingleClick()
        {
            exPortTypeIndex = 1;
        }

        private void ManyClick()
        {
            exPortTypeIndex = 0;
        }

        private void Dispose()
        {
            if (_xhdataPath != string.Empty)
            {
                string backupfile = Path.Combine(Path.GetDirectoryName(_xhdataPath),
                                                             Path.GetFileNameWithoutExtension(_xhdataPath) +
                                                             ".xhbackup");
                if (File.Exists(backupfile))
                {
                    if (MessageBox.Show("是否保存对信号数据的修改？", "提示", MessageBoxButton.YesNo, 
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SaveXhData();
                    }
                    else
                    {
                        File.Delete(backupfile);
                    }
                }
            }

            GDoper.Dispose();
            SDexportor.Dispose();
            ABoper.Dispose();
        }

        private void ExportToSvg()
        {
            ExportToSVG ets = new ExportToSVG();
            List<LineData> ld = new List<LineData>();
            List<CurveData> cd = new List<CurveData>();
            List<LineData> posd = new List<LineData>();
            List<StationData> sd = new List<StationData>();
            List<SignalData> sds = new List<SignalData>();
            List<SignalData> sdx = new List<SignalData>();

            sfwindow = new SaveFileDialog();
            //sfwindow.CheckFileExists = true;
            sfwindow.AddExtension = true;
            sfwindow.Title = "输出图形";
            sfwindow.Filter = "svg files (*.svg)|*.svg|All files (*.*)|*.*";
            if (sfwindow.ShowDialog() == true)
            {
                Cursor = 1;
                try
                {
                    ets.CreatSvg(sfwindow.FileName);
                    ISingleDataViewModel pddata = _datascollection.SingleOrDefault<ISingleDataViewModel>(p => p.TypeNameProperty.Equals("坡度"));
                    if (pddata != null)
                    {
                        if (pddata.ShowDataProperty)
                        {
                            foreach (LineDataModel item in pddata.DataCollection.Select(q => q as LineDataModel))
                            {
                                ld.Add(new LineData()
                                {
                                    AngleProperty = item.AngleProperty,
                                    EndPositionProperty = item.EndPositionProperty,
                                    HatProperty = item.HatProperty,
                                    HeightProperty = item.HeightProperty,
                                    LengthProperty = item.LengthProperty,
                                    PathDataProperty = item.PathDataProperty,
                                    PositionProperty = item.PositionProperty,
                                    RadioProperty = item.RadioProperty,
                                    ScaleProperty = item.ScaleProperty,
                                    SectionNumProperty = item.SectionNumProperty
                                });
                            }
                            ets.ExportLineData(ld, "green", "3", 200, 100);
                        }
                    }


                    ISingleDataViewModel qxdata = _datascollection.SingleOrDefault<ISingleDataViewModel>(p => p.TypeNameProperty.Equals("曲线"));
                    if (qxdata != null)
                    {
                        if (qxdata.ShowDataProperty)
                        {
                            foreach (LineDataModel item in qxdata.DataCollection.Select(q => q as LineDataModel))
                            {
                                cd.Add(new CurveData()
                                {
                                    AngleProperty = item.AngleProperty,
                                    EndPositionProperty = item.EndPositionProperty,
                                    HatProperty = item.HatProperty,
                                    HeightProperty = item.HeightProperty,
                                    LengthProperty = item.LengthProperty,
                                    PathDataProperty = item.PathDataProperty,
                                    PositionProperty = item.PositionProperty,
                                    RadioProperty = item.RadioProperty,
                                    ScaleProperty = item.ScaleProperty,
                                    SectionNumProperty = item.SectionNumProperty
                                });
                            }
                            ets.ExportCurveData(cd, "green", "3", 270, 100);
                        }
                    }

                    ISingleDataViewModel lcdata = _datascollection.SingleOrDefault<ISingleDataViewModel>(p => p.TypeNameProperty.Equals("里程"));
                    if (lcdata != null)
                    {
                        if (lcdata.ShowDataProperty)
                        {
                            foreach (StationDataMode item in lcdata.DataCollection.Select(q => q as StationDataMode))
                            {
                                posd.Add(new LineData()
                                {
                                    AngleProperty = 0,
                                    EndPositionProperty = 0,
                                    HatProperty = item.HatProperty,
                                    HeightProperty = 0,
                                    LengthProperty = item.LengthProperty,
                                    PathDataProperty = string.Empty,
                                    PositionProperty = item.PositionProperty,
                                    RadioProperty = 0,
                                    ScaleProperty = item.ScaleProperty,
                                    SectionNumProperty = item.SectionNumProperty
                                });
                            }
                            ets.ExportPosData(posd, "black", "3", 360, 100);
                        }
                    }

                    ISingleDataViewModel czdata = _datascollection.SingleOrDefault<ISingleDataViewModel>(p => p.TypeNameProperty.Equals("车站"));
                    if (czdata != null)
                    {
                        if (czdata.ShowDataProperty)
                        {
                            foreach (StationDataMode item in czdata.DataCollection.Select(q => q as StationDataMode))
                            {
                                sd.Add(new StationData()
                                {
                                    HatProperty = item.HatProperty,
                                    LengthProperty = item.LengthProperty,
                                    PathDataProperty = item.PathDataProperty,
                                    PositionProperty = item.PositionProperty,
                                    ScaleProperty = item.ScaleProperty,
                                    SectionNumProperty = item.SectionNumProperty,
                                    StationNameProperty = item.StationNameProperty
                                });
                            }
                            ets.ExportStationData(sd, "black", "2", 15, 100, 100);
                        }
                    }

                    ISingleDataViewModel xhsdata = _datascollection.SingleOrDefault<ISingleDataViewModel>(p => p.TypeNameProperty.Equals("信号机(S)"));
                    if (xhsdata != null)
                    {
                        if (xhsdata.ShowDataProperty)
                        {
                            foreach (StationDataMode item in xhsdata.DataCollection.Select(q => q as StationDataMode))
                            {
                                sds.Add(new SignalData()
                                {
                                    HatProperty = item.HatProperty,
                                    LengthProperty = item.RealLength,
                                    PathDataProperty = item.PathDataProperty,
                                    PositionProperty = item.PositionProperty,
                                    ScaleProperty = item.ScaleProperty,
                                    SectionNumProperty = item.SectionNumProperty,
                                    StationNameProperty = item.StationNameProperty
                                });
                            }
                            ets.ExportSignalData(sds, 0, 420, 100);
                        }
                    }

                    ISingleDataViewModel xhxdata = _datascollection.SingleOrDefault<ISingleDataViewModel>(p => p.TypeNameProperty.Equals("信号机(X)"));
                    if (xhxdata != null)
                    {
                        if (xhxdata.ShowDataProperty)
                        {
                            foreach (StationDataMode item in xhxdata.DataCollection.Select(q => q as StationDataMode))
                            {
                                sdx.Add(new SignalData()
                                {
                                    HatProperty = item.HatProperty,
                                    LengthProperty = item.RealLength,
                                    PathDataProperty = item.PathDataProperty,
                                    PositionProperty = item.PositionProperty,
                                    ScaleProperty = item.ScaleProperty,
                                    SectionNumProperty = item.SectionNumProperty,
                                    StationNameProperty = item.StationNameProperty
                                });
                            }
                            ets.ExportSignalData(sdx, 1, 480, 100);
                        }
                    }
                    ets.SaveSvg();
                    Cursor = 0;
                    MessengerInstance.Send<string>("输出图形完成！", "ReadDataRight");
                }
                catch
                {
                    MessengerInstance.Send<string>("输出图形出错！", "ReadDataError");
                }
            }
        }

        private void showRightDialog()
        {
            if (CurrentDatasProperty.CurrentDataProperty != null)
            {
                StationDataMode sdm = CurrentDatasProperty.CurrentDataProperty as StationDataMode;
                if (sdm != null)
                {
                    if (sdm.Type == DataType.Single || sdm.Type == DataType.SingleS)
                    {
                        if (sdm.StationNameProperty.Split(':')[0].Equals("2"))
                        {
                            MoveSignal();
                        }
                        else if (sdm.StationNameProperty.Split(':')[0].Equals("3"))
                        {
                            beginModifyDianfx();
                        }
                    }
                }
            }           
           
        }

        private void ExportToOtFormat()
        {
            sfwindow = new SaveFileDialog();
            sfwindow.Filter = "All files (*.*)|*.*";
            sfwindow.Title = "输出文件";
            if (sfwindow.ShowDialog() == true)
            {
                try
                {
                    Cursor = 1;
                    GDoper.GetPoDuData(out pdxlist, out pdslist);
                    cdlxlist = pdxlist.TakeWhile(p => p.Cdl != "+:+").Select(q => q.Cdl).ToList();
                    if (cdlxlist.Count == 0)
                    {
                        cdlxlist.Add("0+0:0+0");
                    }
                    GDoper.GetQuxianData(out qxxlist, out qxslist);
                    GDoper.GetBjData(out czxlist, out czslist);

                    ChangeToTxt ctt = new ChangeToTxt();

                    ctt.ChangeToOtFile(qxxlist, pdxlist, czxlist, cdlxlist.ToArray(), sfwindow.FileName);
                    Cursor = 0;
                    MessengerInstance.Send<string>("输出OT文件完成！", "ReadDataRight");
                }
                catch
                {
                    MessengerInstance.Send<string>("输出OT文件失败！", "ReadDataError");
                }
            }            
            
        }
        private bool hadanyXhdata()
        {
            foreach (ISingleDataViewModel Singleitem in _DataBin)
            {
                if (Singleitem.TypeNum == (int)DataType.Single)
                {
                    return true;
                }
                if (Singleitem.TypeNum == (int)DataType.SingleS)
                {
                    return true;
                }
            }
            return CanloadxhProperty || false;
        }
        private void cleanXhdata()
        {
            SingleDataViewModel sdvm = null;

            foreach (ISingleDataViewModel Singleitem in _DataBin)
            {
                if (Singleitem.TypeNum == (int)DataType.Single)
                {
                    sdvm = (SingleDataViewModel)Singleitem;                    
                    break;
                }
            }
            if (sdvm != null)
            {
                _DataBin.Remove(sdvm);                
            }

            sdvm = null;
            foreach (ISingleDataViewModel Singleitem in _datascollection)
            {
                if (Singleitem.TypeNum == (int)DataType.Single)
                {
                    sdvm = (SingleDataViewModel)Singleitem;
                    break;
                }
            }
            if (sdvm != null)
            {
                _datascollection.Remove(sdvm);
                IsXinhaoLoadedProperty = false;
            }
           
            sdvm = null;
            foreach (ISingleDataViewModel Singleitem in _DataBin)
            {
                if (Singleitem.TypeNum == (int)DataType.SingleS)
                {
                    sdvm = (SingleDataViewModel)Singleitem;
                    break;
                }
            }
            if (sdvm != null)
            {
                _DataBin.Remove(sdvm);                
            }

            sdvm = null;
            foreach (ISingleDataViewModel Singleitem in _datascollection)
            {
                if (Singleitem.TypeNum == (int)DataType.SingleS)
                {
                    sdvm = (SingleDataViewModel)Singleitem;
                    break;
                }
            }
            if (sdvm != null)
            {
                _datascollection.Remove(sdvm);
                IsXinhaoSLoaded = false;
            }

            IniSheets.CleanXhDataFile();
            
        }

        private string getcurrentpos(string pos, int secnum)
        {
            float startpos = float.Parse(pdxlist[0].Qdglb) * 1000;
            float offset = 0;
            string[] parts = null;

            for (int i = 1; i < secnum; i++)
            {
                parts = cdlxlist[i - 1].Split(':');
                offset += (float.Parse(parts[0].Split('+')[1]) - float.Parse(parts[1].Split('+')[1]));
            }

            float realcurrentpos = (float.Parse(pos) * 1000 - startpos) + offset;
            return realcurrentpos.ToString("#0.000");
        }

        private ControlCenter formatMu(string filepath, float protectdis, int tk,int oriv)
        {
            Com_LocoModel cl = new Com_LocoModel();

            cl.LoadMUfromFilepath(filepath);
            List<ILoco_model> im = new List<ILoco_model>();
            im.Add(cl);

            ControlCenter cc = new ControlCenter(im, new List<ITrain_model>(), 2);
            cc.ProtectDis = protectdis;
            cc.TK = tk;
            cc.Ypsilon = cl.Ypsilon;           
            cc.Group.Path = filepath;
            cc.OriSpeed = oriv;            

            return cc;
        }

        private void editTrain()
        {
            tiWindow = new TrainInfoWindow();
            tiWindow.ShowDialog();
        }

        private void calculetUmBreakDis()
        {
            gbdWindow = new GetBreakDisWindow();

            if (_contrCen.Group.Locomotives.Count == 0)
            {
                MessengerInstance.Send<string>(string.Empty, "iniTrain");
            }
            else
            {
                MessengerInstance.Send<string>(string.Format("{0}|{1}|{2}|{3}|{4}",
                    ((MUGroup)(_contrCen.Group)).TrainName,
                    _contrCen.Group.Path,
                    _contrCen.ProtectDis.ToString(),
                    _contrCen.TK.ToString(),
                    _contrCen.OriSpeed.ToString()),
                    "iniTrain");
            }
            gbdWindow.Show();
        }               
        
        private void calculetedis(string info)
        {
            string[] add = info.Split(':');
            int containsignals = 0;
            float d = 0;

            try
            {
                if (_contrCen != null)
                {
                    ChangeToTxt ctt = new ChangeToTxt();

                    if (lineparts == null)
                    {
                        lineparts = ctt.ChangeToSimFormat(qxxlist, pdxlist, czxlist, cdlxlist.ToArray());                        
                    }

                    _contrCen.FormatLinepartStruct(lineparts);
                    StationDataMode sdm = CurrentDatasProperty.CurrentDataProperty as StationDataMode;
                    if (sdm.Type != DataType.Single && sdm.Type != DataType.SingleS)
                    {
                        MessengerInstance.Send("未选择信号机", "reciveDisinfos");
                        return;
                    }

                    string[] parts = sdm.StationNameProperty.Split(':');
                    if (parts[0].StartsWith("Q") || parts[0].Equals("3"))
                    {
                        MessengerInstance.Send("未选择信号机", "reciveDisinfos");
                        return;
                    }
                        
                    string crp = getcurrentpos(sdm.PositionProperty.ToString("#0.00"), sdm.SectionNumProperty);

                    _contrCen.ProtectDis = float.Parse(add[0]);
                    _contrCen.TK = float.Parse(add[1]);
                    _contrCen.ExportDetail = add[2].Equals("1") ? true : false;
                    _contrCen.OriSpeed = int.Parse(add[3]);

                    decimal dis = _contrCen.CalculateDisWithEndpos(int.Parse(Math.Round(float.Parse(crp)).ToString()), _contrCen.OriSpeed, Direction);
                    int currentindex = CurrentDatasProperty.DataCollection.IndexOf(sdm);

                    if (Direction == 0)
                    {
                        for (int i = currentindex; i < CurrentDatasProperty.DataCollection.Count; i++)
                        {
                            sdm = CurrentDatasProperty.DataCollection[i] as StationDataMode;
                            parts = sdm.StationNameProperty.Split(':');

                            if ((parts[0].StartsWith("1") || parts[0].StartsWith("2")))
                            {
                                containsignals++;
                                continue;
                            }

                            if (parts[0].StartsWith("Q") || parts[0].Equals("3"))
                            {
                                d += (sdm.RealLength * sdm.ScaleProperty);
                            }

                            if (d > (float)dis)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = currentindex; i >= 0; i--)
                        {
                            sdm = CurrentDatasProperty.DataCollection[i] as StationDataMode;
                            parts = sdm.StationNameProperty.Split(':');

                            if ((parts[0].StartsWith("1") || parts[0].StartsWith("2")))
                            {
                                containsignals++;
                                continue;
                            }

                            if (parts[0].StartsWith("Q") || parts[0].Equals("3"))
                            {
                                d += (sdm.RealLength * sdm.ScaleProperty);
                            }

                            if (d > (float)dis)
                            {
                                break;
                            }
                        }
                    }

                    string resultText = string.Format("制动距离(m)：{0}\r\n制动时间(s):{1}\r\n平均坡度(‰):{2}\r\n共包含{3}个闭塞分区\r\n",
                        dis.ToString("F3"),
                        _contrCen.TotalBreakTime.ToString("F0"),
                        _contrCen.AverHeightProperty.ToString("F3"),
                        containsignals.ToString());

                    if (add[2].Equals("1"))
                    {
                        if (File.Exists("result.txt"))
                            File.Delete("result.txt");
                        File.AppendAllText("result.txt", _contrCen.process);
                    }

                    MessengerInstance.Send(resultText, "reciveDisinfos");
                }
            }
            catch(ControlCenter.OverlineRangeException olre)
            {
                MessengerInstance.Send(olre.Message, "reciveDisinfos");
            }
            catch(NullReferenceException nre)
            {
                MessengerInstance.Send("无线路数据", "reciveDisinfos");
            }
            catch(ControlCenter.OverMaxSpeedException omse)
            {
                MessengerInstance.Send(omse.Message, "reciveDisinfos");
            }
            catch
            {
                MessengerInstance.Send("计算制动距离出错", "reciveDisinfos");
            }           
            
        }

        private void test() { }

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
        private RelayCommand _loadldhCommand;

        /// <summary>
        /// Gets the LoadLdhCommand.
        /// </summary>
        public RelayCommand LoadLdhCommand
        {
            get
            {
                return _loadldhCommand
                    ?? (_loadldhCommand = new RelayCommand(
                    () =>
                    {
                        LoadLdhMap();
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
                                                  if (sdm.SectionNumProperty != cdlxlist.Count() + 1)
                                                  {
                                                      divinfos = divinfos + "," + string.Format("{0}:{1}",
                                                          cdlxlist[sdm.SectionNumProperty - 1].Split(':')[0],
                                                          cdlxlist[sdm.SectionNumProperty - 1].Split(':')[0]);
                                                  }
                                                  else
                                                  {
                                                      divinfos = divinfos + "," + string.Format("{0}:{1}",
                                                         cdlxlist[sdm.SectionNumProperty - 2].Split(':')[0],
                                                         cdlxlist[sdm.SectionNumProperty - 2].Split(':')[0]);
                                                  }
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
                                                  "ResourcesQJ");
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
                                              if (!MoveXinHaoCommand.CanExecute(null))
                                              {
                                                  return;
                                              }
                                              MoveSignal();                                              
                                          },
                                          () => 
                                          {
                                              try
                                              {
                                                  return CurrentDatasProperty.SelectedItems.Count == 1;
                                              }
                                              catch
                                              {
                                                  return false;
                                              }
                                          }));
            }
        }
       
        private RelayCommand _movesignalsCommand;

        /// <summary>
        /// Gets the MoveSignalsCommand.
        /// </summary>
        public RelayCommand MoveSignalsCommand
        {
            get
            {
                return _movesignalsCommand
                    ?? (_movesignalsCommand = new RelayCommand(
                    () =>
                    {
                        MoveSignals(MoveingDistence);
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
                                              string[] stationNames = CurrentDatasProperty.SelectedItems.
                                                                       Select(p => p as StationDataMode).
                                                                       Select(p => p.StationNameProperty).ToArray();
                                              //StationDataMode sdm = CurrentDatasProperty.CurrentDataProperty as StationDataMode;
                                              foreach (string item in stationNames)
                                              {
                                                  DeleteXinhao(item);
                                              }

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

        private RelayCommand _exportsignaldataCommand;

        /// <summary>
        /// Gets the ExportSignalDataCommand.
        /// </summary>
        public RelayCommand ExportSignalDataCommand
        {
            get
            {
                return _exportsignaldataCommand
                    ?? (_exportsignaldataCommand = new RelayCommand(
                                          () =>
                                          {
                                              if (exPortTypeIndex == 1)
                                                  ExportSignalData();
                                              else
                                                  ExportSignalDataSparately();
                                          }));
            }
        }

        private RelayCommand _exportsignaldataseparatelyCommand;

        /// <summary>
        /// Gets the ExportSignalDataSeparatelyCommand.
        /// </summary>
        public RelayCommand ExportSignalDataSeparatelyCommand
        {
            get
            {
                return _exportsignaldataseparatelyCommand
                    ?? (_exportsignaldataseparatelyCommand = new RelayCommand(
                                          () =>
                                          {
                                              ExportSignalDataSparately();
                                          }));
            }
        }
        private RelayCommand _savexhdataCommand;

        /// <summary>
        /// Gets the SaveXhDataCommand.
        /// </summary>
        public RelayCommand SaveXhDataCommand
        {
            get
            {
                return _savexhdataCommand
                    ?? (_savexhdataCommand = new RelayCommand(
                                          () =>
                                          {
                                              SaveXhData();
                                          }));
            }
        }

        private RelayCommand _modifycdldataCommand;

        /// <summary>
        /// Gets the ModifycdlDataCommand.
        /// </summary>
        public RelayCommand ModifycdlDataCommand
        {
            get
            {
                return _modifycdldataCommand
                    ?? (_modifycdldataCommand = new RelayCommand(
                                          () =>
                                          {
                                              ModifyCdldata();
                                          }));
            }
        }
        private RelayCommand _autofitCommand;

        /// <summary>
        /// Gets the AutoFitCommand.
        /// </summary>
        public RelayCommand AutoFitCommand
        {
            get
            {
                return _autofitCommand
                    ?? (_autofitCommand = new RelayCommand(
                                          () =>
                                          {
                                              Autofit();
                                          }));
            }
        }

        private RelayCommand _singleclickCommand;

        /// <summary>
        /// Gets the SingleClickCommand.
        /// </summary>
        public RelayCommand SingleClickCommand
        {
            get
            {
                return _singleclickCommand
                    ?? (_singleclickCommand = new RelayCommand(
                                          () =>
                                          {
                                              SingleClick();
                                          }));
            }
        }

        private RelayCommand _manyclickCommand;

        /// <summary>
        /// Gets the ManyClickCommand.
        /// </summary>
        public RelayCommand ManyClickCommand
        {
            get
            {
                return _manyclickCommand
                    ?? (_manyclickCommand = new RelayCommand(
                                          () =>
                                          {
                                              ManyClick();
                                          }));
            }
        }

        private RelayCommand _disposeCommand;

        /// <summary>
        /// Gets the DisposeCommand.
        /// </summary>
        public RelayCommand DisposeCommand
        {
            get
            {
                return _disposeCommand
                    ?? (_disposeCommand = new RelayCommand(
                                          () =>
                                          {
                                              Dispose();
                                          }));
            }
        }

        private RelayCommand _exporttosvgCommand;

        /// <summary>
        /// Gets the ExportToSvgCommand.
        /// </summary>
        public RelayCommand ExportToSvgCommand
        {
            get
            {
                return _exporttosvgCommand
                    ?? (_exporttosvgCommand = new RelayCommand(
                                          () =>
                                          {
                                              ExportToSvg();
                                          }));
            }
        }

        private RelayCommand _beginInsertDianfxCommand;

        /// <summary>
        /// Gets the BeginInsertDianFXCommand.
        /// </summary>
        public RelayCommand BeginInsertDianFXCommand
        {
            get
            {
                return _beginInsertDianfxCommand
                    ?? (_beginInsertDianfxCommand = new RelayCommand(
                    () =>
                    {
                        beginInsertDianfx();
                    }));
            }
        }
        private RelayCommand _beginModifyDFXCommand;

        /// <summary>
        /// Gets the BeginModifyDFXCommand.
        /// </summary>
        public RelayCommand BeginModifyDFXCommand
        {
            get
            {
                return _beginModifyDFXCommand
                    ?? (_beginModifyDFXCommand = new RelayCommand(
                    () =>
                    {
                        beginModifyDianfx();
                    }));
            }
        }
        private RelayCommand _DeleteDFXCommand;

        /// <summary>
        /// Gets the DeleteDFXCommand.
        /// </summary>
        public RelayCommand DeleteDFXCommand
        {
            get
            {
                return _DeleteDFXCommand
                    ?? (_DeleteDFXCommand = new RelayCommand(
                    () =>
                    {
                        StationDataMode sdm = CurrentDatasProperty.CurrentDataProperty as StationDataMode;
                        if (sdm != null)
                            DeleteDianFXx(sdm.StationNameProperty);
                    }));
            }
        }
        private RelayCommand _openxhdataCommand;

        /// <summary>
        /// Gets the OpenXhDataCommand.
        /// </summary>
        public RelayCommand OpenXhDataCommand
        {
            get
            {
                return _openxhdataCommand
                    ?? (_openxhdataCommand = new RelayCommand(
                    () =>
                    {
                        openXhdata();
                    }));
            }
        }
        private RelayCommand _creatNewXhdataCommand;

        /// <summary>
        /// Gets the CreatNewXhdataCommand.
        /// </summary>
        public RelayCommand CreatNewXhdataCommand
        {
            get
            {
                return _creatNewXhdataCommand
                    ?? (_creatNewXhdataCommand = new RelayCommand(
                    () =>
                    {
                        creatNewxhdata();
                    }));
            }
        }
        private RelayCommand _savexhdataToanotherfileCommand;

        /// <summary>
        /// Gets the SaveXhdataToAntherFileCommand.
        /// </summary>
        public RelayCommand SaveXhdataToAntherFileCommand
        {
            get
            {
                return _savexhdataToanotherfileCommand
                    ?? (_savexhdataToanotherfileCommand = new RelayCommand(
                    () =>
                    {
                        saveOtherXhData();
                    }));
            }
        }
        private RelayCommand _exportToOTformatCommand;

        /// <summary>
        /// Gets the ExportToOTformatCommand.
        /// </summary>
        public RelayCommand ExportToOTformatCommand
        {
            get
            {
                return _exportToOTformatCommand
                    ?? (_exportToOTformatCommand = new RelayCommand(
                    () =>
                    {
                        ExportToOtFormat();
                    }));
            }
        }
        private RelayCommand _beginAdjustsignalDisCommand;

        /// <summary>
        /// Gets the ExportToOTformatCommand.
        /// </summary>
        public RelayCommand BeginAdjustSignalDisCommand
        {
            get
            {
                return _beginAdjustsignalDisCommand
                    ?? (_beginAdjustsignalDisCommand = new RelayCommand(
                    () =>
                    {
                       
                    }));
            }
        }

        private RelayCommand _editTrainInfoCommand;

        /// <summary>
        /// Gets the EditTrainInfoCommand.
        /// </summary>
        public RelayCommand EditTrainInfoCommand
        {
            get
            {
                return _editTrainInfoCommand
                    ?? (_editTrainInfoCommand = new RelayCommand(
                    () =>
                    {
                        editTrain();
                    }));
            }
        }

        private RelayCommand _calculetumbreakdisCommand;

        /// <summary>
        /// Gets the CalculetUmBreakDisCommand.
        /// </summary>
        public RelayCommand CalculetUmBreakDisCommand
        {
            get
            {
                return _calculetumbreakdisCommand
                    ?? (_calculetumbreakdisCommand = new RelayCommand(
                    () =>
                    {
                        calculetUmBreakDis();
                    }));
            }
        }

        private RelayCommand _testCommand;

        /// <summary>
        /// Gets the TestCommand.
        /// </summary>
        public RelayCommand TestCommand
        {
            get
            {
                return _testCommand
                    ?? (_testCommand = new RelayCommand(
                    () =>
                    {
                        test();
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

        private class ShowProcessBar
        {
            ProcessWindow proceswindow;
            public System.Windows.Threading.Dispatcher Disp = null;
            public ShowProcessBar()
            {
                proceswindow = new ProcessWindow();
            }

            public void show()
            {
                if (Disp != null)
                    Disp.BeginInvoke(new Action(() => { proceswindow.ShowDialog(); }));
            }

            public void close()
            {
                if (Disp != null)
                    Disp.BeginInvoke(new Action(() => { proceswindow.Close(); }));
            }
        }        
                 
    }
}