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
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace AddCartService
{
    public class Service1 : IService1
    {
        public List<string> getProducts()
        {
            string address = "";
            List<string> items = new List<string>();
            List<String> products = new List<string>();
            try
            {
                //address = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))) + "\\" + "Cart.csv";
                //address = new Uri(address).LocalPath;
                address = AppDomain.CurrentDomain.BaseDirectory + "addcart.csv";
                var reader = new StreamReader(File.OpenRead(address));

                items = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    items.Add(line);
                }
                String[] row = new String[items.Count];
                foreach (string uniqueitem in items)
                {
                    row = uniqueitem.Split(',');
                    if (!products.Contains(row[0].Trim()))
                    {
                        products.Add(row[0].Trim());
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {

            }
            return products;
        }

        public List<string> getColors(string product)
        {
            string address = "";
            List<string> items = new List<string>();
            List<String> colors = new List<string>();
            try
            {
                address = AppDomain.CurrentDomain.BaseDirectory + "addcart.csv";
                address = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))) + "\\" + "addcart.csv";
                address = new Uri(address).LocalPath;
                var reader = new StreamReader(File.OpenRead(address));

                items = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line.Contains(product))
                        items.Add(line);
                }
                String[] row = new String[items.Count];
                foreach (string uniqueitem in items)
                {
                    row = uniqueitem.Split(',');
                    if (!colors.Contains(row[1].Trim()))
                    {
                        colors.Add(row[1].Trim());
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {

            }
            return colors;
        }

        public List<string> getSizes(string product, string color)
        {
            string address = "";
            List<string> items = new List<string>();
            List<String> sizes = new List<string>();
            try
            {

                address = AppDomain.CurrentDomain.BaseDirectory + "addcart.csv";
                address = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))) + "\\" + "addcart.csv";
                address = new Uri(address).LocalPath;
                var reader = new StreamReader(File.OpenRead(address));

                items = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line.Contains(product) && line.Contains(color))
                        items.Add(line);
                }
                String[] row = new String[items.Count];
                foreach (string uniqueitem in items)
                {
                    row = uniqueitem.Split(',');
                    if (!sizes.Contains(row[3].Trim()))
                    {
                        sizes.Add(row[3].Trim());
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {

            }
            return sizes;
        }

        public String getQuantity(string product, string color, string size)
        {
            string address = "";
            List<string> items = new List<string>();
            string quantity = "";
            try
            {
                address = AppDomain.CurrentDomain.BaseDirectory + "addcart.csv";
                address = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))) + "\\" + "addcart.csv";
                address = new Uri(address).LocalPath;
                var reader = new StreamReader(File.OpenRead(address));

                items = new List<string>();
                String[] unique;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    unique = line.Split(',');
                    if (unique[0].Equals(product) && unique[1].Equals(color) && unique[3].Equals(size))
                    {
                        quantity = unique[4];
                        break;
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {

            }
            return quantity;
        }

        public String getPrice(string product, string color, string size)
        {
            string address = "";
            List<string> items = new List<string>();
            string price = "";
            try
            {
                address = AppDomain.CurrentDomain.BaseDirectory + "addcart.csv";
                address = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))) + "\\" + "addcart.csv";
                address = new Uri(address).LocalPath;
                var reader = new StreamReader(File.OpenRead(address));

                items = new List<string>();
                String[] unique;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    unique = line.Split(',');
                    if (unique[0].Equals(product) && unique[1].Equals(color) && unique[3].Equals(size))
                    {
                        price = unique[2];
                        break;
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {

            }
            return price;
        }

        public String sendConfirmation(string product, string color, string size, string quant, string price, string emailaddress)
        {
            String result = "";
            if (string.IsNullOrEmpty(emailaddress))
            {
                result = "Please enter email address";
            }
            else if (!Regex.IsMatch(emailaddress, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$"))
            {
                result = "Please enter email address in proper format";
            }
            else
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(
                      "thakkarharsh90@gmail.com", "incredibles9");
                MailMessage msg = new MailMessage();
                msg.To.Add(emailaddress);
                msg.From = new MailAddress("thakkarharsh90@gmail.com");
                msg.Subject = "DSOD Assignment 3 Part 3";
                msg.Body = "Please find below your cart details" + "\n" + "\n" +
                    "Product : " + product + "\n" +
                    "Color : " + color + "\n" +
                    "Size: " + size + "\n" +
                    "Quantity : " + quant + "\n" +
                    "Total Price : " + price + "\n";
                client.Send(msg);
                result = "An status email is being sent to the email provided which contains all your cart details.";
            }
            return result;
        }
    }
}
