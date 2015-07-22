using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FinanceApplication
{
    class Payment
    {
        String admNo;

        public String AdmNo
        {
            get { return admNo; }
            set { admNo = value; }
        }
        String purpose;

        public String Purpose
        {
            get { return purpose; }
            set { purpose = value; }
        }
        String type;

        public String Type
        {
            get { return type; }
            set { type = value; }
        }
        String amount;

        public String Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        
        string slip_CheckNo;

        public string Slip_CheckNo
        {
            get { return slip_CheckNo; }
            set { slip_CheckNo = value; }
        }

       




        public void MakePayment()
        {
            financeappDataSet fdset = new financeappDataSet();
            financeappDataSetTableAdapters.payment_summaryTableAdapter fadapter = new financeappDataSetTableAdapters.payment_summaryTableAdapter();
            financeappDataSetTableAdapters.fees_structureTableAdapter fpayadapter = new financeappDataSetTableAdapters.fees_structureTableAdapter();
            financeappDataSetTableAdapters.fees_structureTableAdapter feeadapter=new financeappDataSetTableAdapters.fees_structureTableAdapter();
            feeadapter.Fill(fdset.fees_structure);
            fadapter.Fill(fdset.payment_summary);

            //to add the payment to the payment summary table
            if (type == "Check")
            {
                try
                {
                    fadapter.Insert(Convert.ToInt32(admNo), type, slip_CheckNo, purpose, null, amount);
                    MessageBox.Show("The Entry has been added to the database");
                    StudentPaymentRecord str = new StudentPaymentRecord();
                    str.PageClear();
                }
                catch (Exception e1)
                {
                    MessageBox.Show("There was an Error while trying to write to the database. Please try again. Error Message: " + e1.Message);
                }
            }
            else if (type == "Bank Slip")
            {
                try
                {
                    fadapter.Insert(Convert.ToInt32(admNo), type, null, purpose, Slip_CheckNo, amount);
                    MessageBox.Show("The Entry has been added to the database");
                    StudentPaymentRecord str = new StudentPaymentRecord();
                    str.PageClear();
                }
                catch (Exception e2)
                {
                    MessageBox.Show("There was an Error while trying to write to the database. Please try again.Error Message: " + e2.Message);
                }
            }
            else
            {
                try
                {
                    fadapter.Insert(Convert.ToInt32(admNo), type, null, purpose, null, amount);
                    MessageBox.Show("The Entry has been added to the database");
                    StudentPaymentRecord str = new StudentPaymentRecord();
                    str.PageClear();
                }
                catch (Exception e2)
                {
                    MessageBox.Show("There was an Error while trying to write to the database. Please try again.Error Message: " + e2.Message);
                }
            }

            //will return all payments made by the student with the admission number specified
            //as reflected by the payment summary table in the database
            financeappDataSet fdset2 = new financeappDataSet();//have to create this since data has changed from insertion above
            financeappDataSetTableAdapters.payment_summaryTableAdapter fsummary = new financeappDataSetTableAdapters.payment_summaryTableAdapter();
            fsummary.Fill(fdset2.payment_summary);
            var sqlwhere1="student_id="+admNo;
            DataTable DataTable1 = fdset2.payment_summary.Select(sqlwhere1).CopyToDataTable();
            int num_rows=DataTable1.Rows.Count;
            int count=0;
            
            //iterate through all the payments made by the student while adding the values of the 'amount' column
            //to get the total amount paid by the student.
            int sum=0;
            while(count<num_rows){
                DataRow row1=DataTable1.Rows[count];
                sum=sum+Convert.ToInt32(row1[6].ToString());
                count++;
            }

            //compare the total paid by the particular student with each year's total in the fee structure
            for (int count2 = 1; count2 < 5; count2++)
            {
                for (int count3 = 1; count3 < 4; count3++)
                {
                    string sqlwhere2 = "form=" + count2 + "and term=" + count3;

                    DataTable DataTable2 = fdset.fees_structure.Select(sqlwhere2).CopyToDataTable();
                    //only one row will be returned, since year and term are primary keys
                    DataRow row2 = DataTable2.Rows[0];
                    var full_payable = row2[15];//contains total of the fee structure votehead
                        
                    //evaluates the total of each term against the total amount paid by the student in entirety
                    //and subtracts the amount paid until the term where there is no full payment of the term's total
                    //then calls a the function to insert that remaining amount in that term and year's record.
                    if (sum > Convert.ToInt32(full_payable))
                    {
                        sum -= Convert.ToInt32(full_payable);
                    }

                    else
                    {
                        InsertDb(sum, count2, count3);
                        sum = 0;
                        //EEEEEWWWW!!!! I hate to do this but C# leaves me no choice
                        goto breakloop;
                    }
                }
            
            }
        breakloop: ;
        }
        

        //inserts the amount paid into the payment_details table within each individual breakdown of the voteheads
        private void InsertDb(int sum, int count2, int count3)
        {
            MessageBox.Show("sum in insertdb:" + sum.ToString());
            int form = count2;
            int term = count3;
            financeappDataSet fdset = new financeappDataSet();
            financeappDataSetTableAdapters.fees_structureTableAdapter pdadapter = new financeappDataSetTableAdapters.fees_structureTableAdapter();
            pdadapter.Fill(fdset.fees_structure);
            var sqlwhere = "form=" + form + " and term=" + term;
            DataTable DataTable = fdset.fees_structure.Select(sqlwhere).CopyToDataTable();
            int no_rows = DataTable.Rows.Count;
            DataRow row = DataTable.Rows[0];

            //voteheads
            int bes = Convert.ToInt32(row["bes"].ToString());
            int rmi = Convert.ToInt32(row["rmi"].ToString());
            int ltt = Convert.ToInt32(row["ltt"].ToString());
            int adm = Convert.ToInt32(row["adm"].ToString());
            int ewc = Convert.ToInt32(row["ewc"].ToString());
            int medical = Convert.ToInt32(row["medical"].ToString());
            int pe = Convert.ToInt32(row["pe"].ToString());
            int bog = Convert.ToInt32(row["bog"].ToString());
            int pta = Convert.ToInt32(row["pta"].ToString());
            int activity = Convert.ToInt32(row["activity"].ToString());
            int mentorship = Convert.ToInt32(row["mentorship"].ToString());
            int caution_money = Convert.ToInt32(row["caution_money"].ToString());
            int district_mock = Convert.ToInt32(row["district_mock"].ToString());
            MessageBox.Show(bes.ToString());

            //votehead breakdown payment in payment_details relation
            financeappDataSet fdset2 = new financeappDataSet();
            financeappDataSetTableAdapters.payment_detailsTableAdapter fpayadapter = new financeappDataSetTableAdapters.payment_detailsTableAdapter();
            fpayadapter.Fill(fdset2.payment_details);

            //query the payment_details relation to get the payment for the admission number set
            var sqlwhere2 = "admission_no=" + Convert.ToInt32(admNo)+ " and form="+form+" and term="+term;
            try
            {
                DataTable dtable = fdset2.payment_details.Select(sqlwhere2).CopyToDataTable();
                DataRow row1 = dtable.Rows[0];
                int num_rows = dtable.Rows.Count;
                MessageBox.Show("num rows" + num_rows.ToString()+" bes"+row1["bes"]);
            }
            catch
            {

            }
            //divide the sum among the various voteheads for the particular term
            //each form has a particular order for dividing the sum among the voteheads
            if(form==1){
                if(sum>Convert.ToInt32(row["caution_money"])){

                }
            }
            else if (form == 2)
            {

            }
            else if (form == 3)
            {

            }
            else if (form == 4)
            {

            }
            
        }

    }
}
