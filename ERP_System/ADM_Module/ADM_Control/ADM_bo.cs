using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace ERP_System.ADM_Module.ADM_Module
{
    public class ADM_bo
    {
        ADM_dto dtoresult = new ADM_dto();
        ADM_da ProcessData = new ADM_da();

        #region Create User

        public ADM_dto DisplayCompanyCode()
        {
            try
            {
                dtoresult = ProcessData.SelectCompany();
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ADM_dto DisplayDepartment()
        {
            try
            {
                dtoresult = ProcessData.SelectDepartment();
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ADM_dto DisplaySection(string dpt)
        {
            try
            {
                dtoresult = ProcessData.SelectSection(dpt);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ADM_dto CreateNewUser(string com, string id, string usn, string pwd, string email, string dpt,string sec)
        {
            try
            {
                if (string.IsNullOrEmpty(com))
                {
                    dtoresult.message = "Company cannot empty.";
                    return dtoresult;
                }
                if (string.IsNullOrEmpty(id))
                {
                    dtoresult.message = "Login id cannot empty.";
                    return dtoresult;
                }
                if (string.IsNullOrEmpty(usn))
                {
                    dtoresult.message = "Username cannot empty.";
                    return dtoresult;
                }
                if (string.IsNullOrEmpty(pwd))
                {
                    dtoresult.message = "Password cannot empty.";
                    return dtoresult;
                }
                if (string.IsNullOrEmpty(email))
                {
                    email = "-";
                }
                if (string.IsNullOrEmpty(dpt))
                {
                    dtoresult.message = "Department cannot empty.";
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(sec))
                {
                    dtoresult.message = "Section cannot empty.";
                    return dtoresult;
                }

                // check duplicate user
                dtoresult = ProcessData.SelectDuplicateEmpNo(com, id);
                if (dtoresult.dtcheck.Rows.Count > 0)
                {
                    dtoresult.message = "This employee no already existed.";

                }
                else
                {
                    dtoresult = ProcessData.InsertNewUser(com, id, usn, Encrypt(pwd), dpt, email,sec);

                }
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        #endregion

        #region User Permission
        public ADM_dto DisplaySearchUserId(string com, string prefix)
        {
            try
            {

                List<string> user_list = new List<string>();

                string keyword;

                if (com == "Select Company")
                {
                    keyword = "";
                }
                else
                {
                    keyword = "and com='" + com + "'";

                }

                dtoresult = ProcessData.SelectUserList(keyword, prefix);
                if (dtoresult.dtcheck.Rows.Count > 0)
                {

                    for (int i = 0; i < dtoresult.dtcheck.Rows.Count; i++)
                    {
                        user_list.Add(dtoresult.dtcheck.Rows[i][0].ToString());
                    }

                }
                else
                {
                    dtoresult.message = "No result found";
                }

                dtoresult.list_user = user_list;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public ADM_dto DisplayFirstModule()
        {
            try
            {
                dtoresult = ProcessData.SelectFirstModule();
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ADM_dto DisplaySecondModule(string f_code)
        {
            try
            {
                dtoresult = ProcessData.SelectSecondModule(f_code);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ADM_dto DisplayThirdModule(string s_code)
        {
            try
            {
                dtoresult = ProcessData.SelectThirdModule(s_code);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ADM_dto DisplayUserPermission(string com, string user_id)
        {
            try
            {
                if (string.IsNullOrEmpty(user_id))
                {
                    dtoresult.message = "User Id cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }
                
                if (com=="Select Company")
                {
                    dtoresult.message = "Company cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                dtoresult = ProcessData.SelectUserPermission(com, user_id);
                if (dtoresult.dtcheck.Rows.Count > 0)
                {
                    dtoresult.sts = true;
                }
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ADM_dto InsertNewPrivilege(string com, string user_id, string f_code, string s_code, string t_code,string usn)
        {
            try
            {

                if (com == "Select Company")
                {
                    dtoresult.message = "Please select company first.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(user_id))
                {
                    dtoresult.message = "User id cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }


                if (f_code == "Select First Module")
                {
                    dtoresult.message = "Please select first module.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (s_code == "Select Second Module")
                {
                    dtoresult.message = "Please select second module.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (t_code == "Select Third Module")
                {
                    dtoresult.message = "Please select third module.";
                    dtoresult.sts = false;
                    return dtoresult;
                }


                //check account validity
                dtoresult = ProcessData.SelectUserAccount(com, user_id);
                if (dtoresult.dtcheck.Rows.Count > 0)
                {
                    //check module exist before or not
                    dtoresult = ProcessData.SelectSameModule_User(com, user_id, f_code, s_code, t_code);
                    if (dtoresult.dtcheck.Rows.Count > 0)
                    {
                        dtoresult.message = "This module privilege already existed.";
                        dtoresult.sts = false;
                        return dtoresult;
                    }
                    else
                    {
                        //add module privilege
                        dtoresult = ProcessData.InsertNewPrivilege(com, user_id, f_code, s_code, t_code, usn);
                        
                    }

                }
                else
                {
                    dtoresult.message = "Invalid account.";
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

        public ADM_dto DeletePrivilege(string com, string user_id, string id)
        {
            try
            {
                if (com == "Select Company")
                {
                    dtoresult.message = "Please select company first.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(user_id))
                {
                    dtoresult.message = "User id cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(id))
                {
                    dtoresult.message = "Please select a privilege to delete.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                dtoresult = ProcessData.DeleteSelectedPrivilege(com, user_id, id);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region User Privilege Refer

        public ADM_dto DisplaySearchReferUserId(string com, string prefix)
        {
            try
            {

                List<string> user_list = new List<string>();

                string keyword;

                if (com == "Select Company")
                {
                    keyword = "";
                }
                else
                {
                    keyword = "and com='" + com + "'";

                }

                dtoresult = ProcessData.SelectUserList(keyword, prefix);
                if (dtoresult.dtcheck.Rows.Count > 0)
                {

                    for (int i = 0; i < dtoresult.dtcheck.Rows.Count; i++)
                    {
                        user_list.Add(dtoresult.dtcheck.Rows[i][0].ToString());
                    }

                }
                else
                {
                    dtoresult.message = "No result found";
                }

                dtoresult.list_r_user = user_list;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public ADM_dto ReferUserPrivilege(string n_com, string n_user_id, string r_com, string r_user_id, string usn)
        {
            try
            {
                //int pri_count;
                //pri_count = 0;
                if (string.IsNullOrEmpty(n_user_id))
                {
                    dtoresult.message = "New user id cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(r_user_id))
                {
                    dtoresult.message = "Referrer user id cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (n_com == "Select Company")
                {
                    dtoresult.message = "New user company code cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }


                if (r_com == "Select Company")
                {
                    dtoresult.message = "Referrer user company code cannot empty.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                //check new user account validity
                dtoresult = ProcessData.SelectUserAccount(n_com, n_user_id);
                if (dtoresult.dtcheck.Rows.Count == 0)
                {
                    dtoresult.message = "Account no found.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                //check new user got assign any privilege or not before
                dtoresult = ProcessData.SelectCountUserPrivilege(n_com, n_user_id);
                if (dtoresult.dtcheck.Rows.Count > 0)
                {
                    //pri_count =Convert.ToInt32(dtoresult.dtcheck.Rows[0]["p_count"].ToString());

                    //if (pri_count > 0)
                    //{

                    //}
                    dtoresult.sts = false;
                    dtoresult.message = "This user already assign some privilege before please remove all before refer to anoth user.";
                    return dtoresult;
                }
                else
                {
                    dtoresult = ProcessData.DuplicateUserPrivilege(n_com, n_user_id, r_com, r_user_id, usn);

                }
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