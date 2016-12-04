using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inter_face.Models;
using Inter_face.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inter_face.ViewModel
{
   public class TrainInfoOperatorViewModel : ViewModelBase
    {
        private OpenFileDialog ofd;
        private SaveFileDialog sfd;
        private SpeedBreakWindow sbwindow;
        private TractionPowerOpWindow tpowindow;

        private ObservableCollection<Speed_breakMode> s_bPair;
        private ObservableCollection<TractionPowerArrayViewModel> tpArray = null;

        public ObservableCollection<Speed_breakMode> S_BPair
        {
            get { return s_bPair; }
            set { s_bPair = value; }
        }

        /// <summary>
        /// The <see cref="TrainName" /> property's name.
        /// </summary>
        public const string TrainNamePropertyName = "TrainName";

        private string _trainnameProperty = string.Empty;

        /// <summary>
        /// Sets and gets the TrainName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string TrainName
        {
            get
            {
                return _trainnameProperty;
            }

            set
            {
                if (_trainnameProperty == value)
                {
                    return;
                }

                _trainnameProperty = value;
                RaisePropertyChanged(TrainNamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TotalLength" /> property's name.
        /// </summary>
        public const string TotalLengthPropertyName = "TotalLength";

        private string _totallengthProperty = string.Empty;

        /// <summary>
        /// Sets and gets the TotalLength property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string TotalLength
        {
            get
            {
                return _totallengthProperty;
            }

            set
            {
                if (_totallengthProperty == value)
                {
                    return;
                }

                _totallengthProperty = value;
                RaisePropertyChanged(TotalLengthPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TotalWeightProperty" /> property's name.
        /// </summary>
        public const string TotalWeightPropertyPropertyName = "TotalWeightProperty";

        private string _totalweightProperty = string.Empty;

        /// <summary>
        /// Sets and gets the TotalWeightProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string TotalWeightProperty
        {
            get
            {
                return _totalweightProperty;
            }

            set
            {
                if (_totalweightProperty == value)
                {
                    return;
                }

                _totalweightProperty = value;
                RaisePropertyChanged(TotalWeightPropertyPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="BasicalBreakFactors" /> property's name.
        /// </summary>
        public const string BasicalBreakFactorsPropertyName = "BasicalBreakFactors";

        private string _basicalbreakfactorsProperty = string.Empty;

        /// <summary>
        /// Sets and gets the BasicalBreakFactors property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string BasicalBreakFactors
        {
            get
            {
                return _basicalbreakfactorsProperty;
            }

            set
            {
                if (_basicalbreakfactorsProperty == value)
                {
                    return;
                }

                _basicalbreakfactorsProperty = value;
                RaisePropertyChanged(BasicalBreakFactorsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="YFactors" /> property's name.
        /// </summary>
        public const string YFactorsPropertyName = "YFactors";

        private string _yfactorsProperty = string.Empty;

        /// <summary>
        /// Sets and gets the YFactors property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string YFactors
        {
            get
            {
                return _yfactorsProperty;
            }

            set
            {
                if (_yfactorsProperty == value)
                {
                    return;
                }

                _yfactorsProperty = value;
                RaisePropertyChanged(YFactorsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CanEdit" /> property's name.
        /// </summary>
        public const string CanEditPropertyName = "CanEdit";

        private bool _caneditProperty = false;

        /// <summary>
        /// Sets and gets the CanEdit property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool CanEdit
        {
            get
            {
                return _caneditProperty;
            }

            set
            {
                if (_caneditProperty == value)
                {
                    return;
                }

                _caneditProperty = value;
                RaisePropertyChanged(CanEditPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CurrentIndex" /> property's name.
        /// </summary>
        public const string CurrentIndexPropertyName = "CurrentIndex";

        private int _currentindexProperty = 0;

        /// <summary>
        /// Sets and gets the CurrentIndex property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int CurrentIndex
        {
            get
            {
                return _currentindexProperty;
            }

            set
            {
                if (_currentindexProperty == value)
                {
                    return;
                }

                _currentindexProperty = value;
                RaisePropertyChanged(CurrentIndexPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CurrentFilePath" /> property's name.
        /// </summary>
        public const string CurrentFilePathPropertyName = "CurrentFilePath";

        private string _currentfilepath = string.Empty;

        /// <summary>
        /// Sets and gets the CurrentFilePath property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string CurrentFilePath
        {
            get
            {
                return _currentfilepath;
            }

            set
            {
                if (_currentfilepath == value)
                {
                    return;
                }

                _currentfilepath = value;
                RaisePropertyChanged(CurrentFilePathPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="StartingPower" /> property's name.
        /// </summary>
        public const string StartingPowerPropertyName = "StartingPower";

        private string _startingPower = "0";

        /// <summary>
        /// Sets and gets the StartingPower property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string StartingPower
        {
            get
            {
                return _startingPower;
            }

            set
            {
                if (_startingPower == value)
                {
                    return;
                }

                _startingPower = value;
                RaisePropertyChanged(StartingPowerPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="StartingResisdengce" /> property's name.
        /// </summary>
        public const string StartingResisdengcePropertyName = "StartingResisdengce";

        private string _startingResisdence = "0";

        /// <summary>
        /// Sets and gets the StartingResisdengce property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string StartingResisdengce
        {
            get
            {
                return _startingResisdence;
            }

            set
            {
                if (_startingResisdence == value)
                {
                    return;
                }

                _startingResisdence = value;
                RaisePropertyChanged(StartingResisdengcePropertyName);
            }
        }

        public TrainInfoOperatorViewModel()
        {
            S_BPair = new ObservableCollection<Models.Speed_breakMode>();

            MessengerInstance.Register<ObservableCollection<Speed_breakMode>>(this, "updatasb",
                (p) => 
                {
                    s_bPair.Clear();
                    foreach (Speed_breakMode item in p)
                    {
                        s_bPair.Add(item);
                    }                    
                });

            MessengerInstance.Register<ObservableCollection<TractionPowerArrayViewModel>>(this, "sendTpArray",
                p => 
                {
                    tpArray = p;
                });
        }        

        private void edit()
        {
            CanEdit = true;
        }

        private void loadTrain()
        {
            string filename;
            string[] infos;
            string[] parts;
            string[] pairs;
            ofd = new OpenFileDialog();
            ofd.Filter = "车辆文件|*.tr|全部文件|*.*";
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filename = ofd.FileName;
                CurrentFilePath = filename;
                infos = System.IO.File.ReadAllLines(filename);

                try
                {
                    foreach (string info in infos)
                    {
                        switch (info.Split(':')[0])
                        {
                            case "车辆名称":
                                TrainName = info.Split(':')[1];
                                break;
                            case "总长":
                                TotalLength = info.Split(':')[1];
                                break;
                            case "总重量":
                                TotalWeightProperty = info.Split(':')[1];
                                break;
                            case "基本摩擦系数":
                                BasicalBreakFactors = info.Split(':')[1];
                                break;
                            case "制动加速度表":
                                parts = info.Split(':')[1].Split(',');
                                foreach (string part in parts)
                                {
                                    pairs = part.Split('|');
                                    S_BPair.Add(new Speed_breakMode() { Break = pairs[1], Speed = pairs[0] });
                                }
                                break;
                            case "转动惯量":
                                YFactors = info.Split(':')[1];
                                break;
                            case "启动牵引力":
                                StartingPower = info.Split(':')[1];
                                break;
                            case "启动阻力":
                                StartingResisdengce = info.Split(':')[1];
                                break;
                            case "牵引特性曲线":
                                formatTpArray(info.Split(':')[1]);
                                break;
                            default:
                                break;
                        }
                    }

                    CanEdit = false;
                }
                catch
                {

                }
                
            }
        }

        private ObservableCollection<TractionPowerArrayViewModel> formatTpArray(string rawString)
        {
            tpArray = new ObservableCollection<TractionPowerArrayViewModel>();
            TractionPowerArrayViewModel newtp;                  

            /************************************************
            speedarray:0/t10/t20|
            powerarray:index/tRotation/t1/t2,
                       index/tRotation/t1/t2|
            inflectionpoint:index/tRotation/t1/t2,
                            index/tRotation/t1/t2
            *************************************************/

            string[] datas = rawString.Split('|');
            string[] speedinfo = datas[0].Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            string[] powerinfo = datas[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] extrainfo = datas[2].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < powerinfo.Count(); i++)
            {
                newtp = new TractionPowerArrayViewModel();
                string[] parts = powerinfo[i].Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                string[] infs = extrainfo[i].Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                newtp.Index = parts[0];
                bool isinfs = false;

                for (int j = 2; j < parts.Count(); j++)
                {
                    for (int k = 2; k < infs.Count(); k++)
                    {
                        if (speedinfo[j].Equals(infs[k]))
                        {
                            isinfs = true;
                            break;
                        }
                    }

                    newtp.TpModel.Add(new TractionPowerModel()
                    {
                        Power = parts[j],
                        Speed = speedinfo[j],
                        IsinflectionPoint = isinfs
                    });

                    isinfs = false;
                }

                tpArray.Add(newtp);
            }

            return tpArray;
        }

        private void newTrain()
        {
            TrainName = string.Empty;
            TotalLength = string.Empty;
            TotalWeightProperty = string.Empty;
            BasicalBreakFactors = string.Empty;
            CurrentFilePath = string.Empty;
            CanEdit = true;
            S_BPair.Clear();
            tpArray = new ObservableCollection<TractionPowerArrayViewModel>();
        }

        private void saveTrain()
        {
            string content = string.Empty;
            string sbpair = string.Empty;

            content += string.Format("车辆名称:{0}\r\n", TrainName);
            content += string.Format("总长:{0}\r\n", TotalLength);
            content += string.Format("总重量:{0}\r\n", TotalWeightProperty);
            content += string.Format("基本摩擦系数:{0}\r\n", BasicalBreakFactors);
            content += string.Format("启动牵引力:{0}\r\n", StartingPower);
            content += string.Format("启动阻力:{0}\r\n", StartingResisdengce);
            content += string.Format("转动惯量:{0}\r\n", YFactors);
            content += "\r\n";

            foreach (Speed_breakMode item in S_BPair)
            {
                sbpair += string.Format("{0}|{1},", item.Speed, item.Break);
            }
            sbpair = sbpair.TrimEnd(',');
            content += string.Format("制动加速度表:{0}", sbpair);
            content += "\r\n";

            content += string.Format("牵引特性曲线:{0}", formatSaveTplines());

            if (CurrentFilePath.Equals(string.Empty))
            {
                sfd = new SaveFileDialog();
                sfd.AddExtension = true;
                sfd.DefaultExt = "tr";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    CurrentFilePath = sfd.FileName;
                    System.IO.File.AppendAllText(CurrentFilePath, content);
                }
            }
            else
            {
                if (System.IO.File.Exists(CurrentFilePath))
                {
                    System.IO.File.Delete(CurrentFilePath);
                }

                System.IO.File.AppendAllText(CurrentFilePath, content);
            }
        }

        private string formatSaveTplines()
        {
            string speedline = "0\t0\t";
            string powerline = string.Empty;
            string inflectionsline = string.Empty;

            foreach (TractionPowerModel tp in tpArray[0].TpModel)
            {
                speedline += string.Format("{0}\t", tp.Speed);
            }

            speedline.TrimEnd('\t');

            foreach (TractionPowerArrayViewModel item in tpArray)
            {
                powerline += string.Format("{0}\t{1}\t", item.Index, item.Index);
                inflectionsline += string.Format("{0}\t{1}\t",
                    item.Index,
                    item.TpModel.Count(
                        p =>
                        {
                            return p.IsinflectionPoint;
                        }).ToString());

                foreach (TractionPowerModel tp in item.TpModel)
                {
                    powerline += string.Format("{0}\t", tp.Power);
                    if (tp.IsinflectionPoint)
                    {
                        inflectionsline += string.Format("{0}\t", tp.Speed);
                    }
                }

                powerline.TrimEnd('\t');
                inflectionsline.TrimEnd('\t');

                powerline += ",";
                inflectionsline += ",";
            }

            powerline.TrimEnd(',');
            inflectionsline.TrimEnd(',');

            return string.Format("{0}|{1}|{2}", speedline, powerline, inflectionsline);
        }

        private void editSpeedBreaktable()
        {
            sbwindow = new SpeedBreakWindow();

            MessengerInstance.Send(CanEdit, "canEdit");
            MessengerInstance.Send(S_BPair, "sb");
            sbwindow.ShowDialog();
        }

        private void editSpeedPowertable()
        {
            tpowindow = new TractionPowerOpWindow();

            MessengerInstance.Send(tpArray, "getTpArray");
            tpowindow.ShowDialog();
        }
        
        private void unregist()
        {
            MessengerInstance.Unregister(this);
        }

        private RelayCommand _openDataCommand;

        /// <summary>
        /// Gets the OpenDataCommand.
        /// </summary>
        public RelayCommand OpenDataCommand
        {
            get
            {
                return _openDataCommand
                    ?? (_openDataCommand = new RelayCommand(
                    () =>
                    {
                        loadTrain();
                    }));
            }
        }

        private RelayCommand _newDataCommand;

        /// <summary>
        /// Gets the NewDataCommand.
        /// </summary>
        public RelayCommand NewDataCommand
        {
            get
            {
                return _newDataCommand
                    ?? (_newDataCommand = new RelayCommand(
                    () =>
                    {
                        newTrain();
                    }));
            }
        }

        private RelayCommand _saveDataCommand;

        /// <summary>
        /// Gets the SaveDataCommand.
        /// </summary>
        public RelayCommand SaveDataCommand
        {
            get
            {
                return _saveDataCommand
                    ?? (_saveDataCommand = new RelayCommand(
                    () =>
                    {
                        saveTrain();
                    }));
            }
        }

        private RelayCommand _editDataCommand;

        /// <summary>
        /// Gets the EditDataCommand.
        /// </summary>
        public RelayCommand EditDataCommand
        {
            get
            {
                return _editDataCommand
                    ?? (_editDataCommand = new RelayCommand(
                    () =>
                    {
                        edit();
                    },
                    () => !CurrentFilePath.Equals(string.Empty)));
            }
        }

        private RelayCommand _beginEdits_bCommand;

        /// <summary>
        /// Gets the BeginEditS_BCommand.
        /// </summary>
        public RelayCommand BeginEditS_BCommand
        {
            get
            {
                return _beginEdits_bCommand
                    ?? (_beginEdits_bCommand = new RelayCommand(
                    () =>
                    {
                        editSpeedBreaktable();
                    }));
            }
        }

        private RelayCommand _beginEditTpCommand;

        /// <summary>
        /// Gets the BeginEditTpCommand.
        /// </summary>
        public RelayCommand BeginEditTpCommand
        {
            get
            {
                return _beginEditTpCommand
                    ?? (_beginEditTpCommand = new RelayCommand(
                    () =>
                    {
                        editSpeedPowertable();
                    }));
            }
        }        
    }
}
