using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Timers;

namespace Test
{
    /// <summary>
    /// TestDraw.xaml 的交互逻辑
    /// </summary>
    public partial class TestDraw : Window
    {
        public TestDraw()
        {
            InitializeComponent();
        }

        private void LineX1MouseEnter(object sender, MouseEventArgs e)
        {

        }
        //int X1H = 10;
        //void Roll()
        //{
        //    while (X1H >= -290)
        //    {
        //        LineX1.X1 = LineX1.X1 - 10;
        //    }
        //}
        //private void LineX1MouseEnter(object sender, MouseEventArgs e)
        //{
        //    Thread th = new Thread(Roll);
        //    th.Start();

        //}
    }
}
