using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// JogVersion2.xaml 的交互逻辑
    /// </summary>
    public partial class JogVersion2 : Page
    {
        public JogVersion2()
        {
            InitializeComponent();
        }

        private void Mainwindows_Keydown(object sender, KeyEventArgs e) 
        {
            if (true)
            {

            }
        }

        private void JogAndView_Loaded(object sender, RoutedEventArgs e)
        {
            Config.GetGhostConfig();
            TbFixedSpeedOne.Text = Config.fixedSpeedOne;
            TbFixedSpeedTwo.Text = Config.fixedSpeedTwo;
            TbFixedSpeedThree.Text = Config.fixedSpeedthree;
        }

        private void TbFixedSpeedOne_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TbFixedSpeed_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");

            e.Handled = re.IsMatch(e.Text);
        }

        private void btnWriteSpeed_Click(object sender, RoutedEventArgs e)
        {
            Config.fixedSpeedOne = TbFixedSpeedOne.Text;
            Config.fixedSpeedTwo = TbFixedSpeedTwo.Text;
            Config.fixedSpeedthree = TbFixedSpeedThree.Text;
            Config.SetGhostConfig();
        }
    }
}
