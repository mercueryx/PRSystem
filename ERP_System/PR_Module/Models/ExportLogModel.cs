using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_System.PR_Module.Models
{
    public class ExportLogModel
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Username { get; set; }
        public DateTime ExportDate { get; set; }
    }
}