using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace ERP_System.GRN_Module.GRN_Control
{
    public class GRN_da
    {
        GRN_dto dtoresult = new GRN_dto();
        SqlConnection erp_con = new SqlConnection(ResourceModule.ERP_con);
        SqlTransaction trans;

        #region GRN Entry

        public GRN_dto SelectApprovePOVendor(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select distinct ven_code,ven_code+'|  '+ven_name as description from tbl_npi_po_hdr where sts ='APPROVE' and com=@com", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtven = dtcmd;
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

        public GRN_dto SelectApprovePO_VenCode(string ven)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select po_no from tbl_npi_po_hdr where ven_code =@ven_code and sts ='APPROVE'", erp_con);
                cmd.Parameters.AddWithValue("@ven_code", ven);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtpo = dtcmd;
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

        //public GRN_dto SelectPO_FullRecQty(string po)
        //{
        //    try
        //    {
        //        erp_con.Open();
        //        SqlCommand cmd = new SqlCommand("Select count(po_no) as existcount from tbl_npi_po_dtl where po_no=@po_no", erp_con);
        //        cmd.Parameters.AddWithValue("@po_no", po);
        //        SqlDataAdapter adt = new SqlDataAdapter(cmd);
        //        DataTable dtcmd = new DataTable();
        //        adt.Fill(dtcmd);
        //        dtoresult.dtcheck = dtcmd;
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

        public GRN_dto SelectGRN_RN(string rn)
        {
            try
            {
                erp_con.Close();
                SqlCommand cmd = new SqlCommand("Select top 1 RIGHT(grn_no, LEN(grn_no) - 7) as grn_no from tbl_npi_grn_hdr where grn_no like @grn_no + '%'order by add_date desc", erp_con);
                cmd.Parameters.AddWithValue("@grn_no", rn);
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

        public GRN_dto SelectPOHeader_po(string po)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select add_usn,LEFT(CONVERT(VARCHAR, order_date, 120), 10) as order_date,ven_type from tbl_npi_po_hdr where po_no =@po_no", erp_con);
                cmd.Parameters.AddWithValue("@po_no", po);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtpo_info = dtcmd;
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

        public GRN_dto SelectPODetails_po(string po)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select a.po_no,a.catalog_no as ctlno,a.dsc as 'desc',a.ext_dsc as exdesc,a.uom,a.order_qty as o_qty,a.bal_qty from tbl_npi_po_dtl as a inner join tbl_npi_po_hdr as b on a.po_no = b.po_no  where a.order_qty-a.receive_qty >0 and b.sts ='APPROVE' and a.po_no=@po_no ", erp_con);
                cmd.Parameters.AddWithValue("@po_no", po);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtpo_details = dtcmd;
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

        public GRN_dto GenerateGRNHeader(string grn_no,string grn_date,string vendor,string do_no,string rmk,string usn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_npi_grn_hdr (grn_no,grn_date,vendor,do_no,rmk,sts,add_date,add_usn) values (@grn_no,@grn_date,@vendor,@do_no,@rmk,'OPEN',GetDate(),@add_usn)",erp_con);
                cmd.Parameters.AddWithValue("@grn_no",grn_no);
                cmd.Parameters.AddWithValue("@grn_date", grn_date);
                cmd.Parameters.AddWithValue("@vendor", vendor);
                cmd.Parameters.AddWithValue("@do_no", do_no);
                cmd.Parameters.AddWithValue("@rmk", rmk);
                cmd.Parameters.AddWithValue("@add_usn", usn);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto GenerateGRNDetails(string grn_no,string po_no,string catalog_no,string dsc,string ext_dsc,string uom,string o_qty,string rec_qty,string usn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_npi_grn_dtl (grn_no,po_no,catalog_no,dsc,ext_dsc,uom,order_qty,rec_qty,add_date,add_usn) values (@grn_no,@po_no,@catalog_no,@dsc,@ext_dsc,@uom,@order_qty,@rec_qty,GetDate(),@add_usn)", erp_con);
                cmd.Parameters.AddWithValue("@grn_no", grn_no);
                cmd.Parameters.AddWithValue("@po_no", po_no);
                cmd.Parameters.AddWithValue("@catalog_no", catalog_no);
                cmd.Parameters.AddWithValue("@dsc", dsc);
                cmd.Parameters.AddWithValue("@ext_dsc", ext_dsc);
                cmd.Parameters.AddWithValue("@uom", uom);
                cmd.Parameters.AddWithValue("@order_qty", o_qty);
                cmd.Parameters.AddWithValue("@rec_qty", rec_qty);
                cmd.Parameters.AddWithValue("@add_usn", usn);
                dtoresult.cmd = cmd;
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto InsertGRN(string grn_no, string grn_date, string vendor, string do_no, string rmk, string usn,DataTable dtdetails)
        {
            try
            {
                string po_no,catalog_no,dsc,ext_dsc,uom, o_qty,rec_qty;
                decimal dec_rec_qty;
                erp_con.Open();
                trans = erp_con.BeginTransaction();
                //generate grn hdr
                dtoresult = GenerateGRNHeader(grn_no, grn_date, vendor, do_no, rmk, usn);
                dtoresult.cmd.Transaction = trans;
                dtoresult.cmd.ExecuteNonQuery();

                for (int i = 0; i < dtdetails.Rows.Count; i++)
                {
                    po_no = dtdetails.Rows[i]["po_no"].ToString();
                    catalog_no = dtdetails.Rows[i]["ctlno"].ToString();
                    dsc = dtdetails.Rows[i]["desc"].ToString();
                    ext_dsc = dtdetails.Rows[i]["exdesc"].ToString();
                    uom= dtdetails.Rows[i]["uom"].ToString();
                    o_qty= dtdetails.Rows[i]["o_qty"].ToString();
                    rec_qty= dtdetails.Rows[i]["rec_qty"].ToString();
                    dec_rec_qty = decimal.Parse(rec_qty);
                    
                    //generate grn details
                    dtoresult = GenerateGRNDetails(grn_no, po_no, catalog_no, dsc, ext_dsc, uom, o_qty, rec_qty, usn);
                    dtoresult.cmd.Transaction = trans;
                    dtoresult.cmd.ExecuteNonQuery();                  
                }
                trans.Commit();
                dtoresult.grn_no = grn_no;
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

        #region GRN Approve

        public GRN_dto SelectOpenGRN_Vendor()
        {
            try
            {
                erp_con.Open();
                SqlDataAdapter cmd = new SqlDataAdapter("Select distinct a.vendor,a.vendor+'|  '+b.ven_name as description from tbl_npi_grn_hdr as a inner join tbl_npi_po_hdr as b on a.vendor=b.ven_code where a.sts ='OPEN'", erp_con);
                DataTable dtcmd = new DataTable();
                cmd.Fill(dtcmd);
                dtoresult.dtven = dtcmd;
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

        public GRN_dto SelectOpenGRN(string values)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select a.sts,a.grn_no,LEFT(CONVERT(VARCHAR, a.grn_date, 120), 10)as grn_date,a.vendor,b.ven_name,a.do_no,a.rmk from tbl_npi_grn_hdr as a inner join tbl_npi_po_hdr as b on a.vendor = b.ven_code where a.sts ='OPEN' " + values + " group by a.sts,a.grn_no,a.grn_date,a.vendor,b.ven_name,a.do_no,a.rmk", erp_con);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtgrn_hdr = dtcmd;
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

        public GRN_dto SelectOpenGRN_Details(string grn_no)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select a.po_no,a.catalog_no,a.dsc,a.ext_dsc,a.uom,a.rec_qty,b.unit_price as uprice from tbl_npi_grn_dtl as a, tbl_npi_inv_unit_price as b where a.catalog_no=b.catalog_no and a.grn_no = @grn_no", erp_con);
                cmd.Parameters.AddWithValue("@grn_no", grn_no);
                //cmd.Parameters.AddWithValue("@po_no", po_no);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtgrn_dtl = dtcmd;
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

        public GRN_dto GenerateUpdatePOQty(string po_no, string catalog_no, decimal qty)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update tbl_npi_po_dtl set receive_qty=receive_qty+@receive_qty,bal_qty =order_qty-receive_qty-@receive_qty where po_no=@po_no and catalog_no=@ctl_no", erp_con);
                cmd.Parameters.AddWithValue("@receive_qty", qty);
                cmd.Parameters.AddWithValue("@po_no", po_no);
                cmd.Parameters.AddWithValue("@ctl_no", catalog_no);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public GRN_dto GenerateUpdatePOSts(string po_no)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update tbl_npi_po_hdr set sts= (case when (select count(po_no) from tbl_npi_po_dtl where bal_qty > 0 and po_no=@po_no ) > 0 then sts else 'CLOSE' end)  where po_no=@po_no", erp_con);
                cmd.Parameters.AddWithValue("@po_no", po_no);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto GenerateGRNTransaction(string grn_no, string po_no, string receive_qty, string ctl_no, string usn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_npi_transaction (grn_no,po_no,ctl_no,loc,type,receive_qty,rec_dt,rec_usn,add_dt) values (@grn_no,@po_no,@ctl_no,'JFP','FG',@receive_qty,GetDate(),@rec_usn,GetDate())", erp_con);
                cmd.Parameters.AddWithValue("@grn_no", grn_no);
                cmd.Parameters.AddWithValue("@po_no", po_no);
                cmd.Parameters.AddWithValue("@ctl_no", ctl_no);
                cmd.Parameters.AddWithValue("@receive_qty", receive_qty);
                cmd.Parameters.AddWithValue("@rec_usn", usn); 
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public GRN_dto GenerateUpdateInventory(decimal qty, string ctlno)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update tbl_inv set qty=qty+@qty where catalog_no=@ctlno and loc='JFP'", erp_con);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@ctlno", ctlno);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto SelectGRNDetails_GRN(string grn_no)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select grn_no,po_no,catalog_no,rec_qty from tbl_npi_grn_dtl where grn_no =@grn_no",erp_con);
                cmd.Parameters.AddWithValue("@grn_no", grn_no);
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

        public GRN_dto GenerateGRNUpdateSts(string grn_no, string sts)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update tbl_npi_grn_hdr set sts=@sts where grn_no=@grn_no", erp_con);
                cmd.Parameters.AddWithValue("@sts", sts);
                cmd.Parameters.AddWithValue("@grn_no", grn_no);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GRN_dto SelectApprovedGRN(string grn_no)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_npi_grn_hdr where sts ='APPROVE' and grn_no=@grn_no", erp_con);
                cmd.Parameters.AddWithValue("@grn_no", grn_no);
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

        public GRN_dto ApproveGRN(DataTable dtsts,DataTable dtdetails,string usn)
        {
            try
            {
                decimal dec_rec_qty;
                string grn_no, sts, po_no, ctlno, rec_qty;
                erp_con.Open();
                trans = erp_con.BeginTransaction();

                for (int i = 0; i < dtsts.Rows.Count; i++)
                {
                    grn_no = dtsts.Rows[i]["grn_no"].ToString();
                    sts = dtsts.Rows[i]["sts"].ToString();
                    //generate update grn status 
                    dtoresult = GenerateGRNUpdateSts(grn_no, sts);
                    dtoresult.cmd.Transaction = trans;
                    dtoresult.cmd.ExecuteNonQuery();
                }

                for (int i = 0; i < dtdetails.Rows.Count; i++)
                {
                    grn_no = dtdetails.Rows[i]["grn_no"].ToString();
                    po_no = dtdetails.Rows[i]["po_no"].ToString();
                    ctlno = dtdetails.Rows[i]["ctlno"].ToString();
                    rec_qty = dtdetails.Rows[i]["rec_qty"].ToString();
                    dec_rec_qty = decimal.Parse(rec_qty);
                    //generate update po qty
                    dtoresult = GenerateUpdatePOQty(po_no, ctlno, dec_rec_qty);
                    dtoresult.cmd.Transaction = trans;
                    dtoresult.cmd.ExecuteNonQuery();

                    //generate update po sts 
                    dtoresult = GenerateUpdatePOSts(po_no);
                    dtoresult.cmd.Transaction = trans;
                    dtoresult.cmd.ExecuteNonQuery();

                    //generate insert grn transaction
                    dtoresult = GenerateGRNTransaction(grn_no, po_no, rec_qty, ctlno, usn);
                    dtoresult.cmd.Transaction = trans;
                    dtoresult.cmd.ExecuteNonQuery();

                    //generate update inventory qty
                    dtoresult = GenerateUpdateInventory(dec_rec_qty, ctlno);
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

        #region GRN Edit

        public GRN_dto SelectOpenGRN_GRNDate(string grndate)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select grn_no from tbl_npi_grn_hdr where grn_date=@grndate", erp_con);
                cmd.Parameters.AddWithValue("@grndate", grndate);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtgrn = dtcmd;
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

        public GRN_dto SelectGRNHeader(string grn)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select grn_no,LEFT(CONVERT(VARCHAR, grn_date, 120), 10) as grn_date,vendor,do_no,rmk from tbl_npi_grn_hdr where grn_no =@grn", erp_con);
                cmd.Parameters.AddWithValue("@grn", grn);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtgrn_hdr = dtcmd;
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

        public GRN_dto SelectGRN_PODetails(string grn)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select distinct b.po_no, LEFT(CONVERT(VARCHAR, c.order_date, 120), 10) as order_date, c.ven_type, c.add_usn "+
                                                "from tbl_npi_grn_dtl as a inner join tbl_npi_grn_dtl as b on a.grn_no = b.grn_no "+
                                                "inner join tbl_npi_po_hdr as c on b.po_no = c.po_no where a.grn_no =@grn ", erp_con);
                cmd.Parameters.AddWithValue("@grn", grn);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtpo_info = dtcmd;
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

        public GRN_dto SelectGRN_POItemDetails(string grn)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select a.po_no, a.catalog_no as ctlno, a.dsc as 'desc', a.ext_dsc as exdesc, a.uom, a.order_qty as o_qty, a.bal_qty,b.rec_qty "+
                                                "from tbl_npi_po_dtl as a inner join tbl_npi_grn_dtl as b on a.po_no = b.po_no and a.catalog_no = b.catalog_no "+
                                                "where b.grn_no=@grn", erp_con);
                cmd.Parameters.AddWithValue("@grn", grn);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtpo_details = dtcmd;
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

        public GRN_dto UpdateGRNHeader(string grn_no, string grn_date, string vendor, string do_no, string rmk, string usn)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Update tbl_npi_grn_hdr set grn_date=@grn_date,vendor=@vendor,do_no=@do_no,rmk=@rmk,udt_date=GetDate(),udt_usn=@udt_usn where grn_no=@grn_no and sts='OPEN'", erp_con);
                cmd.Parameters.AddWithValue("@grn_no", grn_no);
                cmd.Parameters.AddWithValue("@grn_date", grn_date);
                cmd.Parameters.AddWithValue("@vendor", vendor);
                cmd.Parameters.AddWithValue("@rmk", rmk);
                cmd.Parameters.AddWithValue("@do_no", do_no);
                cmd.Parameters.AddWithValue("@udt_usn", usn);
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

        public GRN_dto DeleteGRN_PO(string po_no,string grn_no)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Delete from tbl_npi_grn_dtl where grn_no=@grn_no and po_no=@po_no", erp_con);
                cmd.Parameters.AddWithValue("@grn_no", grn_no);
                cmd.Parameters.AddWithValue("@po_no", po_no);
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

        public GRN_dto SelectDuplicatePO(string grn, string po, string ctlno)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_npi_grn_dtl where  grn_no=@grn_no and po_no=@po_no and catalog_no=@ctlno", erp_con);
                cmd.Parameters.AddWithValue("@grn_no", grn);
                cmd.Parameters.AddWithValue("@po_no", po);
                cmd.Parameters.AddWithValue("@ctlno", ctlno);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtcheck = dtcmd;
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

        public GRN_dto GenerateUpdateGRNDetailsQty(string grn_no, string po_no,string catalog_no,string qty)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Update tbl_npi_grn_dtl set rec_qty =@qty where grn_no=@grn_no and po_no=@po_no and catalog_no=@ctlno", erp_con);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@grn_no", grn_no);
                cmd.Parameters.AddWithValue("@po_no", po_no);
                cmd.Parameters.AddWithValue("@ctlno", catalog_no);
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

        public GRN_dto EditGRN_AddPODetails(string grn_no, string usn, DataTable dtdetails)
        {
            try
            {
                string po_no, catalog_no, dsc, ext_dsc, uom, o_qty, rec_qty;
                decimal dec_rec_qty;
                erp_con.Open();
                trans = erp_con.BeginTransaction();        

                for (int i = 0; i < dtdetails.Rows.Count; i++)
                {
                    po_no = dtdetails.Rows[i]["po_no"].ToString();
                    catalog_no = dtdetails.Rows[i]["ctlno"].ToString();
                    dsc = dtdetails.Rows[i]["desc"].ToString();
                    ext_dsc = dtdetails.Rows[i]["exdesc"].ToString();
                    uom = dtdetails.Rows[i]["uom"].ToString();
                    o_qty = dtdetails.Rows[i]["o_qty"].ToString();
                    rec_qty = dtdetails.Rows[i]["rec_qty"].ToString();
                    dec_rec_qty = decimal.Parse(rec_qty);

                    //generate grn details
                    dtoresult = GenerateGRNDetails(grn_no, po_no, catalog_no, dsc, ext_dsc, uom, o_qty, rec_qty, usn);
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
        //public GRN_dto Insert
        
     
        #endregion
    }
}