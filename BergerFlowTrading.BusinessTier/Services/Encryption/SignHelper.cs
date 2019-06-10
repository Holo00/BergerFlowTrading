using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BergerFlowTrading.BusinessTier.Services.Encryption
{
    public class SignHelper
    {
        public static string GetSignature2(string method, string url, long nonce, string secret, string postData = "")
        {
            string message = method + url + nonce + postData;
            byte[] signatureBytes = hmacsha256(Encoding.UTF8.GetBytes(secret), Encoding.UTF8.GetBytes(message));
            return ByteArrayToString(signatureBytes);
        }

        private static byte[] hmacsha256(byte[] keyByte, byte[] messageBytes)
        {
            using (var hash = new HMACSHA256(keyByte))
            {
                return hash.ComputeHash(messageBytes);
            }
        }

        public static string GetSignature(string method, string url, long nonce, string secret, string postData = "")
        {
            var message = method + url + nonce + postData;
            byte[] bArray = Encoding.ASCII.GetBytes(message);

            string result = ByteArrayToString(SignHMACSHA256(secret, bArray));

            return result;
        }

        public static string CalculateSignature_HMACSHA512(string text, string secretKey)
        {
            using (var hmacsha512 = new HMACSHA512(Encoding.UTF8.GetBytes(secretKey)))
            {
                hmacsha512.ComputeHash(Encoding.UTF8.GetBytes(text));
                return string.Concat(hmacsha512.Hash.Select(b => b.ToString("x2")).ToArray()); // minimalistic hex-encoding and lower case
            }
        }

        public static string CalculateSignature_HMACSHA256(string text, string secretKey)
        {
            using (var hashMaker = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            {
                hashMaker.ComputeHash(Encoding.UTF8.GetBytes(text));
                return string.Concat(hashMaker.Hash.Select(b => b.ToString("x2")).ToArray()); // minimalistic hex-encoding and lower case
            }
        }

        public static byte[] SignHMACSHA256(string key, byte[] data)
        {
            HMACSHA256 hashMaker = new HMACSHA256(Encoding.ASCII.GetBytes(key));
            return hashMaker.ComputeHash(data);
        }

        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        public static long GetNonce()
        {
            DateTime yearBegin = new DateTime(1990, 1, 1);
            return DateTime.UtcNow.Ticks - yearBegin.Ticks;
        }
        //(Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

        public static long GetNonce2()
        {
            DateTime yearBegin = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return ((DateTime.UtcNow.Ticks - yearBegin.Ticks) / 1000) + 3600;
        }

        public static long GetNonce3()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 3600;
        }
    }
}
