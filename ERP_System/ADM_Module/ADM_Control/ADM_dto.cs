using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace ERP_System.ADM_Module.ADM_Module
{
    public class ADM_dto
    {

        public DataTable dtcom { get; set; }

        public DataTable dtdpt { get; set; }

        public DataTable dtcheck { get; set; }

        public Boolean sts { get; set; }

        public string message { get; set; }

        public List<string> list_user { get; set; }
        
        public List<string> list_r_user { get; set; }

        public DataTable dtfirst_module { get; set; }

        public DataTable dtsecond_module { get; set; }

        public DataTable dtthird_module { get; set; }

        public DataTable dtSec { get; set; }
    }
}