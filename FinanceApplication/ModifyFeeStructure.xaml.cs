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
            var selectedForm="0";
            var selectedTerm="0";

            switch (FormSelector.Text)
            {
                case "Form 1":
                    selectedForm = "1";
                    break;
                case "Form 2":
                    selectedForm = "2";
                    break;
                case "Form 3":
                    selectedForm = "3";
                    break;
                case "Form 4":
                    selectedForm = "4";
                    break;

            }
            switch (TermSelector.Text)
            {
                case "Term 1":
                    selectedTerm = "1";
                    break;
                case "Term 2":
                    selectedTerm = "2";
                    break;
                case "Term 3":
                    selectedTerm = "3";
                    break;
                case "Term 4":
                    selectedTerm = "4";
                    break;
                default:
                    selectedTerm = "0";
                    break;

            }

            if (selectedForm== "0")
            {
                MessageBox.Show("You have not selected a Form");
            }
            else if (selectedTerm == "0")
            {
                MessageBox.Show("You have not selected a Term");
            }
            else
            {
                
                var fstructure = new FeeStructure();
                try
                {
                    var feesstructuredict = fstructure.InitializeClass(selectedForm, selectedTerm);
                    Bes.Text = feesstructuredict["bes"];
                    Rmi.Text = feesstructuredict["rmi"];
                    Ltt.Text = feesstructuredict["ltt"];
                    Adm.Text = feesstructuredict["adm"];
                    Ewc.Text = feesstructuredict["ewc"];
                    Medical.Text = feesstructuredict["medical"];
                    Pe.Text = feesstructuredict["pe"];
                    Bog.Text = feesstructuredict["bog"];
                    Pta.Text = feesstructuredict["pta"];
                    Activity.Text = feesstructuredict["activity"];
                    Mentorship.Text = feesstructuredict["mentorship"];
                }
                catch (Exception e1)
                {
                    MessageBox.Show("There was an error while retrieving data from the database. Error Message: " + e1.Message);
                }
                

            }
        }

        
        private void EnterPayment_Btn_click(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("StudentPaymentRecord.xaml", UriKind.RelativeOrAbsolute));
        }
       
    }
}
