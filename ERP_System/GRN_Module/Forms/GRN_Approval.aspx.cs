using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERP_System.GRN_Module.GRN_Control;
using System.Data;
namespace ERP_System.GRN_Module.Forms
{
    public partial class GRN_Approval : System.Web.UI.Page
    {
        GRN_dto dtoresult = new GRN_dto();
        GRN_bo Process = new GRN_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com,usn,form;
        protected void Page_Load(object sender, EventArgs e)
        {
            form = "GRN_APPROVAL";
            usn = (string)Session["usn"];
            com = (string)Session["com"];
       

            if (!IsPostBack)
            {
                //check permission
                CheckPermission(usn, form);
                //DisplayVendor();
                //CreateGRNTable();
                //CreateDetailsTable();
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
                        DisplayVendor();
                        CreateGRNTable();
                        CreateDetailsTable();
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

        #region Create Tem Table

        private void CreateGRNTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("grn_no"), new DataColumn("sts")});
                ViewState["dtgrn"] = dt;

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void CreateDetailsTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[4] { new DataColumn("grn_no"), new DataColumn("po_no"), new DataColumn("rec_qty"), new DataColumn("ctlno") });
                ViewState["dtdetails"] = dt;

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        #endregion

        #region Display Data

        private void DisplayVendor()
        {
            try
            {
                dtoresult = Process.DisplayOpenGRN_Vendor();
                if (dtoresult.dtven.Rows.Count > 0)
                {
                    ddlven_name.DataSource = dtoresult.dtven;
                    ddlven_name.DataTextField = "description";
                    ddlven_name.DataValueField = "vendor";
                    ddlven_name.DataBind();
                    ddlven_name.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlven_name.SelectedIndex = 0;
                }
                else
                {
                    ddlven_name.Items.Clear();                   
                    ddlven_name.DataSource = null;
                    ddlven_name.DataBind();
                }
            }
            catch (Exception )
            {

                throw;
            }
        }

        private void DisplayOpenGRN_Header(string grn_no, string from, string to, string vendor)
        {
            try
            {
                //filter open po data
                dtoresult = Process.DisplayOpenGRN(grn_no, from, to, vendor);
                if (dtoresult.dtgrn_hdr.Rows.Count > 0)
                {
                    dgvheader.DataSource = dtoresult.dtgrn_hdr;
                    dgvheader.DataBind();
                    //reset po details
                    dgvitems.DataSource = null;
                    dgvitems.DataBind();
                }
                else
                {
                    dgvheader.DataSource = null;
                    dgvheader.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void DisplayOpenGRN_Details(string grn_no)
        {
            try
            {
                dtoresult = Process.DisplayOpenGRN_Details(grn_no);
                if (dtoresult.dtgrn_dtl.Rows.Count > 0)
                {
                    dgvitems.DataSource = dtoresult.dtgrn_dtl;
                    dgvitems.DataBind();
                    //reset po details

                }
                else
                {
                    dgvitems.DataSource = null;
                    dgvitems.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
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
                string grn_no, from, to, ven_code;
                grn_no = txtfilter.Value;
                from = txtfrom.Text;
                to = txtto.Text;
                if (ddlven_name.Items.Count > 0)
                {
                    ven_code = ddlven_name.SelectedItem.Value;
                }
                else
                {
                    ven_code = "";
                }             

                DisplayOpenGRN_Header(grn_no, from, to, ven_code);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string grn_no, from, to, ven_code;
                grn_no = txtfilter.Value;
                from = txtfrom.Text;
                to = txtto.Text;

                if (ddlven_name.Items.Count == 0)
                {
                    ven_code = "";
                }
                else
                {
                    ven_code = ddlven_name.SelectedItem.Value;
                }
             
              

                DisplayOpenGRN_Header(grn_no, from, to, ven_code);

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void dgvheader_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string grn_no;

                GridViewRow row = dgvheader.SelectedRow;
                grn_no = row.Cells[3].Text;
                //po_no = row.Cells[4].Text;
                //po_no = (row.FindControl("lblpo_no") as Label).Text;
                DisplayOpenGRN_Details(grn_no);

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
                string sts, grn_no;
                grn_no = "";
              
               
                DataTable dtgrn = (DataTable)ViewState["dtgrn"];
                DataTable dtdetails = (DataTable)ViewState["dtdetails"];
                foreach (GridViewRow row in dgvheader.Rows)
                {
                    //Finding Dropdown control  
                    DropDownList ddlsts = row.FindControl("ddlsts") as DropDownList;
                    sts = ddlsts.SelectedItem.Value;
                    grn_no = row.Cells[3].Text;
                    if (sts != "OPEN")
                    {
                        if (sts == "APPROVE")
                        {
                            //check exist approved grn
                            dtoresult = Process.CheckExistApproveGRN(grn_no);
                            if (dtoresult.sts == true)
                            {
                                dtgrn.Rows.Add(grn_no, sts);
                            }
                        }
                        else
                        {
                           dtgrn.Rows.Add(grn_no, sts);
                        }
                     
                    }
                   
                }
                dtoresult = Process.UpdateGRNApproval(dtgrn,dtdetails,usn);
                if (dtoresult.sts == true)
                {
                    DisplayPassResult("GRN updated.");
                    RefreshGrid();
                    DisplayVendor();
                    CreateGRNTable();
                    CreateDetailsTable();
                }
                else
                {
                    DisplayFailResult(dtoresult.Message);
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void btnall_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvheader.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvheader.Rows.Count; i++)
                    {
                        ((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).SelectedIndex = ((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).Items.IndexOf(((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).Items.FindByText("APPROVE"));
                    }
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