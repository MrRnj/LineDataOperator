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

namespace Inter_face.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AcdlViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the AcdlViewModel class.
        /// </summary>
        /// 
        /// <summary>
        /// The <see cref="Hat_Front" /> property's name.
        /// </summary>
        public const string Hat_FrontPropertyName = "Hat_Front";

        private string _hatfrontProperty = string.Empty;

        /// <summary>
        /// Sets and gets the Hat_Front property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Hat_Front
        {
            get
            {
                return _hatfrontProperty;
            }

            set
            {
                if (_hatfrontProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(Hat_FrontPropertyName);
                _hatfrontProperty = value;
                RaisePropertyChanged(Hat_FrontPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Hat_After" /> property's name.
        /// </summary>
        public const string Hat_AfterPropertyName = "Hat_After";

        private string _hatafterProperty = string.Empty;

        /// <summary>
        /// Sets and gets the Hat_After property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Hat_After
        {
            get
            {
                return _hatafterProperty;
            }

            set
            {
                if (_hatafterProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(Hat_AfterPropertyName);
                _hatafterProperty = value;
                RaisePropertyChanged(Hat_AfterPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Fst_Front" /> property's name.
        /// </summary>
        public const string Fst_FrontPropertyName = "Fst_Front";

        private string _fstfrontProperty = "-1";

        /// <summary>
        /// Sets and gets the Fst_Front property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Fst_Front
        {
            get
            {
                return _fstfrontProperty;
            }

            set
            {
                if (_fstfrontProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(Fst_FrontPropertyName);
                _fstfrontProperty = value;
                RaisePropertyChanged(Fst_FrontPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Sec_Front" /> property's name.
        /// </summary>
        public const string Sec_FrontPropertyName = "Sec_Front";

        private string _secfrontProperty = "-1";

        /// <summary>
        /// Sets and gets the Sec_Front property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Sec_Front
        {
            get
            {
                return _secfrontProperty;
            }

            set
            {
                if (_secfrontProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(Sec_FrontPropertyName);
                _secfrontProperty = value;
                RaisePropertyChanged(Sec_FrontPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Fst_After" /> property's name.
        /// </summary>
        public const string Fst_AfterPropertyName = "Fst_After";

        private string _fstafterProperty = "-1";

        /// <summary>
        /// Sets and gets the Fst_After property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Fst_After
        {
            get
            {
                return _fstafterProperty;
            }

            set
            {
                if (_fstafterProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(Fst_AfterPropertyName);
                _fstafterProperty = value;
                RaisePropertyChanged(Fst_AfterPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Sec_After" /> property's name.
        /// </summary>
        public const string Sec_AfterPropertyName = "Sec_After";

        private string _secafterProperty = "-1";

        /// <summary>
        /// Sets and gets the Sec_After property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Sec_After
        {
            get
            {
                return _secafterProperty;
            }

            set
            {
                if (_secafterProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(Sec_AfterPropertyName);
                _secafterProperty = value;
                RaisePropertyChanged(Sec_AfterPropertyName);
            }
        }
        public AcdlViewModel()
        {
        }

        private void AddCdl()
        {
            string msg = string.Format("{0}+{1}:{2}+{3}",
                Hat_Front,
                (decimal.Parse(Fst_Front) * 1000 + decimal.Parse(float.Parse(Sec_Front).ToString("#0.000"))).ToString(),
                Hat_After,
                (decimal.Parse(Fst_After) * 1000 + decimal.Parse(float.Parse(Sec_After).ToString("#0.000"))).ToString());
            MessengerInstance.Send(msg, "cdl");
        }

        private bool CanAddCdl()
        {
            try
            {
                return !string.IsNullOrEmpty(Hat_After) &&
                       !string.IsNullOrEmpty(Hat_Front) &&
                        float.Parse(Fst_Front) >= 0 &&
                        !Fst_Front.Contains('.') &&
                        float.Parse(Sec_Front) >= 0 &&
                        float.Parse(Sec_Front) < 1000 &&
                        float.Parse(Fst_After) >= 0 &&
                        !Fst_After.Contains('.') &&
                        float.Parse(Sec_After) >= 0 &&
                        float.Parse(Sec_After) < 1000;

            }
            catch
            {
                return false;
            }
        }

        private RelayCommand _addcdlCommand;

        /// <summary>
        /// Gets the AddCdlCommand.
        /// </summary>
        public RelayCommand AddCdlCommand
        {
            get
            {
                return _addcdlCommand
                    ?? (_addcdlCommand = new RelayCommand(
                                          () =>
                                          {
                                              AddCdl();
                                          },
                                          () => CanAddCdl()));
            }
        }
    }
}