﻿

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace exSplitter
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        GridLength m_WidthCache;

        public MainWindow()
        {
            InitializeComponent();
            //this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        //void MainWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    //获取GridSplitterr的cotrolTemplate中的按钮btn，必须在Loaded之后才能获取到
        //    Button btnGrdSplitter = gsSplitterr.Template.FindName("btnExpend", gsSplitterr) as Button;
        //    if (btnGrdSplitter != null)
        //        btnGrdSplitter.Click += new RoutedEventHandler(btnGrdSplitter_Click);
        //}


        //void btnGrdSplitter_Click(object sender, RoutedEventArgs e)
        //{
        //    GridLength temp = grdWorkbench.ColumnDefinitions[0].Width;
        //    GridLength def = new GridLength();
        //    if (temp.Equals(def))
        //    {
        //        //恢复
        //        grdWorkbench.ColumnDefinitions[0].Width = m_WidthCache;
        //    }
        //    else
        //    {
        //        //折叠
        //        m_WidthCache = grdWorkbench.ColumnDefinitions[0].Width;
        //        grdWorkbench.ColumnDefinitions[0].Width = def;
        //    }
        //}
    }
}

