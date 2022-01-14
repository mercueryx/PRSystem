using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ERP_System.PR_Module.PR_Control;

namespace ERP_System.PR_Module.Forms
{
    public partial class PR_Edit : System.Web.UI.Page
    {
        PR_dto dtoresult = new PR_dto();
        PR_bo Process = new PR_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com, usn, form;

        protected void Page_Load(object sender, EventArgs e)
        {
            txtsearch_date.Attributes.Add("autocomplete", "off");
            usn = (string)Session["usn"];
            com = (string)Session["com"];
            form = "PR_EDIT";

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
                        //DisplayCompanyCode(com);
                        //DisplayUOM();
                        //DisplayCatalog();
                        //DisplayVendorType(com);
                        //DisplaySection(com);
                        DisplayDepartment(com);
                        DisplayCompanyCode();
                        DisplayOpenPR("", usn);
                        DisplayUOM();
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

        private void DisplaySection(string com,string dpt)
        {
            try
            {
                dtoresult = Process.DisplaySection(com,dpt);
                if (dtoresult.dtsec.Rows.Count > 0)
                {
                    ddlsec.DataSource = dtoresult.dtsec;
                    ddlsec.DataTextField = "sec";
                    ddlsec.DataValueField = "sec";
                    ddlsec.DataBind();
                    ddlsec.Items.Insert(0, new ListItem("Select Section", "Select Section"));
                    //ddlcom.Items.Insert(1, new ListItem("JL", "JL"));
                    ddlsec.SelectedIndex = 0;
                }
                else
                {
                    ddlsec.Items.Clear();
                    ddlsec.DataSource = null;
                    ddlsec.DataBind();
                }
            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }

        }

        private void DisplayDepartment(string com)
        {
            try
            {
                dtoresult = Process.DisplayDepartment(com);
                if (dtoresult.dtdpt.Rows.Count > 0)
                {
                    ddlr_dpt.DataSource = dtoresult.dtdpt;
                    ddlr_dpt.DataTextField = "dpt";
                    ddlr_dpt.DataValueField = "dpt";
                    ddlr_dpt.DataBind();
                    ddlr_dpt.Items.Insert(0, new ListItem("Select Department", "Select Department"));
                    //ddlcom.Items.Insert(1, new ListItem("JL", "JL"));
                    ddlr_dpt.SelectedIndex = 0;
                }
                else
                {
                    ddlr_dpt.Items.Clear();
                    ddlr_dpt.DataSource = null;
                    ddlr_dpt.DataBind();
                }
            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayCompanyCode()
        {
            try
            {
                Log_result = Log_Process.DisplayCompanyCode();
                if (Log_result.dtcom.Rows.Count > 0)
                {
                    ddlcom.DataSource = Log_result.dtcom;
                    ddlcom.DataTextField = "company_code";
                    ddlcom.DataValueField = "company_code";
                    ddlcom.DataBind();
                    ddlcom.Items.Insert(0, new ListItem("Select Company", "Select Company"));
                    //ddlcom.Items.Insert(1, new ListItem("JL", "JL"));
                    ddlcom.SelectedIndex = 0;
                }
                else
                {
                    ddlcom.Items.Clear();
                    ddlcom.DataSource = null;
                    ddlcom.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
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

        private void DisplayOpenPR(string orderdate,string usn)
        {
            try
            {
               dtoresult = Process.DisplayOpenPR(orderdate,usn);
                if (dtoresult.dtcheck.Rows.Count > 0)
                {
                    ddls_prno.DataSource = dtoresult.dtcheck;
                    ddls_prno.DataTextField = "pr_rn";
                    ddls_prno.DataValueField = "pr_rn";
                    ddls_prno.DataBind();
                    ddls_prno.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddls_prno.SelectedIndex = 0;
                }
                else
                {
                    ddls_prno.Items.Clear();
                    ddls_prno.DataSource = null;
                    ddls_prno.DataBind();

                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayPRHdr(string pr_rn)
        {
            try
            {
                string sts;
                sts = "";
                dtoresult = Process.GetPRHeader(pr_rn);
                if (dtoresult.dtpr_hdr.Rows.Count > 0)
                {
                    txtName.Value = dtoresult.dtpr_hdr.Rows[0]["nm"].ToString();
                    ddlcom.SelectedIndex = ddlcom.Items.IndexOf(ddlcom.Items.FindByValue(dtoresult.dtpr_hdr.Rows[0]["req_com"].ToString()));
                    ddlr_dpt.SelectedIndex = ddlr_dpt.Items.IndexOf(ddlr_dpt.Items.FindByValue(dtoresult.dtpr_hdr.Rows[0]["req_dpt"].ToString()));

                    //ddlr_dpt.Items.FindByValue(dtoresult.dtpr_hdr.Rows[0]["req_dpt"].ToString()).Selected = true;
                    //ddlsec.Items.FindByValue(dtoresult.dtpr_hdr.Rows[0]["sec"].ToString()).Selected = true;
                    DisplaySection(com, dtoresult.dtpr_hdr.Rows[0]["req_dpt"].ToString());
                    ddlsec.SelectedIndex = ddlsec.Items.IndexOf(ddlsec.Items.FindByValue(dtoresult.dtpr_hdr.Rows[0]["sec"].ToString()));
                    txtpr.Value = dtoresult.dtpr_hdr.Rows[0]["pr_rn"].ToString();
                    sts = dtoresult.dtpr_hdr.Rows[0]["sts"].ToString();
                    if (sts == "OPEN")
                    {
                        txtName.Disabled = false;
                        ddlsec.Enabled = true;
                        ddlcom.Enabled = true;
                        btnupdate.Visible = true;

                    }
                    else
                    {
                        ddlcom.Enabled = false;
                        ddlsec.Enabled = false;
                        txtName.Disabled = true;
                        btnupdate.Visible = false;

                    }

                    DisplayPRDtl(pr_rn);
                }
            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());

            }
        }

        private void DisplayPRDtl(string pr_rn)
        {
            try
            {
                dtoresult = Process.GetPRDetails(pr_rn);
                if (dtoresult.dtpr_dtl.Rows.Count > 0)
                {
                    dgvitems.DataSource = dtoresult.dtpr_dtl;
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

        private void DisplayUOM()
        {
            try
            {
                dtoresult = Process.DisplayAllUOM();
                if (dtoresult.dtUOM.Rows.Count > 0)
                {
                    ddluom.DataSource = dtoresult.dtUOM;
                    ddluom.DataTextField = "purchase_um";
                    ddluom.DataValueField = "purchase_um";
                    ddluom.DataBind();
                    ddluom.Items.Insert(0, new ListItem("Select UOM", "Select UOM"));
                    //ddlcom.Items.Insert(1, new ListItem("JL", "JL"));
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

        private void DisplayUOM_desc(string desc)
        {
            try
            {
                dtoresult = Process.DisplayUOM_desc(desc);
                if (dtoresult.dtUOM.Rows.Count > 0)
                {
                    // product_ref
                    ddluom.DataSource = dtoresult.dtUOM;
                    ddluom.DataTextField = "purchase_um";
                    ddluom.DataValueField = "purchase_um";
                    ddluom.DataBind();
                    //ddluom.Items.Insert(0, new ListItem("Select UOM", "Select UOM"));
                    ////ddlcom.Items.Insert(1, new ListItem("JL", "JL"));
                    //ddluom.SelectedIndex = 0;
                }
                else
                {
                    DisplayUOM();
                }
            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }
        }

        #endregion


        #region Clear Data
        private void ClearHdr()
        {
            txtpr.Value = "";
            ddlsec.SelectedIndex=0;
            ddlr_dpt.SelectedIndex = 0;
            ddlcom.SelectedIndex = 0;
            txtName.Value = "";

            dgvitems.DataSource = null;
            dgvitems.DataBind();


        }

        private void ClearDtl()
        {
            txtitem.Text = "";
            txtpurpose.Text = "";
            txtqty.Text = "";

        }

        #endregion

        protected void txtsearch_date_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string order_date;
                order_date = txtsearch_date.Text;
                DisplayOpenPR(order_date,usn);

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }


        protected void ddls_prno_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string pr_rn;
                pr_rn = ddls_prno.SelectedItem.Value;
                DisplayPRHdr(pr_rn);

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

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                string pr_rn, nm,req_dpt,req_com,req_sec;
                pr_rn = txtpr.Value;
                req_dpt = ddlr_dpt.SelectedValue;
                nm = txtName.Value.ToUpper();
                req_com = ddlcom.SelectedValue;

                if (ddlsec.Items.Count > 0)
                {
                    req_sec = ddlsec.SelectedValue;
                }
                else
                {
                    req_sec = "Select Section";
                }



                dtoresult = Process.EditPRHdr(req_com,pr_rn, nm,req_dpt,req_sec);
                if (dtoresult.sts == true)
                {
                    DisplayPassResult("PR update successfull.");
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

        protected void btncancel_Click(object sender, EventArgs e)
        {
            ClearDtl();
            ClearHdr();
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

        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                string item, purpose, qty,pr_rn,lvl,item_ref,uom;
                item = txtitem.Text;
                purpose = txtpurpose.Text;
                qty = txtqty.Text;
                pr_rn = txtpr.Value;
                lvl = ddllevel.SelectedValue.ToString();

                //if (ddlref.Items.Count > 0)
                //{
                //    item_ref = ddlref.SelectedValue;

                //    if (item_ref == "Select Item Ref")
                //    {
                //        DisplayFailResult("Please select item reference.");
                //        return;
                //    }
                //    item = item + "--" + item_ref;
                //}
                //else
                //{
                //    item_ref = "";
                //    item = item + item_ref;
                //}

                item_ref = txtref.Text.ToUpper();
                if (string.IsNullOrEmpty(item_ref))
                {
                    item = item + "";
                }
                else
                {
                    item = item +"--"+ item_ref;
                }

                if (ddluom.Items.Count > 0)
                {
                    uom = ddluom.SelectedValue;

                    if (uom == "Select UOM")
                    {
                        DisplayFailResult("Please select UOM.");
                        return;
                    }

                }
                else
                {
                    uom = "-";
                }

                dtoresult = Process.AddNewPR_Dtl(pr_rn, item, purpose, qty,lvl, usn,uom);
                if (dtoresult.sts == true)
                {
                    DisplayPassResult("Add item successful.");
                    DisplayPRDtl(pr_rn);
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

        protected void dgvitems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string id, pr_rn;

                id = dgvitems.DataKeys[e.RowIndex].Values[0].ToString();
                //ctlno = e.Keys["catalog_no"].ToString();
                pr_rn = txtpr.Value;
                dtoresult = Process.DeletePRDetails(pr_rn, id);
                if (dtoresult.sts == true)
                {
                    DisplayPRDtl(pr_rn);

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

        protected void ddlr_dpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string dpt;
                dpt = ddlr_dpt.SelectedValue;
                DisplaySection(com, dpt);
            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }
        }

        protected void txtitem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string desc;
                desc = txtitem.Text;
                //dtoresult = Process.DisplayRef(desc, com);
                //if (dtoresult.dtref.Rows.Count > 0)
                //{
                //    // product_ref
                //    ddlref.DataSource = dtoresult.dtref;
                //    ddlref.DataTextField = "product_ref";
                //    ddlref.DataValueField = "product_ref";
                //    ddlref.DataBind();
                //    ddlref.Items.Insert(0, new ListItem("Select Item Ref", "Select Item Ref"));
                //    //ddlcom.Items.Insert(1, new ListItem("JL", "JL"));
                //    ddlref.SelectedIndex = 0;
                //}
                //else
                //{
                //    ddlref.Items.Clear();
                //    ddlref.DataSource = null;
                //    ddlref.DataBind();
                //}

                DisplayUOM_desc(desc);
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
                ClearDtl();
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }


        #region Web Services

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        //[WebMethod(EnableSession = true)]
        public static List<string> GetItems(string prefixText)
        {
            string com;
            com = HttpContext.Current.Session["com"].ToString();
            PR_dto dtoresult = new PR_dto();
            PR_bo Process = new PR_bo();

            List<string> ItemNames = new List<string>();

            dtoresult = Process.DisplayItem(prefixText, com);
            if (dtoresult.message != "No result found")
            {
                ItemNames = dtoresult.List_Item;

            }

            return ItemNames;
        }
        #endregion
    }
}