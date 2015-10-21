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

        public string Bes { get; set; }

        public string Rmi { get; set; }

        public string Ltt { get; set; }

        public string Adm { get; set; }

        public string Ewc { get; set; }

        public string Medical { get; set; }

        public string Pe { get; set; }

        public string Bog { get; set; }

        public string Pta { get; set; }

        public string Activity { get; set; }


        public string Mentorship { get; set; }


        public Dictionary<string,string> Add(string form, string term)
        {
            var fdset = new financeapplicationDataSet();
            var fadapt = new financeapplicationDataSetTableAdapters.fee_structureTableAdapter();
            fadapt.Fill(fdset.fee_structure);
            var columncount = fdset.fee_structure.Columns.Count;
            var rowcount = fdset.fee_structure.Rows.Count;
            var feesitems = new Dictionary<string, string>();

            foreach (DataRow row in fdset.fee_structure.Rows)
            {
                var count = 0;

                if ((row["form"].ToString() != form) || (row["term"].ToString() != term)) continue;
                while (count < columncount)
                {
                    feesitems[fdset.fee_structure.Columns[count].ToString()] = Convert.ToString(row[fdset.fee_structure.Columns[count]]);
                    count++;                        
                }
            }
            
       
            return feesitems;
            
           
        }



        
    }
}
