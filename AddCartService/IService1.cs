using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AddCartService
{
    
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<string> getProducts();

        [OperationContract]
        List<string> getColors(string product);

        [OperationContract]
        List<string> getSizes(string product, string color);

        [OperationContract]
        String getQuantity(string product, string color, string size);

        [OperationContract]
        String getPrice(string product, string color, string size);

        [OperationContract]
        String sendConfirmation(string product, string color, string size, string quant, string price, string emailaddress);
    }
}
