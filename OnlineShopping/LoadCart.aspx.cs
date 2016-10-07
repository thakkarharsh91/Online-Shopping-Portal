using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineShopping
{
    public partial class LoadCart : System.Web.UI.Page
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
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    this.printName.Visible = true;
                    this.printName.Text = "Please click the following button to use Load Cart\n";
                    this.printName.Text = myCookies["Name"];
                    loadCSV();
                }
            }
            else
            {
                Response.Redirect("Home.aspx");
            }
        }


        private void loadCSV()
        {
            //string uriPath1 = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))) + "\\" + "Cart.csv";
            //string uriPath2 = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))) + "\\" + "addcart.csv";
            //string address1 = new Uri(uriPath1).LocalPath;
            //string address2 = new Uri(uriPath2).LocalPath;

            //string address1 = AppDomain.CurrentDomain.BaseDirectory + "Cart.csv";
            //string address2 = AppDomain.CurrentDomain.BaseDirectory + "addcart.csv";

            string address1 = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, @"App_Data\Cart.csv");
            string address2 = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, @"App_Data\addcart.csv");
           

            using (FileStream streamReader = File.OpenRead(address1))
            using (FileStream writeStreamWriter = File.OpenWrite(address2))
            {
                BinaryReader readFile = new BinaryReader(streamReader);
                BinaryWriter writer = new BinaryWriter(writeStreamWriter);

                byte[] buffer = new Byte[1024];
                int bytesRead;

                while ((bytesRead =
                        streamReader.Read(buffer, 0, 1024)) > 0)
                {
                    writeStreamWriter.Write(buffer, 0, bytesRead);
                }
                streamReader.Close();
                writeStreamWriter.Close();
            }
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


        protected void displayCart(object sender, EventArgs e)
        {
            this.hideOthers(); this.makeReadOnly();

            this.testLabel.Text = "1";
            LoadCartService.Service1Client loadService = new LoadCartService.Service1Client();


            this.testLabel.Text = "2";

            string[] allProducts = loadService.loadCart();
            this.testLabel.Text = "3";


            string stringToDisplay = "";

            foreach (string row in allProducts)
            {
                stringToDisplay += row.Replace(',', ' ') + "\n";
            }
            this.txtArea_productList.InnerText = stringToDisplay;
            this.txtArea_productList.Visible = true;
            this.txtArea_productList.Rows = 50;
            this.txtArea_productList.Cols = 45;
            this.testLabel.Text = "4";
        }

        protected void displayStats(object sender, EventArgs e)
        {
            this.hideOthers();

            LoadCartService.Service1Client loadService = new LoadCartService.Service1Client();
            string[] statisticArray = loadService.organizedItems();

            string stringToDisplay = "";

            foreach (string row in statisticArray)
            {
                stringToDisplay += row + "\n";
            }

            this.txtArea_productList.InnerText = stringToDisplay;


            this.txtArea_productList.Rows = 8;
            this.txtArea_productList.Cols = 25;
            this.txtArea_productList.Visible = true;
            this.makeReadOnly();
        }

        protected void displayItemsBySize(object sender, EventArgs e)
        {
            this.hideOthers(); this.makeReadOnly();

            LoadCartService.Service1Client loadService = new LoadCartService.Service1Client();
            string[] productsBySize = loadService.organizedItemsBySize();

            this.txtArea_productList.Rows = 8;
            this.txtArea_productList.Cols = 25;

            string stringToDisplay = "";

            foreach (string row in productsBySize)
            {
                stringToDisplay += row + "\n";
            }
            this.txtArea_productList.Visible = true;
            this.txtArea_productList.InnerText = stringToDisplay;

            this.btn_displayEachSizedItems.Visible = true;

        }


        protected void displayEachSizedItems(object sender, EventArgs e)
        {
            this.hideOthers(); this.makeReadOnly();
            this.btn_displayEachSizedItems.Visible = true;

            this.txtArea_1.Visible = true;
            this.txtArea_2.Visible = true;
            this.txtArea_3.Visible = true;
            this.txtArea_4.Visible = true;
            this.txtArea_5.Visible = true;
            this.txtArea_6.Visible = true;

            string temp1 = "";
            string temp2 = "";
            string temp3 = "";
            string temp4 = "";
            string temp5 = "";
            string temp6 = "";


            LoadCartService.Service1Client loadService = new LoadCartService.Service1Client();
            string[] productsBySize = loadService.loadCart();

            foreach (string eachItem in productsBySize)
            {
                string[] row = eachItem.Replace(",", " ").Split(' ');
                switch (row[3].Trim())
                {
                    case "XS":
                        temp1 += eachItem.Replace(",", " ") + "\n";
                        break;
                    case "S":
                        temp2 += eachItem.Replace(",", " ") + "\n";
                        break;
                    case "M":
                        temp3 += eachItem.Replace(",", " ") + "\n";
                        break;
                    case "L":
                        temp4 += eachItem.Replace(",", " ") + "\n";
                        break;
                    case "XL":
                        temp5 += eachItem.Replace(",", " ") + "\n";
                        break;
                    default:
                        temp6 += eachItem.Replace(",", " ") + "\n";
                        break;
                }
            }

            //this.txtArea_productList.Visible = false;

            this.txtArea_1.InnerText = temp1;
            this.txtArea_2.InnerText = temp2;
            this.txtArea_3.InnerText = temp3;
            this.txtArea_4.InnerText = temp4;
            this.txtArea_5.InnerText = temp5;
            this.txtArea_6.InnerText = temp6;
        }


        protected void displayItemsByColors(object sender, EventArgs e)
        {
            this.hideOthers(); this.makeReadOnly();

            LoadCartService.Service1Client loadService = new LoadCartService.Service1Client();
            string[] productsBySize = loadService.organizedItemsByColor();

            string stringToDisplay = "";

            foreach (string row in productsBySize)
            {
                stringToDisplay += row + "\n";
            }

            this.txtArea_productList.Visible = true;
            this.txtArea_productList.Rows = 8;
            this.txtArea_productList.Cols = 25;

            this.txtArea_productList.InnerText = stringToDisplay;

            this.btn_displayEachColoredItems.Visible = true;

        }


        protected void displayEachColoredItems(object sender, EventArgs e)
        {
            this.hideOthers(); this.makeReadOnly();

            this.btn_displayEachColoredItems.Visible = true;

            this.txtArea_1.Visible = true;
            this.txtArea_2.Visible = true;
            this.txtArea_3.Visible = true;
            this.txtArea_4.Visible = true;
            this.txtArea_5.Visible = true;
            this.txtArea_6.Visible = true;

            string temp1 = "";
            string temp2 = "";
            string temp3 = "";
            string temp4 = "";
            string temp5 = "";
            string temp6 = "";


            LoadCartService.Service1Client loadService = new LoadCartService.Service1Client();
            string[] productsBySize = loadService.loadCart();

            foreach (string eachItem in productsBySize)
            {
                string[] row = eachItem.Replace(",", " ").Split(' ');
                switch (row[1].Trim())
                {
                    case "Black":
                        temp1 += eachItem.Replace(",", " ") + "\n";
                        break;
                    case "White":
                        temp2 += eachItem.Replace(",", " ") + "\n";
                        break;
                    case "Blue":
                        temp3 += eachItem.Replace(",", " ") + "\n";
                        break;
                    case "Maroon":
                        temp4 += eachItem.Replace(",", " ") + "\n";
                        break;
                    case "Golden":
                        temp5 += eachItem.Replace(",", " ") + "\n";
                        break;
                    default:
                        temp6 += eachItem.Replace(",", " ") + "\n";
                        break;
                }
            }

            this.txtArea_1.InnerText = temp1;
            this.txtArea_2.InnerText = temp2;
            this.txtArea_3.InnerText = temp3;
            this.txtArea_4.InnerText = temp4;
            this.txtArea_5.InnerText = temp5;
            this.txtArea_6.InnerText = temp6;
        }


        private void hideOthers()
        {
            this.txtArea_productList.Visible = false;
            this.btn_displayEachSizedItems.Visible = false;

            this.btn_displayEachColoredItems.Visible = false;

            this.txtArea_1.Visible = false;
            this.txtArea_2.Visible = false;
            this.txtArea_3.Visible = false;
            this.txtArea_4.Visible = false;
            this.txtArea_5.Visible = false;
            this.txtArea_6.Visible = false;
        }

        private void makeReadOnly()
        {
            this.txtArea_1.Attributes.Add("readonly", "readonly");
            this.txtArea_2.Attributes.Add("readonly", "readonly");
            this.txtArea_3.Attributes.Add("readonly", "readonly");
            this.txtArea_4.Attributes.Add("readonly", "readonly");
            this.txtArea_5.Attributes.Add("readonly", "readonly");
            this.txtArea_6.Attributes.Add("readonly", "readonly");
            this.txtArea_productList.Attributes.Add("readonly", "readonly");
        }

    }
}