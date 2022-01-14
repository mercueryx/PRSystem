using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERP_System.PR_Module.PR_Control;
using System.Data;
//using System.Web.Services;
namespace ERP_System.PR_Module.Forms
{
    public partial class PR_ENTRY : System.Web.UI.Page
    {
        PR_dto dtoresult = new PR_dto();
        PR_bo Process = new PR_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com, usn, form,sec,dpt,name;
        protected void Page_Load(object sender, EventArgs e)
        {
            usn = (string)Session["usn"];
            com = (string)Session["com"];
            sec = (string)Session["sec"];
            dpt = (string)Session["dpt"];
            name = (string)Session["nm"];
            form = "PR_ENTRY";

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

                        DisplayCompanyCode();
                      
                        CreateItemTable();
                        DisplayDepartment(com);
                        DisplayUOM();
                       // DisplaySection(com);
                       // DisplayCatalog();
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
                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("item_name"), new DataColumn("qty"), new DataColumn("purpose"), new DataColumn("level"), new DataColumn("uom") });
                ViewState["Items"] = dt;
             
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion


        #region DisplayData
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
            catch (Exception ex) 
            {

                DisplayFailResult(ex.ToString());
            }

        }

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


        #region ResetText

        private void ClearText()
        {
          
            // txtcatalog.Text = "";
            txtpurpose.Text = "";
            txtqty.Text = "0";
            txtitem.Text = "";
         
            //ddlcatalog.DataSource = null;
        }

        private void ClearHeader()
        {
            try
            {
               
                txtpr.Value = "";

                //txtName.Value = "";
                ddlsec.SelectedIndex = 0;
              
                ViewState["Items"] = null;
                dgvitems.DataSource = null;
                dgvitems.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void DisableTxtBox(bool enable)
        {
            txtref.Enabled = enable;
            txtpurpose.Enabled = enable;
        }

        #endregion

        #region Page Control
   

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                string req_com,req_dpt,nm,exist_pr,req_sec;
                DataTable dtitems = new DataTable();
                dtitems = (DataTable)ViewState["Items"];
               // com = ddlcom.SelectedItem.Value;
                exist_pr = txtpr.Value;
                if (ddlsec.Items.Count > 0)
                {
                    req_sec = ddlsec.SelectedValue;
                }
                else
                {
                    req_sec = "Select Section";
                }
                req_com = ddlcom.SelectedValue;
                req_dpt = ddlr_dpt.SelectedValue;
              
             
                nm = txtName.Value.ToUpper();
               
                dtoresult = Process.InsertPR(com,req_com,req_dpt,dpt,req_sec, nm,usn,name,dtitems,exist_pr);
                if (dtoresult.sts == true)
                {
                    //txtrev.Value = dtoresult.version;
                    txtpr.Value = dtoresult.pr_rn;

                    DisplayPassResult("PR save successful.");
                    //ClearHeader();
                    //ClearText();
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
            try
            {
                ClearText();
                ClearHeader();
                CreateItemTable();
               // ddlcom.SelectedIndex = 0;
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
                DataTable dt = (DataTable)ViewState["Items"];
                int count = dt.Rows.Count;

                string item, qty, purpose, level, item_ref, uom;
                item = txtitem.Text.ToUpper();
                qty = txtqty.Text;
                purpose = txtpurpose.Text;
                level = ddllevel.SelectedValue.ToString();
                string  aa = ddlsec.SelectedValue;
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
                    item = item+"";
                }
                else
                {
                    item = item + "--" + item_ref;
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

                //check po data null
                dtoresult = Process.CheckPR_DetailNull(item,purpose,qty);
                if (dtoresult.sts == true)
                {
                    dt.Rows.Add(item,qty,purpose,level,uom);
                    ViewState["Items"] = dt;
                    dgvitems.DataSource = dt;
                    dgvitems.DataBind();
                    ClearText();
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

        protected void dgvitems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dt = ViewState["Items"] as DataTable;
                dt.Rows[index].Delete();
                ViewState["Items"] = dt;
                dgvitems.DataSource = dt;
                dgvitems.DataBind();
            }
            catch (Exception ex)
            {

                DisplayFailResult(ex.ToString());
            }
        }

        protected void dgvitems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string item = e.Row.Cells[2].Text;
                    foreach (Button button in e.Row.Cells[0].Controls.OfType<Button>())
                    {
                        if (button.CommandName == "Delete")
                        {
                            button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){return false;};";
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
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

        #endregion



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
            
            dtoresult = Process.DisplayItem(prefixText,com);
            if (dtoresult.message != "No result found")
            {
                ItemNames = dtoresult.List_Item;   
            }



            return ItemNames;
        }
        #endregion
    }
}