using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERP_System.SALES_ORDER.SALES_ORDER_Control;
using System.Data;
using System.Web.Services;
using System.Data.SqlClient;

namespace ERP_System.SALES_ORDER.Forms
{

    public partial class Sales_Contract_Entry : System.Web.UI.Page
    {

        SO_dto dtoresult = new SO_dto();
        SO_bo Process = new SO_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com, usn, form;     

        protected void Page_Load(object sender, EventArgs e)
        {
            usn = (string)Session["usn"];
            com = (string)Session["com"];
            form = "SC_ENTRY";
          
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
                        DisplayUOM();
                        CreateSCDTable();
                        CreateSCDDTable();
                        DisplayGroupCode(com);
                        DisplayCatalogNo(com);
                        DisplaySignatory(com);

                        ddlbillto.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        ddlbillto.SelectedIndex = 0;
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

        private void CreateSCDTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[9] { new DataColumn("no"), new DataColumn("ctlno"), new DataColumn("desc"), new DataColumn("exdesc"), new DataColumn("o_qty"), new DataColumn("foc_qty"), new DataColumn("uom"), new DataColumn("uprice"), new DataColumn("total") });
                ViewState["dtscd"] = dt;
                //rptr1.DataSource = dt;
                //rptr1.DataBind();
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void CreateSCDDTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[8] { new DataColumn("no"), new DataColumn("id"), new DataColumn("etd"), new DataColumn("eta"), new DataColumn("qty"), new DataColumn("foc_qty"), new DataColumn("s_qty"), new DataColumn("p_qty") });
                ViewState["dtscdd"] = dt;
                //rptr1.DataSource = dt;
                //rptr1.DataBind();
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }

        }

        #endregion

        #region DisplayData

        #region Header
    
        private void DisplaySignatory(string com)
        {
            try
            {
                dtoresult = Process.DisplaySignatory(com);
                if (dtoresult.dtSignatory.Rows.Count > 0)
                {
                    ddlsignatory.DataSource = dtoresult.dtSignatory;
                    ddlsignatory.DataTextField = "dsc";
                    ddlsignatory.DataValueField = "signatory_code";
                    ddlsignatory.DataBind();
                    ddlsignatory.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlsignatory.SelectedIndex = 0;
                }
                else
                {
                    ddlsignatory.Items.Clear();
                    ddlsignatory.DataSource = null;
                    ddlsignatory.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayBillTo(string group, string type)
        {
            try
            {
                dtoresult = Process.DisplayBillTo( group, type);
                if (dtoresult.dtBillTo.Rows.Count > 0)
                {
                    ddlbillto.DataSource = dtoresult.dtBillTo;
                    ddlbillto.DataTextField = "dsc";
                    ddlbillto.DataValueField = "ah_code";
                    ddlbillto.DataBind();
                
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

        private void DisplaySoldTo(string group)
        {
            try
            {
                dtoresult = Process.DisplaySoldTo(group);
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

        private void DisplayPurchaseTerm(string com, string ahcode)
        {
            try
            {
                dtoresult = Process.DisplayPurTerm(com, ahcode);
                if (dtoresult.dtTerm.Rows.Count > 0)
                {
                    ddlpay_term.DataSource = dtoresult.dtTerm;
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

        private void DisplayGroupCode(string com)
        {
            try
            {
                dtoresult = Process.DisplayGroupCode(com);
                if (dtoresult.dtgroup.Rows.Count > 0)
                {
                    ddlgroup.DataSource = dtoresult.dtgroup;
                    ddlgroup.DataTextField = "group_code";
                    ddlgroup.DataValueField = "group_code";
                    ddlgroup.DataBind();
                    ddlgroup.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlgroup.SelectedIndex = 0;
                }
                else
                {
                    ddlgroup.Items.Clear();
                    ddlgroup.DataSource = null;
                    ddlgroup.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        #endregion

        #region Details

        private void DisplayUOM()
        {
            try
            {
                dtoresult = Process.DisplayUOM();
                if (dtoresult.dtUOM.Rows.Count > 0)
                {
                    ddluom.DataSource = dtoresult.dtUOM;
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

        private void DisplayCatalogNo(string com)
        {
            try
            {
                dtoresult = Process.DisplayCatalogNo(com);
                if (dtoresult.dtCatalog.Rows.Count > 0)
                {
                    ddlcatalog.DataSource = dtoresult.dtCatalog;
                    ddlcatalog.DataTextField = "dsc";
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

        private void DisplayCatalogInfo(string com, string ctlno)
        {
            try
            {
                dtoresult = Process.DisplayCatalogInfo(ctlno);
                if (dtoresult.dtCtlnoInfo.Rows.Count > 0)
                {
                    txtdesc.Value = dtoresult.dtCtlnoInfo.Rows[0]["dsc"].ToString();
                    txtextra_dsc.Value = dtoresult.dtCtlnoInfo.Rows[0]["ex_dsc"].ToString();
                }
                else
                {
                    txtdesc.Value = "";
                    txtextra_dsc.Value = "";
                }

                dtoresult = Process.DisplayUnitSellPrice(ctlno);
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

        private void DisplayAddCatalogNo()
        {
            try
            {
                DataTable dtscd = (DataTable)ViewState["dtscd"];
                if (dtscd.Rows.Count > 0)
                {
                    ddladd_catalog.DataSource = dtscd;
                    ddladd_catalog.DataTextField = "ctlno";
                    ddladd_catalog.DataValueField = "no";
                    ddladd_catalog.DataBind();
                    ddladd_catalog.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddladd_catalog.SelectedIndex = 0;
                }
                else
                {
                    ddladd_catalog.Items.Clear();
                    ddladd_catalog.DataSource = null;
                    ddladd_catalog.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void GetSelectedData()
        {
            try
            {
                string track_Id;
                foreach (GridViewRow rows in dgvitems.Rows)
                {
                    Label lblid = rows.FindControl("lblid") as Label;
                    track_Id = lblid.Text;

                    if (rows.BackColor == System.Drawing.Color.LightBlue)
                    {
                        RefreshSC_DeliveryDetails(track_Id);

                    }
                    else
                    {
                        DataTable dtscdd = ViewState["dtscdd"] as DataTable;
                        dgv_items_info.DataSource = dtscdd;
                        dgv_items_info.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void RefreshSC_DeliveryDetails(string id)
        {

            try
            {
                string track_Id;
                DataTable dtscdd = ViewState["dtscdd"] as DataTable;
                dgv_items_info.DataSource = dtscdd;
                dgv_items_info.DataBind();

                foreach (GridViewRow rows in dgv_items_info.Rows)
                {

                    Label lblid = rows.FindControl("lblid") as Label;
                    track_Id = lblid.Text;

                    if (track_Id == id)
                    {
                        rows.Visible = true;
                    }
                    else
                    {
                        rows.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        #endregion

        #endregion

        #region ClearText

        private void ClearSCDD()
        {
            txtetd.Text = "";
            txteta.Text = "";
            txtqty.Text = "0.00";
            txtfoc_qty.Text = "0.00";
            txtshipped_qty.Text = "0.00";
            txtplan_qty.Text = "0.00";
            if (object.ReferenceEquals(ddladd_catalog, null))
            {

                ddladd_catalog.SelectedIndex = 0;
            }
           
        }

        private void ClearText()
        {
            txtdesc.Value = "";
            // txtcatalog.Text = "";
            txtextra_dsc.Value = "";
            txtadd_qty.Text = "0.00";
            txtadd_foc.Text = "0.00";
            txtprice.Text = "0.00";
            txt_total.Text = "0.00";
            ddlcatalog.SelectedIndex = 0;
            ddluom.SelectedIndex = 0;
            //ddlcatalog.DataSource = null;
        }

        private void ClearHeader()
        {
            txtscdate.Text = "";
            ddlgroup.SelectedIndex = 0;
            ddltype.SelectedIndex = 0;
            ddlsignatory.SelectedIndex = 0;
            ddlbillto.Items.Clear();
            ddlbillto.DataSource = null;
            ddlbillto.DataBind();
            ddlsold.Items.Clear();
            ddlsold.DataSource = null;
            ddlsold.DataBind();
            ddlpay_term.Items.Clear();
            ddlpay_term.DataSource = null;
            ddlpay_term.DataBind();

        }

     
        #endregion

        #region Header

        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string  group, type;
                //com = ddlcom.SelectedItem.Value;
                if (ddlgroup.Items.Count ==0)
                {
                    group = "";
                }
                else
                {
                    group = ddlgroup.SelectedItem.Value;
                }

                if (ddltype.Items.Count == 0)
                {
                    type = "";
                }
                else
                {
                    type = ddltype.SelectedItem.Value;
                }

                DisplayBillTo( group, type);
                DisplaySoldTo(group);
               
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string group, type, ahcode;
                //com = ddlcom.SelectedItem.Value;            
                group = ddlgroup.SelectedItem.Value;
               
                if (ddlbillto.Items.Count ==0)
                {
                    ahcode = "";
                }
                else
                {
                    ahcode = ddlbillto.SelectedItem.Value;
                }

                if (ddltype.Items.Count == 0)
                {
                    type = "";
                }
                else
                {
                    type = ddltype.SelectedItem.Value;
                }

                DisplayBillTo(group, type);
                DisplaySoldTo( group);
                DisplayPurchaseTerm(com, ahcode);
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
                string  ahcode;
                //com = ddlcom.SelectedItem.Value;
                ahcode = ddlbillto.SelectedItem.Value;

                DisplayPurchaseTerm(com, ahcode);
                
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        #endregion

        #region Details    

        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtscd = (DataTable)ViewState["dtscd"];
                string ctlno, dsc, ex_dsc, uom, qty,foc_qty, u_price, total,c_ctlno;               
                
                dsc = txtdesc.Value;
                ex_dsc = txtextra_dsc.Value;
                qty = txtadd_qty.Text;
                u_price = txtprice.Text;
                total = txt_total.Text;
                foc_qty = txtfoc_qty.Text;

                if (ddlcatalog.Items.Count ==0)
                {
                    ctlno = "";
                }
                else
                {
                    ctlno = ddlcatalog.SelectedItem.Value;
                }
              

                if (ddluom.Items.Count ==0)
                {
                    uom = "";
                }
                else
                {
                    uom = ddluom.SelectedItem.Value;
                }

                dtoresult = Process.CheckSCD_DataNull(ctlno, dsc, ex_dsc, uom, qty,foc_qty,u_price, total);
                if (dtoresult.sts == true)
                {
                    foreach (GridViewRow row in dgvitems.Rows)
                    {

                        
                        c_ctlno = row.Cells[1].Text;
                        if (ctlno == c_ctlno)
                        {
                            DisplayFailResult("This " + ctlno + " already existed.");
                            return;
                        }
                    }

                    dtoresult = Process.AssignAutoNumber(dtscd);
                    dtscd.Rows.Add(dtoresult.auto_no,ctlno, dsc, ex_dsc, qty,foc_qty, uom, u_price, total);
                    ViewState["dtscd"] = dtscd;
                    dgvitems.DataSource = dtscd;
                    dgvitems.DataBind();
                    DisplayAddCatalogNo();
                    ClearText();
                    DisplayPassResult("Add sales contract details successfull.");

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

        protected void resulttimer2_Tick(object sender, EventArgs e)
        {
            lblsaveresult2.Text = "";
            resulttimer2.Enabled = false;
        }

        protected void resulttimer_Tick(object sender, EventArgs e)
        {
            lblsaveresult.Text = "";
            resulttimer.Enabled = false;
        }

        protected void btnadd_clear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearSCDD();
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
              
                string no, etd, eta, qty, foc_qty, ship_qty, plan_qty;

                if (ddladd_catalog.Items.Count ==0)
                {
                    no = "";

                }
                else
                {
                    no = ddladd_catalog.SelectedItem.Value;
                }
               
                etd = txtetd.Text;
                eta = txteta.Text;
                qty = txtqty.Text;
                foc_qty = txtfoc_qty.Text;
                ship_qty = txtshipped_qty.Text;
                plan_qty = txtplan_qty.Text;
                //check data null
                dtoresult = Process.CheckSC_DeliveryDetails(no, etd, eta);
                if (dtoresult.sts == true)
                {
                    //check qty ~~~ will consider
                    DataTable dtscdd = (DataTable)ViewState["dtscdd"];
                    dtoresult = Process.AssignAutoNumber(dtscdd);
                    dtscdd.Rows.Add(dtoresult.auto_no, no, etd, eta, qty, foc_qty, ship_qty, plan_qty);
                    ViewState["dtscdd"] = dtscdd;
                    dgv_items_info.DataSource = dtscdd;
                    dgv_items_info.DataBind();
                    ClearSCDD();
                    DisplayPassResult2("Add sales contract delivery details successful.");
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

        protected void txtplan_qty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string qty = txtplan_qty.Text;
                decimal checkdecimal=0, dec_qty=0;
                int checkint = 0;
                //dec_qty = 0;

                if (!string.IsNullOrEmpty(qty))
                {

                    if (int.TryParse(qty, out checkint))
                    {
                        dec_qty = decimal.Parse(qty);
                        txtplan_qty.Text = dec_qty.ToString("F2");
                    }
                    else
                    {
                        // The string was a valid integer => use result here
                        if (decimal.TryParse(qty, out checkdecimal))
                        {
                            dec_qty = decimal.Parse(qty);
                            txtplan_qty.Text = dec_qty.ToString("F2");
                        }
                        else
                        {
                            txtplan_qty.Text = dec_qty.ToString("F2");
                            return;
                        }
                    }


                }
                else
                {
                    // invalid integer
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Only allow numeric.')</script>");
                    txtplan_qty.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void txtshipped_qty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string qty = txtshipped_qty.Text;
                decimal checkdecimal=0, dec_qty=0;
                int checkint=0;
                //dec_qty = 0;

                if (!string.IsNullOrEmpty(qty))
                {

                    if (int.TryParse(qty, out checkint))
                    {
                        dec_qty = decimal.Parse(qty);
                        txtshipped_qty.Text = dec_qty.ToString("F2");
                    }
                    else
                    {
                        // The string was a valid integer => use result here
                        if (decimal.TryParse(qty, out checkdecimal))
                        {
                            dec_qty = decimal.Parse(qty);
                            txtshipped_qty.Text = dec_qty.ToString("F2");
                        }
                        else
                        {
                            txtshipped_qty.Text = dec_qty.ToString("F2");
                            return;
                        }
                    }


                }
                else
                {
                    // invalid integer
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Only allow numeric.')</script>");
                    txtshipped_qty.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void txtfoc_qty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string qty = txtfoc_qty.Text;
                decimal checkdecimal=0, dec_qty=0;
                int checkint=0;
                //dec_qty = 0;

                if (!string.IsNullOrEmpty(qty))
                {

                    if (int.TryParse(qty, out checkint))
                    {
                        dec_qty = decimal.Parse(qty);
                        txtfoc_qty.Text = dec_qty.ToString("F2");
                    }
                    else
                    {
                        // The string was a valid integer => use result here
                        if (decimal.TryParse(qty, out checkdecimal))
                        {
                            dec_qty = decimal.Parse(qty);
                            txtfoc_qty.Text = dec_qty.ToString("F2");
                        }
                        else
                        {
                            txtfoc_qty.Text = dec_qty.ToString("F2");
                            return;
                        }
                    }


                }
                else
                {
                    // invalid integer
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Only allow numeric.')</script>");
                    txtfoc_qty.Text = "0.00";
                }
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
                decimal checkdecimal = 0 , dec_qty=0;
                int checkint = 0;
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

        protected void dgv_items_info_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {               
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dtscdd = ViewState["dtscdd"] as DataTable;
                dtscdd.Rows[index].Delete();
                ViewState["dtscdd"] = dtscdd;
                GetSelectedData();
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
                string dtl;
                List<string> indexlist = new List<string>();
                int index = Convert.ToInt32(e.RowIndex);
              
                //delete header
                DataTable dtscd = ViewState["dtscd"] as DataTable;
                dtl = dtscd.Rows[index]["po_no"].ToString();
                dtscd.Rows[index].Delete();

                ViewState["dtscd"] = dtscd;
                dgvitems.DataSource = dtscd;
                dgvitems.DataBind();

                //delete details
                string del_dtl;
                DataTable dtscdd = ViewState["dtscd"] as DataTable;
                for (int i = 0; i < dtscdd.Rows.Count; i++)
                {
                    del_dtl = dtscdd.Rows[i]["no"].ToString();
                    if (del_dtl == dtl)
                    {
                        //dtdetails.Rows[i].Delete();
                        indexlist.Add(i.ToString());
                    }
                }

                int count, indexcount;
                count = 0;
                indexcount = 0;
                foreach (string value in indexlist)
                {
                    indexcount = Convert.ToInt32(value);
                    if (count == 0)
                    {

                        dtscdd.Rows[indexcount].Delete();
                        count += 1;
                    }
                    else
                    {
                        indexcount = indexcount - 1;
                        dtscdd.Rows[indexcount].Delete();
                    }

                }

                ViewState["dtscdd"] = dtscdd;
                dgv_items_info.DataSource = dtscdd;
                dgv_items_info.DataBind();

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void txtadd_qty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string qty = txtqty.Text;
                decimal checkdecimal=0, dec_qty=0;
                int checkint =0;
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
                decimal checkdecimal = 0, dec_price =0;
                int checkint =0;
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

        protected void txtadd_foc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string price = txtadd_foc.Text;
                decimal checkdecimal = 0, dec_price=0;
                int checkint = 0;
                //dec_price = 0;

                if (!string.IsNullOrEmpty(price))
                {

                    if (int.TryParse(price, out checkint))
                    {
                        dec_price = decimal.Parse(price);
                        txtadd_foc.Text = dec_price.ToString("F2");
                    }
                    else
                    {
                        // The string was a valid integer => use result here
                        if (decimal.TryParse(price, out checkdecimal))
                        {
                            dec_price = decimal.Parse(price);
                            txtadd_foc.Text = dec_price.ToString("F2");
                        }
                        else
                        {
                            txtadd_foc.Text = dec_price.ToString("F2");
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

        protected void ddlcatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string  ctlno;
                //com = ddlcom.SelectedItem.Value;
                ctlno = ddlcatalog.SelectedItem.Value;
            
                DisplayCatalogInfo(com, ctlno);

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }
  
        protected void dgvitems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string no;
                GridViewRow dtl_row = dgvitems.SelectedRow;
                Label lbl_d_id = dtl_row.FindControl("lblid") as Label;
                no = lbl_d_id.Text;
                foreach (GridViewRow rows in dgvitems.Rows)
                {
                    if (rows.RowIndex == dtl_row.RowIndex)
                    {
                        rows.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        rows.BackColor = System.Drawing.Color.White;
                    }
                }

                RefreshSC_DeliveryDetails(no);

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
                string  group, type, billto, soldto, payterm, sc_date, signatory;
                DataTable dtscd = (DataTable)ViewState["dtscd"];
                DataTable dtscdd = (DataTable)ViewState["dtscdd"];

                if (ddlgroup.Items.Count == 0)
                {

                    group = "";

                }
                else
                {
                    group = ddlgroup.SelectedItem.Value;
                }

                if (ddltype.Items.Count == 0)
                {
                    type = "";
                  
                }
                else
                {
                    type = ddltype.SelectedItem.Value;
                }

                if (ddlbillto.Items.Count ==0)
                {
                    billto = "";
                }
                else
                {
                    billto = ddlbillto.SelectedItem.Value;
                }

                if (ddlsold.Items.Count ==0)
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

                if (ddlsignatory.Items.Count ==0)
                {

                    signatory = "";
                }
                else
                {
                    signatory = ddlsignatory.SelectedItem.Value;
                }

               
                //billto = ddlbillto.SelectedItem.Value;
                //soldto = ddlsold.SelectedItem.Value;
               
             
                sc_date = txtscdate.Text;

                dtoresult = Process.SaveSalesContract(com, group, type, billto, soldto, payterm, sc_date, signatory,usn, dtscd, dtscdd);
                if (dtoresult.sts == true)
                {
                    DisplayPassResult("Save sales contract successfull.");
                    txtsc_no.Value = dtoresult.scno;
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
            //ClearHeader();
            //ClearSCDD();
            //ClearText();
            //CreateSCDDTable();
            //CreateSCDTable();
            //dgvitems.DataSource = null;
            //dgvitems.DataBind();
            //dgv_items_info.DataSource = null;
            //dgv_items_info.DataBind();

            Response.Redirect("~/SALES_ORDER/Forms/Sales_Contract_Entry.aspx", false);
        }

        #endregion
    }
}