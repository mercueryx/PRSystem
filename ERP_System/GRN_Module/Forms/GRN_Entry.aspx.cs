using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERP_System.GRN_Module.GRN_Control;
using System.Data;
namespace ERP_System.GRN_Module.Forms
{
    public partial class GRN_Entry : System.Web.UI.Page
    {
        GRN_dto dtoresult = new GRN_dto();
        GRN_bo Process = new GRN_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com,usn,form;
        protected void Page_Load(object sender, EventArgs e)
        {
            form = "GRN_ENTRY";
            usn = (string)Session["usn"];
            com = (string)Session["com"];
     
            if (!IsPostBack)
            {
                //check permission
                CheckPermission(usn, form);
                //DisplayVenCode(com);
                //CreateDetailsTable();
                //CreateHeaderTable();
                //CreateSaveDetailsTable();

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
                        DisplayVenCode(com);
                        CreateDetailsTable();
                        CreateHeaderTable();
                        CreateSaveDetailsTable();
                    
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

        #region ClearData
        private void ClearGRNData()
        {
            txtgrn.Value = "";
            txtdo.Value = "";
            txtgrndate.Text = "";
            txtrmk.Value = "";
            DisplayVenCode(com);
            //ddlpo.Items.Clear();
            ddlpo.DataSource = null;
            ddlpo.DataBind();
            CreateDetailsTable();
            CreateHeaderTable();
            CreateSaveDetailsTable();
            dgvheader.DataSource = null;
            dgvheader.DataBind();
            dgvitems.DataSource = null;
            dgvitems.DataBind();
        }
        #endregion

        #region Create Tem Table

        private void CreateDetailsTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[7] { new DataColumn("po_no"), new DataColumn("ctlno"), new DataColumn("desc"), new DataColumn("exdesc"), new DataColumn("uom"), new DataColumn("o_qty"), new DataColumn("bal_qty") });
                ViewState["dtdetails"] = dt;
                //rptr1.DataSource = dt;
                //rptr1.DataBind();
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }

        }

        private void CreateHeaderTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[4] { new DataColumn("po_no"), new DataColumn("order_date"), new DataColumn("add_usn"), new DataColumn("ven_type") });
                ViewState["dtheader"] = dt;
                //rptr1.DataSource = dt;
                //rptr1.DataBind();
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }

        }

        private void CreateSaveDetailsTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[7] { new DataColumn("po_no"), new DataColumn("ctlno"), new DataColumn("desc"), new DataColumn("exdesc"), new DataColumn("uom"), new DataColumn("o_qty"), new DataColumn("rec_qty") });
                ViewState["dtsave"] = dt;
                //rptr1.DataSource = dt;
                //rptr1.DataBind();
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }

        }

        #endregion

        #region Display Data

        private void DisplayVenCode(string com)
        {
            try
            {
                dtoresult = Process.DisplayApprovePOVendor(com);
                if (dtoresult.dtven.Rows.Count > 0)
                {
                    ddlven.DataSource = dtoresult.dtven;
                    ddlven.DataTextField = "description";
                    ddlven.DataValueField = "ven_code";
                    ddlven.DataBind();
                    ddlven.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlven.SelectedIndex = 0;
                }
                else
                {
                    ddlven.Items.Clear();
                    ddlven.DataSource = null;
                    ddlven.DataBind();

                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayPO_Ven(string ven)
        {
            try
            {
                dtoresult = Process.DisplayApprovePO_VenCode(ven);
                if (dtoresult.dtpo.Rows.Count > 0)
                {
                    ddlpo.DataSource = dtoresult.dtpo;
                    ddlpo.DataTextField = "po_no";
                    ddlpo.DataValueField = "po_no";
                    ddlpo.DataBind();
                    ddlpo.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlpo.SelectedIndex = 0;
                }
                else
                {
                    ddlpo.Items.Clear();
                    ddlpo.DataSource = null;
                    ddlpo.DataBind();

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

        protected void ddlven_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string ven_code;
                ven_code = ddlven.SelectedItem.Value;
                DisplayPO_Ven(ven_code);
                CreateDetailsTable();
                CreateHeaderTable();
                CreateSaveDetailsTable();
                dgvheader.DataSource = null;
                dgvheader.DataBind();
                dgvitems.DataSource = null;
                dgvitems.DataBind();

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void ddlpo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtheader = (DataTable)ViewState["dtheader"];
                //int count = dtheader.Rows.Count;
                DataTable dtdetails = (DataTable)ViewState["dtdetails"];

                string c_po, po_no, ctlno, desc, exdesc, uom, o_qty, bal_qty,order_date,add_usn,ven_type;
                po_no = ddlpo.SelectedItem.Value;

                if (string.IsNullOrEmpty(po_no))
                {
                    DisplayFailResult("Please select PO number first.");
                    return;
                }
                //check duplicate item
             
                foreach (GridViewRow row in dgvheader.Rows)
                {
                  
                    c_po = row.Cells[1].Text;
                    if (po_no == c_po)
                    {
                        DisplayFailResult("This "+po_no+" already existed.");
                        return;
                    }
                }
              
                //display po header
                dtoresult = Process.DisplayPOHeader(po_no);
                if (dtoresult.dtpo_info.Rows.Count > 0)
                {
                    order_date = dtoresult.dtpo_info.Rows[0]["order_date"].ToString();
                    add_usn= dtoresult.dtpo_info.Rows[0]["add_usn"].ToString();
                    ven_type = dtoresult.dtpo_info.Rows[0]["ven_type"].ToString();
                    dtheader.Rows.Add(po_no, order_date, add_usn,ven_type);
                    ViewState["dtheader"] = dtheader;
                    dgvheader.DataSource = dtheader;
                    dgvheader.DataBind();
                }

              
                
                //display po details
                dtoresult = Process.DisplayPODetails(po_no);
                if (dtoresult.dtpo_details.Rows.Count > 0)
                {
                    for (int i = 0; i < dtoresult.dtpo_details.Rows.Count; i++)
                    {
                        ctlno = dtoresult.dtpo_details.Rows[i]["ctlno"].ToString();
                        desc = dtoresult.dtpo_details.Rows[i]["desc"].ToString();
                        exdesc = dtoresult.dtpo_details.Rows[i]["exdesc"].ToString();
                        uom = dtoresult.dtpo_details.Rows[i]["uom"].ToString();
                        o_qty = dtoresult.dtpo_details.Rows[i]["o_qty"].ToString();
                        bal_qty = dtoresult.dtpo_details.Rows[i]["bal_qty"].ToString();

                        dtdetails.Rows.Add(po_no, ctlno, desc, exdesc, uom, o_qty, bal_qty);

                    }
                    ViewState["dtdetails"] = dtdetails;
                    dgvitems.DataSource = dtdetails;
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
                string po_no;     
                List<string> indexlist = new List<string>();
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dtheader = ViewState["dtheader"] as DataTable;
                po_no = dtheader.Rows[index]["po_no"].ToString();
                dtheader.Rows[index].Delete();
                ViewState["dtheader"] = dtheader;
                dgvheader.DataSource = dtheader;
                dgvheader.DataBind();

                //delete details
                string del_po;
                DataTable dtdetails = ViewState["dtdetails"] as DataTable;
                for (int i = 0; i < dtdetails.Rows.Count; i++)
                {
                    del_po = dtdetails.Rows[i]["po_no"].ToString();
                    if (del_po == po_no)
                    {
                       //dtdetails.Rows[i].Delete();
                        indexlist.Add(i.ToString());
                    }                
                }

                int count,indexcount;
                count = 0;
                indexcount = 0;
                foreach (string value in indexlist)
                {
                    indexcount = Convert.ToInt32(value);
                    if (count == 0)
                    {
                        
                        dtdetails.Rows[indexcount].Delete();
                        count += 1;
                    }
                    else
                    {
                        indexcount = indexcount- 1;
                        dtdetails.Rows[indexcount].Delete();
                    }
                  
                }

                ViewState["dtdetails"] = dtdetails;
                dgvitems.DataSource = dtdetails;
                dgvitems.DataBind();
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
                string recqty, po_no, ctl_no,desc,exdesc,uom,o_qty,ven_code,grn_date,do_no,rmk,exist_grn,bal_qty;
              
               
                //save grn details gridview data in temporary table
                DataTable dtsave = (DataTable)ViewState["dtsave"];
                foreach (GridViewRow row in dgvitems.Rows)
                {
                    //Finding textbox control  
                    TextBox txtrecqty = row.FindControl("txtrecqty") as TextBox;
                    recqty = txtrecqty.Text;
                    po_no = row.Cells[0].Text;
                    ctl_no = row.Cells[1].Text;
                    desc = row.Cells[2].Text;
                    exdesc= row.Cells[3].Text;
                    uom = row.Cells[4].Text;
                    o_qty= row.Cells[5].Text;
                    bal_qty = row.Cells[7].Text;
                    //check receive qty
                    dtoresult = Process.CheckReceiveQty(recqty, bal_qty,o_qty);
                    if (dtoresult.sts == false)
                    {
                        DisplayFailResult(dtoresult.Message);
                        CreateSaveDetailsTable();
                        return;
                    }

                    dtsave.Rows.Add(po_no,ctl_no,desc,exdesc,uom,o_qty,recqty);
                }

                if (ddlven.Items.Count==0)
                {
                    ven_code = "";
                }
                else
                {
                    ven_code = ddlven.SelectedItem.Value;
                }

                grn_date = txtgrndate.Text;
                do_no = txtdo.Value.ToUpper();
                rmk = txtrmk.Value;
                exist_grn = txtgrn.Value;

                //insert all data
                dtoresult = Process.InsertData(grn_date, ven_code, do_no, rmk, usn, dtsave, exist_grn);
                if (dtoresult.sts == true)
                {
                    txtgrn.Value = dtoresult.grn_no;
                    DisplayPassResult("GRN save successful.");
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

        protected void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                ClearGRNData();
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

      
    }
}