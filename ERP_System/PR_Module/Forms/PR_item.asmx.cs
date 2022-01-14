using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ERP_System.PR_Module.Forms
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
        public List<string> GetItemNames(string item)
        {
            List<string> ListItem = new List<string>();
            // string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            //{
            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select item_name from tbl_pr_item where item_name like @item + '%'", con);
                //cmd.CommandType = CommandType.StoredProcedure;

                //SqlParameter parameter = new SqlParameter()
                //{
                //    ParameterName = "@term",
                //    Value = term
                //};
                //cmd.Parameters.Add(parameter);
                //con.Open();
                cmd.Parameters.AddWithValue("@item", item);
                //cmd.Parameters.AddWithValue("@cat", category);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ListItem.Add(string.Format("{0}", rdr["item_name"]));
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
