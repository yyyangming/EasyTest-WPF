using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using Color = System.Windows.Media.Color;
using MessageBox = System.Windows.MessageBox;
using Path = System.Windows.Shapes.Path;
using Point = System.Windows.Point;
using System.ComponentModel;

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
        Trajectory trajectory = new Trajectory();
        TrajectoryLine trajectoryLine = new TrajectoryLine();
        TrajectoryRound trajectoryRound = new TrajectoryRound();
        TrajectoryArc trajectoryArc = new TrajectoryArc();
        Point pointStrat,pointMid,pointEnd;
        CircleData circleData = new CircleData();

        public string XMLPath;//文件路径
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
            PathFigure x_axisFigure = new PathFigure
            {
                IsClosed = true,
                StartPoint = new Point(500, 16) // 路径的起点
            };
            x_axisFigure.Segments.Add(new LineSegment(new Point(500, 24), false)); //第2个点
            x_axisFigure.Segments.Add(new LineSegment(new Point(510, 20), false)); //第3个点
            PathFigure y_axisFigure = new PathFigure
            {
                IsClosed = true,
                StartPoint = new Point(16, 330)                          //路径的起点
            };
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
                Line x_scale = new Line
                {
                    StrokeEndLineCap = PenLineCap.Triangle,
                    StrokeThickness = 1,
                    Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                    X1 = 20 + i * 45
                }; //主x轴标尺
                x_scale.X2 = x_scale.X1;
                x_scale.Y1 = 30;
                x_scale.StrokeThickness = 2;
                x_scale.Y2 = x_scale.Y1 - 10;
                this.chartCanvas.Children.Add(x_scale);
                Line x_in = new Line
                {
                    Stroke = System.Windows.Media.Brushes.LightGray,
                    StrokeThickness = 0.5,
                    X1 = 20 + i * 45,
                    Y1 = 310,
                    X2 = 20 + i * 45,
                    Y2 = 30
                };//x轴轴辅助标尺
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


        /// <summary>
        /// 在Edit页面构建时在canvas构建坐标轴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        #region  所有子页面的实例化和显示
        private void EditClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Config.Check_Time = (bool)zhengxian.IsChecked;
            config.WriteFile();

            e.Cancel = true;
            this.Hide();
            this.Owner.Visibility = Visibility.Visible;//显示父窗体
        }

        private void btnChage_click(object sender, RoutedEventArgs e)
        {
            ControlPage1 controlPage1 = new ControlPage1();
            pageControl1.Content = new Frame() { Content = controlPage1 };
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

        private void btnArea_Click(object sender, RoutedEventArgs e)
        {
            AreaCoat areaCoat = new AreaCoat();
            areaCoat.Show();
        }

        #endregion

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

        #region 选择轨迹命令，点线弧圆
        private void orderPoint_click(object sender, RoutedEventArgs e)
        {
            if (orderPoint.IsChecked == true)
            {
                orderString.IsChecked = false;
                orderArc.IsChecked = false;
                orderRound.IsChecked = false;

                StratTrue = null;
                EndTrue = null;
            }
        }

        private void orderString_click(object sender, RoutedEventArgs e)
        {
            if (orderString.IsChecked == true)
            {
                orderPoint.IsChecked = false;
                orderArc.IsChecked = false;
                orderRound.IsChecked = false;

                StratTrue = null;
                EndTrue = null;
            }
        }

        private void orderArc_click(object sender, RoutedEventArgs e)
        {
            if (orderArc.IsChecked == true)
            {
                orderPoint.IsChecked = false;
                orderString.IsChecked = false;
                orderRound.IsChecked = false;

                StratTrue = null;
                EndTrue = null;
            }
        }

        private void orderRound_click(object sender, RoutedEventArgs e)
        {
            if (orderRound.IsChecked == true)
            {
                orderPoint.IsChecked = false;
                orderString.IsChecked = false;
                orderArc.IsChecked = false;

                StratTrue = null;
                EndTrue = null;
            }
        }
        #endregion

        public class DGCommond_list
        {
            public string property { get; set; }
        }

        


        void item_click(object sender, RoutedEventArgs e)
        {
            
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
            Line line = new Line
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                X1 = trajectoryLine.StratPoint.PointX,
                Y1 = trajectoryLine.StratPoint.PointY,
                X2 = trajectoryLine.EndPoint.PointX,
                Y2 = trajectoryLine.EndPoint.PointY
            };
            CanvasDraw.Children.Add(line);
        }

        Brush PenColor = Brushes.Black;


        /// <summary>
        /// 圆的结构体，point为圆心，radius为半径
        /// </summary>
        public struct CircleData
        {
            public Point center;
            public int radius;
            public Point StratPoint;
        }

        /// <summary>
        ///  输入三个点，返回经过三个圆的结构体
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="pt3"></param>
        /// <returns></returns>
        public void findCircle1(Point pt1, Point pt2, Point pt3,bool A)
        {
            //定义两个点，分别表示两个中点
            Point midpt1 = new Point(); Point midpt2 = new Point();
            //求出点1和点2的中点
            midpt1.X = (pt2.X + pt1.X) / 2;
            midpt1.Y = (pt2.Y + pt1.Y) / 2;
            //求出点3和点1的中点
            midpt2.X = (pt3.X + pt1.X) / 2;
            midpt2.Y = (pt3.Y + pt1.Y) / 2;
            //求出分别与直线pt1pt2，pt1pt3垂直的直线的斜率
            double k1 = -(pt2.X - pt1.X) / (pt2.Y - pt1.Y);
            double k2 = -(pt3.X - pt1.X) / (pt3.Y - pt1.Y);
            //然后求出过中点midpt1，斜率为k1的直线方程（既pt1pt2的中垂线）：y - midPt1.y = k1( x - midPt1.x)
            //以及过中点midpt2，斜率为k2的直线方程（既pt1pt3的中垂线）：y - midPt2.y = k2( x - midPt2.x)
            //定义一个圆的数据的结构体对象CD
            CircleData CD = new CircleData();
            //连立两条中垂线方程求解交点得到：
            CD.center.X = (midpt2.Y - midpt1.Y - k2 * midpt2.X + k1 * midpt1.X) / (k1 - k2);
            CD.center.Y = midpt1.Y + k1 * (midpt2.Y - midpt1.Y - k2 * midpt2.X + k2 * midpt1.X) / (k1 - k2);
            //用圆心和其中一个点求距离得到半径：
            CD.radius = (int)(Math.Sqrt((CD.center.X - pt1.X) * (CD.center.X - pt1.X) + (CD.center.Y - pt1.Y) * (CD.center.Y - pt1.Y)));
            if (A == true)
            {
                CD.StratPoint.X = CD.center.X - CD.radius;
                CD.StratPoint.Y = CD.center.Y;
            }
            else if (A == false)
            {
                CD.StratPoint.X = trajectoryArc.StratPoint.PointX;
                CD.StratPoint.Y = trajectoryArc.StratPoint.PointY;

                #region 判断角度，存在问题
                //const double M_PI = 3.1415926535897;
                //double ma_x = CD.StratPoint.X - CD.center.X;
                //double ma_y = CD.StratPoint.Y - CD.center.Y;
                //double mb_x = trajectoryArc.EndPoint.PointX - CD.center.X;
                //double mb_y = trajectoryArc.EndPoint.PointY - CD.center.Y;
                //double v1 = (ma_x * mb_x) + (ma_y * mb_y);
                //double ma_val = Math.Sqrt(ma_x * ma_x + ma_y * ma_y);
                //double mb_val = Math.Sqrt(mb_x * mb_x + mb_y * mb_y);
                //double cosM = v1 / (ma_val * mb_val);
                //double angleAMB = Math.Acos(cosM) * 180 / M_PI;
                //if (angleAMB<180)
                //    trajectoryArc.Superior = true;
                //else
                //    trajectoryArc.Superior = false;
                #endregion


                //判断优劣弧，(OA.X-OB.X)*(OA.Y-OB.Y)是否大于0，大于则是劣弧
                if ((CD.StratPoint.X-CD.center.X)*(trajectoryArc.EndPoint.PointX-CD.center.X)+(CD.StratPoint.Y-CD.center.Y)*(trajectoryArc.EndPoint.PointY-CD.center.Y)>0)
                {
                    trajectoryArc.Superior = false;
                }
                else if ((CD.StratPoint.X - CD.center.X) * (trajectoryArc.EndPoint.PointX - CD.center.X) + (CD.StratPoint.Y - CD.center.Y) * (trajectoryArc.EndPoint.PointY - CD.center.Y) <= 0)
                {
                    trajectoryArc.Superior = true;
                }


                if (trajectoryArc.ForWardRatation == true)
                {
                    if (CD.StratPoint.X >= CD.center.X && CD.StratPoint.Y >= CD.center.Y)
                    {
                        if (trajectoryArc.EndPoint.PointX>CD.StratPoint.X)
                        {
                            trajectoryArc.Superior = true;
                        }
                        else if (true)
                        {

                        }
                    }
                }

                
            }
            circleData = CD;
        }


        /// <summary>
        /// 绘制圆，首先绘制上半圆，再绘制下半圆，两个半圆不闭合，就完成了整个圆的绘制
        /// </summary>
        public void DrawTrajectoryRound()
        {
            //绘制上半圆
            Path x_Arrow = new Path();
            x_Arrow.Stroke = PenColor;
            PathFigure x_Figure = new PathFigure();
            x_Figure.IsClosed = false;
            x_Figure.StartPoint = circleData.StratPoint;//路径的起点
            Point RoundEndPoint = new Point(circleData.StratPoint.X+circleData.radius*2,circleData.StratPoint.Y);
            x_Figure.Segments.Add(new ArcSegment(RoundEndPoint, new Size(circleData.radius, circleData.radius), 0, true, SweepDirection.Clockwise, true));
            PathGeometry x_Geometry = new PathGeometry();
            x_Geometry.Figures.Add(x_Figure);
            x_Arrow.Data = x_Geometry;
            CanvasDraw.Children.Add(x_Arrow);

            //绘制下半圆
            Path Y_Arrow = new Path();
            Y_Arrow.Stroke = PenColor;
            PathFigure Y_Figure = new PathFigure();
            Y_Figure.IsClosed = false;
            Y_Figure.StartPoint = circleData.StratPoint;//路径的起点
            Point RoundEndPoint2 = new Point(circleData.StratPoint.X + circleData.radius * 2, circleData.StratPoint.Y);
            Y_Figure.Segments.Add(new ArcSegment(RoundEndPoint2, new Size(circleData.radius, circleData.radius), 0, true, SweepDirection.Counterclockwise, true));
            PathGeometry Y_Geometry = new PathGeometry();
            Y_Geometry.Figures.Add(Y_Figure);
            Y_Arrow.Data = Y_Geometry;
            CanvasDraw.Children.Add(Y_Arrow);
        }

        /// <summary>
        /// 绘制弧
        /// </summary>
        public void DrawTrajectoryArc()
        {
            Path x_Arrow = new Path();
            x_Arrow.Stroke = PenColor;
            PathFigure x_Figure = new PathFigure();
            x_Figure.IsClosed = false;
            x_Figure.StartPoint = circleData.StratPoint;//路径的起点
            Point RoundEndPoint = new Point(trajectoryArc.EndPoint.PointX, trajectoryArc.EndPoint.PointY);
            if (trajectoryArc.ForWardRatation == true)
            {
                x_Figure.Segments.Add(new ArcSegment(RoundEndPoint, new Size(circleData.radius, circleData.radius), 0, trajectoryArc.Superior, SweepDirection.Clockwise, true));
            }
            else
                x_Figure.Segments.Add(new ArcSegment(RoundEndPoint, new Size(circleData.radius, circleData.radius), 0, trajectoryArc.Superior, SweepDirection.Counterclockwise, true));
            PathGeometry x_Geometry = new PathGeometry();
            x_Geometry.Figures.Add(x_Figure);
            x_Arrow.Data = x_Geometry;
            CanvasDraw.Children.Add(x_Arrow);
         }
        #endregion

        public bool result;
        /// <summary>
        /// 在文件中添加轨迹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trajectoryBatch_Click(object sender, RoutedEventArgs e)
        {
            // 设置默认值
            trajectoryLine.EndPoint.PointW = 20.0;
            trajectoryRound.EndPoint.PointW = 20.0;
            trajectoryArc.EndPoint.PointW = 20.0;
            trajectoryLine.Lift = false;


            if (XMLPath != null)
            {
                if (StratTrue != null && EndTrue != null)
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
                            result = trajectory.AddTrajectory(trajectoryLine);
                            trajectoryPars.Add((object)trajectoryLine);
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
                            result = trajectory.AddTrajectory(trajectoryLine);
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
                                createTrajectory(trajectoryLine);
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
                                createTrajectory(trajectoryLine);
                            }
                        }
                    }
                }
                else
                    MessageBox.Show("未指定开始点或结束点，请重新设置");
            }
            else
                MessageBox.Show("未指定轨迹文件");
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
            CanvasDraw.Children.Clear();
            
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
                    trajectory.trajectoryLoaded(XMLPath);
                    trajectoryPars = trajectory.OpenTarjectory2();
                    MaxSort = trajectoryPars.Count+1;
                    int i = 1;
                    //获取轨迹数量
                    foreach (var item in trajectoryPars)
                    {
                        if (item.GetType().ToString() == "Test.TrajectoryLine")
                        {
                            UpdateUIDelegate updateUIDelegate = new UpdateUIDelegate(DrawTrajectoryLine);
                            trajectoryLine = (TrajectoryLine)item;
                            createTrajectory(trajectoryLine);
                            this.Dispatcher.Invoke(updateUIDelegate);
                        }
                        if (item.GetType().ToString() == "Test.TrajectoryRound")
                        {
                            UpdateUIDelegate updateUIDelegate = new UpdateUIDelegate(DrawTrajectoryRound);
                            trajectoryRound  = (TrajectoryRound)item;
                            pointStrat.X = trajectoryRound.StratPoint.PointX;
                            pointStrat.Y = trajectoryRound.StratPoint.PointY;
                            pointMid.X = trajectoryRound.MidPoint.PointX;
                            pointMid.Y = trajectoryRound.MidPoint.PointY;
                            pointEnd.X = trajectoryRound.EndPoint.PointX;
                            pointEnd.Y = trajectoryRound.EndPoint.PointY;
                            findCircle1(pointMid,pointStrat,pointEnd,true);
                            createTrajectory(trajectoryRound); 
                            this.Dispatcher.Invoke(updateUIDelegate);
                        }
                        if (item.GetType().ToString() == "Test.TrajectoryArc")
                        {
                            UpdateUIDelegate updateUIDelegate = new UpdateUIDelegate(DrawTrajectoryArc);
                            trajectoryArc = (TrajectoryArc)item;
                            pointStrat.X = trajectoryArc.StratPoint.PointX;
                            pointStrat.Y = trajectoryArc.StratPoint.PointY;
                            pointMid.X = trajectoryArc.MidPoint.PointX;
                            pointMid.Y = trajectoryArc.MidPoint.PointY;
                            pointEnd.X = trajectoryArc.EndPoint.PointX;
                            pointEnd.Y = trajectoryArc.EndPoint.PointY;
                            findCircle1(pointMid, pointStrat, pointEnd,false);
                            createTrajectory(trajectoryArc);
                            this.Dispatcher.Invoke(updateUIDelegate);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///  ！！！文件另存为，利用SavefileDiaglog控件打开轨迹文件，还未完成，需要修改
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
        /// 保存XML文件到打开XML文件的文件中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            trajectory.ReviseTrajectory2(trajectoryPars);
            bool? result = trajectory.trajectorySave();
        }

        /// <summary>
        /// 属性框确认按钮的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Porperty_Click(object sender, RoutedEventArgs e)
        {
            if (OptionsPropertyGrid.SelectedObject != null)
            {
                if (OptionsPropertyGrid.SelectedObject.GetType().ToString() == "Test.TrajectoryLine")
                {
                    trajectoryLine = (TrajectoryLine)OptionsPropertyGrid.SelectedObject;

                    for (int i = 0; i < trajectoryPars.Count; i++)
                    {
                        if (trajectoryPars[i].GetType().ToString() == "Test.TrajectoryLine")
                        {
                            TrajectoryLine trajectory = (TrajectoryLine)trajectoryPars[i];
                            if (trajectory.Sort == trajectoryLine.Sort)
                            {
                                trajectoryPars[i] = trajectoryLine;
                                break;
                            }
                        }
                    }
                }
                if (OptionsPropertyGrid.SelectedObject.GetType().ToString() == "Test.TrajectoryRound")
                {
                    trajectoryRound = (TrajectoryRound)OptionsPropertyGrid.SelectedObject;
                    for (int i = 0; i < trajectoryPars.Count; i++)
                    {
                        if (trajectoryPars[i].GetType().ToString() == "Test.TrajectoryRound")
                        {
                            TrajectoryRound trajectory = (TrajectoryRound)trajectoryPars[i];
                            if (trajectory.Sort == trajectoryRound.Sort)
                            {
                                trajectoryPars[i] = trajectoryRound;
                                break;
                            }
                        }
                    }
                }

                if (OptionsPropertyGrid.SelectedObject.GetType().ToString() == "Test.TrajectoryArc")
                {
                    trajectoryRound = (TrajectoryRound)OptionsPropertyGrid.SelectedObject;
                    for (int i = 0; i < trajectoryPars.Count; i++)
                    {
                        if (trajectoryPars[i].GetType().ToString() == "Test.TrajectoryArc")
                        {
                            TrajectoryRound trajectory = (TrajectoryArc)trajectoryPars[i];
                            if (trajectory.Sort == trajectoryRound.Sort)
                            {
                                trajectoryPars[i] = trajectoryRound;
                                break;
                            }
                        }
                    }
                }
            }
        }
            

            


        #region 创建CheckBox，对应创建的轨迹文件和编的轨迹的数据
        public double CheckBoxheight = 20.000;
        private void createTrajectory(TrajectoryLine trajectoryPar)
        {
            //加入Dispatcher管理线程工作项队列
            App.Current.Dispatcher.Invoke(() =>
            {
                double height = CheckBoxheight;
                CheckBoxheight = CheckBoxheight + 0.001;

                double width = this.CanvasTrajectory.ActualWidth - 2;
                System.Windows.Controls.CheckBox CB = new System.Windows.Controls.CheckBox()
                {
                    Height = height,
                    Width = width
                };

                CanvasTrajectory.Children.Add(CB);
                CB.FontSize = 7;
                //CB.Name = trajectoryPar.Sort;

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
                CB.Content = trajectoryPar.Sort + ": " + " 线 " + " 升降:" + CoatLift + " " + " 胶阀:" + openCoat + " 起:" + trajectoryPar.StratPoint.PointX.ToString()+" "+trajectoryPar.StratPoint.PointY.ToString() + " 终" + trajectoryPar.EndPoint.PointX.ToString()+" "+trajectoryPar.EndPoint.PointY.ToString() ;
                CB.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(CheckBox_PreviewMouseLeftButtonDown);
                CB.ToolTip = CB.Content;
            });
        }
        private void createTrajectory(TrajectoryRound trajectoryPar)
        {
            //加入Dispatcher管理线程工作项队列
            App.Current.Dispatcher.Invoke(() =>
            {
                double height = CheckBoxheight;
                CheckBoxheight = CheckBoxheight + 0.001;

                double width = this.CanvasTrajectory.ActualWidth - 2;
                System.Windows.Controls.CheckBox CB = new System.Windows.Controls.CheckBox()
                {
                    Height = height,
                    Width = width
                };

                CanvasTrajectory.Children.Add(CB);
                CB.FontSize = 7;
                //CB.Name = trajectoryPar.Sort;

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
                CB.Content = trajectoryPar.Sort + ": " + " 圆 " + " 升降:" + CoatLift + " " + " 胶阀:" + openCoat + " 起:" + trajectoryPar.StratPoint.PointX.ToString() + " " + trajectoryPar.StratPoint.PointY.ToString() + "顺时针："+trajectoryRound.ForWardRatation.ToString();
                CB.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(CheckBox_PreviewMouseLeftButtonDown);
                CB.ToolTip = CB.Content;
                // CB.PreviewMouseRightButtonDown += new MouseButtonEventHandler(此处添加右击事件方法);
                // 需要在这个地方添加一个右击事件，右击添加，包含序号和一个选择的命令类型。
            });
        }
        private void createTrajectory(TrajectoryArc trajectoryPar)
        {
            //加入Dispatcher管理线程工作项队列
            App.Current.Dispatcher.Invoke(() =>
            {
                double height = CheckBoxheight;
                CheckBoxheight = CheckBoxheight + 0.001;

                double width = this.CanvasTrajectory.ActualWidth - 2;
                System.Windows.Controls.CheckBox CB = new System.Windows.Controls.CheckBox()
                {
                    Height = height,
                    Width = width
                };

                
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
                CB.Content = trajectoryPar.Sort + ": " + " 弧 " + " 升降:" + CoatLift + " " + " 胶阀:" + openCoat + " 起:" + trajectoryPar.StratPoint.PointX.ToString() + " " + trajectoryPar.StratPoint.PointY.ToString() + "顺时针：" + trajectoryRound.ForWardRatation.ToString();
                CB.ToolTip = CB.Content;
                CB.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(CheckBox_PreviewMouseLeftButtonDown);
                CanvasTrajectory.Children.Add(CB);
            });
        }
        #endregion

        /// <summary>
        /// 将PorpertyGrid数据源随着点击对应的CheckBox而切换数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CheckBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var d = sender as System.Windows.Controls.CheckBox;
            int sort = (int)((d.Height - 20)*1000);
            if (trajectoryPars[sort].GetType().ToString() == "Test.TrajectoryArc")
            {
                OptionsPropertyGrid.SelectedObject = (TrajectoryArc)trajectoryPars[sort];
            }
            if (trajectoryPars[sort].GetType().ToString() == "Test.TrajectoryLine")
            {
                OptionsPropertyGrid.SelectedObject = (TrajectoryLine)trajectoryPars[sort];
            }
            if (trajectoryPars[sort].GetType().ToString() == "Test.TrajectoryRound")
            {
                OptionsPropertyGrid.SelectedObject = (TrajectoryRound)trajectoryPars[sort];
            }
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

        #region 轨迹页面放大缩小的方法和对应的参数
        /// <summary>
        /// 轨迹页面初始大小为1
        /// </summary>
        double BiggerB = 1;
        /// <summary>
        /// 对应轨迹页面的缩小和放大
        /// </summary>
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
        #endregion


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


        bool? StratTrue = null;

        bool? EndTrue = null;

        #region 获取开始点和结束点的事件
        /// <summary>
        /// 获取开始点的坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetStratPoint_Click(object sender, RoutedEventArgs e)
        {
            if (XMLPath != null && (orderPoint.IsChecked == true || orderString.IsChecked == true || orderArc.IsChecked == true || orderRound.IsChecked == true))
            {
                if (orderString.IsChecked == true)
                {
                    trajectoryLine.StratPoint.PointX = nLeft;
                    trajectoryLine.StratPoint.PointY = nTop;
                    StratTrue = true;
                }
                else if (orderRound.IsChecked == true)
                {
                    trajectoryRound.MidPoint.PointX = nLeft;
                    trajectoryRound.MidPoint.PointY = nTop;
                    StratTrue = true;
                }
            }
            else
                MessageBox.Show("请选择命令类型或指定文件");
        }

        

        /// <summary>
        /// 当属性值发生改变时执行的方法
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void OptionsPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// 获取街书店的坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetEndPoint_Click(object sender, RoutedEventArgs e)
        {
            if (XMLPath != null && (orderPoint.IsChecked == true || orderString.IsChecked == true || orderArc.IsChecked == true || orderRound.IsChecked == true))
            {
                if (orderString.IsChecked == true)
                {
                    trajectoryLine.EndPoint.PointX = nLeft;
                    trajectoryLine.EndPoint.PointY = nTop;
                    EndTrue = true;
                }
                else if (orderRound.IsChecked == true)
                {
                    trajectoryRound.EndPoint.PointX = nLeft;
                    trajectoryRound.EndPoint.PointY = nTop;
                    EndTrue = true;
                }
            }
            else
                MessageBox.Show("请选择命令类型或指定文件");
        }
        #endregion

        /// <summary>
        /// 键盘操作移动滑块
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CoatKeyMove_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                Canvas.SetLeft(key, nLeft -= 1);
            }
            if (e.Key == Key.Right)
            {
                Canvas.SetLeft(key, nLeft += 1);
            }
            if (e.Key == Key.Up)
            {
                Canvas.SetTop(key, nTop -= 1);
            }
            if (e.Key == Key.Down)
            {
                Canvas.SetTop(key, nTop += 1);
            }


            //不应该是增加距离，而是应该缩小移动距离，缩小响应时间
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == Key.Left)
            {
                Canvas.SetLeft(key, nLeft -= 10);
            }
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == Key.Right)
            {
                Canvas.SetLeft(key, nLeft += 10);
            }
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == Key.Up)
            {
                Canvas.SetLeft(key, nTop -= 10);
            }
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == Key.Down)
            {
                Canvas.SetLeft(key, nTop += 10);
            }
        }
    }
}
