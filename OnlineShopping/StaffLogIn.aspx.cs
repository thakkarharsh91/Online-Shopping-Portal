using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Web;

namespace OnlineShopping
{
    public partial class StaffLogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void loginadmin_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(username.Text) || String.IsNullOrEmpty(password.Value))
            {
                error.Visible = true;
                error.Text = "All fields are mandatory";
            }

            else
            {

                if (String.IsNullOrEmpty(username.Text) || String.IsNullOrEmpty(password.Value))
                {
                    error.Visible = true;
                    error.Text = "All fields are mandatory";
                }
                else if (username.Text != "adminuser" && !Regex.IsMatch(password.Value, @"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$!%]).{6,20})"))
                {
                    error.Visible = true;
                    error.Text = "The password must contain one digit from 0-9, must contain one lowercase characters,must contain one uppercase characters, must contain one special symbols in the list @#!$%. The length of the password should be between 6 to 20 characters.";
                }
                else
                {
                    EncryptDecrypt.EncryptDecrypt encryptDecrypt = new EncryptDecrypt.EncryptDecrypt();

                    string fileName = "staff.xml";
                    string fLocation = Path.Combine(HttpRuntime.AppDomainAppPath, @"App_Data");
                    fLocation = Path.Combine(fLocation, fileName);

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(fLocation);

                    XmlElement root = xmlDoc.DocumentElement;

                    foreach (XmlNode userNameNode in root.ChildNodes)
                    {
                        string userName = userNameNode.Attributes["Username"].Value;
                        if (userName.Equals(username.Text))
                        {
                            string passWord = "";
                            foreach (XmlNode passWordNode in userNameNode.ChildNodes)
                            {
                                string tempString = passWordNode.InnerText;
                                passWord = encryptDecrypt.decrypt(tempString);
                                if (passWord.Equals(password.Value))
                                {
                                    //cookie code!
                                    HttpCookie myCookies = new HttpCookie(username.Text);
                                    myCookies["Name"] = username.Text;
                                    myCookies.Expires = DateTime.Now.AddMonths(6);
                                    Response.Cookies.Add(myCookies);
                                    //create session
                                    string num = Convert.ToString(Session.Count + 1);
                                    string catalogKey = "person" + num;
                                    Session[catalogKey] = new Person(username.Text);

                                    if (username.Text.Equals("adminuser"))
                                    {
                                        Response.Redirect("AdminFunctionalities.aspx");
                                    }
                                    else //logs in for Staff
                                    {
                                        Response.Redirect("StaffHomePage.aspx");
                                    }
                                }
                                else
                                {
                                    error.Visible = true;
                                    error.Text = "Invalid Username and/or Password! Please try again";
                                    return;
                                }
                            }

                        }
                    }
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}