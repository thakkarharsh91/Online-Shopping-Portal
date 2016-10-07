using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Resources;


namespace LoadCartService
{

    //Service Description:
    //This is the service that is used to Load the cart for our Online Cart Services.
    //The main function of this service is to load the CSV and display all the items so that the administrator of the website can view it.
    //There are different catagories on which the items are bifercated for the ease of administrator.
     public class Service1 : IService1
    {

         //this method reads the csv and load the cart.
         //it returns a List<string> type casted into string which contains the whole list of items from the CSV.
         //string address = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))) + "\\" + "Cart.csv";
         
        public List<string> loadCart()
        {
            string address = "";
            List<string> items = new List<string>();;
            try{
                //Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))) + "\\" + "addcart.csv";
                //address = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))) + "\\" + "Cart.csv";
                //address = new Uri(address).LocalPath;

                //address = AppDomain.CurrentDomain.BaseDirectory + "Cart.csv";

                address = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, @"App_Data\Cart.csv");
                var reader = new StreamReader(File.OpenRead(address));
                
                items = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    items.Add(line);
                }
                reader.Close();
            }
            catch (Exception e)
            {

            }
            //reader = new StreamReader(File.OpenRead(address));
            return items;
        }



        //This method is used to sort the cart as per the items.
        //The method returns a string array which has the item and total number of items.
        public string[] organizedItems()
        {
            string address = "";
            //address = new Uri(address).LocalPath;
            List<string> items = new List<string>(); ;
            try
            {
                //address = AppDomain.CurrentDomain.BaseDirectory + "Cart.csv";
                address = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, @"App_Data\Cart.csv");
                var reader = new StreamReader(File.OpenRead(address));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    items.Add(line);
                }
                reader.Close();
            }
            catch (Exception e)
            {

            }
            
            List<string> jeans = new List<string>();
            List<string> tshirt = new List<string>();
            List<string> tango = new List<string>();
            List<string> shorts = new List<string>();
            List<string> shoes = new List<string>();
            List<string> defaultItems = new List<string>();

            foreach (string eachItem in items)
            {
                string[] row = eachItem.Split(',');
                switch(row[0].Trim())
                {
                    case "Jeans":
                        jeans.Add(eachItem);
                        break;
                    case "tshirt":
                        tshirt.Add(eachItem);
                        break;
                    case "tango":
                        tango.Add(eachItem);
                        break;
                    case "shorts":
                        shorts.Add(eachItem);
                        break;
                    case "shoes":
                        shoes.Add(eachItem);
                        break;
                    default:
                        defaultItems.Add(eachItem);
                        break;
                }
            }

            string[] organizedArray = new string[6];
            organizedArray[0] = "jeans     " + jeans.Count;
            organizedArray[1] = "tshirt    " + tshirt.Count;
            organizedArray[2] = "tango     " + tango.Count;
            organizedArray[3] = "shorts    " + shorts.Count;
            organizedArray[4] = "shoes     " + shoes.Count;
            organizedArray[5] = "others    " + defaultItems.Count;
            return organizedArray;
        }

        //This method is used to sort items as per the size of the items.
        //The method returns a string array which has number of items of each size.
        public string[] organizedItemsBySize()
        {
            string address = "";
            //address = new Uri(address).LocalPath;
            List<string> items = new List<string>();
            try
            {
                //address = AppDomain.CurrentDomain.BaseDirectory + "Cart.csv";
                address = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, @"App_Data\Cart.csv");
                var reader = new StreamReader(File.OpenRead(address));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    items.Add(line);
                }
                reader.Close();
            }
            catch (Exception e)
            { }
            List<string> XS = new List<string>();
            List<string> S = new List<string>();
            List<string> M = new List<string>();
            List<string> L = new List<string>();
            List<string> XL = new List<string>();
            List<string> otherSizes = new List<string>();

            foreach (string eachItem in items)
            {
                string[] row = eachItem.Split(',');
                switch (row[3].Trim())
                {
                    case "XS":
                        XS.Add(eachItem);
                        break;
                    case "S":
                        S.Add(eachItem);
                        break;
                    case "M":
                        M.Add(eachItem);
                        break;
                    case "L":
                        L.Add(eachItem);
                        break;
                    case "XL":
                        XL.Add(eachItem);
                        break;
                    default:
                        otherSizes.Add(eachItem);
                        break;
                }
            }

            string[] organizedArray = new string[6];
            organizedArray[0] = "XS:            " + XS.Count;
            organizedArray[1] = "S:             " + S.Count;
            organizedArray[2] = "M:             " + M.Count;
            organizedArray[3] = "L:             " + L.Count;
            organizedArray[4] = "XL:            " + XL.Count;
            organizedArray[5] = "Special Sized: " + otherSizes.Count;

            return organizedArray;
        }

        //this method is used to bifercate items based on Color of the product.
        
         
         //This method is used to sort items as per the color of the items.
         //The method returns a string array which has number of items of each color.
         public string[] organizedItemsByColor()
        {
            string address = ""; 
            //address = new Uri(address).LocalPath;
            address = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, @"App_Data\Cart.csv");
            List<string> items = new List<string>();
            try
            {
                address = AppDomain.CurrentDomain.BaseDirectory + "Cart.csv";
                var reader = new StreamReader(File.OpenRead(address));
                
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    items.Add(line);
                }
                reader.Close();
            }
            catch (Exception e)
            { }
            
            List<string> Black = new List<string>();
            List<string> White = new List<string>();
            List<string> Blue = new List<string>();
            List<string> Maroon = new List<string>();
            List<string> Golden = new List<string>();
            List<string> otherColors = new List<string>();

            foreach (string eachItem in items)
            {
                string[] row = eachItem.Split(',');
                switch (row[1].Trim())
                {
                    case "Black":
                        Black.Add(eachItem);
                        break;
                    case "White":
                        White.Add(eachItem);
                        break;
                    case "Blue":
                        Blue.Add(eachItem);
                        break;
                    case "Maroon":
                        Maroon.Add(eachItem);
                        break;
                    case "Golden":
                        Golden.Add(eachItem);
                        break;
                    default:
                        otherColors.Add(eachItem);
                        break;
                }
            }

            string[] organizedArray = new string[6];
            organizedArray[0] = "Black:           " + Black.Count;
            organizedArray[1] = "White:           " + White.Count;
            organizedArray[2] = "Blue:            " + Blue.Count;
            organizedArray[3] = "Maroon:          " + Maroon.Count;
            organizedArray[4] = "Golden:          " + Golden.Count;
            organizedArray[5] = "Special Colored: " + otherColors.Count;
            return organizedArray;
        }

    }
}
