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

namespace Test
{
    /// <summary>
    /// 编辑页面.xaml 的交互逻辑
    /// </summary>
    public partial class Edit : Window
    {
        public Edit()
        {
            InitializeComponent();
        }



        private void btnNewFile(object sender, RoutedEventArgs e)
        {
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
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "所有文件.*|*.*";
            fileDialog.InitialDirectory = "E:\\";
            fileDialog.Multiselect = true;
            fileDialog.ShowDialog();
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

            List<ClassInfo> list = GetItemInfos();
            #region 1.指定数据源
            //指定数据源
            comboBox1.ItemsSource = list;
            //如果使用指定数据源，不能直接移除，动态的添加移除//
            //如果需要动态添加，利用循环进行添加，把数组里面的全部添加进去
            #endregion

            #region 2.绑定DataContent
            //或者使用DataContent属性绑定数据源
            //不指指定数据源，绑定数据源
            comboBox1.DataContext = list;
            //接着在wpf中comboBox中添加绑定ItemSource = "{Banding}"
            #endregion
            //本来应该是使用数据库，这边为了简化，直接指定   
            comboBox1.SelectedValuePath = "ClassId";//项的值对应的属性名
            comboBox1.DisplayMemberPath = "ClassName";//项的显示值对应的属性值
        }



        private List<ClassInfo> GetItemInfos()
        {
            List < ClassInfo > list = new List<ClassInfo>();
            list.AddRange(new ClassInfo[]
            {
                new ClassInfo()
                {
                    ClassId = 0,
                    ClassName = "组阀1"
                },
                new ClassInfo()
                {
                    ClassId = 1,
                    ClassName = "组阀2"
                },
                new ClassInfo()
                {
                    ClassId = 2,
                    ClassName = "组阀3"
                },
                new ClassInfo()
                {
                    ClassId = 3,
                    ClassName = "组阀4"
                },
                new ClassInfo()
                {
                    ClassId = 4,
                    ClassName = "组阀5"
                }
            }
            );
            return list ;
        }



        //定义了一个在comboBox的类，其_classid为序号，_className为内容
        public class ClassInfo
        {
            int _classId;
            string _className;
            public int ClassId { get => _classId; set => _classId = value; }
            public string ClassName { get => _className; set => _className = value; }
        }

        //在选中配置的时候提示选择了**配置
        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.MessageBox.Show((comboBox1.SelectedItem as ClassInfo).ClassName);
        }     


    }
}
