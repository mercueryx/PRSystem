using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
namespace ERP_System
{
    public class MessageBox
    {
        public static void Show(Page Page, String Message)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox", "<script language='javascript'>alert('" + Message + "');</script>");
        }
    }
}