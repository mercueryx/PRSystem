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
    public partial class PR_APPROVAL : System.Web.UI.Page
    {

        PR_dto dtoresult = new PR_dto();
        PR_bo Process = new PR_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com, usn, form, dpt,name;

        protected void Page_Load(object sender, EventArgs e)
        {
            usn = (string)Session["usn"];
            com = (string)Session["com"];
            dpt = (string)Session["dpt"];
            name = (string)Session["nm"];
            form = "PR_APPROVAL";

            if (!IsPostBack)
            {
                //check permission
                CheckPermission(usn, form);
            }
        }
        // 
        #region Form Permission

        private void CheckPermission(string usn, string form)
        {
            try
            {
                string requestor, dt;

                requestor = txtnm.Text;
                dt = txtreq_date.Text;
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
                        DisplayDepartment(com);
                        // Louis Added on 20200818
                        string dept = ddlr_dpt.SelectedItem.Value;
                        // End
                        DisplayCertifiedPR(usn,dpt,requestor,dt, dept);
                        CreateItemApproveTable();
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

        private void CreateItemApproveTable()
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

        private void DisplayCertifiedPR(string id ,string dpt,string requestor,string dt, string dept)
        {
            try
            {

                dtoresult = Process.DisplayPR_byUser_Permission(id,dpt,requestor,dt, dept);

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
        // End

        //private void RefreshGrid()
        //{
        //    try
        //    {
        //        string requestor, req_dt;
        //        requestor = txtnm.Text.ToUpper();
        //        req_dt = txtreq_date.Text;
        //        dtoresult = Process.SearchPendingPR_Keyword(dpt, requestor, req_dt);
        //        if (dtoresult.dtitem.Rows.Count > 0)
        //        {
        //            dgvheader.DataSource = dtoresult.dtitem;
        //            dgvheader.DataBind();
        //        }
        //        else
        //        {
        //            dgvheader.DataSource = null;
        //            dgvheader.DataBind();

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        DisplayFailResult(ex.ToString());
        //    }
        //}

        #endregion


        protected void dgvheader_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvheader.PageIndex = e.NewPageIndex;
            string requestor, dt;

            requestor = txtnm.Text;
            dt = txtreq_date.Text;
            // Louis Added on 20200818
            string dept = ddlr_dpt.SelectedItem.Value;
            // End
            DisplayCertifiedPR(usn,dpt,requestor, dt, dept);
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string  requestor, dt;
               
                requestor = txtnm.Text;
                dt = txtreq_date.Text;
                // Louis Added on 20200818
                string dept = ddlr_dpt.SelectedItem.Value;
                // End
                DisplayCertifiedPR(usn,dpt, requestor, dt, dept);
                txtnm.Text = "";
                txtreq_date.Text = "";


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
                    if (sts == "PENDING" || sts == "SELECT")
                    {
                        for (int i = 0; i < dgvheader.Rows.Count; i++)
                        {

                            ((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).SelectedIndex = ((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).Items.IndexOf(((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).Items.FindByText("SELECT"));
                        }
                    }
                    else if (sts == "APPROVED")
                    {
                        for (int i = 0; i < dgvheader.Rows.Count; i++)
                        {

                            ((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).SelectedIndex = ((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).Items.IndexOf(((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).Items.FindByText("APPROVE"));
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
                string sts, id, rn,requestor,dt;
                rn = "";
                requestor = txtnm.Text;
                dt = txtreq_date.Text;
                // Louis Added on 20200818
                string dept = ddlr_dpt.SelectedItem.Value;
                // End

                DataTable dtdtl = (DataTable)ViewState["dtitem"];

                foreach (GridViewRow row in dgvheader.Rows)
                {
                    DropDownList ddlsts = row.FindControl("ddlsts") as DropDownList;
                    sts = ddlsts.SelectedItem.Value;
                    id = row.Cells[1].Text;
                    rn = row.Cells[2].Text;

                    if (sts != "SELECT")
                    {
                        dtoresult = Process.CheckCertifiedPR_Status(rn, id);
                        if (dtoresult.sts == false)
                        {
                            DisplayFailResult("PR status updated by other user. PR will be refresh.");
                            DisplayCertifiedPR(usn,dpt,requestor,dt,dept);

                            return;
                        }


                    }
                    else
                    {
                        sts = "CERTIFIED";

                    }

                    dtdtl.Rows.Add(rn, id, sts);


                }
                dtoresult = Process.Approve_Cancel_PR(dtdtl, usn,name);
                if (dtoresult.sts == true)
                {
                    DisplayCertifiedPR(usn,dpt,requestor,dt, dept);
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

        protected void resulttimer_Tick(object sender, EventArgs e)
        {
            lblsaveresult.Text = "";
            resulttimer.Enabled = false;
        }
    }
}