using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ERP_System.PRD_Module.PRD_Control;
namespace ERP_System.PRD_Module.Forms
{
    public partial class Certify_Packing_Entry : System.Web.UI.Page
    {
        PRD_dto dtoresult = new PRD_dto();
        PRD_bo Process = new PRD_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com,usn,form;

        protected void Page_Load(object sender, EventArgs e)
        {
            usn = (string)Session["usn"];
            com = (string)Session["com"];
            form = "PACKING_CERTIFY";
        
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

                        DisplayBrandCode(com);
                        DisplayMasterCatalog(com);
                        DisplayCustCode(com);
                        CreateHeaderTable();
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

        #region Display Data

        private void DisplayBrandCode(string com)
        {
            try
            {
                dtoresult = Process.DisplayPackingBrandCode(com);
                if (dtoresult.dtbrandcode.Rows.Count > 0)
                {

                    ddlbrand.DataSource = dtoresult.dtbrandcode;
                    ddlbrand.DataTextField = "description";
                    ddlbrand.DataValueField = "brand_code";
                    ddlbrand.DataBind();
                    ddlbrand.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlbrand.SelectedIndex = 0;
                }
                else
                {
                    ddlbrand.Items.Clear();
                    ddlbrand.DataSource = null;
                    ddlbrand.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayMasterCatalog(string com)
        {
            try
            {
               
                dtoresult = Process.DisplayPackingMaster(com);
                if (dtoresult.dtmaster.Rows.Count > 0)
                {
                    ddlmstr.DataSource = dtoresult.dtmaster;
                    ddlmstr.DataTextField = "description";
                    ddlmstr.DataValueField = "mstr_catalog";
                    ddlmstr.DataBind();
                    ddlmstr.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlmstr.SelectedIndex = 0;
                }
                else
                {
                    ddlmstr.Items.Clear();
                    ddlmstr.DataSource = null;
                    ddlmstr.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayCustCode(string com)
        {
            try
            {
                dtoresult = Process.DisplayPackingCustCode(com);
                if (dtoresult.dtcust.Rows.Count > 0)
                {
                    ddlcust.DataSource = dtoresult.dtcust;
                    ddlcust.DataTextField = "description";
                    ddlcust.DataValueField = "cust_code";
                    ddlcust.DataBind();
                    ddlcust.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlcust.SelectedIndex = 0;
                }
                else
                {
                    ddlcust.Items.Clear();
                    ddlcust.DataSource = null;
                    ddlcust.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayPackingInfo(string com,string brand,string mstr,string cust,string trans_date)
        {
            try
            {
                dtoresult = Process.DisplaySearchPackingInfo(com, brand, mstr, cust, trans_date);
                if (dtoresult.dtpkginfo.Rows.Count > 0)
                {
                    dgvheader.DataSource = dtoresult.dtpkginfo;
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

        private void DisplayPackingDetails(string com,string pkg_no)
        {
            try
            {
                dtoresult = Process.DisplayPackingDetails(com,pkg_no);
                if (dtoresult.dtpkgdetails.Rows.Count > 0)
                {
                    dgvitems.DataSource = dtoresult.dtpkgdetails;
                    dgvitems.DataBind();
                    //reset po details

                }
                else
                {
                    dgvitems.DataSource = null;
                    dgvitems.DataBind();
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
                string brand, mstr, cust, trans_date;

                if (object.ReferenceEquals(ddlbrand, null))
                {
                    brand = "";
                }
                else
                {
                    brand = ddlbrand.SelectedItem.Value;
                }

                if (object.ReferenceEquals(ddlmstr, null))
                {
                    mstr = "";
                }
                else
                {
                    mstr = ddlmstr.SelectedItem.Value;
                }

                if (object.ReferenceEquals(ddlcust, null))
                {
                    cust = "";
                }
                else
                {
                    cust = ddlcust.SelectedItem.Value;
                }

                trans_date = txttrans_date.Text;

                DisplayPackingInfo(com, brand, mstr, cust, trans_date);


            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        #endregion

        #region Create Temporary Table

        private void CreateHeaderTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("rn"), new DataColumn("sts"), new DataColumn("trans_date") });
                ViewState["dtheader"] = dt;

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
                dt.Columns.AddRange(new DataColumn[6] { new DataColumn("rn"),new DataColumn("ctlno"), new DataColumn("dsc"), new DataColumn("loc"), new DataColumn("qty"), new DataColumn("trans_date") });
                ViewState["dtdetails"] = dt;

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
                string brand, mstr, cust, trans_date;

                if (ddlbrand.Items.Count ==0)
                {
                    brand = "";
                }
                else
                {
                    brand = ddlbrand.SelectedItem.Value;
                }

                if (ddlmstr.Items.Count ==0)
                {
                    mstr = "";
                }
                else
                {
                    mstr = ddlmstr.SelectedItem.Value;
                }

                if (ddlcust.Items.Count ==0)
                {
                    cust = "";
                }
                else
                {
                    cust = ddlcust.SelectedItem.Value;
                }

                trans_date = txttrans_date.Text;

                DisplayPackingInfo(com, brand, mstr, cust, trans_date);


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
                        ((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).SelectedIndex = ((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).Items.IndexOf(((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).Items.FindByText("CERTIFY"));
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
                string sts, rn,trans_date;
                rn = "";
               
                DataTable dthdr = (DataTable)ViewState["dtheader"];
                DataTable dtdtl = (DataTable)ViewState["dtdetails"];
    
                foreach (GridViewRow row in dgvheader.Rows)
                {
                    DropDownList ddlsts = row.FindControl("ddlsts") as DropDownList;
                    sts = ddlsts.SelectedItem.Value;
                    rn = row.Cells[4].Text;
                    trans_date = row.Cells[6].Text;
                    if (sts != "OPEN")
                    {
                        dtoresult = Process.CheckExistCertifyPkg(rn);
                        if (dtoresult.sts == true)
                        {
                            dthdr.Rows.Add(rn, sts,trans_date);
                        }                  
                    }
                }
                dtoresult = Process.CertifyPacking(dthdr, dtdtl,com, usn);
                if (dtoresult.sts == true)
                {
                    DisplayPassResult("Packing status updated.");
                    RefreshGrid();
                    DisplayBrandCode(com);
                    DisplayMasterCatalog(com);
                    DisplayCustCode(com);
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

        protected void dgvheader_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string pkg_no;
                GridViewRow row = dgvheader.SelectedRow;
                pkg_no = row.Cells[4].Text;
                //po_no = (row.FindControl("lblpo_no") as Label).Text;
                DisplayPackingDetails(com, pkg_no);

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