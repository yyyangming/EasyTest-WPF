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
        protected int nextnum = 0;
        public NewFile2()
        {
            InitializeComponent();
        }

        NewFileGlobal FileGlobal = new NewFileGlobal();
        NewFileBaseConfigure newFileBaseConfigure = new NewFileBaseConfigure();
        NewFileBranchConfigure newFileBranchConfigure = new NewFileBranchConfigure();
        ProgramWizard programWizard = new ProgramWizard();
        private void NewFile2_JogPage_Loaded(object sender, RoutedEventArgs e)
        {
            NewFileConfigure.Content = new Frame() { Content = FileGlobal };
            nextnum = 1;
            JogVersion3 jogVersion3 = new JogVersion3();
            JogPage.Content = new Frame() { Content = jogVersion3 };
        }


        private void btnToBranchConfigure_click(object sender, RoutedEventArgs e)   
        {
            //NewFileBranchConfigure newFileBranchConfigure = new NewFileBranchConfigure();
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (nextnum == 1)
            {
                //一开始的时候出现的时基本参数
                NewFileBaseConfigure newFileBaseConfigure = new NewFileBaseConfigure();
                NewFileConfigure.Content = new Frame() { Content = newFileBaseConfigure };
                nextnum = 2;
            } 
            //基本参数后边时分支参数
            else if (nextnum == 2)
            {
                NewFileBranchConfigure newFileBranchConfigure  = new NewFileBranchConfigure();
                NewFileConfigure.Content = new Frame() { Content = newFileBranchConfigure };
                nextnum = 3;
            }
            //分支参数结束后退出参数设置
            else if (nextnum ==3)
            {
                
                this.Close();
                nextnum = 0;
            }
        }

        private void btnBeforePage_Click(object sender, RoutedEventArgs e)
        {
            //在基本参数时返回全局参数
            if (nextnum == 1)
            {
                programWizard.Show();
                this.Close();
                nextnum = 0;
            }
            //在分支参数时返回基本参数
            else if (nextnum == 2)
            {
                NewFileConfigure.Content = new Frame() { Content = FileGlobal };
                nextnum = 1;
            }
            else if (nextnum == 3)
            {
                NewFileConfigure.Content = new Frame() { Content = newFileBaseConfigure };
                nextnum = 2;
            }
        }
    }
}
