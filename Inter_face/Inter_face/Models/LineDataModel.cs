using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Models
{
    internal class LineDataModel : ViewModelBase, Inter_face.Models.IDataModel
    {
        public DataType Type { get; set; }

        /// <summary>
        /// The <see cref="LengthProperty" /> property's name.
        /// </summary>
        public const string LengthPropertyPropertyName = "LengthProperty";

        private float _lengthProperty = 0;

        /// <summary>
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
        /// The <see cref="RadioProperty" /> property's name.
        /// </summary>
        public const string RadioPropertyPropertyName = "RadioProperty";

        private int _RadioProperty = 0;

        /// <summary>
        /// Sets and gets the RadioProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int RadioProperty
        {
            get
            {
                return _RadioProperty;
            }

            set
            {
                if (_RadioProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(RadioPropertyPropertyName);
                _RadioProperty = value;
                RaisePropertyChanged(RadioPropertyPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="HeightProperty" /> property's name.
        /// </summary>
        public const string HeightPropertyPropertyName = "HeightProperty";

        private float _HeightProperty = 0;

        /// <summary>
        /// Sets and gets the HeightProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public float HeightProperty
        {
            get
            {
                return _HeightProperty;
            }

            set
            {
                if (_HeightProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(HeightPropertyPropertyName);
                _HeightProperty = value;
               
                RaisePropertyChanged(HeightPropertyPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="AngleProperty" /> property's name.
        /// </summary>
        public const string AnglePropertyPropertyName = "AngleProperty";

        private float _AngleProperty = 0;

        /// <summary>
        /// Sets and gets the AngleProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public float AngleProperty
        {
            get
            {
                return _AngleProperty;
            }

            set
            {
                if (_AngleProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(AnglePropertyPropertyName);
                _AngleProperty = value;
                //HeightProperty = PositionProperty + LengthProperty * _AngleProperty;
                RaisePropertyChanged(AnglePropertyPropertyName);
            }
        }

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
        /// The <see cref="EndPositionProperty" /> property's name.
        /// </summary>
        public const string EndPositionPropertyPropertyName = "EndPositionProperty";

        private float _endpostionProperty = 0;

        /// <summary>
        /// Sets and gets the EndPositionProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public float EndPositionProperty
        {
            get
            {
                return _endpostionProperty;
            }

            set
            {
                if (_endpostionProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(EndPositionPropertyPropertyName);
                _endpostionProperty = value;
                RaisePropertyChanged(EndPositionPropertyPropertyName);
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


        public string ChangeData(float oriheight, float oriposition, float length, float angle)
        {
            throw new NotImplementedException();
        }
    }   
}
