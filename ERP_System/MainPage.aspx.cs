using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_System
{
    public partial class MainPage : System.Web.UI.Page
    {

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com, usn, form,sec,dpt;

        protected void Page_Load(object sender, EventArgs e)
        {
          
            usn = (string)Session["usn"];
            com = (string)Session["com"];
            sec = (string)Session["sec"];
            dpt = (string)Session["dpt"];
            form = "MAIN";         

            if (!IsPostBack)
            {

                //check permission
                CheckPermission(usn, form);

            }
        }

        #region Form Permission
        private void CheckPermission(string usn, string form)
        {
            try
            {
                //check form permission
                Log_result = Log_Process.CheckUserLogin(usn, form);
                if (Log_result.access == true)
                {
                    if (Log_result.sts != true)
                    {
                        Response.Redirect("~/MainPage.aspx", false);
                    }
                    else
                    {
                        //DisplayCompanyCode(com);
                        //DisplayUOM();
                        //string com;
                        //com = ddlcom.SelectedItem.Value;           
                        //Session.Add("com",com);
                        //CreateItemTable();
                        //DisplayCatalog();
                    }

                }
                else
                {
                    Response.Redirect("~/index.aspx", false);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion 
    }
}