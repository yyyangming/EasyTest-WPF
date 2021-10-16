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
using System.Windows.Forms;
using System.IO;
using System.Collections.ObjectModel;

namespace Test
{
    /// <summary>
    /// RobotSettings.xaml 的交互逻辑
    /// </summary>
    public partial class RobotSettings : Window
    {
        public RobotSettings()
        {
            ObservableCollection<propertyValue1> propertyValueList1 = new ObservableCollection<propertyValue1>();
            InitializeComponent();
        }


        public class propertyValue1
        {
            public string property { get; set; }
            public string value { get; set; }
        }


        private void DATA_Rabbit_loaded(object sender, RoutedEventArgs e)
        {
            List<propertyValue1> propertyValueList1 = new List<propertyValue1>();
            propertyValueList1.Add(new propertyValue1()
            {
                property = "安全Z轴高度",
                value = "1"
            });

            propertyValueList1.Add(new propertyValue1()
            {
                property = "安全速度",
                value = "2"
            });

            propertyValueList1.Add(new propertyValue1()
            {
                property = "Z轴速度",
                value = "2"
            });
            ((this.FindName("DATA_Rabbit")) as System.Windows.Controls.DataGrid).ItemsSource = propertyValueList1;
        }
    }
}
