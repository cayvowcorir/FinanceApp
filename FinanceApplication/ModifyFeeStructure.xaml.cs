using System;
using System.Collections.Generic;
using System.Data;
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

namespace FinanceApplication
{
    /// <summary>
    /// Interaction logic for ModifyFeeStructure.xaml
    /// </summary>
    public partial class ModifyFeeStructure : Page
    {
        public ModifyFeeStructure()
        {
            InitializeComponent();

        }

        private void Form_Selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Submit_btn_Click(object sender, RoutedEventArgs e)
        {
            /* 
             * 
             * To check if all the form and term fields have been selected
             * 
             * */
            
            
        }

        private void StudentView_Btn_click(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("StudentView.xaml", UriKind.RelativeOrAbsolute));

        }
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void RadPaneGroup_SelectionChanged(object sender, Telerik.Windows.Controls.RadSelectionChangedEventArgs e)
        {

        }

        private void RadPaneGroup_SelectionChanged_1(object sender, Telerik.Windows.Controls.RadSelectionChangedEventArgs e)
        {

        }

        private void Back_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.GoBack();
            }
            catch
            {

            }
            
        }

        private void RadPaneGroup_SelectionChanged_2(object sender, Telerik.Windows.Controls.RadSelectionChangedEventArgs e)
        {

        }

        private void LoadFeeStructure_Btn_Click(object sender, RoutedEventArgs e)
        {
            String selected_form="0";
            String selected_term="0";

            switch (Form_Selector.Text)
            {
                case "Form 1":
                    selected_form = "1";
                    break;
                case "Form 2":
                    selected_form = "2";
                    break;
                case "Form 3":
                    selected_form = "3";
                    break;
                case "Form 4":
                    selected_form = "4";
                    break;

            }
            switch (Term_Selector.Text)
            {
                case "Term 1":
                    selected_term = "1";
                    break;
                case "Term 2":
                    selected_term = "2";
                    break;
                case "Term 3":
                    selected_term = "3";
                    break;
                case "Term 4":
                    selected_term = "4";
                    break;
                default:
                    selected_term = "0";
                    break;

            }

            if (selected_form== "0")
            {
                MessageBox.Show("You have not selected a Form");
            }
            else if (selected_term == "0")
            {
                MessageBox.Show("You have not selected a Term");
            }
            else
            {
                
                FeeStructure fstructure = new FeeStructure();
                try
                {
                    Dictionary<string, string> feesstructuredict = fstructure.InitializeClass(selected_form, selected_term);
                    BES.Text = feesstructuredict["bes"];
                    RMI.Text = feesstructuredict["rmi"];
                    LTT.Text = feesstructuredict["ltt"];
                    ADM.Text = feesstructuredict["adm"];
                    EWC.Text = feesstructuredict["ewc"];
                    MEDICAL.Text = feesstructuredict["medical"];
                    PE.Text = feesstructuredict["pe"];
                    BOG.Text = feesstructuredict["bog"];
                    PTA.Text = feesstructuredict["pta"];
                    ACTIVITY.Text = feesstructuredict["activity"];
                    MENTORSHIP.Text = feesstructuredict["mentorship"];
                }
                catch (Exception e1)
                {
                    MessageBox.Show("There was an error while retrieving data from the database. Error Message: " + e1.Message);
                }
                

            }
        }
       
    }
}
