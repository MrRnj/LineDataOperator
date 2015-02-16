using GalaSoft.MvvmLight;
using ExtractData;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inter_face.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;

namespace Inter_face.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ShowLdhViewModel : ViewModelBase
    {
        private List<LdhinfoModel> ldhInfo;

        public List<LdhinfoModel> LdhInfoProperty
        {
            get { return ldhInfo; }
            set { ldhInfo = value; }
        }
 
        /// <summary>
        /// Initializes a new instance of the ShowLdhViewModel class.
        /// </summary>
        public ShowLdhViewModel()
        {
            ldhInfo = new List<LdhinfoModel>();
            MessengerInstance.Register<string[]>(this, "showldh",
                p =>
                {
                    int n = 0;
                    string[] parts;
                    string[] nextparts;

                    for (int i = 0; i < p.Length; i++)
                    {
                        parts = p[i].Split(':');                        

                        if (i == 0)
                        {
                            LdhInfoProperty.Add(new LdhinfoModel()
                            {
                                LdhProperty = (++n).ToString(),
                                RangeProperty = string.Format("<{0} {1}", parts[0].Split('+')[0],
                                (float.Parse(parts[0].Split('+')[1]) / 1000).ToString("F3"))
                            });
                        }

                        if (i == p.Length - 1)
                        {
                            LdhInfoProperty.Add(new LdhinfoModel()
                            {
                                LdhProperty = (++n).ToString(),
                                RangeProperty = string.Format("{0} {1}<", parts[1].Split('+')[0],
                                (float.Parse(parts[1].Split('+')[1]) / 1000).ToString("F3"))
                            });
                            continue;
                        }

                        nextparts = p[i + 1].Split(':');
                        LdhInfoProperty.Add(new LdhinfoModel()
                        {
                            LdhProperty = (++n).ToString(),
                            RangeProperty = string.Format("{0} {1}--{2} {3}", parts[1].Split('+')[0], 
                            (float.Parse(parts[1].Split('+')[1]) / 1000).ToString("F3"),
                            nextparts[0].Split('+')[0],
                            (float.Parse(nextparts[0].Split('+')[1]) / 1000).ToString("F3"))
                        });
                    }
                });
        }

        private void unregMsg()
        {
            MessengerInstance.Unregister<string[]>(this, "showldh");
        }

        private RelayCommand _unregmsgCommand;

        /// <summary>
        /// Gets the UnregMsgCommand.
        /// </summary>
        public RelayCommand UnregMsgCommand
        {
            get
            {
                return _unregmsgCommand
                    ?? (_unregmsgCommand = new RelayCommand(
                                          () =>
                                          {
                                              unregMsg();
                                          }));
            }
        }
    }
}