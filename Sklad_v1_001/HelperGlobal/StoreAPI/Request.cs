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
        public SupplyDocumentRequestList supplyDocumentRequestList { get; set; }
        public Request(Attributes _attributes)
        {
            this.attributes = _attributes;
            supplyDocument = new SupplyDocumentRequest();
            supplyDocumentRequestList = new SupplyDocumentRequestList();
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
                    response = SupplyDocumentGet(supplyDocument);
                    break;
                case 3:
                    response = SupplyDocumentListPOST(supplyDocumentRequestList);
                    break;
            }
            return response;
        }

        private Response SupplyDocumentPOST(SupplyDocumentRequest _supplyDocumentRequest)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    // Выполняем запрос по адресу и получаем ответ в виде строки
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) =>
                    {
                       // GetCertificateFromStore("");
                        return true;
                    };
                    webClient.Encoding = System.Text.Encoding.UTF8;
                    //webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string temp = JsonConvert.SerializeObject(_supplyDocumentRequest, Newtonsoft.Json.Formatting.Indented);
                    string response = webClient.UploadString(new Uri(@"https://192.168.0.126:60000/api/supplydocument/Post"), "POST", temp);
                    Response resultOUT = JsonConvert.DeserializeObject<Response>(response);
                    if (resultOUT != null)
                    {
                        return resultOUT;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Response resultOUT = new Response();
                resultOUT.ErrorCode = -1;
                resultOUT.DescriptionEX = ex.Message;
                return resultOUT;
            }
            
        }

        private Response SupplyDocumentListPOST(SupplyDocumentRequestList _supplyDocumentRequestList)
        {
            try
            {
                Response response = new Response();
                using (var webClient = new WebClient())
                {
                    // Выполняем запрос по адресу и получаем ответ в виде строки
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) =>
                    {
                        // GetCertificateFromStore("");
                        return true;
                    };
                    webClient.Encoding = System.Text.Encoding.UTF8;
                    //webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string temp = JsonConvert.SerializeObject(_supplyDocumentRequestList, Newtonsoft.Json.Formatting.Indented);
                    string respon = webClient.UploadString(new Uri(@"https://192.168.0.126:60000/api/supplydocument/ListPost"), "POST", temp);
                    response = JsonConvert.DeserializeObject<Response>(respon);
                    if (response.listSupplyDocumentOutput != null)
                    {
                        return response;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Response resultOUT = new Response();
                resultOUT.ErrorCode = -1;
                resultOUT.DescriptionEX = ex.Message;
                return resultOUT;
            }

        }

        private Response SupplyDocumentGet(SupplyDocumentRequest _supplyDocumentRequest)
        {
            Response response = new Response();
            try
            {
                using (var webClient = new WebClient())
                {
                    
                    // Выполняем запрос по адресу и получаем ответ в виде строки
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) =>
                    {
                        // GetCertificateFromStore("");
                        return true;
                    };
                    webClient.Encoding = System.Text.Encoding.UTF8;
                    //webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                    string responseString = webClient.DownloadString(new Uri(@"https://192.168.0.126:60000/api/supplydocument/"+ _supplyDocumentRequest.Document.Status.ToString()));               
                    response.listSupplyDocumentOutput = JsonConvert.DeserializeObject<SupplyDocumentRequestList>(responseString);
                    if (response.listSupplyDocumentOutput != null)
                    {                      
                        return response;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Response resultOUT = new Response();
                resultOUT.ErrorCode = -1;
                resultOUT.DescriptionEX = ex.Message;
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
