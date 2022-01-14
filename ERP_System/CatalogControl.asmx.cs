using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ERP_System.PO_Module.PO_Control;
using System.Configuration;
namespace ERP_System
{
    /// <summary>
    /// Summary description for CatalogControl
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class CatalogControl : System.Web.Services.WebService
    {
       readonly PO_dto dtoresult = new PO_dto();
       readonly  PO_da Process_Data = new PO_da();

        [WebMethod]
        public List<string> GetCatalogNames(string term)
        {
            try
            {
                string com, ven_type;
                com = (string)Session["com"];
                ven_type = (string)Session["ven_type"];
                List<string> listCatalog = new List<string>();
              //  dtoresult = Process_Data.SelectCatalogNo_by_com_type(com,ven_type,term);
                return listCatalog;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
