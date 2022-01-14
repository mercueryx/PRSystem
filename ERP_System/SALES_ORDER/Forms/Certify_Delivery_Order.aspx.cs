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
    public partial class Certify_Delivery_Order : System.Web.UI.Page
    {
        DO_dto dtoresult = new DO_dto();
        DO_bo Process = new DO_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com, usn, form;

        protected void Page_Load(object sender, EventArgs e)
        {
            usn = (string)Session["usn"];
            com = (string)Session["com"];
            form = "DO_ENTRY";

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

                        DisplayOrderType(com);
                        CreateHeaderTable();
                        DisplayOpenInv(com);
                        DisplayOpenDO(com);

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

        private void DisplayOpenDO(string com)
        {
            try
            {
                dtoresult = Process.DisplayOpenDO(com);
                if (dtoresult.dtdo_rn.Rows.Count > 0)
                {
                    ddldo.DataSource = dtoresult.dtdo_rn;
                    ddldo.DataTextField = "do_no";
                    ddldo.DataValueField = "do_no";
                    ddldo.DataBind();
                    ddldo.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddldo.SelectedIndex = 0;
                }
                else
                {
                    ddldo.Items.Clear();
                    ddldo.DataSource = null;
                    ddldo.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayOpenInv(string com)
        {
            try
            {
                dtoresult = Process.DisplayOpenInv(com);
                if (dtoresult.dtinv_rn.Rows.Count > 0)
                {
                    ddlinv.DataSource = dtoresult.dtinv_rn;
                    ddlinv.DataTextField = "inv_no";
                    ddlinv.DataValueField = "inv_no";
                    ddlinv.DataBind();
                    ddlinv.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlinv.SelectedIndex = 0;
                }
                else
                {
                    ddlinv.Items.Clear();
                    ddlinv.DataSource = null;
                    ddlinv.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayOrderType(string com)
        {
            try
            {
                dtoresult = Process.DisplayOrderType(com);
                if (dtoresult.dtgroup.Rows.Count > 0)
                {
                    ddltype.DataSource = dtoresult.dtgroup;
                    ddltype.DataTextField = "group_code";
                    ddltype.DataValueField = "group_code";
                    ddltype.DataBind();
                    ddltype.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddltype.SelectedIndex = 0;
                }
                else
                {
                    ddltype.Items.Clear();
                    ddltype.DataSource = null;
                    ddltype.DataBind();
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
                string type, shipto,do_no,inv_no, soldto, dt_from, dt_to;

                if (ddltype.Items.Count == 0)
                {
                    type = "";
                }
                else
                {
                    type = ddltype.SelectedItem.Value;
                }

                if (ddlship.Items.Count == 0)
                {
                    shipto = "";
                }
                else
                {
                    shipto = ddlship.SelectedItem.Value;
                }

                if (ddlsold.Items.Count == 0)
                {
                    soldto = "";
                }
                else
                {
                    soldto = ddlsold.SelectedItem.Value;
                }

                dt_from = txtfrom.Text;
                dt_to = txtto.Text;

                if (ddldo.Items.Count == 0)
                {
                    do_no = "";
                }
                else
                {
                    do_no = ddldo.SelectedItem.Value;
                }

                if (ddlinv.Items.Count == 0)
                {
                    inv_no = "";
                }
                else
                {
                    inv_no = ddlinv.SelectedItem.Value;
                }

                dtoresult = Process.DisplayDOHeader(com, do_no, inv_no, dt_from, dt_to, soldto, shipto,type);

                if (dtoresult.dthdr.Rows.Count > 0)
                {
                    ClearDataGridView();
                    dgvheader.DataSource = dtoresult.dthdr;
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

        private void DisplayDODtl(string com, string do_no)
        {
            try
            {
                dtoresult = Process.DisplayDODtl(com, do_no);
                if (dtoresult.dtdtl.Rows.Count > 0)
                {
                    dgvdtl.DataSource = dtoresult.dtdtl;
                    dgvdtl.DataBind();

                }
                else
                {
                    dgvdtl.DataSource =null;
                    dgvdtl.DataBind();

                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayDOFoc(string com, string do_no)
        {
            try
            {
                dtoresult = Process.DisplayDOFoc(com, do_no);
                if (dtoresult.dtfoc.Rows.Count > 0)
                {
                    dgvfoc.DataSource = dtoresult.dtfoc;
                    dgvfoc.DataBind();

                }
                else
                {
                    dgvfoc.DataSource = null;
                    dgvfoc.DataBind();

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

        private void DisplayCustomer(string type)
        {
            try
            {
                dtoresult = Process.DisplayCustomer(type);
                if (dtoresult.dtCus.Rows.Count > 0)
                {
                    ddlsold.DataSource = dtoresult.dtCus;
                    ddlsold.DataTextField = "dsc";
                    ddlsold.DataValueField = "ah_code";
                    ddlsold.DataBind();
                    ddlsold.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlsold.SelectedIndex = 0;        

                    ddlship.DataSource = dtoresult.dtCus;
                    ddlship.DataTextField = "dsc";
                    ddlship.DataValueField = "ah_code";
                    ddlship.DataBind();
                    ddlship.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlship.SelectedIndex = 0;
                }
                else
                {
                    ddlsold.Items.Clear();
                    ddlsold.DataSource = null;
                    ddlsold.DataBind();

                    ddlship.Items.Clear();
                    ddlship.DataSource = null;
                    ddlship.DataBind();
                }

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
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

        #region ClearData()

        private void ClearDataGridView()
        {

            dgvheader.DataSource = null;
            dgvheader.DataBind();

            dgvdtl.DataSource = null;
            dgvdtl.DataBind();

            dgvfoc.DataSource = null;
            dgvfoc.DataBind();

        }

        #endregion

        #region Create Tem Table

        private void CreateHeaderTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("do_no"), new DataColumn("sts") });
                ViewState["dtheader"] = dt;

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }

        }

        #endregion

        protected void dgvheader_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string do_no;

                GridViewRow row = dgvheader.SelectedRow;
                do_no = row.Cells[3].Text;
             
                DisplayDODtl(com,do_no);
                DisplayDOFoc(com, do_no);

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
                catch (Exception)
                {

                    throw;
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
                string sts, do_no;
                do_no = "";
                DataTable dtheader = (DataTable)ViewState["dtheader"];
                //DataTable dtdetails = (DataTable)ViewState["dtdetails"];
                foreach (GridViewRow row in dgvheader.Rows)
                {
                    //Finding Dropdown control  
                    DropDownList ddlsts = row.FindControl("ddlsts") as DropDownList;
                    sts = ddlsts.SelectedItem.Value;
                    do_no = row.Cells[3].Text;
                    if (sts != "OPEN")
                    {
                        if (sts == "CERTIFY")
                        {
                            //check exist certified do
                            dtoresult = Process.CheckExistCertifyDONo(do_no);
                            if (dtoresult.sts == true)
                            {
                                dtheader.Rows.Add(do_no, sts);
                            }
                        }
                        else
                        {
                            dtheader.Rows.Add(do_no, sts);
                        }

                    }

                }
                
                dtoresult = Process.UpdateDOCertify(dtheader, usn);
                if (dtoresult.sts == true)
                {
                    DisplayPassResult("Delivery Order updated.");
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

        protected void resulttimer_Tick(object sender, EventArgs e)
        {
            lblsaveresult.Text = "";
            resulttimer.Enabled = false;
        }

        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string type;

                if (ddltype.Items.Count > 0)
                {
                    type = ddltype.SelectedItem.Value;
                }
                else
                {
                    type = "";

                }

                DisplayCustomer(type);
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
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

    }
}