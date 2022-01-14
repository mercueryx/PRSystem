using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace ERP_System.SALES_ORDER.SALES_ORDER_Control
{
    public class DO_da
    {
        SqlConnection erp_con = new SqlConnection(ResourceModule.ERP_con);
        SqlConnection ibas_con = new SqlConnection(ResourceModule.IBAS_con);
        SqlTransaction trans;
        DO_dto dtoresult = new DO_dto();


        #region Delivery Order Entry

        public DO_dto SelectGroupCode(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select distinct group_code from tbl_npi_group_code where com=@com", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtgroup = dtcmd;
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

        public DO_dto SelectCustomer(string group)
        {
            try
            {
                ibas_con.Open();
                SqlCommand cmd = new SqlCommand("Select ah_code,ah_code+'|  '+ah_name  as dsc from AR_ACCT_HOLDER_FILE where group_code=@group", ibas_con);
                // cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@group", group);
                //cmd.Parameters.AddWithValue("@type", type);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtCus = dtcmd;
                //dtoresult.dtsold = dtcmd;
                //dtoresult.dtship = dtcmd;
                //dtoresult.dtbill = dtcmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                ibas_con.Close();
            }
        }

        public DO_dto SelectPaymentTerm(string ah_code)
        {
            try
            {
                ibas_con.Open();
                SqlCommand cmd = new SqlCommand("Select distinct b.term_code ,b.term_code +' | '+b.desc_txt as dsc from AR_ACCT_HOLDER_FILE as a inner join AR_PAYMENT_TERM_TABLE as b on a.company_code = b.company_code and REPLACE(a.term_code, char(9), '') = REPLACE(b.term_code, char(9), '') where a.ah_code=@ahcode", ibas_con);
                //cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@ahcode", ah_code);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtterm = dtcmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                ibas_con.Close();
            }
        }

        public DO_dto SelectDeliveryTerm()
        {
            try
            {
                ibas_con.Open();
                SqlDataAdapter cmd = new SqlDataAdapter("Select distinct delivery_term_code,delivery_term_code+'    |   '+delivery_term_desc as dsc from so_delivery_term_table ", ibas_con);
                DataTable dtcmd = new DataTable();
                cmd.Fill(dtcmd);
                dtoresult.dtdterm = dtcmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                ibas_con.Close();
            }
        }

        public DO_dto SelectCertifySC(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select sc_no from tbl_npi_sc_hdr where com=@com and sts='CERTIFY' order by sc_no desc", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtsc_no = dtcmd;
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

        public DO_dto SelectSC_Catalog(string com, string sc_no)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select ctlno, ctlno+'  |   '+dsc as dsc from tbl_npi_sc_dtl where sc_no=@sc_no and com=@com",erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@sc_no", sc_no);
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

        public DO_dto SelectCatalog_dsc(string sc_no, string ctlno)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select dsc from tbl_npi_sc_dtl where sc_no=@sc_no and ctlno=@ctlno", erp_con);
                cmd.Parameters.AddWithValue("@sc_no", sc_no);
                cmd.Parameters.AddWithValue("@ctlno", ctlno);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtdsc = dtcmd;
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

        public DO_dto SelectUOM()
        {
            try
            {
                ibas_con.Open();
                SqlDataAdapter cmd = new SqlDataAdapter("Select distinct purchase_um from INV_INVENTORY_MASTER_FILE where purchase_um is not null or purchase_um <>''", ibas_con);
                DataTable dtcmd = new DataTable();
                cmd.Fill(dtcmd);
                dtoresult.dtuom = dtcmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                ibas_con.Close();
            }
        }

        public DO_dto SelectFoc_CatalogNo(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select  catalog_no,catalog_no+' | '+dsc as dsc from tbl_inv where SUBSTRING(catalog_no, 1, 1)='F' and com =@com", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                //REPLACE(catalog_no, left(catalog_no, 3), '') as
                //substring(catalog_no, 4, 1) = 'F'
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtf_ctlno = dtcmd;
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

        public DO_dto SelectFOC_Catalog_dsc(string com, string ctlno)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select dsc from tbl_inv where com=@com and catalog_no=@ctlno", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@ctlno", ctlno);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtf_dsc = dtcmd;
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DO_dto SelectDO_RN(string rn)
        {
            try
            {
                erp_con.Close();
                SqlCommand cmd = new SqlCommand("Select top 1 RIGHT(do_no, LEN(do_no) - 6) as do_no from tbl_npi_do_hdr where do_no like @do_no + '%' order by add_dt desc", erp_con);
                cmd.Parameters.AddWithValue("@do_no", rn);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtdo_rn = dtcmd;
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

        public DO_dto SelectInv_RN(string rn)
        {
            try
            {
                erp_con.Close();
                SqlCommand cmd = new SqlCommand("Select top 1 RIGHT(inv_no, LEN(inv_no) - 6) as inv_no from tbl_npi_do_hdr where inv_no like @inv_no + '%' order by add_dt desc", erp_con);
                cmd.Parameters.AddWithValue("@inv_no", rn);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtinv_rn = dtcmd;
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

        public DO_dto GenerateInsertHdr(string do_no, string inv_no, string type, string bill, string sold, string ship, string d_term, string p_term, string d_date, string etd, string eta, string usn,string com)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_npi_do_hdr (do_no,inv_no,do_dt,order_type,sold_to,bill_to,ship_to,payment,delivery_term,etd,eta,sts,add_dt,add_usn,com) values (@do_no,@inv_no,@do_dt,@order_type,@sold_to,@bill_to,@ship_to,@payment,@delivery_term,@etd,@eta,'OPEN',GetDate(),@add_usn,@com)", erp_con);
                cmd.Parameters.AddWithValue("@do_no", do_no);
                cmd.Parameters.AddWithValue("@inv_no", inv_no);
                cmd.Parameters.AddWithValue("@do_dt", d_date);
                cmd.Parameters.AddWithValue("@order_type", type);
                cmd.Parameters.AddWithValue("@sold_to", sold);
                cmd.Parameters.AddWithValue("@bill_to", bill);
                cmd.Parameters.AddWithValue("@ship_to", ship);
                cmd.Parameters.AddWithValue("@payment", p_term);
                cmd.Parameters.AddWithValue("@delivery_term", d_term);
                cmd.Parameters.AddWithValue("@etd", etd);
                cmd.Parameters.AddWithValue("@eta", eta);
                cmd.Parameters.AddWithValue("@add_usn", usn);
                cmd.Parameters.AddWithValue("@com", com);
                dtoresult.cmd = cmd;
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DO_dto GenerateInsertDtl(string do_no,string inv_no,string scno,string ctlno,string dsc,string uom,string qty,string foc_qty,string amount,string usn,string com)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_npi_do_dtl (do_no,inv_no,sc_no,ctlno,dsc,uom,qty,foc_qty,amount,add_dt,add_usn,com) values (@do_no,@inv_no,@sc_no,@ctlno,@dsc,@uom,@qty,@foc_qty,@amount,GetDate(),@add_usn,@com)", erp_con);
                cmd.Parameters.AddWithValue("@do_no", do_no);
                cmd.Parameters.AddWithValue("@inv_no", inv_no);
                cmd.Parameters.AddWithValue("@sc_no", scno);
                cmd.Parameters.AddWithValue("@ctlno", ctlno);
                cmd.Parameters.AddWithValue("@dsc", dsc);
                cmd.Parameters.AddWithValue("@uom", uom);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@foc_qty", foc_qty);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@add_usn", usn);
                cmd.Parameters.AddWithValue("@com", com);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DO_dto GenerateInsertFOc(string do_no,string inv_no,string ctlno,string i_dsc,string uom,string qty,string claim,string gift,string dsc,string rmk,string usn,string com)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_npi_do_dtl_foc (do_no,inv_no,foc_ctlno,dsc,uom,qty,claim_no,gift_code,ex_dsc,rmk,add_dt,add_usn,com) values (@do_no,@inv_no,@foc_ctlno,@dsc,@uom,@qty,@claim_no,@gift_code,@ex_dsc,@rmk,GetDate(),@add_usn,@com)", erp_con);
                cmd.Parameters.AddWithValue("@do_no", do_no);
                cmd.Parameters.AddWithValue("@inv_no", inv_no);
                cmd.Parameters.AddWithValue("@foc_ctlno", ctlno);
                cmd.Parameters.AddWithValue("@dsc", i_dsc);
                cmd.Parameters.AddWithValue("@uom", uom);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@claim_no", claim);
                cmd.Parameters.AddWithValue("@gift_code", gift);
                cmd.Parameters.AddWithValue("@ex_dsc", dsc);
                cmd.Parameters.AddWithValue("@rmk", rmk);
                cmd.Parameters.AddWithValue("@add_usn", usn);
                cmd.Parameters.AddWithValue("@com", com);
                dtoresult.cmd = cmd;
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DO_dto InsertDeliveryDO(string do_no, string inv_no, string type, string bill, string sold, string ship, string d_term, string p_term, string d_date, string etd, string eta, string usn,DataTable dtdtl,DataTable dtfoc,string com)
        {
            try
            {
                string sc_no, ctlno, dsc, uom, qty, foc_qty, amount;
                string f_ctlno, f_dsc, f_uom, f_qty, f_claim, f_gift, f_desc, f_rmk;

                erp_con.Open();
                trans = erp_con.BeginTransaction();

                //generate do hdr cmd
                dtoresult = GenerateInsertHdr(do_no, inv_no, type, bill, sold, ship, d_term, p_term, d_date, etd, eta, usn,com);
                dtoresult.cmd.Transaction = trans;
                dtoresult.cmd.ExecuteNonQuery();

                //generate do details cmd
                for (int i = 0; i < dtdtl.Rows.Count; i++)
                {
                    sc_no = dtdtl.Rows[i]["sc_no"].ToString();
                    ctlno = dtdtl.Rows[i]["ctlno"].ToString();
                    dsc = dtdtl.Rows[i]["dsc"].ToString();
                    uom = dtdtl.Rows[i]["uom"].ToString();
                    qty = dtdtl.Rows[i]["qty"].ToString();
                    foc_qty = dtdtl.Rows[i]["foc_qty"].ToString();
                    amount = dtdtl.Rows[i]["amount"].ToString();

                    dtoresult = GenerateInsertDtl(do_no,inv_no,sc_no,ctlno,dsc,uom,qty,foc_qty,amount,usn,com);
                    dtoresult.cmd.Transaction = trans;
                    dtoresult.cmd.ExecuteNonQuery();

                }

                //generate do foc details cmd
                if(dtfoc.Rows.Count>0)
                {
                    for (int i = 0; i < dtfoc.Rows.Count; i++)
                    {

                        f_ctlno = dtfoc.Rows[i]["ctlno"].ToString();
                        f_dsc = dtfoc.Rows[i]["i_dsc"].ToString();
                        f_uom = dtfoc.Rows[i]["uom"].ToString();
                        f_qty = dtfoc.Rows[i]["qty"].ToString();
                        f_claim = dtfoc.Rows[i]["claim"].ToString();
                        f_gift = dtfoc.Rows[i]["gift"].ToString();
                        f_desc = dtfoc.Rows[i]["dsc"].ToString();
                        f_rmk = dtfoc.Rows[i]["rmk"].ToString();

                        dtoresult = GenerateInsertFOc(do_no, inv_no, f_ctlno, f_dsc, f_uom, f_qty, f_claim, f_gift, f_desc, f_rmk, usn,com);
                        dtoresult.cmd.Transaction = trans;
                        dtoresult.cmd.ExecuteNonQuery();

                    }
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

        #region Certify Delivery Order

        public DO_dto SelectOpenDO(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select do_no from tbl_npi_do_hdr where com=@com and sts ='OPEN'", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtdo_rn = dtcmd;
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

        public DO_dto SelectOpenInvNo(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select inv_no from tbl_npi_do_hdr where sts = 'OPEN' and com=@com", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtinv_rn = dtcmd;
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

        public DO_dto SelectDOHdr(string keyword)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select a.sts,a.do_no,a.do_dt as d_date,a.inv_no,a.sold_to as sold,a.bill_to as bill,a.ship_to as ship,a.payment as pay_term,"+
                                                "a.delivery_term as d_term, a.add_usn as usn, a.add_dt, sum(b.amount) as t_amount "+
                                                "from tbl_npi_do_hdr as a inner join tbl_npi_do_dtl as b "+
                                                "on a.do_no = b.do_no and a.com = b.com where " + keyword + " and a.sts<>'CERTIFY' group by a.sts, a.do_no, a.do_dt, a.inv_no, a.sold_to,"+
                                                "a.bill_to, a.ship_to, a.payment, a.delivery_term, a.add_usn, a.add_dt ", erp_con);              
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dthdr = dtcmd;
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

        public DO_dto SelectDODtl(string do_no, string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select sc_no,ctlno,dsc,uom,qty,foc_qty from tbl_npi_do_dtl where do_no=@do_no and com=@com", erp_con);
                cmd.Parameters.AddWithValue("@do_no", do_no);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtdtl = dtcmd;
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

        public DO_dto SelectDOFoc(string do_no, string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select foc_ctlno,dsc,uom,claim_no,gift_code,qty,rmk from tbl_npi_do_dtl_foc where do_no=@do_no and com=@com", erp_con);
                cmd.Parameters.AddWithValue("@do_no", do_no);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtfoc = dtcmd;
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

        public DO_dto SelectCertifiedDO(string do_no)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_npi_do_hdr where sts ='CERTIFY' and do_no=@do_no", erp_con);
                cmd.Parameters.AddWithValue("@do_no", do_no);
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

        public DO_dto GenerateDOUpdateSts(string do_no, string sts, string usn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update tbl_npi_do_hdr set sts=@sts,udt_usn=@usn,udt_dt=GetDate() where do_no=@do_no", erp_con);
                cmd.Parameters.AddWithValue("@sts", sts);
                cmd.Parameters.AddWithValue("@do_no", do_no);
                cmd.Parameters.AddWithValue("@usn", usn);
                dtoresult.cmd = cmd;
                return dtoresult;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public DO_dto CertifyDO(DataTable dtheader, string usn)
        {
            try
            {
                string do_no, sts;
                erp_con.Open();
                trans = erp_con.BeginTransaction();

                for (int i = 0; i < dtheader.Rows.Count; i++)
                {
                    do_no = dtheader.Rows[i]["do_no"].ToString();
                    sts = dtheader.Rows[i]["sts"].ToString();
                    //generate update sc status 
                    dtoresult = GenerateDOUpdateSts(do_no, sts, usn);
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