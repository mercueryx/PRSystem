using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace ERP_System
{
    public class Login_da
    {
        SqlConnection erp_con = new SqlConnection(ResourceModule.ERP_con);
        SqlConnection ibas_con = new SqlConnection(ResourceModule.IBAS_con);
        Login_dto dtoresult = new Login_dto();

        #region Login
        public Login_dto SelectUser(string usn,string com)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select login_id,com,pwd,usn,dpt,sec from tbl_erp_user where login_id=@usn and com=@com", erp_con);
                cmd.Parameters.AddWithValue("@usn", usn);
                cmd.Parameters.AddWithValue("@com", com);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtUser = dtcmd;
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

        public Login_dto SelectCompany()
        {
            try
            {
                ibas_con.Open();
                SqlDataAdapter cmd = new SqlDataAdapter("Select distinct company_code from sys_user_privilege_file", ibas_con);
                DataTable dtcmd = new DataTable();
                cmd.Fill(dtcmd);
                dtoresult.dtcom = dtcmd;
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

        #region User Permission

        public Login_dto SelectUserPermission(string com, string usn)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_user_permission where com=@com and login_id=@usn", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@usn", usn);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtpermission = dtcmd;
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

        public Login_dto SelectFormPermission(string usn, string form)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_user_permission where login_id=@usn and module_code =@form or login_id=@usn and second_module_code=@form or login_id=@usn and third_module=@form", erp_con);
                cmd.Parameters.AddWithValue("@form", form);
                cmd.Parameters.AddWithValue("@usn", usn);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtpermission = dtcmd;
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