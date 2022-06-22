using Newtonsoft.Json;
using Sklad_v1_001.FormUsers.SupplyDocumentDelivery;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.HelperGlobal.StoreAPI.Model.SupplyDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.HelperGlobal.StoreAPI
{
    public class Request
    {
        Attributes attributes;
        public SupplyDocumentRequest supplyDocument { get; set; }
        public Request(Attributes _attributes)
        {
            this.attributes = _attributes;
            supplyDocument = new SupplyDocumentRequest();
        }

        public Response GetCommand(Int32 _com)
        {
            Response response = null;
            switch (_com)
            {
                case 1:
                    response = SupplyDocumentPOST(supplyDocument);
                    break;
                case 2:
                    break;
            }
            return response;
        }

        private Response SupplyDocumentPOST(SupplyDocumentRequest _supplyDocumentRequest)
        {
            using (var webClient = new WebClient())
            {
                // Выполняем запрос по адресу и получаем ответ в виде строки
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) =>
                {
                    return true;
                };
                webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                string response = webClient.UploadString(new Uri(@"https://192.168.0.112:60000/api/supplydocument"), "POST", JsonConvert.SerializeObject(_supplyDocumentRequest, Newtonsoft.Json.Formatting.Indented));
                Response resultOUT = JsonConvert.DeserializeObject<Response>(response);
                if (resultOUT != null)
                {
                    if (resultOUT.ErrorCode == 0)
                    {

                    }
                    else
                    {

                    }
                }
                return resultOUT;
            }
        }

        private static X509Certificate2 GetCertificateFromStore(string certificateNumber)
        {
            // Get the certificate store for the current user.          
            X509Store store = new X509Store(StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);
                // Place all certificates in an X509Certificate2Collection object.
                // идентификация по серийному номеру - 0364CFD7000CAE138642975ED9A3C76F79
                X509Certificate2Collection certCollection = store.Certificates;
                // If using a certificate with a trusted root you do not need to FindByTimeValid, instead:
                X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection signingCert = currentCerts.Find(X509FindType.FindBySerialNumber, certificateNumber, false);
                // if (signingCert.Count == 0)
                // return null;
                // Return the first certificate in the collection, has the right name and is current.
                return signingCert[0];
            }
            finally
            {
                store.Close();
            }
        }
    }
}
