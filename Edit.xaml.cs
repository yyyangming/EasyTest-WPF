using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using System.Collections.ObjectModel;
using ContextMenu = System.Windows.Controls.ContextMenu;
using MenuItem = System.Windows.Controls.MenuItem;

namespace Test
{
    /// <summary>
    /// 编辑页面.xaml 的交互逻辑
    /// </summary>
    public partial class Edit : Window
    {
        ObservableCollection<propertyValue> propertyValueList = new ObservableCollection<propertyValue>();
        ObservableCollection<DGCommond_list> dGCommond_Lists = new ObservableCollection<DGCommond_list>();
        public Edit()
        {
            
            InitializeComponent();

        }



        private void btnNewFile(object sender, RoutedEventArgs e)
        {
            
            ProgramWizard programWizard = new ProgramWizard();
            programWizard.Show();
            //使用TextBox和button完成新建文件
            //// Create OpenFileDialog 
            //Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();           

            //// Set filter for file extension and default file extension 
            //dlg.DefaultExt = ".txt"; 
            //dlg.Filter = "Text documents (.txt)|*.txt"; 

            //// Display OpenFileDialog by calling ShowDialog method 
            //Nullable<bool> result = dlg.ShowDialog(); 

            //// Get the selected file name and display in a TextBox 
            //if (result == true) 
            //{ 
            //    // Open document 
            //    string filename = dlg.FileName; 
            //    FileNameTextBox.Text = filename; 
            //}


            //OpenFileDialog fileDialog = new OpenFileDialog();
            //fileDialog.Filter = "所有文件.*|*.*";
            //fileDialog.InitialDirectory = "E:\\";
            //fileDialog.Multiselect = true;
            //fileDialog.ShowDialog();


            //if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    string[] filenames = fileDialog.FileNames;
            //    foreach (string str in fileDialog.FileNames)
            //    {
            //// 去除重复的
            //if (!lsb_Files.Items.Contains(str))
            //{
            //    lsb_Files.Items.Add(str);
            //}
            //}
            //}
        }



        private void btnFileSave(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.ShowDialog();
            saveFile.Filter = "轨道文件(*GD)|*.GD";
            string strName = saveFile.FileName;
            saveFile.InitialDirectory = "E:\\";
            //LabFileName.Content = saveFile.FileName;
        }



        private void btnOpenFile(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog openFile = new FolderBrowserDialog();
            openFile.ShowDialog();
        }



        private void Window_Initialized(object sender, EventArgs e)
        {
            //首先要是知道，获取数据源的方法是comboBox.ItemsSource()
            //要知道在这个地方的数据类型是不一样的，是IEnumerable,list的类型是这个类型，可以直接用
            //comboBox1.ItemsSource;
            //获取数据源

            List<ClassConfigInfo> listConfig = GetConfigItemInfos();
            #region 1.指定数据源
            //指定数据源
            //ccbConfig.ItemsSource = listConfig;
            //DATA_GRID.ItemsSource = propertyValueList;
            //如果使用指定数据源，不能直接移除，动态的添加移除//
            //如果需要动态添加，利用循环进行添加，把数组里面的全部添加进去
            #endregion
            #region 2.绑定DataContent
            //或者使用DataContent属性绑定数据源
            //不指指定数据源，绑定数据源
            //ccbConfig.DataContext = listConfig;
            //接着在wpf中comboBox中添加绑定ItemSource = "{Banding}"
            #endregion
            //本来应该是使用数据库，这边为了简化，直接指定   

            //ccbConfig.SelectedValuePath = "ClassId";//项的值对应的属性名
            //ccbConfig.DisplayMemberPath = "ClassName";//项的显示值对应的属性值
            
            

            //List<ClassRunMode> listRouteMode = GetRoutModeInfos();
            //cbbRunMode.SelectedValuePath = "ClassId";
            //cbbRunMode.DisplayMemberPath = "ClassName";
            //cbbRunMode.ItemsSource = listRouteMode;
            //comboBox1.DataContext = listRouteMode;
            
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


        public class propertyValue
        {
            public string property { get; set; }
            public string value { get; set; }
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            propertyValueList.Add(new propertyValue() { 
                property = "接近时高度",
                value="9.00"
            });

            propertyValueList.Add(new propertyValue()
            {
                property = "->（1）开始时X（毫米）",
                value = "-42.25"
            });
            propertyValueList.Add(new propertyValue()
            {
                property = "->（1）开始时Y（毫米）",
                value = "9.25"
            });
            propertyValueList.Add(new propertyValue()
            {
                property = "离开时抬起高度（毫米）",
                value = "9.00"
            });
            propertyValueList.Add(new propertyValue()
            {
                property = "点胶时高度（毫米）",
                value = "6.00"
            });
            propertyValueList.Add(new propertyValue()
            {
                property = "点胶时速度（毫米/毫米）",
                value = "9.25"
            });
            propertyValueList.Add(new propertyValue()
            {
                property = "（2）重点X轴坐标（毫米）",
                value = "9.25"
            });
            propertyValueList.Add(new propertyValue()
            {
                property = "（2）终点Y轴坐标（毫米）",
                value = "9.25"
            });
            propertyValueList.Add(new propertyValue()
            {
                property = "忽略保护区域（毫米）",
                value = "错误"
            });
            propertyValueList.Add(new propertyValue()
            {
                property = "胶线重叠（毫米）",
                value = "0.00"
            });
            propertyValueList.Add(new propertyValue()
            {
                property = "->（1）旋转（角度）",
                value = "0.00"
            });
            propertyValueList.Add(new propertyValue()
            {
                property = "开始距离（毫米）",
                value = "0.00"
            });
            propertyValueList.Add(new propertyValue()
            {
                property = "停止-距离（毫米）",
                value = "0.00"
            });
            propertyValueList.Add(new propertyValue()
            {
                property = "->（1）倾斜角度（毫米）",
                value = "0.00"
            });
            propertyValueList.Add(new propertyValue()
            {
                property = "胶阀配置",
                value = "*****"
            });
            propertyValueList.Add(new propertyValue()
            {
                property = "X-方向（真）",
                value = "9.25"
            });
            ((this.FindName("DATA_GRID")) as System.Windows.Controls.DataGrid).ItemsSource = propertyValueList;
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

        private void Edit_loaded(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            if ( main.OperationAuthority ==0)
            {
                Reconfigure.Visibility= Visibility.Collapsed;
            }
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

        //public static bool b;

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

        private void EditClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //e.Cancel = true;
            //main.menuedit.IsEnabled = true;
        
            //this.Hide();
        }

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

        private void DGcommondList_Loaded(object sender, RoutedEventArgs e)
        {
            dGCommond_Lists.Add(new DGCommond_list()
            {
                property = "1",
            });

            dGCommond_Lists.Add(new DGCommond_list()
            {
                property = "2",
            });
            dGCommond_Lists.Add(new DGCommond_list()
            {
                property = "33",
            });
            dGCommond_Lists.Add(new DGCommond_list()
            {
                property = "44",
            });
            dGCommond_Lists.Add(new DGCommond_list()
            {
                property = "555",
            });
            dGCommond_Lists.Add(new DGCommond_list()
            {
                property = "6",
            });
            dGCommond_Lists.Add(new DGCommond_list()
            {
                property = "7",
            });
            dGCommond_Lists.Add(new DGCommond_list()
            {
                property = "8",
            });
            dGCommond_Lists.Add(new DGCommond_list()
            {
                property = "9",
            });
            dGCommond_Lists.Add(new DGCommond_list()
            {
                property = "0",
            });
            dGCommond_Lists.Add(new DGCommond_list()
            {
                property = "11",
            });
            dGCommond_Lists.Add(new DGCommond_list()
            {
                property = "12",
            });
            dGCommond_Lists.Add(new DGCommond_list()
            {
                property = "121",
            });
            dGCommond_Lists.Add(new DGCommond_list()
            {
                property = "14",
            });
            dGCommond_Lists.Add(new DGCommond_list()
            {
                property = "15",
            });
            dGCommond_Lists.Add(new DGCommond_list()
            {
                property = "16",
            });
            (this.FindName("DGcommondList") as System.Windows.Controls.DataGrid).ItemsSource = dGCommond_Lists;
        }

        private void btnArea_Click(object sender, RoutedEventArgs e)
        {
            AreaCoat areaCoat = new AreaCoat();
            areaCoat.Show();
        }

        private void dataGridGJ_RightClick(object sender, MouseButtonEventArgs e)
        {
            ContextMenu context = new ContextMenu();
            
            MenuItem item = new MenuItem();
            item.Header = "点击删除该行数据";
            item.Height = 20;
            item.Padding = new Thickness(0, 0, 0, 0);
            item.Margin = new Thickness(0, -5, 0, 0);
            item.Click += new RoutedEventHandler(item_click);

            MenuItem AddPoint = new MenuItem();
            AddPoint.Header = "添加点";
            AddPoint.Height = 20;
            AddPoint.Padding = new Thickness(0, 0, 0, 0);
            AddPoint.Margin = new Thickness(0, 0, 0, 0);
            AddPoint.Click += new RoutedEventHandler(item_click);

            MenuItem AddLine = new MenuItem();
            AddLine.Header = "添加线";
            AddLine.Height = 20;
            AddLine.Padding = new Thickness(0, 0, 0, 0);
            AddLine.Margin = new Thickness(0, 0, 0, 0);
            AddLine.Click += new RoutedEventHandler(item_click);

            MenuItem AddHu = new MenuItem();
            AddHu.Header = "添加弧";
            AddHu.Height = 20;
            AddHu.Padding = new Thickness(0, 0, 0, 0);
            AddHu.Margin = new Thickness(0, 0, 0, 0);
            AddHu.Click += new RoutedEventHandler(item_click);

            MenuItem AddSome = new MenuItem();
            AddSome.Header = "添加";
            AddSome.Height = 20;
            AddSome.Padding = new Thickness(0, 0, 0, 0);
            AddSome.Margin = new Thickness(0,0, 0, -5);
            AddSome.Click += new RoutedEventHandler(item_click);
            AddSome.Items.Add(AddPoint);
            AddSome.Items.Add(AddLine);
            AddSome.Items.Add(AddHu);


            context.Items.Add(item);
            context.Items.Add(AddSome);
            context.IsOpen = true;
        }

        void item_click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
