using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ERP_System.PR_Module.PR_Control;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.IO;
using ClosedXML.Excel;
using System.Globalization;
using ERP_System.PR_Module.Models;

namespace ERP_System.PR_Module.Forms
{
    public partial class PR_UPDATE_PO : System.Web.UI.Page
    {

        PR_dto dtoresult = new PR_dto();
        PR_bo Process = new PR_bo();

        Login_dto Log_result = new Login_dto();
        Login_bo Log_Process = new Login_bo();

        string com, usn, form, dpt, nm;

        protected void Page_Load(object sender, EventArgs e)
        {
            usn = (string)Session["usn"];
            com = (string)Session["com"];
            dpt = (string)Session["dpt"];
            nm = (string)Session["nm"];
            form = "PR_UPDATE_PO";

            if (!IsPostBack)
            {
                DisplayDepartment(com);
                //check permission
                CheckPermission(usn, form);

            }
        }

        #region Form Permission

        private void CheckPermission(string usn, string form)
        {
            try
            {
                string requestor, req_dpt, dt;

                requestor = txtnm.Text;
                dt = txtreq_date.Text;
                req_dpt = (ddlr_dpt.Items.Count > 0 ? "" : ddlr_dpt.SelectedItem.Value); //txtreq_dpt.Text;
                // Louis Added on 20200817
                string req_item = txtreq_items.Text;
                // End
                // Louis Added on 20201124
                string req_prno = txtprno.Text;
                string appDate = txtapprdate.Text;
                // End
                // Louis Added on 20201231 
                string appDateTo = txtapprdateTo.Text;
                // End
                // Louis Added on 20211214
                string poNo = txtpono.Text;
                //end
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

                        //DisplayBrandCode(com);
                        //DisplayMasterCatalog(com);
                        //DisplayCustCode(com);
                        //CreateHeaderTable();
                        //CreateDetailsTable();
                        DisplayApprovedPR(req_dpt, requestor, dt, req_item, req_prno, appDate, appDateTo, poNo);
                        CreateUpdatePOTable();

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

        #region Create Temporary Table 

        private void CreateUpdatePOTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("rn"), new DataColumn("id"), new DataColumn("po_no") });
                ViewState["dtitem"] = dt;

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }



        #endregion

        #region DisplayData

        private void DisplayApprovedPR(string req_dpt, string requestor, string dt, string req_items, string req_prno, string appdate, string appDateTo, string pono)
        {
            try
            {

                dtoresult = Process.SearchApprovedPR(req_dpt, dt, requestor, req_items, req_prno, appdate, appDateTo, pono);
                if (dtoresult.dtitem.Rows.Count > 0)
                {
                    dgvheader.DataSource = dtoresult.dtitem;
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

        // Louis Added on 20200818
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
                    ddlr_dpt.Items.Insert(0, new ListItem("All Department", ""));
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
        // End

        #endregion

        protected void cbox_all_CheckedChanged(object sender, EventArgs e)
        {
            try
            {


                if (cbox_all.Checked == true)
                {

                    for (int i = 0; i < dgvheader.Rows.Count; i++)
                    {

                        ((CheckBox)dgvheader.Rows[i].FindControl("cbox_select")).Checked = true;
                    }
                }
                else
                {
                    for (int i = 0; i < dgvheader.Rows.Count; i++)
                    {

                        ((CheckBox)dgvheader.Rows[i].FindControl("cbox_select")).Checked = false;
                    }
                }

            }
            catch (Exception ex)
            {
                DisplayFailResult(ex.ToString());
            }
        }

        protected void dgvheader_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvheader.PageIndex = e.NewPageIndex;
            string req_dpt, nm, dt;
            req_dpt = ddlr_dpt.SelectedItem.Value; //txtreq_dpt.Text;
            nm = txtnm.Text;
            dt = txtreq_date.Text;

            // Louis Added on 20200817
            string req_item = txtreq_items.Text;
            // Louis Added on 20201124
            string req_prno = txtprno.Text;
            string appDate = txtapprdate.Text;

            // Louis Added on 20201231 
            string appDateTo = txtapprdateTo.Text;
            // End

            // Louis Added on 20211214
            string poNo = txtpono.Text;
            //end

            DisplayApprovedPR(req_dpt, nm, dt, req_item, req_prno, appDate, appDateTo,poNo);

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string req_dpt, nm, dt;

                req_dpt = ddlr_dpt.SelectedItem.Value; //txtreq_dpt.Text;
                nm = txtnm.Text;
                dt = txtreq_date.Text;
                // Louis Added on 20200817
                string req_item = txtreq_items.Text;
                // End
                // Louis Added on 20201124
                string req_prno = txtprno.Text;
                string appDate = txtapprdate.Text;
                //

                // Louis Added on 20201231 
                string appDateTo = txtapprdateTo.Text;
                // End

                // Louis Added on 20211214
                string poNo = txtpono.Text;
                //end

                DisplayApprovedPR(req_dpt, nm, dt, req_item, req_prno, appDate,appDateTo, poNo);
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
                string id, rn, requestor, req_dpt, dt, ex_po, po, rmk, price_rmk, gr_rmk;
                //string t_rmk, tprice_rmk, tgr_rmk,sts;
                rn = "";
                rmk = txtrmk.Text;
                price_rmk = txtprice_rmk.Text;
                gr_rmk = txtgr_rmk.Text;
                requestor = txtnm.Text;
                dt = txtreq_date.Text;
                req_dpt = ddlr_dpt.SelectedItem.Value;//txtreq_dpt.Text;
                po = txtpo.Text;

                // Louis Added on 20200817
                string req_item = txtreq_items.Text;
                // End

                // Louis Added on 20201124
                string req_prno = txtprno.Text;
                string appDate = txtapprdate.Text;
                // End

                // Louis Added on 20201231 
                string appDateTo = txtapprdateTo.Text;
                // End

                // Louis Added on 20211214
                string poNo = txtpono.Text;
                //end

                DataTable dtdtl = (DataTable)ViewState["dtitem"];

                dtdtl.Rows.Clear();
                dtdtl.AcceptChanges();
                ViewState.Remove("dtitem");
                ViewState["dtitem"] = dtdtl;

                foreach (GridViewRow row in dgvheader.Rows)
                {
                    id = row.Cells[1].Text;
                    rn = row.Cells[5].Text;
                    ex_po = row.Cells[14].Text;
                    //rmk = row.Cells[11].Text;
                    //rmk = row.Cells[11].Text;
                    //  po = row.Cells[].Text;
                    CheckBox cbox_select = row.FindControl("cbox_select") as CheckBox;
                    if (cbox_select.Checked == true)
                    {
                        dtdtl.Rows.Add(rn, id, ex_po);
                    }

                }
                //var dtdtl1 = dtdtl;
                dtoresult = Process.UpdatePR_PO(dtdtl, po, usn, rmk, price_rmk, gr_rmk);
                if (dtoresult.sts == true)
                {
                    DisplayApprovedPR(req_dpt, requestor, dt, req_item, req_prno, appDate,appDateTo, poNo);
                    DisplayPassResult("Update PO successfull.");

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

        protected void btnExportExten_Click(object sender, EventArgs e)
        {
            var poDatas = new List<POModel>();
        
            DataTable dtdtl = (DataTable)ViewState["dtitem"];

            dtdtl.Rows.Clear();
            dtdtl.AcceptChanges();
            ViewState.Remove("dtitem");
            ViewState["dtitem"] = dtdtl;

            foreach (GridViewRow row in dgvheader.Rows)
            {
                CheckBox cbox_select = row.FindControl("cbox_select") as CheckBox;
                if (cbox_select.Checked == true)
                {
                    poDatas.Add(new POModel()
                    {
                        ID = Convert.ToInt32(row.Cells[1].Text),
                        ApproveDt = row.Cells[3].Text,
                        Level = row.Cells[4].Text,
                        PRNo = row.Cells[5].Text,
                        Requestor = row.Cells[6].Text,
                        Section = row.Cells[7].Text,
                        RequestDate = row.Cells[8].Text,
                        Company = row.Cells[9].Text,
                        ItemName = row.Cells[10].Text,
                        Quantity = row.Cells[11].Text,
                        UOM = row.Cells[12].Text,
                        PRemark = row.Cells[13].Text,
                        PONo = row.Cells[14].Text,
                        GRNNo = row.Cells[15].Text,
                        PurchasingRemark = row.Cells[16].Text,
                        PriceRemark = row.Cells[17].Text,
                        GoodsReceivedRemark = row.Cells[18].Text,
                        ApproveName = row.Cells[19].Text
                    });
                }
            }

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("PO Update");
            workSheet.TabColor = System.Drawing.Color.Black;
            //workSheet.DefaultRowHeight = 12;

            // Assign borders
            workSheet.Cells[1, 1, 1, 16].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[1, 1, 1, 16].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[1, 1, 1, 16].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[1, 1, 1, 16].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            //Header of table  
            //  
            workSheet.Row(1).Height = 33;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;
            workSheet.Row(1).Style.WrapText = true;


            workSheet.Cells[1, 1].Value = "ID";
            workSheet.Cells[1, 2].Value = "Approval" + Environment.NewLine + "Date";
            workSheet.Cells[1, 3].Value = "Level";
            workSheet.Cells[1, 4].Value = "PR No";
            workSheet.Cells[1, 5].Value = "Requestor";
            workSheet.Cells[1, 6].Value = "Section";
            workSheet.Cells[1, 7].Value = "Request" + Environment.NewLine + "Date";
            workSheet.Cells[1, 8].Value = "Co.";
            workSheet.Cells[1, 9].Value = "Approval" + Environment.NewLine + "Name";
            workSheet.Cells[1, 10].Value = "Item Name";
            workSheet.Cells[1, 11].Value = "Qty";
            workSheet.Cells[1, 12].Value = "UOM";
            workSheet.Cells[1, 13].Value = "Purpose/Remark";
            workSheet.Cells[1, 14].Value = "PO No";
            workSheet.Cells[1, 15].Value = "Purchasing" + Environment.NewLine + "Remark";
            workSheet.Cells[1, 16].Value = "Price" + Environment.NewLine + "Remark";
            
            //Body of table  
            //  
            int recordIndex = 2;
            // Extension Log 
            var exportLogs = new List<ExportLogModel>();
            var dt = DateTime.Now;
            // End Log
            foreach (var poData in poDatas)
            {
                // Extension
                exportLogs.Add(new ExportLogModel()
                {
                    ItemId = poData.ID,
                    ExportDate = dt,
                    Username = nm
                });

                workSheet.Cells[recordIndex, 1].Value = poData.ID.ToString("0");
                workSheet.Cells[recordIndex, 2].Value = poData.ApproveDt;
                workSheet.Cells[recordIndex, 3].Value = poData.Level;
                workSheet.Cells[recordIndex, 4].Value = poData.PRNo;
                workSheet.Cells[recordIndex, 5].Value = poData.Requestor;
                workSheet.Cells[recordIndex, 6].Value = poData.Section;
                workSheet.Cells[recordIndex, 7].Value = poData.RequestDate;
                workSheet.Cells[recordIndex, 8].Value = poData.Company;
                workSheet.Cells[recordIndex, 9].Value = poData.ApproveName;
                //workSheet.Cells[recordIndex, 10].Style.Numberformat.Format = @"_-* #,##0.00\ ""€""_-;\-* #,##0.00\ ""€""_-;_-* ""-""??\ ""€""_-;_-@_-";
                workSheet.Cells[recordIndex, 10].Value = HttpUtility.HtmlDecode(poData.ItemName);
                workSheet.Cells[recordIndex, 11].Value = poData.Quantity;
                workSheet.Cells[recordIndex, 12].Value = poData.UOM;
                workSheet.Cells[recordIndex, 13].Value = HttpUtility.HtmlDecode(poData.PRemark);
                workSheet.Cells[recordIndex, 14].Value = poData.PONo;
                workSheet.Cells[recordIndex, 15].Value = HttpUtility.HtmlDecode(poData.PurchasingRemark);
                workSheet.Cells[recordIndex, 16].Value = HttpUtility.HtmlDecode(poData.PriceRemark);
                recordIndex++;
            }
            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();
            workSheet.Column(5).AutoFit();
            workSheet.Column(6).AutoFit();
            workSheet.Column(7).AutoFit();
            workSheet.Column(8).AutoFit();
            workSheet.Column(9).AutoFit();
            workSheet.Column(10).Style.WrapText = true;// Item Name
            workSheet.Column(10).Width = 25;
            workSheet.Column(11).AutoFit();
            workSheet.Column(12).AutoFit();
            workSheet.Column(13).Width = 25;
            workSheet.Column(13).Style.WrapText = true;// Propose Remark
            workSheet.Column(14).AutoFit();
            workSheet.Column(14).Style.WrapText = true; // Purchasing Remark
            workSheet.Column(15).Width = 25;
            workSheet.Column(15).Style.WrapText = true; // Price Remark
            workSheet.Column(16).AutoFit();
            workSheet.Column(16).Style.WrapText = true;

            // ws.Cells[Rowstart, ColStart, RowEnd, ColEnd]
            workSheet.Cells[2, 1, recordIndex, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Alignment is center
            workSheet.Cells[2, 1, recordIndex, 16].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            // Assign borders
            workSheet.Cells[2, 1, recordIndex, 16].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[2, 1, recordIndex, 16].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[2, 1, recordIndex, 16].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[2, 1, recordIndex, 16].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Insert to Logs 
            try
            {
                dtoresult = Process.InsertExportLogs(exportLogs);
            }
            catch (Exception ex)
            {
            }

            string excelName = DateTime.Now.ToString("ddMMyyyyhhmmss") + " POUpdateReport by " + nm.Trim();
            //var excelDirPath = System.Configuration.ConfigurationManager.AppSettings["System:CustomizeDirExcelPath"].ToString();
            //excel.SaveAs(new FileInfo(@"" + excelDirPath + excelName + ".xlsx"));
            using (var memoryStream = new MemoryStream())
            {
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                HttpContext.Current.Response.End();
            }            
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

            //btnExportExten.PerformClick()

            //string id, rn, requestor, req_dpt, dt, ex_po, po, rmk, price_rmk, gr_rmk;
            //DataTable dtdtl = (DataTable)ViewState["dtitem"];

            //dtdtl.Rows.Clear();
            //dtdtl.AcceptChanges();
            //ViewState.Remove("dtitem");
            //ViewState["dtitem"] = dtdtl;

            //foreach (GridViewRow row in dgvheader.Rows)
            //{
            //    id = row.Cells[1].Text;
            //    rn = row.Cells[2].Text;
            //    ex_po = row.Cells[10].Text;
            //    //rmk = row.Cells[11].Text;
            //    //rmk = row.Cells[11].Text;
            //    //  po = row.Cells[].Text;
            //    CheckBox cbox_select = row.FindControl("cbox_select") as CheckBox;
            //    if (cbox_select.Checked == true)
            //    {
            //        dtdtl.Rows.Add(rn, id, ex_po);
            //    }
            //}

            //ExcelExport(dtdtl);


            //        var students = new[]
            //{
            //    new {
            //        Id = "101", Name = "Vivek", Address = "Hyderabad"
            //    },
            //    new {
            //        Id = "102", Name = "Ranjeet", Address = "Hyderabad"
            //    },
            //    new {
            //        Id = "103", Name = "Sharath", Address = "Hyderabad"
            //    },
            //    new {
            //        Id = "104", Name = "Ganesh", Address = "Hyderabad"
            //    },
            //    new {
            //        Id = "105", Name = "Gajanan", Address = "Hyderabad"
            //    },
            //    new {
            //        Id = "106", Name = "Ashish", Address = "Hyderabad"
            //    }
            //};
            //        ExcelPackage excel = new ExcelPackage();
            //        var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            //        workSheet.TabColor = System.Drawing.Color.Black;
            //        workSheet.DefaultRowHeight = 12;
            //        //Header of table  
            //        //  
            //        workSheet.Row(1).Height = 20;
            //        workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //        workSheet.Row(1).Style.Font.Bold = true;
            //        workSheet.Cells[1, 1].Value = "S.No";
            //        workSheet.Cells[1, 2].Value = "Id";
            //        workSheet.Cells[1, 3].Value = "Name";
            //        workSheet.Cells[1, 4].Value = "Address";
            //        //Body of table  
            //        //  
            //        int recordIndex = 2;
            //        foreach (var student in students)
            //        {
            //            workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
            //            workSheet.Cells[recordIndex, 2].Value = student.Id;
            //            workSheet.Cells[recordIndex, 3].Value = student.Name;
            //            workSheet.Cells[recordIndex, 4].Value = student.Address;
            //            recordIndex++;
            //        }
            //        workSheet.Column(1).AutoFit();
            //        workSheet.Column(2).AutoFit();
            //        workSheet.Column(3).AutoFit();
            //        workSheet.Column(4).AutoFit();
            //        string excelName = "studentsRecord";
            //        using (var memoryStream = new MemoryStream())
            //        {
            //            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
            //            excel.SaveAs(memoryStream);
            //            memoryStream.WriteTo(Response.OutputStream);
            //            HttpContext.Current.Response.End();
            //        }



            //XLWorkbook wb = new XLWorkbook();

            //wb.Worksheets.Add(dtdtl, "WorksheetName");

        }

        protected void resulttimer_Tick(object sender, EventArgs e)
        {
            lblsaveresult.Text = "";
            resulttimer.Enabled = false;
        }


        #region Export Data to Excel

        private void ExcelExport(DataTable table)
        {
            using (ExcelPackage packge = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = packge.Workbook.Worksheets.Add("Demo");

                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells["A1"].LoadFromDataTable(table, true);

                //Format the header for column 1-3
                using (ExcelRange range = ws.Cells["A1:C1"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189)); //Set color to dark blue
                    range.Style.Font.Color.SetColor(Color.White);
                }

                ////Example how to Format Column 1 as numeric 
                //using (ExcelRange col = ws.Cells[2, 1, 2 + table.Rows.Count, 1])
                //{
                //    col.Style.Numberformat.Format = "#,##0.00";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}

                ////Write it back to the client
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment;  filename=ExcelExport.xlsx");
                //Response.BinaryWrite(packge.GetAsByteArray());

                //packge.Workbook.Properties.Title = "Attempts";
                //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //this.Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", "ExcellData.xlsx"));
                //this.Response.BinaryWrite(packge.GetAsByteArray());

                //Response.Clear();
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                //Response.AddHeader("Content-Disposition", "attachment; filename=ProposalRequest-" + "DataReport" + ".xslx");
                //Response.BinaryWrite(packge.GetAsByteArray());
                //// myMemoryStream.WriteTo(Response.OutputStream); //works too
                //Response.Flush();
                //Response.Close();

                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + "Datas" + ".xlsx");
                HttpContext.Current.Response.BinaryWrite(packge.GetAsByteArray());
                HttpContext.Current.Response.End();
            }
        }

        class POModel
        {
            public int ID { get; set; }
            public string PRNo { get; set; }
            public string Requestor { get; set; }
            public string Section { get; set; }
            public string RequestDate { get; set; }
            public string Company { get; set; }
            public string ItemName { get; set; }
            public string Quantity { get; set; }
            public string PRemark { get; set; }
            public string Level { get; set; }
            public string PONo { get; set; }
            public string GRNNo { get; set; }
            public string PurchasingRemark {get;set;}
            public string PriceRemark { get; set; }
            public string GoodsReceivedRemark { get; set; }
            public string ApproveName { get; set; }
            public string UOM { get; set; }
            public string ApproveDt { get; set; }
        };

        #endregion
    }
}