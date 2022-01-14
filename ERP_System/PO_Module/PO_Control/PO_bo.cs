using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace ERP_System.PO_Module.PO_Control
{
    public class PO_bo
    {
        PO_dto dtoresult = new PO_dto();
        PO_da ProcessData = new PO_da();

        #region Login

        public PO_dto DisplayAllUser()
        {
            try
            {
                dtoresult = ProcessData.SelectAllUser();
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region PO Entry

        #region Display Data
        public PO_dto DisplayCompanyCode_Vendor(string com)
        {
            try
            {
                dtoresult = ProcessData.SelectAllCompanyCode_Vendor(com);
                return dtoresult;
            }
            catch (Exception ex) 
            {

                throw ex;
            }
        }

        public PO_dto DisplayVendor_Type(string com)
        {
            try
            {
                dtoresult = ProcessData.SelectAllVendor_Type(com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PO_dto DisplayVendor_Code(string type,string com)
        {
            try
            {
                dtoresult = ProcessData.SelectVendorCode_Type(type,com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PO_dto DisplayVendor_Info(string vencode, string com)
        {
            try
            {
                dtoresult = ProcessData.SelectVendor_Info(com, vencode);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PO_dto DisplayPur_Term(string com,string ven_code )
        {
            try
            {
                dtoresult = ProcessData.SelectPurchase_Term(com,ven_code);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PO_dto DisplayCatalogNo()
        {
            try
            {
                dtoresult = ProcessData.SelectCatalogNo();
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PO_dto DisplayCatalogInfo_ctlno(string catalogno)
        {
            try
            {
                dtoresult = ProcessData.SelectCatalogInfo_ctlno(catalogno);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PO_dto DisplayUnitSellPrice(string catalogno)
        {
            try
            {
                dtoresult = ProcessData.SelectUnit_Sell_Price(catalogno);
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public PO_dto DisplayUOM()
        {
            try
            {
                dtoresult = ProcessData.SelectUOM();
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        public PO_dto CheckPO_DataNull(string catalog_no, string desc, string ex_desc, string qty, string uom, string price, string total)
        {
            try
            {
                if (string.IsNullOrEmpty(catalog_no))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Catalog no cannot empty.";
                    return dtoresult;
                }
                if (string.IsNullOrEmpty(desc))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Description cannot empty.";
                    return dtoresult;
                }
                if (string.IsNullOrEmpty(ex_desc))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Extra Description cannot empty.";
                    return dtoresult;
                }
                if (string.IsNullOrEmpty(qty))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Quantity cannot empty.";
                    return dtoresult;
                }
                //if (string.IsNullOrEmpty(uom))
                //{
                //    dtoresult.sts = false;
                //    dtoresult.Message = "UOM cannot empty.";
                //    return dtoresult;
                //}
                if (string.IsNullOrEmpty(price))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Unit price cannot empty.";
                    return dtoresult;
                }
                if (string.IsNullOrEmpty(total))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Total cannot empty.";
                    return dtoresult;
                }
                if (qty == "0.00")
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Order qty cannot empty.";
                    return dtoresult;
                }
                dtoresult.sts = true;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PO_dto InsertPO(string com, string ven_type, string ven_code,string ven_name, string order_dt, string ven_contact, string term_code, string pur_term, string place, string rmk,DataTable dtitem,string usn,string exist_po)
        {
            try
            {

                //get running no
                string po_no;
                po_no = GetPO_No();
                //check data null

                if (string.IsNullOrEmpty(ven_code))
                {
                    dtoresult.Message = "Vendor code cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(place))
                {
                    dtoresult.Message = "Place to delivery cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(ven_name))
                {
                    dtoresult.Message = "Vendor name cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(ven_type))
                {
                    dtoresult.Message = "Vendor type cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(order_dt))
                {
                    dtoresult.Message = "Order Date cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }


                if (string.IsNullOrEmpty(ven_contact))
                {
                    dtoresult.Message = "Vendor Contact cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                //check if user continue click save button after save
                if (!string.IsNullOrEmpty(exist_po))
                {
                    dtoresult.Message = "Please click new button to submit new po.";
                    dtoresult.sts = false;
                    return dtoresult;
                }
                if (dtitem.Rows.Count > 0)
                {
                    dtoresult = ProcessData.InsertPOData(com, po_no, ven_type, ven_code, ven_name, order_dt, ven_contact, term_code, pur_term, place, rmk, dtitem, usn);
                    dtoresult.po_no = po_no;
                }
                else
                {
                    dtoresult.Message = "PO details cannot empty.";
                }
                
               
            
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string GetPO_No()
        {
            try
            {

                int rn;
                string po_no,year,month;      
                year = DateTime.Now.ToString("yy");
                month = DateTime.Now.ToString("MM");
                po_no = "PO" + year + month;
                dtoresult = ProcessData.SelectPO_RN(po_no);
                if (dtoresult.dtrn.Rows.Count > 0)
                {
                    rn = Convert.ToInt32(dtoresult.dtrn.Rows[0]["po_no"]);
                    rn = rn + 1;
                  
                }
                else
                {
                    rn = 1;
                }
                po_no = po_no + rn.ToString("D3");
                return po_no;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region PO Approval

        public PO_dto DisplayOpenPO_Vendor()
        {
            try
            {
                dtoresult = ProcessData.SelectOpenPO_VendorName();
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PO_dto DisplayOpenPO(string po_no,string from,string to,string vendor)
        {
            try
            {
                string values,po_query,date_query,vendor_query;
                values = "";
                po_query = "";
                date_query = "";
                vendor_query = "";
                //generate search condition
                if (!string.IsNullOrEmpty(po_no))
                {
                    po_query = " and po_no ='" + po_no + "'";
                }
                if (!string.IsNullOrEmpty(from))
                {
                    if (!string.IsNullOrEmpty(to))
                    {
                        date_query = " and order_date >='" + from + "' and order_date <= '"+to+"'";
                    }
                  
                }
                if (!string.IsNullOrEmpty(vendor))
                {
                    vendor_query = " and ven_code = '" + vendor + "'";
                }

                values = po_query + date_query + vendor_query;

                dtoresult = ProcessData.SelectOpenPO(values);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PO_dto DisplayOpenPo_Details(string po)
        {
            try
            {
                dtoresult = ProcessData.SelectPODetails(po);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PO_dto UpdatePOApproval(DataTable dtpo)
        {
            try
            {
                if (dtpo.Rows.Count > 0)
                {
                    dtoresult = ProcessData.UpdatePO_sts(dtpo);
                }
                else
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "No data updated.";
                }
            
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region PO Edit

        public PO_dto DisplayAllVenCode(string com)
        {
            try
            {
                dtoresult = ProcessData.SelectAllVenCode(com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PO_dto DisplayOpenPO_byOrderDate(string com, string order_date)
        {
            try
            {
                dtoresult = ProcessData.SelectOpenPONumber(com, order_date);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        public PO_dto DisplaySelectedPOHdr(string po_no)
        {
            try
            {
                dtoresult = ProcessData.SelectPOHeader(po_no);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PO_dto DisplaySelectedPODtl(string po_no)
        {
            try
            {
                dtoresult = ProcessData.SelectPODetails(po_no);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PO_dto UpdatePOHeader(string com,string po_no, string ven_type, string ven_code, string ven_name, string order_dt, string ven_contact, string term_code, string pur_term, string place, string rmk, string usn)
        {
            try
            {
                //check data null
                if (string.IsNullOrEmpty(ven_type))
                {
                    dtoresult.Message = "Vendor type cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(ven_code))
                {
                    dtoresult.Message = "Vendor code cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(pur_term))
                {
                    dtoresult.Message = "Purchase term cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(term_code))
                {
                    dtoresult.Message = "Term code cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(order_dt))
                {
                    dtoresult.Message = "Order Date cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }
                if (string.IsNullOrEmpty(ven_contact))
                {
                    dtoresult.Message = "Vendor Contact cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(place))
                {
                    dtoresult.Message = "Place cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(ven_name))
                {
                    dtoresult.Message = "Vendor name cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                //check if user continue click save button after save
                if (string.IsNullOrEmpty(po_no))
                {
                    dtoresult.Message = "Please select a PO to edit.";
                    dtoresult.sts = false;
                    return dtoresult;
                }
               
                    dtoresult = ProcessData.UpdatePOHdr(com, po_no, ven_type, ven_code, ven_name, order_dt, ven_contact, term_code, pur_term, place, rmk, usn);
                    dtoresult.po_no = po_no;
             
                    dtoresult.Message = "PO header update successful.";
             


                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PO_dto UpdatePODetails(string po_no, string catalog_no, string dsc, string ex_dsc, string uom, string qty, string unit_price, string total, string usn)
        {
            try
            {
                //check same po no same catalog
                dtoresult = ProcessData.SelectSamePODtlCatalog(po_no, catalog_no);
                if (dtoresult.dtcheck.Rows.Count > 0)
                {
                    dtoresult.Message = "This catalog no already existed in this po no.";
                    dtoresult.sts = false;
                    return dtoresult;
                }
                dtoresult = ProcessData.UpdatePODtl(po_no, catalog_no, dsc, ex_dsc, uom, qty, unit_price, total, usn);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PO_dto DeletePODetails(string po_no, string id)
        {
            try
            {
                //check left how many po catalog before delete 
                //not allow to delete if there is no more po catalog after delete selected catalog
                dtoresult = ProcessData.SelectCountPODtl(po_no);
                if (dtoresult.dtcheck.Rows.Count > 0)
                {
                    if (dtoresult.dtcheck.Rows.Count > 1)
                    {
                        //delete catalog
                        dtoresult = ProcessData.DeleteSelectedPODtl(po_no, id);

                    }
                    else
                    {
                        dtoresult.Message = "PO details cannot empty after delete this catalog no.";
                        dtoresult.sts = false;
                      
                    }

                }
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PO_dto AddNewPODetails(string po_no, string catalog_no, string dsc, string ex_dsc, string uom, string qty, string unit_price, string total, string usn)
        {
            try
            {
                //check same po no same catalog
                if (string.IsNullOrEmpty(po_no))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "PO number cannot empty.";
                    return dtoresult;
                }
                dtoresult = ProcessData.SelectSamePODtlCatalog(po_no, catalog_no);
                if (dtoresult.dtcheck.Rows.Count > 0)
                {
                    dtoresult.Message = "This catalog no already existed in this po no.";
                    dtoresult.sts = false;
                    return dtoresult;
                }
                dtoresult = ProcessData.AddNewPODtl(po_no, catalog_no, dsc, ex_dsc, uom, qty, unit_price, total, usn);
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