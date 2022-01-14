using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace ERP_System.SALES_ORDER.SALES_ORDER_Control
{
    public class SO_dto
    {
        #region SC Entry

        public DataTable dtloc { get; set; }

        public DataTable dtgroup { get; set; }

        public DataTable dtBillTo { get; set; }

        public DataTable dtSoldTo { get; set; }

        public DataTable dtCom { get; set; }

        public DataTable dtSignatory { get; set; }

        public DataTable dtTerm { get; set; }

        public DataTable dtCatalog { get; set; }

        public DataTable dtCtlnoInfo { get; set; }

        public DataTable dtUOM { get; set; }

        public Boolean sts { get; set; }

        public string Message { get; set; }

        public string auto_no { get; set; }

        public List<string> List_Catalog { get; set; }

        public DataTable dtprice { get; set; }

        public SqlCommand cmd { get; set; }

        public string scno { get; set; }

        public DataTable dtrn { get; set; }

        #endregion

        #region SC Certify

        public DataTable dtheader { get; set; }

        public DataTable dtscd { get; set; }

        public DataTable dtscdd { get; set; }

        public DataTable dtcheck { get; set; }
        #endregion
    }
}