using Sklad_v1_001.Control.FlexMessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Sklad_v1_001.HelperGlobal.MessageBoxTitleHelper;

namespace Sklad_v1_001.Crypto
{
    public class CryptDecrypt
    {
        private string additionalPassword;
        private string secretKey;

        public CryptDecrypt()
        {
            additionalPassword = "AbcdeFghi";
            secretKey = "Boris";
        }

        public string SecretKey
        {
            get
            {
                return secretKey;
            }

            set
            {
                secretKey = value;
            }
        }

        public string AdditionalPassword
        {
            get
            {
                return additionalPassword;
            }

            set
            {
                additionalPassword = value;
            }
        }

        private byte[] GetKey(string secretKey)
        {
            string sk = null;
            string fullsecretKey = String.Concat(secretKey, AdditionalPassword);
            if (Encoding.UTF8.GetByteCount(fullsecretKey) < 24)
            {
                sk = fullsecretKey.PadRight(24, ' ');
            }
            else
            {
                sk = fullsecretKey.Substring(0, 24);
            }
            return Encoding.UTF8.GetBytes(sk);
        }

        public string Encrypt(string data)
        {
            using (var des = new TripleDESCryptoServiceProvider { Mode = CipherMode.ECB, Key = GetKey(secretKey), Padding = PaddingMode.PKCS7 })
            using (var desEncrypt = des.CreateEncryptor())
            {
                var buffer = Encoding.UTF8.GetBytes(data);
                return Convert.ToBase64String(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            }
        }

        public string Decrypt(string data)
        {
            data = data.Replace(" ", "+");
            using (var des = new TripleDESCryptoServiceProvider { Mode = CipherMode.ECB, Key = GetKey(secretKey), Padding = PaddingMode.PKCS7 })
            using (var desEncrypt = des.CreateDecryptor())
            {
                try
                {
                    var buffer = Convert.FromBase64String(data);
                    return Encoding.UTF8.GetString(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
                }
                catch
                {
                    FlexMessageBox mb = new FlexMessageBox();
                    mb.Show(Properties.Resources.ErrorCryptPassword, GenerateTitle(TitleType.Error, Properties.Resources.BadPassword), MessageBoxButton.OK, MessageBoxImage.Error);
                    return "";
                }
            }
        }

        public Boolean checkValid(string etalonPassword, string checkedPassword)
        {
            return etalonPassword == Encrypt(checkedPassword);
        }

        public string EncryptCashBox(string data)
        {
            using (var des = new TripleDESCryptoServiceProvider { Mode = CipherMode.ECB, Key = GetKey(secretKey), Padding = PaddingMode.PKCS7 })
            using (var desEncrypt = des.CreateEncryptor())
            {
                var buffer = Encoding.UTF8.GetBytes(data);
                String cryptString = Convert.ToBase64String(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
                byte[] bytes = new byte[cryptString.Length * sizeof(char)];
                System.Buffer.BlockCopy(cryptString.ToCharArray(), 0, bytes, 0, bytes.Length);
                cryptString = bytes.Aggregate(0, (a, b) => a + b).ToString();

                return cryptString;
            }
        }
        public Boolean checkValidCashBox(string etalonPassword, string checkedPassword)
        {
            return etalonPassword == EncryptCashBox(checkedPassword);
        }
    }
}
