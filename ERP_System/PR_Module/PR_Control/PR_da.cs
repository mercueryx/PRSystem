using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ERP_System.PR_Module.Models;

namespace ERP_System.PR_Module.PR_Control
{
    public class PR_da
    {
        PR_dto dtoresult = new PR_dto();
        SqlConnection helpdesk_con = new SqlConnection(ResourceModule.HD_con);
        SqlConnection ibas_con = new SqlConnection(ResourceModule.IBAS_con);
        SqlConnection erp_con = new SqlConnection(ResourceModule.ERP_con);
        SqlTransaction trans;


        #region Login
        public PR_dto SelectAllUser()
        {
            try
            {
                helpdesk_con.Open();
                SqlDataAdapter cmd = new SqlDataAdapter("Select * from tblUser", helpdesk_con);
                DataTable dtcmd = new DataTable();
                cmd.Fill(dtcmd);
                dtoresult.dtuser = dtcmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                helpdesk_con.Close();
            }
        }
        #endregion


        #region PR Entry

        public PR_dto SelectSection(string com,string dpt)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select distinct sec from tbl_user_dpt where com=@com and dpt=@dpt", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@dpt", dpt);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtsec = dtcmd;
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

        public PR_dto SelectAllCompany()
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select distinct company_code from tbl_npi_vendor_master ", erp_con);
              
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtcom = dtcmd;
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

        public PR_dto SelectItemRef(string desc, string com)
        {
            try
            {
                ibas_con.Open();
                //SqlCommand cmd = new SqlCommand("select distinct item_name from tbl_pr_item where item_name like '%' + @item + '%' and com=@com", erp_con);
                SqlCommand cmd = new SqlCommand("select distinct product_ref from inv_inventory_master_file where description=@desc and company_code=@com ", ibas_con);
                cmd.Parameters.AddWithValue("@desc", desc);
                cmd.Parameters.AddWithValue("@com", com);
                //cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtref = dtcmd;
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

        public PR_dto SelectAllUOM()
        {
            try
            {
                ibas_con.Open();
                SqlCommand cmd = new SqlCommand("Select distinct purchase_um from inv_inventory_master_file where purchase_um <> '' and purchase_um is not null", ibas_con);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtUOM = dtcmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;

            }
            finally
            {

            }

        }
           
        public PR_dto SelectUOM_desc(string desc)
        {
            try
            {
                ibas_con.Open();
                SqlCommand cmd = new SqlCommand("select distinct purchase_um from inv_inventory_master_file where  description=@desc and purchase_um <> '' and purchase_um is not null", ibas_con);
                //company_code = @com and
                //cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@desc", desc);

                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);

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

        public PR_dto SelectDepartment(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select distinct dpt from tbl_user_dpt where com=@com", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtdpt = dtcmd;
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

        public PR_dto SelectItem_local(string prefix,string com)
        {
            try
            {
                erp_con.Open();
                //SqlCommand cmd = new SqlCommand("select distinct item_name from tbl_pr_item where item_name like '%' + @item + '%' and com=@com", erp_con);
                SqlCommand cmd = new SqlCommand("select distinct item_name from tbl_pr_item where item_name like '%' + @item + '%'", erp_con);
                cmd.Parameters.AddWithValue("@item", prefix);
                //cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        public PR_dto SelectItem_ibas(string prefix,string com)
        {
            try
            {
                ibas_con.Open();
                //SqlCommand cmd = new SqlCommand("select distinct top 10 description as item_name from inv_inventory_master_file where description like '%' + @item + '%' and company_code=@com", ibas_con);
                SqlCommand cmd = new SqlCommand("select distinct top 10 description as item_name from inv_inventory_master_file where description like '%' + @item + '%' and left(catalog_no,4) in ('rs1s','ro1o','cs1s','co1o','mo1o','ms1s')", ibas_con);
                cmd.Parameters.AddWithValue("@item", prefix);
                //cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        public PR_dto InsertNewItem(string item_name,string usn)
        {
            try
            {

                erp_con.Open();

                SqlCommand cmd = new SqlCommand("Insert into tbl_pr_item (item_name,dt,usn) values (@item_name,GetDate(),@usn)", erp_con);
                cmd.Parameters.AddWithValue("@item_name", item_name);
                cmd.Parameters.AddWithValue("@usn", usn);
                cmd.ExecuteNonQuery();
                dtoresult.sts = true;
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

        public PR_dto SelectExistItem(string item)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select item_name from tbl_pr_item where item_name=@item", erp_con);
                cmd.Parameters.AddWithValue("@item", item);
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

        public PR_dto SelectPR_RN(string rn)
        {
            try
            {
                erp_con.Close();
                SqlCommand cmd = new SqlCommand("Select top 1 RIGHT(pr_rn, LEN(pr_rn) - 6) as pr_rn from tbl_pr_hdr where pr_rn like @pr_rn + '%'order by add_dt desc", erp_con);
                cmd.Parameters.AddWithValue("@pr_rn", rn);
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

        public PR_dto GeneratePRHeader(string com,string req_com, string pr_rn, string req_dpt,string dpt,string sec,string nm,string usn,string name)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("Insert into tbl_pr_hdr (com,req_com,pr_rn,req_dpt,dpt,sec,nm,sts,add_dt,add_usn,add_nm) values (@com,@req_com,@pr_rn,@req_dpt,@dpt,@sec,@nm,'OPEN',GetDate(),@add_usn,@add_nm)", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@req_com", req_com);
                cmd.Parameters.AddWithValue("@pr_rn", pr_rn);
                cmd.Parameters.AddWithValue("@nm", nm);
                cmd.Parameters.AddWithValue("@req_dpt", req_dpt);
                cmd.Parameters.AddWithValue("@dpt", dpt);
                cmd.Parameters.AddWithValue("@sec", sec);
                cmd.Parameters.AddWithValue("@add_usn", usn);
                cmd.Parameters.AddWithValue("@add_nm", name);
                dtoresult.cmd = cmd;
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public PR_dto GeneratePRDetails(string pr_rn, string item, string qty, string purpose,string lvl,string usn,string uom)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_pr_dtl (pr_rn,item_name,qty,purpose,lvl,sts,uom) values (@pr_rn,@item_name,@qty,@purpose,@lvl,'OPEN',@uom)", erp_con);
                cmd.Parameters.AddWithValue("@pr_rn", pr_rn);
                cmd.Parameters.AddWithValue("@item_name", item);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@lvl", lvl);
                cmd.Parameters.AddWithValue("@purpose", purpose);
                cmd.Parameters.AddWithValue("@uom", uom);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PR_dto InsertPR(string com,string req_com, string pr_rn,string req_dpt, string dpt, string sec,string nm, string usn,string name, DataTable dtitem)
        {
            try
            {
                string item,qty,purpose,lvl,uom;
                erp_con.Open();
                trans = erp_con.BeginTransaction();
                //generate po hdr cmd
                dtoresult = GeneratePRHeader(com,req_com,pr_rn,req_dpt,dpt,sec,nm,usn,name);
                dtoresult.cmd.Transaction = trans;
                dtoresult.cmd.ExecuteNonQuery();

                //generate po details cmd
                for (int i = 0; i < dtitem.Rows.Count; i++)
                {
                    item = dtitem.Rows[i]["item_name"].ToString();
                    qty = dtitem.Rows[i]["qty"].ToString();
                    purpose = dtitem.Rows[i]["purpose"].ToString();
                    lvl = dtitem.Rows[i]["level"].ToString();
                    uom = dtitem.Rows[i]["uom"].ToString();
                    //dtoresult = SelectExistItem(item);
                    //if (dtoresult.dtcheck.Rows.Count == 0)
                    //{
                    //    dtoresult = GenerateInsertNewItem(item, usn);
                    //    dtoresult.cmd.Transaction = trans;
                    //    dtoresult.cmd.ExecuteNonQuery();
                    //}
                    dtoresult = GeneratePRDetails(pr_rn,item,qty,purpose,lvl,usn,uom);
                    dtoresult.cmd.Transaction = trans;
                    dtoresult.cmd.ExecuteNonQuery();

                }
                trans.Commit();
                dtoresult.pr_rn = pr_rn;

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


        #region PR Edit
        public PR_dto SelectOpenPR(string usn,string requestdate)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select pr_rn from tbl_pr_hdr where sts ='OPEN' and add_usn=@nm and LEFT(CONVERT(VARCHAR, add_dt, 120), 10)=@dt", erp_con);
                cmd.Parameters.AddWithValue("@nm", usn);
                cmd.Parameters.AddWithValue("@dt", requestdate);
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

        public PR_dto SelectAllOpenPR(string usn)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select pr_rn from tbl_pr_hdr where sts ='OPEN' and add_usn=@nm", erp_con);
                cmd.Parameters.AddWithValue("@nm", usn);
               // cmd.Parameters.AddWithValue("@dt", requestdate);
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

        public PR_dto SelectPRHeader(string pr_rn)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select com,pr_rn,dpt,sec,req_dpt,req_com,nm,sts from tbl_pr_hdr where pr_rn=@pr_rn", erp_con);
                cmd.Parameters.AddWithValue("@pr_rn", pr_rn);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtpr_hdr = dtcmd;
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

        public PR_dto SelectPRDetails(string pr_rn)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_pr_dtl where pr_rn=@pr_rn and sts='OPEN'", erp_con);
                cmd.Parameters.AddWithValue("@pr_rn", pr_rn);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtpr_dtl = dtcmd;
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

        public PR_dto SelectCountItemLeft(string pr_rn)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select item_name from tbl_pr_dtl where pr_rn=@rn", erp_con);
                //cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@rn", pr_rn);
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

        public PR_dto SelectSamePR_Item(string pr_rn, string item)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_pr_dtl where pr_rn=@pr_rn and item_name=@item", erp_con);
                cmd.Parameters.AddWithValue("@pr_rn", pr_rn);
                cmd.Parameters.AddWithValue("@item", item);
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

        public PR_dto UpdatePRHDR(string req_com,string pr_rn, string nm,string req_dpt,string sec)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Update tbl_pr_hdr set req_com=@req_com,nm=@nm,req_dpt=@dpt,sec=@sec where pr_rn=@rn", erp_con);
                cmd.Parameters.AddWithValue("@req_com", req_com);
                cmd.Parameters.AddWithValue("@nm", nm);
                cmd.Parameters.AddWithValue("@dpt", req_dpt);
                cmd.Parameters.AddWithValue("@rn", pr_rn);
                cmd.Parameters.AddWithValue("@sec", sec);
                cmd.ExecuteNonQuery();
                dtoresult.sts = true;
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

        public PR_dto AddNewPRDtl(string pr_rn,string item,string qty,string purpose,string lvl,string usn,string uom)
        {
            try
            {
                erp_con.Open();
                dtoresult = GeneratePRDetails(pr_rn,item,qty,purpose,lvl,usn,uom);
                dtoresult.cmd.ExecuteNonQuery();
                dtoresult.sts = true;
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

        public PR_dto DeletePR_Item(string pr_rn, string id)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Delete from tbl_pr_dtl where pr_rn=@rn and id=@id", erp_con);
                cmd.Parameters.AddWithValue("@rn", pr_rn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                dtoresult.sts = true;
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

        #endregion


        #region PR Certiy

        public PR_dto SelectAll_P_C_PR(string id)
        {
            try
            {

                erp_con.Open();

                //SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req,a.req_dpt as dpt,a.req_com,Left(convert(varchar,a.add_dt,120),10) as req_dt, " +
                //                                "b.item_name as itm, b.qty as qty, b.purpose as pur, b.lvl as lvl,a.add_nm as add_usn from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                //                                "on a.pr_rn = b.pr_rn where a.sts in ('PENDING', 'OPEN') and b.sts = 'OPEN' and a.sec in (select pic_sec from tbl_pic_dpt_sec where login_id=@id) ", erp_con);

                SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req,a.req_dpt as dpt, a.sec,a.req_com,Left(convert(varchar,a.add_dt,120),10) as req_dt, " +
                                              "b.item_name as itm, b.qty as qty, b.purpose as pur, b.lvl as lvl,a.add_nm as add_usn from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                                              "on a.pr_rn = b.pr_rn where a.sts in ('PENDING', 'OPEN') and b.sts = 'OPEN' and a.req_dpt in (select pic_dpt from tbl_pic_dpt_sec where login_id=@id) ", erp_con);

                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        public PR_dto SelectAll_OpenPR_IT()
        {
            try
            {

                erp_con.Open();

                //SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req,a.req_dpt as dpt,a.req_com,Left(convert(varchar,a.add_dt,120),10) as req_dt, " +
                //                                "b.item_name as itm, b.qty as qty, b.purpose as pur, b.lvl as lvl,a.add_nm as add_usn from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                //                                "on a.pr_rn = b.pr_rn where a.sts in ('PENDING', 'OPEN') and b.sts = 'OPEN' and a.sec in (select pic_sec from tbl_pic_dpt_sec where login_id=@id) ", erp_con);

                SqlDataAdapter cmd = new SqlDataAdapter("select b.id, a.pr_rn as pr_rn, a.nm as req,a.req_dpt as dpt, a.sec,a.req_com,Left(convert(varchar,a.add_dt,120),10) as req_dt, " +
                                              "b.item_name as itm, b.qty as qty, b.purpose as pur, b.lvl as lvl,a.add_nm as add_usn from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                                              "on a.pr_rn = b.pr_rn where a.sts in ('PENDING', 'OPEN') and b.sts = 'OPEN' ", erp_con);

                //cmd.Parameters.AddWithValue("@id", id);
                //SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                cmd.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        public PR_dto SelectP_C_PR_byRequestor(string id, string requestor)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req,a.req_dpt as dpt,a.req_com,Left(convert(varchar,a.add_dt,120),10) as req_dt, " +
                                                "b.item_name as itm, b.qty as qty, b.purpose as pur, b.lvl as lvl,a.add_nm as add_usn  from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                                                "on a.pr_rn = b.pr_rn where a.sts in ('PENDING', 'OPEN') and b.sts = 'OPEN' and a.req_dpt in  (select pic_dpt from tbl_pic_dpt_sec where login_id=@id) and a.nm=@nm", erp_con);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nm", requestor);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        public PR_dto SelectP_C_PR_byRequestDate(string id, string req_dt)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req,a.req_dpt as dpt,a.req_com,Left(convert(varchar,a.add_dt,120),10) as req_dt, " +
                                                "b.item_name as itm, b.qty as qty, b.purpose as pur, b.lvl as lvl,a.add_nm as add_usn  from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                                                "on a.pr_rn = b.pr_rn where a.sts in ('PENDING', 'OPEN') and b.sts = 'OPEN' and a.req_dpt in  (select pic_dpt from tbl_pic_dpt_sec where login_id=@id) and LEFT(CONVERT(VARCHAR, a.add_dt, 120), 10)=@req_dt", erp_con);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@req_dt", req_dt);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        public PR_dto SelectP_C_PR_byRequestDate_Requestor(string id, string req_dt,string nm)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req,a.req_dpt as dpt,a.req_com,Left(convert(varchar,a.add_dt,120),10) as req_dt, " +
                                                "b.item_name as itm, b.qty as qty, b.purpose as pur, b.lvl as lvl,a.add_nm as add_usn  from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                                                "on a.pr_rn = b.pr_rn where a.sts in ('PENDING', 'OPEN') and b.sts = 'OPEN' and a.req_dpt in  (select pic_dpt from tbl_pic_dpt_sec where login_id=@id) and LEFT(CONVERT(VARCHAR, a.add_dt, 120), 10)=@req_dt and a.nm=@nm", erp_con);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@req_dt", req_dt);
                cmd.Parameters.AddWithValue("@nm", nm);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        // Louis Added on 20200818

        public PR_dto SelectP_C_PR_byRequestDate_Requestor_Enhance(string id, string sec, string req_dt, string nm, string dept)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req,a.req_dpt as dpt,a.req_com,Left(convert(varchar,a.add_dt,120),10) as req_dt, " +
                                                "b.item_name as itm, b.qty as qty, b.purpose as pur, b.lvl as lvl,a.add_nm as add_usn  from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                                                "on a.pr_rn = b.pr_rn where a.sts in ('PENDING', 'OPEN') and b.sts = 'OPEN' and a.req_dpt in  (select pic_dpt from tbl_pic_dpt_sec where login_id=@id) and LEFT(CONVERT(VARCHAR, a.add_dt, 120), 10)=@req_dt and a.nm=@nm", erp_con);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@req_dt", req_dt);
                cmd.Parameters.AddWithValue("@nm", nm);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        // END

        public PR_dto SelectPR_Status(string rn,string id)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_pr_dtl where sts <>'OPEN' and pr_rn=@rn and id=@id", erp_con);
                cmd.Parameters.AddWithValue("@rn", rn);
                cmd.Parameters.AddWithValue("@id", id);
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

        public PR_dto GenerateUpdateDTL_sts(string id, string pr_rn,string sts,string usn,string name)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update tbl_pr_dtl set sts=@sts,verify_nm=@verify_nm,verify_by=@by,verify_dt=GetDate() where id=@id and pr_rn=@rn", erp_con);
                cmd.Parameters.AddWithValue("@sts", sts);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@rn", pr_rn);
                cmd.Parameters.AddWithValue("@by", usn);
                cmd.Parameters.AddWithValue("@verify_nm", name);
                dtoresult.cmd=cmd;
                return dtoresult;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public PR_dto UpdatePR_sts(DataTable dtdtl, string usn,string name)
        {
            try
            {
                erp_con.Open();
                trans = erp_con.BeginTransaction();
                string id, pr_rn, sts;
                for (int i = 0; i < dtdtl.Rows.Count; i++)
                {
                    id = dtdtl.Rows[i]["id"].ToString();
                    pr_rn = dtdtl.Rows[i]["rn"].ToString();
                    sts = dtdtl.Rows[i]["sts"].ToString();

                    if (sts != "OPEN")
                    {
                        dtoresult = GenerateUpdateDTL_sts(id, pr_rn, sts, usn,name);
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

        public PR_dto UpdatePR_Sts()
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE R SET R.sts = 'PENDING' FROM tbl_pr_hdr AS R INNER JOIN tbl_pr_dtl AS P ON R.pr_rn = P.pr_rn " +
                                                "WHERE(select count(a.sts) from tbl_pr_dtl as a where a.sts = 'OPEN' and a.pr_rn = R.pr_rn) = 0 " +
                                                "and(select count(a.sts) from tbl_pr_dtl as a where a.sts in ('REJECTED', 'CERTIFIED') and a.pr_rn = R.pr_rn) > 0; " +
                                                "UPDATE R SET R.sts = 'REJECTED' FROM tbl_pr_hdr AS R INNER JOIN tbl_pr_dtl AS P ON R.pr_rn = P.pr_rn " +
                                                "WHERE(select count(a.sts) from tbl_pr_dtl as a where a.sts in ('OPEN', 'CERTIFIED') and a.pr_rn = R.pr_rn) = 0 " +
                                                "and(select count(a.sts) from tbl_pr_dtl as a where a.sts = 'REJECTED' and a.pr_rn = R.pr_rn) > 0; " +
                                                "UPDATE R SET R.sts = 'CLOSED' FROM tbl_pr_hdr AS R INNER JOIN tbl_pr_dtl AS P ON R.pr_rn = P.pr_rn " +
                                                "WHERE(select count(a.sts) from tbl_pr_dtl as a where a.sts in ('OPEN', 'CERTIFIED') and a.pr_rn = R.pr_rn) = 0 " +
                                                "and(select count(a.sts) from tbl_pr_dtl as a where a.sts in ('REJECTED', 'APPROVED') and a.pr_rn = R.pr_rn) > 0", erp_con);
                cmd.ExecuteNonQuery();
                dtoresult.sts = true;
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


        //public PR_dto UpdatePR_Sts_Reject()
        //{
        //    try
        //    {
        //        erp_con.Open();
        //        SqlCommand cmd = new SqlCommand("UPDATE R SET R.sts = 'REJECTED' FROM tbl_pr_hdr AS R INNER JOIN tbl_pr_dtl AS P ON R.pr_rn = P.pr_rn "+
        //                                        "WHERE(select count(a.sts) from tbl_pr_dtl as a where a.sts in ('OPEN', 'CERTIFIED') and a.pr_rn = R.pr_rn) = 0 "+
        //                                        "and(select count(a.sts) from tbl_pr_dtl as a where a.sts = 'REJECTED' and a.pr_rn = R.pr_rn) > 0", erp_con);
        //        cmd.ExecuteNonQuery();
        //        dtoresult.sts = true;
        //        return dtoresult;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally
        //    {
        //        erp_con.Close();
        //    }
        //}


        //public PR_dto UpdatePR_Sts_Closed()
        //{
        //    try
        //    {
        //        erp_con.Open();
        //        SqlCommand cmd = new SqlCommand("UPDATE R SET R.sts = 'CLOSED' FROM tbl_pr_hdr AS R INNER JOIN tbl_pr_dtl AS P ON R.pr_rn = P.pr_rn "+
        //                                        "WHERE(select count(a.sts) from tbl_pr_dtl as a where a.sts in ('OPEN', 'CERTIFIED') and a.pr_rn = R.pr_rn) = 0 "+
        //                                        "and(select count(a.sts) from tbl_pr_dtl as a where a.sts = 'REJECTED' and a.pr_rn = R.pr_rn) > 0 "+
        //                                        "and(select count(a.sts) from tbl_pr_dtl as a where a.sts = 'APPROVED' and a.pr_rn = R.pr_rn) > 0", erp_con);
        //        cmd.ExecuteNonQuery();
        //        dtoresult.sts = true;
        //        return dtoresult;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally
        //    {
        //        erp_con.Close();
        //    }
        //}
        #endregion


        #region PR Approve

        #region Normal User

        public PR_dto Select_Certified_PR(string id)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req, a.dpt as dpt,a.req_com, Left(convert(varchar, a.add_dt, 120), 10) as req_dt, " +
                                                "b.item_name as itm, b.qty as qty, b.purpose as pur, b.lvl as lvl,a.add_nm as add_usn,b.verify_nm as verify_by,Left(convert(varchar,b.verify_dt,120),10) as verify_dt from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                                                "on a.pr_rn = b.pr_rn where b.sts = 'CERTIFIED' and a.req_dpt in (select pic_dpt from tbl_pic_dpt_sec where login_id=@id) ", erp_con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        public PR_dto SelectCertified_PR_byRequestor(string id, string requestor)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req,a.req_dpt as dpt,a.req_com,Left(convert(varchar,a.add_dt,120),10) as req_dt, " +
                                                "b.item_name as itm, b.qty as qty, b.purpose as pur, b.lvl as lvl,a.add_nm as add_usn,b.verify_nm as verify_by,Left(convert(varchar,b.verify_dt,120),10) as verify_dt from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                                                "on a.pr_rn = b.pr_rn where b.sts = 'CERTIFIED' and a.req_dpt in (select pic_dpt from tbl_pic_dpt_sec where login_id=@id) and a.nm=@nm", erp_con);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nm", requestor);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        public PR_dto SelectCertified_PR_byRequestDate(string id, string req_dt)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req,a.req_dpt as dpt,a.req_com,Left(convert(varchar,a.add_dt,120),10) as req_dt, " +
                                                "b.item_name as itm, b.qty as qty, b.purpose as pur, b.lvl as lvl,a.add_nm as add_usn,b.verify_nm as verify_by,Left(convert(varchar,b.verify_dt,120),10) as verify_dt from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                                                "on a.pr_rn = b.pr_rn where b.sts = 'CERTIFIED' and a.req_dpt in (select pic_dpt from tbl_pic_dpt_sec where login_id=@id) and LEFT(CONVERT(VARCHAR, a.add_dt, 120), 10)=@req_dt", erp_con);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@req_dt", req_dt);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        public PR_dto SelectCertified_PR_byRequestDate_Requestor(string id, string req_dt, string nm)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req,a.req_dpt as dpt,a.req_com,Left(convert(varchar,a.add_dt,120),10) as req_dt, " +
                                                "b.item_name as itm, b.qty as qty, b.purpose as pur, b.lvl as lvl,a.add_nm as add_usn,b.verify_nm as verify_by,Left(convert(varchar,b.verify_dt,120),10) as verify_dt from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                                                "on a.pr_rn = b.pr_rn where b.sts = 'CERTIFIED' and a.req_dpt in (select pic_dpt from tbl_pic_dpt_sec where login_id=@id) and LEFT(CONVERT(VARCHAR, a.add_dt, 120), 10)=@req_dt and a.nm=@nm", erp_con);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@req_dt", req_dt);
                cmd.Parameters.AddWithValue("@nm", nm);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        #endregion


        #region Malu apa bossku

        public PR_dto SelectAll_Certified_PR()
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req, a.req_dpt as dpt,a.req_com, Left(convert(varchar, a.add_dt, 120), 10) as req_dt, " +
                                                "b.item_name as itm, b.qty as qty, b.purpose as pur, b.lvl as lvl,a.add_nm as add_usn,b.verify_nm as verify_by,Left(convert(varchar,b.verify_dt,120),10) as verify_dt from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                                                "on a.pr_rn = b.pr_rn where b.sts = 'CERTIFIED' ", erp_con);
               // cmd.Parameters.AddWithValue("@dpt", dpt);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        public PR_dto SelectAllCertified_PR_byRequestor( string requestor)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req,a.req_dpt as dpt,a.req_com,Left(convert(varchar,a.add_dt,120),10) as req_dt, " +
                                                "b.item_name as itm, b.qty as qty, b.purpose as pur, b.lvl as lvl,a.add_nm as add_usn,b.verify_nm as verify_by,Left(convert(varchar,b.verify_dt,120),10) as verify_dt from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                                                "on a.pr_rn = b.pr_rn where b.sts = 'CERTIFIED' and a.nm=@nm", erp_con);

                //cmd.Parameters.AddWithValue("@dpt", dpt);
                cmd.Parameters.AddWithValue("@nm", requestor);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        public PR_dto SelectAllCertified_PR_byRequestDate(string req_dt)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req,a.req_dpt as dpt,a.req_com,Left(convert(varchar,a.add_dt,120),10) as req_dt, " +
                                                "b.item_name as itm, b.qty as qty, b.purpose as pur, b.lvl as lvl,a.add_nm as add_usn,b.verify_nm as verify_by,Left(convert(varchar,b.verify_dt,120),10) as verify_dt from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                                                "on a.pr_rn = b.pr_rn where b.sts = 'CERTIFIED'  and LEFT(CONVERT(VARCHAR, a.add_dt, 120), 10)=@req_dt", erp_con);

                //cmd.Parameters.AddWithValue("@dpt", dpt);
                cmd.Parameters.AddWithValue("@req_dt", req_dt);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        public PR_dto SelectAllCertified_PR_byRequestDate_Requestor(string req_dt, string nm)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req,a.req_dpt as dpt,a.req_com,Left(convert(varchar,a.add_dt,120),10) as req_dt, " +
                                                "b.item_name as itm, b.qty as qty, b.purpose as pur, b.lvl as lvl,a.add_nm as add_usn,b.verify_nm as verify_by,Left(convert(varchar,b.verify_dt,120),10) as verify_dt from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                                                "on a.pr_rn = b.pr_rn where b.sts = 'CERTIFIED' and LEFT(CONVERT(VARCHAR, a.add_dt, 120), 10)=@req_dt and a.nm=@nm", erp_con);

                //cmd.Parameters.AddWithValue("@dpt", dpt);
                cmd.Parameters.AddWithValue("@req_dt", req_dt);
                cmd.Parameters.AddWithValue("@nm", nm);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        // Louis Added on 20200826
        public PR_dto SelectAllCertified_PR_byStoredProcedure(string req, string date, string dept)
        {
            try
            {
                erp_con.Open();

                string sql = "[dbo].[SP_GetPRCERTIFIED_Datas]";

                using (SqlCommand command = new SqlCommand(sql, erp_con))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@req_dt", String.IsNullOrEmpty(date)? "" : date);
                    command.Parameters.AddWithValue("@nm", String.IsNullOrEmpty(req)? "" : req);
                    command.Parameters.AddWithValue("@dept", String.IsNullOrEmpty(dept)? "" : dept);
                    
                    DataTable dt = new DataTable();
                    SqlDataAdapter adt = new SqlDataAdapter(command);
                    adt.Fill(dt);
                    dtoresult.dtitem = dt;
                }

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
        // End

        #endregion

        public PR_dto Select_Certified_PR_Status(string rn, string id)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_pr_dtl where sts not in ('OPEN','CERTIFIED') and pr_rn=@rn and id=@id", erp_con);
                cmd.Parameters.AddWithValue("@rn", rn);
                cmd.Parameters.AddWithValue("@id", id);
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

        public PR_dto Generate_Approval_sts(string id, string pr_rn, string sts, string usn,string nm)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update tbl_pr_dtl set sts=@sts,approve_nm=@approve_nm,approve_by=@by,approve_dt=GetDate() where id=@id and pr_rn=@rn", erp_con);
                cmd.Parameters.AddWithValue("@sts", sts);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@rn", pr_rn);
                cmd.Parameters.AddWithValue("@by", usn);
                cmd.Parameters.AddWithValue("@approve_nm", nm);
                dtoresult.cmd = cmd;
                return dtoresult;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public PR_dto UpdateApproval_sts(DataTable dtdtl, string usn,string name)
        {
            try
            {
                erp_con.Open();
                trans = erp_con.BeginTransaction();
                string id, pr_rn, sts;
                for (int i = 0; i < dtdtl.Rows.Count; i++)
                {
                    id = dtdtl.Rows[i]["id"].ToString();
                    pr_rn = dtdtl.Rows[i]["rn"].ToString();
                    sts = dtdtl.Rows[i]["sts"].ToString();

                    if (sts != "CERTIFIED")
                    {
                        dtoresult = Generate_Approval_sts(id, pr_rn, sts, usn,name);
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


        #region Update PR (PO)

        //public PR_dto SelectApprovedPR()
        //{
        //    try
        //    {
               
        //        erp_con.Open();
        //        SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req, a.dpt as dpt, Left(convert(varchar, a.add_dt, 120), 10) as req_dt, " +
        //                                        "b.item_name as itm, b.qty as qty, b.lvl as lvl,b.po_no,b.grn_no from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
        //                                        "on a.pr_rn = b.pr_rn where b.sts = 'APPROVED'", erp_con);
              
        //        SqlDataAdapter adt = new SqlDataAdapter(cmd);
        //        DataTable dtcmd = new DataTable();
        //        adt.Fill(dtcmd);
        //        dtoresult.dtitem = dtcmd;
        //        return dtoresult;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally
        //    {
        //        erp_con.Close();
        //    }
        //}

        public PR_dto SelectSearchApprovedPR(string keyword)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req, a.sec as sec,a.req_com, Left(convert(varchar, a.add_dt, 120), 10) as req_dt, " +
                                                "b.item_name as itm, b.qty, b.lvl as lvl,b.po_no,b.grn_no,b.rmk,b.price_rmk,b.gr_rmk, b.purpose as pur, b.approve_nm, b.uom, isnull(Left(convert(varchar, b.approve_dt, 120), 10),'') as approve_dt "+
                                                ", isnull(ex.total,0) as totalexp " +
                                                "from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                                                "on a.pr_rn = b.pr_rn "+
                                                "left outer join (select isnull(count(id),0) as total, item_id from tbl_export_log with (nolock) group by item_id) as ex on b.id = ex.item_id" +
                                                "  where b.sts = 'APPROVED' "+keyword + " order by b.id asc", erp_con);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        public PR_dto GenerateUpdatePR_PO(string rn, string id, string po,string rmk,string price_rmk,string gr_rmk)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update tbl_pr_dtl set po_no= IsNull(NULLIF(@po,''),po_no),rmk= IsNull(NULLIF(@rmk,''),rmk),price_rmk= IsNull(NULLIF(@price_rmk,''),price_rmk), " +
                                                "gr_rmk= IsNull(NULLIF(@gr_rmk,''),gr_rmk),po_dt=GetDate() where id=@id and pr_rn=@rn", erp_con);
                cmd.Parameters.AddWithValue("@po", po);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@rmk", rmk);
                cmd.Parameters.AddWithValue("@price_rmk", price_rmk);
                cmd.Parameters.AddWithValue("@gr_rmk", gr_rmk);
                cmd.Parameters.AddWithValue("@rn", rn);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public PR_dto GenerateupdatePO_Modified(string rn, string id, string usn, string old_po, string po)
        {
            try
            {
              
                SqlCommand cmd = new SqlCommand("Insert into tbl_erp_log (rmk,pre,new,t_id,rn,add_usn,add_dt)values('CHANGE PO',@pre,@new,@tid,@rn,@usn,GetDate())", erp_con);
                cmd.Parameters.AddWithValue("@pre", old_po);
                cmd.Parameters.AddWithValue("@new", po);
                cmd.Parameters.AddWithValue("@tid", id);
                cmd.Parameters.AddWithValue("@rn", rn);
                cmd.Parameters.AddWithValue("@usn", usn);
                dtoresult.cmd = cmd;
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public PR_dto UpdatePR_Item_PO(DataTable dtdtl, string usn,string po, string rmk, string price_rmk, string gr_rmk)
        {
            try
            {
                erp_con.Open();
                trans = erp_con.BeginTransaction();
                string id, pr_rn,ex_po;
                for (int i = 0; i < dtdtl.Rows.Count; i++)
                {
                    id = dtdtl.Rows[i]["id"].ToString();
                    pr_rn = dtdtl.Rows[i]["rn"].ToString();
                
                    ex_po = dtdtl.Rows[i]["po_no"].ToString();


                    dtoresult = GenerateUpdatePR_PO(pr_rn,id,po,rmk,price_rmk,gr_rmk);
                    dtoresult.cmd.Transaction = trans;
                    dtoresult.cmd.ExecuteNonQuery();

                    if (ex_po != "-")
                    {
                        dtoresult = GenerateupdatePO_Modified(pr_rn, id, usn, ex_po, po);
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


        #region PR View

        public PR_dto SelectPRStatus_User(string usn,string keyword,string dpt)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req, a.sec as sec,a.req_com, Left(convert(varchar, a.add_dt, 120), 10) as req_dt, " +
                                                "b.item_name as itm, b.qty as qty, b.lvl as lvl,b.sts,b.verify_nm,Left(convert(varchar, b.verify_dt, 120), 10) as verify_dt, "+
                                                "b.approve_nm,Left(convert(varchar, b.approve_dt, 120), 10) as approve_dt,b.po_no,b.grn_no,b.rmk,b.gr_rmk, b.Purpose as pur, b.uom from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                                                "on a.pr_rn = b.pr_rn where a.dpt =@dpt " + keyword, erp_con);
                cmd.Parameters.AddWithValue("@usn", usn);
                cmd.Parameters.AddWithValue("@dpt", dpt);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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


        public PR_dto SelectPRStatus_PUR(string keyword)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select  b.id, a.pr_rn as pr_rn, a.nm as req, a.sec as sec,a.req_com, Left(convert(varchar, a.add_dt, 120), 10) as req_dt, " +
                                                "b.item_name as itm, b.qty as qty, b.lvl as lvl,b.sts,b.verify_nm,Left(convert(varchar, b.verify_dt, 120), 10) as verify_dt, " +
                                                "b.approve_nm,Left(convert(varchar, b.approve_dt, 120), 10) as approve_dt,b.po_no,b.grn_no,b.rmk,b.gr_rmk, b.Purpose as pur, b.uom from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                                                "on a.pr_rn = b.pr_rn " + keyword, erp_con);
                //cmd.Parameters.AddWithValue("@usn", usn);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        #endregion


        #region PR Approval History
        public PR_dto SelectApprovedPR_Management(string keyword)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select b.id, a.pr_rn as pr_rn, a.nm as req, a.dpt as dpt,a.req_com, Left(convert(varchar, a.add_dt, 120), 10) as req_dt, " +
                                                "b.item_name as itm, b.qty as qty, b.lvl as lvl,a.add_nm,b.sts,b.verify_nm as verify_by,Left(convert(varchar, b.verify_dt, 120), 10) as verify_dt, " +
                                                "b.approve_nm as approve_by,Left(convert(varchar, b.approve_dt, 120), 10) as approve_dt,b.po_no,b.price_rmk,b.rmk,b.gr_rmk, b.Purpose as pur from tbl_pr_hdr as a inner join tbl_pr_dtl as b " +
                                                "on a.pr_rn = b.pr_rn " + keyword, erp_con);
                //cmd.Parameters.AddWithValue("@usn", usn);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtitem = dtcmd;
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

        #endregion

        #region Export Log
        public PR_dto GenerateInsertExportLog(ExportLogModel model)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("Insert into tbl_export_log (item_id,export_usn,export_dt)values(@itemID,@usn,@dt)", erp_con);
                cmd.Parameters.AddWithValue("@itemID", model.ItemId);
                cmd.Parameters.AddWithValue("@usn", model.Username);
                cmd.Parameters.AddWithValue("@dt", model.ExportDate);
                dtoresult.cmd = cmd;
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public PR_dto GenerateInsertExportLogs(List<ExportLogModel> models)
        {
            try
            {
                erp_con.Open();
                trans = erp_con.BeginTransaction();
                foreach (var item in models)
                {
                    dtoresult = GenerateInsertExportLog(item);
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