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

namespace FinanceApplication
{
    /// <summary>
    /// Interaction logic for StudentPaymentRecord.xaml
    /// </summary>
    public partial class StudentPaymentRecord : Page
    {
        public StudentPaymentRecord()
        {
            InitializeComponent();

                        
        }
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
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
        private void FeeStructure_Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("ModifyFeeStructure.xaml", UriKind.RelativeOrAbsolute));
        }

        private void adm_no_txtbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddStudent stdnt = new AddStudent();
            Dictionary<string, string> dict = stdnt.RetrieveDetails(adm_no_txtbx.Text);
            try
            {
                first_name.Text = dict["first_name"];
                last_name.Text = dict["last_name"];
                
            }
            catch
            {

            }
        }

        private void Submit_Btn_Click(object sender, RoutedEventArgs e)
        {
            Payment pay = new Payment();
            pay.AdmNo=adm_no_txtbx.Text;
            pay.Purpose= purpose.Text;
            pay.Type=payment_type.Text;
            pay.Amount=amount.Text;
            pay.Slip_CheckNo=slip_check_no.Text;

            try
            {
                pay.MakePayment();
                
            }
            catch (Exception e1)
            {
                MessageBox.Show("There was an error. Error Message: " + e1.Message);
            }
            
        }
       
        public void PageClear()
        {
            adm_no_txtbx.Text = String.Empty;
            amount.Text = String.Empty;
            first_name.Text = String.Empty;
            last_name.Text = String.Empty;
            slip_check_no.Text = String.Empty;
        }


        private void payment_type_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((payment_type.Text == "Cheque") || (payment_type.Text == "Bank Slip"))
            {
                slip_check_number_label.IsEnabled = true;
                slip_check_no.IsEnabled = true;

            }
            else
            {
                slip_check_number_label.IsEnabled = false;
                slip_check_no.IsEnabled = false;
            }
        }

        private void PaymentRecords_TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            financeappDataSet fdset = new financeappDataSet();
            financeappDataSetTableAdapters.payment_summaryTableAdapter fsdadapter = new financeappDataSetTableAdapters.payment_summaryTableAdapter();
            fsdadapter.Fill(fdset.payment_summary);
            payment_records_grid.DataContext = fsdadapter.GetData();

        }

        private void IndividualPayRecord_TextChanged(object sender, TextChangedEventArgs e)
        {
            financeappDataSet fdset = new financeappDataSet();
            financeappDataSetTableAdapters.payment_summaryTableAdapter fsdadapter = new financeappDataSetTableAdapters.payment_summaryTableAdapter();
            fsdadapter.Fill(fdset.payment_summary);
            payment_records_grid.DataContext = fsdadapter.GetData();
        }

        private void Adm_no_votehead_brkdwn_TextChanged(object sender, TextChangedEventArgs e)
        {

        }   
        


    }
}
