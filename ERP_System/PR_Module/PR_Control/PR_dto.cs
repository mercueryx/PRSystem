using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace ERP_System.PR_Module.PR_Control
{
    public class PR_dto
    {

        public DataTable dtuser { get; set; }

        public DataTable dtcom { get; set; }

        public DataTable dtcategory { get; set; }

        public DataTable dtitem { get; set; }

        public string message { get; set; }

        public List<string> List_Item { get; set; }

        public Boolean sts { get; set; }

        public DataTable dtcheck { get; set; }

        public SqlCommand cmd { get; set; }

        public string pr_rn { get; set; }

        public DataTable dtrn { get; set; }

        public DataTable dtpr_hdr { get; set; }

        public DataTable dtpr_dtl { get; set; }

        public DataTable dtdpt { get; set; }

        public DataTable dtsec { get; set; }

        public DataTable  dtref { get; set; }

        public DataTable dtUOM { get; set; }
    }
}