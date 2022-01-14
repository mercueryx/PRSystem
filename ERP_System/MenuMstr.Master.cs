using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ERP_System
{
    public partial class MenuMstr : System.Web.UI.MasterPage
    {
        Login_dto dtoresult = new Login_dto();
        Login_bo Process = new Login_bo();
        string com, usn;

        protected void Page_Load(object sender, EventArgs e)
        {
            usn = (string)Session["usn"];
            com = (string)Session["com"];

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
            Response.Cache.SetNoStore();

            if (!IsPostBack)
            {
                if (Session["usn"] == null)
                {
                   
                    Response.Redirect("~/index.aspx");
                  
                }
                else
                {
                    UserPermission(com, usn);
                }
            }
            Page.Header.DataBind();
          
        }

        #region Check Permission

        private void UserPermission(string com, string usn)
        {
            try
            {
                if (!string.IsNullOrEmpty(com) && !string.IsNullOrEmpty(usn))
                {
                    string module, second_module, third_module;
                    dtoresult = Process.CheckPermission(com, usn);
                    if (dtoresult.dtpermission.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtoresult.dtpermission.Rows.Count; i++)
                        {
                            module = dtoresult.dtpermission.Rows[i]["module_code"].ToString();
                            second_module = dtoresult.dtpermission.Rows[i]["second_module_code"].ToString();
                            third_module = dtoresult.dtpermission.Rows[i]["third_module"].ToString();

                            FindControl(module).Visible = true;
                            FindControl(second_module).Visible = true;
                            FindControl(third_module).Visible = true;

                        }
                    }
                }
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
    }
}