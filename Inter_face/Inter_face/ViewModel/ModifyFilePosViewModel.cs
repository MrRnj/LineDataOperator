using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Inter_face.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ModifyFilePosViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the ModifyFilePosViewModel class.
        /// </summary>
        public ModifyFilePosViewModel()
        {
            fileNames = new ObservableCollection<string>();
            MessengerInstance.Register<string[]>(this, "ModifyFilePos",
                (p) =>
                {
                    foreach (string filename in p)
                    {
                        FileNamesProperty.Add(filename);
                    }
                });
        }

        private ObservableCollection<string> fileNames;

        public ObservableCollection<string> FileNamesProperty
        {
            get { return fileNames; }
            set { fileNames = value; }
        }

        /// <summary>
        /// The <see cref="selectedIndex" /> property's name.
        /// </summary>
        public const string selectedIndexPropertyName = "selectedIndex";

        private int _selectedindexProperty = 0;

        /// <summary>
        /// Sets and gets the selectedIndex property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int selectedIndex
        {
            get
            {
                return _selectedindexProperty;
            }

            set
            {
                if (_selectedindexProperty == value)
                {
                    return;
                }

                _selectedindexProperty = value;
                RaisePropertyChanged(selectedIndexPropertyName);
            }
        }

        private void Up()
        {
            string temp = FileNamesProperty[selectedIndex];
            int index = selectedIndex;
            FileNamesProperty.RemoveAt(selectedIndex);
            FileNamesProperty.Insert(index - 1, temp);
            selectedIndex = index - 1;
        }

        private void Down()
        {
            string temp = FileNamesProperty[selectedIndex];
            int index = selectedIndex;
            FileNamesProperty.RemoveAt(selectedIndex);
            FileNamesProperty.Insert(index + 1, temp);
            selectedIndex = index + 1;
        }

        private void Delete()
        {
            if (FileNamesProperty.Count != 0)
            {
                FileNamesProperty.RemoveAt(selectedIndex);
            }
        }

        private void commit()
        {
            List<string> result = new List<string>();
            foreach (string filename in FileNamesProperty)
            {
                result.Add(filename);
            }

            MessengerInstance.Send<string[]>(result.ToArray(), "sendFilenames");
        }

        private void NoChanged()
        {
            MessengerInstance.Send<string[]>(new string[] { }, "sendFilenames");
        }

        private RelayCommand _upCommand;

        /// <summary>
        /// Gets the UpCommand.
        /// </summary>
        public RelayCommand UpCommand
        {
            get
            {
                return _upCommand
                    ?? (_upCommand = new RelayCommand(
                    () =>
                    {
                        if (!UpCommand.CanExecute(null))
                        {
                            return;
                        }

                        Up();
                    },
                    () => 
                    {
                       return selectedIndex > 0;
                    }));
            }
        }

        private RelayCommand _downCommand;

        /// <summary>
        /// Gets the DownCommand.
        /// </summary>
        public RelayCommand DownCommand
        {
            get
            {
                return _downCommand
                    ?? (_downCommand = new RelayCommand(
                    () =>
                    {
                        if (!DownCommand.CanExecute(null))
                        {
                            return;
                        }

                        Down();
                    },
                    () => { return selectedIndex < fileNames.Count - 1; }));
            }
        }

        private RelayCommand _commitCommand;

        /// <summary>
        /// Gets the CommitCommand.
        /// </summary>
        public RelayCommand CommitCommand
        {
            get
            {
                return _commitCommand
                    ?? (_commitCommand = new RelayCommand(
                    () =>
                    {
                        commit();
                    }));
            }
        }

        private RelayCommand _cancelCommand;

        /// <summary>
        /// Gets the CancelCommand.
        /// </summary>
        public RelayCommand CancelCommand
        {
            get
            {
                return _cancelCommand
                    ?? (_cancelCommand = new RelayCommand(
                    () =>
                    {
                        NoChanged();
                    }));
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
                        if (!DeleteCommand.CanExecute(null))
                        {
                            return;
                        }

                        Delete();
                    },
                    () => { return FileNamesProperty.Count != 0; }));
            }
        }
    }
}