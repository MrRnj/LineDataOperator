using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ExtractData;
using System.Linq;
using System.IO;
using System.Windows;
using System.Windows.Forms;

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
        FulfillSource ffs;
        OpenFileDialog openacessfile;
        private List<string> deletedSheetsname;

        /// <summary>
        /// Initializes a new instance of the ModifyFilePosViewModel class.
        /// </summary>
        public ModifyFilePosViewModel()
        {
            fileNames = new ObservableCollection<string>();
            MessengerInstance.Register<FulfillSource>(this, "ModifyFilePos",
                (p) =>
                {
                    ffs = p;

                    foreach (string filename in p.GetExistSheets())
                    {
                        FileNamesProperty.Add(filename);
                    }
                });
            deletedSheetsname = new List<string>();
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
                if (!deletedSheetsname.Contains(FileNamesProperty[selectedIndex]))
                    deletedSheetsname.Add(FileNamesProperty[selectedIndex]);
                FileNamesProperty.RemoveAt(selectedIndex);
            }
            selectedIndex = 0;
        }

        private void Add(string[] files)
        {
            string existnames = string.Empty;
            List<string> existFiles = ffs.HasExistsheet(files.Select(p => Path.GetFileNameWithoutExtension(p)).ToArray());

            if (existFiles != null && existFiles.Count != 0)
            {
                foreach (string item in existFiles)
                {
                    existnames += (item + @"\r\n");
                }

                if (System.Windows.MessageBox.Show(string.Format(@"是否覆盖下列数据：\r\n{0}", existnames),
                    "提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {
                    foreach (string item in existFiles)
                    {
                        if (!deletedSheetsname.Contains(item))
                            deletedSheetsname.Add(item);
                    }
                }                
            }

            foreach (string file in files)
            {
                if (!existFiles.Contains(Path.GetFileNameWithoutExtension(file)))
                    FileNamesProperty.Add(file);
            }
        }

        private void loadformDialog()
        {
            openacessfile = new OpenFileDialog();

            openacessfile.Filter = "access files (*.mdb)|*.mdb|All files (*.*)|*.*";
            openacessfile.FilterIndex = 1;
            openacessfile.Multiselect = true;

            if (openacessfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] datapathes = openacessfile.FileNames;
                Add(datapathes);
            }
        }

        private void dropEnter(System.Windows.DragEventArgs e)
        {
            if (e.KeyStates == DragDropKeyStates.LeftMouseButton && e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] filenames = ((System.Array)(e.Data.GetData(System.Windows.DataFormats.FileDrop))) as string[];
                if (filenames != null && filenames.All(p => System.IO.Path.GetExtension(p).Equals(".mdb")))
                    e.Effects = System.Windows.DragDropEffects.Link;
                else
                    e.Effects = System.Windows.DragDropEffects.None;

            }
            else
                e.Effects = System.Windows.DragDropEffects.None;
        }

        private void loadformDrop(System.Windows.DragEventArgs e)
        {
            string[] filepathes = null;

            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                filepathes = (((System.Array)(e.Data.GetData(System.Windows.DataFormats.FileDrop))) as string[]);
                if (filepathes != null)
                {
                    Add(filepathes.Where(p => Path.GetExtension(p).ToLower().Equals(".mdb")).ToArray());
                }
            }
        }

        private void commit()
        {
            List<string> result = new List<string>();

            ffs.DeleteSheet(deletedSheetsname.ToArray());
            
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

        private void RemoveAll()
        {
            foreach (string filename in FileNamesProperty)
            {
                if (!deletedSheetsname.Contains(filename))
                    deletedSheetsname.Add(filename);                
            }

            FileNamesProperty.Clear();
            selectedIndex = -1;          
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
                    () => { return FileNamesProperty.Count != 0 && selectedIndex != -1; }));
            }
        }

        private RelayCommand _removeallCommand;

        /// <summary>
        /// Gets the RemoveAllCommand.
        /// </summary>
        public RelayCommand RemoveAllCommand
        {
            get
            {
                return _removeallCommand
                    ?? (_removeallCommand = new RelayCommand(
                    () =>
                    {
                        if (!RemoveAllCommand.CanExecute(null))
                        {
                            return;
                        }

                        RemoveAll();
                    },
                    () => { return FileNamesProperty.Count != 0; }));
            }
        }

        private RelayCommand _loadCommand;

        /// <summary>
        /// Gets the CancelCommand.
        /// </summary>
        public RelayCommand LoadCommand
        {
            get
            {
                return _loadCommand
                    ?? (_loadCommand = new RelayCommand(
                    () =>
                    {
                        loadformDialog();
                    }));
            }
        }

        private RelayCommand<System.Windows.DragEventArgs> _dragenterCommand;

        /// <summary>
        /// Gets the CancelCommand.
        /// </summary>
        public RelayCommand<System.Windows.DragEventArgs> DragEnterCommand
        {
            get
            {
                return _dragenterCommand
                    ?? (_dragenterCommand = new RelayCommand<System.Windows.DragEventArgs>(
                    (e) =>
                    {
                        dropEnter(e);
                    }));
            }
        }

        private RelayCommand<System.Windows.DragEventArgs> _loadformDropCommand;

        /// <summary>
        /// Gets the CancelCommand.
        /// </summary>
        public RelayCommand<System.Windows.DragEventArgs> LoadFormDropCommand
        {
            get
            {
                return _loadformDropCommand
                    ?? (_loadformDropCommand = new RelayCommand<System.Windows.DragEventArgs>(
                    (e) =>
                    {
                        loadformDrop(e);
                    }));
            }
        }
    }
}