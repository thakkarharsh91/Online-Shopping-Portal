using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;

namespace CreditCardVerification
{
    public class Service1 : IService1
    {
        public string creditCardVerification(string cardTypeDropdown, string creditCardNumber, string date, string cvv, string name, string email)
        {
            string result = "";
            string test = creditCardNumber[0].ToString();
            if (cardTypeDropdown.Equals("0"))
            {
                result = "Please select the type of card";
            }
            else if (string.IsNullOrEmpty(creditCardNumber) || string.IsNullOrEmpty(date) ||
                string.IsNullOrEmpty(cvv) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            {
                result = "All fields are mandatory";
            }
            else if (!Regex.IsMatch(creditCardNumber, "^[0-9]+$") || !Regex.IsMatch(date, "^[0-9]+$")
            || !Regex.IsMatch(cvv, "^[0-9]+$"))
            {
                result = "Only numeric digits allowed for card number,expiry date and cvv field";
            }
            else if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                result = "Only letters allowed for name field with no spaces";
            }
            else if (creditCardNumber.Length != 16)
            {
                result = "Credit card should be a 16 digit number";
            }
            else if (date.Length != 6)
            {
                result = "Expiry date should include both month and year";
            }
            else if (cvv.Length != 3)
            {
                result = "Please enter proper cvv of 3 number";
            }
            else if (!Regex.IsMatch(email, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$"))
            {
                result = "Please enter email address in proper format";
            }
            else if (cardTypeDropdown.Equals("1") && !creditCardNumber[0].ToString().Equals("2"))
            {
                result = "You selected enRoute as the cardtype but the card belongs to some different vendor.";
            }
            else if (cardTypeDropdown.Equals("2") && !creditCardNumber[0].ToString().Equals("3"))
            {
                result = "You selected AMEX/Diners Club/JCB as the cardtype but the card belongs to some different vendor.";
            }
            else if (cardTypeDropdown.Equals("3") && !creditCardNumber[0].ToString().Equals("4"))
            {
                result = "You selected VISA as the cardtype but the card belongs to some different vendor.";
            }
            else if (cardTypeDropdown.Equals("4") && !creditCardNumber[0].ToString().Equals("5"))
            {
                result = "You selected MasterCard as the cardtype but the card belongs to some different vendor.";
            }
            else if (cardTypeDropdown.Equals("5") && !creditCardNumber[0].ToString().Equals("6"))
            {
                result = "You selected Discover as the cardtype but the card belongs to some different vendor.";
            }
            else
            {
                DateTime localDate = DateTime.Now;
                String monthNow = DateTime.Now.Month.ToString();
                String yearNow = DateTime.Now.Year.ToString();
                String month = date.Substring(0, 2);
                String year = date.Substring(2, 4);
                if (Int32.Parse(month) < Int32.Parse(monthNow) && Int32.Parse(year) < Int32.Parse(yearNow))
                {
                    result = "Please enter valid expiry date";
                }
                else
                {
                    int count = 0;
                    int[] doubleArray = new int[creditCardNumber.Length / 2];
                    for (int i = creditCardNumber.Length - 2; i >= 0; i -= 2)
                    {
                        int data = int.Parse(creditCardNumber[i].ToString());
                        doubleArray[count] = data * 2;
                        count++;
                    }

                    int totalPart1 = 0;
                    foreach (int i in doubleArray)
                    {
                        string number = i.ToString();
                        for (int j = 0; j < number.Length; j++)
                        {
                            totalPart1 += int.Parse(number[j].ToString());
                        }
                    }

                    int totalPart2 = 0;
                    for (int i = creditCardNumber.Length - 1; i >= 0; i -= 2)
                    {
                        totalPart2 += int.Parse(creditCardNumber[i].ToString());
                    }

                    int total = totalPart1 + totalPart2;

                    if (total % 10 == 0)
                    {
                        string cardType;
                        switch (creditCardNumber.Substring(0, 1))
                        {
                            case "2":
                                cardType = "enRoute";
                                break;
                            case "3":
                                cardType = "AMEX/Diners Club/JCB";
                                break;
                            case "4":
                                cardType = "VISA";
                                break;
                            case "5":
                                cardType = "MasterCard";
                                break;
                            case "6":
                                cardType = "Discover";
                                break;
                            default:
                                cardType = "Unknown";
                                break;
                        }
                        try
                        {
                            result = "An status email is being sent to the email provided which contains all your credit card details.";
                        }
                        catch (Exception exp)
                        {
                            result = "Something wrong happened while sending email. Please try again.";
                        }
                    }
                    else
                    {
                        result = "The card is invalid. Please try again or enter different card details.";
                    }
                }
            }
            return result;
        }
    }
}
