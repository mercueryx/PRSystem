using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ERP_System.PR_Module.Models;

namespace ERP_System.PR_Module.PR_Control
{

    public class PR_bo
    {
        PR_dto dtoresult = new PR_dto();
        PR_da ProcessData = new PR_da();

        #region Login

        public PR_dto DisplayAllUser()
        {
            try
            {
                dtoresult = ProcessData.SelectAllUser();
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region PR Entry

        public PR_dto DisplayCompanyCode()
        {
            try
            {
                dtoresult = ProcessData.SelectAllCompany();
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PR_dto DisplayDepartment(string com)
        {
            try
            {
                dtoresult = ProcessData.SelectDepartment(com);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PR_dto DisplaySection(string com,string dpt)
        {
            try
            {
                dtoresult = ProcessData.SelectSection(com,dpt);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PR_dto DisplayAllUOM()
        {
            try
            {
                dtoresult = ProcessData.SelectAllUOM();
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public PR_dto DisplayUOM_desc( string desc)
        {
            try
            {
                dtoresult = ProcessData.SelectUOM_desc( desc);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PR_dto DisplayItem(string prefix,string com)
        {
            try
            {
                List<string> itemlist = new List<string>();
                //select item from ibas database
                dtoresult = ProcessData.SelectItem_ibas(prefix, com);
                if (dtoresult.dtitem.Rows.Count > 0)
                {

                    for (int i = 0; i < dtoresult.dtitem.Rows.Count; i++)
                    {
                        itemlist.Add(dtoresult.dtitem.Rows[i][0].ToString());
                    }

                }
                else
                {
                    //select item from local database
                    dtoresult = ProcessData.SelectItem_local(prefix,com);
                    if (dtoresult.dtitem.Rows.Count > 0)
                    {

                        for (int i = 0; i < dtoresult.dtitem.Rows.Count; i++)
                        {
                            itemlist.Add(dtoresult.dtitem.Rows[i][0].ToString());
                        }
                    }
                    else
                    {
                        dtoresult.message = "No result found";
                    }
                
                  
                }
                dtoresult.List_Item = itemlist;
                return dtoresult;
            
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PR_dto DisplayRef(string desc, string com)
        {
            try
            {
                dtoresult = ProcessData.SelectItemRef(desc, com);
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PR_dto CheckPR_DetailNull(string item, string purpose, string qty)
        {
            try
            {
              
                if (string.IsNullOrEmpty(qty))
                {
                    dtoresult.sts = false;
                    dtoresult.message = "Quantity cannot empty.";
                    return dtoresult;
                }
              
                if (string.IsNullOrEmpty(item))
                {
                    dtoresult.sts = false;
                    dtoresult.message = "Item name cannot empty.";
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(purpose))
                {
                    dtoresult.sts = false;
                    dtoresult.message = "Purpose cannot empty.";
                    return dtoresult;
                }

                dtoresult.sts = true;
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public PR_dto InsertPR(string com,string req_com,string req_dpt, string dpt,string sec, string nm, string usn,string name, DataTable dtitem,string exist_pr)
        {
            try
            {

                //get running no
                string pr_rn,c_item;
                pr_rn = GetPR_RN();
                //check data null

                if (req_com == "Select Company")
                {
                    dtoresult.message = "Please select company first.";
                    dtoresult.sts = false;
                    return dtoresult;
                }
                if (sec == "Select Section")
                {
                    dtoresult.message = "Please select section first.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                if (sec == "Select Department")
                {
                    dtoresult.message = "Please select department first.";
                    dtoresult.sts = false;
                    return dtoresult;
                }


                if (string.IsNullOrEmpty(req_dpt))
                {
                    dtoresult.message = "Requestor Department cannot empty";
                    dtoresult.sts = false;
                    return dtoresult;
                }
                if (string.IsNullOrEmpty(nm))
                {
                    dtoresult.message = "Requester cannot empty.";
                    ////dtoresult.sts = false;
                    return dtoresult;
                }

                //check if user continue click save button after save
                if (!string.IsNullOrEmpty(exist_pr))
                {
                    dtoresult.message = "Please click new button to submit new PR.";
                    dtoresult.sts = false;
                    return dtoresult;
                }
                if (dtitem.Rows.Count > 0)
                {
                    dtoresult = ProcessData.InsertPR(com, req_com,pr_rn, req_dpt,dpt,sec,nm,usn,name,dtitem);
                    if (dtoresult.sts == true)
                    {
                        for (int i = 0; i < dtitem.Rows.Count; i++)
                        {
                            c_item = dtitem.Rows[i]["item_name"].ToString();
                            dtoresult = ProcessData.SelectExistItem(c_item);
                            if (dtoresult.dtcheck.Rows.Count == 0)
                            {
                                dtoresult = ProcessData.InsertNewItem(c_item, usn);

                            }                      

                        }
                    }
                                       
                    dtoresult.pr_rn = pr_rn;
                }
                else
                {
                    dtoresult.message = "PR details cannot empty.";
                }



                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string GetPR_RN()
        {
            try
            {

                int rn;
                string pr_rn, year, month;
                year = DateTime.Now.ToString("yy");
                month = DateTime.Now.ToString("MM");
                pr_rn = "PR" + year + month;
                dtoresult = ProcessData.SelectPR_RN(pr_rn);
                if (dtoresult.dtrn.Rows.Count > 0)
                {
                    rn = Convert.ToInt32(dtoresult.dtrn.Rows[0]["pr_rn"]);
                    rn = rn + 1;

                }
                else
                {
                    rn = 1;
                }
                pr_rn = pr_rn + rn.ToString("D3");
                return pr_rn;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region PR Edit

        public PR_dto GetPRHeader(string pr_rn)
        {
            try
            {
                dtoresult = ProcessData.SelectPRHeader(pr_rn);
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PR_dto GetPRDetails(string pr_rn)
        {
            try
            {
                dtoresult = ProcessData.SelectPRDetails(pr_rn);
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PR_dto DisplayOpenPR(string requestdate,string usn)
        {
            try
            {
                if (string.IsNullOrEmpty(requestdate))
                {
                    dtoresult = ProcessData.SelectAllOpenPR(usn);
                }
                else
                {
                    dtoresult = ProcessData.SelectOpenPR(usn, requestdate);

                }
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PR_dto EditPRHdr(string req_com,string pr_rn, string nm,string req_dpt,string sec)
        {
            try
            {

                if (req_com == "Select Company")
                {
                    dtoresult.sts = false;
                    dtoresult.message = "Please select company first.";
                    return dtoresult;

                }
                if (string.IsNullOrEmpty(req_dpt))
                {
                    dtoresult.sts = false;
                    dtoresult.message = "Requestor department cannot empty.";
                    return dtoresult;

                }

                if (string.IsNullOrEmpty(sec))
                {
                    dtoresult.sts = false;
                    dtoresult.message = "Requestor section cannot empty.";
                    return dtoresult;

                }

                if (string.IsNullOrEmpty(nm))
                {
                    dtoresult.sts = false;
                    dtoresult.message = "Requestor cannot empty.";
                    return dtoresult;

                }
                dtoresult = ProcessData.UpdatePRHDR(req_com,pr_rn, nm,req_dpt,sec);
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PR_dto AddNewPR_Dtl(string pr_rn, string item, string purpose, string qty,string lvl,string usn,string uom)
        {
            try
            {
                if (string.IsNullOrEmpty(pr_rn))
                {
                    dtoresult.sts = false;
                    dtoresult.message = "Please select a PR to edit.";
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(item))
                {
                    dtoresult.sts = false;
                    dtoresult.message = "Item name cannot empty.";
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(qty))
                {
                    dtoresult.sts = false;
                    dtoresult.message = "Quantity cannot empty.";
                    return dtoresult;
                }

                if (string.IsNullOrEmpty(purpose))
                {
                    dtoresult.sts = false;
                    dtoresult.message = "Purpose cannot empty.";
                    return dtoresult;
                }

                dtoresult = ProcessData.SelectSamePR_Item(pr_rn,item);
                if (dtoresult.dtcheck.Rows.Count > 0)
                {
                    dtoresult.message = "This item already exist in this PR.";
                    dtoresult.sts = false;
                    return dtoresult;
                }

                dtoresult = ProcessData.AddNewPRDtl(pr_rn, item, qty, purpose,lvl, usn,uom);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PR_dto DeletePRDetails(string pr_rn, string id)
        {
            try
            {
                //check left how many po catalog before delete 
                //not allow to delete if there is no more po catalog after delete selected catalog
                dtoresult = ProcessData.SelectCountItemLeft(pr_rn);
                if (dtoresult.dtcheck.Rows.Count > 0)
                {
                    //if (dtoresult.dtcheck.Rows.Count > 1)
                    //{
                        //delete catalog
                        dtoresult = ProcessData.DeletePR_Item(pr_rn, id);

                    //}
                    //else
                    //{
                    //    dtoresult.message = "PR details cannot empty after delete this item.";
                    //    dtoresult.sts = false;

                    //}

                }
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region PR Certify
        public PR_dto DisplayPendingCertifyPR_All(string id,string sec)
        {
            try
            {
                if (sec == "IT")
                {
                    dtoresult = ProcessData.SelectAll_OpenPR_IT();
                }
                else
                {
                    dtoresult = ProcessData.SelectAll_P_C_PR(id);
                }
              
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public PR_dto SearchPendingPR_Keyword(string id, string requestor,string req_dt)
        {
            try
            {
                if (string.IsNullOrEmpty(requestor) && string.IsNullOrEmpty(req_dt))
                {
                    dtoresult = ProcessData.SelectAll_P_C_PR(id);

                }
                else if (string.IsNullOrEmpty(requestor) && !string.IsNullOrEmpty(req_dt))
                {
                    dtoresult = ProcessData.SelectP_C_PR_byRequestDate(id, req_dt);
                }

                else if (string.IsNullOrEmpty(req_dt) && !string.IsNullOrEmpty(requestor))
                {
                    dtoresult = ProcessData.SelectP_C_PR_byRequestor(id, requestor);
                }
                else
                {
                    dtoresult = ProcessData.SelectP_C_PR_byRequestDate_Requestor(id, req_dt, requestor);
                }
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PR_dto SearchPendingPR_Keyword2(string id, string sec , string requestor, string req_dt, string dept)
        {
            try
            {
                if (string.IsNullOrEmpty(requestor) && string.IsNullOrEmpty(req_dt))
                {
                    dtoresult = ProcessData.SelectAll_P_C_PR(id);

                }
                else if (string.IsNullOrEmpty(requestor) && !string.IsNullOrEmpty(req_dt))
                {
                    dtoresult = ProcessData.SelectP_C_PR_byRequestDate(id, req_dt);
                }

                else if (string.IsNullOrEmpty(req_dt) && !string.IsNullOrEmpty(requestor))
                {
                    dtoresult = ProcessData.SelectP_C_PR_byRequestor(id, requestor);
                }
                else
                {
                    dtoresult = ProcessData.SelectP_C_PR_byRequestDate_Requestor(id, req_dt, requestor);
                }
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PR_dto CheckPR_Status(string rn,string id)
        {
            try
            {
                dtoresult = ProcessData.SelectPR_Status(rn,id);
                if (dtoresult.dtcheck.Rows.Count > 0)
                {
                    dtoresult.sts = false;
                }
                else
                {
                    dtoresult.sts = true;
                }
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PR_dto Certify_Cancel_PR(DataTable dtdtl, string usn,string name)
        {
            try
            {
                dtoresult = ProcessData.UpdatePR_sts(dtdtl, usn,name);
                if (dtoresult.sts == true)
                {
                    dtoresult = ProcessData.UpdatePR_Sts();
                 

                }
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region PR Approve

        #region Normal User
     
        public PR_dto SearchCertifiedPR_User(string id, string requestor, string dt)
        {
            try
            {
                if (!string.IsNullOrEmpty(requestor) && string.IsNullOrEmpty(dt))
                {
                    dtoresult = ProcessData.SelectCertified_PR_byRequestor(id, requestor);
                }
                else if (!string.IsNullOrEmpty(dt) && string.IsNullOrEmpty(requestor))
                {
                    dtoresult = ProcessData.SelectCertified_PR_byRequestDate(id, dt);
                }
                else if (!string.IsNullOrEmpty(dt) && !string.IsNullOrEmpty(requestor))
                {
                    dtoresult = ProcessData.SelectCertified_PR_byRequestDate_Requestor(id, dt, requestor);
                }
                else
                {
                    dtoresult = ProcessData.Select_Certified_PR(id);
                }
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region Malu Apa Boss Ku
        //public PR_dto DisplayAllCertifiedPR()
        //{
        //    try
        //    {
        //        dtoresult = ProcessData.SelectAll_Certified_PR();
        //        return dtoresult;
        //    } 
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public PR_dto SearchCertifiedPR_Boss(string requestor, string dt, string dept)
        {
            try
            {
                //if (!string.IsNullOrEmpty(requestor) && string.IsNullOrEmpty(dt))
                //{
                //    dtoresult = ProcessData.SelectAllCertified_PR_byRequestor(requestor);
                //}
                //else if (!string.IsNullOrEmpty(dt) && string.IsNullOrEmpty(requestor))
                //{
                //    dtoresult = ProcessData.SelectAllCertified_PR_byRequestDate(dt);
                //}
                //else if (!string.IsNullOrEmpty(dt) && !string.IsNullOrEmpty(requestor))
                //{
                //    dtoresult = ProcessData.SelectAllCertified_PR_byRequestDate_Requestor(dt, requestor);
                //}
                //else
                //{
                //    //dtoresult = ProcessData.SelectAll_Certified_PR();
                   
                //}

                dtoresult = ProcessData.SelectAllCertified_PR_byStoredProcedure(requestor, dt, dept);
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        public PR_dto DisplayPR_byUser_Permission(string id,string dpt,string requestor,string dt, string dept)
        {
            try
            {
                //if (dpt != "MG")
                //{
                //    dtoresult = SearchCertifiedPR_User(id, requestor, dt);

                //}
                //else
                //{
                    dtoresult = SearchCertifiedPR_Boss(requestor, dt,dept);
                //}

                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public PR_dto CheckCertifiedPR_Status(string rn, string id)
        {
            try
            {
                dtoresult = ProcessData.Select_Certified_PR_Status(rn, id);
                if (dtoresult.dtcheck.Rows.Count > 0)
                {
                    dtoresult.sts = false;
                }
                else
                {
                    dtoresult.sts = true;
                }
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PR_dto Approve_Cancel_PR(DataTable dtdtl, string usn,string name)
        {
            try
            {
                dtoresult = ProcessData.UpdateApproval_sts(dtdtl, usn,name);
                if (dtoresult.sts == true)
                {
                    dtoresult = ProcessData.UpdatePR_Sts();


                }
                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region PR Update PO

        public PR_dto SearchApprovedPR(string req_dpt,string dt,string requestor, string req_items, string req_prno, string appdate, string appDateTo, string pono)
        {
            try
            {
                string keyword;
                if (!string.IsNullOrEmpty(requestor))
                {
                    //requestor = "and a.nm='"+requestor+"' ";
                    // Louis Enhance on 20200818 
                    requestor = "and a.nm like '%' +'" + requestor + "'+ '%' ";
                    // END
                }
                else
                {
                    requestor = "";
                }

                if (!string.IsNullOrEmpty(dt))
                {
                    dt = "and LEFT(CONVERT(VARCHAR, a.add_dt, 120), 10)='" + dt + "' ";
                }
                else
                {
                    dt = "";
                }

                if (!string.IsNullOrEmpty(req_dpt))
                {
                    req_dpt = "and a.req_dpt='" + req_dpt + "' ";
                }
                else
                {
                    req_dpt = "";
                }

                // Louis added on 20200817
                if (!String.IsNullOrEmpty(req_items))
                {
                    req_items = "and (isnull(b.item_name,'') + isnull(b.rmk,'')) like '%' + '" + req_items + "' + '%'";
                }
                else
                {
                    req_items = "";
                }

                // Louis added on 20201124
                if (!String.IsNullOrEmpty(req_prno))
                {
                    req_prno = "and (isnull(a.pr_rn,'')) like '%' + '" + req_prno + "' + '%'";
                }
                else
                {
                    req_prno = "";
                }

                // Louis Remark on 20201231 - to enhance to between function
                //if (!string.IsNullOrEmpty(appdate))
                //{
                //    appdate = "and isnull(LEFT(CONVERT(VARCHAR, b.approve_dt, 120), 10),'') ='" + appdate + "' ";
                //}
                //else
                //{
                //    appdate = "";
                //}

                if (!string.IsNullOrEmpty(appdate) && !string.IsNullOrEmpty(appDateTo))
                {
                    appdate = "and isnull(LEFT(CONVERT(VARCHAR, b.approve_dt, 120), 10),'') BETWEEN '" + appdate + "' AND '" + appDateTo + "'";
                }
                else
                {
                    appdate = "";
                }

                if (!string.IsNullOrEmpty(pono))
                {
                    // Louis Enhance on 20211214 
                    requestor = "and isnull(b.po_no,'') like '%' +'" + pono + "'+ '%' ";
                    // END
                }
                else
                {
                    requestor = "";
                }

                keyword = requestor + dt + req_dpt + req_items + req_prno + appdate;
                dtoresult = ProcessData.SelectSearchApprovedPR(keyword);
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public PR_dto UpdatePR_PO(DataTable dtdtl, string po, string usn, string rmk, string price_rmk, string gr_rmk)
        {
            try
            {
                //if (string.IsNullOrEmpty(po))
                //{
                //    dtoresult.message = "PO number cannot empty.";
                //    dtoresult.sts = false;
                //    return dtoresult;
                //}

                dtoresult = ProcessData.UpdatePR_Item_PO(dtdtl, usn, po,rmk,price_rmk,gr_rmk);
     
                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion


        #region PR View
        public PR_dto ViewMyPR(string com,string usn,string requestor,string req_dpt, string dt, string sts,string sec,string dpt, string req_item, string req_prno)
        {
            try
            {
                string keyword;
            
                if (!string.IsNullOrEmpty(requestor))
                {
                    requestor = "and a.nm='" + requestor + "' ";
                }
                else
                {
                    requestor = "";
                }

                if (!string.IsNullOrEmpty(dt))
                {
                    dt = "and LEFT(CONVERT(VARCHAR, a.add_dt, 120), 10)='" + dt + "' ";
                }
                else
                {
                    dt = "";
                }

                if (!string.IsNullOrEmpty(req_dpt))
                {
                    req_dpt = "and a.req_dpt='" + req_dpt + "' ";
                }
                else
                {
                    req_dpt = "";
                }

                if (sts=="Select Status")
                {
                   
                    sts = "";
                }
                else
                { 
                    sts = "and b.sts='" + sts + "' ";
                }

                if (!String.IsNullOrEmpty(req_item))
                {
                    req_item = "and (isnull(b.item_name,'') + isnull(b.rmk,'')) like '%' + '" + req_item + "' + '%'";
                }
                else
                {
                    req_item = "";
                }

                if (!String.IsNullOrEmpty(req_prno))
                {
                    req_prno = "and (isnull(a.pr_rn,'')) like '%' + '" + req_prno + "' + '%'";
                }
                else
                {
                    req_prno = "";
                }

                keyword = requestor + dt + req_dpt + sts + req_item + req_prno;

                if (!string.IsNullOrEmpty(sec))
                {
                    //if (sec == "PUR. DEPT")
                    if (sec == "PURCHASING" || sec == "IT") 
                    {
                        if (!string.IsNullOrEmpty(keyword))
                        {
                            keyword = "where  a.com='" + com + "' " + keyword;
                        }
                        dtoresult = ProcessData.SelectPRStatus_PUR(keyword);
                        //
                        return dtoresult;
                    }
                    else
                    {
                        dtoresult = ProcessData.SelectPRStatus_User(usn, keyword, dpt);
                    }

                }
           

                return dtoresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        #endregion

        #region PR Approval History

        public PR_dto ViewApprovePR(string usn, string requestor, string dt, string sec, string req_dpt, string sts)
        {
            try
            {
                string keyword;

                if (sec == "IT")
                {
                    sec = "where b.approve_by <> '" + usn + "' or b.verify_by <> '"+usn+ "' or b.approve_by = '" + usn + "' or b.verify_by = '" + usn + "'";

                }
                else
                {
                    sec = "where b.approve_by='" + usn + "' or b.verify_by='" + usn + "'";
                }

                if (!string.IsNullOrEmpty(requestor))
                {
                    requestor = "and a.nm='" + requestor + "' ";
                }
                else
                {
                    requestor = "";
                }

                if (!string.IsNullOrEmpty(dt))
                {
                    dt = "and LEFT(CONVERT(VARCHAR, a.add_dt, 120), 10)='" + dt + "' ";
                }
                else
                {
                    dt = "";
                }

                if (!string.IsNullOrEmpty(req_dpt))
                {
                    req_dpt = "and a.req_dpt='" + req_dpt + "' ";
                }
                else
                {
                    req_dpt = "";
                }

                if (sts == "Select Status")
                {

                    sts = "";
                }
                else
                {
                    sts = "and b.sts='" + sts + "' ";
                }

              


                keyword = sec+ requestor + dt + req_dpt + sts;
                dtoresult = ProcessData.SelectApprovedPR_Management(keyword);
                return dtoresult;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Export Log
        public PR_dto InsertExportLogs(List<ExportLogModel> models)
        {
            try
            {
                dtoresult = ProcessData.GenerateInsertExportLogs(models);

                return dtoresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion
    }
}