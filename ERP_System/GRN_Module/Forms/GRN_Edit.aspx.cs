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
    public partial class GRN_Edit : System.Web.UI.Page
    {
        GRN_dto dtoresult = new GRN_dto();
        GRN_bo Process = new GRN_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com, usn, form;

        protected void Page_Load(object sender, EventArgs e)
        {
            usn = (string)Session["usn"];
            com = (string)Session["com"];
            form = "GRN_EDIT";

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
                        DisplayVenCode(com);
                        CreateSaveDetailsTable();
                        CreateDetailsTable();
                        CreateHeaderTable();
                        
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

        #region ClearTemTable

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

        private void DisplayOpenGRN(string grn_date)
        {
            try
            {
                dtoresult = Process.DisplayOpenGRN_GRNDate(grn_date);
                if (dtoresult.dtgrn.Rows.Count > 0)
                {
                    ddls_grn.DataSource = dtoresult.dtgrn;
                    ddls_grn.DataTextField = "grn_no";
                    ddls_grn.DataValueField = "grn_no";
                    ddls_grn.DataBind();
                    ddls_grn.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddls_grn.SelectedIndex = 0;
                }
                else
                {
                    ddls_grn.Items.Clear();
                    ddls_grn.DataSource = null;
                    ddls_grn.DataBind();
                   
                }

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

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

        private void DisplayGRNHeader(string grn)
        {
            try
            {
                dtoresult = Process.DisplayGRNHeader(grn);
                if (dtoresult.dtgrn_hdr.Rows.Count > 0)
                {
                    txtgrn.Value = dtoresult.dtgrn_hdr.Rows[0]["grn_no"].ToString();
                    txtgrndate.Text = dtoresult.dtgrn_hdr.Rows[0]["grn_date"].ToString();
                    txtrmk.Value = dtoresult.dtgrn_hdr.Rows[0]["rmk"].ToString();
                    txtdo.Value = dtoresult.dtgrn_hdr.Rows[0]["do_no"].ToString();
                    ddlven.SelectedIndex = ddlven.Items.IndexOf(ddlven.Items.FindByValue(dtoresult.dtgrn_hdr.Rows[0]["vendor"].ToString()));
                    DisplayPO_Ven(ddlven.SelectedItem.Value);

                }
                else
                {
                    ResetHeader();
                    ResetGridview();
                    ResetTemTableGridview();
                    DisplayFailResult("No data found.");
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        private void DisplayGRNPO(string grn)
        {
            try
            {
                dtoresult = Process.DisplayGRN_PODetails(grn);
                if (dtoresult.dtpo_info.Rows.Count > 0)
                {
                    dgvheader.DataSource = dtoresult.dtpo_info;
                    dgvheader.DataBind();
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

        private void DisplayGRNPO_Items(string grn)
        {
            try
            {
                dtoresult = Process.DisplayGRN_POItemDetails(grn);
                if (dtoresult.dtpo_details.Rows.Count > 0)
                {
                    dgvitems.DataSource = dtoresult.dtpo_details;
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

        private void DisplayFailResult_Items(string message)
        {
            resulttimer2.Enabled = true;
            lblsaveresult2.Text = message;
            //lblresult.Text = message;
            lblsaveresult2.ForeColor = System.Drawing.Color.Red;
            // lblresult.ForeColor = System.Drawing.Color.Red;
        }

        private void DisplayPassResult_Items(string message)
        {
            resulttimer2.Enabled = true;
            lblsaveresult2.Text = message;
            // lblresult.Text = message;
            lblsaveresult2.ForeColor = System.Drawing.Color.Blue;
            // lblresult.ForeColor = System.Drawing.Color.Blue;
        }

        #endregion

        #region Reset Data

        private void ResetHeader()
        {
            txtgrn.Value = "";
            txtgrndate.Text = "";
            txtdo.Value = "";
            txtrmk.Value = "";
            txtsearch_date.Text = "";
            //ddlpo.Items.Clear();
            ddlpo.DataSource = null;
            ddlpo.DataBind();
            DisplayVenCode(com);
        }

        private void ResetGridview()
        {
            dgvheader.DataSource = null;
            dgvheader.DataBind();

            dgvitems.DataSource = null;
            dgvitems.DataBind();
        }

        private void ResetTemTableGridview()
        {
            CreateHeaderTable();
            CreateDetailsTable();
            CreateSaveDetailsTable();
            dgvadd_header.DataSource = null;
            dgvadd_header.DataBind();
            dgvadd_details.DataSource = null;
            dgvadd_details.DataBind();
        }

        #endregion
     
        protected void txtsearch_date_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string grn_date;
                grn_date = txtsearch_date.Text;
                DisplayOpenGRN(grn_date);
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

        protected void ddlpo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtheader = (DataTable)ViewState["dtheader"];
                //int count = dtheader.Rows.Count;
                DataTable dtdetails = (DataTable)ViewState["dtdetails"];

                string c_po, po_no, ctlno, desc, exdesc, uom, o_qty, bal_qty, order_date, add_usn, ven_type;
                po_no = ddlpo.SelectedItem.Value;
                if (string.IsNullOrEmpty(po_no))
                {
                    DisplayFailResult_Items("Please select PO number first.");
                    return;
                }
                //check duplicate item

                foreach (GridViewRow row in dgvadd_header.Rows)
                {

                    c_po = row.Cells[1].Text;
                    if (po_no == c_po)
                    {
                        DisplayFailResult_Items("This " + po_no + " already existed.");
                        return;
                    }
                }

                //display po header
                dtoresult = Process.DisplayPOHeader(po_no);
                if (dtoresult.dtpo_info.Rows.Count > 0)
                {
                    order_date = dtoresult.dtpo_info.Rows[0]["order_date"].ToString();
                    add_usn = dtoresult.dtpo_info.Rows[0]["add_usn"].ToString();
                    ven_type = dtoresult.dtpo_info.Rows[0]["ven_type"].ToString();
                    dtheader.Rows.Add(po_no, order_date, add_usn, ven_type);
                    ViewState["dtheader"] = dtheader;
                    dgvadd_header.DataSource = dtheader;
                    dgvadd_header.DataBind();
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

                    dgvadd_details.DataSource = dtdetails;
                    dgvadd_details.DataBind();
                }
                else
                {
                    dgvadd_details.DataSource = null;
                    dgvadd_details.DataBind();
                }

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
                //update
                string grn_no,po_no,catalog,rec_qty,bal_qty,o_qty;
                grn_no = txtgrn.Value;
                GridViewRow row = dgvitems.SelectedRow;
                TextBox txtrecqty = row.FindControl("txtrecqty") as TextBox;
                rec_qty = txtrecqty.Text;
                po_no = row.Cells[1].Text;
                catalog = row.Cells[2].Text;
                o_qty = row.Cells[6].Text;
           
                bal_qty= row.Cells[8].Text;
                dtoresult = Process.UpdateGRNRecQty(grn_no, po_no, catalog, rec_qty, bal_qty, o_qty);
                if (dtoresult.sts == true)
                {
                    DisplayPassResult("Update received quantity successfull.");

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

        protected void btnclose_Click(object sender, EventArgs e)
        {
            try
            {
            
                ResetTemTableGridview();

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
                string recqty, po_no, ctl_no, desc, exdesc, uom, o_qty, bal_qty;

                string grn_no;
                grn_no = txtgrn.Value;
                //save grn details gridview data in temporary table
                DataTable dtsave = (DataTable)ViewState["dtsave"];
                foreach (GridViewRow row in dgvadd_details.Rows)
                {
                    //Finding textbox control  
                    TextBox txtrecqty = row.FindControl("txtrecqty") as TextBox;
                    recqty = txtrecqty.Text;
                    po_no = row.Cells[0].Text;
                    ctl_no = row.Cells[1].Text;
                    desc = row.Cells[2].Text;
                    exdesc = row.Cells[3].Text;
                    uom = row.Cells[4].Text;
                    o_qty = row.Cells[5].Text;
                    bal_qty = row.Cells[7].Text;
                    //check receive qty
                    dtoresult = Process.CheckReceiveQty(recqty, bal_qty, o_qty);
                    if (dtoresult.sts == false)
                    {
                        DisplayFailResult_Items(dtoresult.Message);
                        CreateSaveDetailsTable();
                        return;
                    }
                    // check same po
                    dtoresult = Process.CheckDuplicatePO(grn_no, po_no, ctl_no);
                    if (dtoresult.sts == false)
                    {
                        DisplayFailResult_Items(dtoresult.Message);
                        CreateSaveDetailsTable();
                        return;
                    }
                    dtsave.Rows.Add(po_no, ctl_no, desc, exdesc, uom, o_qty, recqty);
                }            

                //insert all data
                dtoresult = Process.EditGRN_AddNewPO(grn_no,usn,dtsave);
                if (dtoresult.sts == true)
                {
              
                    DisplayPassResult_Items("Add new po successful.");
                    DisplayGRNPO(grn_no);
                    DisplayGRNPO_Items(grn_no);
                    ResetTemTableGridview();
                }
                else
                {
                    DisplayFailResult_Items(dtoresult.Message);
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

        protected void dgvadd_header_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                dgvadd_header.DataSource = dtheader;
                dgvadd_header.DataBind();

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

                int count, indexcount;
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
                        indexcount = indexcount - 1;
                        dtdetails.Rows[indexcount].Delete();
                    }

                }

                ViewState["dtdetails"] = dtdetails;
                dgvadd_details.DataSource = dtdetails;
                dgvadd_details.DataBind();
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
                string grn_no, po;
                grn_no = txtgrn.Value;
                TableCell po_cell = dgvheader.Rows[e.RowIndex].Cells[1];
                //int index = Convert.ToInt32(e.RowIndex);
                po = po_cell.Text;
                //delete selected po
                dtoresult = Process.DeleteSelectedGRN_PO(grn_no, po);
                if (dtoresult.sts == true)
                {
                    DisplayPassResult("Delete data successfull.");
                    DisplayGRNPO(grn_no);
                    DisplayGRNPO_Items(grn_no);
                }
            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void ddls_grn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string grn;
                grn = ddls_grn.SelectedItem.Value;
                DisplayGRNHeader(grn);
                DisplayGRNPO(grn);
                DisplayGRNPO_Items(grn);
                ResetTemTableGridview();

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
                string grn_no,grn_date, vendor, do_no, rmk;
                rmk = txtrmk.Value;
                do_no = txtdo.Value;
                grn_date = txtgrndate.Text;
                if (ddlven.Items.Count == 0)
                {
                    vendor = "";
                }
                else
                {
                    vendor = ddlven.SelectedItem.Value;
                }
                grn_no = txtgrn.Value;
                dtoresult = Process.UpdateGRNHeader(grn_no, grn_date, vendor, do_no, rmk, usn);
                if (dtoresult.sts == true)
                {
                    DisplayPassResult("Update GRN header successfull.");
                    ResetHeader();
                    
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

     

        protected void ddlven_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string ven;
                ven = ddlven.SelectedItem.Value;
                DisplayPO_Ven(ven);

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

   


    }
}