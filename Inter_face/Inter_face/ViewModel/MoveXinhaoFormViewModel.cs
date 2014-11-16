using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;

namespace Inter_face.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MoveXinhaoFormViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MoveXinhaoFormViewModel class.
        /// </summary>
        public MoveXinhaoFormViewModel()
        {
            MessengerInstance.Register<string>(this, "Resources", 
                (p) => 
                {
                    string[] parts = p.Split(':');
                    StartPosition = float.Parse(parts[0]);
                    EndPosition = float.Parse(parts[1]);
                    MaxValue = (int)Math.Round(EndPosition * 1000 - float.Parse(parts[2]) * 1000 - float.Parse(parts[4]), 0);
                    MinValue = -(int)Math.Round(float.Parse(parts[2]) * 1000 - StartPosition * 1000 - float.Parse(parts[3]), 0);
                    //CurrentPosition = StartPosition * 1000;
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
        private void UnRegist()
        {
            MessengerInstance.Unregister(this);
        }
        private RelayCommand _MoveCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand MoveCommand
        {
            get
            {
                return _MoveCommand
                    ?? (_MoveCommand = new RelayCommand(
                                          () =>
                                          {
                                              MessengerInstance.Send<double>(CurrentPosition, "Distence");                                             
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
    }
}