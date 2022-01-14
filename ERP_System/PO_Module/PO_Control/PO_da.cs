using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace ERP_System.PO_Module.PO_Control
{
    public class PO_da
    {
        PO_dto dtoresult = new PO_dto();
        SqlConnection helpdesk_con = new SqlConnection(ResourceModule.HD_con);
        SqlConnection ibas_con = new SqlConnection(ResourceModule.IBAS_con);
        SqlConnection erp_con = new SqlConnection(ResourceModule.ERP_con);
        SqlTransaction trans;

        #region Login
        public PO_dto SelectAllUser()
        {
            try
            {
                helpdesk_con.Open();
                SqlDataAdapter cmd = new SqlDataAdapter("Select * from tblUser", helpdesk_con);
                DataTable dtcmd = new DataTable();
                cmd.Fill(dtcmd);
                dtoresult.dtUser = dtcmd;
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

        #region PO Entry

        public PO_dto SelectAllCompanyCode_Vendor(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select distinct company_code from tbl_npi_vendor_master where company_code=@com", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtCompanyCode = dtcmd;
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

        public PO_dto SelectAllVendor_Type(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select type_code,type_code +' - '+type_desc as description from tbl_npi_vendor_type where com=@com group by type_code,type_desc", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtVen_Type = dtcmd;
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

        public PO_dto SelectVendorCode_Type(string type, string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select vendor_code,vendor_code+'|  '+vendor_name as description from tbl_npi_vendor_master where company_code=@com and type_code=@type group by vendor_code,Vendor_name", erp_con);
                //or type_code = @type and company_code = @com
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtVen_Code = dtcmd;
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

        public PO_dto SelectVendor_Info(string com, string ven_code)
        {
            try
            {
                string str;
                str = "Select a.contact_person1, a.term_code, a.term_code + ' | ' + b.desc_txt1 as term_desc from tbl_npi_vendor_master as a inner join tbl_npi_term_code as b " +
                    "on rtrim(a.term_code) = rtrim(b.term_code) and a.company_code = b.company_code where rtrim(a.vendor_code) =@vencode and a.company_code =@com";
                erp_con.Open();
                SqlCommand cmd = new SqlCommand(str, erp_con);
                cmd.Parameters.AddWithValue("@vencode", ven_code.Trim());
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtVen_Info = dtcmd;
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

        public PO_dto SelectPurchase_Term(string com, string ven_code)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select b.purchase_term_code as pur_code ,b.purchase_term_code +' | '+b.desc_txt as pur_desc from tbl_npi_vendor_master as a inner join tbl_npi_purchase_term as b on a.company_code = b.company_code and rtrim(a.purchase_term_code) = rtrim(b.purchase_term_code) where a.company_code =@com and a.vendor_code=@vendor ", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@vendor", ven_code);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtPur_Term = dtcmd;
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

        public PO_dto SelectCatalogNo()
        {
            try
            {

                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select catalog_no,catalog_no+' |   '+dsc as catalog_desc from tbl_inv where loc ='JFP'", erp_con);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtcatalog = dtcmd;
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

        public PO_dto SelectCatalogInfo_ctlno(string catalogno)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select distinct dsc,ex_dsc from tbl_inv where catalog_no =@catalogno", erp_con);
                cmd.Parameters.AddWithValue("@catalogno", catalogno);
                // cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtcataloginfo = dtcmd;
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

        public PO_dto SelectUnit_Sell_Price(string catalogno)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select a.unit_price,c.sell_price from tbl_npi_inv_unit_price as a ,tbl_inv as b,tbl_npi_inv_sell_price as c where a.catalog_no=b.catalog_no and a.com=b.com and b.com=c.com and b.catalog_no = c.catalog_no and a.catalog_no =@catalogno", erp_con);
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

        public PO_dto SelectUOM()
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

        public PO_dto SelectPO_RN(string rn)
        {
            try
            {
                erp_con.Close();
                SqlCommand cmd = new SqlCommand("Select top 1 RIGHT(po_no, LEN(po_no) - 6) as po_no from tbl_npi_po_hdr where po_no like @po_no + '%'order by add_date desc", erp_con);
                cmd.Parameters.AddWithValue("@po_no", rn);
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

        public PO_dto GeneratePODetails(string po_no, string catalog_no, string dsc, string ex_dsc, string uom, string qty, string unit_price, string total, string usn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_npi_po_dtl (po_no,catalog_no,dsc,ext_dsc,order_qty,unit_price,receive_qty,bal_qty,uom,total_amt,add_date,add_usn) values (@po_no,@catalog_no,@dsc,@ext_dsc,@order_qty,@unit_price,'0.00',@bal_qty,@uom,@total_amt,GetDate(),@add_usn)", erp_con);
                cmd.Parameters.AddWithValue("@po_no", po_no);
                cmd.Parameters.AddWithValue("@catalog_no", catalog_no);
                cmd.Parameters.AddWithValue("@dsc", dsc);
                cmd.Parameters.AddWithValue("@ext_dsc", ex_dsc);
                cmd.Parameters.AddWithValue("@order_qty", qty);
                cmd.Parameters.AddWithValue("@bal_qty", qty);
                cmd.Parameters.AddWithValue("@uom", uom);
                cmd.Parameters.AddWithValue("@total_amt", total);
                cmd.Parameters.AddWithValue("@unit_price", unit_price);
                cmd.Parameters.AddWithValue("@add_usn", usn);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PO_dto GeneratePOHeader(string com, string po_no, string ven_type, string ven_code, string ven_name, string order_dt, string ven_contact, string term_code, string pur_term, string place, string rmk, string usn)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("Insert into tbl_npi_po_hdr (com,po_no,rev_no,order_date,ven_type,ven_code,ven_name,ven_contact,term_code,destination,pur_term,rmk,sts,add_date,add_usn) values (@com,@po_no,'1',@order_date,@ven_type,@ven_code,@ven_name,@ven_contact,@term_code,@destination,@pur_term,@rmk,'OPEN',GetDate(),@add_usn)", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@po_no", po_no);
                cmd.Parameters.AddWithValue("@order_date", order_dt);
                cmd.Parameters.AddWithValue("@ven_type", ven_type);
                cmd.Parameters.AddWithValue("@ven_code", ven_code);
                cmd.Parameters.AddWithValue("@ven_name", ven_name);
                cmd.Parameters.AddWithValue("@ven_contact", ven_contact);
                cmd.Parameters.AddWithValue("@term_code", term_code);
                cmd.Parameters.AddWithValue("@destination", place);
                cmd.Parameters.AddWithValue("@pur_term", pur_term);
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

        public PO_dto InsertPOData(string com, string po_no, string ven_type, string ven_code, string ven_name, string order_dt, string ven_contact, string term_code, string pur_term, string place, string rmk, DataTable dtitem, string usn)
        {
            try
            {
                string catalog_no, dsc, ex_dsc, uom, qty, unit_price, total;
                erp_con.Open();
                trans = erp_con.BeginTransaction();
                //generate po hdr cmd
                dtoresult = GeneratePOHeader(com, po_no, ven_type, ven_code, ven_name, order_dt, ven_contact, term_code, pur_term, place, rmk, usn);
                dtoresult.cmd.Transaction = trans;
                dtoresult.cmd.ExecuteNonQuery();

                //generate po details cmd
                for (int i = 0; i < dtitem.Rows.Count; i++)
                {
                    catalog_no = dtitem.Rows[i]["ctlno"].ToString();
                    dsc = dtitem.Rows[i]["desc"].ToString();
                    ex_dsc = dtitem.Rows[i]["exdesc"].ToString();
                    uom = dtitem.Rows[i]["uom"].ToString();
                    qty = dtitem.Rows[i]["o_qty"].ToString();
                    unit_price = dtitem.Rows[i]["uprice"].ToString();
                    total = dtitem.Rows[i]["total"].ToString();

                    dtoresult = GeneratePODetails(po_no, catalog_no, dsc, ex_dsc, uom, qty, unit_price, total, usn);
                    dtoresult.cmd.Transaction = trans;
                    dtoresult.cmd.ExecuteNonQuery();

                }
                trans.Commit();
                dtoresult.po_no = po_no;

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

        #region PO Approval
        public PO_dto SelectOpenPO(string values)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select sts,po_no,LEFT(CONVERT(VARCHAR, order_date, 120), 10)as order_dt,ven_type as po_type,ven_code,ven_name,term_code,destination as place from tbl_npi_po_hdr where sts ='OPEN' " + values + "", erp_con);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtPO = dtcmd;
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

        public PO_dto SelectOpenPO_VendorName()
        {
            try
            {
                erp_con.Open();
                SqlDataAdapter cmd = new SqlDataAdapter("Select ven_code,ven_code+'|  '+ven_name as description from tbl_npi_po_hdr where sts ='OPEN'", erp_con);
                DataTable dtcmd = new DataTable();
                cmd.Fill(dtcmd);
                dtoresult.dtVen_Code = dtcmd;
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

        public PO_dto SelectOpenPO_Details(string po)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select catalog_no as ctlno,dsc as 'desc',ext_dsc as exdesc,order_qty as o_qty,uom as UOM,unit_price as uprice,total_amt as total from tbl_npi_po_dtl where po_no =@po_no", erp_con);
                cmd.Parameters.AddWithValue("@po_no", po);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtPO_Details = dtcmd;
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

        public PO_dto GenerateUpdatePO(string po_no, string sts)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update tbl_npi_po_hdr set sts =@sts where po_no=@po_no", erp_con);
                cmd.Parameters.AddWithValue("@sts", sts);
                cmd.Parameters.AddWithValue("@po_no", po_no);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public PO_dto UpdatePO_sts(DataTable dtpo)
        {
            try
            {
                string po_no, sts;
                erp_con.Open();
                trans = erp_con.BeginTransaction();
                for (int i = 0; i < dtpo.Rows.Count; i++)
                {
                    po_no = dtpo.Rows[i]["po"].ToString();
                    sts = dtpo.Rows[i]["sts"].ToString();
                    dtoresult = GenerateUpdatePO(po_no, sts);
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

        #region PO Edit

        public PO_dto SelectAllVenCode(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select vendor_code,vendor_code+'|  '+vendor_name as description from tbl_npi_vendor_master where company_code=@com group by vendor_code,Vendor_name", erp_con);
                //or type_code = @type and company_code = @com
                //cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtVen_Code = dtcmd;
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

        public PO_dto SelectOpenPONumber(string com, string orderdate)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select po_no from tbl_npi_po_hdr where sts='OPEN' and com=@com and order_date=@o_dt", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@o_dt", orderdate);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtPO = dtcmd;
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

        public PO_dto SelectPOHeader(string po_no)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select com,po_no,rev_no,LEFT(CONVERT(VARCHAR, order_date, 120), 10) as order_date,ven_type,ven_code,ven_name,ven_contact,term_code,destination,pur_term,rmk from tbl_npi_po_hdr where po_no=@po", erp_con);
                cmd.Parameters.AddWithValue("@po", po_no);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtpo_hdr = dtcmd;
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

        public PO_dto SelectPODetails(string po_no)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_npi_po_dtl where po_no=@po", erp_con);
                cmd.Parameters.AddWithValue("@po", po_no);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtPO_Details = dtcmd;
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

        public PO_dto UpdatePOHdr(string com, string po_no, string ven_type, string ven_code, string ven_name, string order_dt, string ven_contact, string term_code, string pur_term, string place, string rmk, string usn)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Update tbl_npi_po_hdr set com=@com,order_date=@o_dt,ven_type=@v_type,ven_code=@v_code," +
                                                "ven_name=@v_name,ven_contact=@v_contact,term_code=@term_code," +
                                                "destination=@des,pur_term=@p_term,rmk=@rmk,udt_date=GetDate(),udt_usn=@usn where po_no=@pono", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@o_dt", order_dt);
                cmd.Parameters.AddWithValue("@v_type", ven_type);
                cmd.Parameters.AddWithValue("@v_code", ven_code);
                cmd.Parameters.AddWithValue("@v_name", ven_name);
                cmd.Parameters.AddWithValue("@v_contact", ven_contact);
                cmd.Parameters.AddWithValue("@term_code", term_code);
                cmd.Parameters.AddWithValue("@des", place);
                cmd.Parameters.AddWithValue("@p_term", pur_term);
                cmd.Parameters.AddWithValue("@rmk", rmk);
                cmd.Parameters.AddWithValue("@usn", usn);
                cmd.Parameters.AddWithValue("@pono", po_no);
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

        public PO_dto UpdatePODtl(string po_no, string catalog_no, string dsc, string ex_dsc, string uom, string qty, string unit_price, string total, string usn)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Update tbl_npi_po_dtl set catalog_no=@ctlno,dsc=@dsc,ext_dsc=@ex_dsc," +
                                                "order_qty=@qty,unit_price=@unit_price,bal_qty=@qty,uom=@uom," +
                                                "total_amt=@total,udt_date=GetDate(),udt_usn=@usn where po_no=@po", erp_con);
                cmd.Parameters.AddWithValue("@po", po_no);
                cmd.Parameters.AddWithValue("@ctlno", catalog_no);
                cmd.Parameters.AddWithValue("@dsc", dsc);
                cmd.Parameters.AddWithValue("@ex_dsc", ex_dsc);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@uom", uom);
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@unit_price", unit_price);
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

        public PO_dto SelectSamePODtlCatalog(string po_no, string ctlno)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_npi_po_dtl where po_no=@po and catalog_no=@ctlno", erp_con);
                cmd.Parameters.AddWithValue("@po", po_no);
                cmd.Parameters.AddWithValue("@ctlno", ctlno);
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

        public PO_dto AddNewPODtl(string po_no, string catalog_no, string dsc, string ex_dsc, string uom, string qty, string unit_price, string total, string usn)
        {
            try
            {
                erp_con.Open();
                dtoresult = GeneratePODetails(po_no, catalog_no, dsc, ex_dsc, uom, qty, unit_price, total, usn);
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

        public PO_dto SelectCountPODtl(string po_no)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select catalog_no  from tbl_npi_po_dtl where po_no=@po", erp_con);
                cmd.Parameters.AddWithValue("@po", po_no);
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

        public PO_dto DeleteSelectedPODtl(string po_no, string id)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Delete from tbl_npi_po_dtl where po_no=@po and id=@id", erp_con);
                cmd.Parameters.AddWithValue("@po", po_no);
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
    }
}