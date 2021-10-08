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
    /// ToolConfigure.xaml 的交互逻辑
    /// </summary>
    public partial class ToolConfigure : Window
    {
        public ToolConfigure()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToolConfigure_Click(object sender, RoutedEventArgs e)
        {
            ToolConfigureConfigure toolConfigureConfigure = new ToolConfigureConfigure();
            toolConfigureConfigure.Show();
        }

        private void ChooseColor_Click(object sender, RoutedEventArgs e)
        {
            ToolConfigureChooseColor toolConfigureChooseColor= new ToolConfigureChooseColor();
            toolConfigureChooseColor.Show();
        }
    }
}
