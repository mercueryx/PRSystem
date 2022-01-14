using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace ERP_System.SALES_ORDER.SALES_ORDER_Control
{
	public class SO_bo
	{
        SO_dto dtoresult = new SO_dto();
        SO_da ProcessData = new SO_da();

        #region SO Entry

        #region Header
        public SO_dto DisplayCompanyCode()
        {
            try
            {
                dtoresult = ProcessData.SelectCompany();
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SO_dto DisplayGroupCode(string com)
        {
            try
            {
                dtoresult = ProcessData.SelectGroupCode(com);
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SO_dto DisplayBillTo(string group,string type)
        {
            try
            {
                if (type == "LOCAL")
                {
                    type = "Y";
                }
                else if(type=="EXPORT")
                {
                    type = "N";
                }
                dtoresult = ProcessData.SelectBillTo(group, type);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SO_dto DisplaySoldTo(string group)
        {
            try
            {
                dtoresult = ProcessData.SelectSoldTo( group);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SO_dto DisplaySignatory(string com)
        {
            try
            {
                dtoresult = ProcessData.SelectSignatory(com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SO_dto DisplayPurTerm(string com, string ahcode)
        {
            try
            {
                dtoresult = ProcessData.SelectPaymentTerm(com, ahcode);
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Details

        public SO_dto DisplayCatalogNo(string com)
        {
            try
            {
                dtoresult = ProcessData.SelectCatalogNo(com);
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }     

        public SO_dto DisplayCatalogInfo(string ctlno)
        {
            try
            {
                dtoresult = ProcessData.SelectCatalogInfo( ctlno);
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SO_dto DisplayUnitSellPrice(string catalogno)
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

        public SO_dto DisplayUOM()
        {
            try
            {
                dtoresult = ProcessData.SelectUOM();
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SO_dto CheckSCD_DataNull(string ctlno, string dsc, string ex_dsc, string uom, string qty,string foc_qty, string u_price, string total)
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
                if (string.IsNullOrEmpty(ex_dsc))
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
                if (string.IsNullOrEmpty(u_price))
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

        public SO_dto AssignAutoNumber(DataTable dttable)
        {
            try
            {
                string no;
                no = "0";
                int row_count = dttable.Rows.Count - 1;
               if (dttable.Rows.Count > 0)
                {
                    no = dttable.Rows[row_count]["no"].ToString();
                    no = (Convert.ToInt32(no) + 1).ToString();

                }
                else
                {
                    no = "1";
                }
                dtoresult.auto_no = no;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SO_dto CheckSC_DeliveryDetails(string no, string etd, string eta)
        {
            try
            {
                if (string.IsNullOrEmpty(no))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Please select catalog first before proceed.";
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(etd))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "ETD cannot empty.";
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(no))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "ETA cannot empty";
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
        #endregion

        #region Insert Data
        private string GetSC_No()
        {
            try
            {

                int rn;
                string scno, year, month;
                year = DateTime.Now.ToString("yy");
                month = DateTime.Now.ToString("MM");
                scno = "SC" + year + month;
                dtoresult = ProcessData.SelectSC_RN(scno);
                if (dtoresult.dtrn.Rows.Count > 0)
                {
                    rn = Convert.ToInt32(dtoresult.dtrn.Rows[0]["sc_no"]);
                    rn = rn + 1;

                }
                else
                {
                    rn = 1;
                }
                scno = scno + rn.ToString("D3");
                return scno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public SO_dto SaveSalesContract( string com, string group, string type, string billto, string soldto, string payterm, string sc_date, string signatory, string usn,DataTable dtscd,DataTable dtscdd)
        {
            try
            {
                string ctlno, dtlrn,d_dtlrn,rn;
                int match_count;
                match_count = 0;
                //check header empty field
                if (string.IsNullOrEmpty(group))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Group code cannot empty.";
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(type))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Type cannot empty.";
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(billto))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Bill to cannot empty.";
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(soldto))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Sold to cannot empty.";
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(payterm))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Payment term cannot empty.";
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(sc_date))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Date cannot empty.";
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(signatory))
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Signatory cannot empty.";
                    return dtoresult;
                }

                rn = GetSC_No();
                dtoresult.scno = rn;

                if (dtscd.Rows.Count == 0)
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Sales contract details cannot empty.";
                    return dtoresult;
                }
                else
                {
                    for (int scd_i = 0; scd_i < dtscd.Rows.Count; scd_i++)
                    {
                        dtlrn = dtscd.Rows[scd_i]["no"].ToString();
                        ctlno = dtscd.Rows[scd_i]["ctlno"].ToString();

                        for (int scdd_i = 0; scdd_i < dtscdd.Rows.Count; scdd_i++)
                        {
                            d_dtlrn = dtscdd.Rows[scdd_i]["no"].ToString();
                            if (dtlrn == d_dtlrn)
                            {
                                match_count += 1;
                            }
                        }
                        if (match_count == 0)
                        {
                            dtoresult.sts = false;
                            dtoresult.Message = "Delivery details cannot empty for " + ctlno + ".";
                            return dtoresult;
                            //break;
                        }
                    }
                }
                //check delivery details for each catalog
              
              
                dtoresult = ProcessData.InsertSalesContract(com, rn, group, type, billto, soldto, payterm, sc_date, signatory, usn,dtscd,dtscdd);
                return dtoresult;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #endregion

        #region SO Certify
        public SO_dto DisplaySoldto_Billto(string type)
        {
            try
            {
                if (type == "LOCAL")
                {
                    type = "Y";
                }
                else if (type == "EXPORT")
                {
                    type = "N";
                }
                dtoresult = ProcessData.SelectSoldTo_BillTo(type);
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SO_dto DisplaySCHeader(string com,string type,string billto,string soldto,string scno,string scdate)
        {
            try
            {
                string keyword;
                keyword = "";

                com = " a.com='" + com + "'";
                
                if (string.IsNullOrEmpty(scno))
                {
                    scno = "";
                }
                else
                {
                    scno = " and a.sc_no ='" + scno + "'";
                }

                if (string.IsNullOrEmpty(type))
                {
                    type = "";
                }
                else
                {
                    type = " and a.order_type ='" + type + "'";
                }

                if (string.IsNullOrEmpty(billto))
                {
                    billto = "";

                }
                else
                {
                    billto = " and a.bill_to ='" + billto + "'";

                }

                if (string.IsNullOrEmpty(soldto))
                {
                    soldto = "";

                }
                else
                {
                    soldto = " and a.sold_to ='" + soldto + "'";

                }

                if (string.IsNullOrEmpty(scdate))
                {
                    scdate = "";

                }
                else
                {
                    scdate = " and a.sc_date ='" + scdate + "'";

                }

                keyword =com+ scno + type + billto + soldto + scdate;

                dtoresult = ProcessData.SelectSCHeader(keyword);

                return dtoresult;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public SO_dto DisplaySCDetails(string sc_no)
        {
            try
            {
                dtoresult = ProcessData.SelectSCDetails(sc_no);
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SO_dto DisplaySCDeliveryDetails(string sc_no, string dtlno)
        {
            try
            {
                dtoresult = ProcessData.SelectSCDeliveryDetails(sc_no, dtlno);
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SO_dto DisplayOpenSC(string com)
        {
            try
            {
                dtoresult = ProcessData.SelectAllOpenSC(com);
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SO_dto CheckExistCertifySCNo(string sc_no)
        {
            try
            {
                dtoresult = ProcessData.SelectCertifiedSC(sc_no);
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

        public SO_dto UpdateSCCertify(DataTable dtheader, string usn)
        {
            try
            {
                if (dtheader.Rows.Count > 0)
                {
                     dtoresult = ProcessData.CertifySC(dtheader,usn);
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