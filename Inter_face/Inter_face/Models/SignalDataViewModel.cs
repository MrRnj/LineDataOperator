using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Models
{
   public class SignalDataViewModel : ViewModelBase
    {
        
        /// <summary>
        /// The <see cref="Guanhao" /> property's name.
        /// </summary>
        public const string GuanhaoPropertyName = "Guanhao";

        private string _guanhaoProperty = string.Empty;

        /// <summary>
        /// Sets and gets the Guanhao property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Guanhao
        {
            get
            {
                return _guanhaoProperty;
            }

            set
            {
                if (_guanhaoProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(GuanhaoPropertyName);
                _guanhaoProperty = value;
                RaisePropertyChanged(GuanhaoPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="PartImenber" /> property's name.
        /// </summary>
        public const string PartImenberPropertyName = "PartImenber";

        private int _partonemenberProperty = 0;

        /// <summary>
        /// Sets and gets the PartImenber property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int PartImenber
        {
            get
            {
                return _partonemenberProperty;
            }

            set
            {
                if (_partonemenberProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(PartImenberPropertyName);
                _partonemenberProperty = value;
                RaisePropertyChanged(PartImenberPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="PartII" /> property's name.
        /// </summary>
        public const string PartIIPropertyName = "PartII";

        private int _part2Property = 0;

        /// <summary>
        /// Sets and gets the PartII property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int PartII
        {
            get
            {
                return _part2Property;
            }

            set
            {
                if (_part2Property == value)
                {
                    return;
                }

                RaisePropertyChanging(PartIIPropertyName);
                _part2Property = value;
                RaisePropertyChanged(PartIIPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="SectionNum" /> property's name.
        /// </summary>
        public const string SectionNumPropertyName = "SectionNum";

        private string _sectionnumProperty = "0";

        /// <summary>
        /// Sets and gets the SectionNum property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SectionNum
        {
            get
            {
                return _sectionnumProperty;
            }

            set
            {
                if (_sectionnumProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(SectionNumPropertyName);
                _sectionnumProperty = value;
                if (secNumbers != null)
                    SelectedIndex = secNumbers.IndexOf(int.Parse(_sectionnumProperty == null ? "0" : _sectionnumProperty));
                RaisePropertyChanged(SectionNumPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="SelectedIndex" /> property's name.
        /// </summary>
        public const string SelectedIndexPropertyName = "SelectedIndex";

        private int _selectediindexProperty = -1;

        /// <summary>
        /// Sets and gets the SelectedIndex property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                return _selectediindexProperty;
            }

            set
            {
                if (_selectediindexProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(SelectedIndexPropertyName);
                _selectediindexProperty = value;
                RaisePropertyChanged(SelectedIndexPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsSelected" /> property's name.
        /// </summary>
        public const string IsSelectedPropertyName = "IsSelected";

        private bool _isselectedProperty = false;

        /// <summary>
        /// Sets and gets the IsSelected property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return _isselectedProperty;
            }

            set
            {
                if (_isselectedProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(IsSelectedPropertyName);
                _isselectedProperty = value;
                RaisePropertyChanged(IsSelectedPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsEnableProperty" /> property's name.
        /// </summary>
        public const string IsEnablePropertyPropertyName = "IsEnableProperty";

        private bool _isenableProperty = true;

        /// <summary>
        /// Sets and gets the IsEnableProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsEnableProperty
        {
            get
            {
                return _isenableProperty;
            }

            set
            {
                if (_isenableProperty == value)
                {
                    return;
                }

                _isenableProperty = value;
                RaisePropertyChanged(IsEnablePropertyPropertyName);
            }
        }
        private List<string> cdlInfo;

        public List<string> CdlInfoProperty
        {
            get { return cdlInfo; }
            set { cdlInfo = value; }
        }

        private List<int> secNumbers;

        public List<int> SecNumbersProperty
        {
            get { return secNumbers; }
            set { secNumbers = value; }
        }
        private string type;

        public string TypeProperty
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// The <see cref="Mark" /> property's name.
        /// </summary>
        public const string MarkPropertyName = "Mark";

        private string _markProperty = string.Empty;

        /// <summary>
        /// Sets and gets the Mark property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Mark
        {
            get
            {
                return _markProperty;
            }

            set
            {
                if (_markProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(MarkPropertyName);
                _markProperty = value;
                RaisePropertyChanged(MarkPropertyName);
            }
        }

        private RelayCommand<string> _SecNumberChangedCommand;

        /// <summary>
        /// Gets the SceNumberChangedCommand.
        /// </summary>
        public RelayCommand<string> SceNumberChangedCommand
        {
            get
            {
                return _SecNumberChangedCommand
                    ?? (_SecNumberChangedCommand = new RelayCommand<string>(
                                          (p) =>
                                          {
                                              if (p.Equals("In"))
                                                  InSectionumChanged();
                                              else
                                                  OutSectionumChanged();
                                          }));
            }
        }       

        private void OutSectionumChanged()
        {
            int sec = int.Parse(SectionNum);
            if (sec > CdlInfoProperty.Count)
                Guanhao = CdlInfoProperty[sec - 2].Split(':')[1].Split('+')[0];
            else
                Guanhao = CdlInfoProperty[sec - 1].Split(':')[0].Split('+')[0];
        }
        private void InSectionumChanged()
        {
            int sec = int.Parse(SectionNum);
            if (sec > CdlInfoProperty.Count)
                Guanhao = CdlInfoProperty[sec - 2].Split(':')[1].Split('+')[0];
            else
                Guanhao = CdlInfoProperty[sec - 1].Split(':')[0].Split('+')[0];
        }

    }
}
