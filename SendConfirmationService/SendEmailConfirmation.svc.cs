using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Net.Mime;
using System.Net.Mail;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

//Reference lib for pdf
//http://pdfsharp.codeplex.com/releases/view/37054





namespace SendConfirmationService
{
    public class Service1 : ISendEmailConfirmation
    {
        //Service Description:
        //This service is the restful service.
        //This service is used to send an email confirmation to the customer, just like people generally receive after their purchase.
        //This service takes as input the address of the customer, email id and the order details; which will be formatted to be contained into a bill.
        //A pdf is also created for the same and is attached to the email and sent to the customer.


        private bool valid = false;

        //This method is used to verify the email address.
        //It returns a boolean saying if the email was valid or not!
        private bool validateEmail(string email)
        {
            bool valid = false;
            if (String.IsNullOrEmpty(email))
                return false;
            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", this.CheckDomain,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (valid)
                return false;

            try
            {
                return Regex.IsMatch(email,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }


        //This method is used to check the domain of the email address.
        private string CheckDomain(Match match)
        {
            IdnMapping mapIDN = new IdnMapping();

            string domain = match.Groups[2].Value;
            try
            {
                domain = mapIDN.GetAscii(domain);
            }
            catch (ArgumentException)
            {
                valid = true;
            }
            return match.Groups[1].Value + domain;
        }


        //This is the method that uses all other methods and produces an email with attachement and send it to the given email id.
        //It returns a string message that says if the email was improper or if the email was sent properly.
        //Also, because multiple pdfs might created for each email sent, the method also deletes the attached pdf after the email is sent.
        public string sendEmail(string to, string orderString, string address)
        {
            bool isEmailValid = this.validateEmail(to);

            if(isEmailValid == false)
            {
                return "Please enter a valid email address";
            }
            string[] displayName = to.Split('@');

            string email = "";
            email =  "ONLINE SHOPPING CART                                     " + DateTime.Now.ToShortDateString() + "\n";
            email += "Estimated Delivery:                                              " + DateTime.Now.AddDays(7).ToShortDateString();
            email += "\n\n\n";
            email += "This is the confirmation that following order has been shipped to the following address:\n";
            email += address + "\n\n";
            email += "Order Summary:\n";
            email += orderString + "\n\n\n";
            email += "                                             See you again!\n";
            email += "                     x x x x x Thank you for shopping x x x x x ";

           try
            {
                MailMessage m = new MailMessage();
                SmtpClient sc = new SmtpClient();
 
                m.From = new MailAddress("dsodasign3@gmail.com", "Nitya Sheth");
                m.To.Add(new MailAddress(to, displayName[1]));

                FileStream fs;
                m.Subject = "ONLINE SHOPPING CART - Order Confirmation";
                m.Body = email;
                if (this.createPDFBill(to, email) == true)
                {//create pdf, attach it and then send!
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + displayName[0]+".pdf", FileMode.Open, FileAccess.Read);
                    Attachment a = new Attachment(fs, displayName[0]+".pdf", MediaTypeNames.Application.Octet);
                    m.Attachments.Add(a);
                    
                    sc.Host = "smtp.gmail.com";
                    sc.Port = 587;
                    sc.Credentials = new System.Net.NetworkCredential("dsodasign3@gmail.com", "Asu1208662984");
                    sc.EnableSsl = true;
                    sc.Send(m);
                    fs.Close(); //need to maintain order: send email and then close file stream!
                }
                else
                { //if somehow pdf is not created, the email is sent anyway!
                    sc.Host = "smtp.gmail.com";
                    sc.Port = 587;
                    sc.Credentials = new System.Net.NetworkCredential("dsodasign3@gmail.com", "Asu1208662984");
                    sc.EnableSsl = true; 
                    sc.Send(m);
                }

                bool aaa = File.Exists(AppDomain.CurrentDomain.BaseDirectory + displayName[0]+".pdf");
                if (aaa)
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + displayName[0] +".pdf");
                return "Email sent successfully!!";
            }
            catch (Exception e)
            {
                return "Something went wrong!";
            }
        }

        
        //This method uses the PDFSharp external library which created pdf.
        //It returns a boolean saying if the pdf was created or not.
        private bool createPDFBill(string to, string orderString)
        {
            string[] displayName = to.Split('@');
            try
            {
                PdfDocument pdf = new PdfDocument();
                PdfPage pdfPage = pdf.AddPage();

                XGraphics graph = XGraphics.FromPdfPage(pdfPage);
                XFont font = new XFont("Verdana", 14, XFontStyle.Bold);
                XTextFormatter tf = new XTextFormatter(graph);

                XRect rect = new XRect(10, 10, pdfPage.Width.Point-10, pdfPage.Height.Point - 10);
                //0, 0, pdfPage.Width.Point, pdfPage.Height.Point
                graph.DrawRectangle(XBrushes.SeaShell, rect);

                tf.DrawString(orderString, font, XBrushes.Black, rect, XStringFormats.TopLeft);

                //graph.DrawString(emailString, font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft); //XStringFormats.Center
                pdf.Save(AppDomain.CurrentDomain.BaseDirectory + displayName[0]+".pdf");
                //Process.sta
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
