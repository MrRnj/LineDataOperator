using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Models
{
   public class StationDataMode : ViewModelBase, Inter_face.Models.IDataModel
    {
        public DataType Type { get; set; }   
       
        /// <summary>
        /// The <see cref="PathDataProperty" /> property's name.
        /// </summary>
        public const string PathDataPropertyPropertyName = "PathDataProperty";

        private string _PathDataProperty = string.Empty;

        /// <summary>
        /// Sets and gets the PathDataProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string PathDataProperty
        {
            get
            {
                return _PathDataProperty;
            }

            set
            {
                if (_PathDataProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(PathDataPropertyPropertyName);
                _PathDataProperty = value;
                RaisePropertyChanged(PathDataPropertyPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="PositionProperty" /> property's name.
        /// </summary>
        public const string PositionPropertyPropertyName = "PositionProperty";

        private float _PositionProperty = 0;

        /// <summary>
        /// Sets and gets the PositionProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>       
        public float PositionProperty
        {
            get
            {
                return _PositionProperty;
            }

            set
            {
                if (_PositionProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(PositionPropertyPropertyName);
                _PositionProperty = value;
                RaisePropertyChanged(PositionPropertyPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedProperty" /> property's name.
        /// </summary>
        public const string SelectedPropertyPropertyName = "SelectedProperty";

        private bool _SelectedProperty = false;

        /// <summary>
        /// Sets and gets the SelectedProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool SelectedProperty
        {
            get
            {
                return _SelectedProperty;
            }

            set
            {
                if (_SelectedProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(SelectedPropertyPropertyName);
                _SelectedProperty = value;
                RaisePropertyChanged(SelectedPropertyPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="SectionNumProperty" /> property's name.
        /// </summary>
        public const string SectionNumPropertyPropertyName = "SectionNumProperty";

        private int _SectionNumProperty = 1;

        /// <summary>
        /// Sets and gets the SectionNumProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int SectionNumProperty
        {
            get
            {
                return _SectionNumProperty;
            }

            set
            {
                if (_SectionNumProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(SectionNumPropertyPropertyName);
                _SectionNumProperty = value;
                RaisePropertyChanged(SectionNumPropertyPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="ScaleProperty" /> property's name.
        /// </summary>
        public const string ScalePropertyPropertyName = "ScaleProperty";

        private int _ScaleProperty = 1;

        /// <summary>
        /// Sets and gets the ScaleProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int ScaleProperty
        {
            get
            {
                return _ScaleProperty;
            }

            set
            {
                if (_ScaleProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(ScalePropertyPropertyName);
                if (value < 0)
                {
                    _ScaleProperty = 1;
                }
                else
                    _ScaleProperty = value;
                RaisePropertyChanged(ScalePropertyPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="HatProperty" /> property's name.
        /// </summary>
        public const string HatPropertyPropertyName = "HatProperty";

        private string _HatProperty = string.Empty;

        /// <summary>
        /// Sets and gets the HatProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string HatProperty
        {
            get
            {
                return _HatProperty;
            }

            set
            {
                if (_HatProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(HatPropertyPropertyName);
                _HatProperty = value;
                RaisePropertyChanged(HatPropertyPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="StationNameProperty" /> property's name.
        /// </summary>
        public const string StationNamePropertyPropertyName = "StationNameProperty";

        private string _stationnameProperty = "车站";

        /// <summary>
        /// Sets and gets the StationNameProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string StationNameProperty
        {
            get
            {
                return _stationnameProperty;
            }

            set
            {
                if (_stationnameProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(StationNamePropertyPropertyName);
                _stationnameProperty = value;
                RaisePropertyChanged(StationNamePropertyPropertyName);
            }
        }
        public string ChangeData(float oriheight, float oriposition, float length, float angle)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The <see cref="LengthProperty" /> property's name.
        /// </summary>
        public const string LengthPropertyPropertyName = "LengthProperty";

        private float _lengthProperty = 0;

        /// <summary>
        /// 几何长度，UI长度
        /// Sets and gets the LengthProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public float LengthProperty
        {
            get
            {
                return _lengthProperty;
            }

            set
            {
                if (_lengthProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(LengthPropertyPropertyName);
                _lengthProperty = value;
                RaisePropertyChanged(LengthPropertyPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="RealLength" /> property's name.
        /// </summary>
        public const string RealLengthPropertyName = "RealLength";

        private float _reallengthProperty = 0;

        /// <summary>
        /// 实际长度
        /// Sets and gets the RealLength property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public float RealLength
        {
            get
            {
                return _reallengthProperty;
            }

            set
            {
                if (_reallengthProperty == value)
                {
                    return;
                }

                _reallengthProperty = value;
                RaisePropertyChanged(RealLengthPropertyName);
            }
        }
    }
}
