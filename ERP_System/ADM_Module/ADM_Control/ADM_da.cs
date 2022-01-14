using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace ERP_System.ADM_Module.ADM_Module
{
    public class ADM_da
    {
        SqlConnection erp_con = new SqlConnection(ResourceModule.ERP_con);
        SqlConnection ibas_con = new SqlConnection(ResourceModule.IBAS_con);
        ADM_dto dtoresult = new ADM_dto();

        #region Create User

        public ADM_dto SelectCompany()
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

        public ADM_dto SelectDepartment()
        {
            try
            {
                erp_con.Open();
                SqlDataAdapter cmd = new SqlDataAdapter("Select distinct dpt from tbl_user_dpt", erp_con);
                DataTable dtcmd = new DataTable();
                cmd.Fill(dtcmd);
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

        public ADM_dto SelectSection( string dpt)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select distinct sec from tbl_user_dpt where dpt=@dpt", erp_con);
                //cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@dpt", dpt);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtSec = dtcmd;
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

        public ADM_dto SelectDuplicateEmpNo(string com, string emp)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_erp_user where login_id=@emp and com=@com", erp_con);
                cmd.Parameters.AddWithValue("@emp", emp);
                cmd.Parameters.AddWithValue("@com", com);
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

        public ADM_dto InsertNewUser(string com, string id, string usn, string pwd, string dpt, string email,string sec)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Insert into tbl_erp_user (com,login_id,pwd,usn,email,dpt,sec,dtn) values (@com,@emp,@pwd,@usn,@email,@dpt,@sec,GetDate())", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@emp", id);
                cmd.Parameters.AddWithValue("@pwd", pwd);
                cmd.Parameters.AddWithValue("@usn", usn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@dpt", dpt);
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

        #endregion

        #region User Permission

        public ADM_dto SelectFirstModule()
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select module,module_code from tbl_module", erp_con);
                //cmd.Parameters.AddWithValue("@user", prefix);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtfirst_module = dtcmd;
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

        public ADM_dto SelectSecondModule(string first_module)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select second_module,second_module_code from tbl_second_module where module_code=@f_code", erp_con);
                cmd.Parameters.AddWithValue("@f_code", first_module);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtsecond_module = dtcmd;
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

        public ADM_dto SelectThirdModule(string second_module)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select third_module,third_module_code from tbl_third_module where second_module_code=@s_code", erp_con);
                cmd.Parameters.AddWithValue("@s_code", second_module);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dtcmd = new DataTable();
                adt.Fill(dtcmd);
                dtoresult.dtthird_module = dtcmd;
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

        public ADM_dto SelectUserList(string keyword,string prefix)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select login_id from tbl_erp_user where login_id like '%' + @user + '%' " + keyword, erp_con);
                cmd.Parameters.AddWithValue("@user", prefix);
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

        public ADM_dto SelectUserPermission(string com, string usn)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select id, module_code,second_module_code,third_module from tbl_user_permission where login_id=@id and com=@com order by module_code", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@id", usn);
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

        public ADM_dto SelectSameModule_User(string com, string user, string module_code, string second_module, string third_module)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_user_permission where com=@com and login_id=@id and module_code=@f_code " +
                                                "and second_module_code=@s_module and third_module=@t_module", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@id", user);
                cmd.Parameters.AddWithValue("@f_code", module_code);
                cmd.Parameters.AddWithValue("@s_module", second_module);
                cmd.Parameters.AddWithValue("@t_module", third_module);
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

        public ADM_dto InsertNewPrivilege(string n_com, string n_user, string module_code, string second_module, string third_module, string usn)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Insert into tbl_user_permission (com,login_id,module_code,second_module_code,third_module,add_date,add_usn) values " +
                                                "(@com,@id,@f_code,@s_code,@t_code,GetDate(),@usn)", erp_con);
                cmd.Parameters.AddWithValue("@com", n_com);
                cmd.Parameters.AddWithValue("@id", n_user);
                cmd.Parameters.AddWithValue("@f_code", module_code);
                cmd.Parameters.AddWithValue("@s_code", second_module);
                cmd.Parameters.AddWithValue("@t_code", third_module);
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

        public ADM_dto DeleteSelectedPrivilege(string com, string user_id, string id)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Delete from tbl_user_permission where com=@com and login_id=@login_id and id=@id", erp_con);
                cmd.Parameters.AddWithValue("@com",com);
                cmd.Parameters.AddWithValue("@login_id", user_id);
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


        #region User Privilege Refer
        public ADM_dto SelectUserAccount(string com, string user_id)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_erp_user where com=@com and login_id=@id", erp_con);
                cmd.Parameters.AddWithValue("@com",com);
                cmd.Parameters.AddWithValue("@id", user_id);
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

        public ADM_dto SelectCountUserPrivilege(string com, string usn)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tbl_user_permission where com=@com and login_id=@id", erp_con);
                cmd.Parameters.AddWithValue("@com", com);
                cmd.Parameters.AddWithValue("@id", usn);
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

        public ADM_dto DuplicateUserPrivilege(string n_com,string n_usn,string r_com, string r_usn,string usn)
        {
            try
            {
                erp_con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_user_permission (com, login_id, module_code,second_module_code,third_module,add_date,add_usn) " +
                                                "SELECT @n_com, @n_id,module_code,second_module_code,third_module,GetDate(),@usn FROM tbl_user_permission " +
                                                "WHERE com=@com and login_id=@id", erp_con);
                cmd.Parameters.AddWithValue("@n_com",n_com);
                cmd.Parameters.AddWithValue("@n_id",n_usn);
                cmd.Parameters.AddWithValue("@usn",usn);
                cmd.Parameters.AddWithValue("@com",r_com);
                cmd.Parameters.AddWithValue("@id",r_usn);
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