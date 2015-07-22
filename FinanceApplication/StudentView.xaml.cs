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

        private void AddStudent_Submit_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (first_name.Text == "" || last_name.Text == "" || dob.Text == "" || gender.Text == "" | adm_date.Text == "" || adm_no.Text == "" || parent_contact.Text == "" || parent_name.Text == "")
            {
                MessageBox.Show("You have left out a field");
            }
            else
            {
                try
                {
                    AddStudent addStudent = new AddStudent();

                    addStudent.FirstName = first_name.Text;
                    addStudent.LastName = last_name.Text;
                    addStudent.DofBirth = dob.Text;
                    addStudent.Gender = gender.Text;
                    addStudent.AdmNo = adm_no.Text;
                    addStudent.AdmDate = adm_date.Text;
                    addStudent.ParentName = parent_name.Text;
                    addStudent.ParentContact = parent_contact.Text;
                    addStudent.New();
                    PageClear();
                }
                catch (Exception ex)
                {
                    string exceptionmessage = ex.Message.ToString();
                    MessageBox.Show("Error occured while trying to write to the database. Check the input values and try again. Error Message: " + exceptionmessage);
                }
            }
         
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FeeStructure_Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("ModifyFeeStructure.xaml", UriKind.RelativeOrAbsolute));
        }


        public void PageClear()
        {
            first_name.Text= String.Empty;
            last_name.Text = String.Empty;          
            adm_no.Text = String.Empty;
            parent_contact.Text = String.Empty;
            parent_name.Text = String.Empty;
        }

        private void StudentDetail_Tbx_KeyUp(object sender, TextChangedEventArgs e)
        {
            AddStudent stdnt = new AddStudent();
            Dictionary<string, string> dict=stdnt.RetrieveDetails(adm_no_modify.Text);
            try
            {
                first_name_modify.Text = dict["first_name"];
                last_name_modify.Text = dict["last_name"];
                dob_modify.Text = dict["dof_birth"];
                gender_modify.Text = dict["gender"];
                adm_date_modify.Text = dict["adm_date"];
                parent_name_modify.Text = dict["parent_name"];
                parent_contact_modify.Text = dict["parent_contact"];
            }
            catch
            {

            }
        }

        private void ModifyStudent_Submit_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (first_name_modify.Text == "" || last_name_modify.Text == "" || dob_modify.Text == "" || gender_modify.Text == "" | adm_date_modify.Text == "" || adm_no_modify.Text == "" || parent_contact_modify.Text == "" || parent_name_modify.Text == "")
            {
                MessageBox.Show("You have left out a field");
            }
            else
            {
                try
                {
                    AddStudent addStudent = new AddStudent();

                    addStudent.FirstName = first_name_modify.Text;
                    addStudent.LastName = last_name_modify.Text;
                    addStudent.DofBirth = dob_modify.Text;
                    addStudent.Gender = gender_modify.Text;
                    addStudent.AdmNo = adm_no_modify.Text;
                    addStudent.AdmDate = adm_date_modify.Text;
                    addStudent.ParentName = parent_name_modify.Text;
                    addStudent.ParentContact = parent_contact_modify.Text;
                    addStudent.Modify();
                    PageClear();
                }
                catch (Exception ex)
                {
                    string exceptionmessage = ex.Message.ToString();
                    MessageBox.Show("Error occured while trying to write to the database. Check the input values and try again. Error Message: " + exceptionmessage);
                }
            }

        }

        private void ViewStudents_Tab_Loaded(object sender, RoutedEventArgs e)
        {
            financeappDataSet fdset = new financeappDataSet();
            financeappDataSetTableAdapters.students_detailsTableAdapter fsdadapter = new financeappDataSetTableAdapters.students_detailsTableAdapter();
            fsdadapter.Fill(fdset.students_details);
            view_students_grid.DataContext = fsdadapter.GetData();
        }


       
        
    }
}
