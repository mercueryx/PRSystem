using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace ERP_System.SALES_ORDER.SALES_ORDER_Control
{
	public class DO_bo
	{
        DO_dto dtoresult = new DO_dto();
        DO_da Process_Data = new DO_da();

        #region Delivery Order Entry

        public DO_dto DisplayOrderType(string com)
        {
            try
            {
                dtoresult = Process_Data.SelectGroupCode(com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DO_dto DisplayCustomer(string group)
        {
            try
            {
                dtoresult = Process_Data.SelectCustomer(group);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DO_dto DisplayDeliveryTerm()
        {
            try
            {
                dtoresult = Process_Data.SelectDeliveryTerm();
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DO_dto DisplayPaymentTerm(string ah_code)
        {
            try
            {
                dtoresult = Process_Data.SelectPaymentTerm(ah_code);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DO_dto DisplayCertifySC(string com)
        {
            try
            {
                dtoresult = Process_Data.SelectCertifySC(com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DO_dto DisplaySC_Catalog(string com, string sc_no)
        {
            try
            {
                dtoresult = Process_Data.SelectSC_Catalog(com, sc_no);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DO_dto DisplayCatalog_dsc(string sc_no,string ctlno)
        {
            try
            {
                dtoresult = Process_Data.SelectCatalog_dsc(sc_no, ctlno);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DO_dto DisplayFOC_Catalog(string com)
        {
            try
            {
                dtoresult = Process_Data.SelectFoc_CatalogNo(com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DO_dto DisplayFOC_Catalog_dsc(string com, string ctlno)
        {
            try
            {
                dtoresult = Process_Data.SelectFOC_Catalog_dsc(com,ctlno);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DO_dto DisplayUOM()
        {
            try
            {
                dtoresult = Process_Data.SelectUOM();
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DO_dto CheckDtl_DataNull(string ctlno, string dsc, string scno, string uom, string qty, string foc_qty, string amount)
        {
            try
            {
                if (string.IsNullOrEmpty(ctlno))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Catalog no cannot empty.";
                    return dtoresult;
                }
                if (string.IsNullOrEmpty(dsc))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Description cannot empty.";
                    return dtoresult;
                }
                if (string.IsNullOrEmpty(scno))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Sales Contract no Description cannot empty.";
                    return dtoresult;
                }
                if (string.IsNullOrEmpty(qty))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Quantity cannot empty.";
                    return dtoresult;
                }
                if (string.IsNullOrEmpty(foc_qty))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "FOC Quantity cannot empty.";
                    return dtoresult;
                }
                if (string.IsNullOrEmpty(uom))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "UOM cannot empty.";
                    return dtoresult;
                }
                if (string.IsNullOrEmpty(amount))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Amount cannot empty.";
                    return dtoresult;
                }
             
                if (qty == "0.00")
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Quantity cannot empty.";
                    return dtoresult;
                }


                if (amount == "0.00")
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Amount cannot empty.";
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

        public DO_dto CheckFoc_DataNull(string ctlno, string i_dsc, string uom, string qty, string claim, string gift, string dsc, string rmk)
        {
            try
            {
                if (string.IsNullOrEmpty(ctlno))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Catalog no cannot empty.";
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(i_dsc))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Item description cannot empty.";
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(uom))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "UOM cannot empty.";
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(qty))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Quantity cannot empty.";
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

        private string GetDO_No()
        {
            try
            {

                int rn;
                string do_no, year, month;
                year = DateTime.Now.ToString("yy");
                month = DateTime.Now.ToString("MM");
                do_no = "DO" + year + month;
                dtoresult = Process_Data.SelectDO_RN(do_no);
                if (dtoresult.dtdo_rn.Rows.Count > 0)
                {
                    rn = Convert.ToInt32(dtoresult.dtdo_rn.Rows[0]["do_no"]);
                    rn = rn + 1;

                }
                else
                {
                    rn = 1;
                }
                do_no = do_no + rn.ToString("D3");
                return do_no;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string GetINV_No()
        {
            try
            {

                int rn;
                string inv_no, year, month;
                year = DateTime.Now.ToString("yy");
                month = DateTime.Now.ToString("MM");
                inv_no = "IL" + year + month;
                dtoresult = Process_Data.SelectInv_RN(inv_no);
                if (dtoresult.dtinv_rn.Rows.Count > 0)
                {
                    rn = Convert.ToInt32(dtoresult.dtinv_rn.Rows[0]["inv_no"]);
                    rn = rn + 1;

                }
                else
                {
                    rn = 1;
                }
                inv_no = inv_no + rn.ToString("D3");
                return inv_no;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DO_dto SaveDeliveryOrder(string type, string bill, string sold, string ship, string d_term, string pay_term, string d_date, string etd, string eta, string usn, DataTable dtdtl, DataTable dtfoc,string exist_do_no,string com)
        {
          
                try
                {
                    string do_rn, inv_rn;
                 
                    //check header empty field
                  

                    if (string.IsNullOrEmpty(type))
                    {
                        dtoresult.sts = false;
                        dtoresult.Message = "Order type cannot empty.";
                        return dtoresult;
                    }

                    if (string.IsNullOrEmpty(bill))
                    {
                        dtoresult.sts = false;
                        dtoresult.Message = "Bill to cannot empty.";
                        return dtoresult;
                    }

                    if (string.IsNullOrEmpty(sold))
                    {
                        dtoresult.sts = false;
                        dtoresult.Message = "Sold to cannot empty.";
                        return dtoresult;
                    }

                    if (string.IsNullOrEmpty(ship))
                    {
                        dtoresult.sts = false;
                        dtoresult.Message = "Ship to cannot empty.";
                        return dtoresult;
                    }

                    if (string.IsNullOrEmpty(d_term))
                    {
                        dtoresult.sts = false;
                        dtoresult.Message = "Delivery term cannot empty.";
                        return dtoresult;
                    }

                    if (string.IsNullOrEmpty(pay_term))
                    {
                        dtoresult.sts = false;
                        dtoresult.Message = "Payment term cannot empty.";
                        return dtoresult;
                    }

                    if (string.IsNullOrEmpty(d_date))
                    {
                        dtoresult.sts = false;
                        dtoresult.Message = "Delivery order date cannot empty.";
                        return dtoresult;
                    }

                    if (string.IsNullOrEmpty(etd))
                    {
                        dtoresult.sts = false;
                        dtoresult.Message = "ETD cannot empty.";
                        return dtoresult;
                    }

                    if (string.IsNullOrEmpty(eta))
                    {
                        dtoresult.sts = false;
                        dtoresult.Message = "ETA cannot empty.";
                        return dtoresult;
                    }

                    do_rn = GetDO_No();
                    dtoresult.do_no = do_rn;

                    inv_rn = GetINV_No();
                    dtoresult.inv_no = inv_rn;
                    //check delivery details for each catalog

                    //check if user continue click save button after save
                    if (!string.IsNullOrEmpty(exist_do_no))
                    {
                        dtoresult.Message = "Please click new button to submit new delivery order.";
                        dtoresult.sts = false;
                        return dtoresult;
                    }

                    if (dtdtl.Rows.Count == 0)
                    {
                        dtoresult.Message = "Delivery order details cannot empty.";
                        dtoresult.sts = false;
                        return dtoresult;
                    }

                
                    dtoresult = Process_Data.InsertDeliveryDO(do_rn,inv_rn,type,bill,sold,ship,d_term,pay_term,d_date,etd,eta,usn,dtdtl,dtfoc,com);
                    return dtoresult;


                }
                catch (Exception ex)
                {

                    throw ex;
                }
       
        }
        #endregion

        #region Delivery Order Certify

        public DO_dto DisplayOpenDO(string com)
        {
            try
            {
                dtoresult = Process_Data.SelectOpenDO(com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DO_dto DisplayOpenInv(string com)
        {
            try
            {
                dtoresult = Process_Data.SelectOpenInvNo(com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DO_dto DisplayDOHeader(string com, string do_no, string inv_no, string d_dt_from, string d_dt_to, string sold,string ship,string type)
        {
            try
            {
                string keyword;
                keyword = "";

                com = " a.com='" + com + "'";

                if (string.IsNullOrEmpty(type))
                {
                    type = "";
                }
                else
                {
                    type = " and a.order_type ='" + type + "'";
                }

                if (string.IsNullOrEmpty(do_no))
                {
                    do_no = "";
                }
                else
                {
                    do_no = " and a.do_no ='" + do_no + "'";
                }

                if (string.IsNullOrEmpty(inv_no))
                {
                    inv_no = "";
                }
                else
                {
                    inv_no = " and a.inv_no ='" + inv_no + "'";
                }

                if (string.IsNullOrEmpty(sold))
                {
                    sold = "";

                }
                else
                {
                    sold = " and a.sold_to ='" + sold + "'";

                }

                if (string.IsNullOrEmpty(ship))
                {
                    ship = "";

                }
                else
                {
                    ship = " and a.ship_to ='" + ship + "'";

                }

                if (string.IsNullOrEmpty(d_dt_from))
                {
                    d_dt_from = "";

                }
                else
                {
                    d_dt_from = " and a.do_dt >='" + d_dt_from + "'";

                }


                if (string.IsNullOrEmpty(d_dt_to))
                {
                    d_dt_to = "";

                }
                else
                {
                    d_dt_to = " and a.do_dt =<'" + d_dt_to + "'";

                }

                keyword = com + do_no + inv_no + sold + ship + d_dt_from + d_dt_to;

                dtoresult = Process_Data.SelectDOHdr(keyword);

                return dtoresult;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public DO_dto DisplayDODtl(string com, string do_no)
        {
            try
            {
                dtoresult = Process_Data.SelectDODtl(do_no, com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }    
        }

        public DO_dto DisplayDOFoc(string com, string do_no)
        {
            try
            {
                dtoresult = Process_Data.SelectDOFoc(do_no, com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DO_dto CheckExistCertifyDONo(string do_no)
        {
            try
            {
                dtoresult = Process_Data.SelectCertifiedDO(do_no);
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

        public DO_dto UpdateDOCertify(DataTable dtheader, string usn)
        {
            try
            {
                if (dtheader.Rows.Count > 0)
                {
                    dtoresult = Process_Data.CertifyDO(dtheader, usn);
                }
                else
                {
                  
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

    }
}