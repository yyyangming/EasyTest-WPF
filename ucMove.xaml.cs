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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test
{
    /// <summary>
    /// ucMove.xaml 的交互逻辑
    /// </summary>
    public partial class ucMove : UserControl
    {

        private void picCoat_Initialized(object sender, EventArgs e)
        {
            picCoat.Source = new BitmapImage(new Uri("imgs/涂装.png"));
        }

        //public ucMove()
        //{
        //    InitializeComponent();
        //}
        //public class ClassConfigInfo
        //{
        //    int _classId;
        //    string _className;
        //    public int ClassId { get => _classId; set => _classId = value; }
        //    public string ClassName { get => _className; set => _className = value; }
        //}
        //private List<ClassConfigInfo> GetConfigItemInfos()
        //{
        //    List<ClassConfigInfo> list = new List<ClassConfigInfo>();
        //    list.AddRange(new ClassConfigInfo[]
        //    {
        //        new ClassConfigInfo()
        //        {
        //            ClassId = 0,
        //            ClassName = "阀1"
        //        },
        //        new ClassConfigInfo()
        //        {
        //            ClassId = 1,
        //            ClassName = "阀2"
        //        },
        //        new ClassConfigInfo()
        //        {
        //            ClassId = 2,
        //            ClassName = "阀3"
        //        },
        //        new ClassConfigInfo()
        //        {
        //            ClassId = 3,
        //            ClassName = "阀4"
        //        },
        //        new ClassConfigInfo()
        //        {
        //            ClassId = 4,
        //            ClassName = "阀5"
        //        }
        //    }
        //    );
        //    return list;
        //}
    }
}
