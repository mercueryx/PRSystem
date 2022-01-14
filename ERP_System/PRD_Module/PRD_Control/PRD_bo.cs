using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace ERP_System.PRD_Module.PRD_Control
{
    public class PRD_bo
    {
        PRD_da ProcessData = new PRD_da();
        PRD_dto dtoresult = new PRD_dto();

        #region Create Packing List

        #region Display Data

        public PRD_dto DisplayBrandCode(string com)
        {
            try
            {
                dtoresult = ProcessData.SelectBrandCode(com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto DisplayMasterCatalog_ByBrand(string com, string brand)
        {
            try
            {
                dtoresult = ProcessData.SelectMasterCatalog_ByBrand(com, brand);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto DisplayDefaultLocation(string com)
        {
            try
            {
                dtoresult = ProcessData.SelectDefaultLocation(com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto DisplayCatalogNo_ByLoc(string com, string loc)
        {
            try
            {
                dtoresult = ProcessData.SelectCatalogNo_ByLoc(com, loc);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto GetCatalogDescription(string com, string loc, string ctlno)
        {
            try
            {
                dtoresult = ProcessData.SelectCatalogDescription(com, loc, ctlno);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        public PRD_dto CheckReceiveQty(string req_qty)
        {
            try
            {
                decimal dec_reqqty=0, checkdecimal=0;
                int checkint=0;
                dec_reqqty = decimal.Parse(req_qty);
              

                if (!string.IsNullOrEmpty(req_qty))
                {
                    
                    if (int.TryParse(req_qty, out checkint))
                    {
                        dec_reqqty = decimal.Parse(req_qty);

                    }
                    else
                    {
                        // The string was a valid integer => use result here
                        if (decimal.TryParse(req_qty, out checkdecimal))
                        {
                            dec_reqqty = decimal.Parse(req_qty);

                        }

                    }         
                }
                else
                {
                    dtoresult.message = "Please key in numeric value for qty.";
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

        public PRD_dto CheckDuplicateDetails(DataTable dtdisplay,string loc,string ctlno)
        {
            try
            {
                string check_loc, check_ctlno;
                for (int i = 0; i < dtdisplay.Rows.Count; i++)
                {
                    check_loc = dtdisplay.Rows[i]["loc"].ToString();
                    check_ctlno= dtdisplay.Rows[i]["catalog_no"].ToString();
                    if (loc == check_loc && ctlno == check_ctlno)
                    {
                        dtoresult.sts = false;
                        dtoresult.message = "This catalog no and location already existed.";
                        return dtoresult;
                    }

                }
                dtoresult.sts = true;
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string GetPkgList_No()
        {
            try
            {

                int rn;
                string pl_no, year, month;
                year = DateTime.Now.ToString("yy");
                month = DateTime.Now.ToString("MM");
                pl_no = "PL" + year + month;
                dtoresult = ProcessData.SelectPkgList_RN(pl_no);
                if (dtoresult.dtrn.Rows.Count > 0)
                {
                    rn = Convert.ToInt32(dtoresult.dtrn.Rows[0]["pl_rn"]);
                    rn = rn + 1;

                }
                else
                {
                    rn = 1;
                }
                pl_no = pl_no + rn.ToString("D3");
                return pl_no;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto InsertNewPackingList(DataTable dtdetails, string com, string brand, string mstr_ctlno, string ef_dt, string refno,string usn,string exist_ver)
        {
            try
            {
                string plno,ver;
                decimal dec_ver;

                // get runnig number
                plno = GetPkgList_No();

                if (dtdetails.Rows.Count > 0)
                {
                    // check data null
                    if (string.IsNullOrEmpty(com))
                    {
                        dtoresult.message = "Company code cannot empty.";
                        dtoresult.sts = false;
                    }

                    if (string.IsNullOrEmpty(brand))
                    {
                        dtoresult.message = "Brand code cannot empty.";
                        dtoresult.sts = false;
                    }

                    if (string.IsNullOrEmpty(mstr_ctlno))
                    {
                        dtoresult.message = "Master catalog cannot empty.";
                        dtoresult.sts = false;
                    }

                    if (string.IsNullOrEmpty(ef_dt))
                    {
                        dtoresult.message = "Effective date cannot empty.";
                        dtoresult.sts = false;
                    }

                    //check if user continue click save button after save
                    if (!string.IsNullOrEmpty(exist_ver))
                    {
                        dtoresult.message = "Please click new button to create new packing list.";
                        dtoresult.sts = false;
                        return dtoresult;
                    }

                    //select duplicate master catalog
                    dtoresult = ProcessData.SelectSamePackingListHdr(com, mstr_ctlno, brand);
                    if (dtoresult.dtcheck.Rows.Count > 0)
                    {
                        dec_ver =decimal.Parse(dtoresult.dtcheck.Rows[0]["ver"].ToString());
                        //dec_ver = Convert.ToInt32(ver);
                        dec_ver = dec_ver + decimal.Parse("0.1");
                        ver = dec_ver.ToString();
                    }
                    else
                    {
                        ver = "1.1";
                    }


                    dtoresult = ProcessData.InsertPackingList(dtdetails, com, plno, brand, mstr_ctlno, ver, ef_dt, refno, usn);
                    if (dtoresult.sts == true)
                    {
                        dtoresult.version = ver;
                        dtoresult.message = "Create packing list successful.";
                    }
                    
                }
                else
                {
                    dtoresult.message = "Please fill in the packing list details to proceed.";
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

        #region Packing Entry

        public PRD_dto DisplayPkgList_MasterCatalog(string com,string brand)
        {
            try
            {
           
                dtoresult = ProcessData.SelectMasterCatalog_PackingList(com, brand);
               
             
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto DisplayRefNo(string rn)
        {
            try
            {
                dtoresult = ProcessData.SelectPackingListRefno(rn);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto DisplayCustCode(string com)
        {
            try
            {
                dtoresult = ProcessData.SelectCustCode(com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto DisplayPackingListDetails(string rn, decimal qty)
        {
            try
            {
                dtoresult = ProcessData.SelectPackingListDetails(rn, qty);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string GetPacking_No()
        {
            try
            {

                int rn;
                string pkgno, year, month;
                year = DateTime.Now.ToString("yy");
                month = DateTime.Now.ToString("MM");
                pkgno = "PK" + year + month;
                dtoresult = ProcessData.SelectPacking_RN(pkgno);
                if (dtoresult.dtrn.Rows.Count > 0)
                {
                    rn = Convert.ToInt32(dtoresult.dtrn.Rows[0]["pkg_no"]);
                    rn = rn + 1;

                }
                else
                {
                    rn = 1;
                }
                pkgno = pkgno + rn.ToString("D3");
                return pkgno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto NewPackingEntry(DataTable dtdetails, string com,string brand, string refno, string pl_rn, string rmk, string trans_date, string cust_code, string qty, string usn)
        {
            try
            {
                string mstr_ctlno,pkg_rn, ctlno,t_qty,o_qty;
                decimal onhand_qty, total_qty;
                mstr_ctlno = "";
                //check data null
                if (string.IsNullOrEmpty(brand))
                {
                    dtoresult.message = "Brand code cannot empty";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(trans_date))
                {
                    dtoresult.message = "Transaction date cannot empty";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(cust_code))
                {
                    dtoresult.message = "Customer code cannot empty";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (qty == "0.00")
                {
                    dtoresult.message = "Quantity cannot 0.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                //get master catalog
                dtoresult = ProcessData.SelectMasterCatalogByRN(pl_rn);
                if (dtoresult.dtcheck.Rows.Count > 0)
                {
                    mstr_ctlno = dtoresult.dtcheck.Rows[0]["mstr_catalog"].ToString();
                }

                //get packing running number
                pkg_rn = GetPacking_No();

                //check on hand quantity enough for packing or not
                for (int i = 0; i < dtdetails.Rows.Count; i++)
                {
                    ctlno = dtdetails.Rows[i]["catalog"].ToString();                                   
                    t_qty = dtdetails.Rows[i]["qty"].ToString();
                    o_qty = dtdetails.Rows[i]["on_qty"].ToString();

                    onhand_qty = decimal.Parse(o_qty);
                    total_qty = decimal.Parse(t_qty);

                    if (total_qty > onhand_qty)
                    {
                        dtoresult.message = "Insufficient onhand qty for " + ctlno + ".";
                        dtoresult.sts = false;
                        return dtoresult;
                    }
                }
                //insert packing info
                dtoresult = ProcessData.InsertNewPacking(dtdetails, com, brand, mstr_ctlno, refno, pkg_rn, rmk, trans_date, cust_code, qty, usn);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Certify Packing

        public PRD_dto DisplayPackingBrandCode(string com)
        {
            try
            {
                dtoresult = ProcessData.SelectPackingBrand(com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto DisplayPackingMaster(string com)
        {
            try
            {
                dtoresult = ProcessData.SelectPackingMaster(com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto DisplayPackingCustCode(string com)
        {
            try
            {
                dtoresult = ProcessData.SelectPackingCustCode(com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto DisplaySearchPackingInfo(string com,string brand, string mstr, string cust, string trans_date)
        {
            try
            {
                string brand_qry, mstr_qry, cust_qry, date_qry,values;
                brand_qry = "";
                mstr_qry = "";
                cust_qry = "";
                date_qry = "";

                if (!string.IsNullOrEmpty(brand))
                {
                    brand_qry = " and brd_code ='"+brand+"'";
                }

                if (!string.IsNullOrEmpty(mstr))
                {
                    mstr_qry = " and mstr_catalog ='" + mstr + "'";

                }

                if (!string.IsNullOrEmpty(cust))
                {
                    cust_qry = " and cust_code ='" + cust + "'";

                }

                if (!string.IsNullOrEmpty(trans_date))
                {
                   date_qry = " and trans_date ='" + trans_date + "'";

                }
                values = brand_qry + mstr_qry + cust_qry + date_qry;
                dtoresult = ProcessData.SelectPackingInfo(com, values);


                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto DisplayPackingDetails(string com,string pkg_no)
        {
            try
            {
                dtoresult = ProcessData.SelectPackingDetails_RN(com,pkg_no);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto CheckExistCertifyPkg(string rn)
        {
            try
            {
                dtoresult = ProcessData.SelectCertifiedPKG(rn);
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

        public PRD_dto CertifyPacking(DataTable dthdr, DataTable dtdetails, string com, string usn)
        {
            try
            {
                string rn,pkg_no, ctlno, dsc, loc, qty,o_qty,trans_date;
                if (dthdr.Rows.Count > 0)
                {
                    for (int i = 0; i < dthdr.Rows.Count; i++)
                    {
                        rn = dthdr.Rows[i]["rn"].ToString();
                        trans_date = dthdr.Rows[i]["trans_date"].ToString();
                        dtoresult = ProcessData.SelectPackingDetails_RN(com, rn);
                        for (int count = 0; count < dtoresult.dtpkgdetails.Rows.Count; count++)
                        {
                            pkg_no = dtoresult.dtpkgdetails.Rows[count]["pkg_no"].ToString();
                            ctlno = dtoresult.dtpkgdetails.Rows[count]["catalog_no"].ToString();
                            dsc = dtoresult.dtpkgdetails.Rows[count]["dsc"].ToString();
                            loc = dtoresult.dtpkgdetails.Rows[count]["loc"].ToString();
                            qty = dtoresult.dtpkgdetails.Rows[count]["req_qty"].ToString();
                            o_qty = dtoresult.dtpkgdetails.Rows[count]["o_qty"].ToString();

                            //check qty 
                            if (decimal.Parse(qty) > decimal.Parse(o_qty))
                            {
                                dtoresult.sts = false;
                                dtoresult.message = ctlno + "request qty cannot more than onhand qty";
                                return dtoresult;
                            }
                            dtdetails.Rows.Add(pkg_no, ctlno, dsc, loc, qty, trans_date);

                        }
                    }
                    dtoresult = ProcessData.UpdatePackingInfo(dthdr, dtdetails, com, usn);
                }
                else
                {
                    dtoresult.message = "No data updated.";
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