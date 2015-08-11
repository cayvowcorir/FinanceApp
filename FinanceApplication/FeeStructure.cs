using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FinanceApplication
{
    class FeeStructure
    {
        private string selected_form;
        private string selected_term;
        public Dictionary<string, string> InitializeClass(string selected_form, string selected_term)
        {
            // TODO: Complete member initialization
            this.selected_form = selected_form;
            this.selected_term = selected_term;
            return Add(selected_form, selected_term);
        }

        private string bes;

        public string Bes
        {
            get { return bes; }
            set { bes = value; }
        }
        private string rmi;

        public string Rmi
        {
            get { return rmi; }
            set { rmi = value; }
        }
        private string ltt;
        
        public string Ltt
        {
            get { return ltt; }
            set { ltt = value; }
        }
        private string adm;

        public string Adm
        {
            get { return adm; }
            set { adm = value; }
        }
        private string ewc;

        public string Ewc
        {
            get { return ewc; }
            set { ewc = value; }
        }
        private string medical;

        public string Medical
        {
            get { return medical; }
            set { medical = value; }
        }
        private string pe;

        public string Pe
        {
            get { return pe; }
            set { pe = value; }
        }
        private string bog;

        public string Bog
        {
            get { return bog; }
            set { bog = value; }
        }
        private string pta;

        public string Pta
        {
            get { return pta; }
            set { pta = value; }
        }
        private string activity;

        public string Activity
        {
            get { return activity; }
            set { activity = value; }
        }
        private string mentorship;
        
       

        public string Mentorship
        {
            get { return mentorship; }
            set { mentorship = value; }
        }
        

        public Dictionary<string,string> Add(string form, string term)
        {
            financeappDataSet fdset = new financeappDataSet();
            financeappDataSetTableAdapters.fee_structureTableAdapter fadapt = new financeappDataSetTableAdapters.fee_structureTableAdapter();
            fadapt.Fill(fdset.fee_structure);
            int columncount = fdset.fee_structure.Columns.Count;
            int rowcount = fdset.fee_structure.Rows.Count;
            var feesitems = new Dictionary<string, string>();

            foreach (DataRow row in fdset.fee_structure.Rows)
            {
                var count = 0;
                
                if ((row["form"].ToString() == form) && (row["term"].ToString()==term))
                {
                    while (count < columncount)
                    {
                        feesitems[fdset.fee_structure.Columns[count].ToString()] = Convert.ToString(row[fdset.fee_structure.Columns[count]]);
                        count++;                        
                    }
                    
                }
                         
            }
            
       
            return feesitems;
            
           
        }



        
    }
}
