using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inter_face.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inter_face.ViewModel
{
    public class SpeedBreakOperateViewModel : ViewModelBase
    {
        private ObservableCollection<Models.Speed_breakMode> s_bPair;

        public ObservableCollection<Models.Speed_breakMode> S_BPair
        {
            get { return s_bPair; }
            set { s_bPair = value; }
        }

        /// <summary>
        /// The <see cref="CanEdit" /> property's name.
        /// </summary>
        public const string CanEditPropertyName = "CanEdit";

        private bool _caneditProperty = false;

        /// <summary>
        /// Sets and gets the CanEdit property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool CanEdit
        {
            get
            {
                return _caneditProperty;
            }

            set
            {
                if (_caneditProperty == value)
                {
                    return;
                }

                _caneditProperty = value;
                RaisePropertyChanged(CanEditPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CurrentIndex" /> property's name.
        /// </summary>
        public const string CurrentIndexPropertyName = "CurrentIndex";

        private int _currentindexProperty = -1;

        /// <summary>
        /// Sets and gets the CurrentIndex property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int CurrentIndex
        {
            get
            {
                return _currentindexProperty;
            }

            set
            {
                if (_currentindexProperty == value)
                {
                    return;
                }

                _currentindexProperty = value;
                RaisePropertyChanged(CurrentIndexPropertyName);
            }
        }

        public SpeedBreakOperateViewModel()
        {
            S_BPair = new ObservableCollection<Speed_breakMode>();

            MessengerInstance.Register<ObservableCollection<Speed_breakMode>>(this, "sb",
                (p) =>
                {
                    foreach (Speed_breakMode item in p)
                    {
                        S_BPair.Add(item);
                    }
                });

            MessengerInstance.Register<bool>(this, "canEdit", (p) => { CanEdit = p; });

            S_BPair = new ObservableCollection<Models.Speed_breakMode>();
        }

        private void deleteData()
        {
            if (CurrentIndex != -1)
            {
                S_BPair.RemoveAt(CurrentIndex);
                if (S_BPair.Count != 0)
                {
                    CurrentIndex = 0;
                }
            }
        }

        private void addData()
        {
            S_BPair.Add(new Speed_breakMode() { Break = string.Empty, Speed = string.Empty });
        }

        private void edit()
        {
            CanEdit = true;
        }

        private void sendMesseng()
        {
            MessengerInstance.Send(s_bPair, "updatasb");
        }

        private void Unregist()
        {
            MessengerInstance.Unregister(this);            
        }

        private void pasteData()
        {
            string str = System.Windows.Clipboard.GetText();//获取剪切板数据
            s_bPair.Clear();

            if (!string.IsNullOrEmpty(str.Trim()))
            {
                string[] rows = str.Split('\n');

                foreach (string row in rows)
                {
                    string[] cols;
                    if (!string.IsNullOrEmpty(row.Trim()))
                    {
                        cols = row.Split('\t');
                        if (cols.Length >= 2)
                            s_bPair.Add(new Speed_breakMode() { Break = cols[1].Trim(), Speed = cols[0].Trim() });
                    }
                }

                System.Windows.Clipboard.SetDataObject(string.Empty, false);
            }
        }

        private RelayCommand _deleteCommand;

        /// <summary>
        /// Gets the DeleteCommand.
        /// </summary>
        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand
                    ?? (_deleteCommand = new RelayCommand(
                    () =>
                    {
                        deleteData();
                    },
                    () => s_bPair.Count != 0)); ;
            }
        }

        private RelayCommand _addCommand;

        /// <summary>
        /// Gets the AddCommand.
        /// </summary>
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand
                    ?? (_addCommand = new RelayCommand(
                    () =>
                    {
                        addData();
                    }));
            }
        }

        private RelayCommand _saveCommand;

        /// <summary>
        /// Gets the SaveCommand.
        /// </summary>
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand
                    ?? (_saveCommand = new RelayCommand(
                    () =>
                    {
                        sendMesseng();
                    }));
            }
        }

        private RelayCommand _unRegistCommand;

        /// <summary>
        /// Gets the UnregistCommand.
        /// </summary>
        public RelayCommand UnregistCommand
        {
            get
            {
                return _unRegistCommand
                    ?? (_unRegistCommand = new RelayCommand(
                    () =>
                    {
                        Unregist();
                    }));
            }
        }

        private RelayCommand _pasteDataCommand;

        /// <summary>
        /// Gets the PasteDataCommand.
        /// </summary>
        public RelayCommand PasteDataCommand
        {
            get
            {
                return _pasteDataCommand
                    ?? (_pasteDataCommand = new RelayCommand(
                    () =>
                    {
                        pasteData();
                    },
                    () =>
                    {
                        return !string.IsNullOrEmpty(System.Windows.Clipboard.GetText()) && CanEdit;
                    }
                    ));
            }
        }
    }
}
