using GalaSoft.MvvmLight;
using Inter_face.Models;
using System.Collections.Generic;

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
        private float startpos;
        private int startSecnum;
        private float endpos;
        private int endSecnum;
        private float leftedgepos;
        private int leftedgeSecnum;
        private float rightedgepos;
        private int rightedgeSecnum;
        private float middlepos;
        private int middleSecnum;
        private List<string> cdl;

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

        private float _leftdisProperty = 0;

        /// <summary>
        /// Sets and gets the LeftDis property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public float LeftDis
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
                refreshData();
                RaisePropertyChanged(LeftDisPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="RightDis" /> property's name.
        /// </summary>
        public const string RightDisPropertyName = "RightDis";

        private float _rightdisProperty = 0;

        /// <summary>
        /// Sets and gets the RightDis property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public float RightDis
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
        public UpdataDianFXViewModel()
        {
            MessengerInstance.Register<DianFXneededInfosMode>(this, "DfxInputInfos",
                p =>
                {
                    string[] infos = p.DfxInfosProperty.Split(':');
                    cdl = p.CdlListProperty;

                    startpos = float.Parse(p.LeftPosProperty.Split('+')[1]);
                    startSecnum = int.Parse(p.LeftPosProperty.Split('+')[0]);

                    endpos = float.Parse(p.RightPosProperty.Split('+')[1]);
                    endSecnum = int.Parse(p.RightPosProperty.Split('+')[0]);

                    middlepos = float.Parse(infos[3].Split('+')[1]);
                    middleSecnum = int.Parse(infos[3].Split('+')[0]);

                    leftedgepos = float.Parse(infos[2].Split('+')[1]);
                    leftedgeSecnum = int.Parse(infos[2].Split('+')[0]);

                    rightedgepos = float.Parse(infos[4].Split('+')[1]);
                    rightedgeSecnum = int.Parse(infos[4].Split('+')[0]);

                    StartShowPos = formatShowpos(startpos, startSecnum);
                    EndShowPos = formatShowpos(endpos, endSecnum);

                    StartDis = getDis(leftedgepos, leftedgeSecnum, startpos, startSecnum).ToString("F3");
                    Enddis = getDis(rightedgepos, rightedgeSecnum, endpos, endSecnum).ToString("F3");

                    LeftDis = getDis(middlepos, middleSecnum, leftedgepos, leftedgeSecnum);
                    RightDis = getDis(rightedgepos, rightedgeSecnum, middlepos, middleSecnum);

                    Hat = getHat(middleSecnum);
                    PartI = int.Parse(middlepos.ToString("F3").Split('.')[0]);
                    PartII = int.Parse(middlepos.ToString("F3").Split('.')[1]);

                    Name = infos[1];
                });
        }

        private float getDis(float fstPos, int fstSecnum, float secPos, int secSecnum)
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

            return secPos - fstPos - offset;
        }

        private void refreshData()
        {
            middlepos = (PartI * 1000 + PartII) / 1000;
            middleSecnum = getSecnum(middlepos);
            if (middleSecnum != 0)
            {
                Hat = getHat(middleSecnum);

                leftedgepos = (float)offsetPosBackword(middlepos, LeftDis, ref leftedgeSecnum);
                rightedgepos = (float)offsetPosForword(middlepos, RightDis, ref rightedgeSecnum);
                LeftEdgePos = formatShowpos(leftedgepos, leftedgeSecnum);
                RightEdgePos = formatShowpos(rightedgepos, rightedgeSecnum);

                StartDis = getDis(leftedgepos, leftedgeSecnum, startpos, startSecnum).ToString("F0");
                Enddis = getDis(endpos, endSecnum, rightedgepos, rightedgeSecnum).ToString("F0");
            }

        }
        private string formatShowpos(float pos, int Secnum)
        {
            string posInstring = pos.ToString("F3");
            string part1 = posInstring.Split('.')[0];
            string part2 = posInstring.Split('.')[1];
            string hat = string.Empty;

            if (Secnum == cdl.Count)
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
            if (secNum == cdl.Count)
            {
                return cdl[secNum - 2].Split(':')[1].Split('+')[0];
            }
            else
            {
                return cdl[secNum - 1].Split(':')[0].Split('+')[0];
            }
        }

        private int getSecnum(float pos)
        {
            float usedPos = pos * 1000;
            int sec = 0;
            int matchedtimes = 0;
            string[] parts = { };

            for (int i = startSecnum; i <= endSecnum; i++)
            {
                if (i == cdl.Count)
                {
                    parts = cdl[i - 2].Split(':');
                    if ((decimal)usedPos - decimal.Parse(parts[1].Split('+')[1]) >= 0.0009m)
                    {
                        sec = i;
                        matchedtimes++;
                    }
                    else if (decimal.Parse(parts[0].Split('+')[1]) - (decimal)usedPos >= 0.0009m)
                    {
                        sec = i - 1;
                        matchedtimes++;
                    }
                }
                else
                {
                    parts = cdl[i - 1].Split(':');
                    if ((decimal)usedPos - decimal.Parse(parts[1].Split('+')[1]) >= 0.0009m)
                    {
                        sec = i + 1;
                        matchedtimes++;
                    }
                    else if (decimal.Parse(parts[0].Split('+')[1]) - (decimal)usedPos >= 0.0009m)
                    {
                        sec = i;
                        matchedtimes++;
                    }
                }
            }

            return matchedtimes > 1 ? 0 : sec;
        }

        private decimal offsetPosForword(float middlepos, float dis, ref int secnum)
        {
            float offset = 0;
            string[] parts = { };
            secnum = middleSecnum;
            float realdis = getDis(endpos, endSecnum, middlepos, middleSecnum);
            if (realdis < dis)
            {
                dis = realdis;
                secnum = endSecnum;
            }
            else
            {
                offset = 0;
                if (middleSecnum == cdl.Count)
                {
                    parts = cdl[middleSecnum - 2].Split(':');
                }
                else
                {
                    parts = cdl[middleSecnum - 1].Split(':');
                }
                float limitdis = float.Parse(parts[0].Split('+')[1]) - middlepos;

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
                        float lastpos = float.Parse(parts[1].Split('+')[1]);
                        if ((i + 1) == cdl.Count)
                        {
                            parts = cdl[i - 1].Split(':');
                        }
                        else
                        {
                            parts = cdl[i].Split(':');
                        }
                        limitdis += (float.Parse(parts[0].Split('+')[1]) - lastpos);
                    }
                }
            }

            return (decimal)middlepos + (decimal)dis + (decimal)offset;
        }

        private decimal offsetPosBackword(float middlepos, float dis, ref int secnum)
        {
            float offset = 0;
            string[] parts = { };
            secnum = middleSecnum;
            float realdis = getDis(middlepos, middleSecnum, startpos, startSecnum);
            if (realdis < dis)
            {
                dis = realdis;
                secnum = startSecnum;
            }
            else
            {
                offset = 0;
                if (middleSecnum == cdl.Count)
                {
                    parts = cdl[middleSecnum - 2].Split(':');
                }
                else
                {
                    parts = cdl[middleSecnum - 1].Split(':');
                }
                float limitdis = middlepos - float.Parse(parts[1].Split('+')[1]);

                for (int i = middleSecnum; i > startSecnum; i--)
                {
                    if (limitdis - dis >= 0)
                    {
                        break;
                    }
                    else
                    {
                        secnum -= 1;
                        offset += (float.Parse(parts[1].Split('+')[1]) - float.Parse(parts[0].Split('+')[1]));
                        float lastpos = float.Parse(parts[0].Split('+')[1]);
                        parts = cdl[i - 2].Split(':');
                        limitdis += (float.Parse(parts[1].Split('+')[1]) - lastpos);
                    }
                }
            }

            return (decimal)middlepos - (decimal)dis - (decimal)offset;
        }

        private void commit()
        {
            StationDataMode dianfxdata = new StationDataMode()
            {
                HatProperty = Hat,
                LengthProperty = LeftDis + RightDis,
                PathDataProperty = "5:6 2 1 2:#00DC5625:#FF000000:M0,0 L50,0",
                PositionProperty = middlepos,
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

            MessengerInstance.Send<StationDataMode>(dianfxdata, "UpdataDianfx");
        }
    }
}