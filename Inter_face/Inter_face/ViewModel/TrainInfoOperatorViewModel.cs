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

        private ObservableCollection<Models.Speed_breakMode> s_bPair;

        public ObservableCollection<Models.Speed_breakMode> S_BPair
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
                                S_BPair.Add(new Models.Speed_breakMode() { Break = pairs[1], Speed = pairs[0] });
                            }
                            break;
                        case "转动惯量":
                            YFactors = info.Split(':')[1];
                            break;
                        default:
                            break;
                    }
                }

                CanEdit = false;
            }
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
        }

        private void saveTrain()
        {
            string content = string.Empty;
            string sbpair = string.Empty;

            content += string.Format("车辆名称:{0}\r\n", TrainName);
            content += string.Format("总长:{0}\r\n", TotalLength);
            content += string.Format("总重量:{0}\r\n", TotalWeightProperty);
            content += string.Format("基本摩擦系数:{0}\r\n", BasicalBreakFactors);
            content += string.Format("转动惯量:{0}\r\n", YFactors);
            content += "\r\n";
            foreach (Models.Speed_breakMode item in S_BPair)
            {
                sbpair += string.Format("{0}|{1},", item.Speed, item.Break);
            }
            sbpair=sbpair.TrimEnd(',');
            content += string.Format("制动加速度表:{0}", sbpair);

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

        private void editSpeedBreaktable()
        {
            sbwindow = new SpeedBreakWindow();

            MessengerInstance.Send(CanEdit, "canEdit");
            MessengerInstance.Send(S_BPair, "sb");
            sbwindow.ShowDialog();
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
    }
}
