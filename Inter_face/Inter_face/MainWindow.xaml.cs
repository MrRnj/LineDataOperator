using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Fluent;
using ExtractData;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Inter_face
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        string pdfilepath ;
        string pdtempfilepath;
        string qxfilepath;
        string qxtempfilepath;
        string bjfilepath;
        string bjtemptfilepath;
        string basefilepath;
        int last_star_pos;

        List<ExtractData.ChangeToTxt.PoduOutputData> pdx;
        List<ExtractData.ChangeToTxt.PoduOutputData> pds;
        List<ExtractData.ChangeToTxt.QuxianOutputData> qxx;
        List<ExtractData.ChangeToTxt.QuxianOutputData> qxs;
        List<ExtractData.ChangeToTxt.CheZhanOutputData> bjx;
        List<ExtractData.ChangeToTxt.CheZhanOutputData> bjs;
        

        OpenFileDialog openacessfile;
        SaveFileDialog savetxtfile;
        CheckDatasign cds;
        CheckDataLogic cdl;
        ModifyFilenamesWindow modifyfilenameswindow;
        ExtractData.WorkSheetBase worksheetbase;
        ExtractData.GraphyDataOper gdo;
        int maxprocessbarvalue = 0;
        //string result;
       // ProcessForm pf;

        public MainWindow()
        {
            InitializeComponent();
            openacessfile = new OpenFileDialog();
            initialWorkbooks();

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<string>(this, "ReadDataError", p =>
            {
                AddInfobox(p, string.Empty, string.Empty, 0, "1");
            });

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<string>(this, "ReadDataErrorWithOperate", p =>
            {
                string msg = p.Split('|')[0];
                string[] moreinfo = p.Split('|')[1].Split('*');

                AddInfobox(msg, moreinfo[2], moreinfo[1], int.Parse(moreinfo[0]), "1");
            });

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<string>(this, "ReadDataRight", p =>
            {
                AddInfobox(p, string.Empty, string.Empty, 0, "0");
            });

            Messenger.Default.Register<string[]>(this, "sendFilenames", (p)
                =>
                {
                    if (modifyfilenameswindow != null)
                    {
                        modifyfilenameswindow.Close();
                    }

                    if (p.Length != 0)
                    {
                        FillExcel fe = new FillExcel(pdfilepath, qxfilepath, bjfilepath);
                        fe.DatapathChanged += new FillExcel.DatapathChangedEvent(fe_DatapathChanged);
                        fe.Loaderror += new FillExcel.LoaderrorEventhandler(fe_Loaderror);

                        fe.datapathes = p;

                        maxprocessbarvalue = 3 * p.Length * 10;
                        this.ProgressBar.Maximum = maxprocessbarvalue;
                        this.ProgressBar.Value = 0;
                        this.ProgressBar.Visibility = Visibility.Visible;

                        this.loadformdatabase.IsEnabled = false;

                        Thread fethread = new Thread(new ThreadStart(fe.FillData));
                        fethread.Start();
                    }
                });
        }

        private void sendDispatcher()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<System.Windows.Threading.Dispatcher>
                (this.Dispatcher, "Dispatcher");
        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            sendDispatcher();
        }


        private void initialWorkbooks()
        {
            last_star_pos = 0;
            pdx = new List<ChangeToTxt.PoduOutputData>();
            pds = new List<ChangeToTxt.PoduOutputData>();
            qxx = new List<ChangeToTxt.QuxianOutputData>();
            qxs = new List<ChangeToTxt.QuxianOutputData>();
            bjx = new List<ChangeToTxt.CheZhanOutputData>();
            bjs = new List<ChangeToTxt.CheZhanOutputData>();
            basefilepath = ReadSettings.GetBasefilepath();
            pdfilepath = ReadSettings.GetPdfilepath();
            pdtempfilepath = ReadSettings.GetPdtempfilepath();
            qxfilepath = ReadSettings.GetQxfilepath();
            qxtempfilepath = ReadSettings.GetQxtempfilepath();
            bjfilepath = ReadSettings.GetBjfilepath();
            bjtemptfilepath = ReadSettings.GetBjtempfilepath();
            IniSheets.CleanXhDataFile();
            gdo = GraphyDataOper.CreatOper(pdtempfilepath,
                bjtemptfilepath,
                qxtempfilepath,
                ReadSettings.GetXhDatafilepath());
            Messenger.Default.Send<ExtractData.GraphyDataOper>(gdo, "gdo");
            
        }

        #region loaddatabase
        private void loadformdatabase_Click(object sender, RoutedEventArgs e)
        {
            IniSheets.MakeCleanBooks(pdfilepath, qxfilepath, bjfilepath, pdtempfilepath, qxtempfilepath, bjtemptfilepath);
            this.ProcessLabel.Content = "工作表初始化完成";

            openacessfile.Filter = "access files (*.mdb)|*.mdb|All files (*.*)|*.*";
            openacessfile.FilterIndex = 1;
            openacessfile.Multiselect = true;

            if (openacessfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] datapathes = openacessfile.FileNames;
                if (datapathes.Length > 1)
                {
                    modifyfilenameswindow = new ModifyFilenamesWindow();
                    Messenger.Default.Send<string[]>(datapathes, "ModifyFilePos");                    
                    modifyfilenameswindow.ShowDialog();
                }
                else
                {
                    Messenger.Default.Send<string[]>(datapathes, "sendFilenames");
                }
            }

        }

        void fe_Loaderror(object sender, FillExcel.LoaderrorEventArgs e)
        {
            System.Windows.MessageBox.Show(e.Errormessage, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            this.statugrid.Dispatcher.BeginInvoke(new Action(() =>
           {
               this.ProgressBar.Visibility = Visibility.Hidden;
               this.loadformdatabase.IsEnabled = true;
           }));
        }

        void fe_DatapathChanged(object sender, FulfillSource.DatapathChangedEventArgs e)
        {
            string labcontent = string.Format("正在读取:{0}", e.CurrentDatapath);

            this.statugrid.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.ProcessLabel.Content = labcontent;
                this.ProgressBar.Value += 10;
                //result += this.ProcessLabel.Content;
                if (this.ProgressBar.Value == this.ProgressBar.Maximum)
                {
                    this.ProcessLabel.Content = "读取完成";
                    this.ProgressBar.Visibility = Visibility.Hidden;
                    this.loadformdatabase.IsEnabled = true;
                    AddInfobox("数据读取完成", string.Empty, string.Empty, 0, "3");
                }
            }));
        }
        #endregion        

        #region opensourcesheet
        private void openpdfile_Click(object sender, RoutedEventArgs e)
        {
            //worksheetbase = ExtractData.WorkSheetBase.CreatSingleBase();

            try
            {
               // worksheetbase.OpenWorkBook(pdfilepath);
                gdo.OpenWorkBook(pdfilepath);
            }

            catch (ExtractData.WorkSheetBase.WorksheetNotOnlyException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (ExtractData.WorkSheetBase.OnlyoneWorksheetException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (System.Exception ex) 
            {
                System.Windows.MessageBox.Show(ex.Message, "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            finally 
            {
                 pds.Clear();
                 pdx.Clear();
            }
        }

        private void openqxfile_Click(object sender, RoutedEventArgs e)
        {
            //worksheetbase = ExtractData.WorkSheetBase.CreatSingleBase();

            try
            {                
                //worksheetbase.OpenWorkBook(qxfilepath);
                gdo.OpenWorkBook(qxfilepath);
            }

            catch (ExtractData.WorkSheetBase.WorksheetNotOnlyException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (ExtractData.WorkSheetBase.OnlyoneWorksheetException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            finally
            {
                qxs.Clear();
                qxx.Clear();
            }
        }

        private void openbjfile_Click(object sender, RoutedEventArgs e)
        {
           // worksheetbase = ExtractData.WorkSheetBase.CreatSingleBase();

            try
            {                
               // worksheetbase.OpenWorkBook(bjfilepath);
                gdo.OpenWorkBook(bjfilepath);
            }

            catch (ExtractData.WorkSheetBase.WorksheetNotOnlyException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (ExtractData.WorkSheetBase.OnlyoneWorksheetException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            finally
            {
                bjs.Clear();
                bjx.Clear();
            }
        }
        #endregion

        #region openmergesheet
        private void opentemppdfile_Click(object sender, RoutedEventArgs e)
        {
            //worksheetbase = ExtractData.WorkSheetBase.CreatSingleBase();

            try
            {
                //worksheetbase.OpenWorkBook(pdtempfilepath);
                gdo.OpenWorkBook(pdtempfilepath);
            }

            catch (ExtractData.WorkSheetBase.WorksheetNotOnlyException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (ExtractData.WorkSheetBase.OnlyoneWorksheetException ex)
            {
                System.Windows.MessageBox.Show("无坡度数据！", "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
           

        private void opentempqxfile_Click(object sender, RoutedEventArgs e)
        {
           // worksheetbase = ExtractData.WorkSheetBase.CreatSingleBase();

                try
                {
                    //worksheetbase.OpenWorkBook(qxtempfilepath);
                    gdo.OpenWorkBook(qxtempfilepath);
                }

                catch (ExtractData.WorkSheetBase.WorksheetNotOnlyException ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                catch (ExtractData.WorkSheetBase.OnlyoneWorksheetException ex)
                {
                    System.Windows.MessageBox.Show("无曲线数据！", "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }

        private void opentempbjfile_Click(object sender, RoutedEventArgs e)
        {
           // worksheetbase = ExtractData.WorkSheetBase.CreatSingleBase();

            try
            {
                //worksheetbase.OpenWorkBook(bjtemptfilepath);
                gdo.OpenWorkBook(bjtemptfilepath);
            }

            catch (ExtractData.WorkSheetBase.WorksheetNotOnlyException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (ExtractData.WorkSheetBase.OnlyoneWorksheetException ex)
            {
                System.Windows.MessageBox.Show("无标记数据！", "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion     
        
        #region CheckdataSign
        
        private void checkdatasign_Click(object sender, RoutedEventArgs e)
        {
            this.ProgressBar.Value = 0;
            this.ProgressBar.Maximum = 60;
            this.ProgressBar.Visibility = Visibility.Visible;
            cds = new CheckDatasign(pdfilepath, pdtempfilepath, qxfilepath, qxtempfilepath, bjfilepath, bjtemptfilepath, last_star_pos);
            cds.bjs = bjs; cds.bjx = bjx; cds.qxs = qxs; cds.qxx = qxx; cds.pds = pds; cds.pdx = pdx;

            cds.ChecksignChanged += new CheckDatasign.ChecksignChangedEventhandler(cds_ChecksignChanged);
            cds.Checkfinishied += new CheckDatasign.CheckfinishiedEventhandler(cds_Checkfinishied);
            this.checkdatasign.IsEnabled = false;
            Messenger.Default.Send<bool>(true, "CleanData");
            
            Thread CheckDatasignthread = new Thread(() => 
            {
                
                try
                {                    
                    cds.checksign();
                }

                catch (ExtractData.CheckSheetDataSign.CheckWorkbookErrorException ex)
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        // System.Windows.MessageBox.Show(ex.ErrorMessage); 
                        string errormessage = string.Format("工作簿：“{0}”的工作表：“{1}”\r\n存在读取标记错误，请检查后修改！",
                                                            System.IO.Path.GetFileNameWithoutExtension(ex.ErrorSource), ex.ErrorPosition);
                        AddInfobox(errormessage, ex.ErrorSource, ex.ErrorPosition, 0, "1");
                    }));
                }

                catch (ExtractData.ReadSheetInfo.ValueMissingException vme)
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        string[] errors = vme.Message.Split('?');
                        string errormessage = string.Format("工作簿：“{0}”的工作表：“{1}”\r\n存在空值，请检查后修改！",
                                                            System.IO.Path.GetFileNameWithoutExtension(errors[0]), errors[1]);
                        AddInfobox(errormessage, errors[0], errors[1], int.Parse(errors[3]), "1");
                    }));
                }

                catch (CheckDatasign.OnlyoneSheetException ex)
                {
                    this.Dispatcher.BeginInvoke(new Action(() => { System.Windows.MessageBox.Show(ex.Message); }));
                }

                catch (System.Exception ex) 
                {
                    this.Dispatcher.BeginInvoke(new Action(() => { System.Windows.MessageBox.Show(ex.Message); }));
                }

                finally 
                {
                    this.Dispatcher.BeginInvoke(new Action(() => { this.checkdatasign.IsEnabled = true; }));
                }
            });

            CheckDatasignthread.Start();
        }

        void cds_ChecksignChanged(object sender, CheckDatasign.ChecksignChangedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.Dispatcher.BeginInvoke(new Action(() => 
                {
                    this.ProcessLabel.Content = e.CurrentChecksheet;
                    this.ProgressBar.Value += 10;
                }));
            }));
        }

        void cds_Checkfinishied(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.ProcessLabel.Content = "检查完成";
                this.checkdatasign.IsEnabled = true;
                this.ProgressBar.Visibility = Visibility.Hidden;
                AddInfobox("数据读取标记检查完毕，没发现问题！", string.Empty, string.Empty, 0, "3");
                pdx = cds.pdx; pds = cds.pds; qxx = cds.qxx; qxs = cds.qxs; bjx = cds.bjx; bjs = cds.bjs;
            }));            
        }

        #endregion

        #region ChangeToTxt
        
        private void changeS_Click(object sender, RoutedEventArgs e)
        {
            ChangeToTxt ctt = new ChangeToTxt();

            if (bjs.Count != 0 && qxs.Count != 0 && pds.Count != 0)
            {
                savetxtfile = new SaveFileDialog();
                savetxtfile.Filter = "All files (*.*)|*.*";

                if (savetxtfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string save_name = System.IO.Path.GetFileNameWithoutExtension(savetxtfile.FileName);
                    string directoryname = System.IO.Path.GetDirectoryName(savetxtfile.FileName);
                    string bjs_save_name = System.IO.Path.Combine(directoryname, save_name + ".BJS");
                    string qxs_save_name = System.IO.Path.Combine(directoryname, save_name + ".QXS");
                    string pds_save_name = System.IO.Path.Combine(directoryname, save_name + ".pdS");
                    ctt.ChangeToBjs(bjs_save_name, bjs);
                    ctt.ChangeToPds(pds_save_name, pds);
                    ctt.ChangeToQxs(qxs_save_name, qxs);

                    AddInfobox("上行文件转换完成！", string.Empty, string.Empty, 0, "3");
                }
            }
            else 
            {
                AddInfobox("请先检查读取标记！", string.Empty, string.Empty, 0, "1");
            }
        }

        private void changeX_Click(object sender, RoutedEventArgs e)
        {
            ChangeToTxt ctt = new ChangeToTxt();

            if (bjx.Count != 0 && qxx.Count != 0 && pdx.Count != 0)
            {
                savetxtfile = new SaveFileDialog();
                savetxtfile.Filter = "All files (*.*)|*.*";

                if (savetxtfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string save_name = System.IO.Path.GetFileNameWithoutExtension(savetxtfile.FileName);
                    string directoryname = System.IO.Path.GetDirectoryName(savetxtfile.FileName);
                    string bjx_save_name = System.IO.Path.Combine(directoryname, save_name + ".bjx");
                    string qxx_save_name = System.IO.Path.Combine(directoryname, save_name + ".qxx");
                    string pdx_save_name = System.IO.Path.Combine(directoryname, save_name + ".pdx");
                    ctt.ChangeToBjx(bjx_save_name, bjx);
                    ctt.ChangeToPdx(pdx_save_name, pdx);
                    ctt.ChangeToQxx(qxx_save_name, qxx);

                    AddInfobox("下行文件转换完成！", string.Empty, string.Empty, 0, "3");
                }
            }
            else 
            {
                AddInfobox("请先检查读取标记！", string.Empty, string.Empty, 0, "1");
            }
        }

        private void changeSX_Click(object sender, RoutedEventArgs e)
        {
            ChangeToTxt ctt = new ChangeToTxt();

            if (bjx.Count != 0 && qxx.Count != 0 && pdx.Count != 0)
            {
                savetxtfile = new SaveFileDialog();
                savetxtfile.Filter = "All files (*.*)|*.*";

                if (savetxtfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string save_name = System.IO.Path.GetFileNameWithoutExtension(savetxtfile.FileName);
                    string directoryname = System.IO.Path.GetDirectoryName(savetxtfile.FileName);
                    string bjx_save_name = System.IO.Path.Combine(directoryname, save_name + ".bjx");
                    string qxx_save_name = System.IO.Path.Combine(directoryname, save_name + ".qxx");
                    string pdx_save_name = System.IO.Path.Combine(directoryname, save_name + ".pdx");
                    string bjs_save_name = System.IO.Path.Combine(directoryname, save_name + ".BJS");
                    string qxs_save_name = System.IO.Path.Combine(directoryname, save_name + ".QXS");
                    string pds_save_name = System.IO.Path.Combine(directoryname, save_name + ".pdS");
                    ctt.ChangeToBjx(bjx_save_name, bjx);
                    ctt.ChangeToPdx(pdx_save_name, pdx);
                    ctt.ChangeToQxx(qxx_save_name, qxx);
                    ctt.ChangeToBjs(bjs_save_name, bjs);
                    ctt.ChangeToPds(pds_save_name, pds);
                    ctt.ChangeToQxs(qxs_save_name, qxs);

                    AddInfobox("上下行文件转换完成！", string.Empty, string.Empty, 0, "3");
                }
            }
            else 
            {
                AddInfobox("请先检查读取标记！", string.Empty, string.Empty, 0, "1");
            }
        }

        #endregion

        #region showinfobox

        private void AddInfobox(string message,string erorrsource, string position, int rowindex, string situation) 
        {
            ShowInfoBox sib = new ShowInfoBox();
            sib.Situation = situation;
            sib.Message = string.Format("{0}\r\n{1}", System.DateTime.Now.ToLongTimeString(), message);
            sib.Position = position;
            sib.Rowindex = rowindex;
            sib.FilePath = erorrsource;

            sib.ContentbuttonClick += new ShowInfoBox.ContentbuttonClickEventhandler(sib_ContentbuttonClick);
            sib.OperationbuttonClick += new ShowInfoBox.OperationbuttonClickEventhandler(sib_OperationbuttonClick);

            this.infoboxpanel.Children.Add(sib);
        }

        void sib_OperationbuttonClick(object sender, ShowInfoBox.OperationbuttonClickEventArgs e)
        {
            ShowInfoBox infobox=sender as ShowInfoBox;
            if (e.Situation == OperationSituation.Close)
            {
                this.infoboxpanel.Children.Remove(infobox);
            }
            else if (e.Situation == OperationSituation.Ignore) 
            {
                infobox.Situation = "2";
            }
        }

        void sib_ContentbuttonClick(object sender, ShowInfoBox.ContentbuttonClickEventArgs e)
        {
            try
            {                
                gdo.OpenErrorWorksheet(e.FilePath, e.Position, e.Rowindex);
            }

            catch (ExtractData.WorkSheetBase.WorksheetNotOnlyException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch 
            {
                System.Windows.MessageBox.Show("打开错误文件出错！", "错误", MessageBoxButton.OK);
            }
        }

        #endregion

        #region checkdatalogic

        private void checklogic_Click(object sender, RoutedEventArgs e)
        {
            this.ProgressBar.Value = 0;
            this.ProgressBar.Maximum = 30;           

            if (pds.Count != 0 && qxs.Count != 0 && bjs.Count != 0)
            {
                this.ProgressBar.Visibility = Visibility.Visible;
                cdl = new CheckDataLogic(pdtempfilepath, qxtempfilepath, bjtemptfilepath);
                cdl.CheckDataChanged += new CheckDataLogic.CheckDataChangedEventhandler(cdl_CheckDataChanged);
                cdl.CheckDataFinished += new CheckDataLogic.CheckDataFinishedEventhandler(cdl_CheckDataFinished);
                this.checklogic.IsEnabled = false;

                Thread CheckDatalogicthread = new Thread(() =>
                {
                    try
                    {
                        cdl.Checklogic();
                    }

                    catch (LogicErrorException ex)
                    {
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            AddInfobox(ex.ErrorMesssage, ex.Sourcepath, "下行", ex.Rowindex, "1");
                            this.ProgressBar.Value = 0;
                        }));
                    }

                    finally 
                    {
                        this.Dispatcher.BeginInvoke(new Action(() => { this.checklogic.IsEnabled = true; }));
                    }
                });

                CheckDatalogicthread.Start();
            }
            else 
            {
                AddInfobox("请先检查读取标记！", string.Empty, string.Empty, 0, "1");
            }
        }

        void cdl_CheckDataChanged(object sender, CheckDataLogic.CheckDataChangedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.ProcessLabel.Content = string.Format("正在检查：{0}", e.CurrentChecksheet);
                this.ProgressBar.Value += 10;
            }));
        }      

        void cdl_CheckDataFinished(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.ProcessLabel.Content = "检查完成";
                this.ProgressBar.Value = 0;
                this.ProgressBar.Visibility = Visibility.Collapsed;
                AddInfobox("数据逻辑检查完成，没发现任何问题！", string.Empty, string.Empty, 0, "3");
            }));
        }


        #endregion                 

        #region ExposeData

        private void changemerge_Click(object sender, RoutedEventArgs e)
        {
            string savefilname;
            ChangeMergeData cmd;

            try
            {
                savetxtfile = new SaveFileDialog();
                //savetxtfile.CheckFileExists = true;
                savetxtfile.Filter = "All files (*.*)|*.*|excel 文件|*.xlsx";
                savetxtfile.FilterIndex = 1;

                if (savetxtfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    savefilname = savetxtfile.FileName;
                    cmd = new ChangeMergeData(pdtempfilepath, qxtempfilepath, bjtemptfilepath, basefilepath, savefilname);
                    cmd.SheetChanged += new ChangeMergeData.SheetChangedEventHandler(cmd_SheetChanged);

                    maxprocessbarvalue = 30;
                    this.ProgressBar.Maximum = maxprocessbarvalue;
                    this.ProgressBar.Value = 0;
                    this.ProgressBar.Visibility = Visibility.Visible;

                    Thread mergethread = new Thread(new ThreadStart(cmd.Changemergedata));
                    mergethread.Start();
                }

            }

            catch (ExtractData.ChangeToExcel.OnlyoneWorksheetException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch
            {
                AddInfobox("输出数据出错", string.Empty, string.Empty, 0, "1");
            }
        }

        void cmd_SheetChanged(object sender, ChangeToExcel.SheetChangedEventArgs e)
        {
            string labcontent = string.Format("{0}", e.ChangeSheetName);

            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.ProcessLabel.Content = labcontent;
                this.ProgressBar.Value += 10;
                //result += this.ProcessLabel.Content;
                if (this.ProgressBar.Value == this.ProgressBar.Maximum)
                {
                    this.ProcessLabel.Content = "输出完成";
                    this.ProgressBar.Visibility = Visibility.Hidden;                    
                    AddInfobox("数据输出完成", string.Empty, string.Empty, 0, "3");
                }
            }));
        }
       

        #endregion        

        #region Load&SaveData

        private void SaveOpdata_Click(object sender, RoutedEventArgs e)
        {
            savetxtfile = new SaveFileDialog();
            //savetxtfile.CheckFileExists = true;
            savetxtfile.Filter = "处理后数据文件|*.Aop|All files (*.*)|*.*";
            savetxtfile.FilterIndex = 1;

            try
            {

                if (savetxtfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SaveLoadToZip.SaveSourcedata(savetxtfile.FileName, System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(pdtempfilepath)),
                        pdtempfilepath, qxtempfilepath, bjtemptfilepath);
                    AddInfobox("数据保存完成", string.Empty, string.Empty, 0, "3");
                }
            }

            catch (InvalidDataException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "储存出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch(ApplicationException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "储存出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
      
        private void SaveSourcedata_Click(object sender, RoutedEventArgs e)
        {
            savetxtfile = new SaveFileDialog();
            //savetxtfile.CheckFileExists = true;
            savetxtfile.Filter = "原始数据文件|*.Bop|All files (*.*)|*.*";
            savetxtfile.FilterIndex = 1;

            try
            {

                if (savetxtfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SaveLoadToZip.SaveSourcedata(savetxtfile.FileName, System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(pdfilepath)),
                        pdfilepath, qxfilepath, bjfilepath);
                    AddInfobox("数据保存完成", string.Empty, string.Empty, 0, "3");
                }
            }

            catch (InvalidDataException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "储存出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (ApplicationException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "储存出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void loadformSourceZip_Click(object sender, RoutedEventArgs e)
        {
            openacessfile = new OpenFileDialog();
            openacessfile.Filter = "原始数据文件|*.Bop|All files (*.*)|*.*";            

            try
            {
                if (openacessfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //SaveLoadToZip.DeleteFiles(pdfilepath, qxfilepath, bjfilepath);
                    SaveLoadToZip.ExtractSourcedata(openacessfile.FileName);
                    AddInfobox("数据读取完成", string.Empty, string.Empty, 0, "3");
                }
            }

            catch (InvalidDataException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (DirectoryNotFoundException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (IOException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (NotSupportedException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (ApplicationException ex) 
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void loadformOpZip_Click(object sender, RoutedEventArgs e)
        {
            openacessfile = new OpenFileDialog();
            openacessfile.Filter = "原始数据文件|*.Aop|All files (*.*)|*.*";           

            try
            {
                if (openacessfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //SaveLoadToZip.DeleteFiles(pdtempfilepath, qxtempfilepath, bjtemptfilepath);
                    SaveLoadToZip.ExtractSourcedata(openacessfile.FileName);
                    AddInfobox("数据读取完成", string.Empty, string.Empty, 0, "3");
                }
            }

            catch (InvalidDataException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (DirectoryNotFoundException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (IOException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (NotSupportedException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (ApplicationException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }       

        #endregion              

        #region CreatNewWork

        private void CreatNewWork_Click(object sender, RoutedEventArgs e)
        {
            CreatNewwork cnk = new CreatNewwork(pdfilepath, qxfilepath, bjfilepath);


            cnk.Loaderror += new CreatNewwork.LoaderrorEventhandler(cnk_Loaderror);
            cnk.DatapathChanged += new CreatNewwork.DatapathChangedEvent(cnk_DatapathChanged);           

            IniSheets.MakeCleanBooks(pdfilepath, qxfilepath, bjfilepath, pdtempfilepath, qxtempfilepath, bjtemptfilepath);
            this.ProcessLabel.Content = "工作表初始化完成";

            maxprocessbarvalue = 30;
            this.ProgressBar.Maximum = maxprocessbarvalue;
            this.ProgressBar.Value = 0;
            this.ProgressBar.Visibility = Visibility.Visible;

            CreatNewWork.IsEnabled = false;

            Thread cnthread = new Thread(new ThreadStart(cnk.Creat));
            cnthread.Start();           
        }

        void cnk_DatapathChanged(object sender, FulfillSource.DatapathChangedEventArgs e)
        {
            string labcontent = string.Format("正在新建:{0}", e.CurrentDatapath);

            this.statugrid.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.ProcessLabel.Content = labcontent;
                this.ProgressBar.Value += 10;
                //result += this.ProcessLabel.Content;
                if (this.ProgressBar.Value == this.ProgressBar.Maximum)
                {
                    this.ProcessLabel.Content = "新建完成";
                    this.ProgressBar.Visibility = Visibility.Hidden;
                    this.CreatNewWork.IsEnabled = true;
                    AddInfobox("表格新建完成", string.Empty, string.Empty, 0, "3");
                }
            }));
        }

        void cnk_Loaderror(object sender, CreatNewwork.LoaderrorEventArgs e)
        {            
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                System.Windows.MessageBox.Show(e.Errormessage, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                this.ProgressBar.Visibility = Visibility.Hidden;
                this.loadformdatabase.IsEnabled = true;
            }));
        }

        #endregion

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

            
        }
       
        private void loadbopdata(string filename)
        {
            try
            {
                SaveLoadToZip.ExtractSourcedata(filename);
                AddInfobox("数据读取完成", string.Empty, string.Empty, 0, "3");
                Title = filename;
            }

            catch (InvalidDataException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (DirectoryNotFoundException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (IOException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (NotSupportedException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (ApplicationException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void loadaopdata(string filename)
        {

            try
            {
                SaveLoadToZip.ExtractSourcedata(filename);
                AddInfobox("数据读取完成", string.Empty, string.Empty, 0, "3");
                Title = filename;
            }

            catch (InvalidDataException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (DirectoryNotFoundException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (IOException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (NotSupportedException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (ApplicationException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "读取出错", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void loaddb(string[] filepathes)
        {
            if (filepathes.Length != 0)
            {
                IniSheets.MakeCleanBooks(pdfilepath, qxfilepath, bjfilepath, pdtempfilepath, qxtempfilepath, bjtemptfilepath);
                this.ProcessLabel.Content = "工作表初始化完成";

                if (filepathes.Length > 1)
                {
                    modifyfilenameswindow = new ModifyFilenamesWindow();
                    Messenger.Default.Send<string[]>(filepathes, "ModifyFilePos");                    
                    modifyfilenameswindow.ShowDialog();
                }
                else
                {
                    Messenger.Default.Send<string[]>(filepathes, "sendFilenames");
                }
            }
        }

        private void loadformdatabase_DragEnter(object sender, System.Windows.DragEventArgs e)
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

        private void loadformdatabase_Drop(object sender, System.Windows.DragEventArgs e)
        {
            string[] filepathes = null;

            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                filepathes = (((System.Array)(e.Data.GetData(System.Windows.DataFormats.FileDrop))) as string[]);
                if (filepathes != null)
                {
                    loaddb(filepathes.Where(p => System.IO.Path.GetExtension(p).ToLower().Equals(".mdb")).ToArray());
                }
            }
        }

        private void loadformSourceZip_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            if (e.KeyStates == DragDropKeyStates.LeftMouseButton && e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] filenames = ((System.Array)(e.Data.GetData(System.Windows.DataFormats.FileDrop))) as string[];
                if (filenames != null && filenames.All(p => System.IO.Path.GetExtension(p).ToLower().Equals(".bop")))
                    e.Effects = System.Windows.DragDropEffects.Link;
                else
                    e.Effects = System.Windows.DragDropEffects.None;

            }
            else
                e.Effects = System.Windows.DragDropEffects.None;
        }

        private void loadformSourceZip_Drop(object sender, System.Windows.DragEventArgs e)
        {       

            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string filepath = (((System.Array)(e.Data.GetData(System.Windows.DataFormats.FileDrop))).GetValue(0) as string);
                if (filepath != null)
                {
                    loadbopdata(filepath);
                }
            }
        }

        private void loadformOpZip_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            if (e.KeyStates == DragDropKeyStates.LeftMouseButton && e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] filenames = ((System.Array)(e.Data.GetData(System.Windows.DataFormats.FileDrop))) as string[];
                if (filenames != null && filenames.All(p => System.IO.Path.GetExtension(p).ToLower().Equals(".aop")))
                    e.Effects = System.Windows.DragDropEffects.Link;
                else
                    e.Effects = System.Windows.DragDropEffects.None;

            }
            else
                e.Effects = System.Windows.DragDropEffects.None;
        }

        private void loadformOpZip_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string filepath = (((System.Array)(e.Data.GetData(System.Windows.DataFormats.FileDrop))).GetValue(0) as string);
                if (filepath != null)
                {
                    loadaopdata(filepath);
                }
            }
        }       
        
    }

    #region internalclass
    internal class FillExcel
    {
        string pdfilepath, qxfilepath, bjfilepath;
        ExtractData.FulfillSource ffs = null;
        public string[] datapathes;

        public FillExcel(string pdfilepath, string qxfilepath, string bjfilepath)
        {
            this.pdfilepath = pdfilepath;
            this.qxfilepath = qxfilepath;
            this.bjfilepath = bjfilepath;
            ffs = new FulfillSource(pdfilepath, qxfilepath, bjfilepath);
            ffs.DatapathChanged += new FulfillSource.DatapathChangedEventHandler(ffs_DatapathChanged);
        }

        void ffs_DatapathChanged(object sender, FulfillSource.DatapathChangedEventArgs e)
        {
            OnDatapathChanged(sender, e);
        }

        public void FillData()
        {
            try
            {                
                ffs.FillBj(datapathes);
                ffs.FillPd(datapathes);
                ffs.FillQx(datapathes);               
            }

            catch (InvalidDataException ex)
            {
                OnLoaderror(null, new LoaderrorEventArgs(ex.Message));
            }

            catch (InvalidOperationException ex)
            {
                OnLoaderror(null, new LoaderrorEventArgs(ex.Message));
            }

            catch (System.Runtime.InteropServices.COMException ce)
            {
                OnLoaderror(null, new LoaderrorEventArgs(ce.Message));
            }

            catch(System.Exception ex)
            {
                OnLoaderror(null, new LoaderrorEventArgs(ex.Message));
            }

            finally 
            {
                if (ffs != null) 
                {
                    ffs.Dispose();
                }
            }
        }

        #region DatapathChangedEvent

        public delegate void DatapathChangedEvent(object sender, ExtractData.FulfillSource.DatapathChangedEventArgs e);
        public event DatapathChangedEvent DatapathChanged;

        protected virtual void OnDatapathChanged(object sender, ExtractData.FulfillSource.DatapathChangedEventArgs e)
        {
            if (DatapathChanged != null)
            {
                DatapathChanged(sender, e);
            }
        }

        public delegate void LoaderrorEventhandler(object sender, LoaderrorEventArgs e);
        public event LoaderrorEventhandler Loaderror;

        protected virtual void OnLoaderror(object sender, LoaderrorEventArgs e)
        {
            if (Loaderror != null)
                Loaderror(sender, e);
        }

        public class LoaderrorEventArgs : EventArgs
        {
            public string Errormessage { get; set; }
            public LoaderrorEventArgs(string errormessage)
            {
                this.Errormessage = errormessage;
            }
        }

        #endregion
    }

    internal class CreatNewwork 
    {
        string pdfilepath, qxfilepath, bjfilepath;
        ExtractData.FulfillSource ffs;

        public CreatNewwork(string pdfilepath, string qxfilepath, string bjfilepath) 
        {
            this.pdfilepath = pdfilepath;
            this.qxfilepath = qxfilepath;
            this.bjfilepath = bjfilepath;
            ffs = new FulfillSource(pdfilepath, qxfilepath, bjfilepath);
        }

        public void Creat() 
        {
            try
            {
                OnDatapathChanged(this, new FulfillSource.DatapathChangedEventArgs("正在新建坡度文件"));
                ffs.CreatNewPodu();
                OnDatapathChanged(this, new FulfillSource.DatapathChangedEventArgs("正在新建曲线文件"));
                ffs.CreatNewQuxian();
                OnDatapathChanged(this, new FulfillSource.DatapathChangedEventArgs("正在新建标记文件"));
                ffs.CreatNewBiaoji();
                ffs.Dispose();
            }

            catch (InvalidDataException ex)
            {
                OnLoaderror(null, new LoaderrorEventArgs(ex.Message));
            }

            catch (InvalidOperationException ex)
            {
                OnLoaderror(null, new LoaderrorEventArgs(ex.Message));
            }

            catch 
            {
                OnLoaderror(null, new LoaderrorEventArgs("文件读取有误"));
            }
        }

        #region DatapathChangedEvent

        public delegate void DatapathChangedEvent(object sender, ExtractData.FulfillSource.DatapathChangedEventArgs e);
        public event DatapathChangedEvent DatapathChanged;

        protected virtual void OnDatapathChanged(object sender, ExtractData.FulfillSource.DatapathChangedEventArgs e)
        {
            if (DatapathChanged != null)
            {
                DatapathChanged(sender, e);
            }
        }

        public delegate void LoaderrorEventhandler(object sender, LoaderrorEventArgs e);
        public event LoaderrorEventhandler Loaderror;

        protected virtual void OnLoaderror(object sender, LoaderrorEventArgs e)
        {
            if (Loaderror != null)
                Loaderror(sender, e);
        }

        public class LoaderrorEventArgs : EventArgs
        {
            public string Errormessage { get; set; }
            public LoaderrorEventArgs(string errormessage)
            {
                this.Errormessage = errormessage;
            }
        }

        #endregion

       
    }

    internal class CheckDatasign
    {
        string pdfilepath;
        string pdtempfilepath;
        string qxfilepath;
        string qxtempfilepath;
        string bjfilepath;
        string bjtemptfilepath;
        int last_star_pos;
        CheckSheetDataSign csds = null;
        ReadSheetInfo rsi = null;
        ExChangeDir ecd = null;
        List<ExtractData.ChangeToTxt.CheZhanOutputData> bjdata = new List<ChangeToTxt.CheZhanOutputData>();
        List<ExtractData.ChangeToTxt.PoduOutputData> pddata = new List<ChangeToTxt.PoduOutputData>();
        List<ExtractData.ChangeToTxt.QuxianOutputData> qxdata = new List<ChangeToTxt.QuxianOutputData>();
        public List<ExtractData.ChangeToTxt.CheZhanOutputData> bjx;
        public List<ExtractData.ChangeToTxt.CheZhanOutputData> bjs;
        public List<ExtractData.ChangeToTxt.PoduOutputData> pdx;
        public List<ExtractData.ChangeToTxt.PoduOutputData> pds;
        public List<ExtractData.ChangeToTxt.QuxianOutputData> qxx;
        public List<ExtractData.ChangeToTxt.QuxianOutputData> qxs;

        public CheckDatasign(string pdfilepath, string pdtempfilepath, string qxfilepath,
            string qxtempfilepath, string bjfilepath, string bjtemptfilepath, int last_star_pos)
        {
            this.pdfilepath = pdfilepath;
            this.pdtempfilepath = pdtempfilepath;
            this.qxfilepath = qxfilepath;
            this.qxtempfilepath = qxtempfilepath;
            this.bjfilepath = bjfilepath;
            this.bjtemptfilepath = bjtemptfilepath;
            this.last_star_pos = last_star_pos;
        }

        public void checksign()
        {
            csds = new CheckSheetDataSign();
            rsi = new ReadSheetInfo();
            ecd = new ExChangeDir(pdtempfilepath, qxtempfilepath, bjtemptfilepath);
            WorkSheetBase.Dispose();
            bool adjust = true;

            if (System.Windows.MessageBox.Show("是否自动调整坡度数据？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.No)
            {
                adjust = false;
            }

            try
            {
                OnChecksignChanged(this, new ChecksignChangedEventArgs("正在检查标记文件"));

                try
                {
                    if (!csds.CheckBjBook(bjfilepath))
                    {
                        throw new System.Exception("检查标记文件所注标记出错");
                    }
                }
                catch (CheckSheetDataSign.OnlyoneSheetException e)
                {
                    throw new OnlyoneSheetException(e.Message);
                }
                catch (ExtractData.CheckSheetDataSign.CheckWorkbookErrorException cee)
                {
                    throw cee;
                }
                catch (ExtractData.ReadSheetInfo.ValueMissingException ve)
                {
                    throw ve;
                }
                catch (System.Exception e)
                {
                    throw e;
                }

                OnChecksignChanged(this, new ChecksignChangedEventArgs("正在检查坡度文件"));

                try
                {
                    if (!csds.CheckPdBook(pdfilepath))
                    {
                        throw new System.Exception("检查坡度文件所注标记出错");
                    }
                }
                catch (CheckSheetDataSign.OnlyoneSheetException e)
                {
                    throw new OnlyoneSheetException(e.Message);
                }
                catch (ExtractData.CheckSheetDataSign.CheckWorkbookErrorException cee)
                {
                    throw cee;
                }
                catch (ExtractData.ReadSheetInfo.ValueMissingException ve)
                {
                    throw ve;
                }
                catch (System.Exception e)
                {
                    throw e;
                }

                OnChecksignChanged(this, new ChecksignChangedEventArgs("正在检查曲线文件"));

                try
                {
                    if (!csds.CheckQxBook(qxfilepath))
                    {
                        throw new System.Exception("检查曲线文件所注标记出错");
                    }
                }
                catch (CheckSheetDataSign.OnlyoneSheetException e)
                {
                    throw new OnlyoneSheetException(e.Message);
                }
                catch (ExtractData.CheckSheetDataSign.CheckWorkbookErrorException cee)
                {
                    throw cee;
                }
                catch (ExtractData.ReadSheetInfo.ValueMissingException ve)
                {
                    throw ve;
                }
                catch (System.Exception e)
                {
                    throw e;
                }

                last_star_pos = csds.Last_star_pos;

                OnChecksignChanged(this, new ChecksignChangedEventArgs("正在合并标记文件"));
                bjdata = rsi.GetBjInfo(bjfilepath, last_star_pos);
                MergeSheet.MergeBj(bjtemptfilepath, bjdata);
                ecd.GetBj_list(ref bjx, ref bjs);

                OnChecksignChanged(this, new ChecksignChangedEventArgs("正在合并坡度文件"));
                pddata = rsi.GetPdInfo(pdfilepath, last_star_pos);
                if (!MergeSheet.MergePd(pdtempfilepath, pddata, pdfilepath, adjust))
                {
                    System.Windows.MessageBox.Show("出错了");
                }
                ecd.GetPd_list(ref pdx, ref pds);

                OnChecksignChanged(this, new ChecksignChangedEventArgs("正在合并曲线文件"));
                qxdata = rsi.GetQxInfo(qxfilepath, last_star_pos);
                MergeSheet.MergeQx(qxtempfilepath, qxdata);
                ecd.GetQx_list(ref qxx, ref qxs);

                OnCheckfinishied(this, null);
            }

            catch (ExtractData.CheckSheetDataSign.CheckWorkbookErrorException ex)
            {
                throw new ExtractData.CheckSheetDataSign.CheckWorkbookErrorException(ex.ErrorMessage, ex.ErrorSource, ex.ErrorPosition);
            }

            catch (CheckSheetDataSign.OnlyoneSheetException ex)
            {
                throw new OnlyoneSheetException(ex.Message);
            }

            catch (ExtractData.ReadSheetInfo.ValueMissingException ve)
            {
                throw ve;
            }

            finally
            {
                if (csds != null)
                {
                    csds.Dispose();
                }

                if (rsi != null)
                {
                    rsi.Dispose();
                }

                if (ecd != null)
                {
                    ecd.Dispose();
                }
            }
        }

        #region ChecksignChanged

        public delegate void ChecksignChangedEventhandler(object sender, ChecksignChangedEventArgs e);
        public event ChecksignChangedEventhandler ChecksignChanged;

        protected virtual void OnChecksignChanged(object sender, ChecksignChangedEventArgs e)
        {
            if (ChecksignChanged != null)
                ChecksignChanged(sender, e);
        }

        public class ChecksignChangedEventArgs : EventArgs
        {
            public string CurrentChecksheet { get; set; }
            public ChecksignChangedEventArgs(string currentchecksheet)
            {
                this.CurrentChecksheet = currentchecksheet;
            }
        }

        public delegate void CheckfinishiedEventhandler(object sender, EventArgs e);
        public event CheckfinishiedEventhandler Checkfinishied;

        protected virtual void OnCheckfinishied(object sender, EventArgs e)
        {
            if (Checkfinishied != null)
                Checkfinishied(sender, e);
        }

        #endregion

        #region OnlyonesheetException

        public class OnlyoneSheetException : ApplicationException
        {
            public OnlyoneSheetException(string message)
                : base(message)
            { }
        }

        #endregion
    }

    internal class CheckDataLogic 
    {
        string pdtempfilepath, qxtempfilepath, bjtemptfilepath;
        CheckSheetLogic csl = null;

        public CheckDataLogic(string pdtempfilepath, string qxtempfilepath, string bjtemptfilepath) 
        {
            this.pdtempfilepath = pdtempfilepath;
            this.qxtempfilepath = qxtempfilepath;
            this.bjtemptfilepath = bjtemptfilepath;

            csl = new CheckSheetLogic();
        }

        public void Checklogic() 
        {
            try
            {
                OnCheckDataChanged(this, new CheckDataChangedEventArgs(pdtempfilepath));
                csl.CheckPdsheetLogic(pdtempfilepath);
                OnCheckDataChanged(this, new CheckDataChangedEventArgs(qxtempfilepath));
                csl.CheckQxsheetLogic(qxtempfilepath, pdtempfilepath);
                OnCheckDataChanged(this, new CheckDataChangedEventArgs(bjtemptfilepath));
                csl.CheckBjsheetLogic(bjtemptfilepath, pdtempfilepath);
                OnCheckDataFinished(this, null);
            }

            catch (LogicErrorException ex)
            {
                throw new LogicErrorException(ex.Sourcepath, ex.Rowindex, ex.ErrorMesssage);
            }

            finally
            {
                if (csl != null)
                    csl.Dispose();
            }
        }

        #region CheckDataChanged CheckDataFinished

        public delegate void CheckDataChangedEventhandler(object sender, CheckDataChangedEventArgs e);
        public event CheckDataChangedEventhandler CheckDataChanged;

        protected virtual void OnCheckDataChanged(object sender, CheckDataChangedEventArgs e) 
        {
            if (CheckDataChanged != null)
                CheckDataChanged(sender, e);
        }

        public class CheckDataChangedEventArgs : EventArgs
        {
            public string CurrentChecksheet { get; set; }
            public CheckDataChangedEventArgs(string currentchecksheet)
            {
                this.CurrentChecksheet = currentchecksheet;
            }
        }

        public delegate void CheckDataFinishedEventhandler(object sender, EventArgs e);
        public event CheckDataFinishedEventhandler CheckDataFinished;

        protected virtual void OnCheckDataFinished(object sender, EventArgs e) 
        {
            if (CheckDataFinished != null)
                CheckDataFinished(sender, e);
        }     

        #endregion
    }

    internal class ChangeMergeData 
    {
        ChangeToExcel cte = null;
        string pdtempfilepath, qxtempfilepath, bjtemptfilepath, basefilepath, savefilepath;

        public ChangeMergeData(string pdtempfilepath, string qxtempfilepath, string bjtempfilepath, string basefilepath, string savefilepath) 
        {
            this.pdtempfilepath = pdtempfilepath;
            this.qxtempfilepath = qxtempfilepath;
            this.bjtemptfilepath = bjtempfilepath;
            this.basefilepath = basefilepath;
            this.savefilepath = savefilepath;

            cte = new ChangeToExcel();
            cte.SheetChanged += new ChangeToExcel.SheetChangedEventHandler(cte_SheetChanged);
        }

        public void Changemergedata() 
        {
            try 
            {
                cte.ChangeMergeWorkbook(pdtempfilepath, qxtempfilepath, bjtemptfilepath, basefilepath, savefilepath);
            }

            catch (ExtractData.ChangeToExcel.OnlyoneWorksheetException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch
            {
                System.Windows.MessageBox.Show("输出数据出错！", "出错了！", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void cte_SheetChanged(object sender, ChangeToExcel.SheetChangedEventArgs e)
        {
            OnSheetChanged(sender, e);
        }

        #region SheetChangedEvent

        public delegate void SheetChangedEventHandler(object sender, ChangeToExcel.SheetChangedEventArgs e);
        public event SheetChangedEventHandler SheetChanged;

        protected virtual void OnSheetChanged(object sender, ChangeToExcel.SheetChangedEventArgs e) 
        {
            if (SheetChanged != null)
                SheetChanged(sender, e);
        }

        #endregion

    }

    #endregion
    
}
