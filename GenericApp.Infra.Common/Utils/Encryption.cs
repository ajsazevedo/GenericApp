using System;
using System.Security.Cryptography;
using System.Text;

namespace GenericApp.Infra.Common.Utils
{
    public static class Encryption
    {
        public static string ToSHA256LowerCase(string value)
        {
            return ToSHA256(value).ToLower();
        }

        public static string ToSHA1LowerCase(string value)
        {
            return ToSHA1(value).ToLower();
        }

        public static string ToSHA256(string value)
        {
            byte[] hashValue, messageBytes = new UnicodeEncoding().GetBytes(value);

            using (var SHhash = new SHA256Managed())
            {
                hashValue = SHhash.ComputeHash(messageBytes);
            }

            return BitConverter.ToString(hashValue).Replace("-", string.Empty);
        }

        public static string ToSHA1(string value)
        {
            byte[] hashValue, messageBytes = Encoding.Default.GetBytes(value);

            using (var ShHash = new SHA1Managed())
            {
                hashValue = ShHash.ComputeHash(messageBytes);
            }

            return BitConverter.ToString(hashValue).Replace("-", string.Empty);
        }
    }
}
