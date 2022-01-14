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
    public partial class Packing_Entry : System.Web.UI.Page
    {
        PRD_dto dtoresult = new PRD_dto();
        PRD_bo Process = new PRD_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com,usn,form;

        protected void Page_Load(object sender, EventArgs e)
        {
            form = "PACKING_ENTRY";
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

                        DisplayBrandCode(com);
                        DisplayCustCode(com);

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

        private void DisplayMasterCatalog(string com, string brand)
        {
            try
            {
                if (object.ReferenceEquals(ddlbrand, null))
                {
                    brand = "";
                }
                dtoresult = Process.DisplayPkgList_MasterCatalog(com, brand);
                if (dtoresult.dtmaster.Rows.Count > 0)
                {
                    ddlmstr.DataSource = dtoresult.dtmaster;
                    ddlmstr.DataTextField = "description";
                    ddlmstr.DataValueField = "pl_rn";
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

        private void DisplayRefNo()
        {
            try
            {
                string rn;
                if (object.ReferenceEquals(ddlmstr, null))
                {
                    rn = "";
                }
                else
                {
                    rn = ddlmstr.SelectedItem.Value;
                }
                dtoresult = Process.DisplayRefNo(rn);
                if (dtoresult.dtdesc.Rows.Count > 0)
                {
                    txtref.Value = dtoresult.dtdesc.Rows[0]["refno"].ToString();
                }
                else
                {
                    txtref.Value = "";
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
                dtoresult = Process.DisplayCustCode(com);
                if (dtoresult.dtcust.Rows.Count > 0)
                {
                    ddlcust.DataSource = dtoresult.dtcust;
                    ddlcust.DataTextField = "description";
                    ddlcust.DataValueField = "ah_code";
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

        private void DisplayPackingDetails(string rn, decimal qty)
        {
            try
            {
                dtoresult = Process.DisplayPackingListDetails(rn, qty);
                if (dtoresult.dtpkgdetails.Rows.Count > 0)
                {
                    dgvheader.DataSource = dtoresult.dtpkgdetails;
                    dgvheader.DataBind();
                    ViewState["dtdetails"] = dtoresult.dtpkgdetails;
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

        #endregion

        #region Reset Data
        private void ResetText()
        {
          
            DisplayCustCode(com);
            ddlmstr.Items.Clear();
            ddlmstr.DataSource = null;
            ddlmstr.DataBind();
            txtref.Value = "";
            txtrmk.Value = "";
            txttrans_date.Text = "";
            txtqty.Text = "0.00";
            dgvheader.DataSource = null;
            dgvheader.DataBind();
            //reset temporary table

        }

        #endregion
    

        protected void txtqty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string rn;
                string qty = txtqty.Text;
                decimal checkdecimal=0, dec_qty=0;
                int checkint=0;
                //dec_qty = 0;

                if (ddlmstr.Items.Count ==0)
                {
                    rn = "";
                }
                else
                {
                    rn = ddlmstr.SelectedItem.Value;
                }

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
                            dec_qty = decimal.Parse(txtqty.Text);

                            //txtqty.Text = dec_qty.ToString("F2");
                            return;
                        }
                    }
                }
                else
                {
                    // invalid integer                
                    dec_qty = decimal.Parse(txtqty.Text);
                    txtqty.Text = "0.00";
                    
                }
                DisplayPackingDetails(rn, dec_qty);


            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void ddlbrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
                string brand; 
                brand = ddlbrand.SelectedItem.Value;
                ResetText();
                //display master catalog
                DisplayMasterCatalog(com, brand);    
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void ddlmstr_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string rn;
                decimal qty;
                rn = ddlmstr.SelectedItem.Value;
                qty = decimal.Parse(txtqty.Text);
                //display refno
                DisplayRefNo();

                //display packing details
                DisplayPackingDetails(rn, qty);
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
                DataTable dtdetails = (DataTable)ViewState["dtdetails"];

                string brand, refno, rn, rmk, trans_date, cust_code, qty;
                

                if (ddlbrand.Items.Count ==0)
                {
                    DisplayFailResult("Brand code cannot empty");
                    return;
                }
              
                if (ddlmstr.Items.Count ==0)
                {
                    DisplayFailResult("Master catalog cannot empty");
                    return;
                }

                if (ddlcust.Items.Count ==0)
                {
                    DisplayFailResult("Customer Code cannot empty");
                    return;
                }

                brand = ddlbrand.SelectedItem.Value;
                rn = ddlmstr.SelectedItem.Value;
                cust_code = ddlcust.SelectedItem.Value;           
                refno = txtref.Value;
                qty = txtqty.Text;
                rmk = txtrmk.Value;
                trans_date = txttrans_date.Text;
                dtoresult = Process.NewPackingEntry(dtdetails, com, brand, refno, rn, rmk, trans_date, cust_code, qty, usn);
                if (dtoresult.sts == true)
                {
                    DisplayPassResult("Packing entry successfull.");
                    DisplayBrandCode(com);

                    ResetText();
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