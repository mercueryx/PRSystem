using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERP_System.INV_Module.INV_Control;
using System.Data;
namespace ERP_System.INV_Module.Forms
{
    public partial class Stock_Transfer_Requisition : System.Web.UI.Page
    {
        INV_dto dtoresult = new INV_dto();
        INV_bo Process = new INV_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com,usn,form;

        protected void Page_Load(object sender, EventArgs e)
        {
            usn = (string)Session["usn"];
            com = (string)Session["com"];
            form = "STOCK_TRANSFER";
       
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
                        Response.Redirect("/MainPage.aspx", false);
                    }
                    else
                    {
                        DisplayLocation(com);
                        CreateDetailsTable();
                        CreateSaveTable();
                        CreateTransactionTable();
                    }

                }
                else
                {
                    Response.Redirect("/index.aspx", false);
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        #endregion 

        #region Display Data

        private void DisplayLocation(string com)
        {
            try
            {
                dtoresult = Process.DisplayLocation(com);
                if (dtoresult.dtloc_from.Rows.Count > 0)
                {
                    ddlfrom.DataSource = dtoresult.dtloc_from;
                    ddlfrom.DataTextField = "dsc";
                    ddlfrom.DataValueField = "loc";
                    ddlfrom.DataBind();
                    ddlfrom.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlfrom.SelectedIndex = 0;
                }
                else
                {
                    ddlfrom.Items.Clear();
                    ddlfrom.DataSource = null;
                    ddlfrom.DataBind();

                }

                if (dtoresult.dtloc_to.Rows.Count > 0)
                {
                    ddlto.DataSource = dtoresult.dtloc_to;
                    ddlto.DataTextField = "dsc";
                    ddlto.DataValueField = "loc";
                    ddlto.DataBind();
                    ddlto.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlto.SelectedIndex = 0;
                }
                else
                {
                    ddlto.Items.Clear();
                    ddlto.DataSource = null;
                    ddlto.DataBind();

                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayCatalogNo(string com, string loc)
        {
            try
            {
                dtoresult = Process.DisplayCatalogNo(com, loc);
                if (dtoresult.dtctlno.Rows.Count > 0)
                {
                    ddlctlno.DataSource = dtoresult.dtctlno;
                    ddlctlno.DataTextField = "dsc";
                    ddlctlno.DataValueField = "catalog_no";
                    ddlctlno.DataBind();
                    ddlctlno.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlctlno.SelectedIndex = 0;
                }
                else
                {
                    ddlctlno.Items.Clear();
                    ddlctlno.DataSource = null;
                    ddlctlno.DataBind();
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

        #endregion

        #region Create Tem Table

        private void CreateDetailsTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[4] { new DataColumn("catalog_no"), new DataColumn("dsc"), new DataColumn("uom"), new DataColumn("qty") });
                ViewState["dtdetails"] = dt;
                //rptr1.DataSource = dt;
                //rptr1.DataBind();
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }

        }

        private void CreateSaveTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[6] { new DataColumn("catalog_no"), new DataColumn("dsc"), new DataColumn("uom") ,new DataColumn("qty"), new DataColumn("reqqty"), new DataColumn("refno") });
                ViewState["dtsave"] = dt;
                //rptr1.DataSource = dt;
                //rptr1.DataBind();
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void CreateTransactionTable()
        {
            try
            {
                DataTable dt = new DataTable();              
                dt.Columns.AddRange(new DataColumn[7] { new DataColumn("catalog_no"), new DataColumn("ex_dsc"), new DataColumn("dsc"), new DataColumn("qty"), new DataColumn("uom"), new DataColumn("cur"), new DataColumn("refno") });
                ViewState["dttrans"] = dt;
                //rptr1.DataSource = dt;
                //rptr1.DataBind();
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        #endregion

        #region ClearData
        private void ClearTransferRequisitionData()
        {
            txtrmk.Value = "";
            txttranx.Value = "";
            txtreqdate.Text = "";
            DisplayLocation(com);       
            ddlctlno.Items.Clear();
            ddlctlno.DataSource = null;
            ddlctlno.DataBind();
            CreateDetailsTable();
            CreateSaveTable();
            CreateTransactionTable();
            dgvheader.DataSource = null;
            dgvheader.DataBind();        
        }
        #endregion

        protected void ddlfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                string loc;
                loc = ddlfrom.SelectedItem.Value;
                DisplayCatalogNo(com,loc);
                CreateDetailsTable();                      
                dgvheader.DataSource = null;
                dgvheader.DataBind();            

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void ddlctlno_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
              
                DataTable dtdetails = (DataTable)ViewState["dtdetails"];


                string ctlno,c_catalog,loc_from,dsc,uom,o_qty;
              
                dsc = "";
                //tranx_no = "";
                ctlno = ddlctlno.SelectedItem.Value;
                loc_from = ddlfrom.SelectedItem.Value;

                if (string.IsNullOrEmpty(ctlno))
                {
                    DisplayFailResult("Please select catalog number first.");
                    return;
                }
                if (string.IsNullOrEmpty(loc_from))
                {
                    DisplayFailResult("Please select from location first.");
                    return;
                }


                foreach (GridViewRow row in dgvheader.Rows)
                {

                    c_catalog = row.Cells[1].Text;
                    if (ctlno == c_catalog)
                    {
                        DisplayFailResult("This " + ctlno + " already existed.");
                        return;
                    }
                }

                //display inventory details
                dtoresult = Process.DisplayInvDtl(com, loc_from, ctlno);
                if (dtoresult.dtinv_dtl.Rows.Count > 0)
                {
                    ctlno = dtoresult.dtinv_dtl.Rows[0]["catalog_no"].ToString();
                    dsc = dtoresult.dtinv_dtl.Rows[0]["dsc"].ToString();
                    uom = dtoresult.dtinv_dtl.Rows[0]["uom"].ToString();
                    //tranx_no = dtoresult.dtinv_dtl.Rows[0]["tranx_no"].ToString();
                    o_qty = dtoresult.dtinv_dtl.Rows[0]["qty"].ToString();
                 
                    dtdetails.Rows.Add(ctlno, dsc, uom,o_qty);
                    ViewState["dtdetails"] = dtdetails;
                    dgvheader.DataSource = dtdetails;
                    dgvheader.DataBind();
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

        protected void dgvheader_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //delete header 
                string ctlno;
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dtdetails = ViewState["dtdetails"] as DataTable;
                ctlno = dtdetails.Rows[index]["catalog_no"].ToString();
                dtdetails.Rows[index].Delete();
                ViewState["dtdetails"] = dtdetails;
                dgvheader.DataSource = dtdetails;
                dgvheader.DataBind();            
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                ClearTransferRequisitionData();
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
                string tranx_no,reqqty, ctl_no, desc, uom, o_qty, request_date, from_loc, rmk, to_loc, refno;
               
                tranx_no = txttranx.Value;
                //save grn details gridview data in temporary table
                DataTable dtsave = (DataTable)ViewState["dtsave"];
                DataTable dttrans = (DataTable)ViewState["dttrans"];
                foreach (GridViewRow row in dgvheader.Rows)
                {
                    //Finding textbox control  
                    TextBox txtreqqty = row.FindControl("txtreqqty") as TextBox;
                    TextBox txtrefno = row.FindControl("txtref") as TextBox;
                    reqqty = txtreqqty.Text;
                    refno = txtrefno.Text;
                    ctl_no = row.Cells[1].Text;
                    desc = row.Cells[2].Text;
                    uom = row.Cells[3].Text;
                    o_qty = row.Cells[5].Text;
                    //check receive qty
                    dtoresult = Process.CheckReceiveQty(reqqty, o_qty);
                    if (dtoresult.sts == false)
                    {
                        DisplayFailResult(dtoresult.Message);
                        CreateSaveTable();
                        return;
                    }
                    if (reqqty == "0.00")
                    {
                        DisplayFailResult("Cannot transfer 0 qty.");
                        CreateSaveTable();
                        return;
                    }
                    dtsave.Rows.Add(ctl_no, desc, uom, reqqty,o_qty,refno);
                }

                if (object.ReferenceEquals(ddlfrom, null))
                {
                    DisplayFailResult("From Location cannot empty.");
                    return;
                }
                else
                {
                    from_loc= ddlfrom.SelectedItem.Value;
                }

                if (object.ReferenceEquals(ddlto, null))
                {
                    DisplayFailResult("To Location cannot empty.");
                    return;
                }
                else
                {                    
                    to_loc = ddlto.SelectedItem.Value;
                }
                request_date = txtreqdate.Text;             
                rmk = txtrmk.Value;
             
                //insert all data
                dtoresult = Process.InsertData(tranx_no,dtsave, from_loc,to_loc,rmk,request_date,com,usn,dttrans);
                if (dtoresult.sts == true)
                {
                    txttranx.Value = dtoresult.tranx_no;
                    DisplayPassResult("Transfer requisition successful.");                 
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
    }
}