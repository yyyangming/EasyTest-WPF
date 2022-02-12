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
    /// ProgramWizard.xaml 的交互逻辑
    /// </summary>
    public partial class ProgramWizard : Window
    {
        Config config = new Config();
        public ProgramWizard()
        {
            InitializeComponent();
        }

        private void BtnNewFile_Click(object sender, RoutedEventArgs e)
        {
            if (Newlength.Text !=null&&NewWidth.Text!=null)
            {
                try
                {
                    if (int.Parse(Newlength.Text) < 0 && int.Parse(NewWidth.Text) < 0)
                    {
                        MessageBox.Show("宽高必须是正整数");
                    }
                    else if (int.Parse(Newlength.Text) > 100 && int.Parse(NewWidth.Text) > 300)
                    {
                        MessageBox.Show("宽高大于最高限制");
                    }
                    else
                    {
                        NewFile2 newFile21 = new NewFile2();
                        this.Close();
                        newFile21.Show();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("宽高必须是正整数");
                }
            }
            else MessageBox.Show("宽高不能为空");
        }



        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            short val;
            if (!Int16.TryParse(e.Text, out val))
                e.Handled = true;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void ProgramWizard_Loaded(object sender, RoutedEventArgs e)
        {
            config.ReadFile();
            Newlength.Text = Config.originpointX.ToString();
            NewWidth.Text = Config.originpointY.ToString();
        }
    }
}
