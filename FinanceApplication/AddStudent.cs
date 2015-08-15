using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FinanceApplication
{
    class AddStudent
    {
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        private DateTime _dofBirth;

        public string DofBirth
        {
            get { return Convert.ToString(_dofBirth); }
            set { _dofBirth = Convert.ToDateTime(value); }
        }
        private string _gender;

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
        private DateTime _admDate;

        public string AdmDate
        {
            get { return Convert.ToString(_admDate); }
            set { _admDate = Convert.ToDateTime(value); }
        }
        private string _admNo;

        public string AdmNo
        {
            get { return _admNo.ToString(); }
            set { _admNo = value; }
        }
        private string _parentContact;

        public string ParentContact
        {
            get { return _parentContact; }
            set { _parentContact = value; }
        }
        private string _parentName;

        public string ParentName
        {
            get { return _parentName; }
            set { _parentName = value; }
        }       

        private string _connectionString = Properties.Settings.Default.financeappConnectionString;
        public void New()
        {
            

            try
            {
                var adm=Convert.ToInt32(AdmNo);
                var fdataset = new financeappDataSet();
                var sdetailsAdapter = new financeappDataSetTableAdapters.students_detailsTableAdapter();
                var pdetailsAdapter = new financeappDataSetTableAdapters.payment_detailsTableAdapter();

                try
                {
                    sdetailsAdapter.Insert(Convert.ToInt32(_admNo), _firstName, _lastName, _dofBirth, _gender, _admDate, _parentName, _parentContact);
                    
                    //loop to initialize payment details of the new student from form 1-4 to 0 in db
                    for (var count1 = 1; count1 < 5; count1++)
                    {
                        for (var count2 = 1; count2 < 4; count2++)
                        {
                            pdetailsAdapter.Insert(Convert.ToInt32(_admNo), 0, 0, 0, 0, 0, 0, 0,
                                0, 0, 0, 0, 0, 0, count1, count2, 0);
                        }
                    }

                    MessageBox.Show("The Student " + _firstName + " has been added to the database");
                    var stv = new StudentView();
                    stv.PageClear();
                }
                catch (Exception e2)
                {
                    MessageBox.Show("An error has occured. Error Message: " + e2.Message.ToString());
                }            
                
            }
            catch(Exception e1)
            {                
                MessageBox.Show("Error occured while trying to write to the database. Please try again. Error Message: "+ e1.Message.ToString());
            }

            

        }

        public Dictionary<string, string> RetrieveDetails(string admissionNo)
        {
            var adm = admissionNo;
            var fdset = new financeappDataSet();
            var fadapter = new financeappDataSetTableAdapters.students_detailsTableAdapter();
            fadapter.Fill(fdset.students_details);

            var stdictionary=new Dictionary<string, string>();
            var columncount=fdset.students_details.Columns.Count;
            var count = 0;
            var sqlwhere = "admission_no=" + adm;
            try
            {
                var newDataTable = fdset.students_details.Select(sqlwhere).CopyToDataTable();
                var row = newDataTable.Rows[0];
                
                while (count < columncount)
                {
                    stdictionary[newDataTable.Columns[count].ToString()] = Convert.ToString(row[newDataTable.Columns[count]]);
                    count++;
                }
            }
            catch
            {
                
            }        

            return stdictionary;
        }



        internal void Modify()
        {
            try
            {
                var adm = Convert.ToInt32(AdmNo);
                var fdataset = new financeappDataSet();
                var fdatasetadapter = new financeappDataSetTableAdapters.students_detailsTableAdapter();
                fdatasetadapter.Fill(fdataset.students_details);
                var sqlwhere = "admission_no=" + adm;
                var newDataTable = fdataset.students_details.Select(sqlwhere).CopyToDataTable();
                var row = newDataTable.Rows[0];
                try
                {
                    MessageBox.Show((Convert.ToInt32(row[0].ToString())).ToString());
                    //fdatasetadapter.Update(Convert.ToInt32(admNo), firstName, lastName, dofBirth, gender, admDate, parentName, parentContact);
                    MessageBox.Show("The Student " + row[0].ToString() + " has been added to the database");
                    var stv = new StudentView();
                    stv.PageClear();
                }
                catch (Exception e2)
                {
                    MessageBox.Show("An error has occured. Error Message: " + e2.Message.ToString());
                }

            }
            catch (Exception e1)
            {
                MessageBox.Show("Error occured while trying to write to the database. Please try again. Error Message: " + e1.Message.ToString());
            }

        }
    }
}
