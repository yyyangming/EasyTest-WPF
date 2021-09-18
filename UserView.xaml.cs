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
    /// UserView.xaml 的交互逻辑
    /// </summary>
    public partial class UserView : Window
    {
        public UserView()
        {
            InitializeComponent();
        }

        private void UserView_initoalized(object sender, EventArgs e)
        {
            Canvas mainPanel = new Canvas();
        }
        /// <summary>
        /// 绘制线段
        /// </summary>
        protected void DrawingLine(Point startPt, Point endPt)
        {

        }

    }
}
