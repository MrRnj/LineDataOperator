using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.ViewModel
{
    class ModifyQujianViewModel : ViewModelBase
    {
        private int orisectionum;
        private int currentsectionum;
        public ModifyQujianViewModel()
        {
            MessengerInstance.Register<string>(this, "Resources",
               (p) =>
               {
                   string[] parts = p.Split(';');
                   StartPosition = float.Parse(parts[0]);
                   EndPosition = float.Parse(parts[1]);
                   DivInfos = parts[3];
                   string[] infos = divinfos.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                   MaxValue = (int)Math.Round(EndPosition * 1000 - float.Parse(parts[2]) * 1000 - float.Parse(parts[4]), 0);
                   MinValue = 0;
                   CurrentLC = string.Format("{0} {1}", infos[0].Split(':')[0].Split('+')[0],
                     string.Format("{0}+{1}", StartPosition.ToString("F3").Split('.')[0], 
                     StartPosition.ToString("F3").Split('.')[1]));
                   orisectionum = int.Parse(parts[5]);
               });
        }
        /// <summary>
        /// The <see cref="StartPosition" /> property's name.
        /// </summary>
        public const string StartPositionPropertyName = "StartPosition";

        private float _startpositionProperty = 0;

        /// <summary>
        /// Sets and gets the StartPosition property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public float StartPosition
        {
            get
            {
                return _startpositionProperty;
            }

            set
            {
                if (_startpositionProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(StartPositionPropertyName);
                _startpositionProperty = value;
                RaisePropertyChanged(StartPositionPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="EndPosition" /> property's name.
        /// </summary>
        public const string EndPositionPropertyName = "EndPosition";

        private float _endpositionProperty = 0;

        /// <summary>
        /// Sets and gets the EndPosition property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public float EndPosition
        {
            get
            {
                return _endpositionProperty;
            }

            set
            {
                if (_endpositionProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(EndPositionPropertyName);
                _endpositionProperty = value;
                RaisePropertyChanged(EndPositionPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="CurrentPosition" /> property's name.
        /// </summary>
        public const string CurrentPositionPropertyName = "CurrentPosition";

        private double _currentpositionProperty = 0;

        /// <summary>
        /// Sets and gets the CurrentPosition property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double CurrentPosition
        {
            get
            {
                return _currentpositionProperty;
            }

            set
            {
                if (_currentpositionProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(CurrentPositionPropertyName);
                _currentpositionProperty = value;
                RaisePropertyChanged(CurrentPositionPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="DialogStatus" /> property's name.
        /// </summary>
        public const string DialogStatusPropertyName = "DialogStatus";

        private bool? _dialogstatusProperty = null;

        /// <summary>
        /// Sets and gets the DialogStatus property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool? DialogStatus
        {
            get
            {
                return _dialogstatusProperty;
            }

            set
            {
                if (_dialogstatusProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(DialogStatusPropertyName);
                _dialogstatusProperty = value;
                RaisePropertyChanged(DialogStatusPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="MaxValue" /> property's name.
        /// </summary>
        public const string MaxValuePropertyName = "MaxValue";

        private int _maxvalueProperty = 0;

        /// <summary>
        /// Sets and gets the MaxValue property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int MaxValue
        {
            get
            {
                return _maxvalueProperty;
            }

            set
            {
                if (_maxvalueProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(MaxValuePropertyName);
                _maxvalueProperty = value;
                RaisePropertyChanged(MaxValuePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="MinValue" /> property's name.
        /// </summary>
        public const string MinValuePropertyName = "MinValue";

        private int _minvalueProperty = 0;

        /// <summary>
        /// Sets and gets the MinValue property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int MinValue
        {
            get
            {
                return _minvalueProperty;
            }

            set
            {
                if (_minvalueProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(MinValuePropertyName);
                _minvalueProperty = value;
                RaisePropertyChanged(MinValuePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="CurrentLC" /> property's name.
        /// </summary>
        public const string CurrentLCPropertyName = "CurrentLC";

        private string _currentlc = string.Empty;

        /// <summary>
        /// Sets and gets the CurrentLC property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string CurrentLC
        {
            get
            {
                return _currentlc;
            }

            set
            {
                if (_currentlc == value)
                {
                    return;
                }

                RaisePropertyChanging(CurrentLCPropertyName);
                _currentlc = value;
                RaisePropertyChanged(CurrentLCPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="DivInfos" /> property's name.
        /// </summary>
        public const string DivInfosPropertyName = "DivInfos";

        private string divinfos = string.Empty;

        /// <summary>
        /// Sets and gets the DivInfos property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string DivInfos
        {
            get
            {
                return divinfos;
            }

            set
            {
                if (divinfos == value)
                {
                    return;
                }

                RaisePropertyChanging(DivInfosPropertyName);
                divinfos = value;
                RaisePropertyChanged(DivInfosPropertyName);
            }
        }
        private void UnRegist()
        {
            MessengerInstance.Unregister(this);
        }

        private void ValueChanged()
        {
            if (!string.IsNullOrEmpty(divinfos))
            {
                string[] infos = divinfos.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                float len = (float)CurrentPosition;
                float start = StartPosition;
                currentsectionum = orisectionum;

                for (int i = 0; i < infos.Length; i++)
                {
                    if (float.Parse(infos[i].Split(':')[0].Split('+')[1]) >= start * 1000 + len)
                    {
                        CurrentLC = string.Format("{0} {1}", infos[i].Split(':')[0].Split('+')[0],
                        string.Format("{0}+{1}", (start + CurrentPosition / 1000).ToString("F3").Split('.')[0],
                        (start + CurrentPosition / 1000).ToString("F3").Split('.')[1]));
                        break;
                    }
                    else
                    {
                        len = (float)(CurrentPosition - 
                            (float.Parse(infos[i].Split(':')[0].Split('+')[1]) - start * 1000));
                        start = float.Parse(infos[i].Split(':')[1].Split('+')[1]) / 1000;
                        currentsectionum += 1;
                        if (i == infos.Length - 1)
                        {
                            CurrentLC = string.Format("{0} {1}", infos[i].Split(':')[0].Split('+')[0],
                        string.Format("{0}+{1}", (start + len / 1000).ToString("F3").Split('.')[0],
                        (start + len / 1000).ToString("F3").Split('.')[1]));
                            break;
                        }                        
                    }
                }                
            }
        }
        private RelayCommand _InsertCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand InsertCommand
        {
            get
            {
                return _InsertCommand
                    ?? (_InsertCommand = new RelayCommand(
                                          () =>
                                          {
                                              if (CurrentPosition < 0)
                                                  return;
                                              MessengerInstance.Send<string>(string.Format("{0}:{1}:{2}",
                                                  CurrentLC.Split(' ')[1].Replace('+', '.'),
                                                  currentsectionum.ToString(), "2"),
                                                  "InsertXinhao");
                                              MessengerInstance.Send<bool?>(true, "status");
                                          }));
            }
        }
        private RelayCommand _CancelCommand;

        /// <summary>
        /// Gets the CancelCommand.
        /// </summary>
        public RelayCommand CancelCommand
        {
            get
            {
                return _CancelCommand
                    ?? (_CancelCommand = new RelayCommand(
                                          () =>
                                          {
                                              MessengerInstance.Send<bool?>(true, "status");
                                          }));
            }
        }
        private RelayCommand _UnRegistCommand;

        /// <summary>
        /// Gets the unregistCommand.
        /// </summary>
        public RelayCommand unregistCommand
        {
            get
            {
                return _UnRegistCommand
                    ?? (_UnRegistCommand = new RelayCommand(
                                          () =>
                                          {
                                              UnRegist();
                                          }));
            }
        }
        private RelayCommand _ValueChangedCommand;

        /// <summary>
        /// Gets the ValueChangedCommand.
        /// </summary>
        public RelayCommand ValueChangedCommand
        {
            get
            {
                return _ValueChangedCommand
                    ?? (_ValueChangedCommand = new RelayCommand(
                                          () =>
                                          {
                                              ValueChanged();

                                          }));
            }
        }
    }
}
