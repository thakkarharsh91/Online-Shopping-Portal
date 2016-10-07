using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Xml;

namespace EstimateDeliveryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EstimateDelivery" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EstimateDelivery.svc or EstimateDelivery.svc.cs at the Solution Explorer and start debugging.
    public class EstimateDelivery : IEstimateDelivery
    {
        public string estimateDeliveryDays(string zip)
        {
            // Read the file and display it line by line.
            int zipcode = int.Parse(zip);
            string[] nearZip = new string[3];
            string[] distance = new string[3];

            //Read all the Pincode from file zipcode_list.txt to get 3 pincode nearby user entered Pincode

            string fileName = "zipcode_list.txt";
            string fLocation = Path.Combine(HttpRuntime.AppDomainAppPath, @"App_Data");
            fLocation = Path.Combine(fLocation, fileName);

            var lines = File.ReadAllLines(fLocation);


            int testzip1 = int.Parse(lines[(lines.Length - 1)]);
            int testzip2 = int.Parse(lines[1]);
            if (zipcode >= testzip1)
            {
                nearZip[0] = lines[lines.Length - 1];
                nearZip[1] = lines[(lines.Length - 2)];
                nearZip[2] = lines[(lines.Length - 3)];
            }
            else if (zipcode < testzip2)
            {
                nearZip[0] = lines[0];
                nearZip[1] = lines[1];
                nearZip[2] = lines[2];
            }
            else
            {
                for (int i = 1; i < (lines.Length - 1); i++)
                {
                    int testzip = int.Parse(lines[i]);
                    if (zipcode <= (testzip))
                    {
                        nearZip[0] = lines[i - 1];
                        nearZip[1] = lines[i];
                        nearZip[2] = lines[i + 1];
                        break;
                    }
                }
            }

            //Calculating minimum distance from 3 nearbyZip 

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    string requestUrl = null;
                    requestUrl = "https://www.zipwise.com/webservices/distance.php?key=4mhks6jux3pasxqj&zip1=" + zip + "&zip2=" + nearZip[i] +"&format=xml";
                    var webresponse = new System.Net.WebClient().DownloadString(requestUrl);
                    XmlDocument xmlResponse = new XmlDocument();
                    xmlResponse.LoadXml(webresponse);
                    XmlNodeList element = xmlResponse.GetElementsByTagName("distance");
                    distance[i] = Convert.ToString(element[0].InnerXml);
                }
                catch (Exception e)
                {
                    return "Invalid Zipcode";
                }

            }

            float[] dist_float = Array.ConvertAll(distance, s => float.Parse(s));
            float min_distance = dist_float.Min();

            // Minimum delivery time is one day for less than 30 miles, for every 30 miles it extends delivery time by one day.

            int min_days = 1;
      
            while (min_distance > 30)
            {
                min_distance = min_distance - 30;
                min_days = min_days + 1;
            }

            // Checks the current time, if current time is past 3oclock then it add one additional day as current day will not be included in businees day.
            /* For testing different time, you can replace 
            DateTime curr_time = DateTime.Now; with 
            DateTime curr_time = DateTime.Parse(<desired time as below>); */

            DateTime curr_time = DateTime.Now;
            DateTime cutoff_time = DateTime.Parse("15:00:00.000");

            if (curr_time.TimeOfDay > cutoff_time.TimeOfDay)
            {
                min_days = min_days + 1;
                curr_time = curr_time.AddDays((double)min_days);
                curr_time = curr_time.Date;

                string returnMessage = "Your estimated delivery date is " + curr_time.ToString("d");
                return returnMessage;
            }
            else
            {
                curr_time = curr_time.AddDays((double)min_days);
                curr_time = curr_time.Date;

                string returnMessage = "If you order today before 3PM, Your estimated delivery date is " + curr_time.ToString("d");
                return returnMessage;
            }
        }
    }
}
