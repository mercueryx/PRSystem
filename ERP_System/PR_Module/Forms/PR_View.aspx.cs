using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERP_System.PR_Module.PR_Control;
namespace ERP_System.PR_Module.Forms
{
    public partial class PR_View : System.Web.UI.Page
    {
        PR_dto dtoresult = new PR_dto();
        PR_bo Process = new PR_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com, usn, form, dpt,sec;
        string requestor, req_dpt, dt, sts, req_itm;

        protected void Page_Load(object sender, EventArgs e)
        {
            usn = (string)Session["usn"];
            com = (string)Session["com"];
            dpt = (string)Session["dpt"];
            sec = (string)Session["sec"];
            form = "PR_VIEW";

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
                //string requestor, req_dpt, dt,sts;

                requestor = txtnm.Text;
                dt = txtreq_date.Text;
                req_dpt = ddlr_dpt.SelectedValue;
                sts = ddlsts.SelectedValue;
                // Louis Added on 20200817
                string req_item = txtreq_items.Text;
                // End
                // Louis Added on 20201124
                string req_prno = txtprno.Text;
                // End

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

                        //DisplayBrandCode(com);
                        //DisplayMasterCatalog(com);
                        //DisplayCustCode(com);
                        //CreateHeaderTable();
                        //CreateDetailsTable();
                        DisplayMyPR(com,usn,sts,req_dpt, requestor, dt,sec, req_item, req_prno);
                        DisplayDepartment(com);
                        //CreateUpdatePOTable();
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

        private void DisplayMyPR(string com,string usn,string sts,string req_dpt, string requestor, string dt,string sec, string req_items, string req_prno)
        {
            try
            {

                dtoresult = Process.ViewMyPR(com,usn,requestor,req_dpt, dt, sts,sec,dpt, req_items,req_prno);
                if (dtoresult.dtitem.Rows.Count > 0)
                {
                    dgvheader.DataSource = dtoresult.dtitem;
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

        protected void dgvheader_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvheader.PageIndex = e.NewPageIndex;
            requestor = txtnm.Text;
            dt = txtreq_date.Text;
            req_dpt = ddlr_dpt.SelectedValue;
            sts = ddlsts.SelectedValue;
            // Louis Added on 20200817
            string req_item = txtreq_items.Text;
            // End
            // Louis Added on 20201124
            string req_prno = txtprno.Text;
            // End
            DisplayMyPR(com, usn, sts, req_dpt, requestor, dt, sec, req_item, req_prno);
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

        private void DisplayDepartment(string com)
        {
            try
            {
                dtoresult = Process.DisplayDepartment(com);
                if (dtoresult.dtdpt.Rows.Count > 0)
                {
                    ddlr_dpt.DataSource = dtoresult.dtdpt;
                    ddlr_dpt.DataTextField = "dpt";
                    ddlr_dpt.DataValueField = "dpt";
                    ddlr_dpt.DataBind();
                    ddlr_dpt.Items.Insert(0, new ListItem("All Department", ""));
                    //ddlcom.Items.Insert(1, new ListItem("JL", "JL"));
                    ddlr_dpt.SelectedIndex = 0;
                }
                else
                {
                    ddlr_dpt.Items.Clear();
                    ddlr_dpt.DataSource = null;
                    ddlr_dpt.DataBind();
                }
            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }
        }

        #endregion


        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {

                string req_dpt, nm, dt,sts;
                req_dpt = ddlr_dpt.SelectedValue;
                nm = txtnm.Text;
                dt = txtreq_date.Text;
                sts = ddlsts.SelectedValue;
                // Louis Added on 20200817
                string req_item = txtreq_items.Text;
                // End
                // Louis Added on 20201124
                string req_prno = txtprno.Text;
                // End
                DisplayMyPR(com,usn,sts,req_dpt, nm, dt,sec,req_item, req_prno);

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
    }
}