using System;
using System.Security.Cryptography;
using System.Text;

namespace Demo.Common.Cryptography
{
    public class Cryptography
    {
        public static string Encrypt(string inputString, string key = "stratatel")
        {
            if (string.IsNullOrEmpty(inputString))
                return string.Empty;

            Byte[] IVector = new Byte[] { 27, 9, 45, 27, 0, 72, 171, 54 };
            Byte[] buffer = Encoding.ASCII.GetBytes(inputString);

            TripleDESCryptoServiceProvider
                tripleDes = new TripleDESCryptoServiceProvider();

            MD5CryptoServiceProvider
                MD5 = new MD5CryptoServiceProvider();

            tripleDes.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            tripleDes.IV = IVector;

            ICryptoTransform ITransform = tripleDes.CreateEncryptor();

            return Convert.ToBase64String(ITransform.TransformFinalBlock(buffer, 0, buffer.Length));
        }

        public static string Decrypt(string inputString, string key = "stratatel")
        {

            if (string.IsNullOrEmpty(inputString))
                return string.Empty;

            Byte[] IVector = new Byte[] { 27, 9, 45, 27, 0, 72, 171, 54 };
            Byte[] buffer = System.Convert.FromBase64String(inputString);

            TripleDESCryptoServiceProvider
                tripleDes = new TripleDESCryptoServiceProvider();

            MD5CryptoServiceProvider
                MD5 = new MD5CryptoServiceProvider();

            tripleDes.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            tripleDes.IV = IVector;

            ICryptoTransform ITransform = tripleDes.CreateDecryptor();

            return Encoding.ASCII.GetString(ITransform.TransformFinalBlock(buffer, 0, buffer.Length));
        }
    }
}
