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
    public partial class JogAndView : Window
    {

        public JogAndView()
        {
            InitializeComponent();
        }



        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            //this.Owner.Visibility = Visibility.Visible;//显示父窗体
            //base.OnClosing(e);
        }

        //private void btnShowJog_Click(object sender, RoutedEventArgs e)
        //{
        //    JogPage.Visibility = Visibility.Visible;
        //}

        private void btnOpenJog_Click(object sender, RoutedEventArgs e)
        {
            if (markConfigure.Width > 500 && markConfigure.Width < 820)
            {
                markConfigure.Width = markConfigure.Width - 290;
                //JogVersion2 jogVersion2 = new JogVersion2();
                //JogPage.Source = new Uri(Convert.ToString( jogVersion2));
            }
            else if (markConfigure.Width < 360&& markConfigure.Width >300)
            {
                markConfigure.Width += 290;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (markConfigure.Width > 500)
            {
                markConfigure.Width = markConfigure.Width - 300;
                //JogVersion2 jogVersion2 = new JogVersion2();
                //JogPage.Source = new Uri(Convert.ToString( jogVersion2));
            }
            else if (markConfigure.Width < 360)
            {
                markConfigure.Width += 300;
            }
            //if (jogAndView.Width>600&& jogAndView.Width <800)
            //{
            //    jogAndView.Width =  jogAndView.Width - 292;
            //}
            //else
            //{
            //    jogAndView.Width = jogAndView.Width + 292;
            //}
        }

        //private void btnOpenView_Click(object sender, RoutedEventArgs e)
        //{
        //    if (markConfigure.Width > 780 && markConfigure.Width <820)
        //    {
        //        markConfigure.Width = markConfigure.Width - 370;
        //        markconfigure.Margin = new Thickness    (-370, 0, 0, 0);
        //    }
        //    else if ( markConfigure.Width < 200)
        //    {
        //        markConfigure.Width += 370;
        //        markconfigure.Margin = new Thickness(0, 0, 0, 0);
        //    }
        //    else if (markConfigure.Width < 440 && markConfigure.Width > 420)
        //    {
        //        markConfigure.Width += 370;
        //        markconfigure.Margin = new Thickness(0, 0, 0, 0);
        //    }
        //    else if (markConfigure.Width >480&& markConfigure.Width < 520)
        //    {
        //        markConfigure.Width = markConfigure.Width - 370;
        //        markconfigure.Margin = new Thickness(-370, 0, 0, 0);
        //    }

        //}

        //private void btnOpenView_Click(object sender, RoutedEventArgs e)
        //{
        //    if (MarkUserView.Width == 370)
        //    {
        //        markConfigure.Width=430;
        //        MarkUserView.Width = 0;
        //    }
        //    else
        //    {
        //        MarkUserView.Width = 370;
        //        markConfigure.Width = 800;
        //    }

        //}


    }

}
