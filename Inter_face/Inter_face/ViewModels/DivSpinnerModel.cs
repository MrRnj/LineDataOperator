using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;


namespace Inter_face
{
    public class DivSpinnerModel :  ViewModelBase
    {
        public DivSpinnerModel()
        {

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

        private float _enpositionProperty = 0;

        /// <summary>
        /// Sets and gets the EndPosition property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public float EndPosition
        {
            get
            {
                return _enpositionProperty;
            }

            set
            {
                if (_enpositionProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(EndPositionPropertyName);
                _enpositionProperty = value;
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
    }
}