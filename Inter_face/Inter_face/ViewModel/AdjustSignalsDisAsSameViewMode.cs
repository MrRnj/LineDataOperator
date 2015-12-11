using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.ViewModel
{
    public class AdjustSignalsDisAsSameViewMode : ViewModelBase
    {
       
        /// <summary>
        /// The <see cref="Dis" /> property's name.
        /// </summary>
        public const string DisPropertyName = "Dis";

        private string _disProperty = "0";

        /// <summary>
        /// Sets and gets the Hat_Front property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Dis
        {
            get
            {
                return _disProperty;
            }

            set
            {
                if (_disProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(DisPropertyName);
                _disProperty = value;
                RaisePropertyChanged(DisPropertyName);
            }
        }

        private void Adjust()
        {
            MessengerInstance.Send<float>(float.Parse(Dis), "AdjustSignalsDis");
        }

        private bool CanAdjust()
        {
            try
            {
                return !Dis.Contains(".") && float.Parse(Dis) >= 0;
            }
            catch
            {
                return false;
            }
        }

        private RelayCommand _adjustCommand;

        /// <summary>
        /// Gets the AddCdlCommand.
        /// </summary>
        public RelayCommand AdjustCommand
        {
            get
            {
                return _adjustCommand
                    ?? (_adjustCommand = new RelayCommand(
                                          () =>
                                          {
                                              Adjust();
                                          },
                                          () => CanAdjust()));
            }
        }        
    }
}
