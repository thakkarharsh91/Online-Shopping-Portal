using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SendConfirmationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ISendEmailConfirmation
    {
        [WebGet(UriTemplate = "sendEmail?email={email}&order={order}&address={address}")]
        [OperationContract]
        string sendEmail(string email, string order, string address);
    }

}
