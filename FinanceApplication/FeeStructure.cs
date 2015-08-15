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
        private string _selectedForm;
        private string _selectedTerm;
        public Dictionary<string, string> InitializeClass(string selectedForm, string selectedTerm)
        {
            // TODO: Complete member initialization
            this._selectedForm = selectedForm;
            this._selectedTerm = selectedTerm;
            return Add(selectedForm, selectedTerm);
        }

        private string _bes;

        public string Bes
        {
            get { return _bes; }
            set { _bes = value; }
        }
        private string _rmi;

        public string Rmi
        {
            get { return _rmi; }
            set { _rmi = value; }
        }
        private string _ltt;
        
        public string Ltt
        {
            get { return _ltt; }
            set { _ltt = value; }
        }
        private string _adm;

        public string Adm
        {
            get { return _adm; }
            set { _adm = value; }
        }
        private string _ewc;

        public string Ewc
        {
            get { return _ewc; }
            set { _ewc = value; }
        }
        private string _medical;

        public string Medical
        {
            get { return _medical; }
            set { _medical = value; }
        }
        private string _pe;

        public string Pe
        {
            get { return _pe; }
            set { _pe = value; }
        }
        private string _bog;

        public string Bog
        {
            get { return _bog; }
            set { _bog = value; }
        }
        private string _pta;

        public string Pta
        {
            get { return _pta; }
            set { _pta = value; }
        }
        private string _activity;

        public string Activity
        {
            get { return _activity; }
            set { _activity = value; }
        }
        private string _mentorship;
        
       

        public string Mentorship
        {
            get { return _mentorship; }
            set { _mentorship = value; }
        }
        

        public Dictionary<string,string> Add(string form, string term)
        {
            var fdset = new financeappDataSet();
            var fadapt = new financeappDataSetTableAdapters.fee_structureTableAdapter();
            fadapt.Fill(fdset.fee_structure);
            var columncount = fdset.fee_structure.Columns.Count;
            var rowcount = fdset.fee_structure.Rows.Count;
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
