using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineShopping
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void member_Click(object sender, EventArgs e)
        {
            Response.Redirect("Member.aspx");
        }

        protected void staff_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaffLogIn.aspx");
        }
    }
}