using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Inter_face.ViewModel
{
    public class ShowCellDetailInfoViewModel : ViewModelBase
    {
        /// <summary>
        /// The <see cref="DetailInfo" /> property's name.
        /// </summary>
        public const string DetailInfoPropertyName = "DetailInfo";

        private string _detailInfo = string.Empty;

        /// <summary>
        /// Sets and gets the DetailInfo property.
        /// Changes to that property's value raise the PropertyChange  xcd event. 
        /// </summary>
        public string DetailInfo
        {
            get
            {
                return _detailInfo;
            }

            set
            {
                if (_detailInfo == value)
                {
                    return;
                }

                _detailInfo = value;
                RaisePropertyChanged(DetailInfoPropertyName);
            }
        }

        public ShowCellDetailInfoViewModel()
        {
            DetailInfo = "无信息";
            MessengerInstance.Register<string>(this, "CellDetailInfo", p => { DetailInfo = p; });
        }

        private RelayCommand _closedCommand;

        /// <summary>
        /// Gets the ClosedCommand.
        /// </summary>
        public RelayCommand ClosedCommand
        {
            get
            {
                return _closedCommand
                    ?? (_closedCommand = new RelayCommand(
                    () =>
                    {
                        MessengerInstance.Unregister(this);
                    }));
            }
        }
    }
}
