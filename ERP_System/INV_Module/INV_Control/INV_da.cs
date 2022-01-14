using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace ERP_System.INV_Module.INV_Control
{

    public class INV_da
    {
        INV_dto dtoresult = new INV_dto();
        SqlConnection erp_con = new SqlConnection(ResourceModule.ERP_con);
        SqlTransaction trans;

        #region Transfer Requisition

        public INV_dto SelectLocation(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select loc,loc+char(9)+'|'+char(9)+dsc as dsc from tbl_npi_loc where com =@com", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtloc_from = dtcmd;
                dtoresult.dtloc_to = dtcmd;
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                erp_con.Close();
            }
        }

        public INV_dto SelectCatalogByLoc(string com, string loc)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select catalog_no,catalog_no + char(9)+'|'+char(9)+dsc as dsc from tbl_inv where com=@com and loc=@loc and mstr_type <>'T'", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@loc", loc);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtctlno = dtcmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                erp_con.Close();
            }
        }

        public INV_dto SelectInvDtl(string com, string loc, string ctlno)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select catalog_no,dsc,ex_dsc,uom,cur,qty from tbl_inv where com=@com and loc=@loc and catalog_no=@ctlno", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@loc", loc);
                //cmd.Parameters.AddWithValue("@sts", sts);
                cmd.Parameters.AddWithValue("@ctlno", ctlno);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtinv_dtl = dtcmd;
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                erp_con.Close();
            }
        }

        //for insert
        public INV_dto SelectLocationInvtype(string com, string loc)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select inv_type from tbl_npi_loc where com=@com and loc=@loc", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@loc", loc);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtinv_type = dtcmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                erp_con.Close();
            }
        }

        public INV_dto SelectCatalogInfo(string com, string ctlno)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_inv where com=@com and catalog_no=@ctlno", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@ctlno", ctlno);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtctlno = dtcmd;
                return dtoresult;
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                erp_con.Close();
            }
        }

        public INV_dto SelectTranx_RN(string rn)
        {
            try
            {
                erp_con.Close();
                SqlCommand cmd = new SqlCommand("Select top 1 RIGHT(tranx_no, LEN(tranx_no) - 6) as tranx_no from tbl_npi_transfer_req_hdr where tranx_no like @tranx_no + '%'order by add_date desc", erp_con);
                cmd.Parameters.AddWithValue("@tranx_no", rn);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtrn = dtcmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                erp_con.Close();

            }
        }

        public INV_dto GenerateUpdateDeductLocQty(string com, string loc, string ctlno, decimal req_qty)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update tbl_inv set qty=qty-@req_qty where com=@com and loc=@loc and catalog_no=@ctlno", erp_con);
                cmd.Parameters.AddWithValue("@req_qty", req_qty);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@loc", loc);
                cmd.Parameters.AddWithValue("@ctlno", ctlno);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public INV_dto GenerateUpdateAddLocQty(string com, string loc, string ctlno, decimal req_qty)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update tbl_inv set qty=qty+@req_qty where com=@com and loc=@loc and catalog_no=@ctlno", erp_con);
                cmd.Parameters.AddWithValue("@req_qty", req_qty);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@loc", loc);
                cmd.Parameters.AddWithValue("@ctlno", ctlno);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        public INV_dto GenerateInsert_TransferReqHdr(string com, string tranx_no,string f_loc, string t_loc,string rmk,string req_date, string usn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_npi_transfer_req_hdr (com,tranx_no,f_loc,t_loc,rmk,req_date,add_date,add_usn) values (@com,@tranx_no,@f_loc,@t_loc,@rmk,@req_date,GetDate(),@usn)", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@tranx_no", tranx_no);
                cmd.Parameters.AddWithValue("@f_loc", f_loc);
                cmd.Parameters.AddWithValue("@t_loc", t_loc);
                cmd.Parameters.AddWithValue("@rmk", rmk);
                cmd.Parameters.AddWithValue("@req_date", req_date);
                cmd.Parameters.AddWithValue("@usn", usn);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public INV_dto GenerateInsert_TransferReqDtl(string com,string ctlno,string dsc,string qty,string f_loc,string t_loc,string tranx_no,string ref_no,string usn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_npi_transfer_req_dtl (com,catalog_no,dsc,qty,f_loc,t_loc,tranx_no,ref_no,add_date,add_usn) values (@com,@ctlno,@dsc,@qty,@f_loc,@t_loc,@tranx,@ref,GetDate(),@usn)", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@ctlno", ctlno);
                cmd.Parameters.AddWithValue("@dsc", dsc);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@f_loc", f_loc);
                cmd.Parameters.AddWithValue("@t_loc", t_loc);
                cmd.Parameters.AddWithValue("@tranx", tranx_no);
                cmd.Parameters.AddWithValue("@ref", ref_no);
                cmd.Parameters.AddWithValue("@usn", usn);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public INV_dto SelectDuplicateTransReq(string com, string catalog_no, string type, string loc)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_inv where com=@com and catalog_no=@ctlno and loc=@loc and type=@type and sts='O'", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@ctlno", catalog_no);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@loc", loc);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtcheck = dtcmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                erp_con.Close();
            }
        }

        public INV_dto GenerateInsertLocTransferInv(string com,string catalog_no,string ex_dsc,string dsc,string qty,string type,string uom,string loc,string cur,string usn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_inv (com,catalog_no,ex_dsc,dsc,qty,type,uom,loc,cur,sts,add_date,add_usn) values (@com,@ctlno,@ex_dsc,@dsc,@qty,@type,@uom,@loc,@cur,'O',GetDate(),@usn)", erp_con);
                cmd.Parameters.AddWithValue("@com",com);
                cmd.Parameters.AddWithValue("@ctlno", catalog_no);
                cmd.Parameters.AddWithValue("@ex_dsc", ex_dsc);
                cmd.Parameters.AddWithValue("@dsc", dsc);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@uom", uom);
                cmd.Parameters.AddWithValue("@loc", loc);
                cmd.Parameters.AddWithValue("@cur", cur);
                //cmd.Parameters.AddWithValue("@tranx_no", tranx_no);
                cmd.Parameters.AddWithValue("@usn", usn);
                dtoresult.cmd = cmd;
                return dtoresult;          
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public INV_dto GenerateUpdateLocTransferInv_Add(string com, string catalog_no, string loc, string type,decimal qty)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("Update tbl_inv set qty=qty+@qty where com=@com and catalog_no=@ctlno and loc=@loc and type=@type and sts='O'", erp_con);
        //        cmd.Parameters.AddWithValue("@com", com);
        //        cmd.Parameters.AddWithValue("@ctlno", catalog_no);             
        //        cmd.Parameters.AddWithValue("@type", type);             
        //        cmd.Parameters.AddWithValue("@loc", loc);
        //        cmd.Parameters.AddWithValue("@qty", qty);
        //        dtoresult.cmd = cmd;
        //        return dtoresult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public INV_dto GenerateUpdateLocTransferInv_Deduct(string com, string catalog_no, string loc, string type, decimal qty)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("Update tbl_inv set qty=qty-@qty where com=@com and catalog_no=@ctlno and loc=@loc and type=@type and sts='O'", erp_con);
        //        cmd.Parameters.AddWithValue("@com", com);
        //        cmd.Parameters.AddWithValue("@ctlno", catalog_no);
        //        cmd.Parameters.AddWithValue("@type", type);
        //        cmd.Parameters.AddWithValue("@loc", loc);
        //        cmd.Parameters.AddWithValue("@qty", qty);
        //        dtoresult.cmd = cmd;
        //        return dtoresult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public INV_dto InsertTransReq_out_inv(Boolean sts,DataTable dttrans,string type, string tranx_no, string com, string f_loc, string t_loc, string rmk, string req_date, string usn)
        //{
        //    try
        //    {
        //        string ctlno, dsc, uom, reqqty, ex_dsc, cur, refno;
        //        decimal dec_req_qty;
        //        erp_con.Open();
        //        trans = erp_con.BeginTransaction();
        //        for (int i = 0; i < dttrans.Rows.Count; i++)
        //        {
        //            ctlno = dttrans.Rows[i]["catalog_no"].ToString();
        //            dsc = dttrans.Rows[i]["dsc"].ToString();
        //            uom = dttrans.Rows[i]["uom"].ToString();
        //            reqqty = dttrans.Rows[i]["qty"].ToString();
        //            refno = dttrans.Rows[i]["refno"].ToString();
        //            ex_dsc = dttrans.Rows[0]["ex_dsc"].ToString();
        //            cur = dttrans.Rows[0]["cur"].ToString();
        //            dec_req_qty = decimal.Parse(reqqty);
                  
        //            // deduct from JFP
        //            dtoresult = GenerateUpdateDeductLocQty(com, f_loc, ctlno, dec_req_qty);
        //            dtoresult.cmd.Transaction = trans;
        //            dtoresult.cmd.ExecuteNonQuery();

        //            if (sts == true)
        //            {
        //                //update qty in inventory table (to loc)
        //                dtoresult = GenerateUpdateLocTransferInv_Add(com, ctlno, t_loc, type, dec_req_qty);
        //                dtoresult.cmd.Transaction = trans;
        //                dtoresult.cmd.ExecuteNonQuery();
        //            }
        //            else
        //            {
        //                //insert new loc in inventory table (to loc)
        //                dtoresult = GenerateInsertLocTransferInv(com, ctlno, ex_dsc, dsc, reqqty, type, uom, t_loc, cur, usn);
        //                dtoresult.cmd.Transaction = trans;
        //                dtoresult.cmd.ExecuteNonQuery();
        //            }
                 

        //            // insert transfer requisition header
        //            dtoresult = GenerateInsert_TransferReqHdr(com, tranx_no, f_loc, t_loc, rmk, req_date, usn);
        //            dtoresult.cmd.Transaction = trans;
        //            dtoresult.cmd.ExecuteNonQuery();

        //            //  insert transfer requisition details
        //            dtoresult = GenerateInsert_TransferReqDtl(com, ctlno, dsc, reqqty, f_loc, t_loc, tranx_no, refno, usn);
        //            dtoresult.cmd.Transaction = trans;
        //            dtoresult.cmd.ExecuteNonQuery();


        //        }
        //        //trans.Rollback();
        //        trans.Commit();
        //        dtoresult.sts = true;
        //        return dtoresult;
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        erp_con.Close();
        //    }
        //}

        //public INV_dto InsertTransReq_in_inv(Boolean sts,DataTable dttrans, string type, string tranx_no, string com, string f_loc, string t_loc, string rmk, string req_date, string usn)
        //{
        //    try
        //    {
        //        string ctlno, dsc, uom, reqqty, ex_dsc, cur, refno;
        //        decimal dec_req_qty;
        //        erp_con.Open();
        //        trans = erp_con.BeginTransaction();
        //        for (int i = 0; i < dttrans.Rows.Count; i++)
        //        {
        //            ctlno = dttrans.Rows[i]["catalog_no"].ToString();
        //            dsc = dttrans.Rows[i]["dsc"].ToString();
        //            uom = dttrans.Rows[i]["uom"].ToString();
        //            reqqty = dttrans.Rows[i]["qty"].ToString();
        //            refno = dttrans.Rows[i]["refno"].ToString();
        //            ex_dsc = dttrans.Rows[0]["ex_dsc"].ToString();
        //            cur = dttrans.Rows[0]["cur"].ToString();
        //            dec_req_qty = decimal.Parse(reqqty);

        //            // add to JFP
        //            dtoresult = GenerateUpdateAddLocQty(com, t_loc, ctlno, dec_req_qty);
        //            dtoresult.cmd.Transaction = trans;
        //            dtoresult.cmd.ExecuteNonQuery();

        //            if (sts == true)
        //            {
        //                //update qty in inventory table (to loc)
        //                dtoresult = GenerateUpdateLocTransferInv_Deduct(com, ctlno, t_loc, type, dec_req_qty);
        //                dtoresult.cmd.Transaction = trans;
        //                dtoresult.cmd.ExecuteNonQuery();
        //            }
        //            //else
        //            //{
        //            //    //insert new loc in inventory table (to loc)
        //            //    dtoresult = GenerateInsertLocTransferInv(com, ctlno, ex_dsc, dsc, reqqty, type, uom, t_loc, cur, usn);
        //            //    dtoresult.cmd.Transaction = trans;
        //            //    dtoresult.cmd.ExecuteNonQuery();
        //            //}

        //            // insert transfer requisition header
        //            dtoresult = GenerateInsert_TransferReqHdr(com, tranx_no, f_loc, t_loc, rmk, req_date, usn);
        //            dtoresult.cmd.Transaction = trans;
        //            dtoresult.cmd.ExecuteNonQuery();

        //            //insert transfer requisition details
        //            dtoresult = GenerateInsert_TransferReqDtl(com, ctlno, dsc, reqqty, f_loc, t_loc, tranx_no, refno, usn);
        //            dtoresult.cmd.Transaction = trans;
        //            dtoresult.cmd.ExecuteNonQuery();


        //        }
        //        trans.Commit();
        //        dtoresult.sts = true;
        //        return dtoresult;
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        erp_con.Close();
        //    }
        //}

        public INV_dto InsertTransReq(Boolean sts,DataTable dttrans, string type, string tranx_no, string com, string f_loc, string t_loc, string rmk, string req_date, string usn)
        {
            try
            {
                string ctlno, dsc, uom, reqqty, ex_dsc, cur, refno;
                decimal dec_req_qty;
                erp_con.Open();
                trans = erp_con.BeginTransaction();
                for (int i = 0; i < dttrans.Rows.Count; i++)
                {
                    ctlno = dttrans.Rows[i]["catalog_no"].ToString();
                    dsc = dttrans.Rows[i]["dsc"].ToString();
                    uom = dttrans.Rows[i]["uom"].ToString();
                    reqqty = dttrans.Rows[i]["qty"].ToString();
                    refno = dttrans.Rows[i]["refno"].ToString();
                    ex_dsc = dttrans.Rows[0]["ex_dsc"].ToString();
                    cur = dttrans.Rows[0]["cur"].ToString();
                    dec_req_qty = decimal.Parse(reqqty);

                 

                    //deduct qty from location
                    dtoresult = GenerateUpdateDeductLocQty(com, f_loc, ctlno, dec_req_qty);
                    dtoresult.cmd.Transaction = trans;
                    dtoresult.cmd.ExecuteNonQuery();


                    if (sts == true)
                    {
                        //add qty to location
                        dtoresult = GenerateUpdateAddLocQty(com, t_loc, ctlno, dec_req_qty);
                        dtoresult.cmd.Transaction = trans;
                        dtoresult.cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        if (t_loc != "JFP")
                        {
                            //insert new loc in inventory table (to loc)
                            dtoresult = GenerateInsertLocTransferInv(com, ctlno, ex_dsc, dsc, reqqty, type, uom, t_loc, cur, usn);
                            dtoresult.cmd.Transaction = trans;
                            dtoresult.cmd.ExecuteNonQuery();
                        }
                      
                    }
                   

                    // insert transfer requisition header
                    dtoresult = GenerateInsert_TransferReqHdr(com, tranx_no, f_loc, t_loc, rmk, req_date, usn);
                    dtoresult.cmd.Transaction = trans;
                    dtoresult.cmd.ExecuteNonQuery();

                    // insert transfer requisition details
                    dtoresult = GenerateInsert_TransferReqDtl(com, ctlno, dsc, reqqty, f_loc, t_loc, tranx_no, refno, usn);
                    dtoresult.cmd.Transaction = trans;
                    dtoresult.cmd.ExecuteNonQuery();


                }
                trans.Commit();
                dtoresult.sts = true;
                return dtoresult;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                erp_con.Close();
            }
        }

     
        #endregion
    }
}