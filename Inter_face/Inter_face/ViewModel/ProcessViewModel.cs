using ExtractData;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inter_face.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;


namespace Inter_face.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ProcessViewModel : ViewModelBase
    {
        /// <summary>
        /// The <see cref="ProPersent" /> property's name.
        /// </summary>
        public const string ProPersentPropertyName = "ProPersent";

        private int _propersentProperty = 0;

        /// <summary>
        /// Sets and gets the ProPersent property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int ProPersent
        {
            get
            {
                return _propersentProperty;
            }

            set
            {
                if (_propersentProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(ProPersentPropertyName);
                _propersentProperty = value;
                RaisePropertyChanged(ProPersentPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Message" /> property's name.
        /// </summary>
        public const string MessagePropertyName = "Message";

        private string _messageProperty = string.Empty;

        /// <summary>
        /// Sets and gets the Message property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Message
        {
            get
            {
                return _messageProperty;
            }

            set
            {
                if (_messageProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(MessagePropertyName);
                _messageProperty = value;
                RaisePropertyChanged(MessagePropertyName);
            }
        }
        /// <summary>
        /// Initializes a new instance of the ProcessViewModel class.
        /// </summary>
        public ProcessViewModel()
        {
            ProPersent = 0;
            Message = string.Empty;
            MessengerInstance.Register<int>(this, "processes", p => { ProPersent = p; });
            MessengerInstance.Register<string>(this, "msg", p => { Message = p; });
        }

        private void Unreg()
        {
            MessengerInstance.Unregister<int>(this, "processes");
            MessengerInstance.Unregister<string>(this, "msg");
        }

        private RelayCommand _closeCommand;

        /// <summary>
        /// Gets the CloseCommand.
        /// </summary>
        public RelayCommand CloseCommand
        {
            get
            {
                return _closeCommand
                    ?? (_closeCommand = new RelayCommand(
                                          () =>
                                          {
                                              Unreg();
                                          }));
            }
        }
    }
}