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
    /// ToolConfigureCamrea.xaml 的交互逻辑
    /// </summary>
    public partial class ToolConfigureCamrea : Page
    {
        public ToolConfigureCamrea()
        {
            InitializeComponent();
        }

        private void ToolConfigureCamera_Click(object sender, RoutedEventArgs e)
        {
            btn_Fluid_shape.IsEnabled = false;
            btn_Fluid_shape.Opacity = 0.5;
            btn_ChooseColor.IsEnabled = false;
            btn_ChooseColor.Opacity = 0.5;
            btn_ToolShape.IsEnabled = false;
            btn_ToolShape.Opacity = 0.5;
            btn_Calilrate_roate.IsEnabled = false;
            btn_Calilrate_roate.Opacity = 0.5;
        }
    }
}
