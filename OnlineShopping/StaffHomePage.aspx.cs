using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineShopping
{
    public partial class StaffHomePage : System.Web.UI.Page
    {
        Person p;
        HttpCookie myCookies;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session.Count != 0))
            {
                string num = Convert.ToString(Session.Count);
                string catalogKey = "person" + num;
                p = (Person)Session[catalogKey];
                if (p == null)
                    Response.Redirect("Home.aspx");
                myCookies = Request.Cookies[p._userName];
                if ((myCookies == null) || (myCookies["Name"] == ""))
                {
                    //logout! or home!
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    this.printName.Visible = true;
                    this.printName.Text = "Please click the following button to use Load Cart\n";
                    this.printName.Text = "Welcome " + myCookies["Name"];
                }
            }
            else
            {
                Response.Redirect("Home.aspx");
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoadCart.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string num = Convert.ToString(Session.Count);
            string catalogKey = "person" + num;
            p = (Person)Session[catalogKey];
            Session.Remove(p._userName);
            Session["person" + Session.Count] = null;
            Response.Redirect("Home.aspx");
        }
    }
}