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

namespace Test
{
    /// <summary>
    /// UserJog.xaml 的交互逻辑
    /// </summary>
    public partial class UserJog : Window
    {
        public UserJog()
        {
            InitializeComponent();
        }



        private void UserJog_Initialized(object sender, EventArgs e)
        {
            //首先要是知道，获取数据源的方法是comboBox.ItemsSource()
            //要知道在这个地方的数据类型是不一样的，是IEnumerable,list的类型是这个类型，可以直接用
            //comboBox1.ItemsSource;
            //获取数据源

            List<ClassConfigInfo> listConfig = GetConfigItemInfos();
            #region 1.指定数据源
            //指定数据源
            ccbConfig.ItemsSource = listConfig;
            //如果使用指定数据源，不能直接移除，动态的添加移除//
            //如果需要动态添加，利用循环进行添加，把数组里面的全部添加进去
            #endregion
            #region 2.绑定DataContent
            //或者使用DataContent属性绑定数据源
            //不指指定数据源，绑定数据源
            ccbConfig.DataContext = listConfig;
            //接着在wpf中comboBox中添加绑定ItemSource = "{Banding}"
            #endregion
            //本来应该是使用数据库，这边为了简化，直接指定   
            ccbConfig.SelectedValuePath = "ClassId";//项的值对应的属性名
            ccbConfig.DisplayMemberPath = "ClassName";//项的显示值对应的属性值
        }
        public class ClassConfigInfo
        {
            int _classId;
            string _className;
            public int ClassId { get => _classId; set => _classId = value; }
            public string ClassName { get => _className; set => _className = value; }
        }
        private List<ClassConfigInfo> GetConfigItemInfos()
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
    }
}
