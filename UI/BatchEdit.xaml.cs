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
using System.Xml;

namespace Test.UI
{
    /// <summary>
    /// BatchEdit.xaml 的交互逻辑
    /// </summary>
    public partial class BatchEdit : Window
    {
        public string Sort;
        public BatchEdit()
        {
            InitializeComponent();
        }

        private void BacthEdit_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            string XMLPath = "E:\\desket\\GitDevelop\\MySelf\\EasyCoat-WPF\\XML\\Test.xml";
            doc.Load(XMLPath);

            XmlElement xle = doc.CreateElement("trajectory");

        }
    }
}
