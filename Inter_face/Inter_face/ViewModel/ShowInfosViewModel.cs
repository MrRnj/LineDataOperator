using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.ViewModel
{
    public class ShowInfosViewModel : ViewModelBase
    {
        /// <summary>
        /// The <see cref="Infos" /> property's name.
        /// </summary>
        public const string InfosPropertyName = "Infos";

        private string _infosProperty = "";

        /// <summary>
        /// Sets and gets the Infos property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Infos
        {
            get
            {
                return _infosProperty;
            }

            set
            {
                if (_infosProperty == value)
                {
                    return;
                }

                _infosProperty = value;
                RaisePropertyChanged(InfosPropertyName);
            }
        }

        public ShowInfosViewModel()
        {
            MessengerInstance.Register<string>(this, "showinfos",
                p =>
                {
                    Infos += string.Format("{0}{1}", "\r\n", p);
                });
        }

        private RelayCommand _unRegistedCommand;

        /// <summary>
        /// Gets the UnRegistedCommand.
        /// </summary>
        public RelayCommand UnRegistedCommand
        {
            get
            {
                return _unRegistedCommand
                    ?? (_unRegistedCommand = new RelayCommand(
                    () =>
                    {
                        MessengerInstance.Unregister(this);
                        
                    }));
            }
        }
    }
}
