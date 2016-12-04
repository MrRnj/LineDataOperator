using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inter_face.BackUps;
using Inter_face.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Inter_face.ViewModel
{
   public class CalculeteDianFXViewModel : ViewModelBase
    {
        private string preValue;
        private OpenFileDialog ofd;

        private ObservableCollection<DianFXModel> dianfxColecotion;

        public ObservableCollection<DianFXModel> DianfxColection
        {
            get { return dianfxColecotion; }
            set { dianfxColecotion = value; }
        }

        private ObservableCollection<DianFXModel> dianfxColecotionS;

        public ObservableCollection<DianFXModel> DianfxColectionS
        {
            get { return dianfxColecotionS; }
            set { dianfxColecotionS = value; }
        }        

        /// <summary>
        /// The <see cref="DianfxIndex" /> property's name.
        /// </summary>
        public const string DianfxIndexPropertyName = "DianfxIndex";
        //0-下行 1-上行
        private int _dianfxIndex = 0;

        /// <summary>
        /// Sets and gets the DianfxIndex property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int DianfxIndex
        {
            get
            {
                return _dianfxIndex;
            }

            set
            {
                if (_dianfxIndex == value)
                {
                    return;
                }

                _dianfxIndex = value;
                RaisePropertyChanged(DianfxIndexPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="FrontDis" /> property's name.
        /// </summary>
        public const string FrontDisPropertyName = "FrontDis";

        private string _frontdis = "0";

        /// <summary>
        /// Sets and gets the FrontDis property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// 断标离中心距离
        /// </summary>
        public string FrontDis
        {
            get
            {
                return _frontdis;
            }

            set
            {
                if (_frontdis == value)
                {
                    return;
                }

                _frontdis = value;
                RaisePropertyChanged(FrontDisPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="BackDis" /> property's name.
        /// </summary>
        public const string BackDisPropertyName = "BackDis";

        private string _backDis = "0";

        /// <summary>
        /// Sets and gets the BackDis property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// 合标离中心距离
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

        private string _frontTime = "0";

        /// <summary>
        /// Sets and gets the FrontTime property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FrontTime
        {
            get
            {
                return _frontTime;
            }

            set
            {
                if (_frontTime == value)
                {
                    return;
                }

                _frontTime = value;
                RaisePropertyChanged(FrontTimePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="BackTime" /> property's name.
        /// </summary>
        public const string BackTimePropertyName = "BackTime";

        private string _backTime = "0";

        /// <summary>
        /// Sets and gets the BackTime property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string BackTime
        {
            get
            {
                return _backTime;
            }

            set
            {
                if (_backTime == value)
                {
                    return;
                }

                _backTime = value;
                RaisePropertyChanged(BackTimePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="FilePath" /> property's name.
        /// </summary>
        public const string FilePathPropertyName = "FilePath";

        private string _filepath = string.Empty;

        /// <summary>
        /// Sets and gets the FilePath property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FilePath
        {
            get
            {
                return _filepath;
            }

            set
            {
                if (_filepath == value)
                {
                    return;
                }

                _filepath = value;
                RaisePropertyChanged(FilePathPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ResistenceRange" /> property's name.
        /// </summary>
        public const string ResistenceRangePropertyName = "ResistenceRange";

        private string _resistenceRange = "1";

        /// <summary>
        /// Sets and gets the ResistenceRange property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ResistenceRange
        {
            get
            {
                return _resistenceRange;
            }

            set
            {
                if (_resistenceRange == value)
                {
                    return;
                }

                _resistenceRange = value;
                RaisePropertyChanged(ResistenceRangePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TrainName" /> property's name.
        /// </summary>
        public const string TrainNamePropertyName = "TrainName";

        private string _trainName = "无车辆数据";

        /// <summary>
        /// Sets and gets the TrainName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string TrainName
        {
            get
            {
                return _trainName;
            }

            set
            {
                if (_trainName == value)
                {
                    return;
                }

                _trainName = value;
                RaisePropertyChanged(TrainNamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CurrentTrainIndex" /> property's name.
        /// </summary>
        public const string CurrentTrainIndexPropertyName = "CurrentTrainIndex";

        private int _currentTrainindexProperty = 0;

        /// <summary>
        /// Sets and gets the CurrentTrainIndex property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int CurrentTrainIndex
        {
            get
            {
                return _currentTrainindexProperty;
            }

            set
            {
                if (_currentTrainindexProperty == value)
                {
                    return;
                }

                _currentTrainindexProperty = value;
                RaisePropertyChanged(CurrentTrainIndexPropertyName);
            }
        }

        public ObservableCollection<string> IndexList
        {
            get; set;
        }
        

        public CalculeteDianFXViewModel()
        {
            DianfxColection = new ObservableCollection<DianFXModel>();
            DianfxColectionS = new ObservableCollection<DianFXModel>();
            IndexList = new ObservableCollection<string>();

            MessengerInstance.Register<DianFXInfos>(this, "Ca_DianfxInfos", 
                (p) =>
                {
                    string[] parts;                   

                    if (p != null)
                    {
                        parts = p.OtherInfos.Split('|');
                        //trainame:filepath:resistenceRange:frontDis:backDis:frontTime:backTime
                        TrainName = parts[0];
                        FilePath = parts[1];
                        ResistenceRange = parts[2];
                        FrontDis = parts[3];
                        BackDis = parts[4];
                        FrontTime = parts[5];
                        BackTime = parts[6];
                        CurrentTrainIndex = int.Parse(parts[7]);
                        if (FilePath != string.Empty)
                        {
                            string[] infos = System.IO.File.ReadAllLines(FilePath);

                            foreach (string info in infos)
                            {
                                switch (info.Split(':')[0])
                                {                                    
                                    case "牵引特性曲线":
                                        formatIndexlist(info.Split(':')[1]);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }                        

                        foreach (DianFXModel item in p.DfxX)
                        {
                            DianfxColection.Add(item);
                        }

                        foreach (DianFXModel item in p.DfxS)
                        {
                            DianfxColectionS.Add(item);
                        }
                    }
                    else
                    {
                        TrainName = string.Empty;
                        FilePath = string.Empty;
                        ResistenceRange = "1";
                        FrontDis = "0";
                        BackDis = "0";
                        FrontTime = "0";
                        BackTime = "0";
                        CurrentTrainIndex = 0;
                    }
                });
        }

        private void loadTrainInfo()
        {            
            string[] infos;

            ofd = new OpenFileDialog();
            ofd.Filter = "车辆文件|*.tr|全部文件|*.*";
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FilePath = ofd.FileName;
                
                infos = System.IO.File.ReadAllLines(FilePath);

                foreach (string info in infos)
                {
                    switch (info.Split(':')[0])
                    {
                        case "车辆名称":
                            TrainName = info.Split(':')[1];
                            break;
                        case "牵引特性曲线":
                            formatIndexlist(info.Split(':')[1]);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void formatIndexlist(string rawline)
        {
            string[] parts = rawline.Split('|');
            string[] powers = parts[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            IndexList.Clear();
            foreach (string power in powers)
            {
                IndexList.Add(
                    power.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries)[0]);
            }
            
        }

        private void Calculete()
        {
            DianFXInfos dfi = new DianFXInfos();
            dfi.OtherInfos = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}",
               TrainName,
               FilePath,
               ResistenceRange,
               FrontDis,
               BackDis,
               FrontTime,
               BackTime,
               CurrentTrainIndex.ToString());

            foreach (DianFXModel items in DianfxColectionS)
            {
                items.BackPosition = (float.Parse(items.MidPosition) - float.Parse(items.BackDis)).ToString("F3");
                items.FrontPosition= (float.Parse(items.MidPosition) + float.Parse(items.FrontDis)).ToString("F3");

                dfi.DfxS.Add(items);
            }

            foreach (DianFXModel itemx in DianfxColection)
            {
                itemx.BackPosition = (float.Parse(itemx.MidPosition) + float.Parse(itemx.BackDis)).ToString("F3");
                itemx.FrontPosition = (float.Parse(itemx.MidPosition) - float.Parse(itemx.FrontDis)).ToString("F3");

                dfi.DfxX.Add(itemx);
            }
            
            MessengerInstance.Send(dfi, "CaculeteDianfx");
        }

        private void setDis()
        {
            List<DianFXModel> currentfxs;

            if (_dianfxIndex == 0)
            {
                currentfxs = dianfxColecotionS.ToList();
                dianfxColecotionS.Clear();
                //上行 从右至左
                foreach (DianFXModel item in currentfxs)
                {                   
                    //合
                    item.BackPosition = (float.Parse(item.MidPosition) - float.Parse(BackDis)).ToString();
                    //断
                    item.FrontPosition= (float.Parse(item.MidPosition) + float.Parse(FrontDis)).ToString();

                    item.BackDis = BackDis;
                    item.FrontDis = FrontDis;

                    dianfxColecotionS.Add(item);
                    //分相长度：frt-back
                }
            }
            else
            {
                currentfxs = dianfxColecotion.ToList();
                dianfxColecotion.Clear();
                //下行 从左至右
                foreach (DianFXModel item in currentfxs)
                {                   
                    //合
                    item.BackPosition = (float.Parse(item.MidPosition) + float.Parse(BackDis)).ToString();
                    //断
                    item.FrontPosition = (float.Parse(item.MidPosition) - float.Parse(FrontDis)).ToString();

                    item.BackDis = BackDis;
                    item.FrontDis = FrontDis;

                    dianfxColecotion.Add(item);
                    //分相长度：back-frt
                }
            }
        }

        private void setTime()
        {
            List<DianFXModel> currentfxs;

            if (_dianfxIndex == 0)
            {
                currentfxs = dianfxColecotionS.ToList();
                dianfxColecotionS.Clear();
                //上行 从右至左
                foreach (DianFXModel item in currentfxs)
                {
                    //合
                    item.BackTime = BackTime;
                    //断
                    item.FrontTime = FrontTime;                    
                    
                    dianfxColecotionS.Add(item);
                    //分相长度：frt-back
                }
            }
            else
            {
                currentfxs = dianfxColecotion.ToList();
                dianfxColecotion.Clear();
                //下行 从左至右
                foreach (DianFXModel item in currentfxs)
                {
                    //合
                    item.BackTime = BackTime;
                    //断
                    item.FrontTime = FrontTime;
                    
                    dianfxColecotion.Add(item);
                    //分相长度：back-frt
                }
            }
        }

        private void dataGrid_BeginningEdit(DataGridBeginningEditEventArgs e)
        {
            preValue = (e.Column.GetCellContent(e.Row) as TextBlock).Text;
        }

        private void dataGrid_CellEditEnding(DataGridCellEditEndingEventArgs e)
        {
            string newValue = (e.EditingElement as System.Windows.Controls.TextBox).Text;

            if (newValue != preValue)
            {
                
            }
        }

        #region Commands

        private RelayCommand _loadtrainCommand;

        /// <summary>
        /// Gets the LoadTrainCommand.
        /// </summary>
        public RelayCommand LoadTrainCommand
        {
            get
            {
                return _loadtrainCommand
                    ?? (_loadtrainCommand = new RelayCommand(
                    () =>
                    {
                        loadTrainInfo();
                    }));
            }
        }

        private RelayCommand _calculateCommand;

        /// <summary>
        /// Gets the CalculateCommand.
        /// </summary>
        public RelayCommand CalculateCommand
        {
            get
            {
                return _calculateCommand
                    ?? (_calculateCommand = new RelayCommand(
                    () =>
                    {
                        Calculete();
                    }));
            }
        }

        private RelayCommand _setTimeCommand;

        /// <summary>
        /// Gets the SetTimeCommand.
        /// </summary>
        public RelayCommand SetTimeCommand
        {
            get
            {
                return _setTimeCommand
                    ?? (_setTimeCommand = new RelayCommand(
                    () =>
                    {
                        setTime();
                    }));
            }
        }

        private RelayCommand _setDisCommand;

        /// <summary>
        /// Gets the SetDisCommand.
        /// </summary>
        public RelayCommand SetDisCommand
        {
            get
            {
                return _setDisCommand
                    ?? (_setDisCommand = new RelayCommand(
                    () =>
                    {
                        setDis();
                    }));
            }
        }
        #endregion
    }
}
