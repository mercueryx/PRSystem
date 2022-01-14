using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace ERP_System.SALES_ORDER.SALES_ORDER_Control
{
    public class SO_da
    {
        SO_dto dtoresult = new SO_dto();
        SqlConnection erp_con = new SqlConnection(ResourceModule.ERP_con);
        SqlConnection ibas_con = new SqlConnection(ResourceModule.IBAS_con);
        SqlTransaction trans;


        #region SC Entry   

        #region Header

        public SO_dto SelectCompany()
        {
            try
            {
                ibas_con.Open();
                SqlDataAdapter cmd = new SqlDataAdapter("select distinct company_code from ar_acct_holder_file", ibas_con);
                DataTable dtcmd = new DataTable();
                cmd.Fill(dtcmd);
                dtoresult.dtCom = dtcmd;
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

        public SO_dto SelectLocation(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select loc,loc+'|  '+dsc as dsc from tbl_npi_loc where sts ='O' and com=@com", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtloc = dtcmd;
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

        public SO_dto SelectGroupCode(string com)
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

        public SO_dto SelectBillTo(string group, string type)
        {
            try
            {
                ibas_con.Open();
                SqlCommand cmd = new SqlCommand("Select ah_code,ah_code+'|  '+ah_name  as dsc from AR_ACCT_HOLDER_FILE where  group_code=@group and local_ah=@type",ibas_con);
               // cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@group", group);
                cmd.Parameters.AddWithValue("@type", type);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtBillTo = dtcmd;
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

        public SO_dto SelectSoldTo( string group)
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
                dtoresult.dtSoldTo = dtcmd;
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

        public SO_dto SelectSignatory(string com)
        {
            try
            {
                ibas_con.Open();
                SqlCommand cmd = new SqlCommand("Select distinct signatory_code,signatory_code+'  |  '+name as dsc  from so_signatory_table ", ibas_con);
               // cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtSignatory = dtcmd;
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

        public SO_dto SelectPaymentTerm(string com, string ah_code)
        {
            try
            {
                ibas_con.Open();
                SqlCommand cmd = new SqlCommand("Select distinct b.term_code ,b.term_code +' | '+b.desc_txt as dsc from AR_ACCT_HOLDER_FILE as a inner join AR_PAYMENT_TERM_TABLE as b on a.company_code = b.company_code and REPLACE(a.term_code, char(9), '') = REPLACE(b.term_code, char(9), '') where a.ah_code=@ahcode",ibas_con);
                //cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@ahcode", ah_code);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtTerm = dtcmd;
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

        #endregion

        #region Details

        public SO_dto SelectCatalogNo(string com)
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
                dtoresult.dtCatalog = dtcmd;
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

        ////test autocomplete
        //public SO_dto SelectCatalogNo_search(string com, string keyword)
        //{
        //    try
        //    {
        //        ibas_con.Open();

        //        SqlCommand cmd = new SqlCommand("select REPLACE( catalog_no, left(catalog_no, 3), '' ) as catalog_no, REPLACE( catalog_no, left(catalog_no, 3), '' )+' | '+description as dsc from INV_INVENTORY_MASTER_FILE where substring(catalog_no, 4,1)='F' and company_code =@com and REPLACE( catalog_no, left(catalog_no, 3), '' ) like '%@ctlno%'", ibas_con);
        //        cmd.Parameters.AddWithValue("@com", com);
        //        SqlDataReader rdr = cmd.ExecuteReader() ;
        //        //DataTable dtcmd = new DataTable();
        //        //adt.Fill(dtcmd);
        //        //dtoresult.dtCatalog = dtcmd;
        //        while (rdr.Read())
        //        {

        //            dtoresult.List_Catalog.Add(rdr.GetString(0));
        //        }
        //        return dtoresult;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally
        //    {
        //        ibas_con.Close();
        //    }
        //}
        public SO_dto SelectUnit_Sell_Price(string catalogno)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select a.unit_price,c.sell_price from tbl_npi_inv_unit_price as a ,tbl_inv as b,tbl_npi_inv_sell_price as c where a.catalog_no=b.catalog_no and a.com=b.com and b.com=c.com and b.catalog_no = c.catalog_no and b.type='F' and a.catalog_no =@catalogno", erp_con);
                cmd.Parameters.AddWithValue("@catalogno", catalogno);
                // cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtprice = dtcmd;
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

        public SO_dto SelectCatalogInfo(string ctlno)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select distinct dsc,ex_dsc from tbl_inv where type='F' and catalog_no =@catalogno", erp_con);
                cmd.Parameters.AddWithValue("@catalogno", ctlno);
                // cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtCtlnoInfo = dtcmd;
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

        public SO_dto SelectUOM()
        {
            try
            {
                ibas_con.Open();
                SqlDataAdapter cmd = new SqlDataAdapter("Select distinct purchase_um from INV_INVENTORY_MASTER_FILE where purchase_um is not null or purchase_um <>''", ibas_con);
                DataTable dtcmd = new DataTable();
                cmd.Fill(dtcmd);
                dtoresult.dtUOM = dtcmd;
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

        #endregion

        #region Insert Data

        public SO_dto SelectSC_RN(string rn)
        {
            try
            {
                erp_con.Close();
                SqlCommand cmd = new SqlCommand("Select top 1 RIGHT(sc_no, LEN(sc_no) - 6) as sc_no from tbl_npi_sc_hdr where sc_no like @sc_no + '%'order by add_dt desc", erp_con);
                cmd.Parameters.AddWithValue("@sc_no", rn);
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

        public SO_dto GenerateInsertHeader(string rn,string com, string group, string type, string billto, string soldto, string payterm, string sc_date, string signatory, string usn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_npi_sc_hdr (com,group_code,order_type,sc_no,sc_date,sold_to,bill_to,pay_term,signatory,sts,add_usn,add_dt) values (@com,@group_code,@order_type,@sc_no,@sc_date,@sold_to,@bill_to,@pay_term,@signatory,'OPEN',@add_usn,GetDate())",erp_con);
                cmd.Parameters.AddWithValue("@com",com);
                cmd.Parameters.AddWithValue("@group_code", group);
                cmd.Parameters.AddWithValue("@order_type", type);
                cmd.Parameters.AddWithValue("@sc_no", rn);
                cmd.Parameters.AddWithValue("@sc_date", sc_date);
                cmd.Parameters.AddWithValue("@sold_to", soldto);
                cmd.Parameters.AddWithValue("@bill_to", billto);
                cmd.Parameters.AddWithValue("@pay_term", payterm);
                cmd.Parameters.AddWithValue("@signatory", signatory);
                cmd.Parameters.AddWithValue("@add_usn", usn);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SO_dto GenerateInsertSCD(string com, string rn, string dtlrn, string ctlno, string dsc, string exdsc, string uom, string qty, string foc_qty, string uprice, string total, string usn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_npi_sc_dtl (com,sc_no,dtl_no,ctlno,dsc,ex_dsc,qty,foc_qty,unit_price,amount,add_usn,add_dt) values (@com,@sc_no,@dtl_no,@ctlno,@dsc,@ex_dsc,@qty,@foc_qty,@unit_price,@amount,@add_usn,GetDate())",erp_con);           
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@sc_no", rn);
                cmd.Parameters.AddWithValue("@dtl_no", dtlrn);
                cmd.Parameters.AddWithValue("@ctlno", ctlno);
                cmd.Parameters.AddWithValue("@dsc", dsc);
                cmd.Parameters.AddWithValue("@ex_dsc", exdsc);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@foc_qty", foc_qty);
                cmd.Parameters.AddWithValue("@unit_price", uprice);
                cmd.Parameters.AddWithValue("@amount", total);
                cmd.Parameters.AddWithValue("@add_usn", com);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SO_dto GenerateInsertSCDD(string rn,string dtlno,string etd,string eta,string qty,string f_qty,string s_qty,string p_qty,string usn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_npi_sc_dtl_info (sc_no,dtl_no,etd,eta,qty,foc_qty,ship_qty,plan_qty,add_usn,add_dt) values (@sc_no,@dtl_no,@etd,@eta,@qty,@foc_qty,@ship_qty,@plan_qty,@add_usn,GetDate())", erp_con);           
                cmd.Parameters.AddWithValue("@sc_no",rn);
                cmd.Parameters.AddWithValue("@dtl_no",dtlno);
                cmd.Parameters.AddWithValue("@etd",etd);
                cmd.Parameters.AddWithValue("@eta",eta);
                cmd.Parameters.AddWithValue("@qty",qty);
                cmd.Parameters.AddWithValue("@foc_qty",f_qty);
                cmd.Parameters.AddWithValue("@ship_qty",s_qty);
                cmd.Parameters.AddWithValue("@plan_qty",p_qty);
                cmd.Parameters.AddWithValue("@add_usn",usn);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SO_dto InsertSalesContract(string com,string rn,string group,string type,string billto,string soldto,string payterm,string sc_date,string signatory,string usn, DataTable dtscd, DataTable dtscdd)
        {
            try
            {
                string dtlrn,ctlno, dsc, exdsc, uom, qty, foc_qty, uprice, total;
                string etd, eta, s_qty, p_qty;
                erp_con.Open();
                trans = erp_con.BeginTransaction();
                //generate sales contract header
                dtoresult = GenerateInsertHeader(rn, com, group, type, billto, soldto, payterm, sc_date, signatory, usn);
                dtoresult.cmd.Transaction = trans;
                dtoresult.cmd.ExecuteNonQuery();

                //generate sales contract details cmd
                for (int i = 0; i < dtscd.Rows.Count; i++)
                {
                    dtlrn = dtscd.Rows[i]["no"].ToString();
                    ctlno = dtscd.Rows[i]["ctlno"].ToString();
                    dsc=dtscd.Rows[i]["desc"].ToString();
                    exdsc=dtscd.Rows[i]["exdesc"].ToString();
                    qty=dtscd.Rows[i]["o_qty"].ToString();
                    foc_qty=dtscd.Rows[i]["foc_qty"].ToString();
                    uom=dtscd.Rows[i]["uom"].ToString();
                    uprice=dtscd.Rows[i]["uprice"].ToString();
                    total=dtscd.Rows[i]["total"].ToString();

                    dtoresult = GenerateInsertSCD(com,rn,dtlrn,ctlno,dsc,exdsc,uom,qty,foc_qty,uprice,total,usn);
                    dtoresult.cmd.Transaction = trans;
                    dtoresult.cmd.ExecuteNonQuery();

                }


                //generate sales contract delivery details cmd
                for (int i = 0; i < dtscdd.Rows.Count; i++)
                {
                    dtlrn = dtscdd.Rows[i]["id"].ToString();
                    etd = dtscdd.Rows[i]["etd"].ToString();
                    eta = dtscdd.Rows[i]["eta"].ToString();
                    qty = dtscdd.Rows[i]["qty"].ToString();
                    foc_qty = dtscdd.Rows[i]["foc_qty"].ToString();
                    s_qty = dtscdd.Rows[i]["s_qty"].ToString();
                    p_qty = dtscdd.Rows[i]["p_qty"].ToString();

                    dtoresult = GenerateInsertSCDD(rn, dtlrn, etd, eta, qty, foc_qty, s_qty,p_qty,usn);
                    dtoresult.cmd.Transaction = trans;
                    dtoresult.cmd.ExecuteNonQuery();

                }
                trans.Commit();
                dtoresult.scno = rn;

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

        #endregion

        #region SC Certify

        public SO_dto SelectSoldTo_BillTo(string type)
        {
            try
            {
                ibas_con.Open();
                SqlCommand cmd = new SqlCommand("Select ah_code,ah_code+'|  '+ah_name  as dsc from AR_ACCT_HOLDER_FILE where local_ah=@type", ibas_con);
                // cmd.Parameters.AddWithValue("@com", com);
                //cmd.Parameters.AddWithValue("@group", group);
                cmd.Parameters.AddWithValue("@type", type);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);

                dtoresult.dtSoldTo = dtcmd;
                dtoresult.dtBillTo = dtcmd;
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

        public SO_dto SelectBillTo_Type(string group, string type)
        {
            try
            {
                ibas_con.Open();
                SqlCommand cmd = new SqlCommand("Select ah_code,ah_code+'|  '+ah_name  as dsc from AR_ACCT_HOLDER_FILE where and local_ah=@type", ibas_con);
                // cmd.Parameters.AddWithValue("@com", com);
                //cmd.Parameters.AddWithValue("@group", group);
                cmd.Parameters.AddWithValue("@type", type);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtBillTo = dtcmd;
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

        public SO_dto SelectSCHeader(string keyword)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select a.sc_no,a.sts,LEFT(CONVERT(VARCHAR, a.sc_date, 120), 10) as sc_date,a.order_type,a.group_code,sum(b.amount)as total, " +
                                                "a.add_usn, a.sold_to, a.bill_to, a.pay_term, a.signatory from tbl_npi_sc_hdr as a inner join tbl_npi_sc_dtl as b on a.com = b.com " +
                                                "and a.sc_no = b.sc_no where " + keyword + " and a.sts<>'CERTIFY' group by a.sc_no, a.sts, a.sc_date, a.order_type, a.group_code, " +
                                                "a.add_usn, a.sold_to, a.bill_to, a.pay_term, a.signatory", erp_con);
                //cmd.Parameters.AddWithValue("@sc_no", sc_no);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtheader = dtcmd;
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

        public SO_dto SelectSCDetails(string sc_no)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select sc_no,dtl_no,ctlno,dsc,qty,foc_qty from tbl_npi_sc_dtl where sc_no=@sc_no",erp_con);
                cmd.Parameters.AddWithValue("@sc_no", sc_no);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtscd = dtcmd;
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

        public SO_dto SelectSCDeliveryDetails(string sc_no,string dtlno)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select dtl_no,LEFT(CONVERT(VARCHAR, etd, 120), 10) as etd,LEFT(CONVERT(VARCHAR, eta, 120), 10) as eta,qty,foc_qty,ship_qty,plan_qty from tbl_npi_sc_dtl_info where sc_no=@scno and dtl_no=@dtlno", erp_con);
                cmd.Parameters.AddWithValue("@scno", sc_no);
                cmd.Parameters.AddWithValue("@dtlno", dtlno);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtscdd = dtcmd;
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

        public SO_dto SelectAllOpenSC(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select sc_no from tbl_npi_sc_hdr where sts ='OPEN' and com=@com order by sc_no desc", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
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

        public SO_dto SelectCertifiedSC(string sc_no)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_npi_sc_hdr where sts ='CERTIFY' and sc_no=@sc_no", erp_con);
                cmd.Parameters.AddWithValue("@sc_no", sc_no);
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

        public SO_dto GenerateSCUpdateSts(string sc_no, string sts, string usn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update tbl_npi_sc_hdr set sts=@sts,udt_usn=@usn,udt_dt=GetDate() where sc_no=@sc_no",erp_con);
                cmd.Parameters.AddWithValue("@sts", sts);
                cmd.Parameters.AddWithValue("@sc_no", sc_no);
                cmd.Parameters.AddWithValue("@usn", usn);
                dtoresult.cmd = cmd;
                return dtoresult;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public SO_dto CertifySC(DataTable dtheader, string usn)
        {
            try
            {              
                string sc_no, sts;
                erp_con.Open();
                trans = erp_con.BeginTransaction();

                for (int i = 0; i < dtheader.Rows.Count; i++)
                {
                    sc_no = dtheader.Rows[i]["sc_no"].ToString();
                    sts = dtheader.Rows[i]["sts"].ToString();
                    //generate update sc status 
                    dtoresult = GenerateSCUpdateSts(sc_no, sts,usn);
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