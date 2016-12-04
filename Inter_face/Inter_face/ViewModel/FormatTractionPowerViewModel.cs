using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inter_face.ViewModel
{
   public class FormatTractionPowerViewModel : ViewModelBase
    {
        private ObservableCollection<TractionPowerArrayViewModel> tpCollection;

        public ObservableCollection<TractionPowerArrayViewModel> TpCollection
        {
            get { return tpCollection; }
            set { tpCollection = value; }
        }

        /// <summary>
            /// The <see cref="CurrentIndex" /> property's name.
            /// </summary>
        public const string CurrentIndexPropertyName = "CurrentIndex";

        private int _currentIndex = -1;

        /// <summary>
        /// Sets and gets the CurrentIndex property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int CurrentIndex
        {
            get
            {
                return _currentIndex;
            }

            set
            {
                if (_currentIndex == value)
                {
                    return;
                }

                _currentIndex = value;
                if (_currentIndex != -1)
                {
                    if (TpCollection.Count != 0)
                    {
                        CurrentTpArray = TpCollection[_currentIndex];
                    }
                }
                else
                {
                    CurrentTpArray = new TractionPowerArrayViewModel();
                }
                RaisePropertyChanged(CurrentIndexPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CurrentTpArray" /> property's name.
        /// </summary>
        public const string CurrentTpArrayPropertyName = "CurrentTpArray";

        private TractionPowerArrayViewModel _currentArray = new TractionPowerArrayViewModel();

        /// <summary>
        /// Sets and gets the CurrentTpArray property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public TractionPowerArrayViewModel CurrentTpArray
        {
            get
            {
                return _currentArray;
            }

            set
            {
                if (_currentArray == value)
                {
                    return;
                }

                _currentArray = value;
                RaisePropertyChanged(CurrentTpArrayPropertyName);
            }
        }

        public FormatTractionPowerViewModel()
        {
            TpCollection = new ObservableCollection<TractionPowerArrayViewModel>();

            MessengerInstance.Register<ObservableCollection<TractionPowerArrayViewModel>>(this, "getTpArray",
                p =>
                {
                    if (p == null)
                    {
                        TpCollection.Clear();
                    }
                    else
                    {
                        TpCollection.Clear();
                        foreach (TractionPowerArrayViewModel tp in p)
                        {
                            TpCollection.Add(tp);
                        }
                    }
                });
        }

        private void refreshTractionPower(string filepath)
        {
            TpCollection.Clear();

            loadTractionPowerFromfile(filepath);
        }

        private void loadTractionPowerFromfile(string filepath)
        {
            List<string> powers = new List<string>();
            List<string> inflectionpoints = new List<string>();
            int index = 1;
            int currentt = 0;
           
            string[] lines = File.ReadAllLines(filepath);
            string[] speeds = lines[0].Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 1; i < lines.Count(); i++)
            {
                int t = int.Parse(lines[i].Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                if (t > currentt)
                {
                    powers.Add(lines[i]);
                    currentt = t;
                    index++;
                    continue;
                }
                break;
            }

            for (int i = index; i < lines.Count(); i++)
            {
                inflectionpoints.Add(lines[i]);
            }

            TractionPowerArrayViewModel newTp;
            for (int i = 0; i < powers.Count(); i++)            
            {
                newTp = new TractionPowerArrayViewModel();
                string[] parts = powers[i].Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                string[] infs = inflectionpoints[i].Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                newTp.Index = parts[0];
                bool isinfs = false;

                for (int j = 2; j < parts.Count(); j++)
                {
                    for (int k = 2; k < infs.Count(); k++)
                    {
                        if (speeds[j].Equals(infs[k]))
                        {
                            isinfs = true;
                            break;
                        }
                    }

                    newTp.TpModel.Add(new Models.TractionPowerModel()
                    {
                        Power = parts[j],
                        Speed = speeds[j],
                        IsinflectionPoint = isinfs
                    });

                    isinfs = false;
                }

                TpCollection.Add(newTp);
            }
        }

        private void saveTractionPowerArray()
        {
            if (tpCollection.Count != 0)
            {
                int count = tpCollection[0].TpModel.Count;
                for (int i = 1; i < tpCollection.Count; i++)
                {
                    if (tpCollection[i].TpModel.Count != count)
                    {
                        MessageBox.Show(string.Format("第个{0}把位与上一个把位速度数量不同请核实！", i), "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            
            MessengerInstance.Send(TpCollection, "sendTpArray");
        }

        private void deleteTractionPowerArray()
        {
            if (CurrentIndex != -1)
            {
                TpCollection.RemoveAt(CurrentIndex);
                CurrentIndex = 0;
            }
        }
        
        private void addTractionPowerArray()
        {
            TractionPowerArrayViewModel newtpArray = new TractionPowerArrayViewModel();
            newtpArray.Index = "新把位";

            TpCollection.Add(newtpArray);
            CurrentIndex = TpCollection.Count - 1;
        }

        private void dropEnter(DragEventArgs e)
        {
            if (e.KeyStates == DragDropKeyStates.LeftMouseButton && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filenames = ((Array)(e.Data.GetData(DataFormats.FileDrop))) as string[];
                string end = Path.GetExtension(filenames[0]);
                if (filenames != null && filenames.All(p => Path.GetExtension(p).ToLower().Equals(".tra")))
                    e.Effects = DragDropEffects.Link;
                else
                    e.Effects = DragDropEffects.None;

            }
            else
                e.Effects = DragDropEffects.None;
        }

        private void loadformDrop(DragEventArgs e)
        {
            string[] filepathes = null;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                filepathes = (((Array)(e.Data.GetData(DataFormats.FileDrop))) as string[]);
                if (filepathes != null)
                {
                    refreshTractionPower(filepathes[0]);
                }
            }
        }

        private void unregist()
        {
            MessengerInstance.Unregister(this);
        }

        private RelayCommand<DragEventArgs> _dragenterCommand;

        /// <summary>
        /// Gets the CancelCommand.
        /// </summary>
        public RelayCommand<DragEventArgs> DragEnterCommand
        {
            get
            {
                return _dragenterCommand
                    ?? (_dragenterCommand = new RelayCommand<DragEventArgs>(
                    (e) =>
                    {
                        dropEnter(e);
                    }));
            }
        }

        private RelayCommand<DragEventArgs> _loadformDropCommand;

        /// <summary>
        /// Gets the CancelCommand.
        /// </summary>
        public RelayCommand<DragEventArgs> LoadFormDropCommand
        {
            get
            {
                return _loadformDropCommand
                    ?? (_loadformDropCommand = new RelayCommand<DragEventArgs>(
                    (e) =>
                    {
                        loadformDrop(e);
                    }));
            }
        }

        private RelayCommand _savetpCommand;

        /// <summary>
        /// Gets the SaveTpCommand.
        /// </summary>
        public RelayCommand SaveTpCommand
        {
            get
            {
                return _savetpCommand
                    ?? (_savetpCommand = new RelayCommand(
                    () =>
                    {
                        saveTractionPowerArray();
                    }));
            }
        }

        private RelayCommand _unregistCommand;

        /// <summary>
        /// Gets the UnRegistCommand.
        /// </summary>
        public RelayCommand UnRegistCommand
        {
            get
            {
                return _unregistCommand
                    ?? (_unregistCommand = new RelayCommand(
                    () =>
                    {
                        unregist();
                    }));
            }
        }

        private RelayCommand _DeleteTpArrayCommand;

        /// <summary>
        /// Gets the DeleteTpArrayCommand.
        /// </summary>
        public RelayCommand DeleteTpArrayCommand
        {
            get
            {
                return _DeleteTpArrayCommand
                    ?? (_DeleteTpArrayCommand = new RelayCommand(
                    () =>
                    {
                        deleteTractionPowerArray();
                    }));
            }
        }

        private RelayCommand _AddTpArrayCommand;

        /// <summary>
        /// Gets the AddTpArrayCommand.
        /// </summary>
        public RelayCommand AddTpArrayCommand
        {
            get
            {
                return _AddTpArrayCommand
                    ?? (_AddTpArrayCommand = new RelayCommand(
                    () =>
                    {
                        addTractionPowerArray();
                    }));
            }
        }
    }
}
