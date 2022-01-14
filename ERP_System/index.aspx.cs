using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
namespace ERP_System
{
    public partial class index : System.Web.UI.Page
    {
        Login_dto dtoresult = new Login_dto();
        Login_bo Process = new Login_bo();

        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                DisplayCompanyCode();             
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();       
                //new data
            }
        }

        #region Display Data

        private void DisplayFailResult(string message)
        {
            resulttimer.Enabled = true;
            lblsaveresult.Text = message;
            //lblresult.Text = message;
            lblsaveresult.ForeColor = System.Drawing.Color.Red;
            // lblresult.ForeColor = System.Drawing.Color.Red;
        }

        private void DisplayPassResult(string message)
        {
            resulttimer.Enabled = true;
            lblsaveresult.Text = message;
            // lblresult.Text = message;
            lblsaveresult.ForeColor = System.Drawing.Color.Blue;
            // lblresult.ForeColor = System.Drawing.Color.Blue;
        }

        private void DisplayCompanyCode()
        {
            try
            {
                dtoresult = Process.DisplayCompanyCode();
                if (dtoresult.dtcom.Rows.Count > 0)
                {
                    ddlcom.DataSource = dtoresult.dtcom;
                    ddlcom.DataTextField = "company_code";
                    ddlcom.DataValueField = "company_code";
                    ddlcom.DataBind();
                    ddlcom.Items.Insert(0, new ListItem(String.Empty, String.Empty));               
                    //ddlcom.Items.Insert(1, new ListItem("JL", "JL"));
                    ddlcom.SelectedIndex = 0;
                }
                else
                {
                    ddlcom.Items.Clear();
                    ddlcom.DataSource = null;
                    ddlcom.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                string usn, pwd,com;
                usn = txtloginid.Value;
                pwd = txtpwd.Value;
                com = ddlcom.SelectedItem.Value;

                dtoresult = Process.CheckLogin(usn, pwd,com);
                if (dtoresult.message == "OK")
                {
                    Session["usn"] = usn;
                    Session["com"] = dtoresult.dtUser.Rows[0]["com"].ToString();
                    Session["dpt"]= dtoresult.dtUser.Rows[0]["dpt"].ToString();
                   // Session["post"] = dtoresult.dtUser.Rows[0]["post"].ToString();
                    Session["sec"] = dtoresult.dtUser.Rows[0]["sec"].ToString();
                    Session["nm"] = dtoresult.dtUser.Rows[0]["usn"].ToString();
                    Response.Redirect("MainPage.aspx", false);
                }
                else
                {
                    DisplayFailResult(dtoresult.message);
                    txtloginid.Value = "";
                    txtpwd.Value = "";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void resulttimer_Tick(object sender, EventArgs e)
        {
            lblsaveresult.Text = "";
            resulttimer.Enabled = false;
        }
    }
}