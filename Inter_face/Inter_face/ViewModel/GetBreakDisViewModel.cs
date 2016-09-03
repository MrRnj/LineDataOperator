using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inter_face.ViewModel
{
   public class GetBreakDisViewModel : ViewModelBase
    {
       private OpenFileDialog ofd;

        /// <summary>
        /// The <see cref="ProtectedDis" /> property's name.
        /// </summary>
        public const string ProtectedDisPropertyName = "ProtectedDis";

        private string _protectedDis = "0";

        /// <summary>
        /// Sets and gets the ProtectedDis property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ProtectedDis
        {
            get
            {
                return _protectedDis;
            }

            set
            {
                if (_protectedDis == value)
                {
                    return;
                }

                _protectedDis = value;
                RaisePropertyChanged(ProtectedDisPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TK" /> property's name.
        /// </summary>
        public const string TKPropertyName = "TK";

        private string _tK = "0";

        /// <summary>
        /// Sets and gets the TK property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string TK
        {
            get
            {
                return _tK;
            }

            set
            {
                if (_tK == value)
                {
                    return;
                }

                _tK = value;
                RaisePropertyChanged(TKPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TrainName" /> property's name.
        /// </summary>
        public const string TrainNamePropertyName = "TrainName";

        private string _trainName = "无车辆";

        /// <summary>
        /// Sets and gets the TrainName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string TrainName
        {
            get
            {
                return _trainName;
            }

            set
            {
                if (_trainName == value)
                {
                    return;
                }

                _trainName = value;
                RaisePropertyChanged(TrainNamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TotalLength" /> property's name.
        /// </summary>
        public const string TotalLengthPropertyName = "TotalLength";

        private string _totalLength = "0";

        /// <summary>
        /// Sets and gets the TotalLength property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string TotalLength
        {
            get
            {
                return _totalLength;
            }

            set
            {
                if (_totalLength == value)
                {
                    return;
                }

                _totalLength = value;
                RaisePropertyChanged(TotalLengthPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TotalWeight" /> property's name.
        /// </summary>
        public const string TotalWeightPropertyName = "TotalWeight";

        private string _totalWeight = "0";

        /// <summary>
        /// Sets and gets the TotalWeight property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string TotalWeight
        {
            get
            {
                return _totalWeight;
            }

            set
            {
                if (_totalWeight == value)
                {
                    return;
                }

                _totalWeight = value;
                RaisePropertyChanged(TotalWeightPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="FilePath" /> property's name.
        /// </summary>
        public const string FilePathPropertyName = "FilePath";

        private string _filePath = string.Empty;

        /// <summary>
        /// Sets and gets the FilePath property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FilePath
        {
            get
            {
                return _filePath;
            }

            set
            {
                if (_filePath == value)
                {
                    return;
                }

                _filePath = value;
                RaisePropertyChanged(FilePathPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ContentText" /> property's name.
        /// </summary>
        public const string ContentTextPropertyName = "ContentText";

        private string _contentText = "无信息";

        /// <summary>
        /// Sets and gets the ContentText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ContentText
        {
            get
            {
                return _contentText;
            }

            set
            {
                if (_contentText == value)
                {
                    return;
                }

                _contentText = value;
                RaisePropertyChanged(ContentTextPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SaveProcess" /> property's name.
        /// </summary>
        public const string SaveProcessPropertyName = "SaveProcess";

        private bool _saveProcess = true;

        /// <summary>
        /// Sets and gets the SaveProcess property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool SaveProcess
        {
            get
            {
                return _saveProcess;
            }

            set
            {
                if (_saveProcess == value)
                {
                    return;
                }

                _saveProcess = value;
                RaisePropertyChanged(SaveProcessPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="OriSpeed" /> property's name.
        /// </summary>
        public const string OriSpeedPropertyName = "OriSpeed";

        private string _oriSpeed = "0";

        /// <summary>
        /// Sets and gets the OriSpeed property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string OriSpeed
        {
            get
            {
                return _oriSpeed;
            }

            set
            {
                if (_oriSpeed == value)
                {
                    return;
                }

                _oriSpeed = value;
                RaisePropertyChanged(OriSpeedPropertyName);
            }
        }

        public GetBreakDisViewModel()
        {
            MessengerInstance.Register<string>(this, "iniTrain", 
                p =>
                {
                    if (!p.Equals(string.Empty))
                    {
                        //trainame:filepath:protectdis:tk:oriV
                        string[] infos = p.Split('|');
                        fullfillInfos(infos[1], infos[2], infos[3], infos[4]);
                    }
                });

            MessengerInstance.Register<string>(this, "reciveDisinfos",
                p =>
                {
                    ContentText = p;
                });
        }

        private void fullfillInfos(string path, string protectdis, string tk,string orispeed)
        {
            string[] lines = File.ReadAllLines(path);

            if (lines != null)
            {
                foreach (string info in lines)
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
                            TotalWeight = info.Split(':')[1];
                            break;                        
                        default:
                            break;
                    }
                }
            }

            FilePath = path;
            TK = tk;
            OriSpeed = orispeed;
            ProtectedDis = protectdis;
        }

        private void loadtrain()
        {
            ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "车辆文件|*.tr|所有文件|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fullfillInfos(ofd.FileName, "0", "0", "0");

                formatMu();
            }           
        }

        private void formatMu()
        {
            MessengerInstance.Send(string.Format("{0}|{1}|{2}|{3}|{4}", 
                TrainName, FilePath, ProtectedDis, TK, OriSpeed), "formatMu");
        }

        private void unformatMu()
        {
            MessengerInstance.Send(string.Empty, "formatMu");
        }
        
        private void calculeteDis()
        {
            MessengerInstance.Send(string.Format("{0}:{1}:{2}:{3}", 
                ProtectedDis, TK, SaveProcess ? "1" : "0", OriSpeed.ToString()), "calculeteDis");
        }      

        private void dispose()
        {
            MessengerInstance.Unregister(this);
        }

        private RelayCommand _loadtrainCommand;

        /// <summary>
        /// Gets the LoadTrainCommand.
        /// </summary>
        public RelayCommand LoadTrainCommand
        {
            get
            {
                return _loadtrainCommand
                    ?? (_loadtrainCommand = new RelayCommand(
                    () =>
                    {
                        loadtrain();
                    }));
            }
        }

        private RelayCommand _formatMuCommand;

        /// <summary>
        /// Gets the FormatMuCommand.
        /// </summary>
        public RelayCommand FormatMuCommand
        {
            get
            {
                return _formatMuCommand
                    ?? (_formatMuCommand = new RelayCommand(
                    () =>
                    {
                        formatMu();
                    },
                    () => !FilePath.Equals(string.Empty)));
            }
        }

        private RelayCommand _unformatMuCommand;

        /// <summary>
        /// Gets the UnformatMuCommand.
        /// </summary>
        public RelayCommand UnformatMuCommand
        {
            get
            {
                return _unformatMuCommand
                    ?? (_unformatMuCommand = new RelayCommand(
                    () =>
                    {
                        unformatMu();
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
                        dispose();
                    }));
            }
        }

        private RelayCommand _calculeteDisCommand;

        /// <summary>
        /// Gets the CalculeteDisCommand.
        /// </summary>
        public RelayCommand CalculeteDisCommand
        {
            get
            {
                return _calculeteDisCommand
                    ?? (_calculeteDisCommand = new RelayCommand(
                    () =>
                    {
                        calculeteDis();
                    }));
            }
        }
    }
}
