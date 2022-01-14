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
    public partial class PO_Edit : System.Web.UI.Page
    {

        PO_dto dtoresult = new PO_dto();
        PO_bo Process = new PO_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com, usn, form;

        protected void Page_Load(object sender, EventArgs e)
        {
          
            usn = (string)Session["usn"];
            com = (string)Session["com"];
            form = "PO_EDIT";

            if (!IsPostBack)
            {
                txtsearch_date.Attributes.Add("autocomplete", "off");
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
                        DisplayCompanyCode(com);
                        DisplayUOM();              
                        DisplayCatalog();
                        DisplayVendorType(com);
                        DisplayVendorCode(com);
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

        private void DisplayVendorCode(string com)
        {
            try
            {
                dtoresult = Process.DisplayAllVenCode(com);
                if (dtoresult.dtVen_Code.Rows.Count > 0)
                {
                    ddlven_code.DataSource = dtoresult.dtVen_Code;
                    ddlven_code.DataTextField = "description";
                    ddlven_code.DataValueField = "vendor_code";
                    ddlven_code.DataBind();
                    ddlven_code.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlven_code.SelectedIndex = 0;
                }
                else
                {
                    ddlven_code.Items.Clear();
                    ddlven_code.DataSource = null;
                    ddlven_code.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayCompanyCode(string com)
        {
            try
            {
                dtoresult = Process.DisplayCompanyCode_Vendor(com);
                if (dtoresult.dtCompanyCode.Rows.Count > 0)
                {
                    //ddlcom.DataSource = dtoresult.dtCompanyCode;
                    //ddlcom.DataTextField = "company_code";
                    //ddlcom.DataValueField = "company_code";
                    //ddlcom.DataBind();
                    //ddlcom.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    //ddlcom.SelectedIndex = 0;
                    txtcom.Value = dtoresult.dtCompanyCode.Rows[0]["company_code"].ToString();
                }
                else
                {
                    //ddlcom.Items.Clear();
                    //ddlcom.DataSource = null;
                    //ddlcom.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }

        }      

        private void DisplayUOM()
        {
            try
            {
                dtoresult = Process.DisplayUOM();
                if (dtoresult.dtuom.Rows.Count > 0)
                {
                    ddluom.DataSource = dtoresult.dtuom;
                    ddluom.DataTextField = "purchase_um";
                    ddluom.DataValueField = "purchase_um";
                    ddluom.DataBind();
                    ddluom.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddluom.SelectedIndex = 0;
                }
                else
                {
                    ddluom.Items.Clear();
                    ddluom.DataSource = null;
                    ddluom.DataBind();

                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayCatalog()
        {
            try
            {
                dtoresult = Process.DisplayCatalogNo();
                if (dtoresult.dtcatalog.Rows.Count > 0)
                {
                    ddlcatalog.DataSource = dtoresult.dtcatalog;
                    ddlcatalog.DataTextField = "catalog_desc";
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

        private void DisplayVendorType(string com)
        {
            try
            {
                dtoresult = Process.DisplayVendor_Type(com);
                if (dtoresult.dtVen_Type.Rows.Count > 0)
                {
                    ddlven_type.DataSource = dtoresult.dtVen_Type;
                    ddlven_type.DataTextField = "description";
                    ddlven_type.DataValueField = "type_code";
                    ddlven_type.DataBind();
                    ddlven_type.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlven_type.SelectedIndex = 0;
                }
                else
                {
                    ddlven_type.Items.Clear();
                    ddlven_type.DataSource = null;
                    ddlven_type.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayVendorCode(string type, string com)
        {
            try
            {
                dtoresult = Process.DisplayVendor_Code(type, com);
                if (dtoresult.dtVen_Code.Rows.Count > 0)
                {
                    ddlven_code.DataSource = dtoresult.dtVen_Code;
                    ddlven_code.DataTextField = "description";
                    ddlven_code.DataValueField = "vendor_code";
                    ddlven_code.DataBind();
                    ddlven_code.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlven_code.SelectedIndex = 0;
                }
                else
                {
                    ddlven_code.Items.Clear();
                    ddlven_code.DataSource = null;
                    ddlven_code.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayCatalogInfo(string catalogno)
        {
            try
            {
                dtoresult = Process.DisplayCatalogInfo_ctlno(catalogno);
                if (dtoresult.dtcataloginfo.Rows.Count > 0)
                {
                    txtdesc.Value = dtoresult.dtcataloginfo.Rows[0]["dsc"].ToString();
                    txtextra_dsc.Value = dtoresult.dtcataloginfo.Rows[0]["ex_dsc"].ToString();

                }
                dtoresult = Process.DisplayUnitSellPrice(catalogno);
                if (dtoresult.dtprice.Rows.Count > 0)
                {
                    txtprice.Text = dtoresult.dtprice.Rows[0]["unit_price"].ToString();
                    decimal total = Convert.ToDecimal(txtqty.Text) * Convert.ToDecimal(txtprice.Text);
                    txt_total.Text = Math.Round(Convert.ToDecimal(total), 2).ToString();
                }

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayOpenPO(string orderdate)
        {
            try
            {
                dtoresult = Process.DisplayOpenPO_byOrderDate(com,orderdate);
                if (dtoresult.dtPO.Rows.Count > 0)
                {
                    ddls_pono.DataSource = dtoresult.dtPO;
                    ddls_pono.DataTextField = "po_no";
                    ddls_pono.DataValueField = "po_no";
                    ddls_pono.DataBind();
                    ddls_pono.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddls_pono.SelectedIndex = 0;
                }
                else
                {
                    ddls_pono.Items.Clear();
                    ddls_pono.DataSource = null;
                    ddls_pono.DataBind();

                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DiplayPOInfo(string po_no)
        {
            try
            {
                dtoresult = Process.DisplaySelectedPOHdr(po_no);
                if (dtoresult.dtpo_hdr.Rows.Count > 0)
                {
                    txtpo.Value = dtoresult.dtpo_hdr.Rows[0]["po_no"].ToString();
                    txtrev.Value = dtoresult.dtpo_hdr.Rows[0]["rev_no"].ToString();
                    //ddlcom.SelectedIndex = ddlcom.Items.IndexOf(ddlcom.Items.FindByText(dtoresult.dtpo_hdr.Rows[0]["com"].ToString()));
                    ddlven_type.SelectedIndex = ddlven_type.Items.IndexOf(ddlven_type.Items.FindByValue(dtoresult.dtpo_hdr.Rows[0]["ven_type"].ToString()));
                    ddlven_code.SelectedIndex = ddlven_code.Items.IndexOf(ddlven_code.Items.FindByValue(dtoresult.dtpo_hdr.Rows[0]["ven_code"].ToString()));
                    DisplayVendorInfo(com, ddlven_code.SelectedItem.Value);
                    DisplayPurchase_Term(com, ddlven_code.SelectedItem.Value);
                    ddlterm_code.SelectedIndex = ddlterm_code.Items.IndexOf(ddlterm_code.Items.FindByValue(dtoresult.dtpo_hdr.Rows[0]["term_code"].ToString()));
                    ddlpur_term.SelectedIndex = ddlpur_term.Items.IndexOf(ddlpur_term.Items.FindByValue(dtoresult.dtpo_hdr.Rows[0]["pur_term"].ToString()));
                    txtven_contact.Value = dtoresult.dtpo_hdr.Rows[0]["ven_contact"].ToString();
                    txtadddate.Text = dtoresult.dtpo_hdr.Rows[0]["order_date"].ToString();
                    ddlplace.SelectedIndex = ddlplace.Items.IndexOf(ddlplace.Items.FindByValue(dtoresult.dtpo_hdr.Rows[0]["destination"].ToString()));
                    txtrmk.Value = dtoresult.dtpo_hdr.Rows[0]["rmk"].ToString();

                    DisplayPODetails(po_no);

                }
             
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayPODetails(string po_no)
        {
            try
            {
                dtoresult = Process.DisplayOpenPo_Details(po_no);
                if (dtoresult.dtPO_Details.Rows.Count > 0)
                {
                    dgvitems.DataSource = dtoresult.dtPO_Details;
                    dgvitems.DataBind();

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

        private void DisplayPurchase_Term(string com, string ven_code)
        {
            try
            {
                dtoresult = Process.DisplayPur_Term(com, ven_code);
                if (dtoresult.dtPur_Term.Rows.Count > 0)
                {
                    ddlpur_term.DataSource = dtoresult.dtPur_Term;
                    ddlpur_term.DataTextField = "pur_desc";
                    ddlpur_term.DataValueField = "pur_code";
                    ddlpur_term.DataBind();
                    ddlpur_term.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlpur_term.SelectedIndex = 0;

                }
                else
                {
                    ddlpur_term.Items.Clear();
                    ddlpur_term.DataSource = null;
                    ddlpur_term.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayVendorInfo(string com, string vencode)
        {
            try
            {
                dtoresult = Process.DisplayVendor_Info(vencode, com);
                if (dtoresult.dtVen_Info.Rows.Count > 0)
                {
                    ddlterm_code.DataSource = dtoresult.dtVen_Info;
                    ddlterm_code.DataTextField = "term_desc";
                    ddlterm_code.DataValueField = "term_code";
                    ddlterm_code.DataBind();
                    ddlterm_code.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlterm_code.SelectedIndex = 0;
                    txtven_contact.Value = dtoresult.dtVen_Info.Rows[0]["contact_person1"].ToString();

                }
                else
                {
                    ddlterm_code.Items.Clear();
                    ddlterm_code.DataSource = null;
                    ddlterm_code.DataBind();
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

        #region ResetText

        private void ClearText()
        {
            txtdesc.Value = "";
            // txtcatalog.Text = "";
            txtextra_dsc.Value = "";
            txtqty.Text = "0.00";
            txtprice.Text = "0.00";
            txt_total.Text = "0.00";
            ddlcatalog.SelectedIndex = 0;
            ddluom.SelectedIndex = 0;
            //ddlcatalog.DataSource = null;
        }

        private void ClearHeader()
        {
            try
            {
                txtrev.Value = "";
                txtpo.Value = "";
                ddlven_type.Items.Clear();
                ddlven_type.DataSource = null;
                ddlven_type.DataBind();
                ddlven_code.Items.Clear();
                ddlven_code.DataSource = null;
                ddlven_code.DataBind();
                txtadddate.Text = "";
                txtven_contact.Value = "";
                ddlterm_code.Items.Clear();
                ddlterm_code.DataSource = null;
                ddlterm_code.DataBind();
                ddlpur_term.Items.Clear();
                ddlpur_term.DataSource = null;
                ddlpur_term.DataBind();
                txtrmk.Value = "";
          
                dgvitems.DataSource = null;
                dgvitems.DataBind();
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

      

        #endregion

        protected void ddlcatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string catalogno = ddlcatalog.SelectedItem.Value;
                DisplayCatalogInfo(catalogno);
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
                //dec_qty = 0;

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
                            txtqty.Text = dec_qty.ToString("F2");
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

        protected void txtprice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string price = txtprice.Text;
                decimal checkdecimal=0, dec_price=0;
                int checkint=0;
                //dec_price = 0;

                if (!string.IsNullOrEmpty(price))
                {

                    if (int.TryParse(price, out checkint))
                    {
                        dec_price = decimal.Parse(price);
                        txtprice.Text = dec_price.ToString("F2");
                    }
                    else
                    {
                        // The string was a valid integer => use result here
                        if (decimal.TryParse(price, out checkdecimal))
                        {
                            dec_price = decimal.Parse(price);
                            txtprice.Text = dec_price.ToString("F2");
                        }
                        else
                        {
                            txtprice.Text = dec_price.ToString("F2");
                            return;
                        }
                    }


                }
                else
                {
                    // invalid integer
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Only allow numeric.')</script>");
                    txtprice.Text = "0.00";
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

        protected void txtsearch_date_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string order_date;
                order_date = txtsearch_date.Text;
                DisplayOpenPO(order_date);

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void ddlven_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string ven_code;
                ven_code = ddlven_code.SelectedItem.Value;
                DisplayVendorInfo(com, ven_code);
                DisplayPurchase_Term(com, ven_code);
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void dgvitems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string id, po_no;
         
                id =dgvitems.DataKeys[e.RowIndex].Values[0].ToString();
                //ctlno = e.Keys["catalog_no"].ToString();
                po_no = txtpo.Value;
                dtoresult = Process.DeletePODetails(po_no, id);
                if (dtoresult.sts == true)
                {
                    DisplayPODetails(po_no);

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

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                string  ven_type, ven_code, ven_name, order_dt, ven_contact, term_code, pur_term, place, rmk, po;


                if (ddlven_type.Items.Count == 0)
                {
                    ven_type = "";
                }
                else
                {
                    ven_type = ddlven_type.SelectedItem.Value;
                }

                if (ddlven_code.Items.Count == 0)
                {
                    ven_code = "";
                    ven_name = "";
                }
                else
                {
                    ven_code = ddlven_code.SelectedItem.Value;
                    ven_name = ddlven_code.SelectedItem.Text;
                    ven_name = ven_name.Split('|')[1];
                }

                if (ddlterm_code.Items.Count ==0)
                {
                    term_code = "";

                }
                else
                {
                    term_code = ddlterm_code.SelectedItem.Value;
                }

                if (ddlpur_term.Items.Count ==0)
                {
                    pur_term = "";
                }
                else
                {
                    pur_term = ddlpur_term.SelectedItem.Value;
                }

                if (object.ReferenceEquals(ddlplace, null))
                {
                    place = "";
                }
                else
                {
                    place = ddlplace.SelectedItem.Value;
                }
               
                
               
                order_dt = txtadddate.Text;
                ven_contact = txtven_contact.Value;
             
             
                rmk = txtrmk.Value;
                po = txtpo.Value;
                dtoresult = Process.UpdatePOHeader(com,po, ven_type, ven_code, ven_name, order_dt, ven_contact, term_code, pur_term, place, rmk, usn);
                if (dtoresult.sts == true)
                {                  
                    DisplayPassResult("PO save successful.");
                   
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

        protected void btncancel_Click(object sender, EventArgs e)
        {
            ClearText();
            ClearHeader();
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                string po_no,catalog_no, desc, ex_desc, qty, uom, price, total;
                po_no = txtpo.Value;
                catalog_no = ddlcatalog.SelectedItem.Value;
                desc = txtdesc.Value.Trim();
                ex_desc = txtextra_dsc.Value.Trim();
                qty = txtqty.Text;
                uom = ddluom.SelectedItem.Value;
                price = txtprice.Text;
                total = txt_total.Text;

                dtoresult = Process.AddNewPODetails(po_no, catalog_no, desc, ex_desc, uom, qty, price, total, usn);
                if (dtoresult.sts == true)
                {
                    DisplayPassResult("Add items succesfull.");
                    DisplayPODetails(po_no);
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

        protected void btnclear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearText();
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void ddls_pono_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string po_no;
                po_no = ddls_pono.SelectedItem.Value;
                DiplayPOInfo(po_no);

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void ddlven_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string ven_type, com;
                ven_type = ddlven_type.SelectedItem.Value;
                com =txtcom.Value;
                //Session.Add("ven_type", ven_type);
                DisplayVendorCode(ven_type, com);
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }    

      

         
    }
}