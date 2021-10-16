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
    /// ConformalCoatingCharacterizationWizard.xaml 的交互逻辑
    /// </summary>
    public partial class ConformalCoatingCharacterizationWizard : Window
    {
        public ConformalCoatingCharacterizationWizard()
        {
            InitializeComponent();
        }

        //CharacterizationResponseTimes characterizationResponseTimes = new CharacterizationResponseTimes();



        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            CharaterzationWizard charaterzationWizard= new CharaterzationWizard(); 
            CharacterzationMain.Content = charaterzationWizard;

        }
        private void PatternToResponse_Click(object sender, RoutedEventArgs e)
        {
            CharacterizationResponseTimes characterizationResponseTimes = new CharacterizationResponseTimes();
            CharacterzationMain.Content = characterizationResponseTimes;

        }
        private void WizardToPattern_Click(object sender, RoutedEventArgs e)
        {
            CharacterzationPattern characterzationPattern = new CharacterzationPattern();
            CharacterzationMain.Content = characterzationPattern;
        }
    }
}
