using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERP_System.PO_Module.PO_Control;
using System.Data;
namespace ERP_System.PO_Module.Forms
{
    public partial class PO_Approval : System.Web.UI.Page
    {
        PO_dto dtoresult = new PO_dto();
        PO_bo Process = new PO_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com, usn,form;
        protected void Page_Load(object sender, EventArgs e)
        {
            usn = (string)Session["usn"];
            com = (string)Session["com"];
            form = "PO_APPROVAL";
           
            if (!IsPostBack)
            {
                //check permission
                CheckPermission(usn, form);
                //DisplayVendor();
                //CreateItemTable();

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
                        CreateItemTable();
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

        private void CreateItemTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("po"), new DataColumn("sts") });
                ViewState["dtpo"] = dt;
              
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
                dtoresult = Process.DisplayOpenPO_Vendor();
                if (dtoresult.dtVen_Code.Rows.Count > 0)
                {
                    ddlven_name.DataSource = dtoresult.dtVen_Code;
                    ddlven_name.DataTextField = "description";
                    ddlven_name.DataValueField = "ven_code";
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
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayOpenPO_Header(string po,string from,string to,string ven_code)
        {
            try
            {
                //filter open po data
                dtoresult = Process.DisplayOpenPO(po, from, to, ven_code);
                if (dtoresult.dtPO.Rows.Count > 0)
                {
                    dgvheader.DataSource = dtoresult.dtPO;
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
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayOpenPO_Details(string po)
        {
            try
            {
                dtoresult = Process.DisplayOpenPo_Details(po);
                if (dtoresult.dtPO_Details.Rows.Count > 0)
                {
                    dgvitems.DataSource = dtoresult.dtPO_Details;
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
            lblsaveresult.Text = message;
            // lblresult.Text = message;
            lblsaveresult.ForeColor = System.Drawing.Color.Blue;
            // lblresult.ForeColor = System.Drawing.Color.Blue;
        }

        private void RefreshGrid()
        {
            try
            {
                string po, from, to, ven_code;
                po = txtfilter.Value;
                from = txtfrom.Text;
                to = txtto.Text;
                if (object.ReferenceEquals(ddlven_name, null))
                {
                    ven_code = "";
                }
                else
                {
                    ven_code = ddlven_name.SelectedItem.Value;
                }

                DisplayOpenPO_Header(po, from, to, ven_code);
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
                string po, from, to, ven_code;
                po = txtfilter.Value;
                from = txtfrom.Text;
                to = txtto.Text;
                if (ddlven_name.Items.Count ==0)
                {
                    ven_code = "";
                }
                else
                {
                    ven_code = ddlven_name.SelectedItem.Value;
                }

                DisplayOpenPO_Header(po, from, to, ven_code);

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
                string po_no;
              
                GridViewRow row = dgvheader.SelectedRow;
                po_no= row.Cells[3].Text;
                //po_no = (row.FindControl("lblpo_no") as Label).Text;
                DisplayOpenPO_Details(po_no);
               
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
                string sts,po;
                po = "";
                DataTable dt = (DataTable)ViewState["dtpo"];
                foreach (GridViewRow row in dgvheader.Rows)
                {
                    //Finding Dropdown control  
                    DropDownList ddlsts = row.FindControl("ddlsts") as DropDownList;                  
                        sts = ddlsts.SelectedItem.Value;
                        po = row.Cells[3].Text;

                    if (sts != "OPEN")
                    {
                        dt.Rows.Add(po, sts);
                    }
                                 
                }
                dtoresult = Process.UpdatePOApproval(dt);
                if (dtoresult.sts == true)
                {
                    DisplayPassResult("PO updated.");
                    RefreshGrid();
                    DisplayVendor();
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