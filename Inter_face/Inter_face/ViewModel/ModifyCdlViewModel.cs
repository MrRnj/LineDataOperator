﻿using ExtractData;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inter_face.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using Microsoft.Win32;

namespace Inter_face.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ModifyCdlViewModel : ViewModelBase
    {
        private bool _isModify ;
        ACdlWindow acwindow;
        
        /// <summary>
        /// Initializes a new instance of the ModifyCdlViewModel class.
        /// </summary>
        private ObservableCollection<string> CdlCollection;
        public ObservableCollection<string> CdlCollectionProperty
        {
            get { return CdlCollection; }
            set { CdlCollection = value; }
        }

        /// <summary>
        /// The <see cref="SeletedItem" /> property's name.
        /// </summary>
        public const string SeletedItemPropertyName = "SeletedItem";

        private int _seleteditemProperty = -1;

        /// <summary>
        /// Sets and gets the SeletedItem property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int SeletedItem
        {
            get
            {
                return _seleteditemProperty;
            }

            set
            {
                if (_seleteditemProperty == value)
                {
                    return;
                }

                RaisePropertyChanging(SeletedItemPropertyName);
                _seleteditemProperty = value;
                RaisePropertyChanged(SeletedItemPropertyName);
            }
        }

        ExtractData.GraphyDataOper gdo;

        public ModifyCdlViewModel()
        {
            _isModify = false;
            CdlCollectionProperty = new ObservableCollection<string>();
           /* string _currentdir = Environment.CurrentDirectory;
            gdo = GraphyDataOper.CreatOper(Path.Combine(_currentdir, @"excelmodels\接坡面数据_Single.xlsx"),
                Path.Combine(_currentdir, @"excelmodels\接标记数据_single.xlsx"),
                Path.Combine(_currentdir, @"excelmodels\接曲线数据_single.xlsx"),
                Path.Combine(_currentdir, @"excelmodels\信号机数据_single.xlsx"));*/

            MessengerInstance.Register<string>(this, "cdl", P => { Insertcdldata(P); });
            MessengerInstance.Register<string>(this, "Changecdl", P => { Changecdldata(P); });
            MessengerInstance.Register<ExtractData.GraphyDataOper>(this, "gdoInWindow", p => { gdo = p; });
        }

        public void Loadcdldata()
        {
            string[] cdls = gdo.GetCdlData(Path.Combine(Environment.CurrentDirectory, @"excelmodels\接坡面数据.xlsx"));

            if (cdls != null)
            {
                foreach (string item in cdls)
                {
                    CdlCollectionProperty.Add(item);
                }
            }
        }

        public void Savecdldata()
        {
            try
            {
                gdo.SaveCdlData(CdlCollectionProperty.ToArray(),
                    Path.Combine(Environment.CurrentDirectory, @"excelmodels\接坡面数据.xlsx"));
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "出错", System.Windows.MessageBoxButton.OK);
            }
        }

        public void Deletecdldata()
        {
            int index = SeletedItem;
            CdlCollectionProperty.RemoveAt(SeletedItem);
            if (CdlCollectionProperty.Count == 0)
                index = -1;
            SeletedItem = index;
        }

        public void Upcdldata()
        {
            string temp = CdlCollectionProperty[SeletedItem];
            int index = SeletedItem;
            CdlCollectionProperty.RemoveAt(SeletedItem);
            CdlCollectionProperty.Insert(index - 1, temp);
            SeletedItem = index - 1;
        }

        public void Downcdldata()
        {
            string temp = CdlCollectionProperty[SeletedItem];
            int index = SeletedItem;
            CdlCollectionProperty.RemoveAt(SeletedItem);
            CdlCollectionProperty.Insert(index + 1, temp);
            SeletedItem = index + 1;
        }

        public void Insertcdldata(string content)
        {
            if (!CdlCollection.Contains(content))
            {
                int index = SeletedItem == -1 ? 0 : SeletedItem;
                CdlCollectionProperty.Insert(index, content);
                SeletedItem = index;
            }
            acwindow.Close();               
        }

        public void Changecdldata(string content)
        {
            if (SeletedItem != -1)
            {
                int index = SeletedItem;
                CdlCollectionProperty.RemoveAt(index);
                CdlCollectionProperty.Insert(index, content);
                SeletedItem = index;
            }
            acwindow.Close();
        }

        public void ModifyCdlData()
        {
            if (SeletedItem != -1)
            {
                acwindow = new ACdlWindow();
                MessengerInstance.Send<string>(CdlCollectionProperty[SeletedItem], "Mcdl");                
                acwindow.ShowDialog();
            }
        }

        private RelayCommand _upcdldataCommand;

        /// <summary>
        /// Gets the UpcdlDataCommand.
        /// </summary>
        public RelayCommand UpcdlDataCommand
        {
            get
            {
                return _upcdldataCommand
                    ?? (_upcdldataCommand = new RelayCommand(
                                          () =>
                                          {
                                              Upcdldata();
                                          },
                                          () => SeletedItem == 0 || SeletedItem == -1 ? false : true));
            }
        }

        private RelayCommand _downcdldataCommand;

        /// <summary>
        /// Gets the DowncdlDataCommand.
        /// </summary>
        public RelayCommand DowncdlDataCommand
        {
            get
            {
                return _downcdldataCommand
                    ?? (_downcdldataCommand = new RelayCommand(
                                          () =>
                                          {

                                              Downcdldata();
                                          },
                                          () => SeletedItem == CdlCollectionProperty.Count - 1 ? false : true));
            }
        }

        private RelayCommand _deletedataCommand;

        /// <summary>
        /// Gets the DeletedataCommand.
        /// </summary>
        public RelayCommand DeletedataCommand
        {
            get
            {
                return _deletedataCommand
                    ?? (_deletedataCommand = new RelayCommand(
                                          () =>
                                          {
                                              Deletecdldata();
                                          },
                                          () => SeletedItem == -1 ? false : true));
            }
        }

        private RelayCommand _savedataCommand;

        /// <summary>
        /// Gets the SaveDataCommand.
        /// </summary>
        public RelayCommand SaveDataCommand
        {
            get
            {
                return _savedataCommand
                    ?? (_savedataCommand = new RelayCommand(
                                          () =>
                                          {
                                              Savecdldata();
                                          }
                                          ));
            }
        }

        private RelayCommand _loaddataCommand;

        /// <summary>
        /// Gets the LoadDataCommand.
        /// </summary>
        public RelayCommand LoadDataCommand
        {
            get
            {
                return _loaddataCommand
                    ?? (_loaddataCommand = new RelayCommand(
                                          () =>
                                          {
                                              Loadcdldata();
                                          }));
            }
        }

        private RelayCommand _insertcdldataCommand;

        /// <summary>
        /// Gets the InsertCdlDataCommand.
        /// </summary>
        public RelayCommand InsertCdlDataCommand
        {
            get
            {
                return _insertcdldataCommand
                    ?? (_insertcdldataCommand = new RelayCommand(
                                          () =>
                                          {
                                              acwindow = new ACdlWindow();
                                              acwindow.ShowDialog();
                                          }));
            }
        }

        private RelayCommand _changeCdlCommand;

        /// <summary>
        /// Gets the ChangeCdlCommand.
        /// </summary>
        public RelayCommand ChangeCdlCommand
        {
            get
            {
                return _changeCdlCommand
                    ?? (_changeCdlCommand = new RelayCommand(
                    () =>
                    {
                        ModifyCdlData();
                    },
                                          () => SeletedItem == -1 ? false : true));
            }
        }
    }
}