using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERP_System.ADM_Module.ADM_Module;
namespace ERP_System.ADM_Module.Forms
{
    public partial class User_Permission : System.Web.UI.Page
    {

        ADM_dto dtoresult = new ADM_dto();
        ADM_bo process = new ADM_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();


        string com, usn, form, sec, dpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            usn = (string)Session["usn"];
            com = (string)Session["com"];
            sec = (string)Session["sec"];
            dpt = (string)Session["dpt"];
            form = "USER_PERMISSION";


            if (!IsPostBack)
            {
                DisplayFirstModule();
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
                        DisplayCompanyCode();

                        //CreateItemTable();
                        // DisplayCatalog();
                        //RefreshGrid("","");
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


        #region DisplayData
        private void DisplayCompanyCode()
        {
            try
            {
                Log_result = Log_Process.DisplayCompanyCode();
                if (Log_result.dtcom.Rows.Count > 0)
                {
                    ddlcom.DataSource = Log_result.dtcom;
                    ddlcom.DataTextField = "company_code";
                    ddlcom.DataValueField = "company_code";
                    ddlcom.DataBind();
                    ddlcom.Items.Insert(0, new ListItem("Select Company", "Select Company"));
                    //ddlcom.Items.Insert(1, new ListItem("JL", "JL"));
                    ddlcom.SelectedIndex = 0;



                    ddl_r_com.DataSource = Log_result.dtcom;
                    ddl_r_com.DataTextField = "company_code";
                    ddl_r_com.DataValueField = "company_code";
                    ddl_r_com.DataBind();
                    ddl_r_com.Items.Insert(0, new ListItem("Select Company", "Select Company"));
                    //ddlcom.Items.Insert(1, new ListItem("JL", "JL"));
                    ddl_r_com.SelectedIndex = 0;
                }
                else
                {
                    ddlcom.Items.Clear();
                    ddlcom.DataSource = null;
                    ddlcom.DataBind();

                    ddl_r_com.Items.Clear();
                    ddl_r_com.DataSource = null;
                    ddl_r_com.DataBind();
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

        private void DisplayFirstModule()
        {
            try
            {
                dtoresult = process.DisplayFirstModule();
                if (dtoresult.dtfirst_module.Rows.Count > 0)
                {
                    ddlfirst.DataSource = dtoresult.dtfirst_module;
                    ddlfirst.DataTextField = "module";
                    ddlfirst.DataValueField = "module_code";
                    ddlfirst.DataBind();
                    ddlfirst.Items.Insert(0, new ListItem("Select First Module", "Select First Module"));
                    //ddlcom.Items.Insert(1, new ListItem("JL", "JL"));
                    ddlfirst.SelectedIndex = 0;
                }
                else
                {
                    ddlfirst.Items.Clear();
                    ddlfirst.DataSource = null;
                    ddlfirst.DataBind();
                }
            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }

        }

        private void DisplaySecondModule(string f_code)
        {
            try
            {
                dtoresult = process.DisplaySecondModule(f_code);
                if (dtoresult.dtsecond_module.Rows.Count > 0)
                {
                    ddlsecond.DataSource = dtoresult.dtsecond_module;
                    ddlsecond.DataTextField = "second_module";
                    ddlsecond.DataValueField = "second_module_code";
                    ddlsecond.DataBind();
                    ddlsecond.Items.Insert(0, new ListItem("Select Second Module", "Select Second Module"));
                    //ddlcom.Items.Insert(1, new ListItem("JL", "JL"));
                    ddlsecond.SelectedIndex = 0;
                }
                else
                {
                    ddlsecond.Items.Clear();
                    ddlsecond.DataSource = null;
                    ddlsecond.DataBind();
                }
            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayThirdModule(string s_code)
        {
            try
            {
                dtoresult = process.DisplayThirdModule(s_code);
                if (dtoresult.dtthird_module.Rows.Count > 0)
                {
                    ddlthird.DataSource = dtoresult.dtthird_module;
                    ddlthird.DataTextField = "third_module";
                    ddlthird.DataValueField = "third_module";
                    ddlthird.DataBind();
                    ddlthird.Items.Insert(0, new ListItem("Select Third Module", "Select Third Module"));
                    //ddlcom.Items.Insert(1, new ListItem("JL", "JL"));
                    ddlthird.SelectedIndex = 0;
                }
                else
                {
                    ddlthird.Items.Clear();
                    ddlthird.DataSource = null;
                    ddlthird.DataBind();
                }
            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }
        }

        private void RefreshGrid(string com,string user_id)
        {
            try
            {
                dtoresult = process.DisplayUserPermission(com, user_id);
            
                    if (dtoresult.dtcheck.Rows.Count > 0)
                    {
                        dgvheader.DataSource = dtoresult.dtcheck;
                        dgvheader.DataBind();
                    }
                    else
                    {
                        dgvheader.DataSource = null;
                        dgvheader.DataBind();

                    }
            
            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }
        }

        #endregion


        #region Web Services

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        //[WebMethod(EnableSession = true)]
        public static List<string> GetUser(string prefixText)
        {
            string com;
            //com = HttpContext.Current.Session["s_com"].ToString();
            if (HttpContext.Current.Session["s_com"] != null)
            {
                com = HttpContext.Current.Session["s_com"].ToString();
            }
            else
            {
                com = "Select Company";
            }
            ADM_dto dtoresult = new ADM_dto();
            ADM_bo Process = new ADM_bo();

            List<string> Userlist = new List<string>();

            dtoresult = Process.DisplaySearchUserId(com, prefixText);
            if (dtoresult.message != "No result found")
            {
                Userlist = dtoresult.list_user;

            }

            return Userlist;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetRUser(string prefixText)
        {
            string com;
            if (HttpContext.Current.Session["r_com"] != null)
            {
                com = HttpContext.Current.Session["r_com"].ToString();
            }
            else
            {
                com = "Select Company";
            }
        

            ADM_dto dtoresult = new ADM_dto();
            ADM_bo Process = new ADM_bo();

            List<string> Userlist = new List<string>();

            dtoresult = Process.DisplaySearchReferUserId(com, prefixText);
            if (dtoresult.message != "No result found")
            {
                Userlist = dtoresult.list_r_user;

            }

            return Userlist;
        }
        #endregion


        #region Add / Delete User Privilege

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string com, user_id;
                com = ddlcom.SelectedValue;
                user_id = txtuser.Text;
                dtoresult = process.DisplayUserPermission(com, user_id);
                if (dtoresult.sts == true)
                {             
                        dgvheader.DataSource = dtoresult.dtcheck;
                        dgvheader.DataBind();                               
                }
                else
                {
                    DisplayFailResult(dtoresult.message);
                    dgvheader.DataSource = null;
                    dgvheader.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void ddlfirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string f_code;
                f_code = ddlfirst.SelectedValue;
                DisplaySecondModule(f_code);
                ddlthird.Items.Clear();
                ddlthird.DataSource = null;
                ddlthird.DataBind();

               // ddlsecond.SelectedIndex = 0;
               // ddlthird.SelectedIndex = 0;

            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }
        }

        protected void resulttimer_Tick(object sender, EventArgs e)
        {
            lblsaveresult.Text = "";
            resulttimer.Enabled = false;
        }

        protected void ddlcom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["s_com"] = ddlcom.SelectedValue;

            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }
        }

     
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                string f_code, s_code, t_code,com,user_id;

                if (ddlfirst.Items.Count ==0)
                {
                    f_code = "Select First Module";
                }
                else
                {
                    f_code = ddlfirst.SelectedValue;
                }


                if (ddlsecond.Items.Count == 0)
                {
                    s_code = "Select Second Module";
                }
                else
                {
                    s_code = ddlsecond.SelectedValue;
                }


                if (ddlthird.Items.Count == 0)
                {
                    t_code = "Select Third Module";
                }
                else
                {
                    t_code = ddlthird.SelectedValue;
                }

                com = ddlcom.SelectedValue;
                user_id = txtuser.Text;

                dtoresult = process.InsertNewPrivilege(com, user_id, f_code, s_code, t_code, usn);
                if (dtoresult.sts == true)
                {
                    DisplayPassResult("Add privilege successfull.");
                    RefreshGrid(com, user_id);
                }
               
            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }
        }

        protected void ddlsecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string s_code;
                s_code = ddlsecond.SelectedValue;
                DisplayThirdModule(s_code);
               // ddlsecond.SelectedIndex = 0;
                //ddlthird.SelectedIndex = 0;

            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }
        }

        protected void dgvheader_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string id, com,user_id;

                id = dgvheader.DataKeys[e.RowIndex].Values[0].ToString();
                //ctlno = e.Keys["catalog_no"].ToString();
                com = ddlcom.SelectedValue;
                user_id = txtuser.Text;
                dtoresult = process.DeletePrivilege(com, user_id, id);
                if (dtoresult.sts == true)

                { 
                    DisplayPassResult("Delete privilege successfull.");
                    RefreshGrid(com, user_id);


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

    
        #endregion


        #region User Privilege Refer
        protected void btnrefer_Click(object sender, EventArgs e)
        {
            try
            {
                string n_com,n_user, r_com, r_user;
                r_com = ddl_r_com.SelectedValue;
                r_user = txt_r_user.Text;
                n_com = ddlcom.SelectedValue;
                n_user = txtuser.Text;
                dtoresult = process.ReferUserPrivilege(n_com, n_user, r_com, r_user, usn);
                if (dtoresult.sts == true)
                {
                    DisplayPassResult("User privilege referred successfull.");
                    RefreshGrid(n_com, n_user);

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

        protected void ddl_r_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["r_com"] = ddlcom.SelectedValue;

            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }
        }

        protected void btn_r_search_Click(object sender, EventArgs e)
        {
            try
            {
                string com, user_id;
                com = ddl_r_com.SelectedValue;
                user_id = txt_r_user.Text;
                dtoresult = process.DisplayUserPermission(com, user_id);
                if (dtoresult.sts == true)
                {

                    dgvrefer.DataSource = dtoresult.dtcheck;
                    dgvrefer.DataBind();


                }
                else
                {
                    DisplayFailResult(dtoresult.message);
                    dgvrefer.DataSource = null;
                    dgvrefer.DataBind();

                }
            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }
        }
        #endregion

    }
}