using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace ERP_System.INV_Module.INV_Control
{
    public class INV_bo
    {
        INV_dto dtoresult = new INV_dto();
        INV_da ProcessData = new INV_da();

        #region Transfer Requisition

        public INV_dto DisplayLocation(string com)
        {
            try
            {
                dtoresult = ProcessData.SelectLocation(com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public INV_dto DisplayCatalogNo(string com, string loc)
        {
            try
            {
                dtoresult = ProcessData.SelectCatalogByLoc(com, loc);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public INV_dto DisplayInvDtl(string com, string loc, string ctlno)
        {
            try
            {
                dtoresult = ProcessData.SelectInvDtl(com, loc, ctlno);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public INV_dto CheckReceiveQty(string req_qty, string o_qty)
        {
            try
            {
                decimal dec_reqqty=0, dec_on_qty=0, checkdecimal=0;
                int checkint=0;
                dec_reqqty = decimal.Parse(req_qty);
                dec_on_qty = decimal.Parse(o_qty);

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
                    if (dec_reqqty > dec_on_qty)
                    {
                        dtoresult.Message = "Request qty cannot more than onhand qty.";
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

        private string GetGRN_No(string loc)

        {
            try
            {

                int rn;
                string tranx_no, year, month;
                year = DateTime.Now.ToString("yy");
                month = DateTime.Now.ToString("MM");
                tranx_no = loc + year + month;
                dtoresult = ProcessData.SelectTranx_RN(tranx_no);
                if (dtoresult.dtrn.Rows.Count > 0)
                {
                    rn = Convert.ToInt32(dtoresult.dtrn.Rows[0]["tranx_no"]);
                    rn = rn + 1;

                }
                else
                {
                    rn = 1;
                }
                tranx_no = tranx_no + rn.ToString("D3");
                return tranx_no;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public INV_dto InsertData(string c_tranx_no,DataTable dtsave, string from_loc, string to_loc, string rmk, string req_date,string com,string usn,DataTable dttrans)
        {
            try
            {
                //generate tranx_no
                Boolean exist_sts;
                string tranx_no,ctlno,dsc,uom,type,reqqty,ex_dsc,cur,refno;
                tranx_no = GetGRN_No(to_loc);
                type = "";
                cur = "";
                ex_dsc = "";
                refno = "";
                ctlno = "";
                // select to location type
                dtoresult = ProcessData.SelectLocationInvtype(com, to_loc);
                if(dtoresult.dtinv_type.Rows.Count>0)
                {
                    type = dtoresult.dtinv_type.Rows[0]["inv_type"].ToString();
                }
                for (int i = 0; i < dtsave.Rows.Count; i++)
                {
                    ctlno = dtsave.Rows[i]["catalog_no"].ToString();
                    dsc = dtsave.Rows[i]["dsc"].ToString();
                    uom = dtsave.Rows[i]["uom"].ToString();
                    reqqty = dtsave.Rows[i]["qty"].ToString();
                    refno = dtsave.Rows[i]["refno"].ToString();
                    //select inv info by 
                    dtoresult = ProcessData.SelectCatalogInfo(com, ctlno);
                    if(dtoresult.dtctlno.Rows.Count>0)
                    {
                        ex_dsc = dtoresult.dtctlno.Rows[0]["ex_dsc"].ToString();
                        cur = dtoresult.dtctlno.Rows[0]["cur"].ToString();
                    }
                    dttrans.Rows.Add(ctlno,ex_dsc,dsc,reqqty,uom,cur,refno);
                }
                dtoresult.tranx_no = tranx_no;

                //check if user continue click save button after save
                if (!string.IsNullOrEmpty(c_tranx_no))
                {
                    dtoresult.Message = "Please click new button to submit new transfer requisition.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (from_loc == to_loc)
                {
                    dtoresult.sts = false;
                    dtoresult.Message = "Cannot transfer between two same location.";
                    return dtoresult;
                }
                //check loc duplicate or not 
                dtoresult = ProcessData.SelectDuplicateTransReq(com, ctlno, type, to_loc);
                if (dtoresult.dtcheck.Rows.Count > 0)
                {
                    exist_sts = true;
                }
                else
                {
                    exist_sts = false;
                }
                ////out from JFP
                //if (from_loc == "JFP")
                //{
                //    dtoresult = ProcessData.InsertTransReq_out_inv(exist_sts,dttrans, type, tranx_no, com, from_loc, to_loc, rmk, req_date, usn);
                //    return dtoresult;
                //}
                //// into JFP
                //if (to_loc == "JFP")
                //{
                //    dtoresult = ProcessData.InsertTransReq_in_inv(exist_sts,dttrans, type, tranx_no, com, from_loc, to_loc, rmk, req_date, usn);
                //    return dtoresult;
                //}
                //not from / to JFP
                dtoresult = ProcessData.InsertTransReq(exist_sts,dttrans, type, tranx_no, com, from_loc, to_loc, rmk, req_date, usn);             
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