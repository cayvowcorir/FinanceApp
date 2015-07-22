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
        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        private DateTime dofBirth;

        public string DofBirth
        {
            get { return Convert.ToString(dofBirth); }
            set { dofBirth = Convert.ToDateTime(value); }
        }
        private string gender;

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        private DateTime admDate;

        public string AdmDate
        {
            get { return Convert.ToString(admDate); }
            set { admDate = Convert.ToDateTime(value); }
        }
        private string admNo;

        public string AdmNo
        {
            get { return admNo.ToString(); }
            set { admNo = value; }
        }
        private string parentContact;

        public string ParentContact
        {
            get { return parentContact; }
            set { parentContact = value; }
        }
        private string parentName;

        public string ParentName
        {
            get { return parentName; }
            set { parentName = value; }
        }       

        private string connectionString = Properties.Settings.Default.finanaceappConnectionString;
        public void New()
        {
            

            try
            {
                int Adm=Convert.ToInt32(AdmNo);
                financeappDataSet fdataset = new financeappDataSet();
                financeappDataSetTableAdapters.students_detailsTableAdapter fdatasetadapter = new financeappDataSetTableAdapters.students_detailsTableAdapter();

                try
                {
                    fdatasetadapter.Insert(Convert.ToInt32(admNo), firstName, lastName, dofBirth, gender, admDate, parentName, parentContact);
                    MessageBox.Show("The Student " + firstName + " has been added to the database");
                    StudentView stv = new StudentView();
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

        public Dictionary<string, string> RetrieveDetails(string admission_no)
        {
            string Adm = admission_no;
            financeappDataSet fdset = new financeappDataSet();
            financeappDataSetTableAdapters.students_detailsTableAdapter fadapter = new financeappDataSetTableAdapters.students_detailsTableAdapter();
            fadapter.Fill(fdset.students_details);

            Dictionary<string, string> stdictionary=new Dictionary<string, string>();
            int columncount=fdset.students_details.Columns.Count;
            int count = 0;
            var sqlwhere = "admission_no=" + Adm;
            try
            {
                DataTable _newDataTable = fdset.students_details.Select(sqlwhere).CopyToDataTable();
                DataRow row = _newDataTable.Rows[0];
                
                while (count < columncount)
                {
                    stdictionary[_newDataTable.Columns[count].ToString()] = Convert.ToString(row[_newDataTable.Columns[count]]);
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
                int Adm = Convert.ToInt32(AdmNo);
                financeappDataSet fdataset = new financeappDataSet();
                financeappDataSetTableAdapters.students_detailsTableAdapter fdatasetadapter = new financeappDataSetTableAdapters.students_detailsTableAdapter();
                fdatasetadapter.Fill(fdataset.students_details);
                var sqlwhere = "admission_no=" + Adm;
                DataTable _newDataTable = fdataset.students_details.Select(sqlwhere).CopyToDataTable();
                DataRow row = _newDataTable.Rows[0];
                try
                {
                    MessageBox.Show((Convert.ToInt32(row[0].ToString())).ToString());
                    //fdatasetadapter.Update(Convert.ToInt32(admNo), firstName, lastName, dofBirth, gender, admDate, parentName, parentContact);
                    MessageBox.Show("The Student " + row[0].ToString() + " has been added to the database");
                    StudentView stv = new StudentView();
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
