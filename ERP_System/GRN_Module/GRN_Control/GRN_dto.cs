using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace ERP_System.GRN_Module.GRN_Control
{
    public class GRN_dto
    {
        #region GRN Entry
        public DataTable dtven { get; set; }

        public DataTable dtpo { get; set; }

        public DataTable dtpo_details { get; set; }

        public DataTable dtpo_info { get; set; }

        public SqlCommand cmd { get; set; }

        public DataTable dtgrn_dtl { get; set; }

        public Boolean sts { get; set; }

        public string grn_no { get; set; }

        public DataTable dtcheck { get; set; }

        public DataTable dtrn { get; set; }

        public string Message { get; set; }

        public DataTable dtgrn_hdr { get; set; }

        public DataTable dtdtl { get; set; }
        #endregion

        #region GRN Edit

        public DataTable dtgrn { get; set; }

        #endregion
    }
}