using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CreditCardVerification
{
    [ServiceContract]
    public interface IService1
    {

        [WebGet(UriTemplate = "creditCardVerification?cardType={cardType}&creditCardNumber={creditCardNumber}&date={date}&cvv={cvv}&name={name}&email={email}")]
        [OperationContract]
        string creditCardVerification(string cardType, string creditCardNumber, string date, string cvv, string name, string email);
    }
}
