using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERP_System.PR_Module.PR_Control;
namespace ERP_System.PR_Module.Forms
{
    public partial class PR_Approval_History : System.Web.UI.Page
    {
        PR_dto dtoresult = new PR_dto();
        PR_bo Process = new PR_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com, usn, form, dpt,sec;
        string requestor, req_dpt, dt, sts;
        protected void Page_Load(object sender, EventArgs e)
        {
            usn = (string)Session["usn"];
            com = (string)Session["com"];
            dpt = (string)Session["dpt"];
            sec = (string)Session["sec"];
            form = "PR_APPROVAL_HISTORY";

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
               

                requestor = txtnm.Text;
                dt = txtreq_date.Text;
                req_dpt = txtreq_dpt.Text;
                sts = ddlsts.SelectedValue;

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
                        DisplayMyPR(usn, sts, req_dpt, requestor, dt);
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

        private void DisplayMyPR(string usn, string sts, string req_dpt, string requestor, string dt)
        {
            try
            {

                dtoresult = Process.ViewApprovePR(usn, requestor, dt,sec,req_dpt,sts);
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

        private void DisplayFailResult(string message)
        {
            resulttimer.Enabled = true;
            lblsaveresult.Text = message;
            //lblresult.Text = message;
            lblsaveresult.ForeColor = System.Drawing.Color.Red;
            // lblresult.ForeColor = System.Drawing.Color.Red;
        }

        protected void dgvheader_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvheader.PageIndex = e.NewPageIndex;
            requestor = txtnm.Text;
            dt = txtreq_date.Text;
            req_dpt = txtreq_dpt.Text;
            sts = ddlsts.SelectedValue;
            DisplayMyPR(usn, sts, req_dpt, requestor, dt);

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

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {

                string req_dpt, nm, dt, sts;
                req_dpt = txtreq_dpt.Text;
                nm = txtnm.Text;
                dt = txtreq_date.Text;
                sts = ddlsts.SelectedValue;
                DisplayMyPR(usn, sts, req_dpt, nm, dt);

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