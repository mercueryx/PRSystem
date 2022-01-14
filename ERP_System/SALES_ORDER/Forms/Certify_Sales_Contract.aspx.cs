using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ERP_System.SALES_ORDER.SALES_ORDER_Control;

namespace ERP_System.SALES_ORDER.Forms
{
    public partial class Certify_Sales_Contract : System.Web.UI.Page
    {
        SO_dto dtoresult = new SO_dto();
        SO_bo Process = new SO_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com, usn, form;

        protected void Page_Load(object sender, EventArgs e)
        {
            form = "GRN_APPROVAL";
            usn = (string)Session["usn"];
            com = (string)Session["com"];

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
                        //DisplayVendor();
                        //CreateGRNTable();
                        CreateHeaderTable();
                        DisplaySCNo(com);
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

        #region DisplayDate

  

        private void DisplayBillTo(string type)
        {
            try
            {
                dtoresult = Process.DisplaySoldto_Billto(type);
                if (dtoresult.dtBillTo.Rows.Count > 0)
                {
                    ddlbillto.DataSource = dtoresult.dtBillTo;
                    ddlbillto.DataTextField = "dsc";
                    ddlbillto.DataValueField = "ah_code";
                    ddlbillto.DataBind();
                    ddlbillto.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlbillto.SelectedIndex = 0;
                }
                else
                {
                    ddlbillto.Items.Clear();
                    ddlbillto.DataSource = null;
                    ddlbillto.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplaySoldTo(string type)
        {
            try
            {
                dtoresult = Process.DisplaySoldto_Billto(type);
                if (dtoresult.dtSoldTo.Rows.Count > 0)
                {
                    ddlsold.DataSource = dtoresult.dtSoldTo;
                    ddlsold.DataTextField = "dsc";
                    ddlsold.DataValueField = "ah_code";
                    ddlsold.DataBind();
                    ddlsold.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlsold.SelectedIndex = 0;
                }
                else
                {
                    ddlsold.Items.Clear();
                    ddlsold.DataSource = null;
                    ddlsold.DataBind();
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

        private void DisplaySCDetails(string sc_no)
        {
            try
            {
                dtoresult = Process.DisplaySCDetails(sc_no);
                if (dtoresult.dtscd.Rows.Count > 0)
                {
                    dgvscd.DataSource = dtoresult.dtscd;
                    dgvscd.DataBind();
                    //reset po details

                }
                else
                {
                    dgvscd.DataSource = null;
                    dgvscd.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplaySCDeliveryDetails(string sc_no, string dtl_no)
        {
            try
            {
                dtoresult = Process.DisplaySCDeliveryDetails(sc_no,dtl_no);
                if (dtoresult.dtscdd.Rows.Count > 0)
                {
                    dgvscdd.DataSource = dtoresult.dtscdd;
                    dgvscdd.DataBind();
                    //reset po details

                }
                else
                {
                    dgvscdd.DataSource = null;
                    dgvscdd.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplaySCNo(string com)
        {
            try
            {
                dtoresult = Process.DisplayOpenSC(com);
                if (dtoresult.dtrn.Rows.Count > 0)
                {
                    ddlsc_no.DataSource = dtoresult.dtrn;
                    ddlsc_no.DataTextField = "sc_no";
                    ddlsc_no.DataValueField = "sc_no";
                    ddlsc_no.DataBind();
                    ddlsc_no.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlsc_no.SelectedIndex = 0;
                }
                else
                {
                    ddlsc_no.Items.Clear();
                    ddlsc_no.DataSource = null;
                    ddlsc_no.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }


        private void RefreshGrid()
        {
            try
            {
                string type, billto, soldto, sc_no, sc_date;

                if (ddltype.Items.Count == 0)
                {
                    type = "";
                }
                else
                {
                    type = ddltype.SelectedItem.Value;
                }

                if (ddlbillto.Items.Count == 0)
                {
                    billto = "";
                }
                else
                {
                    billto = ddlbillto.SelectedItem.Value;
                }

                if (ddlsold.Items.Count == 0)
                {
                    soldto = "";
                }
                else
                {
                    soldto = ddlsold.SelectedItem.Value;
                }

                sc_date = txtscdate.Text;

                if (ddlsc_no.Items.Count == 0)
                {
                    sc_no = "";
                }
                else
                {
                    sc_no = ddlsc_no.SelectedItem.Value;
                }

                dtoresult = Process.DisplaySCHeader(com, type, billto, soldto, sc_no, sc_date);
                if (dtoresult.dtheader.Rows.Count > 0)
                {
                    ClearDataGridView();
                    dgvheader.DataSource = dtoresult.dtheader;
                    dgvheader.DataBind();

                }
                else
                {
                    ClearDataGridView();
                    DisplayFailResult("No result found.");

                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }
        #endregion

        #region Reset Data

        private void ClearDataGridView()
        {

            dgvheader.DataSource=null;
            dgvheader.DataBind();

            dgvscd.DataSource = null;
            dgvscd.DataBind();

            dgvscdd.DataSource = null;
            dgvscdd.DataBind();

        }

        #endregion

        #region Create Tem Table

        private void CreateHeaderTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("sc_no"), new DataColumn("sts") });
                ViewState["dtheader"] = dt;

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
                RefreshGrid();
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

        protected void dgvscd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sc_no,dtl_no;

                GridViewRow row = dgvscd.SelectedRow;
                Label lblsc_no = row.FindControl("lblsc_no") as Label;
                Label lblid = row.FindControl("lblid") as Label;
                sc_no = lblsc_no.Text;
                dtl_no = lblid.Text;
                //po_no = row.Cells[4].Text;
                //po_no = (row.FindControl("lblpo_no") as Label).Text;
                DisplaySCDeliveryDetails(sc_no, dtl_no);

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
                string sc_no;

                GridViewRow row = dgvheader.SelectedRow;
                sc_no = row.Cells[2].Text;
                //po_no = row.Cells[4].Text;
                //po_no = (row.FindControl("lblpo_no") as Label).Text;
                DisplaySCDetails(sc_no);

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
                string sts, sc_no;
                sc_no = "";
                DataTable dtheader = (DataTable)ViewState["dtheader"];
                //DataTable dtdetails = (DataTable)ViewState["dtdetails"];
                foreach (GridViewRow row in dgvheader.Rows)
                {
                    //Finding Dropdown control  
                    DropDownList ddlsts = row.FindControl("ddlsts") as DropDownList;
                    sts = ddlsts.SelectedItem.Value;
                    sc_no = row.Cells[2].Text;
                    if (sts != "OPEN")
                    {
                        if (sts == "CERTIFY")
                        {
                            //check exist approved sc
                            dtoresult = Process.CheckExistCertifySCNo(sc_no);
                            if (dtoresult.sts == true)
                            {
                                dtheader.Rows.Add(sc_no, sts);
                            }
                        }
                        else
                        {
                            dtheader.Rows.Add(sc_no, sts);
                        }

                    }

                }
                dtoresult = Process.UpdateSCCertify(dtheader, usn);
                if (dtoresult.sts == true)
                {
                    DisplayPassResult("Sales contract updated.");
                    RefreshGrid();

                    CreateHeaderTable();
                }
                else
                {
                    DisplayFailResult("No data updated.");
                    ClearDataGridView();
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
                        ((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).SelectedIndex = ((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).Items.IndexOf(((DropDownList)dgvheader.Rows[i].FindControl("ddlsts")).Items.FindByText("CERTIFY"));
                    }
                }

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string type;
                //com = ddlcom.SelectedItem.Value;


                if (ddltype.Items.Count == 0)
                {
                    type = "";
                }
                else
                {
                    type = ddltype.SelectedItem.Value;
                }

                DisplayBillTo(type);
                DisplaySoldTo(type);

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

    }
}