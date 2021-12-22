using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using MySql.Data.MySqlClient;
using System.Data;
using System.Xml;
using System.ComponentModel;
using Color = System.Windows.Media.Color;
using Path = System.Windows.Shapes.Path;
using Point = System.Windows.Point;
using System.Threading;
using System.Windows.Controls.Primitives;
using MessageBox = System.Windows.MessageBox;

namespace Test
{
    /// <summary>
    /// 编辑页面.xaml 的交互逻辑
    /// </summary>
    public partial class Edit : Window
    {
        int MaxSort = 1;

        /// <summary>
        /// 实例化所有子窗口
        /// </summary>
        NewFileBaseConfigure sub = new NewFileBaseConfigure();
        ProgramWizard programWizard = new ProgramWizard();
        ControlPage2 controlPage2 = new ControlPage2();
        SaveFileDialog XmlSaveFile = new SaveFileDialog();


        Config config = new Config();
        object propertyGrid = new object();
        SaveFileDialog saveFile = new SaveFileDialog();
        // 创建打开文件夹
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

        static String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456;database=easycoat;";
        string strsql = null;
        MySqlConnection conn = new MySqlConnection(connetStr);
        
        ObservableCollection<object> trajectoryPars = new ObservableCollection<object>();

        /// <summary>
        /// 实例化所需要用的类
        /// </summary>
        TrajectoryPar trajectoryPar = new TrajectoryPar();
        TrajectoryLine trajectoryLine = new TrajectoryLine();
        Trajectory trajectory = new Trajectory();
        TrajectoryRound trajectoryRound = new TrajectoryRound();
        Line line1 = new Line();

        Path x_Arrow = new Path();//x轴箭头,绘制圆所需要的path

        //string XMLPath = "XMLPath";
        string XMLPath;
        public int Checksort = 0;

        /// <summary>
        /// 滑块的Y
        /// </summary>
        static double nTop;
        /// <summary>
        /// 滑块的X轴
        /// </summary>
        static double nLeft;



        /// <summary>
        /// 生成坐标系
        /// </summary>
        public void DrawArrow()
        {
            Line x_axis = new Line();//x轴
            Line y_axis = new Line();//y轴
            x_axis.Stroke = System.Windows.Media.Brushes.Black;
            y_axis.Stroke = System.Windows.Media.Brushes.Black;
            x_axis.StrokeThickness = 2;
            y_axis.StrokeThickness = 2;
            x_axis.X1 = 20;
            x_axis.Y1 = 20;

            x_axis.X2 = 500;
            x_axis.Y2 = 20;

            y_axis.X1 = 20;
            y_axis.Y1 = 20;
            y_axis.X2 = 20;
            y_axis.Y2 = 330;
            this.chartCanvas.Children.Add(x_axis);
            this.chartCanvas.Children.Add(y_axis);

            Path x_axisArrow = new Path();//x轴箭头
            Path y_axisArrow = new Path();//y轴箭头
            x_axisArrow.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            y_axisArrow.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            PathFigure x_axisFigure = new PathFigure();
            x_axisFigure.IsClosed = true;
            x_axisFigure.StartPoint = new Point(500, 16); // 路径的起点
            x_axisFigure.Segments.Add(new LineSegment(new Point(500, 24), false)); //第2个点
            x_axisFigure.Segments.Add(new LineSegment(new Point(510, 20), false)); //第3个点
            PathFigure y_axisFigure = new PathFigure();
            y_axisFigure.IsClosed = true;
            y_axisFigure.StartPoint = new Point(16, 330);                          //路径的起点
            y_axisFigure.Segments.Add(new LineSegment(new Point(24, 330), false)); //第2个点
            y_axisFigure.Segments.Add(new LineSegment(new Point(20, 340), false)); //第3个点
            PathGeometry x_axisGeometry = new PathGeometry();
            PathGeometry y_axisGeometry = new PathGeometry();
            x_axisGeometry.Figures.Add(x_axisFigure);
            y_axisGeometry.Figures.Add(y_axisFigure);
            x_axisArrow.Data = x_axisGeometry;
            y_axisArrow.Data = y_axisGeometry;
            this.chartCanvas.Children.Add(x_axisArrow);
            this.chartCanvas.Children.Add(y_axisArrow);
            DrawScale();
        }
        /// <summary>
        /// 生成其坐标，箭头，刻度等
        /// </summary>
        public void DrawScale() 
        {
            for (int i = 1; i < 13; i++)
            {
                Line x_scale = new Line(); //主x轴标尺
                x_scale.StrokeEndLineCap = PenLineCap.Triangle;
                x_scale.StrokeThickness = 1;
                x_scale.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                x_scale.X1 = 20 + i * 45;
                x_scale.X2 = x_scale.X1;
                x_scale.Y1 = 30;
                x_scale.StrokeThickness = 2;
                x_scale.Y2 = x_scale.Y1 - 10;
                this.chartCanvas.Children.Add(x_scale);
                Line x_in = new Line();//x轴轴辅助标尺
                x_in.Stroke = System.Windows.Media.Brushes.LightGray;
                x_in.StrokeThickness = 0.5;
                x_in.X1 = 20 + i * 45;
                x_in.Y1 = 310;
                x_in.X2 = 20 + i * 45;
                x_in.Y2 = 30;
                this.chartCanvas.Children.Add(x_in);
            }
            for (int j = 0; j < 30; j++)
            {
                Line y_scale = new Line(); //主Y轴标尺
                y_scale.StrokeEndLineCap = PenLineCap.Triangle;
                y_scale.StrokeThickness = 1;
                y_scale.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                y_scale.X1 = 20;            //原点x=40
                if (j % 5 == 0)
                {
                    y_scale.StrokeThickness = 3;
                    y_scale.X2 = y_scale.X1 + 8;//大刻度线
                }
                else
                    y_scale.X2 = y_scale.X1 + 4;//小刻度线
                y_scale.Y1 = 320 - j * 10;  //每10px作一个刻度
                y_scale.Y2 = y_scale.Y1;
                this.chartCanvas.Children.Add(y_scale);
            }
            for (int i = 1; i < 6; i++)
            {
                Line y_in = new Line();//y轴辅助标尺
                y_in.Stroke = System.Windows.Media.Brushes.LightGray;
                y_in.StrokeThickness = 0.5;
                y_in.X1 = 30;
                y_in.Y1 = 320 - i * 50;
                y_in.X2 = 500;
                y_in.Y2 = 320 - i * 50;
                this.chartCanvas.Children.Add(y_in);
            }
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            DrawArrow();
        }

        #region 暂时放弃的部分

        private void AddMessage_Click(object sender, EventArgs e)
        {
            //string sql = "insert into trajectory values('" + cuowu.Content + "')";

            //MySqlCommand mySqlCommand = new MySqlCommand();


            //ClsDB.ClsDBControl DBC = new OptDB.ClsDB.ClsDBControl();
            //string strSql = "insert into trajectory values(" + this.cuowu.Content.Trim().ToString() + "," +
            //this.textBox2.Text.Trim().ToString() + "," + this.textBox3.Text.Trim().ToString() + ")";
            //if (DBC.insertDB(strSql))
            //{
            //    MessageBox.Show("OK");
            //}
        }

        #region 定义了一个在comboBox的类，其_classid为序号，_className为内容

        public class ClassConfigInfo
        {
            int _classId;
            string _className;
            public int ClassId { get => _classId; set => _classId = value; }
            public string ClassName { get => _className; set => _className = value; }
        }
        #endregion
        #region  假定这是从数据库提出的 ，使用使用conboBox类创建数据，使用此方法调用数据
        public List<ClassConfigInfo> GetConfigItemInfos()
        {
            List<ClassConfigInfo> list = new List<ClassConfigInfo>();
            list.AddRange(new ClassConfigInfo[]
            {
                new ClassConfigInfo()
                {
                    ClassId = 0,
                    ClassName = "    配置1"
                },
                new ClassConfigInfo()
                {
                    ClassId = 1,
                    ClassName = "    配置2"
                },
                new ClassConfigInfo()
                {
                    ClassId = 2,
                    ClassName = "    配置3"
                },
                new ClassConfigInfo()
                {
                    ClassId = 3,
                    ClassName = "    配置4"
                },
                new ClassConfigInfo()
                {
                    ClassId = 4,
                    ClassName = "    配置5"
                }
            }
            );
            return list;
        }

        public class ClassRunMode
        {
            int _classId;
            string _className;
            public int ClassId { get => _classId; set => _classId = value; }
            public string ClassName { get => _className; set => _className = value; }
        }
        #region 
        private List<ClassRunMode> GetRoutModeInfos()
        {
            List<ClassRunMode> list = new List<ClassRunMode>();
            list.AddRange(new ClassRunMode[]
            {
                new ClassRunMode()
                {
                    ClassId = 0,
                    ClassName = "模式1"
                },
                new ClassRunMode()
                {
                    ClassId = 1,
                    ClassName = "模式2"
                },
                new ClassRunMode()
                {
                    ClassId = 2,
                    ClassName = "模式3"
                },
                new ClassRunMode()
                {
                    ClassId = 3,
                    ClassName = "模式4"
                },
                new ClassRunMode()
                {
                    ClassId = 4,
                    ClassName = "模式5"
                }
            }
            );
            return list;
        }
        #endregion
        #endregion
        #endregion

        public bool insertDB(string strsql)
        {
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand();
                string strSqlStr = "insert easycoat values('第n条数据');";
                mySqlCommand.CommandText = strsql;
                mySqlCommand.Connection = conn;
                mySqlCommand.ExecuteNonQuery();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public Edit()
        {
            InitializeComponent();
        }



        private void btnNewFile(object sender, RoutedEventArgs e)
        {
            XmlSaveFile.ShowDialog();
        }



        public void Edit_loaded(object sender, RoutedEventArgs e)
        {
            // 配置文件赋值
            config.ReadFile();
            zhengxian.IsChecked = Config.Check_Time;

            // 属性表赋值,暂未完成修改
            Trajectory proPertytrajectory = new Trajectory();
            foreach (var item in proPertytrajectory.collection)
            {
                string typeName = item.GetType().ToString();
                if (true)
                {

                }
            }
            string xmlPathProperty = "E:\\desket\\GitDevelop\\MySelf\\EasyCoat-WPF\\XML\\Test.xml";
            
            //propertyGrid = proPertytrajectory.OpenTarjectory2(xmlPathProperty, 3);
            OptionsPropertyGrid.SelectedObject = propertyGrid;

            #region 数据库调用，暂时不用，改用调用xml文件
            //string insertsql = "";

            //string query = "SELECT XuHao,type,List FROM trajectory ORDER BY XuHao ASC";
            //if (conn.State == ConnectionState.Closed)
            //{
            //    conn.Open();
            //}

            //MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //DGcommondList.ItemsSource = dt.DefaultView;

            //CDbMysql cDbMysql = new CDbMysql(connetStr);

            //if (cDbMysql.db_header.State == ConnectionState.Closed)
            //{
            //    cuowu.Content = "连接有问题";
            //}
            //else
            //    cuowu.Content = "连接正常";
            //strsql = "SELECT * FROM trajectory";
            ////DGcommondList.ItemsSource = cDbMysql.GetDataTable("strsql").DefaultView;
            #endregion
        }



        private void EditClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Config.Check_Time = (bool)zhengxian.IsChecked;
            config.WriteFile();

            e.Cancel = true;
            this.Hide();
            this.Owner.Visibility = Visibility.Visible;//显示父窗体
        }

        private void ProductionConguration(object sender, RoutedEventArgs e)
        {
            ProductionConfiguration productionConfiguration = new ProductionConfiguration();
            productionConfiguration.Show();
        }
        private void PressControl(object sender, RoutedEventArgs e)
        {
            PressureState pressureState = new PressureState();
            pressureState.Show();
        }
        private void NeederFinder_click(object sender, RoutedEventArgs e)
        {
            NeedleFinderZ finder = new NeedleFinderZ();
            finder.Show();
        }
        private void FanWidth_click(object sender, RoutedEventArgs e)
        {
            //弧形宽度的class名好像写错了。。。将就用把
            PressureAdjust pressureAdjust = new PressureAdjust();
            pressureAdjust.Show();
        }
        private void MaintenanceConfiguration(object sender, RoutedEventArgs e)
        {
            MaintenanceConfiguration maintenanceConfiguration = new MaintenanceConfiguration(); 
            maintenanceConfiguration.Show();
        }
        private void ToolConfigure_click(object sender, RoutedEventArgs e)
        {
            ToolConfigureTab toolConfigure = new ToolConfigureTab();
            toolConfigure.Show();
        }
        private void FixtureConfigure_click(object sender, RoutedEventArgs e)
        {
            FixtureConfigure toolConfigure = new FixtureConfigure();    
            toolConfigure .Show();
        }
        private void ConveyorSeetings_click(object sender, RoutedEventArgs e)
        {
            ConveyorSettings conveyorSettings= new ConveyorSettings();
            conveyorSettings.Show();
        }
        private void logConfigure_click(object sender, RoutedEventArgs e)
        {
            logConfigure logConfigure1= new logConfigure();
            logConfigure1.Show();
        }
        private void ConfigureProductMap_Click(object sender, RoutedEventArgs e)
        {
            ConfigureProductMap configureProductMap = new ConfigureProductMap();
            configureProductMap.Show();
        }
        private void PasswordManager_Click(object sender, RoutedEventArgs e)
        {
            PasswordManager configurePasswordManager = new PasswordManager();
            configurePasswordManager.Show();
        }
        private void NewPatternTeach_click(object sender, RoutedEventArgs e)
        {
            NewPatternTeach newPatternTeach = new NewPatternTeach();
            newPatternTeach.Show();
        }
        private void Rabbit_Click(object sender, RoutedEventArgs e)
        {
            RobotSettings robotSettings = new RobotSettings();
            robotSettings.Show();
        }
        private void Custom_click(object sender, RoutedEventArgs e)
        {
            CustomButtons customButtons = new CustomButtons();
            customButtons.Show();
        }
        private void ConfigureSpeed_click(object sender, RoutedEventArgs e)
        {
            SpeedConfigure speedConfigure = new SpeedConfigure();
            speedConfigure.Show();
        }
        private void btnLive(object sender, RoutedEventArgs e)
        {
            UserView userView = new UserView();
            userView.Show();
        }
        private void btnJogShow_click(object sender, RoutedEventArgs e)
        {
            JogCk jogCk = new JogCk();
            jogCk.Show();
        }
        private void btnJogAndViewShow_click(object sender, RoutedEventArgs e)
        {
            //b = false;
            JogAndView jogAndView = new JogAndView();
            jogAndView.ShowDialog();
            //btnJogandView.IsEnabled = b;
        }
        private void btnHide_click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        private MainWindow main = null;
        public Edit(MainWindow mainwindow) 
        {
            InitializeComponent();
           main = mainwindow;
        }

        private void orderPoint_click(object sender, RoutedEventArgs e)
        {
            if (orderPoint.IsChecked == true)
            {
                orderString.IsChecked = false;
                orderArc.IsChecked = false;
                orderRound.IsChecked = false;
            }
        }

        private void orderString_click(object sender, RoutedEventArgs e)
        {
            if (orderString.IsChecked == true)
            {
                orderPoint.IsChecked = false;
                orderArc.IsChecked = false;
                orderRound.IsChecked = false;
            }
        }

        private void orderArc_click(object sender, RoutedEventArgs e)
        {
            if (orderArc.IsChecked == true)
            {
                orderPoint.IsChecked = false;
                orderString.IsChecked = false;
                orderRound.IsChecked = false;
            }
        }

        private void orderRound_click(object sender, RoutedEventArgs e)
        {
            if (orderRound.IsChecked == true)
            {
                orderPoint.IsChecked = false;
                orderString.IsChecked = false;
                orderArc.IsChecked = false;
            }
        }
        public class DGCommond_list
        {
            public string property { get; set; }
        }

        private void btnArea_Click(object sender, RoutedEventArgs e)
        {
            AreaCoat areaCoat = new AreaCoat();
            areaCoat.Show();
        }


        void item_click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnChage_click(object sender, RoutedEventArgs e)
        {
            ControlPage1 controlPage1 = new ControlPage1();
            pageControl1.Content = new Frame() { Content = controlPage1};
        }

        private void btnChage_click2(object sender, RoutedEventArgs e)
        {
            
            pageControl1.Content = new Frame() { Content = controlPage2 };
        }

        private void Config_Click(object sender, RoutedEventArgs e)
        {
            if (ConfigureAll.IsSelected == true)
            {
                programWizard.Show();
            }
            else if (ConfigureBase.IsSelected == true)
            {

            }
            else if (ConfigureSub.IsSelected == true)
            {
                
            }
        }



        public class XmlInsert
        {
            public void xmlinsert()
            {
                Trajectory trajectory = new Trajectory();
                //trajectory.newTarjectory();
            }
        }


        private void AddPoint_Click(object sender, RoutedEventArgs e)
        {
            UI.BatchEdit batchEdit = new UI.BatchEdit();
            //DataRowView DRV = (DataRowView)DGcommondList.SelectedItem;
            //batchEdit.Sort = DRV.Row[0].ToString();
            //batchEdit.Show();
        }

        #region
        /// <summary>
        /// 执行wpf页面委托
        /// </summary>
        private delegate void UpdateUIDelegate( );

        
        /// <summary>
        /// 绘线方法
        /// </summary>
        public void DrawTrajectoryLine()
        {
            Line line = new Line();
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 1;
            line.X1 = trajectoryLine.StratPoint.PointX;
            line.Y1 = trajectoryLine.StratPoint.PointY;
            line.X2 = trajectoryLine.EndPoint.PointX;
            line.Y2 = trajectoryLine.EndPoint.PointY;
            this.chartCanvas.Children.Add(line);
        }

        public double x = 100;
        public double y = 100;
        public int r = 50;
        Brush PenColor = Brushes.Black;

        /// <summary>
        /// 绘制圆，暂时固定了圆心和半径,起点设置为StratPoint,终点到时候设置为stratpoint-1
        /// </summary>
        public void DrawTrajectoryRound()
        {
            Path x_Arrow = new Path();//x轴箭头

            x_Arrow.Stroke = PenColor;

            PathFigure x_Figure = new PathFigure();
            x_Figure.IsClosed = true;
            x_Figure.StartPoint = new Point(x, y);//路径的起点
            x_Figure.Segments.Add(new ArcSegment(new Point(x, y + r / 2), new Size(2 * r, 2 * r), 1, true, SweepDirection.Clockwise, true));
            PathGeometry x_Geometry = new PathGeometry();
            x_Geometry.Figures.Add(x_Figure);
            x_Arrow.Data = x_Geometry;
            chartCanvas.Children.Add(x_Arrow);
        }

        /// <summary>
        /// 弃用原始的类对印的绘图方法
        /// </summary>
        public void DrawTrajectory()
        {
            Line line = new Line();
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 1;
            line.X1 = trajectoryPar.StartPointX;
            line.Y1 = trajectoryPar.StartPointY;
            line.X2 = trajectoryPar.EndPointX;
            line.Y2 = trajectoryPar.EndPointY;
            this.chartCanvas.Children.Add(line);
        }
        #endregion

        public bool result;
        private void trajectoryBatch_Click(object sender, RoutedEventArgs e)
        {
            
            trajectoryLine.EndPoint.PointW = 11.0;
            trajectoryLine.Lift = true;
            if (XMLPath != null)
            {

                if (orderPoint.IsChecked == false && orderString.IsChecked == false && orderArc.IsChecked == false && orderRound.IsChecked == false)
                {
                    MessageBox.Show("请选择命令后再点击确认");
                }
                else
                {
                    if (orderPoint.IsChecked == true)
                    {
                        trajectoryLine.Type = "Point";
                    }
                    else if (orderString.IsChecked == true)
                    {
                        trajectoryLine.Type = "Line";

                        trajectoryLine.Sort = MaxSort.ToString();
                        result = trajectory.AddTrajectory(trajectoryLine, XMLPath);
                        Thread thread = new Thread(ThreadUpdateUILine);
                        thread.Start();

                    }
                    else if (orderArc.IsChecked == true)
                    {
                        trajectoryLine.Type = "Arc";
                    }
                    else if (orderRound.IsChecked == true)
                    {
                        trajectoryLine.Type = "Round";

                        trajectoryLine.Sort = MaxSort.ToString();
                        result = trajectory.AddTrajectory(trajectoryLine, XMLPath);
                        Thread thread = new Thread(ThreadUpdateUIRound);
                        thread.Start();
                    }
                    

                    void ThreadUpdateUIRound()
                    {
                        UpdateUIDelegate updateUIDelegate = new UpdateUIDelegate(DrawTrajectoryRound);
                        if (result == true)
                        {
                            // 实现事先定义的轨迹类，后面用绘图方法绘制
                            this.Dispatcher.Invoke(updateUIDelegate);
                            createTrajectoryLine(trajectoryLine);
                        }
                    }


                    void ThreadUpdateUILine()
                    {
                        // 将绘图方法将给委托，使用线程线程运行
                        UpdateUIDelegate updateUIDelegate = new UpdateUIDelegate(DrawTrajectoryLine);
                        if (result == true)
                        {
                            // 实现事先定义的轨迹类，后面用绘图方法绘制
                            this.Dispatcher.Invoke(updateUIDelegate);
                            createTrajectoryLine(trajectoryLine);
                        }
                    }
                }
            }
            else
                MessageBox.Show("未指定轨迹文件");
        }


        private void DGcommondList_Selected(object sender, RoutedEventArgs e)
        {
            //DataRowView DRV = (DataRowView)DGcommondList.SelectedItem;
            //int SelectedSort = int.Parse(DRV.Row[0].ToString());
            //trajectoryPar = (trajectory.OpenTarjectory(XMLPath, SelectedSort));
            //OptionsPropertyGrid.SelectedObject = trajectoryPar;
            //OptionsPropertyGrid.Refresh();
        }

        /// <summary>
        /// 利用OpenfileDiaglog控件打开轨迹文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            // 为文件扩展名和默认文件扩展名设置过滤器
            dlg.DefaultExt = ".XML";
            dlg.Filter = "轨迹文件(.XML)|*.XML";
            // 显示窗口
            Nullable<bool> result = dlg.ShowDialog();
            CanvasTrajectory.Children.Clear();

            Thread thread = new Thread(ThreadUpdateUI);
            thread.Start();
            /// <summary>
            /// 画图线程
            /// </summary>
            void ThreadUpdateUI()
            {
                
                if (result == true)
                {
                    // 获取文件名
                    XMLPath = System.IO.Path.GetFullPath(dlg.FileName);
                    //trajectoryPars = trajectory.OpenTarjectory(XMLPath);
                    trajectoryPars = trajectory.OpenTarjectory2(XMLPath);

                    //获取轨迹数量
                    foreach (var item in trajectoryPars)
                    {
                        if ((item.GetType()).ToString() == "Test.TrajectoryLine")
                        {
                            UpdateUIDelegate updateUIDelegate = new UpdateUIDelegate(DrawTrajectoryLine);
                            trajectoryLine = (TrajectoryLine)item;
                            createTrajectoryLine(trajectoryLine);
                            this.Dispatcher.Invoke(updateUIDelegate);
                        }
                        if ((item.GetType()).ToString() == "Test.TrajectoryRound")
                        {
                            UpdateUIDelegate updateUIDelegate = new UpdateUIDelegate(DrawTrajectoryRound);
                            trajectoryRound  = (TrajectoryRound)item;
                            //此处缺少CreateTrajectoryRound的方法，后边补充
                            this.Dispatcher.Invoke(updateUIDelegate);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 利用SavefileDiaglog控件打开轨迹文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileSave(object sender, RoutedEventArgs e)
        {
            saveFile.InitialDirectory = "E:\\desket\\GitDevelop\\MySelf\\EasyCoat-WPF\\XML";
            saveFile.Filter = "轨道文件(*XML)|*.XML";
            
            if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //string strName = saveFile.FileName;
            }
            //LabFileName.Content = saveFile.FileName;
        }
        /// <summary>
        /// 通过传入的xml文件中的Sort最大值，自动创建对应数量的checkboxk控件
        /// </summary>
        /// <param name="x"></param>
        private void createTrajectoryPar(TrajectoryPar trajectoryPar) 
        {
            //加入Dispatcher管理线程工作项队列
            App.Current.Dispatcher.Invoke(() =>
            {
                double height = 20;
                double width = this.CanvasTrajectory.ActualWidth - 2;
                System.Windows.Controls.CheckBox CB = new System.Windows.Controls.CheckBox()
                {
                    Height = height,
                    Width = width
                };

                CanvasTrajectory.Children.Add(CB);
                CB.FontSize = 7;

                string openCoat;
                if (trajectoryPar.Open == true)
                {
                    openCoat = "开";
                }
                else
                    openCoat = "关";
                string CoatLift;
                if (trajectoryPar.Lift == true)
                {
                    CoatLift = "升";
                }
                else
                    CoatLift = "降";
                CB.Content = trajectoryPar.Sort + ": " + trajectoryPar.Type + " 升降:" + CoatLift + " " + " 胶阀:" + openCoat + " 起:" + trajectoryPar.StartPoint + " 终" + trajectoryPar.EndPoint;
                MaxSort = int.Parse(trajectoryPar.Sort) + 1;
            });
        }

        private void createTrajectoryLine(TrajectoryLine trajectoryPar)
        {
            //加入Dispatcher管理线程工作项队列
            App.Current.Dispatcher.Invoke(() =>
            {
                double height = 20;
                double width = this.CanvasTrajectory.ActualWidth - 2;
                System.Windows.Controls.CheckBox CB = new System.Windows.Controls.CheckBox()
                {
                    Height = height,
                    Width = width
                };

                CanvasTrajectory.Children.Add(CB);
                CB.FontSize = 7;

                string openCoat;
                if (trajectoryPar.Open == true)
                {
                    openCoat = "开";
                }
                else
                    openCoat = "关";
                string CoatLift;
                if (trajectoryPar.Lift == true)
                {
                    CoatLift = "升";
                }
                else
                    CoatLift = "降";
                CB.Content = trajectoryPar.Sort + ": " + trajectoryPar.Type + " 升降:" + CoatLift + " " + " 胶阀:" + openCoat + " 起:" + trajectoryPar.StratPoint.PointX.ToString()+" "+trajectoryPar.StratPoint.PointY.ToString() + " 终" + trajectoryPar.EndPoint.PointX.ToString()+" "+trajectoryPar.EndPoint.PointY.ToString() ;
            });
        }
        
        /// <summary>
        /// 滑块的移动，坐标的确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Thumb myThumb = (Thumb)sender;
             nTop = Math.Round(Canvas.GetTop(myThumb) + e.VerticalChange,2);
             nLeft = Math.Round(Canvas.GetLeft(myThumb) + e.HorizontalChange,2);
            //防止Thumb控件被拖出容器。
            if (nTop <= 0)
                nTop = 0;
            if (nTop >= (chartCanvas.Height - myThumb.Height))
                nTop = chartCanvas.Height - myThumb.Height;
            if (nLeft <= 0)
                nLeft = 0;
            if (nLeft >= (chartCanvas.Width - myThumb.Width))
                nLeft = chartCanvas.Width - myThumb.Width;
            Canvas.SetTop(myThumb, nTop); 
            Canvas.SetLeft(myThumb, nLeft);
            tt.Text = "Top:" + nTop.ToString() +"  "+ "Left:" + nLeft.ToString();
        }

        private void Bigger_Click(object sender, RoutedEventArgs e)
        {
            Point point = Mouse.GetPosition(chartCanvas);
            trajectory.ScaleEasingAnimationShow(chartCanvas,point,1,2);
        }

        
        double BiggerB = 1;
        private void ChangeBig_Click(object sender, MouseWheelEventArgs e)
        {
            Point point = Mouse.GetPosition(chartCanvas);
            if (e.Delta > 0)
            {
                if (BiggerB <= 1200.0)
                {
                    trajectory.ScaleEasingAnimationShow(chartCanvas, new Point(0, 0), BiggerB, 0.2);
                    BiggerB = BiggerB + 0.2;
                }
                else return;
            }
            if (e.Delta < 0)
            {
                if ( BiggerB >1.0)
                {
                    trajectory.ScaleEasingAnimationShow2(chartCanvas, new Point(0, 0), BiggerB, 0.2);
                    BiggerB = BiggerB - 0.2;
                }
            }
        }
        double FloatElement = 1;
        private void FloatElement_Click(object sender, RoutedEventArgs e)
        {
           trajectory.FloatElement(chartCanvas, FloatElement);
            FloatElement = FloatElement -  0.01;
        }

        private void FloatElement2_Click(object sender, RoutedEventArgs e)
        {
            trajectory.FloatElement2(chartCanvas, 1);
            FloatElement = FloatElement + 0.01;
        }

        private void btnGetStratPoint_Click(object sender, RoutedEventArgs e)
        {

                trajectoryLine.StratPoint.PointX = nLeft;
                trajectoryLine.StratPoint.PointY = nTop;
            
        }

        private void btnGetEndPoint_Click(object sender, RoutedEventArgs e)
        {
            if (XMLPath != null)
            {
                trajectoryLine.EndPoint.PointX = nLeft;
                trajectoryLine.EndPoint.PointY = nTop;
            }
        }

        private void Small_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
