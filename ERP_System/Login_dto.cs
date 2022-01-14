using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace ERP_System
{
    public class Login_dto
    {

        public DataTable dtUser { get; set; }

        public string message { get; set; }

        public DataTable dtpermission { get; set; }

        public Boolean sts { get; set; }

        public Boolean access { get; set; }

        public DataTable dtcom { get; set; }
    }
}