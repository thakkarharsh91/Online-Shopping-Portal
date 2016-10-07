using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Xml;

namespace ShoppingCheckout
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ShoppingCheckOutService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ShoppingCheckOutService.svc or ShoppingCheckOutService.svc.cs at the Solution Explorer and start debugging.
    public class ShoppingCheckOutService : IShoppingCheckOutService
    {
        public string[] CheckOutPage(string items, string zipcode)
        {

            float total_price = 0;
            float tax = 0;

            XmlDocument xml_response = new XmlDocument();

            try
            {
                string requestUrl = "https://taxrates.api.avalara.com/postal?country=usa&postal=" + zipcode + "&apikey=N4C1M7BGSqVoBCtf%2Bjk8ti%2FXiMm3zsJW%2FYc0dPtAtoLuY5hcoPTGnAXmm9PIdveFrGDyFojyncsaLzVLkufNCQ%3D%3D";
                using (WebClient wc = new WebClient())
                {
                    var json = wc.DownloadString(requestUrl);
                    xml_response = JsonConvert.DeserializeXmlNode(json, "root");
                }
                XmlNodeList element = xml_response.GetElementsByTagName("totalRate");
                tax = float.Parse(Convert.ToString(element[0].InnerXml));
            }

            catch (Exception e)
            {
                string[] error_msg = new string[] { "Invalid ZipCode !!" };
                return error_msg;
            }
            
            string fileName = "billing_detail.txt";
            string fLocation = Path.Combine(HttpRuntime.AppDomainAppPath, @"App_Data");
            fLocation = Path.Combine(fLocation, fileName);


            string[] user_item = items.Split('|');
            string user_item_name = user_item[0];
            string user_item_quatity = user_item[1];
            string user_item_unitPrice = user_item[2];

            File.AppendAllText(fLocation, (items + Environment.NewLine));
            total_price = total_price + (float.Parse(user_item_unitPrice) * float.Parse(user_item_quatity));

            File.AppendAllText(fLocation, ("Tax|" + tax + Environment.NewLine));

            tax = tax / 100;

            total_price = total_price + (total_price * tax);

            File.AppendAllText(fLocation, ("Total Price|" + total_price + Environment.NewLine));

            string[] billing_summary = File.ReadAllLines(fLocation);

            File.Delete(fLocation);

            return billing_summary;
        }
    }
}
