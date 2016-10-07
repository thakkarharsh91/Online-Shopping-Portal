using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace EncryptDecrypt
{
    public class EncryptDecrypt
    {
        private static byte[] secretKey = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static byte[] initVector = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

        public string encrypt(string plainMessage) {
            SymmetricAlgorithm desAlgorithm = DES.Create();
            ICryptoTransform cryptTransform = desAlgorithm.CreateEncryptor(secretKey, initVector);
            byte[] ibuffer = Encoding.Unicode.GetBytes(plainMessage);
            byte[] oBuffer = cryptTransform.TransformFinalBlock(ibuffer, 0, ibuffer.Length);
            return Convert.ToBase64String(oBuffer);
        
        }

        public string decrypt(string encrtyptedMessage) {
           
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform cryptTransform = algorithm.CreateDecryptor(secretKey, initVector);
            byte[] ibuffer = Convert.FromBase64String(encrtyptedMessage);
            byte[] oBuffer = cryptTransform.TransformFinalBlock(ibuffer, 0, ibuffer.Length);
            return Encoding.Unicode.GetString(oBuffer);
        }
    }
}