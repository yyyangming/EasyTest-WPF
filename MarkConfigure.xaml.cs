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
    /// MarkConfigure.xaml 的交互逻辑
    /// </summary>
    public partial class MarkConfigure : Window
    {
        public MarkConfigure()
        {
            InitializeComponent();
        }



        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            this.Owner.Visibility = Visibility.Visible;//显示父窗体
            base.OnClosing(e);
        }

        private void btnShowJog_Click(object sender, RoutedEventArgs e)
        {
            JogPage.Visibility = Visibility.Visible;
        }
    }
}
