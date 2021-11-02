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
    /// NewFile2.xaml 的交互逻辑
    /// </summary>
    public partial class NewFile2 : Window
    {
        public NewFile2()
        {
            InitializeComponent();
        }

        private void NewFile2_JogPage_Loaded(object sender, RoutedEventArgs e)
        {
            JogVersion2 jogVersion2 = new JogVersion2();
            JogPage.Content = new Frame() { Content = jogVersion2 };
        }

        private void btnMarkConfigure_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            MarkConfigure markConfigure = new MarkConfigure();
            markConfigure.Owner = this;
            markConfigure.ShowDialog();
        }

        private void btnToBranchConfigure_click(object sender, RoutedEventArgs e)   
        {
            NewFileBranchConfigure newFileBranchConfigure = new NewFileBranchConfigure();

        }
    }
}
