using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERP_System.ADM_Module.ADM_Module;

namespace ERP_System.ADM_Module.Forms
{
    public partial class Create_User : System.Web.UI.Page
    {
        ADM_dto dtoresult = new ADM_dto();
        ADM_bo Process = new ADM_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string usn,form;

        protected void Page_Load(object sender, EventArgs e)
        {
            form = "CREATE_USER";
            usn = (string)Session["usn"];
      

            if (!IsPostBack)
            {
                //check permission
                CheckPermission(usn, form);
                //DisplayCompanyCode();
                //DisplayDepartment();
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
                        DisplayCompanyCode();
                        DisplayDepartment();
                        
                    }

                }
                else
                {
                    Response.Redirect("~/index.aspx", false);
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        #endregion 

        #region Display Data
        
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
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayDepartment()
        {
            try
            {
                dtoresult = Process.DisplayDepartment();
                if (dtoresult.dtdpt.Rows.Count > 0)
                {
                    ddldpt.DataSource = dtoresult.dtdpt;
                    ddldpt.DataTextField = "dpt";
                    ddldpt.DataValueField = "dpt";
                    ddldpt.DataBind();
                    ddldpt.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddldpt.SelectedIndex = 0;
                }
                else
                {
                    ddldpt.Items.Clear();
                    ddldpt.DataSource = null;
                    ddldpt.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplaySection(string dpt)
        {
            try
            {
                dtoresult = Process.DisplaySection(dpt);
                if (dtoresult.dtSec.Rows.Count > 0)
                {
                    ddlsection.DataSource = dtoresult.dtSec;
                    ddlsection.DataTextField = "sec";
                    ddlsection.DataValueField = "sec";
                    ddlsection.DataBind();
                    ddlsection.Items.Insert(0, new ListItem("Select Section", "Select Section"));
                    //ddlcom.Items.Insert(1, new ListItem("JL", "JL"));
                    ddlsection.SelectedIndex = 0;
                }
                else
                {
                    ddlsection.Items.Clear();
                    ddlsection.DataSource = null;
                    ddlsection.DataBind();
                }
            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }

        }

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

        #endregion

        #region Reset Text

        private void ResetData()
        {
            txtemail.Text = "";
            txtempid.Text = "";
            txtpwd.Text = "";
            txtusn.Text = "";
            DisplayCompanyCode();
            DisplayDepartment();
        }


        #endregion

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                string com, dpt, pwd, emp, usn, email,sec;

                if (ddlcom.Items.Count ==0)
                {
                    com = "";
                }
                else
                {
                    com = ddlcom.SelectedItem.Value;
                }

            
                if (ddldpt.Items.Count == 0)
                {
                    dpt = "";
                }
                else
                {
                    dpt = ddldpt.SelectedItem.Value;
                }

                if (ddlsection.Items.Count == 0)
                {
                    sec = "";
                }
                else
                {
                    sec = ddlsection.SelectedItem.Value;
                }
                //com = ddlcom.SelectedItem.Value;
                //dpt = ddldpt.SelectedItem.Value;
                emp = txtempid.Text.Trim().ToUpper();
                email = txtemail.Text.Trim();
                pwd = txtpwd.Text.Trim();
                usn = txtusn.Text;
                dtoresult = Process.CreateNewUser(com, emp, usn, pwd, email, dpt,sec);
                if (dtoresult.sts == true)
                {
                    DisplayPassResult("Add new user successful.");
                    ResetData();
                }
                else
                {
                    DisplayFailResult(dtoresult.message);
                }

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void ddldpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string dpt;
            


                if (ddldpt.Items.Count == 0)
                {
                    dpt = "";
                }
                else
                {
                    dpt = ddldpt.SelectedItem.Value;
                }
                DisplaySection(dpt);
            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }
        }

        //protected void btnnew_Click(object sender, EventArgs e)
        //{

        //}

        protected void resulttimer_Tick(object sender, EventArgs e)
        {
            lblsaveresult.Text = "";
            resulttimer.Enabled = false;
        }
    }
}