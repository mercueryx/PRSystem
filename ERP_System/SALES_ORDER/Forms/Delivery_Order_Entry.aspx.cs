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
    public partial class Delivery_Order_Entry : System.Web.UI.Page
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
                        //DisplayCompanyCode();
                        DisplayOrderType(com);
                        DisplayDeliveryTerm();
                        DisplayCertifySC(com);
                        DisplayUOM();
                        DisplayFOC_Catalog(com);
                        CreateDO_dtl();
                        CreateDO_foc_dtl();
                        //DisplayGroupCode(com);
                        //DisplayCatalogNo(com);


                        //ddlbillto.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        //ddlbillto.SelectedIndex = 0;
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

        private void DisplayDeliveryTerm()
        {
            try
            {
                dtoresult = Process.DisplayDeliveryTerm();
                if (dtoresult.dtdterm.Rows.Count > 0)
                {
                    ddldelivery_term.DataSource = dtoresult.dtdterm;
                    ddldelivery_term.DataTextField = "dsc";
                    ddldelivery_term.DataValueField = "delivery_term_code";
                    ddldelivery_term.DataBind();
                    ddldelivery_term.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddldelivery_term.SelectedIndex = 0;
                }
                else
                {
                    ddldelivery_term.Items.Clear();
                    ddldelivery_term.DataSource = null;
                    ddldelivery_term.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayCertifySC(string com)
        {
            try
            {
                dtoresult = Process.DisplayCertifySC(com);
                if (dtoresult.dtsc_no.Rows.Count > 0)
                {
                    ddlscno.DataSource = dtoresult.dtsc_no;
                    ddlscno.DataTextField = "sc_no";
                    ddlscno.DataValueField = "sc_no";
                    ddlscno.DataBind();
                    ddlscno.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlscno.SelectedIndex = 0;
                }
                else
                {
                    ddlscno.Items.Clear();
                    ddlscno.DataSource = null;
                    ddlscno.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayCatalog(string com,string sc_no)
        {
            try
            {
                dtoresult = Process.DisplaySC_Catalog(com,sc_no);
                if (dtoresult.dtctlno.Rows.Count > 0)
                {
                    ddlcatalog.DataSource = dtoresult.dtctlno;
                    ddlcatalog.DataTextField = "dsc";
                    ddlcatalog.DataValueField = "ctlno";
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

        private void DisplayCatalog_dsc(string sc_no, string ctlno)
        {
            try
            {
                dtoresult = Process.DisplayCatalog_dsc(sc_no, ctlno);
                if (dtoresult.dtdsc.Rows.Count > 0)
                {
                    txtdesc.Value =dtoresult.dtdsc.Rows[0]["dsc"].ToString();

                }
                else
                {
                    txtdesc.Value = "";
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

                    ddlf_uom.DataSource = dtoresult.dtuom;
                    ddlf_uom.DataTextField = "purchase_um";
                    ddlf_uom.DataValueField = "purchase_um";
                    ddlf_uom.DataBind();
                    ddlf_uom.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlf_uom.SelectedIndex = 0;
                }
                else
                {
                    ddluom.Items.Clear();
                    ddluom.DataSource = null;
                    ddluom.DataBind();

                    ddlf_uom.Items.Clear();
                    ddlf_uom.DataSource = null;
                    ddlf_uom.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayFOC_Catalog(string com)
        {
            try
            {
                dtoresult = Process.DisplayFOC_Catalog(com);
                if (dtoresult.dtf_ctlno.Rows.Count > 0)
                {
                    ddlf_catalog.DataSource = dtoresult.dtf_ctlno;
                    ddlf_catalog.DataTextField = "dsc";
                    ddlf_catalog.DataValueField = "catalog_no";
                    ddlf_catalog.DataBind();
                    ddlf_catalog.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlf_catalog.SelectedIndex = 0;
                }
                else
                {
                    ddlf_catalog.Items.Clear();
                    ddlf_catalog.DataSource = null;
                    ddlf_catalog.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayFOC_Catalog_dsc(string com, string ctlno)
        {
            try
            {
                dtoresult = Process.DisplayFOC_Catalog_dsc(com, ctlno);
                if (dtoresult.dtf_dsc.Rows.Count > 0)
                {
                    txtf_idsc.Text = dtoresult.dtf_dsc.Rows[0]["dsc"].ToString();
                }
                else
                {
                    txtf_idsc.Text = "";
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
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


                    ddlbillto.DataSource = dtoresult.dtCus;
                    ddlbillto.DataTextField = "dsc";
                    ddlbillto.DataValueField = "ah_code";
                    ddlbillto.DataBind();
                    ddlbillto.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlbillto.SelectedIndex = 0;


                    ddlshipto.DataSource = dtoresult.dtCus;
                    ddlshipto.DataTextField = "dsc";
                    ddlshipto.DataValueField = "ah_code";
                    ddlshipto.DataBind();
                    ddlshipto.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlshipto.SelectedIndex = 0;
                }
                else
                {
                    ddlsold.Items.Clear();
                    ddlsold.DataSource = null;
                    ddlsold.DataBind();

                    ddlbillto.Items.Clear();
                    ddlbillto.DataSource = null;
                    ddlbillto.DataBind();

                    ddlshipto.Items.Clear();
                    ddlshipto.DataSource = null;
                    ddlshipto.DataBind();
                }

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayPaymentTerm(string ah_code)
        {
            try
            {
                dtoresult = Process.DisplayPaymentTerm(ah_code);
                if (dtoresult.dtterm.Rows.Count > 0)
                {
                    ddlpay_term.DataSource = dtoresult.dtterm;
                    ddlpay_term.DataTextField = "dsc";
                    ddlpay_term.DataValueField = "term_code";
                    ddlpay_term.DataBind();
                    ddlpay_term.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlpay_term.SelectedIndex = 0;
                }
                else
                {
                    ddlpay_term.Items.Clear();
                    ddlpay_term.DataSource = null;
                    ddlpay_term.DataBind();
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

        private void DisplayFailResult2(string message)
        {
            resulttimer2.Enabled = true;
            lblsaveresult2.Text = message;
            //lblresult.Text = message;
            lblsaveresult2.ForeColor = System.Drawing.Color.Red;
            // lblresult.ForeColor = System.Drawing.Color.Red;
        }

        private void DisplayPassResult2(string message)
        {
            resulttimer2.Enabled = true;
            lblsaveresult2.Text = message;
            // lblresult.Text = message;
            lblsaveresult2.ForeColor = System.Drawing.Color.Blue;
            // lblresult.ForeColor = System.Drawing.Color.Blue;
        }

        #endregion

        #region Create Tem Table

        private void CreateDO_dtl()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[7] { new DataColumn("sc_no"), new DataColumn("ctlno"), new DataColumn("dsc"), new DataColumn("uom"), new DataColumn("qty"), new DataColumn("foc_qty"), new DataColumn("amount")});
                ViewState["dt_do_dtl"] = dt;
                //rptr1.DataSource = dt;
                //rptr1.DataBind();
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void CreateDO_foc_dtl()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[8] { new DataColumn("ctlno"), new DataColumn("i_dsc"), new DataColumn("uom"), new DataColumn("qty"), new DataColumn("claim"), new DataColumn("gift"), new DataColumn("dsc"), new DataColumn("rmk")});
                ViewState["dt_do_foc"] = dt;
                //rptr1.DataSource = dt;
                //rptr1.DataBind();
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }

        }

        #endregion

        #region Clear Text

        private void ClearDtlText()
        {
            txtadd_foc.Text = "0.00";
            txtd_qty.Text = "0.00";
            txtamount.Text = "0.00";
            txtdesc.Value = "";
            if (ddlcatalog.Items.Count > 0)
            {

                ddlcatalog.SelectedIndex = 0;
            }

            if (ddlscno.Items.Count > 0)
            {

                ddlscno.SelectedIndex = 0;
            }

            if (ddluom.Items.Count > 0)
            {

                ddluom.SelectedIndex = 0;
            }
        }

        private void ClearHdrText()
        {
            txteta.Text = "";
            txtetd.Text = "";
            txtd_date.Text = "";
            //txtdesc.Value = "";
            if (ddltype.Items.Count > 0)
            {

                ddltype.SelectedIndex = 0;
            }

            if (ddlbillto.Items.Count > 0)
            {

                ddlbillto.SelectedIndex = 0;
            }

            if (ddlsold.Items.Count > 0)
            {

                ddlsold.SelectedIndex = 0;
            }

            if (ddlshipto.Items.Count > 0)
            {

                ddlshipto.SelectedIndex = 0;
            }

            if (ddldelivery_term.Items.Count > 0)
            {

                ddldelivery_term.SelectedIndex = 0;
            }

            if (ddlpay_term.Items.Count > 0)
            {

                ddlpay_term.SelectedIndex = 0;
            }

        }

        private void ClearFocText()
        {
            txtf_idsc.Text = "";
            txtf_iqty.Text = "0.00";
            txtf_claim.Text = "";
            txtf_gift.Text = "";
            txtf_dsc.Text = "";
            txtrmk.Text = "";
            if (ddlf_catalog.Items.Count > 0)
            {

                ddlf_catalog.SelectedIndex = 0;
            }

            if (ddlf_uom.Items.Count > 0)
            {

                ddlf_uom.SelectedIndex = 0;
            }
        }

        private void ClearGridview()
        {
            dgvitems.DataSource = null;
            dgvitems.DataBind();
            dgv_items_info.DataSource = null;
            dgv_items_info.DataBind();
        }

        #endregion

        protected void resulttimer_Tick(object sender, EventArgs e)
        {
            lblsaveresult.Text = "";
            resulttimer.Enabled = false;
        }

        protected void btnnew_Click(object sender, EventArgs e)
        {
            //ClearHdrText();
            //ClearDtlText();
            //ClearFocText();
            //CreateDO_dtl();
            //CreateDO_foc_dtl();
            //ClearGridview();
            Response.Redirect("~/SALES_ORDER/Forms/Delivery_Order_Entry.aspx", false);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                string  type, billto, soldto,shipto, payterm, d_term, d_date,etd,eta,exist_do_no;
                DataTable dtdtl = (DataTable)ViewState["dt_do_dtl"];
                DataTable dtfoc = (DataTable)ViewState["dt_do_foc"];

                if (ddltype.Items.Count == 0)
                {

                    type = "";

                }
                else
                {
                    type = ddltype.SelectedItem.Value;
                }

                if (ddlshipto.Items.Count == 0)
                {
                    shipto = "";

                }
                else
                {
                    shipto = ddlshipto.SelectedItem.Value;
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

                if (ddlpay_term.Items.Count == 0)
                {
                    payterm = "";

                }
                else
                {
                    payterm = ddlpay_term.SelectedItem.Value;
                }

                if (ddldelivery_term.Items.Count == 0)
                {

                    d_term = "";
                }
                else
                {
                    d_term = ddldelivery_term.SelectedItem.Value;
                }

                d_date = txtd_date.Text;
                etd = txtetd.Text;
                eta = txteta.Text;
                exist_do_no = txtdelivery_no.Value;

             
                dtoresult = Process.SaveDeliveryOrder(type,billto,soldto,shipto,d_term,payterm,d_date,etd,eta,usn,dtdtl,dtfoc, exist_do_no,com);
                if (dtoresult.sts == true)
                {
                    DisplayPassResult("Save delivery order successfull.");
                    txtdelivery_no.Value = dtoresult.do_no;
                    txtinv_no.Value = dtoresult.inv_no;
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

        #region Details

        protected void ddlscno_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sc_no;
                if (ddlscno.Items.Count > 0)
                {
                    sc_no = ddlscno.SelectedItem.Value;

                }
                else
                {
                    sc_no = "";
                }

                DisplayCatalog(com, sc_no);
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void ddlcatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string ctlno,sc_no;
                if (ddlcatalog.Items.Count > 0)
                {
                    ctlno = ddlcatalog.SelectedItem.Value;
                }
                else
                {
                    ctlno = "";
                }

                if (ddlscno.Items.Count > 0)
                {
                    sc_no = ddlscno.SelectedItem.Value;

                }
                else
                {
                    sc_no = "";
                }


                DisplayCatalog_dsc(sc_no, ctlno);
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
                DataTable dtdtl = (DataTable)ViewState["dt_do_dtl"];
                string sc_no, ctlno, dsc, uom, qty, foc_qty, amount,c_ctlno;

                if (ddlscno.Items.Count > 0)
                {
                    sc_no = ddlscno.SelectedItem.Value;
                }
                else
                {
                    sc_no = "";
                }


                if (ddlcatalog.Items.Count > 0)
                {
                    ctlno = ddlcatalog.SelectedItem.Value;
                }
                else
                {
                    ctlno = "";
                }

                if (ddluom.Items.Count > 0)
                {
                    uom = ddluom.SelectedItem.Value;
                }
                else
                {
                    uom = "";
                }

                dsc = txtdesc.Value;
                qty = txtd_qty.Text;
                foc_qty = txtadd_foc.Text;
                amount = txtamount.Text;

                dtoresult = Process.CheckDtl_DataNull(ctlno, dsc, sc_no, uom, qty, foc_qty, amount);
                if (dtoresult.sts == true)
                {
                    foreach (GridViewRow row in dgvitems.Rows)
                    {

                        c_ctlno = row.Cells[2].Text;
                        if (ctlno == c_ctlno)
                        {
                            DisplayFailResult("This " + ctlno + " already existed.");
                            return;
                        }
                    }

                    //dtoresult = Process.AssignAutoNumber(dtscd);
                    dtdtl.Rows.Add(sc_no, ctlno, dsc, uom, qty, foc_qty,amount);
                    ViewState["dt_do_dtl"] = dtdtl;
                    dgvitems.DataSource = dtdtl;
                    dgvitems.DataBind();
                  
                    ClearDtlText();
                    DisplayPassResult("Add delivery order details successfull.");
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
            ClearDtlText();
        }

        protected void dgvitems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dtdo_dtl = ViewState["dt_do_dtl"] as DataTable;
                dtdo_dtl.Rows[index].Delete();
                ViewState["dt_do_dtl"] = dtdo_dtl;
                dgvitems.DataSource = dtdo_dtl;
                dgvitems.DataBind();
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }


        #endregion

        #region Header

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

        protected void ddlbillto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string ah_code;

                if (ddlbillto.Items.Count > 0)
                {
                    ah_code = ddlbillto.SelectedItem.Value;
                }
                else
                {
                    ah_code = "";
                }
                DisplayPaymentTerm(ah_code);
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void txtd_qty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string qty = txtd_qty.Text;
                decimal checkdecimal=0, dec_qty=0;
                int checkint=0;
                //dec_qty = 0;

                if (!string.IsNullOrEmpty(qty))
                {

                    if (int.TryParse(qty, out checkint))
                    {
                        dec_qty = decimal.Parse(qty);
                        txtd_qty.Text = dec_qty.ToString("F2");
                    }
                    else
                    {
                        // The string was a valid integer => use result here
                        if (decimal.TryParse(qty, out checkdecimal))
                        {
                            dec_qty = decimal.Parse(qty);
                            txtd_qty.Text = dec_qty.ToString("F2");
                        }
                        else
                        {
                            txtd_qty.Text = dec_qty.ToString("F2");
                            return;
                        }
                    }


                }
                else
                {
                    // invalid integer
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Only allow numeric.')</script>");
                    txtd_qty.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void txtadd_foc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string qty = txtadd_foc.Text;
                decimal checkdecimal=0, dec_qty=0;
                int checkint=0;
                //dec_qty = 0;

                if (!string.IsNullOrEmpty(qty))
                {

                    if (int.TryParse(qty, out checkint))
                    {
                        dec_qty = decimal.Parse(qty);
                        txtadd_foc.Text = dec_qty.ToString("F2");
                    }
                    else
                    {
                        // The string was a valid integer => use result here
                        if (decimal.TryParse(qty, out checkdecimal))
                        {
                            dec_qty = decimal.Parse(qty);
                            txtadd_foc.Text = dec_qty.ToString("F2");
                        }
                        else
                        {
                            txtadd_foc.Text = dec_qty.ToString("F2");
                            return;
                        }
                    }


                }
                else
                {
                    // invalid integer
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Only allow numeric.')</script>");
                    txtadd_foc.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void txtamount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string qty = txtamount.Text;
                decimal checkdecimal=0, dec_qty=0;
                int checkint=0;
                //dec_qty = 0;

                if (!string.IsNullOrEmpty(qty))
                {

                    if (int.TryParse(qty, out checkint))
                    {
                        dec_qty = decimal.Parse(qty);
                        txtamount.Text = dec_qty.ToString("F2");
                    }
                    else
                    {
                        // The string was a valid integer => use result here
                        if (decimal.TryParse(qty, out checkdecimal))
                        {
                            dec_qty = decimal.Parse(qty);
                            txtamount.Text = dec_qty.ToString("F2");
                        }
                        else
                        {
                            txtamount.Text = dec_qty.ToString("F2");
                            return;
                        }
                    }


                }
                else
                {
                    // invalid integer
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Only allow numeric.')</script>");
                    txtamount.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        #endregion

        #region FOC

        protected void ddlf_catalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string ctlno;
                if (ddlf_catalog.Items.Count > 0)
                {
                    ctlno = ddlf_catalog.SelectedItem.Value;
                }
                else
                {
                    ctlno = "";
                }
                DisplayFOC_Catalog_dsc(com, ctlno);

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtfoc = (DataTable)ViewState["dt_do_foc"];
                string ctlno, i_dsc, uom, qty, claim, gift, dsc, rmk;

                if (ddlf_catalog.Items.Count > 0)
                {
                    ctlno = ddlf_catalog.SelectedItem.Value;

                }
                else
                {
                    ctlno = "";

                }

                if (ddlf_uom.Items.Count > 0)
                {
                    uom = ddlf_uom.SelectedItem.Value;

                }
                else
                {
                    uom = "";

                }

                i_dsc = txtf_idsc.Text;
                qty = txtf_iqty.Text;
                claim = txtf_claim.Text;
                gift = txtf_gift.Text;
                dsc = txtf_dsc.Text;
                rmk = txtrmk.Text;
                dtoresult = Process.CheckFoc_DataNull(ctlno, i_dsc, uom, qty, claim, gift, dsc, rmk);

                if (dtoresult.sts == true)
                {

                    if (string.IsNullOrEmpty(claim))
                    {
                        claim = "-";
                    }
                    if (string.IsNullOrEmpty(gift))
                    {
                        gift = "-";
                    }
                    if (string.IsNullOrEmpty(dsc))
                    {
                        dsc = "-";
                    }
                    if (string.IsNullOrEmpty(rmk))
                    {
                        rmk = "-";
                    }
                    //dtoresult = Process.AssignAutoNumber(dtscd);
                    dtfoc.Rows.Add(ctlno, i_dsc, uom,qty, claim, gift,dsc,rmk);
                    ViewState["dt_do_foc"] = dtfoc;
                    dgv_items_info.DataSource = dtfoc;
                    dgv_items_info.DataBind();

                    ClearFocText();
                    DisplayPassResult2("Add foc details successfull.");
                }
                else
                {
                    DisplayFailResult2(dtoresult.Message);
                }

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void resulttimer2_Tick(object sender, EventArgs e)
        {
            lblsaveresult2.Text = "";
            resulttimer2.Enabled = false;
        }

        protected void dgv_items_info_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dtdo_foc = ViewState["dt_do_foc"] as DataTable;
                dtdo_foc.Rows[index].Delete();
                ViewState["dt_do_foc"] = dtdo_foc;
                dgv_items_info.DataSource = dtdo_foc;
                dgv_items_info.DataBind();
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void txtf_iqty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string qty = txtf_iqty.Text;
                decimal checkdecimal=0, dec_qty=0;
                int checkint=0;
                //dec_qty = 0;

                if (!string.IsNullOrEmpty(qty))
                {

                    if (int.TryParse(qty, out checkint))
                    {
                        dec_qty = decimal.Parse(qty);
                        txtf_iqty.Text = dec_qty.ToString("F2");
                    }
                    else
                    {
                        // The string was a valid integer => use result here
                        if (decimal.TryParse(qty, out checkdecimal))
                        {
                            dec_qty = decimal.Parse(qty);
                            txtf_iqty.Text = dec_qty.ToString("F2");
                        }
                        else
                        {
                            txtf_iqty.Text = dec_qty.ToString("F2");
                            return;
                        }
                    }


                }
                else
                {
                    // invalid integer
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Only allow numeric.')</script>");
                    txtf_iqty.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void btnadd_clear_Click(object sender, EventArgs e)
        {
            ClearFocText();
        }

        #endregion

    }
}