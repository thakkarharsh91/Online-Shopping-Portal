using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineShopping
{
    public partial class HeaderUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String currDate;
            currDate = DateTime.Now.ToString();
            Label3.Text = "  Current Date and Time  : " + currDate;
            Label2.Text = "Welcome to shopcart.com";
        }
    }
}