using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.ViewModel
{
    public class CalculeteAverageGradeViewModel : ViewModelBase
    {
        /// <summary>
        /// The <see cref="SignalInfo" /> property's name.
        /// </summary>
        public const string SignalInfoPropertyName = "SignalInfo";

        private string _signalInfo = string.Empty;

        /// <summary>
        /// Sets and gets the SignalInfo property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SignalInfo
        {
            get
            {
                return _signalInfo;
            }

            set
            {
                if (_signalInfo == value)
                {
                    return;
                }

                _signalInfo = value;
                RaisePropertyChanged(SignalInfoPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="MoreInfos" /> property's name.
        /// </summary>
        public const string MoreInfosPropertyName = "MoreInfos";

        private string _moreInfos = string.Empty;

        /// <summary>
        /// Sets and gets the MoreInfos property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string MoreInfos
        {
            get
            {
                return _moreInfos;
            }

            set
            {
                if (_moreInfos == value)
                {
                    return;
                }

                _moreInfos = value;
                RaisePropertyChanged(MoreInfosPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="PositiveToZero" /> property's name.
        /// </summary>
        public const string PositiveToZeroPropertyName = "PositiveToZero";

        private bool _positiveToZero = false;

        /// <summary>
        /// Sets and gets the PositiveToZero property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool PositiveToZero
        {
            get
            {
                return _positiveToZero;
            }

            set
            {
                if (_positiveToZero == value)
                {
                    return;
                }

                _positiveToZero = value;
                RaisePropertyChanged(PositiveToZeroPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CountOfBlocks" /> property's name.
        /// </summary>
        public const string CountOfBlocksPropertyName = "CountOfBlocks";

        private string _countOfBlocks = "0";

        /// <summary>
        /// Sets and gets the CountOfBlocks property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string CountOfBlocks
        {
            get
            {
                return _countOfBlocks;
            }

            set
            {
                if (_countOfBlocks == value)
                {
                    return;
                }

                _countOfBlocks = value;
                RaisePropertyChanged(CountOfBlocksPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="AverageGrade" /> property's name.
        /// </summary>
        public const string AverageGradePropertyName = "AverageGrade";

        private decimal _averageGrade = 0;

        /// <summary>
        /// Sets and gets the AverageGrade property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public decimal AverageGrade
        {
            get
            {
                return _averageGrade;
            }

            set
            {
                if (_averageGrade == value)
                {
                    return;
                }

                _averageGrade = value;
                RaisePropertyChanged(AverageGradePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CanCalculate" /> property's name.
        /// </summary>
        public const string CanCalculatePropertyName = "CanCalculate";

        private bool _canCalculate = false;

        /// <summary>
        /// Sets and gets the CanCalculate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool CanCalculate
        {
            get
            {
                return _canCalculate;
            }

            set
            {
                if (_canCalculate == value)
                {
                    return;
                }

                _canCalculate = value;
                RaisePropertyChanged(CanCalculatePropertyName);
            }
        }

        private List<string> blocks;

        public List<string> Blocks
        {
            get { return blocks; }
            set { blocks = value; }
        }


        public CalculeteAverageGradeViewModel()
        {
            MessengerInstance.Register<string>(this, "GetAverageGrade", 
                p =>
                {
                    AverageGrade = decimal.Parse(p);
                });

            MessengerInstance.Register<string>(this, "GetSignalInfo",
                p => 
                {
                    string[] parts = p.Split('^');
                    CanCalculate = parts[1].Equals("f") ? false : true;

                    if (CanCalculate)
                        SignalInfo = string.Format("当前信号机：{0}", parts[0].Split(':')[1]);
                    else
                        SignalInfo = parts[0];

                });

            MessengerInstance.Register<string>(this, "GetMoreInfos",
                p =>
                {
                    MoreInfos = p;
                });

            Blocks = new List<string>();

            for (int i = 1; i <= 10; i++)
            {
                Blocks.Add(i.ToString());
            }
        }

        private void Calculate()
        {
            MessengerInstance.Send(string.Format("{0}:{1}", CountOfBlocks, PositiveToZero ? "1" : "0"),
                "sendCB&PTZ");
        }

        private RelayCommand _calculateCommand;

        /// <summary>
        /// Gets the CalculateCommand.
        /// </summary>
        public RelayCommand CalculateCommand
        {
            get
            {
                return _calculateCommand
                    ?? (_calculateCommand = new RelayCommand(
                    () =>
                    {
                        Calculate();
                    }));
            }
        }
    }
}
