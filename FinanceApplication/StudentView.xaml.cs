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
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentView : Page
    {
        public StudentView()
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
                NavigationService?.GoBack();
            }
            catch
            {

            }
            
        }

        private void AddStudent_Submit_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (FirstName.Text == "" || LastName.Text == "" || Dob.Text == "" || Gender.Text == "" | AdmDate.Text == "" || AdmNo.Text == "" || ParentContact.Text == "" || ParentName.Text == "")
            {
                MessageBox.Show("You have left out a field");
            }
            else
            {
                try
                {
                    var addStudent = new AddStudent();

                    addStudent.FirstName = FirstName.Text;
                    addStudent.LastName = LastName.Text;
                    addStudent.DofBirth = Dob.Text;
                    addStudent.Gender = Gender.Text;
                    addStudent.AdmNo = AdmNo.Text;
                    addStudent.AdmDate = AdmDate.Text;
                    addStudent.ParentName = ParentName.Text;
                    addStudent.ParentContact = ParentContact.Text;
                    addStudent.New();
                    PageClear();
                }
                catch (Exception ex)
                {
                    var exceptionmessage = ex.Message.ToString();
                    MessageBox.Show("Error occured while trying to write to the database. Check the input values and try again. Error Message: " + exceptionmessage);
                }
            }
         
        }

        private void FeeStructure_Btn_Click(object sender, RoutedEventArgs e)
        {
            var nav = NavigationService.GetNavigationService(this);
            nav?.Navigate(new Uri("ModifyFeeStructure.xaml", UriKind.RelativeOrAbsolute));
        }


        public void PageClear()
        {
            FirstName.Text= String.Empty;
            LastName.Text = String.Empty;          
            AdmNo.Text = String.Empty;
            ParentContact.Text = String.Empty;
            ParentName.Text = String.Empty;
        }

        private void StudentDetail_Tbx_KeyUp(object sender, TextChangedEventArgs e)
        {
            var stdnt = new AddStudent();
            var dict=stdnt.RetrieveDetails(AdmNoModify.Text);
            try
            {
                FirstNameModify.Text = dict["first_name"];
                LastNameModify.Text = dict["last_name"];
                DobModify.Text = dict["dof_birth"];
                GenderModify.Text = dict["gender"];
                AdmDateModify.Text = dict["adm_date"];
                ParentNameModify.Text = dict["parent_name"];
                ParentContactModify.Text = dict["parent_contact"];
            }
            catch
            {

            }
        }
        private void ModifyStudent_Submit_Btn_Click(object sender, RoutedEventArgs e)
        {
           
            if (FirstNameModify.Text == "" || LastNameModify.Text == "" || DobModify.Text == "" || GenderModify.Text == "" | AdmDateModify.Text == "" || AdmNoModify.Text == "" || ParentContactModify.Text == "" || ParentNameModify.Text == "")
            {
                MessageBox.Show("You have left out a field");
            }
           
            else
            {
                try
                {
                    var addStudent = new AddStudent();

                    addStudent.FirstName = FirstNameModify.Text;
                    addStudent.LastName = LastNameModify.Text;
                    addStudent.DofBirth = DobModify.Text;
                    addStudent.Gender = GenderModify.Text;
                    addStudent.AdmNo = AdmNoModify.Text;
                    addStudent.AdmDate = AdmDateModify.Text;
                    addStudent.ParentName = ParentNameModify.Text;
                    addStudent.ParentContact = ParentContactModify.Text;
                    addStudent.Modify();
                    PageClear();
                }
                catch (Exception ex)
                {
                    var exceptionmessage = ex.Message.ToString();
                    MessageBox.Show("Error occured while trying to write to the database. Check the input values and try again. Error Message: " + exceptionmessage);
                }
            }

        }

        private void ViewStudents_Tab_GotFocus(object sender, RoutedEventArgs e)
        {
            var fdset = new financeapplicationDataSet();
            var fsdadapter = new financeapplicationDataSetTableAdapters.students_detailsTableAdapter();
            fsdadapter.Fill(fdset.students_details);
            ViewStudentsGrid.DataContext = fsdadapter.GetData();
        }

        private void EnterPayment_Btn_Click(object sender, RoutedEventArgs e)
        {
            var nav = NavigationService.GetNavigationService(this);
            nav?.Navigate(new Uri("StudentPaymentRecord.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
