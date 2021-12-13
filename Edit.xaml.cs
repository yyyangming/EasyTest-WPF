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

namespace Test
{
    /// <summary>
    /// 编辑页面.xaml 的交互逻辑
    /// </summary>
    public partial class Edit : Window
    {
        int MaxSort;

        /// <summary>
        /// 实例化所有子窗口
        /// </summary>
        NewFileBaseConfigure sub = new NewFileBaseConfigure();
        ProgramWizard programWizard = new ProgramWizard();
        ControlPage2 controlPage2 = new ControlPage2();


        Config config = new Config();
        TrajectoryPar propertyGrid = new TrajectoryPar();
        SaveFileDialog saveFile = new SaveFileDialog();
        // 创建打开文件夹
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

        static String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456;database=easycoat;";
        string strsql = null;
        MySqlConnection conn = new MySqlConnection(connetStr);
        
        ObservableCollection<TrajectoryPar> trajectoryPars = new ObservableCollection<TrajectoryPar>();

        /// <summary>
        /// 实例化所需要用的类
        /// </summary>
        TrajectoryPar trajectoryPar = new TrajectoryPar();
        Trajectory trajectory = new Trajectory();
        Line line1 = new Line();

        //string XMLPath = "XMLPath";
        string XMLPath;
        public int Checksort = 0;



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
            
            programWizard.Show();
        }


        private void btnOpenFile(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog openFile = new FolderBrowserDialog();
            openFile.ShowDialog();
        }



        private void Window_Initialized(object sender, EventArgs e)
        {

            DrawArrow();
        }

        public void Edit_loaded(object sender, RoutedEventArgs e)
        {
            //配置文件赋值
            config.ReadFile();
            zhengxian.IsChecked = Config.Check_Time;

            
            Trajectory proPertytrajectory = new Trajectory();
            string xmlPathProperty = "E:\\desket\\GitDevelop\\MySelf\\EasyCoat-WPF\\XML\\Test.xml";
            propertyGrid = proPertytrajectory.OpenTarjectory(xmlPathProperty, 1);
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
            #endregion
            //CDbMysql cDbMysql = new CDbMysql(connetStr);

            //if (cDbMysql.db_header.State == ConnectionState.Closed)
            //{
            //    cuowu.Content = "连接有问题";
            //}
            //else
            //    cuowu.Content = "连接正常";
            //strsql = "SELECT * FROM trajectory";
            ////DGcommondList.ItemsSource = cDbMysql.GetDataTable("strsql").DefaultView;
        }



        private void EditClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Config.Check_Time = (bool)zhengxian.IsChecked;
            config.WriteFile();

            //CDbMysql cDbMysql = new CDbMysql("127.0.0.1", "root", "123456", "easycoat", "3006");
            //cDbMysql.db_header.Close();

            e.Cancel = true;
            //main.menuedit.IsEnabled = true;
            this.Hide();
            this.Owner.Visibility = Visibility.Visible;//显示父窗体
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
            List <ClassConfigInfo> list = new List<ClassConfigInfo>();
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
            return list ;
        }
        #endregion

        

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

        public class ClassRouteInfo 
        {
            //int
        }


        //在选中配置的时候提示选择了**配置
        //private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    System.Windows.MessageBox.Show((ccbConfig.SelectedItem as ClassConfigInfo).ClassName);
        //}

        //private void Mltool(object sender, RoutedEventArgs e)
        //{
        //    Jog jog = new Jog();
        //    jog.Show();
        //}




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

        //private void EditClosed(object sender, EventArgs e)
        //{

        //    this.Hide();
        //}

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

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
                trajectory.newTarjectory();
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
        /// 绘图方法
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
        private void ShowLines()
        {
            Line line = new Line();
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 1;
            line.X1 = 100;
            line.Y1 = 100;
            line.X2 = 300;
            line.Y2 = 300;
            this.chartCanvas.Children.Add(line);
        }
        #endregion

        
        private void trajectoryBatch_Click(object sender, RoutedEventArgs e)
        {
            
        }
        public void Run()
        {
            DrawScale();
            DrawArrow();
            Trajectory3D trajectory3D1 = new Trajectory3D();
            Trajectory3D trajectory3D2 = new Trajectory3D();
            trajectory3D1.PointX = propertyGrid.StartPointX;
            trajectory3D1.PointY = propertyGrid.StartPointY;
            trajectory3D2.PointX = propertyGrid.EndPointX;
            trajectory3D2.PointY = propertyGrid.EndPointY;
            line1 = trajectory.trajectoryLine(trajectory3D1, trajectory3D2);
            this.chartCanvas.Children.Add(line1);
            Thread.Sleep(10);
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
            MaxSort = 0;
            // 显示窗口
            Nullable<bool> result = dlg.ShowDialog();
            CanvasTrajectory.Children.Clear();
            /// <summary>
            /// 画图线程
            /// </summary>
            void ThreadUpdateUI()
            {
                UpdateUIDelegate updateUIDelegate = new UpdateUIDelegate(DrawTrajectory);
                if (result == true)
                {
                    // 获取文件名
                    XMLPath = System.IO.Path.GetFullPath(dlg.FileName);
                    trajectoryPars = trajectory.OpenTarjectory(XMLPath);
                    
                    //获取轨迹数量
                    foreach (TrajectoryPar item in trajectoryPars)
                    {
                        trajectoryPar = item;
                        createTrajectoryPar(item);
                        this.Dispatcher.Invoke(updateUIDelegate);
                    }
                }
            }
            Thread thread = new Thread(ThreadUpdateUI);
            thread.Start();
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
            });
        }

        

    }
}
