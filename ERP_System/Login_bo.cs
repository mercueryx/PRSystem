using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Text;
namespace ERP_System
{
    public class Login_bo
    {
        Login_dto dtoresult = new Login_dto();
        Login_da ProcessData = new Login_da();

        public Login_dto DisplayCompanyCode()
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

        public Login_dto CheckLogin(string usn,string pwd,string com)
        {
            try
            {
                string c_pwd;
                c_pwd = "";

              
                if (string.IsNullOrEmpty(usn))
                {
                    dtoresult.message = "Login id cannot empty.";
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(pwd))
                {
                    dtoresult.message = "Password cannot empty.";
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(com))
                {
                    dtoresult.message = "Please select company code first.";
                    return dtoresult;
                }


                dtoresult = ProcessData.SelectUser(usn,com);
                if (dtoresult.dtUser.Rows.Count > 0)
                {
                    c_pwd = dtoresult.dtUser.Rows[0]["pwd"].ToString();
                    c_pwd = Decrypt(c_pwd);

                    if (pwd == c_pwd)
                    {
                        dtoresult.message = "OK";
                    }
                    else
                    {
                        dtoresult.message = "Invalid Password.";
                    }
                }
                else
                {
                    dtoresult.message = "Invalid Account.";
                    
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

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText =Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public Login_dto CheckPermission(string com, string usn)
        {
            try
            {
                dtoresult = ProcessData.SelectUserPermission(com, usn);
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Login_dto CheckUserLogin(string usn,string form)
        {
            try
            {
                if (string.IsNullOrEmpty(usn))
                {
                    dtoresult.access = false;
                }
                else
                {
                    if (form == "MAIN")
                    {
                        dtoresult.access = true;
                        dtoresult.sts = true;
                    }
                    else
                    {
                        dtoresult = ProcessData.SelectFormPermission(usn, form);
                        if (dtoresult.dtpermission.Rows.Count > 0)
                        {
                            dtoresult.access = true;
                            dtoresult.sts = true;
                        }
                        else
                        {
                            dtoresult.sts = false;
                        }
                    }

                }
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}