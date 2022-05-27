using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Threading;

namespace PosBackup.Helper.Ftp
{
    public class FtpClient
    {
        private string password;
        private string userName;
        private string uri;
        private int bufferSize = 1024;

        public bool Passive = true;
        public bool Binary = true;
        public bool EnableSsl = false;
        public bool Hash = false;
        public bool ErrorDownload = false;

        public FtpClient(string uri, string userName, string password)
        {
            this.uri = uri;
            this.userName = userName;
            this.password = password;
        }

        public string ChangeWorkingDirectory(string path)
        {
            uri = combine(uri, path);
            return PrintWorkingDirectory();
        }

        public string DeleteFile(string fileName)
        {
            var request = createRequest(combine(uri, fileName), WebRequestMethods.Ftp.DeleteFile);
            return getStatusDescription(request);
        }

        public string DownloadFile(string source, string dest)
        {
            var a = combine(uri, source);
            var request = createRequest(combine(uri, source), WebRequestMethods.Ftp.DownloadFile);
            byte[] buffer = new byte[bufferSize];
            using (var response = (FtpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var fs = new FileStream(dest, FileMode.OpenOrCreate))
                    {
                        int readCount = stream.Read(buffer, 0, bufferSize);
                        while (readCount > 0)
                        {
                            if (Hash)
                                Console.Write("#");

                            fs.Write(buffer, 0, readCount);
                            readCount = stream.Read(buffer, 0, bufferSize);
                        }
                    }
                }
                return response.StatusDescription;
            }
        }

        public DateTime GetDateTimestamp(string fileName)
        {
            var request = createRequest(combine(uri, fileName), WebRequestMethods.Ftp.GetDateTimestamp);
            using (var response = (FtpWebResponse)request.GetResponse())
            {
                return response.LastModified;
            }
        }

        public Boolean CheckFile(string fileName)
        {
            try
            {
                var request = createRequest(combine(uri, fileName), WebRequestMethods.Ftp.GetFileSize);
                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public long GetFileSize(string fileName)
        {
            var request = createRequest(combine(uri, fileName), WebRequestMethods.Ftp.GetFileSize);
            using (var response = (FtpWebResponse)request.GetResponse())
            {
                return response.ContentLength;
            }
        }

        public ObservableCollection<Shop> ListDirectoryShop()
        {
            var list = new ObservableCollection<Shop>();
            var request = createRequest(WebRequestMethods.Ftp.ListDirectory);

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream, true))
                    {
                        while (!reader.EndOfStream)
                        {
                            Shop shop = new Shop();
                            String shopString = reader.ReadLine();
                            shop.CompanyID = shopString.Split('_')[0];
                            shop.ShopID = shopString.Split('_')[1];
                            list.Add(shop);
                        }
                    }
                }
            }
            return list;
        }

        public string[] ListDirectory()
        {
            var list = new List<string>();
            var request = createRequest(WebRequestMethods.Ftp.ListDirectory);

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream, true))
                    {
                        while (!reader.EndOfStream)
                        {
                            list.Add(reader.ReadLine());
                        }
                    }
                }
            }
            return list.ToArray();
        }

        public string[] ListDirectoryDetails()
        {
            var list = new List<string>();
            var request = createRequest(WebRequestMethods.Ftp.ListDirectoryDetails);
            using (var response = (FtpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream, true))
                    {
                        while (!reader.EndOfStream)
                        {
                            list.Add(reader.ReadLine());
                        }
                    }
                }
            }
            return list.ToArray();
        }

        public string PrintWorkingDirectory()
        {
            var request = createRequest(WebRequestMethods.Ftp.PrintWorkingDirectory);
            return getStatusDescription(request);
        }

        private FtpWebRequest createRequest(string method)
        {
            return createRequest(uri, method);
        }

        private FtpWebRequest createRequest(string uri, string method)
        {
            var r = (FtpWebRequest)WebRequest.Create(uri);
            r.Credentials = new NetworkCredential(userName, password);
            r.Method = method;
            r.UseBinary = Binary;
            r.EnableSsl = EnableSsl;
            r.UsePassive = Passive;
            r.Proxy = null;

            return r;
        }

        private string getStatusDescription(FtpWebRequest request)
        {
            using (var response = (FtpWebResponse)request.GetResponse())
            {
                return response.StatusDescription;
            }
        }

        private string combine(string path1, string path2)
        {
            return Path.Combine(path1, path2).Replace("\\", "/");
        }

        Boolean IsFile(NetworkCredential credentials, String fileUrl)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(fileUrl);
            request.Proxy = null;
            request.Credentials = credentials;
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            try
            {
                //logger.SendMessage("IsFile:"+ fileUrl);
                using (var response = (FtpWebResponse)request.GetResponse())
                using (var responseStream = response.GetResponseStream())
                {
                    return true;
                }
            }
            catch (WebException ex)
            {
                return false;
            }
        }


        long GetFileSize(NetworkCredential credentials, String fileUrl)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(fileUrl);
            request.Proxy = null;
            request.Credentials = credentials;
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            long size = response.ContentLength;
            response.Close();
            return size;
        }
    }
}
