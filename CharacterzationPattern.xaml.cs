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
    /// CharacterzationPattern.xaml 的交互逻辑
    /// </summary>
    public partial class CharacterzationPattern : Page
    {
        public CharacterzationPattern()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        private void PatternToResponse_Click(object sender, RoutedEventArgs e)
        {
            CharacterizationResponseTimes characterizationResponseTimes= new CharacterizationResponseTimes();
            
        }
    }
}
