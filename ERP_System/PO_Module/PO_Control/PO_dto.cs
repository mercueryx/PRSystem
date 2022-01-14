using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace ERP_System.PO_Module.PO_Control
{
    public class PO_dto
    {

        #region Login
        public DataTable dtUser { get; set; }
        #endregion

        #region PO Entry
        public DataTable dtCompanyCode { get; set; }

        public DataTable dtVen_Type { get; set; }

        public DataTable dtVen_Code { get; set; }

        public DataTable  dtVen_Info { get; set; }

        public DataTable dtPur_Term { get; set; }

        public DataTable dtcatalog { get; set; }

        public DataTable  dtcataloginfo { get; set; }

        public DataTable  dtuom { get; set; }

        public Boolean sts { get; set; }

        public string Message { get; set; }

        public DataTable dtprice { get; set; }

        public DataTable dtrn { get; set; }

        public SqlCommand cmd { get; set; }

        public string po_no { get; set; }

        public string version { get; set; }
        #endregion

        #region PO Approval
        public DataTable  dtPO { get; set; }

        public DataTable dtPO_Details { get; set; }
        #endregion

        #region PO Edit

        public DataTable dtpo_hdr { get; set; }
   
        public DataTable dtcheck { get; set; }
      
        #endregion
    }
}