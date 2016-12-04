using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inter_face.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Inter_face.ViewModel
{
   public class TractionPowerArrayViewModel : ViewModelBase
    {
        private ObservableCollection<TractionPowerModel> tpModel;

        public ObservableCollection<TractionPowerModel> TpModel
        {
            get { return tpModel; }
            set { tpModel = value; }
        }

        /// <summary>
        /// The <see cref="Index" /> property's name.
        /// </summary>
        public const string IndexPropertyName = "Index";

        private string _index = "1";

        /// <summary>
        /// Sets and gets the Index property.
        /// 把位
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Index
        {
            get
            {
                return _index;
            }

            set
            {
                if (_index == value)
                {
                    return;
                }

                _index = value;
                RaisePropertyChanged(IndexPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CurrentSpeed" /> property's name.
        /// </summary>
        public const string CurrentSpeedPropertyName = "CurrentSpeed";

        private int _currentSpeed = 0;

        /// <summary>
        /// Sets and gets the CurrentSpeed property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int CurrentSpeed
        {
            get
            {
                return _currentSpeed;
            }

            set
            {
                if (_currentSpeed == value)
                {
                    return;
                }

                _currentSpeed = value;
                RaisePropertyChanged(CurrentSpeedPropertyName);
            }
        }

        public TractionPowerArrayViewModel()
        {
            TpModel = new ObservableCollection<TractionPowerModel>();
            CurrentSpeed = 0;
            Index = "1";
        }

        private void addpower()
        {
            TpModel.Add(new TractionPowerModel() { IsinflectionPoint = false, Power = "", Speed = "" });
        }

        private void deletepower()
        {
            if (CurrentSpeed != -1)
            {
                TpModel.RemoveAt(CurrentSpeed);
                if (TpModel.Count != 0)
                {
                    CurrentSpeed = 0;
                }
            }
        }

        private RelayCommand _deleteArrayCommand;

        /// <summary>
        /// Gets the DeleteArrayCommand.
        /// </summary>
        public RelayCommand DeleteArrayCommand
        {
            get
            {
                return _deleteArrayCommand
                    ?? (_deleteArrayCommand = new RelayCommand(
                    () =>
                    {
                        deletepower();
                    }));
            }
        }

        private RelayCommand _addArrayCommand;

        /// <summary>
        /// Gets the AddArrayCommand.
        /// </summary>
        public RelayCommand AddArrayCommand
        {
            get
            {
                return _addArrayCommand
                    ?? (_addArrayCommand = new RelayCommand(
                    () =>
                    {
                        addpower();
                    }));
            }
        }
    }
}
