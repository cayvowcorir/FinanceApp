using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using Telerik.Windows.Documents.Flow.Model;

namespace FinanceApplication
{
    internal class Payment
    {
        public String AdmNo { get; set; }
        public String Purpose { get; set; }
        public String Type { get; set; }
        public int Amount { get; set; }
        public string SlipCheckNo { get; set; }

        public void MakePayment()
        {
            var fdataset = new financeapplicationDataSet();
            var psummaryadapter =
                new financeapplicationDataSetTableAdapters.payment_summaryTableAdapter();
            var fstructureadapter =
                new financeapplicationDataSetTableAdapters.fee_structureTableAdapter();
            var pdetailsadapter =
                new financeapplicationDataSetTableAdapters.payment_detailsTableAdapter();

            fstructureadapter.Fill(fdataset.fee_structure);
            psummaryadapter.Fill(fdataset.payment_summary);
            pdetailsadapter.Fill(fdataset.payment_details);

            //to add the payment to the payment summary table
            switch (Type)
            {
                case "Check":
                    try
                    {
                        psummaryadapter.Insert(Convert.ToInt32(AdmNo), Type, SlipCheckNo, Purpose, null,
                            Amount.ToString());
                        MessageBox.Show("The Entry has been added to the database");
                        var str = new StudentPaymentRecord();
                        str.PageClear();
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(
                            "There was an Error while trying to write to the database. Please try again. Error Message: " +
                            e1.Message);
                    }
                    break;
                case "Bank Slip":
                    try
                    {
                        psummaryadapter.Insert(Convert.ToInt32(AdmNo), Type, null, Purpose, SlipCheckNo,
                            Amount.ToString());
                        MessageBox.Show("The Entry has been added to the database");
                        var str = new StudentPaymentRecord();
                        str.PageClear();
                    }
                    catch (Exception e2)
                    {
                        MessageBox.Show(
                            "There was an Error while trying to write to the database. Please try again.Error Message: " +
                            e2.Message);
                    }
                    break;

                default:
                    try
                    {
                        psummaryadapter.Insert(Convert.ToInt32(AdmNo), Type, null, Purpose, null, Amount.ToString());
                        MessageBox.Show("The Entry has been added to the database");
                        var str = new StudentPaymentRecord();
                        str.PageClear();
                    }
                    catch (Exception e2)
                    {
                        MessageBox.Show(
                            "There was an Error while trying to write to the database. Please try again.Error Message: " +
                            e2.Message);
                    }
                    break;
            }


            //compare the total paid by the particular student with each year's total in the fee structure
            for (var count1 = 1; count1 < 5; count1++)
            {
                for (var count2 = 1; count2 < 4; count2++)
                {
                    var whereFormTerm = "form=" + count1 + "and term=" + count2;
                    var whereAdm = "admission_no=" + Convert.ToInt32(AdmNo) + "and form=" + count1 + "and term=" +
                                   count2;

                    var fstructureDataTable = fdataset.fee_structure.Select(whereFormTerm).CopyToDataTable();
                    var fstructureDataRow = fstructureDataTable.Rows[0]; //since only one datarow will be returned
                    var totalPayable = fstructureDataRow["total"]; //contains total of the fee structure votehead

                    //specific payment details for the particular student 
                    var paydetailsDataTable = fdataset.payment_details.Select(whereAdm).CopyToDataTable();
                    var paydetailsDataRow = paydetailsDataTable.Rows[0];

                    //check if the value in the payment_details relation is equal to the corresponding value in the fee_structure relation


                    try
                    {
                        pdetailsadapter.Fill(fdataset.payment_details);
                        var pdetailsDataTable = fdataset.payment_details.Select(whereAdm).CopyToDataTable();
                        var pdetailsDataRow = pdetailsDataTable.Rows[0];



                        if (count1 == 1) //for form 1
                        {
                            //select values of each votehead from fee structure
                            var voteheads = new Dictionary<int, string>();
                            voteheads[2] = fstructureDataRow["bes"].ToString();
                            voteheads[9] = fstructureDataRow["rmi"].ToString();
                            voteheads[8] = fstructureDataRow["ltt"].ToString();
                            voteheads[7] = fstructureDataRow["adm"].ToString();
                            voteheads[11] = fstructureDataRow["ewc"].ToString();
                            voteheads[6] = fstructureDataRow["medical"].ToString();
                            voteheads[5] = fstructureDataRow["pe"].ToString();
                            voteheads[4] = fstructureDataRow["bog"].ToString();
                            voteheads[1] = fstructureDataRow["pta"].ToString();
                            voteheads[3] = fstructureDataRow["activity"].ToString();
                            voteheads[10] = fstructureDataRow["mentorship"].ToString();
                            voteheads[0] = fstructureDataRow["caution_money"].ToString();
                            voteheads[12] = fstructureDataRow["district_mock"].ToString();

                            //votehead names stored in a systematic manner in an array so as to ensure systematic deduction
                            //of any money paid e.g, caution money should be deducted before pta
                            string[] voteheadNames =
                            {
                                "caution_money", "pta", "bes", "activity", "bog", "pe", "medical", "adm", "ltt",
                                "rmi", "mentorship", "ewc", "district_mock"
                            };

                            //loop to feed in the values into the database
                            var j = 0;
                            while (Amount > 0 && j < 13)
                            {

                                if (Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]) <
                                    Convert.ToInt32(voteheads[j]) &&
                                    (Amount >= Convert.ToInt32(voteheads[j])))
                                {
                                    Amount = Amount -
                                             (Convert.ToInt32(voteheads[j]) -
                                              Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]));
                                    pdetailsDataRow[voteheadNames[j]] = voteheads[j];

                                }

                                else if ((Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]) <
                                          Convert.ToInt32(voteheads[j]) &&
                                          Amount >
                                          (Convert.ToInt32(voteheads[j]) -
                                           Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]))))
                                {
                                    Amount -= Convert.ToInt32(voteheads[j]) -
                                              Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]);
                                    pdetailsDataRow[voteheadNames[j]] =
                                        pdetailsDataRow[voteheadNames[j]] = voteheads[j];


                                }
                                else if (Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]) <
                                         Convert.ToInt32(voteheads[j]) &&
                                         ((Amount + Convert.ToInt32(pdetailsDataRow[voteheadNames[j]])) <=
                                          Convert.ToInt32(voteheads[j])))
                                {

                                    pdetailsDataRow[voteheadNames[j]] =
                                        Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]) + Amount;
                                    Amount = 0;

                                }


                                pdetailsadapter.Update(pdetailsDataRow);
                                j++;
                            }


                        }

                        else if (count1 == 2) //for form 2
                        {
                            //select values of each votehead from fee structure
                            var voteheads = new Dictionary<int, string>();
                            voteheads[1] = fstructureDataRow["bes"].ToString();
                            voteheads[8] = fstructureDataRow["rmi"].ToString();
                            voteheads[7] = fstructureDataRow["ltt"].ToString();
                            voteheads[6] = fstructureDataRow["adm"].ToString();
                            voteheads[10] = fstructureDataRow["ewc"].ToString();
                            voteheads[5] = fstructureDataRow["medical"].ToString();
                            voteheads[4] = fstructureDataRow["pe"].ToString();
                            voteheads[3] = fstructureDataRow["bog"].ToString();
                            voteheads[0] = fstructureDataRow["pta"].ToString();
                            voteheads[2] = fstructureDataRow["activity"].ToString();
                            voteheads[9] = fstructureDataRow["mentorship"].ToString();
                            voteheads[11] = fstructureDataRow["caution_money"].ToString();
                            voteheads[12] = fstructureDataRow["district_mock"].ToString();

                            //votehead names stored in a systematic manner in an array so as to ensure systematic deduction
                            //of any money paid e.g, caution money should be deducted before pta
                            string[] voteheadNames =
                            {
                                "pta", "bes", "activity", "bog", "pe", "medical", "adm", "ltt",
                                "rmi", "mentorship", "ewc", "caution_money", "district_mock"
                            };

                            //loop to feed in the values into the database
                            var j = 0;
                            while (Amount > 0 && j < 13)
                            {

                                if (Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]) <
                                    Convert.ToInt32(voteheads[j]) &&
                                    (Amount >= Convert.ToInt32(voteheads[j])))
                                {
                                    Amount = Amount -
                                             (Convert.ToInt32(voteheads[j]) -
                                              Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]));
                                    pdetailsDataRow[voteheadNames[j]] = voteheads[j];

                                }

                                else if ((Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]) <
                                          Convert.ToInt32(voteheads[j]) &&
                                          Amount >
                                          (Convert.ToInt32(voteheads[j]) -
                                           Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]))))
                                {
                                    Amount -= Convert.ToInt32(voteheads[j]) -
                                              Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]);
                                    pdetailsDataRow[voteheadNames[j]] =
                                        pdetailsDataRow[voteheadNames[j]] = voteheads[j];


                                }
                                else if (Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]) <
                                         Convert.ToInt32(voteheads[j]) &&
                                         ((Amount + Convert.ToInt32(pdetailsDataRow[voteheadNames[j]])) <=
                                          Convert.ToInt32(voteheads[j])))
                                {

                                    pdetailsDataRow[voteheadNames[j]] =
                                        Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]) + Amount;
                                    Amount = 0;

                                }


                                pdetailsadapter.Update(pdetailsDataRow);
                                j++;
                            }


                        }

                        if (count1 == 3) //for form 3
                        {

                            //select values of each votehead from fee structure
                            var voteheads = new Dictionary<int, string>();
                            voteheads[1] = fstructureDataRow["bes"].ToString();
                            voteheads[8] = fstructureDataRow["rmi"].ToString();
                            voteheads[7] = fstructureDataRow["ltt"].ToString();
                            voteheads[6] = fstructureDataRow["adm"].ToString();
                            voteheads[10] = fstructureDataRow["ewc"].ToString();
                            voteheads[5] = fstructureDataRow["medical"].ToString();
                            voteheads[4] = fstructureDataRow["pe"].ToString();
                            voteheads[3] = fstructureDataRow["bog"].ToString();
                            voteheads[0] = fstructureDataRow["pta"].ToString();
                            voteheads[2] = fstructureDataRow["activity"].ToString();
                            voteheads[9] = fstructureDataRow["mentorship"].ToString();
                            voteheads[11] = fstructureDataRow["caution_money"].ToString();
                            voteheads[12] = fstructureDataRow["district_mock"].ToString();

                            //votehead names stored in a systematic manner in an array so as to ensure systematic deduction
                            //of any money paid e.g, caution money should be deducted before pta
                            string[] voteheadNames =
                            {
                                "pta", "bes", "activity", "bog", "pe", "medical", "adm", "ltt",
                                "rmi", "mentorship", "ewc", "caution_money", "district_mock"
                            };

                            //loop to feed in the values into the database
                            var j = 0;
                            while (Amount > 0 && j < 13)
                            {

                                if (Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]) <
                                    Convert.ToInt32(voteheads[j]) &&
                                    (Amount >= Convert.ToInt32(voteheads[j])))
                                {
                                    Amount = Amount -
                                             (Convert.ToInt32(voteheads[j]) -
                                              Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]));
                                    pdetailsDataRow[voteheadNames[j]] = voteheads[j];

                                }

                                else if ((Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]) <
                                          Convert.ToInt32(voteheads[j]) &&
                                          Amount >
                                          (Convert.ToInt32(voteheads[j]) -
                                           Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]))))
                                {
                                    Amount -= Convert.ToInt32(voteheads[j]) -
                                              Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]);
                                    pdetailsDataRow[voteheadNames[j]] =
                                        pdetailsDataRow[voteheadNames[j]] = voteheads[j];


                                }
                                else if (Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]) <
                                         Convert.ToInt32(voteheads[j]) &&
                                         ((Amount + Convert.ToInt32(pdetailsDataRow[voteheadNames[j]])) <=
                                          Convert.ToInt32(voteheads[j])))
                                {

                                    pdetailsDataRow[voteheadNames[j]] =
                                        Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]) + Amount;
                                    Amount = 0;

                                }


                                pdetailsadapter.Update(pdetailsDataRow);
                                j++;
                            }


                        }

                        if (count1 == 4) //for form 4
                        {
                            //select values of each votehead from fee structure
                            var voteheads = new Dictionary<int, string>();
                            voteheads[1] = fstructureDataRow["bes"].ToString();
                            voteheads[8] = fstructureDataRow["rmi"].ToString();
                            voteheads[7] = fstructureDataRow["ltt"].ToString();
                            voteheads[6] = fstructureDataRow["adm"].ToString();
                            voteheads[11] = fstructureDataRow["ewc"].ToString();
                            voteheads[5] = fstructureDataRow["medical"].ToString();
                            voteheads[4] = fstructureDataRow["pe"].ToString();
                            voteheads[3] = fstructureDataRow["bog"].ToString();
                            voteheads[0] = fstructureDataRow["pta"].ToString();
                            voteheads[2] = fstructureDataRow["activity"].ToString();
                            voteheads[9] = fstructureDataRow["mentorship"].ToString();
                            voteheads[12] = fstructureDataRow["caution_money"].ToString();
                            voteheads[10] = fstructureDataRow["district_mock"].ToString();

                            //votehead names stored in a systematic manner in an array so as to ensure systematic deduction
                            //of any money paid e.g, caution money should be deducted before pta
                            string[] voteheadNames =
                            {
                                "pta", "bes", "activity", "bog", "pe", "medical", "adm", "ltt",
                                "rmi", "mentorship", "district_mock", "ewc", "caution_money"
                            };

                            //loop to feed in the values into the database
                            var j = 0;
                            while (Amount > 0 && j < 13)
                            {

                                if (Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]) <
                                    Convert.ToInt32(voteheads[j]) &&
                                    (Amount >= Convert.ToInt32(voteheads[j])))
                                {
                                    Amount = Amount -
                                             (Convert.ToInt32(voteheads[j]) -
                                              Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]));
                                    pdetailsDataRow[voteheadNames[j]] = voteheads[j];

                                }

                                else if ((Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]) <
                                          Convert.ToInt32(voteheads[j]) &&
                                          Amount >
                                          (Convert.ToInt32(voteheads[j]) -
                                           Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]))))
                                {
                                    Amount -= Convert.ToInt32(voteheads[j]) -
                                              Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]);
                                    pdetailsDataRow[voteheadNames[j]] =
                                        pdetailsDataRow[voteheadNames[j]] = voteheads[j];


                                }
                                else if (Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]) <
                                         Convert.ToInt32(voteheads[j]) &&
                                         ((Amount + Convert.ToInt32(pdetailsDataRow[voteheadNames[j]])) <=
                                          Convert.ToInt32(voteheads[j])))
                                {

                                    pdetailsDataRow[voteheadNames[j]] =
                                        Convert.ToInt32(pdetailsDataRow[voteheadNames[j]]) + Amount;
                                    Amount = 0;

                                }


                                pdetailsadapter.Update(pdetailsDataRow);
                                j++;
                            }


                        }
                    }
                    catch
                    {

                    }

                }
            }
        }


    }
}