using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Models
{
    public class DianFXModel : ViewModelBase
    {
        private string fxName;

        public string FxNane
        {
            get { return fxName; }
            set { fxName = value; }
        }

        private string midPosition;

        public string MidPosition
        {
            get { return midPosition; }
            set { midPosition = value; }
        }

        /// <summary>
        /// The <see cref="FrontPosition" /> 断标位置
        /// </summary>
        public const string FrontPositionPropertyName = "FrontPosition";

        private string frontPosition = string.Empty;

        /// <summary>
        /// Sets and gets the FrontPosition property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FrontPosition
        {
            get
            {
                return frontPosition;
            }

            set
            {
                if (frontPosition == value)
                {
                    return;
                }

                frontPosition = value;
                RaisePropertyChanged(FrontPositionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="FrontDis" /> 中心距断标距离.
        /// </summary>
        public const string FrontDisPropertyName = "FrontDis";

        private string _frontDis = string.Empty;

        /// <summary>
        /// Sets and gets the FrontDis property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FrontDis
        {
            get
            {
                return _frontDis;
            }

            set
            {
                if (_frontDis == value)
                {
                    return;
                }

                _frontDis = value;
                RaisePropertyChanged(FrontDisPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="BackPosition" /> 合标位置
        /// </summary>
        public const string BackPositionPropertyName = "BackPosition";

        private string backPosition = string.Empty;

        /// <summary>
        /// Sets and gets the BackPosition property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string BackPosition
        {
            get
            {
                return backPosition;
            }

            set
            {
                if (backPosition == value)
                {
                    return;
                }

                backPosition = value;
                RaisePropertyChanged(BackPositionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="BackDis" /> 合标距中心距离.
        /// </summary>
        public const string BackDisPropertyName = "BackDis";

        private string _backDis = string.Empty;

        /// <summary>
        /// Sets and gets the BackDis property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string BackDis
        {
            get
            {
                return _backDis;
            }

            set
            {
                if (_backDis == value)
                {
                    return;
                }

                _backDis = value;
                RaisePropertyChanged(BackDisPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="FrontTime" /> property's name.
        /// </summary>
        public const string FrontTimePropertyName = "FrontTime";

        private string frontTime = string.Empty;

        /// <summary>
        /// Sets and gets the FrontTime property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FrontTime
        {
            get
            {
                return frontTime;
            }

            set
            {
                if (frontTime == value)
                {
                    return;
                }

                frontTime = value;
                RaisePropertyChanged(FrontTimePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="BackTime" /> property's name.
        /// </summary>
        public const string BackTimePropertyName = "BackTime";

        private string backTime = string.Empty;

        /// <summary>
        /// Sets and gets the BackTime property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string BackTime
        {
            get
            {
                return backTime;
            }

            set
            {
                if (backTime == value)
                {
                    return;
                }

                backTime = value;
                RaisePropertyChanged(BackTimePropertyName);
            }
        }
        
    }
}
