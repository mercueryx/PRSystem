using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERP_System.PRD_Module.PRD_Control;
using System.Data;

namespace ERP_System.PRD_Module.Forms
{
    public partial class Create_Packing_List : System.Web.UI.Page
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
            form = "CREATE_PACKING_LIST";

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
                        DisplayLocation(com);
                        DisplayBrandCode(com);
                        CreateDisplayTable();

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

        #region ResetText

        private void ClearHeaderText()
        {
            ddlmstr.Items.Clear();
            ddlmstr.DataSource = null;
            ddlmstr.DataBind();
            txtver.Value = "";
            txtrefno.Value = "";
            txtef_date.Text = "";

        }

        private void ClearDetailsText()
        {
            ddlcatalog.Items.Clear();
            ddlcatalog.DataSource = null;
            ddlcatalog.DataBind();

            ViewState["dtdetails"] = null;
            dgvheader.DataSource = null;
            dgvheader.DataBind();
        }

        #endregion

        #region Display Data

        private void DisplayBrandCode(string com)
        {
            try
            {
                dtoresult = Process.DisplayBrandCode(com);
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

        private void DisplayLocation(string com)
        {
            try
            {
                dtoresult = Process.DisplayDefaultLocation(com);
                if (dtoresult.dtloc.Rows.Count > 0)
                {
                    ddlloc.DataSource = dtoresult.dtloc;
                    ddlloc.DataTextField = "description";
                    ddlloc.DataValueField = "loc";
                    ddlloc.DataBind();
                    ddlloc.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlloc.SelectedIndex = 0;
                }
                else
                {
                    ddlloc.Items.Clear();
                    ddlloc.DataSource = null;
                    ddlloc.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayMasterCatalog(string com, string brand)
        {
            try
            {
                dtoresult = Process.DisplayMasterCatalog_ByBrand(com, brand);
                if (dtoresult.dtmaster.Rows.Count > 0)
                {
                    ddlmstr.DataSource = dtoresult.dtmaster;
                    ddlmstr.DataTextField = "description";
                    ddlmstr.DataValueField = "catalog_no";
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

        private void DisplayCatalog(string com, string loc)
        {
            try
            {
                dtoresult = Process.DisplayCatalogNo_ByLoc(com, loc);
                if (dtoresult.dtcatalog.Rows.Count > 0)
                {
                    ddlcatalog.DataSource = dtoresult.dtcatalog;
                    ddlcatalog.DataTextField = "description";
                    ddlcatalog.DataValueField = "catalog_no";
                    ddlcatalog.DataBind();
                    ddlcatalog.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlcatalog.SelectedIndex = 0;
                }
                else
                {
                    ddlcatalog.Items.Clear();
                    ddlcatalog.DataSource = null;
                    ddlcatalog.DataBind();
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

        private void CreateDisplayTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[4] { new DataColumn("loc"), new DataColumn("catalog_no"), new DataColumn("dsc"), new DataColumn("qty") });
                ViewState["dtdisplay"] = dt;
                //rptr1.DataSource = dt;
                //rptr1.DataBind();
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }

        }

        //private void CreateDetailsTable()
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        dt.Columns.AddRange(new DataColumn[4] { new DataColumn("loc"), new DataColumn("catalog_no"), new DataColumn("dsc"), new DataColumn("qty") });
        //        ViewState["dtdetails"] = dt;
        //        //rptr1.DataSource = dt;
        //        //rptr1.DataBind();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        #endregion

        protected void ddlbrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string brand;
                brand = ddlbrand.SelectedItem.Value;
                DisplayMasterCatalog(com, brand);
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void ddlloc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string loc;
                loc = ddlloc.SelectedItem.Value;
                DisplayCatalog(com, loc);
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtdisplay = (DataTable)ViewState["dtdisplay"];
                string loc, ctlno, dsc,qty;
                loc = ddlloc.SelectedItem.Value;
                if (ddlcatalog.Items.Count == 0)
                {
                    ctlno = "";
                }
                else
                {
                    ctlno = ddlcatalog.SelectedItem.Value;
                }
              
                dsc = "";
                qty = txtqty.Text;
                // check empty data

                if (string.IsNullOrEmpty(loc))
                {
                    DisplayFailResult("Default location cannot empty.");
                    return;
                }

                if (string.IsNullOrEmpty(ctlno))
                {
                    DisplayFailResult("Catalog number cannot empty.");
                    return;
                }

                //if (object.ReferenceEquals(ddlcatalog, null))
                //{
                //    DisplayFailResult("Catalog no cannot empty.");
                //    return;
                //}

                //check duplicate item
                dtoresult = Process.CheckDuplicateDetails(dtdisplay, loc, ctlno);
                if (dtoresult.sts == false)
                {
                    DisplayFailResult(dtoresult.message);
                    return;
                }
            
                //get catalog description 
                dtoresult = Process.GetCatalogDescription(com, loc, ctlno);
                if (dtoresult.dtdesc.Rows.Count > 0)
                {
                    dsc = dtoresult.dtdesc.Rows[0]["dsc"].ToString();
                }

                //add data to temporary table and display into gridview
                dtdisplay.Rows.Add(loc,ctlno,dsc, qty);
                ViewState["dtdisplay"] = dtdisplay;
                dgvheader.DataSource = dtdisplay;
                dgvheader.DataBind();
                txtqty.Text = "0.00";

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

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtdisplay = (DataTable)ViewState["dtdisplay"];
               
                string brand, mstr_ctlno, refno, ef_date,ver;
                //qty,loc,ctlno,dsc,


                if (ddlbrand.Items.Count == 0)
                {
                    brand = "";                  
                }
                else
                {
                    brand = ddlbrand.SelectedItem.Value;
                }


                if (ddlmstr.Items.Count == 0)
                {                  
                    mstr_ctlno = "";                 
                }
                else
                {
                    mstr_ctlno = ddlmstr.SelectedItem.Value;
                }

               
              
                refno = txtrefno.Value;
                ef_date = txtef_date.Text;
                ver = txtver.Value;             

                dtoresult = Process.InsertNewPackingList(dtdisplay, com, brand, mstr_ctlno, ef_date, refno, usn,ver);
                if (dtoresult.sts == true)
                {
                    txtver.Value = dtoresult.version;
                    DisplayPassResult(dtoresult.message);
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

        protected void dgvheader_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dtdisplay = ViewState["dtdisplay"] as DataTable;
                dtdisplay.Rows[index].Delete();
                ViewState["dtdisplay"] = dtdisplay;
                dgvheader.DataSource = dtdisplay;
                dgvheader.DataBind();

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void txtqty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string qty = txtqty.Text;
                decimal checkdecimal=0, dec_qty=0;
                int checkint=0;
               // dec_qty = 0;

                if (!string.IsNullOrEmpty(qty))
                {

                    if (int.TryParse(qty, out checkint))
                    {
                        dec_qty = decimal.Parse(qty);
                        txtqty.Text = dec_qty.ToString("F2");
                    }
                    else
                    {
                        // The string was a valid integer => use result here
                        if (decimal.TryParse(qty, out checkdecimal))
                        {
                            dec_qty = decimal.Parse(qty);
                            txtqty.Text = dec_qty.ToString("F2");
                        }
                        else
                        {
                            txtqty.Text = "0.00";
                            //txtqty.Text = dec_qty.ToString("F2");
                            return;
                        }
                    }


                }
                else
                {
                    // invalid integer
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Only allow numeric.')</script>");
                    txtqty.Text = "0.00";
                }


            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void btnnew_Click(object sender, EventArgs e)
        {
            ClearDetailsText();
            ClearHeaderText();
            CreateDisplayTable();

        }
    }
}