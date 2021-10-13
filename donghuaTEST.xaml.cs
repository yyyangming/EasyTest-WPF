using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WpfArcgis
{
    /// <summary>
    /// WDGotoXY.xaml 的交互逻辑
    /// </summary>
    public partial class WDGotoXY : Window
    {

        public WDGotoXY()
        {
            InitializeComponent();

        }

        private void btnSure_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }


        DispatcherTimer tm = new DispatcherTimer();
        void tm_Tick(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            this.tm.Stop();
            this.Close();
        }

        private void button1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tm.Tick += new EventHandler(tm_Tick);
            tm.Interval = TimeSpan.FromSeconds(0.2);
            tm.Start();
        }
    }
}