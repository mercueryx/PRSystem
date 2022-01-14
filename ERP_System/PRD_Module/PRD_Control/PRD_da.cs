using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace ERP_System.PRD_Module.PRD_Control
{
  
    public class PRD_da
    {
        SqlConnection erp_con = new SqlConnection(ResourceModule.ERP_con);
        PRD_dto dtoresult = new PRD_dto();
        SqlTransaction trans;

        #region Create Packing List

        public PRD_dto SelectBrandCode(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select brand_code,brand_code+char(9)+'|'+char(9)+description as description from tbl_npi_brand where com=@com",erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtbrandcode = dtcmd;
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

        public PRD_dto SelectMasterCatalog_ByBrand(string com, string brand)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select catalog_no,catalog_no+char(9)+'|'+char(9)+dsc as description from tbl_inv where com =@com and rtrim(brand_code)=@brand and mstr_type='T'", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@brand", brand.Trim());
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtmaster = dtcmd;
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

        public PRD_dto SelectDefaultLocation(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select loc,loc + char(9)+'|'+char(9)+dsc as description from tbl_npi_loc where com=@com and sts='O'", erp_con);
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

        public PRD_dto SelectCatalogNo_ByLoc(string com, string loc)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select catalog_no,catalog_no+char(9)+'|'+char(9)+dsc as description from tbl_inv where com=@com and loc=@loc", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@loc", loc);
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

        public PRD_dto SelectCatalogDescription(string com, string loc, string ctlno)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select dsc from tbl_inv where com=@com and loc=@loc and catalog_no=@ctlno", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@loc", loc);
                cmd.Parameters.AddWithValue("@ctlno", ctlno);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtdesc = dtcmd;
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

        public PRD_dto SelectSamePackingListHdr(string com, string mstr_catalog,string brand)
        {
            try
            {
                erp_con.Close();
                SqlCommand cmd = new SqlCommand("Select top 1 ver from tbl_npi_bom_hdr where com=@com and mstr_catalog=@ctlno and b_code=@brand order by add_date desc", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@ctlno", mstr_catalog);
                cmd.Parameters.AddWithValue("@brand", brand);
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
   
        public PRD_dto SelectPkgList_RN(string rn)
        {
            try
            {
                erp_con.Close();
                SqlCommand cmd = new SqlCommand("Select top 1 RIGHT(pl_rn, LEN(pl_rn) - 6) as pl_rn from tbl_npi_bom_hdr where pl_rn like @pl_rn + '%'order by add_date desc", erp_con);
                cmd.Parameters.AddWithValue("@pl_rn", rn);
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

        public PRD_dto GeneratePkgListHdr(string com, string pl_no, string brand, string mstr_ctlno, string ver, string ef_dt,string refno,string usn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_npi_bom_hdr (pl_rn,com,b_code,mstr_catalog,ver,sts,effect_date,refno,add_date,add_usn) values (@pl_rn,@com,@brand,@mctlno,@ver,'O',@ef_dt,@refno,GetDate(),@usn)", erp_con);
                cmd.Parameters.AddWithValue("@pl_rn", pl_no);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@brand", brand);
                cmd.Parameters.AddWithValue("@mctlno", mstr_ctlno);
                cmd.Parameters.AddWithValue("@ver", ver);
                cmd.Parameters.AddWithValue("@ef_dt", ef_dt);
                cmd.Parameters.AddWithValue("@refno", refno);
                cmd.Parameters.AddWithValue("@usn", usn);
                dtoresult.cmd = cmd;
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
         
        }

        public PRD_dto GeneratePkgListDtl(string com, string pl_no, string brand, string loc, string ctlno, string dsc, string qty, string usn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_npi_bom_dtl (pl_rn,com,b_code,loc,catalog,dsc,qty,add_date,add_usn) values (@pl_rn,@com,@brand,@loc,@ctlno,@dsc,@qty,GetDate(),@usn)", erp_con);
                cmd.Parameters.AddWithValue("@pl_rn", pl_no);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@brand", brand);
                cmd.Parameters.AddWithValue("@loc", loc);
                cmd.Parameters.AddWithValue("@ctlno", ctlno);
                cmd.Parameters.AddWithValue("@dsc", dsc);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@usn", usn);
                dtoresult.cmd = cmd;
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto InsertPackingList(DataTable dtdetails, string com, string pl_no, string brand, string mstr_ctlno,string ver, string ef_dt,string refno, string usn)
        {
            try
            {
                string loc, ctlno, dsc, qty;
                erp_con.Open();
                trans = erp_con.BeginTransaction();

                //generate pkg list hdr
                dtoresult = GeneratePkgListHdr(com, pl_no, brand, mstr_ctlno, ver, ef_dt,refno, usn);
                dtoresult.cmd.Transaction = trans;
                dtoresult.cmd.ExecuteNonQuery();

                for (int i = 0; i < dtdetails.Rows.Count; i++)
                {
                    loc = dtdetails.Rows[i]["loc"].ToString();
                    ctlno = dtdetails.Rows[i]["catalog_no"].ToString();
                    dsc = dtdetails.Rows[i]["dsc"].ToString();
                    qty = dtdetails.Rows[i]["qty"].ToString();

                    //generate pkg list dtl
                    dtoresult = GeneratePkgListDtl(com, pl_no, brand, loc, ctlno, dsc, qty, usn);
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


        #region Packing Entry

        public PRD_dto SelectMasterCatalog_PackingList(string com,string brand)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select pl_rn,mstr_catalog+char(9)+'|'+char(9)+'version'+char(9)+Cast(ver as varchar(4))as description from tbl_npi_bom_hdr where com=@com and b_code=@brand", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@brand", brand);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtmaster = dtcmd;
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

        public PRD_dto SelectPackingListRefno(string rn)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select refno from tbl_npi_bom_hdr where pl_rn=@rn", erp_con);
                //cmd.Parameters.AddWithValue("@com", com);
                //cmd.Parameters.AddWithValue("@brand", brand);
                //cmd.Parameters.AddWithValue("@ctlno", mstr_ctlno);
                //cmd.Parameters.AddWithValue("@ver", ver);
                cmd.Parameters.AddWithValue("@rn", rn);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtdesc = dtcmd;
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

        public PRD_dto SelectCustCode(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select ah_code,ah_code+char(9)+'|'+char(9)+ah_name as description from tbl_npi_account_holder where com=@com", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtcust = dtcmd;
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

        public PRD_dto SelectPackingListDetails(string rn, decimal qty)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select b.catalog, b.dsc,b.loc,b.qty as r_qty,cast(@qty*b.qty as decimal(10,2)) as qty,c.qty as on_qty,'O' as sts from tbl_npi_bom_hdr as a inner join tbl_npi_bom_dtl as b "+
                                                "on a.pl_rn = b.pl_rn and a.com = b.com inner join tbl_inv as c on b.com = c.com and b.catalog = c.catalog_no where "+
                                                "a.pl_rn = @rn",erp_con);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@rn", rn);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtpkgdetails = dtcmd;
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

        public PRD_dto SelectMasterCatalogByRN(string rn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select mstr_catalog from tbl_npi_bom_hdr where pl_rn =@rn", erp_con);
                cmd.Parameters.AddWithValue("@rn", rn);
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
        }

        public PRD_dto SelectPacking_RN(string rn)
        {
            try
            {
                erp_con.Close();
                SqlCommand cmd = new SqlCommand("Select top 1 RIGHT(pkg_no, LEN(pkg_no) - 6) as pkg_no from tbl_npi_packing_hdr where pkg_no like @pkg_no + '%'order by add_date desc", erp_con);
                cmd.Parameters.AddWithValue("@pkg_no", rn);
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

        public PRD_dto GeneratePackingHdr(string brand, string rn,string com, string m_ctlno, string refno, string qty, string cust, string rmk, string trans_dt, string usn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_npi_packing_hdr (pkg_no,com,brd_code,mstr_catalog,ref_no,qty,cust_code,rmk,sts,trans_date,add_date,add_usn) values (@rn,@com,@brand,@m_ctlno,@refno,@qty,@cust,@rmk,'O',@trans_date,GetDate(),@usn)", erp_con);
                cmd.Parameters.AddWithValue("@rn", rn);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@brand", brand);
                cmd.Parameters.AddWithValue("@m_ctlno", m_ctlno);
                cmd.Parameters.AddWithValue("@refno", refno);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@cust", cust);
                cmd.Parameters.AddWithValue("@rmk", rmk);
                cmd.Parameters.AddWithValue("@trans_date", trans_dt);
                cmd.Parameters.AddWithValue("@usn", usn);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto GeneratePackingDtl(string rn, string com, string ctlno, string dsc, string loc, string qty, string sts, string usn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_npi_packing_dtl (pkg_no,com,catalog_no,dsc,loc,qty,sts,add_date,add_usn) values(@rn,@com,@ctlno,@dsc,@loc,@qty,@sts,GetDate(),@usn)", erp_con);
                cmd.Parameters.AddWithValue("@rn", rn);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@ctlno", ctlno);
                cmd.Parameters.AddWithValue("@dsc", dsc);
                cmd.Parameters.AddWithValue("@loc", loc);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@sts", sts);
                cmd.Parameters.AddWithValue("@usn", usn);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto InsertNewPacking(DataTable dtdetails,string com, string brand, string mstr_ctlno,string refno, string rn, string rmk, string trans_date, string cust_code, string qty, string usn)
        {
            try
            {
                string ctlno, dsc, loc, t_qty, sts;
                erp_con.Open();
                trans = erp_con.BeginTransaction();
                //generate packing header 
                dtoresult = GeneratePackingHdr(brand, rn, com, mstr_ctlno, refno, qty, cust_code, rmk, trans_date, usn);
                dtoresult.cmd.Transaction = trans;
                dtoresult.cmd.ExecuteNonQuery();

                //generate packing details
                for (int i = 0; i < dtdetails.Rows.Count; i++)
                {
                    ctlno = dtdetails.Rows[i]["catalog"].ToString();
                    dsc = dtdetails.Rows[i]["dsc"].ToString();
                    loc = dtdetails.Rows[i]["loc"].ToString();
                    t_qty = dtdetails.Rows[i]["qty"].ToString();
                    sts = dtdetails.Rows[i]["sts"].ToString();

                    dtoresult = GeneratePackingDtl(rn, com, ctlno, dsc, loc, t_qty, sts, usn);
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


        #region Certify Packing

        public PRD_dto SelectPackingBrand(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select distinct a.brd_code as brand_code,a.brd_code+char(9)+'|'+char(9)+b.description as description from tbl_npi_packing_hdr as a inner join tbl_npi_brand as b on a.brd_code=b.brand_code and a.com=b.com where a.com=@com and a.sts='O'", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtbrandcode = dtcmd;
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

        public PRD_dto SelectPackingMaster(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select distinct a.mstr_catalog,a.mstr_catalog+char(9)+'|'+char(9)+b.dsc as description from tbl_npi_packing_hdr as a inner join tbl_inv as b "+
                                                "on a.com=b.com and a.brd_code =b.brand_code and a.mstr_catalog=b.catalog_no where a.com=@com and a.sts='O'", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtmaster = dtcmd;
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

        public PRD_dto SelectPackingCustCode(string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select a.cust_code,a.cust_code+char(9)+'|'+char(9)+b.ah_name as description from tbl_npi_packing_hdr as a " +
                                                "inner join tbl_npi_account_holder as b on a.cust_code = b.ah_code and a.com = b.com where a.com=@com and a.sts='O'", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtcust = dtcmd;
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

        public PRD_dto SelectPackingInfo(string com, string value)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("select sts ,brd_code,mstr_catalog,pkg_no,LEFT(CONVERT(VARCHAR, trans_date, 120), 10)as trans_date,rmk from tbl_npi_packing_hdr " +
                                                "where com =@com and sts='O' " + value + "", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtpkginfo = dtcmd;
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

        public PRD_dto SelectPackingDetails_RN(string com, string rn)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select a.pkg_no,a.catalog_no,a.dsc,a.loc,a.qty as req_qty,b.qty as o_qty from tbl_npi_packing_dtl as a inner join " +
                                               "tbl_inv as b on a.com = b.com and a.catalog_no = b.catalog_no where a.pkg_no=@rn and a.com=@com", erp_con);
                cmd.Parameters.AddWithValue("@rn", rn);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtpkgdetails = dtcmd;
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

        public PRD_dto SelectCertifiedPKG(string rn)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_npi_packing_hdr where sts ='C' and pkg_no=@rn", erp_con);
                cmd.Parameters.AddWithValue("@rn", rn);
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

        public PRD_dto GenerateUpdatePackingHdrSts(string sts, string com, string rn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update tbl_npi_packing_hdr set sts=@sts where com=@com and pkg_no=@rn", erp_con);
                cmd.Parameters.AddWithValue("@sts", sts);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@rn", rn);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto GenerateUpdatePackingDtlSts(string sts, string com, string rn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update tbl_npi_packing_dtl set sts=@sts where com=@com and pkg_no=@rn", erp_con);
                cmd.Parameters.AddWithValue("@sts", sts);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@rn", rn);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto GenerateUpdateInvQty(string ctlno, string com, decimal qty,string loc)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update tbl_inv set qty=qty-@qty where com=@com and loc=@loc and catalog_no=@ctlno", erp_con);
                cmd.Parameters.AddWithValue("@qty", qty);
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

        public PRD_dto GenerateInsertPackingTransaction(string com,string rn,string ctlno,string dsc,string loc,string qty,string trans_date,string usn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_npi_packing_trans (com,rn,catalog_no,dsc,loc,qty,trans_date,add_date,add_usn) values (@com,@rn,@ctlno,@dsc,@loc,@qty,@trans_date,GetDate(),@usn)", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@rn", rn);
                cmd.Parameters.AddWithValue("@ctlno", ctlno);
                cmd.Parameters.AddWithValue("@dsc", dsc);
                cmd.Parameters.AddWithValue("@loc", loc);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@trans_date", trans_date);
                //cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@usn", usn);
                dtoresult.cmd = cmd;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PRD_dto UpdatePackingInfo(DataTable dtheader,DataTable dtdetails,string com,string usn)
        {
            try
            {
                erp_con.Open();
                trans = erp_con.BeginTransaction();
                string rn,pkg_no,sts, ctlno, dsc, loc, qty, trans_date;
                decimal req_qty;
                //generate update packing hdr
                for (int i = 0; i < dtheader.Rows.Count; i++)
                {
                    rn = dtheader.Rows[i]["rn"].ToString();
                    sts = dtheader.Rows[i]["sts"].ToString();

                    dtoresult = GenerateUpdatePackingHdrSts(sts, com, rn);
                    dtoresult.cmd.Transaction = trans;
                    dtoresult.cmd.ExecuteNonQuery();

                    dtoresult = GenerateUpdatePackingDtlSts(sts, com, rn);
                    dtoresult.cmd.Transaction = trans;
                    dtoresult.cmd.ExecuteNonQuery();

                }

                for (int i = 0; i < dtdetails.Rows.Count; i++)
                {
                    pkg_no = dtdetails.Rows[i]["rn"].ToString();
                    ctlno = dtdetails.Rows[i]["ctlno"].ToString();
                    dsc = dtdetails.Rows[i]["dsc"].ToString();
                    loc = dtdetails.Rows[i]["loc"].ToString();
                    qty = dtdetails.Rows[i]["qty"].ToString();
                    trans_date = dtdetails.Rows[i]["trans_date"].ToString();
                    req_qty = decimal.Parse(qty);

                    dtoresult = GenerateUpdateInvQty(ctlno, com, req_qty, loc);
                    dtoresult.cmd.Transaction = trans;
                    dtoresult.cmd.ExecuteNonQuery();

                    dtoresult = GenerateInsertPackingTransaction(com, pkg_no, ctlno, dsc, loc, qty, trans_date, usn);
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