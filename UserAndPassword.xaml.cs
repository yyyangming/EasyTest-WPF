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
using Test;

namespace Test
{
    /// <summary>
    /// UserAndPassword.xaml 的交互逻辑
    /// </summary>
    public partial class UserAndPassword : Window
    {
        public UserAndPassword()
        {
            InitializeComponent();
        }

        
        private void btn_InMainWindow_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            if (@operator.IsChecked==true&&TxtUserPw.Text== "admin")
            {
                MainWindow mainWindow = new MainWindow();
                main.OperationAuthority = 1;
                mainWindow.Custom_1.Content = main.OperationAuthority;
                mainWindow.Show();
                this.Close();
            }
            if (admin.IsChecked == true && TxtUserPw.Text == "admin")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
                main.OperationAuthority = 2;
                mainWindow.Custom_1.Content = main.OperationAuthority;
            }
            if (technician.IsChecked == true && TxtUserPw.Text == "admin")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
                main.OperationAuthority = 3;
                mainWindow.Custom_1.Content = main.OperationAuthority;
            }
            if (developer.IsChecked == true && TxtUserPw.Text == "admin")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
                main.OperationAuthority = 4;
                mainWindow.Custom_1.Content = main.OperationAuthority;
            }

            
        }
    }
}
