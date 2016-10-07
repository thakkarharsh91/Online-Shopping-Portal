using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopping
{
    public class Person
    {
        public string _userName;
        public string _orderSummary;
        public string _address;
        public Person(string name)
        {
            _userName = name;
        }

        public Person(string username, string orderSumary, string address)
        {
            _userName = username;
            _orderSummary = orderSumary;
            _address = address;
        }
    }
}