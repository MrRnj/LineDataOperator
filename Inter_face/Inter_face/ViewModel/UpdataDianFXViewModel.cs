using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inter_face.Models;
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
    public class UpdataDianFXViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the UpdataDianFXViewModel class.
        /// </summary>
        private decimal startpos;
        private int startSecnum;
        private decimal endpos;
        private int endSecnum;
        private decimal leftedgepos;
        private int leftedgeSecnum;
        private decimal rightedgepos;
        private int rightedgeSecnum;
        private decimal middlepos;        
        private List<string> cdl;
        private bool isrefreshData = true;
        private bool isupdata;
        private bool ischeckSecnum = true;

        /// <summary>
        /// The <see cref="Name" /> property's name.
        /// </summary>
        public const string NamePropertyName = "Name";

        private string _nameProperty = string.Empty;

        /// <summary>
        /// Sets and gets the Name property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Name
        {
            get
            {
                return _nameProperty;
            }

            set
            {
                if (_nameProperty == value)
                {
                    return;
                }

                _nameProperty = value;
                RaisePropertyChanged(NamePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="StartShowPos" /> property's name.
        /// </summary>
        public const string StartShowPosPropertyName = "StartShowPos";

        private string _startshowposProperty = string.Empty;

        /// <summary>
        /// Sets and gets the StartShowPos property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string StartShowPos
        {
            get
            {
                return _startshowposProperty;
            }

            set
            {
                if (_startshowposProperty == value)
                {
                    return;
                }

                _startshowposProperty = value;
                RaisePropertyChanged(StartShowPosPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="MidSecnum" /> property's name.
        /// </summary>
        public const string MidSecnumPropertyName = "MidSecnum";

        private int middleSecnum = 0;

        /// <summary>
        /// Sets and gets the MidSecnum property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int MidSecnum
        {
            get
            {
                return middleSecnum;
            }

            set
            {
                if (middleSecnum == value)
                {
                    return;
                }

                middleSecnum = value;
                RaisePropertyChanged(MidSecnumPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="EndShowPos" /> property's name.
        /// </summary>
        public const string EndShowPosPropertyName = "EndShowPos";

        private string _endshowposProperty = string.Empty;

        /// <summary>
        /// Sets and gets the EndShowPos property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string EndShowPos
        {
            get
            {
                return _endshowposProperty;
            }

            set
            {
                if (_endshowposProperty == value)
                {
                    return;
                }

                _endshowposProperty = value;
                RaisePropertyChanged(EndShowPosPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="StartDis" /> property's name.
        /// </summary>
        public const string StartDisPropertyName = "StartDis";

        private string _startdisProperty = string.Empty;

        /// <summary>
        /// Sets and gets the StartDis property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string StartDis
        {
            get
            {
                return _startdisProperty;
            }

            set
            {
                if (_startdisProperty == value)
                {
                    return;
                }

                _startdisProperty = value;
                RaisePropertyChanged(StartDisPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Enddis" /> property's name.
        /// </summary>
        public const string EnddisPropertyName = "Enddis";

        private string _enddisProperty = string.Empty;

        /// <summary>
        /// Sets and gets the Enddis property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Enddis
        {
            get
            {
                return _enddisProperty;
            }

            set
            {
                if (_enddisProperty == value)
                {
                    return;
                }

                _enddisProperty = value;
                RaisePropertyChanged(EnddisPropertyName);
            }
        }
      
        /// <summary>
        /// The <see cref="LeftDis" /> property's name.
        /// </summary>
        public const string LeftDisPropertyName = "LeftDis";

        private decimal _leftdisProperty = 0;

        /// <summary>
        /// Sets and gets the LeftDis property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public decimal LeftDis
        {
            get
            {
                return _leftdisProperty;
            }

            set
            {
                if (_leftdisProperty == value)
                {
                    return;
                }

                _leftdisProperty = value;
                if (isrefreshData)
                    refreshData();
                RaisePropertyChanged(LeftDisPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="RightDis" /> property's name.
        /// </summary>
        public const string RightDisPropertyName = "RightDis";

        private decimal _rightdisProperty = 0;

        /// <summary>
        /// Sets and gets the RightDis property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public decimal RightDis
        {
            get
            {
                return _rightdisProperty;
            }

            set
            {
                if (_rightdisProperty == value)
                {
                    return;
                }

                _rightdisProperty = value;
                if (isrefreshData)
                    refreshData();
                RaisePropertyChanged(RightDisPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LeftEdgePos" /> property's name.
        /// </summary>
        public const string LeftEdgePosPropertyName = "LeftEdgePos";

        private string _leftedgeposProperty = string.Empty;

        /// <summary>
        /// Sets and gets the LeftEdgePos property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LeftEdgePos
        {
            get
            {
                return _leftedgeposProperty;
            }

            set
            {
                if (_leftedgeposProperty == value)
                {
                    return;
                }

                _leftedgeposProperty = value;
                RaisePropertyChanged(LeftEdgePosPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="RightEdgePos" /> property's name.
        /// </summary>
        public const string RightEdgePosPropertyName = "RightEdgePos";

        private string _rightedgeposProperty = string.Empty;

        /// <summary>
        /// Sets and gets the RightEdgePos property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string RightEdgePos
        {
            get
            {
                return _rightedgeposProperty;
            }

            set
            {
                if (_rightedgeposProperty == value)
                {
                    return;
                }

                _rightedgeposProperty = value;
                RaisePropertyChanged(RightEdgePosPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="PartI" /> property's name.
        /// </summary>
        public const string PartIPropertyName = "PartI";

        private int _partiProperty = 0;

        /// <summary>
        /// Sets and gets the PartI property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int PartI
        {
            get
            {
                return _partiProperty;
            }

            set
            {
                if (_partiProperty == value)
                {
                    return;
                }
                if (value < 0)
                {
                    value = 0;
                }
                _partiProperty = value;
                if (isrefreshData)
                    refreshData();
                RaisePropertyChanged(PartIPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="PartII" /> property's name.
        /// </summary>
        public const string PartIIPropertyName = "PartII";

        private float _partiiProperty = 0;

        /// <summary>
        /// Sets and gets the PartII property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public float PartII
        {
            get
            {
                return _partiiProperty;
            }

            set
            {
                if (_partiiProperty == value)
                {
                    return;
                }

                if (value < 0 || value > 999)
                {
                    value = 0;
                }
                _partiiProperty = value;
                if (isrefreshData)
                    refreshData();
                RaisePropertyChanged(PartIIPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Hat" /> property's name.
        /// </summary>
        public const string HatPropertyName = "Hat";

        private string _hatProperty = string.Empty;

        /// <summary>
        /// Sets and gets the Hat property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Hat
        {
            get
            {
                return _hatProperty;
            }

            set
            {
                if (_hatProperty == value)
                {
                    return;
                }

                _hatProperty = value;
                RaisePropertyChanged(HatPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LeftSign" /> property's name.
        /// </summary>
        public const string LeftSignPropertyName = "LeftSign";

        private string _leftsignProperty = string.Empty;

        /// <summary>
        /// Sets and gets the LeftSign property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LeftSign
        {
            get
            {
                return _leftsignProperty;
            }

            set
            {
                if (_leftsignProperty == value)
                {
                    return;
                }

                _leftsignProperty = value;
                RaisePropertyChanged(LeftSignPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="RightSign" /> property's name.
        /// </summary>
        public const string RightSignPropertyName = "RightSign";

        private string _rightsignProperty = string.Empty;

        /// <summary>
        /// Sets and gets the LeftSign property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string RightSign
        {
            get
            {
                return _rightsignProperty;
            }

            set
            {
                if (_rightsignProperty == value)
                {
                    return;
                }

                _rightsignProperty = value;
                RaisePropertyChanged(RightSignPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CurrentMidSecNum" /> property's name.
        /// </summary>
        public const string CurrentMidSecNumPropertyName = "CurrentMidSecNum";

        private int _currentMidsecnum = 0;

        /// <summary>
        /// Sets and gets the CurrentMidSecNum property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int CurrentMidSecNum
        {
            get
            {
                return _currentMidsecnum;
            }

            set
            {
                if (_currentMidsecnum == value)
                {
                    return;
                }

                _currentMidsecnum = value;
                RaisePropertyChanged(CurrentMidSecNumPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CurentNumIndex" /> property's name.
        /// </summary>
        public const string CurentNumIndexPropertyName = "CurentNumIndex";

        private int _currentNumindex = 0;

        /// <summary>
        /// Sets and gets the CurentNumIndex property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int CurentNumIndex
        {
            get
            {
                return _currentNumindex;
            }

            set
            {
                if (_currentNumindex == value)
                {
                    return;
                }

                _currentNumindex = value;
                RaisePropertyChanged(CurentNumIndexPropertyName);
            }
        }

        private ObservableCollection<string> secArray;

        public ObservableCollection<string> SecArray
        {
            get { return secArray; }
            set { secArray = value; }
        }


        public UpdataDianFXViewModel()
        {
            secArray = new ObservableCollection<string>();
            MessengerInstance.Register<DianFXneededInfosMode>(this, "DfxInputInfos",
                p =>
                {
                    isrefreshData = false;

                    string[] infos = p.DfxInfosProperty.Split(':');
                    cdl = p.CdlListProperty;

                    startpos = decimal.Parse(p.LeftPosProperty.Split('+')[1]);
                    startSecnum = int.Parse(p.LeftPosProperty.Split('+')[0]);

                    endpos = decimal.Parse(p.RightPosProperty.Split('+')[1]);
                    endSecnum = int.Parse(p.RightPosProperty.Split('+')[0]);

                    middlepos = decimal.Parse(infos[3].Split('+')[0]);
                    middleSecnum = int.Parse(infos[3].Split('+')[1]);

                    leftedgepos = decimal.Parse(infos[2].Split('+')[0]);
                    leftedgeSecnum = int.Parse(infos[2].Split('+')[1]);
                    LeftEdgePos = formatShowpos(leftedgepos, leftedgeSecnum);

                    rightedgepos = decimal.Parse(infos[4].Split('+')[0]);
                    rightedgeSecnum = int.Parse(infos[4].Split('+')[1]);
                    RightEdgePos = formatShowpos(rightedgepos, rightedgeSecnum);

                    StartShowPos = formatShowpos(startpos, startSecnum);
                    EndShowPos = formatShowpos(endpos, endSecnum);

                    StartDis = getDis(startpos, startSecnum, leftedgepos, leftedgeSecnum).ToString("F3");
                    Enddis = getDis(rightedgepos, rightedgeSecnum, endpos, endSecnum).ToString("F3");

                    LeftDis = getDis(leftedgepos, leftedgeSecnum, middlepos, middleSecnum);
                    RightDis = getDis(middlepos, middleSecnum, rightedgepos, rightedgeSecnum);

                    Hat = getHat(middleSecnum);
                    PartI = int.Parse(middlepos.ToString("F3").Split('.')[0]);
                    PartII = int.Parse(middlepos.ToString("F3").Split('.')[1]);

                    Name = infos[1];

                    isrefreshData = true;
                    isupdata = p.IsUpdataProperty;

                    if (p.DerictionProperty == 1)
                    {
                        LeftSign = "断";
                        RightSign = "合";
                    }
                    else
                    {
                        LeftSign = "合";
                        RightSign = "断";
                    }

                    for (int i = startSecnum; i <= endSecnum; i++)
                    {
                        secArray.Add(i.ToString());                        
                    }

                    CurentNumIndex = secArray.IndexOf(middleSecnum.ToString());                    
                });
        }

        private decimal getDis(decimal fstPos, int fstSecnum, decimal secPos, int secSecnum)
        {
            float offset = 0;
            string[] parts = { };

            if (fstSecnum != secSecnum)
            {
                for (int i = fstSecnum; i < secSecnum; i++)
                {
                    parts = cdl[i - 1].Split(':');
                    offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                }
            }

            return (secPos - fstPos) * 1000 - (decimal)offset;
        }

        private void refreshData(bool seachMidsec = true)
        {
            middlepos = (decimal)(PartI * 1000 + PartII) / 1000;
            if (seachMidsec)
            {
                middleSecnum = getSecnum(middlepos);
                ischeckSecnum = false;
                CurentNumIndex = secArray.IndexOf(middleSecnum.ToString());
                ischeckSecnum = true;
            }           

            if (middleSecnum != 0)
            {
                Hat = getHat(middleSecnum);

                leftedgepos = offsetPosBackword(middlepos, LeftDis, ref leftedgeSecnum);
                rightedgepos = offsetPosForword(middlepos, RightDis, ref rightedgeSecnum);
                LeftEdgePos = formatShowpos(leftedgepos, leftedgeSecnum);
                RightEdgePos = formatShowpos(rightedgepos, rightedgeSecnum);

                StartDis = getDis(startpos, startSecnum, leftedgepos, leftedgeSecnum).ToString("F0");
                Enddis = getDis(rightedgepos, rightedgeSecnum, endpos, endSecnum).ToString("F0");
            }

        }
        private string formatShowpos(decimal pos, int Secnum)
        {
            string posInstring = pos.ToString("F3");
            string part1 = posInstring.Split('.')[0];
            string part2 = posInstring.Split('.')[1];
            string hat = string.Empty;

            if (Secnum == cdl.Count + 1)
            {
                hat = cdl[Secnum - 2].Split(':')[1].Split('+')[0];
            }
            else
            {
                hat = cdl[Secnum - 1].Split(':')[0].Split('+')[0];
            }

            return string.Format("{0} {1}+{2}", hat, part1, part2);
        }

        private string getHat(int secNum)
        {
            if (secNum == cdl.Count + 1)
            {
                return cdl[secNum - 2].Split(':')[1].Split('+')[0];
            }
            else
            {
                return cdl[secNum - 1].Split(':')[0].Split('+')[0];
            }
        }

        private int getSecnum(decimal pos)
        {
            decimal usedPos = pos * 1000;
            decimal start = startpos * 1000;
            decimal end = 0;
            int sec = 0;
            //int matchedtimes = 0;
            string[] parts = { };
            decimal offset = 0;

            for (int i = startSecnum; i <= endSecnum; i++)
            {
                if (i == cdl.Count + 1)
                {
                    parts = cdl[i - 2].Split(':');
                    if(usedPos>= decimal.Parse(parts[0].Split('+')[1]) && usedPos <= end)
                    {
                        sec = i;
                        break;
                    }                    
                    else if (decimal.Parse(parts[0].Split('+')[1]) - usedPos - offset >= 0.0009m)
                    {
                        sec = i - 1;
                        break;
                    }
                }
                else
                {                    
                    parts = cdl[i - 1].Split(':');
                    end = decimal.Parse(parts[0].Split('+')[1]);
                    if (usedPos >= start && usedPos <= end)
                    {
                        sec = i;
                        break;
                    }
                    else
                    {
                        start = decimal.Parse(parts[1].Split('+')[1]);
                        if (i == endSecnum)
                        {
                            sec = endSecnum;
                            break;
                        }
                    }
                   /* if (usedPos + offset - decimal.Parse(parts[0].Split('+')[1]) > 0.0009m)
                    {
                        sec = i + 1;
                        offset += (decimal.Parse(parts[1].Split('+')[1]) - decimal.Parse(parts[0].Split('+')[1]));
                        continue;
                    }
                    else if (decimal.Parse(parts[0].Split('+')[1]) - usedPos - offset >= 0.0009m)
                    {
                        sec = i;
                        break;
                    }*/
                }
            }

            return sec;
        }

        private decimal offsetPosForword(decimal middlepos, decimal dis, ref int secnum)
        {
            float offset = 0;
            string[] parts = { };
            secnum = middleSecnum;
            decimal realdis = getDis(middlepos, middleSecnum, endpos, endSecnum);
            if (realdis < dis)
            {
                dis = realdis;
                secnum = endSecnum;
            }
            else
            {
                offset = 0;
                if (middleSecnum == cdl.Count + 1)
                {
                    parts = cdl[middleSecnum - 2].Split(':');
                }
                else
                {
                    parts = cdl[middleSecnum - 1].Split(':');
                }
                decimal limitdis = decimal.Parse(parts[0].Split('+')[1]) - middlepos * 1000;

                for (int i = middleSecnum; i < endSecnum; i++)
                {
                    if (limitdis - dis >= 0)
                    {
                        break;
                    }
                    else
                    {
                        secnum += 1;
                        offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                        decimal lastpos = decimal.Parse(parts[1].Split('+')[1]);
                        if ((i + 1) == cdl.Count)
                        {
                            parts = cdl[i - 1].Split(':');
                        }
                        else
                        {
                            parts = cdl[i].Split(':');
                        }
                        limitdis += (decimal.Parse(parts[0].Split('+')[1]) - lastpos);
                    }
                }
            }

            RightDis = dis;
            return middlepos + dis / 1000 + (decimal)(offset / 1000);
        }

        private decimal offsetPosBackword(decimal middlepos, decimal dis, ref int secnum)
        {
            float offset = 0;
            string[] parts = { };
            secnum = middleSecnum;
            decimal realdis = getDis(startpos, startSecnum, middlepos, middleSecnum);
            if (realdis < dis)
            {
                dis = realdis;
                secnum = startSecnum;
            }
            else
            {
                offset = 0;
                if (middleSecnum != 1)
                {
                    parts = cdl[middleSecnum - 2].Split(':');
                    decimal limitdis = middlepos * 1000 - decimal.Parse(parts[1].Split('+')[1]);

                    for (int i = middleSecnum; i > startSecnum; i--)
                    {
                        if (limitdis - (decimal)dis >= 0)
                        {
                            break;
                        }
                        else
                        {
                            secnum -= 1;
                            offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                            decimal lastpos = decimal.Parse(parts[0].Split('+')[1]);
                            parts = cdl[i - 2].Split(':');
                            limitdis += (decimal.Parse(parts[1].Split('+')[1]) - lastpos);
                        }
                    }
                }
            }
            LeftDis = dis;
            return middlepos - dis / 1000 - (decimal)(offset / 1000);
        }

        private void commit()
        {
            StationDataMode dianfxdata = new StationDataMode()
            {
                HatProperty = Hat,
                LengthProperty = (float)(LeftDis + RightDis),
                RealLength = (float)(LeftDis + RightDis),
                PathDataProperty = "2:1 0:#00DC5625:#FF4500:M334,361 L436,410 M334,410 L436,361",
                PositionProperty = (float)leftedgepos,
                ScaleProperty = 10,
                SectionNumProperty = middleSecnum,
                SelectedProperty = false,
                StationNameProperty = string.Format("3:{0}:{1}+{2}:{3}+{4}:{5}+{6}:{7}",
                Name,
                leftedgepos.ToString("F3"),
                leftedgeSecnum.ToString(),
                middlepos.ToString("F3"),
                middleSecnum.ToString(),
                rightedgepos.ToString("F3"),
                rightedgeSecnum.ToString(),
                (LeftDis + RightDis).ToString("F3")),
                Type = DataType.Single
            };

            if (!isupdata)
                MessengerInstance.Send<StationDataMode>(dianfxdata, "InsertDianfx");
            else
                MessengerInstance.Send<StationDataMode>(dianfxdata, "UpdataDianfx");
        }

        private void cancel()
        {
            if (!isupdata)
                MessengerInstance.Send<StationDataMode>(null, "InsertDianfx");
            else
                MessengerInstance.Send<StationDataMode>(null, "UpdataDianfx");
        }

        private void checkSecnum()
        {
            string[] parts = { };
            int oldSecnum = MidSecnum;
            MidSecnum = int.Parse(secArray[CurentNumIndex]);

            if (MidSecnum == 1)
            {
                parts = cdl[0].Split(':');
                if (middlepos * 1000 > decimal.Parse(parts[0].Split('+')[1]))
                {
                    ischeckSecnum = false;
                    CurentNumIndex = secArray.IndexOf(oldSecnum.ToString());
                    ischeckSecnum = true;
                    System.Windows.MessageBox.Show("路段号与里程无对应关系！", "错误",
                        System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    return;
                }
            }
            else if (MidSecnum == cdl.Count + 1)
            {
                parts = cdl[MidSecnum - 2].Split(':');
                if (middlepos * 1000 < decimal.Parse(parts[1].Split('+')[1]))
                {
                    ischeckSecnum = false;
                    CurentNumIndex = secArray.IndexOf(oldSecnum.ToString());
                    ischeckSecnum = true;
                    System.Windows.MessageBox.Show("路段号与里程无对应关系！", "错误",
                       System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                parts = cdl[MidSecnum - 1].Split(':');
                if (middlepos * 1000 > decimal.Parse(parts[0].Split('+')[1]))
                {
                    ischeckSecnum = false;
                    CurentNumIndex = secArray.IndexOf(oldSecnum.ToString());
                    ischeckSecnum = true;
                    System.Windows.MessageBox.Show("路段号与里程无对应关系！", "错误",
                        System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    return;
                }
                parts = cdl[MidSecnum - 2].Split(':');
                if (middlepos * 1000 < decimal.Parse(parts[1].Split('+')[1]))
                {
                    ischeckSecnum = false;
                    CurentNumIndex = secArray.IndexOf(oldSecnum.ToString());
                    ischeckSecnum = true;
                    System.Windows.MessageBox.Show("路段号与里程无对应关系！", "错误",
                       System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    return;
                }
            }
            refreshData(false);            
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
                        cancel();
                    }));
            }
        }

        private RelayCommand _checkSecnumCommand;

        /// <summary>
        /// Gets the CheckSecNumCommand.
        /// </summary>
        public RelayCommand CheckSecNumCommand
        {
            get
            {
                return _checkSecnumCommand
                    ?? (_checkSecnumCommand = new RelayCommand(
                    () =>
                    {
                        if (ischeckSecnum)
                            checkSecnum();
                    }));
            }
        }
    }
}