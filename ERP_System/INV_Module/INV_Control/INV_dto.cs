using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace ERP_System.INV_Module.INV_Control
{
    public class INV_dto
    {
        public DataTable dtloc_from { get; set; }

        public DataTable dtloc_to { get; set; }

        public DataTable dtctlno { get; set; }

        public DataTable dtinv_dtl { get; set; }

        public string Message { get; set; }

        public Boolean sts { get; set; }

        public string tranx_no { get; set; }

        public SqlCommand cmd { get; set; }

        public DataTable dtinv_type { get; set; }

        public DataTable dtrn { get; set; }

        public DataTable dtcheck  { get; set; }
    }
}