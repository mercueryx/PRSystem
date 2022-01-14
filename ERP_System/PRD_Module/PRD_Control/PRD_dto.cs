using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace ERP_System.PRD_Module.PRD_Control
{
    public class PRD_dto
    {
        #region Create Packing List

        public DataTable dtbrandcode { get; set; }

        public DataTable dtmaster { get; set; }

        public DataTable dtloc { get; set; }

        public DataTable dtcatalog { get; set; }

        public DataTable dtcheck { get; set; }

        public SqlCommand cmd { get; set; }

        public Boolean sts { get; set; }

        public DataTable dtrn { get; set; }

        public string message { get; set; }

        public DataTable dtdesc { get; set; }

        public string version { get; set; }

        public DataTable dtcust { get; set; }

        public DataTable dtpkgdetails { get; set; }

        public DataTable dtpkginfo { get; set; }
        #endregion
    }
}