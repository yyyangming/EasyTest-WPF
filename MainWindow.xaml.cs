﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static int editExit;
        Edit editForm = new Edit();
        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        protected void btnEdit(object sender, RoutedEventArgs e)
        {
            
            if (!Convert.ToBoolean(editExit))
            {
                MainWindow main = new MainWindow();
                editForm.Show();
                //menuedit.IsEnabled = false;
                editExit = 1;
            }
            else
            {
                editForm.Activate();
            }



        }

        private void btnLive(object sender, RoutedEventArgs e)
        {
            UserView userView = new UserView();
            userView.Show();
        }

        private void ProductMap_click(object sender, RoutedEventArgs e)
        {
            MWChangeProduct mWChangeProduct= new MWChangeProduct();
            mWChangeProduct.Show();
        }

        private void BtnTxt2_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnNewFile(object sender, RoutedEventArgs e)
        {

            ProgramWizard programWizard = new ProgramWizard();
            programWizard.Show();
        }

            //private void BtnTxt2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
            //{
            //    if (LineBtnG2.Visibility == Visibility.Visible)
            //    {
            //        LineBtnG2.Visibility = Visibility.Collapsed;
            //        BtnG2.BorderThickness = new Thickness(1, 1, 1, 0);
            //    }
            //    LineBtnTxt2.Visibility = Visibility.Visible;
            //    BtnTxt2.BorderThickness = new Thickness(2, 2, 0, 0);
            //    BtnTxt2.Background = new SolidColorBrush(Color.FromArgb(1,165,167,159));
            //}

            //private void BtnG2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
            //{
            //    if(LineBtnTxt2.Visibility == Visibility.Visible)
            //    {
            //        LineBtnTxt2.Visibility = Visibility.Collapsed;
            //        BtnTxt2.BorderThickness = new Thickness(1, 1, 1, 0);
            //    }
            //    LineBtnG2.Visibility = Visibility.Visible;
            //    BtnG2.BorderThickness = new Thickness(2, 2, 0, 0);
            //    BtnG2.Background = new SolidColorBrush(Color.FromArgb(1, 165, 167, 159));
            //}

            //private void BtnG_PreviewMouseDown(object sender, MouseButtonEventArgs e)
            //{
            //    if (LineBtnTxt2.Visibility == Visibility.Visible)
            //    {
            //        LineBtnTxt2.Visibility = Visibility.Collapsed;
            //        BtnTxt.BorderThickness = new Thickness(1, 1, 1, 0);
            //    }
            //    LineBtnG2.Visibility = Visibility.Visible;
            //    BtnG.BorderThickness = new Thickness(2, 2, 0, 0);
            //    BtnG.Background = new SolidColorBrush(Color.FromArgb(1, 165, 167, 159));
            //}

            //private void BtnTxt_PreviewMouseDown(object sender, MouseButtonEventArgs e)
            //{
            //    if (LineBtnG2.Visibility == Visibility.Visible)
            //    {
            //        LineBtnG2.Visibility = Visibility.Collapsed;
            //        BtnG.BorderThickness = new Thickness(1, 1, 1, 0);
            //    }
            //    LineBtnTxt2.Visibility = Visibility.Visible;
            //    BtnTxt.BorderThickness = new Thickness(2, 2, 0, 0);
            //    BtnTxt.Background = new SolidColorBrush(Color.FromArgb(1, 165, 167, 159));
            //}

        private void UserButten_Click(object sender, RoutedEventArgs e)
        {
            //StyleTest styleTest= new StyleTest();
            //styleTest.Show();
            Jog jog= new Jog();
            jog.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WpfArcgis.WDGotoXY wDGotoXY    = new WpfArcgis.WDGotoXY();
            wDGotoXY.Show();
        }



        protected void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CustomButtons customButtons = new CustomButtons();
            Custom_1.Content = customButtons.Custom1.Text;
            Custom_2.Content = customButtons.Custom2.Text;
            Custom_3.Content = customButtons.Custom3.Text;
            Custom_4.Content = customButtons.Custom4.Text;
            Custom_5.Content = customButtons.Custom5.Text;
            this.Height = SystemParameters.WorkArea.Height;
            this.Width = SystemParameters.WorkArea.Width;
        }

        private void TabControl_Second_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void MainWindowMenu_Loaded(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            if (main.OperationAuthority == 1)
            {
                MainWindowMenu.Visibility = Visibility.Collapsed;
            }
        }
    }
}
