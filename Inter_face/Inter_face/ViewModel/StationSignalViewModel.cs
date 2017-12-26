using GalaSoft.MvvmLight;
using ExtractData;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inter_face.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace Inter_face.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class StationSignalViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the StationSignalViewModel class.
        /// </summary>
        public StationSignalViewModel()
        {
            StationSignalCollection = new ObservableCollection<StationSignaleDataModel>();            

            MessengerInstance.Register<System.Collections.Generic.List<string>>(this, "cdl", 
                (p) => 
                {
                    cdlist = p;
                    if (cdlist.Count == 1 && cdlist[0].Split(':')[1].Equals(cdlist[0].Split(':')[0]))
                    {
                        SectionNum.Add(1);
                        return;
                    }                        
                    
                    //问题出在此处，当没有长短链时，人为加了个“0+0：0+0”的长短链，这导致了路段号出错
                    for (int i = 0; i <= p.Count; i++)
                    {
                        SectionNum.Add(i + 1);
                    }
                });

            MessengerInstance.Register<System.Collections.Generic.List<StationDataMode>>(this, "stationsignal",
                (p) =>
                {
                    StationSignalCollection.Clear();                    
                    string[] parts = { };
                    StationSignaleDataModel asignal;
                    exchangeddatas = p;

                    foreach (StationDataMode item in p)
                    {                        
                        parts = item.StationNameProperty.Split(':');
                        if (StationSignalCollection.Count(q => q.StationNameProperty == parts[2]) > 0)
                            continue;
                        asignal = new StationSignaleDataModel();
                        asignal.StationNameProperty = parts[2];                       
                        asignal.StationPositionProperty = parts[4];

                        foreach (StationDataMode inneritem in p.Where(q => q.StationNameProperty.Split(':')[2].Equals(parts[2])))
                        {
                            if (inneritem.StationNameProperty.Split(':')[3].Equals("J"))
                            {
                                asignal.InSignalProperty = new SignalDataViewModel()
                                {
                                    SecNumbersProperty = SectionNum,
                                    Guanhao = inneritem.HatProperty,
                                    IsSelected = false,
                                    PartImenber = (int)Math.Floor(inneritem.PositionProperty),
                                    PartII = int.Parse(inneritem.PositionProperty.ToString("F3").Split('.')[1]),
                                    SectionNum = inneritem.SectionNumProperty.ToString(),
                                    CdlInfoProperty = cdlist,
                                    Mark = inneritem.StationNameProperty.Split(':')[1],
                                    SelectedIndex = inneritem.SectionNumProperty - 1,
                                    TypeProperty = "In",
                                    IsEnableProperty = inneritem.PositionProperty == -1 ? false : true
                                };
                            }
                            else if (inneritem.StationNameProperty.Split(':')[3].Equals("C"))
                            {
                                asignal.OutSignalProperty = new SignalDataViewModel()
                                {
                                    SecNumbersProperty = SectionNum,
                                    Guanhao = inneritem.HatProperty,
                                    IsSelected = false,
                                    PartImenber = (int)Math.Floor(inneritem.PositionProperty),
                                    PartII = int.Parse(inneritem.PositionProperty.ToString("F3").Split('.')[1]),
                                    SectionNum = inneritem.SectionNumProperty.ToString(),
                                    CdlInfoProperty = cdlist,
                                    Mark = inneritem.StationNameProperty.Split(':')[1],
                                    SelectedIndex = inneritem.SectionNumProperty - 1,
                                    TypeProperty = "Out",
                                    IsEnableProperty = inneritem.PositionProperty == -1 ? false : true                              
                                };
                            }
                        }

                        StationSignalCollection.Add(asignal);
                    }
                });
        }

        private List<StationDataMode> exchangeddatas;
        private List<string> cdlist;
        private LdhInfoWindow liwindow;
        private string errorMsg;
        /// <summary>
        /// The <see cref="SectionNum" /> property's name.
        /// </summary>
        public const string SectionNumPropertyName = "SectionNum";

        private List<int> _sectionumProperty = new List<int>();

        /// <summary>
        /// Sets and gets the SectionNum property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<int> SectionNum
        {
            get
            {
                return _sectionumProperty;
            }

            set
            {
                if (_sectionumProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(SectionNumPropertyName);
                _sectionumProperty = value;
                RaisePropertyChanged(SectionNumPropertyName);
            }
        }

        public List<string> CdlistProperty
        {
            get { return cdlist; }
            set { cdlist = value; }
        }


        ObservableCollection<StationSignaleDataModel> _stationsignalcollection;
        public ObservableCollection<StationSignaleDataModel> StationSignalCollection
        {
            get { return _stationsignalcollection; }
            set { _stationsignalcollection = value; }
        }

        private List<string> _SectionsInfo;

        public List<string> SectionsInfoProperty
        {
            get { return _SectionsInfo; }
            set { _SectionsInfo = value; }
        }


        /// <summary>
        /// The <see cref="CurrentStationSignal" /> property's name.
        /// </summary>
        public const string CurrentStationSignalPropertyName = "CurrentStationSignal";

        private StationSignaleDataModel _currentstationsignalProperty = null;

        /// <summary>
        /// Sets and gets the CurrentStationSignal property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public StationSignaleDataModel CurrentStationSignal
        {
            get
            {
                return _currentstationsignalProperty;
            }

            set
            {
                if (_currentstationsignalProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(CurrentStationSignalPropertyName);
                _currentstationsignalProperty = value as StationSignaleDataModel;
                if (_currentstationsignalProperty != null)
                {
                    foreach (StationSignaleDataModel item in StationSignalCollection)
                    {
                        item.InSignalProperty.IsSelected = false;
                        item.OutSignalProperty.IsSelected = false;
                        if (item == _currentstationsignalProperty)
                        {
                            item.OutSignalProperty.IsSelected = true;
                            item.InSignalProperty.IsSelected = true;
                        }
                    }
                }
                RaisePropertyChanged(CurrentStationSignalPropertyName);
            }
        }
        private void UpDataStationSignals()
        {
            //无数据取-1    
            string[] parts = { };
            foreach (StationSignaleDataModel item in StationSignalCollection)
            {
                foreach (StationDataMode inneritem in exchangeddatas.Where(q => q.StationNameProperty.Split(':')[2].Equals(item.StationNameProperty)))
                {
                    parts = inneritem.StationNameProperty.Split(':');
                    if (parts[3].Equals("J"))
                    {
                        if (!item.InSignalProperty.IsEnableProperty ||
                            item.InSignalProperty.PartImenber == -1 || 
                            item.InSignalProperty.PartII == -1)
                        {
                            inneritem.PositionProperty = -1;
                        }
                        else
                        {
                            inneritem.PositionProperty = float.Parse((float.Parse(item.InSignalProperty.PartImenber.ToString()) + 
                                                                      float.Parse((item.InSignalProperty.PartII).ToString()) / 1000).ToString("#0.000"));
                        }
                        inneritem.SectionNumProperty = item.InSignalProperty.SectionNum == null ?
                            -1 : int.Parse(item.InSignalProperty.SectionNum);
                        inneritem.StationNameProperty = 
                            string.Format("{0}:{1}:{2}:{3}:{4}", 
                            parts[0], item.InSignalProperty.Mark, parts[2], parts[3], parts[4]);                        
                    }
                    else if (parts[3].Equals("C"))
                    {
                        inneritem.PositionProperty = !item.OutSignalProperty.IsEnableProperty ||
                                                      item.OutSignalProperty.PartImenber == -1 ||
                                                      item.OutSignalProperty.PartII == -1 ?
                            -1 :
                          float.Parse((float.Parse(item.OutSignalProperty.PartImenber.ToString()) +
                                       float.Parse((item.OutSignalProperty.PartII).ToString()) / 1000).ToString("#0.000"));
                        inneritem.SectionNumProperty = item.OutSignalProperty.SectionNum == null ?
                            -1 : int.Parse(item.OutSignalProperty.SectionNum);
                        inneritem.StationNameProperty =
                            string.Format("{0}:{1}:{2}:{3}:{4}",
                            parts[0], item.OutSignalProperty.Mark, parts[2], parts[3], parts[4]);
                    }
                }                
            }
            MessengerInstance.Send(exchangeddatas, "UpdataStationSignal");
        }

        private void ShowLdh()
        {
            liwindow = new LdhInfoWindow();
            MessengerInstance.Send<string[]>(cdlist.ToArray(), "showldh");

            liwindow.ShowInTaskbar = false; 
            liwindow.Show();
        }

        private bool CheckCdlInfo()
        {            

            foreach (StationSignaleDataModel item in StationSignalCollection)
            {                
                if (CheckSingleCdlinfo(item.InSignalProperty))
                {
                    if (!CheckSingleCdlinfo(item.OutSignalProperty))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }                
            }

            return true;
        }

        private bool CheckSingleCdlinfo(SignalDataViewModel sdvm)
        {
            string[] parts;
            float position;
            int secnumber;
            errorMsg = string.Empty;

            if (!sdvm.IsEnableProperty)
                return true;

            if (SectionNum.Count == 1)
                return true;

            if (sdvm.PartImenber != -1 || sdvm.PartII != -1)
            {
                position = sdvm.PartImenber * 1000 + sdvm.PartII;
                secnumber = int.Parse(sdvm.SectionNum);
                if (secnumber == 1)
                {
                    parts = cdlist[0].Split(':');
                    if (position > float.Parse(parts[0].Split('+')[1]))
                    {
                        errorMsg = string.Format("错误：{0} {1}+{2} 路段号设置错误，请改正！",
                            sdvm.Guanhao, sdvm.PartImenber,
                            sdvm.PartII);
                        return false;
                    }
                }
                else if (secnumber == cdlist.Count() + 1)
                {
                    parts = cdlist[secnumber - 2].Split(':');
                    if (position < float.Parse(parts[1].Split('+')[1]))
                    {
                        errorMsg = string.Format("错误：坐标 {0} {1}+{2} 路段号设置错误，请改正！",
                            sdvm.Guanhao, sdvm.PartImenber,
                            sdvm.PartII);
                        return false;
                    }
                }
                else
                {
                    parts = cdlist[secnumber - 1].Split(':');
                    if (position > float.Parse(parts[0].Split('+')[1]))
                    {
                        errorMsg = string.Format("错误：坐标 {0} {1}+{2} 路段号设置错误，请改正！",
                            sdvm.Guanhao, sdvm.PartImenber,
                            sdvm.PartII);
                        return false;
                    }
                    parts = cdlist[secnumber - 2].Split(':');
                    if (position < float.Parse(parts[1].Split('+')[1]))
                    {
                        errorMsg = string.Format("错误：坐标 {0} {1}+{2} 路段号设置错误，请改正！",
                             sdvm.Guanhao, sdvm.PartImenber,
                             sdvm.PartII);
                        return false;
                    }
                }
            }
            return true;    
        }

        private RelayCommand _UpdataCommand;

        /// <summary>
        /// Gets the UpDataCommand.
        /// </summary>
        public RelayCommand UpDataCommand
        {
            get
            {
                return _UpdataCommand
                    ?? (_UpdataCommand = new RelayCommand(
                                          () =>
                                          {
                                              if (CheckCdlInfo())
                                                  UpDataStationSignals();
                                              else
                                              {
                                                  MessageBox.Show(errorMsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                              }
                                          }));
            }
        }

        private RelayCommand _showldhCommand;

        /// <summary>
        /// Gets the ShowLdhCommand.
        /// </summary>
        public RelayCommand ShowLdhCommand
        {
            get
            {
                return _showldhCommand
                    ?? (_showldhCommand = new RelayCommand(
                                          () =>
                                          {
                                              ShowLdh();
                                          }));
            }
        }        
    }
}