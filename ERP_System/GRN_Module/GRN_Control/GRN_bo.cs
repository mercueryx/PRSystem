using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

namespace ERP_System.GRN_Module.GRN_Control
{
    public class GRN_bo
    {
        GRN_dto dtoresult = new GRN_dto();
        GRN_da Process_Data = new GRN_da();

        #region GRN Entry

        public GRN_dto DisplayApprovePOVendor(string com)
        {
            try
            {
                dtoresult = Process_Data.SelectApprovePOVendor(com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto DisplayApprovePO_VenCode(string ven)
        {
            try
            {
                dtoresult = Process_Data.SelectApprovePO_VenCode(ven);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto DisplayPOHeader(string po)
        {
            try
            {
                dtoresult = Process_Data.SelectPOHeader_po(po);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto DisplayPODetails(string po)
        {
            try
            {
                dtoresult = Process_Data.SelectPODetails_po(po);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto CheckReceiveQty(string rec_qty, string bal_qty,string o_qty)
        {
            try
            {
                decimal dec_recqty=0, dec_balqty=0,dec_oqty=0,checkdecimal=0;
                int checkint =0;
                //dec_recqty = 0;
                dec_balqty = decimal.Parse(bal_qty);
                dec_oqty = decimal.Parse(o_qty);
                if (!string.IsNullOrEmpty(rec_qty))
                {
                    if (int.TryParse(rec_qty, out checkint))
                    {
                        dec_recqty = decimal.Parse(rec_qty);
                      
                    }
                    else
                    {
                        // The string was a valid integer => use result here
                        if (decimal.TryParse(rec_qty, out checkdecimal))
                        {
                            dec_recqty = decimal.Parse(rec_qty);
                           
                        }
                      
                    }
                    if (dec_recqty > dec_balqty)
                    {
                        dtoresult.Message = "Receive qty cannot more than balance qty.";
                        dtoresult.sts = false;
                        return dtoresult;
                    }
                    else if (dec_recqty > dec_oqty)
                    {
                        dtoresult.Message = "Receive qty cannot more than order qty.";
                        dtoresult.sts = false;
                        return dtoresult;
                    }
                    else if (dec_recqty == 0)
                    {
                        dtoresult.Message = "Receive qty cannot empty.";
                        dtoresult.sts = false;
                        return dtoresult;
                    }
                    else
                    {
                        dtoresult.sts = true;
                    }

                }
                else
                {
                    dtoresult.Message = "Please key in numeric value for receive qty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string GetGRN_No()
        {
            try
            {

                int rn;
                string grn_no, year, month;
                year = DateTime.Now.ToString("yy");
                month = DateTime.Now.ToString("MM");
                grn_no = "GRN" + year + month;
                dtoresult = Process_Data.SelectGRN_RN(grn_no);
                if (dtoresult.dtrn.Rows.Count > 0)
                {
                    rn = Convert.ToInt32(dtoresult.dtrn.Rows[0]["grn_no"]);
                    rn = rn + 1;

                }
                else
                {
                    rn = 1;
                }
                grn_no = grn_no + rn.ToString("D3");
                return grn_no;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto InsertData(string grn_date, string vendor, string do_no, string rmk, string usn, DataTable dtdetails,string exist_grn)
        {
            try
            {

                //get running no
                string grn_no;
                grn_no = GetGRN_No();

                //check data null
                if (string.IsNullOrEmpty(vendor))
                {
                    dtoresult.Message = "Vendor cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(grn_date))
                {
                    dtoresult.Message = "GRN date cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(do_no))
                {
                    dtoresult.Message = "DO number cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                //check if user continue click save button after save
                if (!string.IsNullOrEmpty(exist_grn))
                {
                    dtoresult.Message = "Please click new button to submit new GRN.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                dtoresult = Process_Data.InsertGRN(grn_no, grn_date, vendor, do_no, rmk, usn, dtdetails);
                dtoresult.grn_no = grn_no;

                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region GRN Approve

        public GRN_dto DisplayOpenGRN_Vendor()
        {
            try
            {
                dtoresult = Process_Data.SelectOpenGRN_Vendor();
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto DisplayOpenGRN(string grn_no, string from, string to, string vendor)
        {
            try
            {
                string values, grn_query, date_query, vendor_query;
                values = "";
                grn_query = "";
                date_query = "";
                vendor_query = "";
                //generate search condition
                if (!string.IsNullOrEmpty(grn_no))
                {
                    grn_query = " and a.grn_no ='" + grn_no + "'";
                }
                if (!string.IsNullOrEmpty(from))
                {
                    if (!string.IsNullOrEmpty(to))
                    {
                        date_query = " and a.grn_date >='" + from + "' and a.grn_date <= '" + to + "'";
                    }

                }
                if (!string.IsNullOrEmpty(vendor))
                {
                    vendor_query = " and a.vendor = '" + vendor + "'";
                }

                values = grn_query + date_query + vendor_query;

                dtoresult = Process_Data.SelectOpenGRN(values);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto DisplayOpenGRN_Details(string grn_no)
        {
            try
            {
                dtoresult = Process_Data.SelectOpenGRN_Details(grn_no);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto CheckExistApproveGRN(string grn_no)
        {
            try
            {
                dtoresult = Process_Data.SelectApprovedGRN(grn_no);
                if (dtoresult.dtcheck.Rows.Count > 0)
                {
                    dtoresult.sts = false;
                }
                else
                {
                    dtoresult.sts = true;
                }
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto UpdateGRNApproval(DataTable dtgrn,DataTable dtdetails,string usn)
        {
            try
            {

               
                //generate grn details table
                string grn_no,sts,po_no,ctlno,rec_qty;
                if (dtgrn.Rows.Count > 0)
                {
                    for (int i = 0; i < dtgrn.Rows.Count; i++)
                    {
                        grn_no = dtgrn.Rows[i]["grn_no"].ToString();
                        sts = dtgrn.Rows[i]["sts"].ToString();

                        //select grn details
                        if (sts == "APPROVE")
                        {
                            dtoresult = Process_Data.SelectGRNDetails_GRN(grn_no);
                            for (int count = 0; count < dtoresult.dtdtl.Rows.Count; count++)
                            {
                                grn_no = dtoresult.dtdtl.Rows[count]["grn_no"].ToString();
                                po_no = dtoresult.dtdtl.Rows[count]["po_no"].ToString();
                                ctlno = dtoresult.dtdtl.Rows[count]["catalog_no"].ToString();
                                rec_qty = dtoresult.dtdtl.Rows[count]["rec_qty"].ToString();
                                dtdetails.Rows.Add(grn_no, po_no, rec_qty, ctlno);
                            }
                        }

                    }
                    dtoresult = Process_Data.ApproveGRN(dtgrn, dtdetails, usn);
                }
                else
                {
                    dtoresult.Message = "No data updated.";
                    dtoresult.sts = false;
                }
               
                return dtoresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GRN Edit

        public GRN_dto DisplayOpenGRN_GRNDate(string grn_date)
        {
            try
            {
                dtoresult = Process_Data.SelectOpenGRN_GRNDate(grn_date);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto DisplayGRNHeader(string grn_no)
        {
            try
            {
                dtoresult = Process_Data.SelectGRNHeader(grn_no);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public GRN_dto DisplayGRN_PO(string grn_no)
        //{
        //    try
        //    {
        //        dtoresult = Process_Data.SelectGRNHeader(grn_no);
        //        return dtoresult;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public GRN_dto DisplayGRN_PODetails(string grn_no)
        {
            try
            {
                dtoresult = Process_Data.SelectGRN_PODetails(grn_no);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto DisplayGRN_POItemDetails(string grn_no)
        {
            try
            {
                dtoresult = Process_Data.SelectGRN_POItemDetails(grn_no);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto UpdateGRNRecQty(string grn_no, string po_no, string catalog, string rec_qty,string bal_qty,string o_qty)
        {
            try
            {
                dtoresult = CheckReceiveQty(rec_qty, bal_qty, o_qty);
                if (dtoresult.sts == false)
                {

                    return dtoresult;
                }
                else
                {
                    dtoresult = Process_Data.GenerateUpdateGRNDetailsQty(grn_no, po_no, catalog, rec_qty);
                }
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto CheckDuplicatePO(string grn, string po, string ctlno)
        {
            try
            {
                dtoresult = Process_Data.SelectDuplicatePO(grn, po, ctlno);
                if (dtoresult.dtcheck.Rows.Count > 0)
                {
                    dtoresult.Message = "This po already existed.";
                    dtoresult.sts = false;
                }
                else
                {
                    dtoresult.sts = true;
                }
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto EditGRN_AddNewPO(string grn_no,string usn, DataTable dtdetails)
        {
            try
            {
                if (dtdetails.Rows.Count > 0)
                {
                    dtoresult = Process_Data.EditGRN_AddPODetails(grn_no, usn, dtdetails);
                }
                else
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Details cannot empty.";
                    
                }
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public GRN_dto DeleteSelectedGRN_PO(string grn_no, string po_no)
        {
            try
            {
                dtoresult = Process_Data.DeleteGRN_PO(po_no, grn_no);
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto UpdateGRNHeader(string grn_no,string grn_dt, string vendor, string do_no, string rmk,string usn)
        {
            try
            {
                //check data null
                if (string.IsNullOrEmpty(grn_dt))
                {
                    dtoresult.Message = "GRN date cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(vendor))
                {
                    dtoresult.Message = "Vendor cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }


                if (string.IsNullOrEmpty(do_no))
                {
                    dtoresult.Message = "DO number cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }


                if (string.IsNullOrEmpty(rmk))
                {
                    dtoresult.Message = "Remark cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(grn_no))
                {
                    dtoresult.Message = "GRN number cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                dtoresult = Process_Data.UpdateGRNHeader(grn_no, grn_dt, vendor, do_no, rmk, usn);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
    }
}