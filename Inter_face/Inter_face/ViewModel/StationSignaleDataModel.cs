using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Models
{
    public class StationSignaleDataModel : ViewModelBase
    {
        public string StationNameProperty { get; set; }
        public string StationPositionProperty { get; set; }      
        

        /// <summary>
        /// The <see cref="InSignalProperty" /> property's name.
        /// </summary>
        public const string InSignalPropertyPropertyName = "InSignalProperty";

        private SignalDataViewModel _insignalProperty = null;

        /// <summary>
        /// Sets and gets the InSignalProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public SignalDataViewModel InSignalProperty
        {
            get
            {
                return _insignalProperty;
            }

            set
            {
                if (_insignalProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(InSignalPropertyPropertyName);
                _insignalProperty = value;
                RaisePropertyChanged(InSignalPropertyPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="OutSignalProperty" /> property's name.
        /// </summary>
        public const string OutSignalPropertyPropertyName = "OutSignalProperty";

        private SignalDataViewModel _outsignalProperty = null;

        /// <summary>
        /// Sets and gets the OutSignalProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public SignalDataViewModel OutSignalProperty
        {
            get
            {
                return _outsignalProperty;
            }

            set
            {
                if (_outsignalProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(OutSignalPropertyPropertyName);
                _outsignalProperty = value;
                RaisePropertyChanged(OutSignalPropertyPropertyName);
            }
        }
    }
}
