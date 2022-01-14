using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ERP_System.ADM_Module.Forms
{
    /// <summary>
    /// Summary description for PR_item
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PR_item : System.Web.Services.WebService
    {
        SqlConnection con = new SqlConnection(ResourceModule.ERP_con);
        [WebMethod]
        public List<string> GetItemNames(string com)
        {
            List<string> ListItem = new List<string>();
            // string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            //{
            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select login_id from tbl_erp_user where com = @com ", con);
                //cmd.CommandType = CommandType.StoredProcedure;

                //SqlParameter parameter = new SqlParameter()
                //{
                //    ParameterName = "@term",
                //    Value = term
                //};
                //cmd.Parameters.Add(parameter);
                //con.Open();
                cmd.Parameters.AddWithValue("@com", com);
                //cmd.Parameters.AddWithValue("@cat", category);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ListItem.Add(string.Format("{0}", rdr["user_list"]));
                }
                return ListItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            // }
        }
    }
}
