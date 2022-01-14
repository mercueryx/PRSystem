using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ERP_System.PR_Module.PR_Control;
namespace ERP_System.PR_Module.Forms
{
    public partial class PR_Certify : System.Web.UI.Page
    {
        PR_dto dtoresult = new PR_dto();
        PR_bo Process = new PR_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com, usn, form,dpt,sec,name;

        protected void Page_Load(object sender, EventArgs e)
        {
            usn = (string)Session["usn"];
            com = (string)Session["com"];
            dpt = (string)Session["dpt"];
            sec = (string)Session["sec"];
            name = (string)Session["nm"];
            form = "PR_CERTIFY";

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

                        //DisplayBrandCode(com);
                        //DisplayMasterCatalog(com);
                        //DisplayCustCode(com);
                        //CreateHeaderTable();
                        //CreateDetailsTable();
                        DisplayPendingPR(usn,sec);
                        CreateItemCertifyTable();
                        DisplayDepartment(com);
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

        #region Create Temporary Table 

        private void CreateItemCertifyTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("rn"), new DataColumn("id"), new DataColumn("sts") });
                ViewState["dtitem"] = dt;

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        #endregion

        #region DisplayData

        private void DisplayPendingPR(string usn,string sec)
        {
            try
            {
                dtoresult = Process.DisplayPendingCertifyPR_All(usn,sec);
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

        private void DisplayPassResult(string message)
        {
            resulttimer.Enabled = true;
            lblsaveresult.Text = message;
            // lblresult.Text = message;
            lblsaveresult.ForeColor = System.Drawing.Color.Blue;
            // lblresult.ForeColor = System.Drawing.Color.Blue;
        }

        private void RefreshGrid()
        {
            try
            {
                string requestor, req_dt;
                requestor = txtnm.Text.ToUpper();
                req_dt = txtreq_date.Text;
                // Louis Added on 20200818
                string dept = ddlr_dpt.SelectedItem.Value;
                // End
                dtoresult = Process.SearchPendingPR_Keyword(usn, requestor, req_dt);
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
                ddlall.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }
        }

        // Louis Added on 20200818
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
                    ddlr_dpt.Items.Insert(0, new ListItem("Select Department", "Select Department"));
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
        // End

        #endregion

        protected void dgvheader_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvheader.PageIndex = e.NewPageIndex;
            DisplayPendingPR(usn, sec);
        }

        protected void btnall_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvheader.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvheader.Rows.Count; i++)
                    {
                        ((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).SelectedIndex = ((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).Items.IndexOf(((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).Items.FindByText("CERTIFIED"));
                    }
                }

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void ddlall_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string sts;
                sts = ddlall.SelectedValue.ToString();
                if (dgvheader.Rows.Count > 0)
                {
                    if (sts == "PENDING" || sts =="SELECT")
                    {
                        for (int i = 0; i < dgvheader.Rows.Count; i++)
                        {

                            ((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).SelectedIndex = ((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).Items.IndexOf(((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).Items.FindByText("SELECT"));
                        }
                    }
                    else if (sts == "CERTIFIED")
                    {
                        for (int i = 0; i < dgvheader.Rows.Count; i++)
                        {

                            ((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).SelectedIndex = ((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).Items.IndexOf(((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).Items.FindByText("CERTIFY"));
                        }
                    }
                    else if (sts == "REJECTED")
                    {
                        for (int i = 0; i < dgvheader.Rows.Count; i++)
                        {

                            ((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).SelectedIndex = ((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).Items.IndexOf(((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).Items.FindByText("REJECT"));
                        }
                    }
                }

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
                string sts,id, rn;
                rn = "";

             
                DataTable dtdtl = (DataTable)ViewState["dtitem"];

                foreach (GridViewRow row in dgvheader.Rows)
                {
                    DropDownList ddlsts = row.FindControl("ddlsts") as DropDownList;
                    sts = ddlsts.SelectedItem.Value;
                    id = row.Cells[1].Text;
                    rn = row.Cells[2].Text;

                    if (sts != "SELECT")
                    {
                        dtoresult = Process.CheckPR_Status(rn, id);
                        if (dtoresult.sts == false)
                        {
                            DisplayFailResult("PR status updated by other user. PR will be refresh.");
                            DisplayPendingPR(dpt,sec);

                            return;
                        }

                     
                    }
                    else
                    {
                        sts = "OPEN";

                    }

                    dtdtl.Rows.Add(rn, id, sts);


                }
                dtoresult = Process.Certify_Cancel_PR(dtdtl,usn,name);
                if (dtoresult.sts == true)
                {
                    RefreshGrid();
                    DisplayPassResult("PR status updated.");
               
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

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshGrid();
                txtnm.Text = "";
                txtreq_date.Text = "";
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