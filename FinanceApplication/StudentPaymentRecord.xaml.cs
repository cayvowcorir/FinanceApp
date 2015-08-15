using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FinanceApplication
{
    /// <summary>
    ///     Interaction logic for StudentPaymentRecord.xaml
    /// </summary>
    public partial class StudentPaymentRecord : Page
    {
        public StudentPaymentRecord()
        {
            InitializeComponent();
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
            var stdnt = new AddStudent();
            var dict = stdnt.RetrieveDetails(AdmNoTxtbx.Text);
            try
            {
                FirstName.Text = dict["first_name"];
                LastName.Text = dict["last_name"];
            }
            catch
            {
            }
        }

        private void Submit_Btn_Click(object sender, RoutedEventArgs e)
        {
            var pay = new Payment
            {
                AdmNo = AdmNoTxtbx.Text,
                Purpose = Purpose.Text,
                Type = PaymentType.Text,
                Amount = Convert.ToInt32(Amount.Text),
                SlipCheckNo = SlipCheckNo.Text
            };

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
            AdmNoTxtbx.Text = String.Empty;
            Amount.Text = String.Empty;
            FirstName.Text = String.Empty;
            LastName.Text = String.Empty;
            SlipCheckNo.Text = String.Empty;
        }

        private void payment_type_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((PaymentType.Text == "Cheque") || (PaymentType.Text == "Bank Slip"))
            {
                SlipCheckNumberLabel.IsEnabled = true;
                SlipCheckNo.IsEnabled = true;
            }
            else
            {
                SlipCheckNumberLabel.IsEnabled = false;
                SlipCheckNo.IsEnabled = false;
            }
        }

        private void PaymentRecords_TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            var fdset = new financeappDataSet();
            var fsdadapter =
                new financeappDataSetTableAdapters.payment_summaryTableAdapter();
            fsdadapter.Fill(fdset.payment_summary);
            PaymentRecordsGrid.DataContext = fsdadapter.GetData();
        }

        private void IndividualPayRecord_TextChanged(object sender, TextChangedEventArgs e)
        {
            var fdset = new financeappDataSet();
            var fsdadapter =
                new financeappDataSetTableAdapters.payment_summaryTableAdapter();
            fsdadapter.Fill(fdset.payment_summary);
            PaymentRecordsGrid.DataContext = fsdadapter.GetData();
        }

        private void Adm_no_votehead_brkdwn_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void Students_Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("StudentView.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}