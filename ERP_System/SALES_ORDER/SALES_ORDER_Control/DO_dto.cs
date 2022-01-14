using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace ERP_System.SALES_ORDER.SALES_ORDER_Control
{
	public class DO_dto
	{
        public DataTable dtgroup { get; set; }

        public DataTable dtsold { get; set; }

        public DataTable dtbill { get; set; }

        public DataTable dtship { get; set; }

        public DataTable dtterm { get; set; }

        public DataTable dtdterm { get; set; }

        public DataTable dtsc_no { get; set; }

        public DataTable dtctlno { get; set; }

        public DataTable dtdsc { get; set; }

        public DataTable dtuom { get; set; }

        public DataTable dtf_ctlno { get; set; }

        public DataTable dtf_dsc { get; set; }

        public DataTable dtCus { get; set; }

        public Boolean sts { get; set; }

        public string Message { get; set; }

        public string do_no { get; set; }

        public string inv_no { get; set; }

        public DataTable dtdo_rn { get; set; }

        public DataTable dtinv_rn { get; set; }

        public SqlCommand cmd { get; set; }

        public DataTable dthdr { get; set; }

        public DataTable dtdtl { get; set; }

        public DataTable dtfoc { get; set; }

        public DataTable dtcheck { get; set; }
    }
}