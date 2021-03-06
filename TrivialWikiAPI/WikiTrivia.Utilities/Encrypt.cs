﻿using System.Security.Cryptography;
using System.Text;

namespace WikiTrivia.Utilities
{
    public static class Encrypt
    {
        public static string GetMD5(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                md5.ComputeHash(Encoding.ASCII.GetBytes(text));
                var result = md5.Hash;
                var str = new StringBuilder();
                for (var i = 1; i < result.Length; i++)
                {
                    str.Append(result[i].ToString());
                }
                return str.ToString();
            }
        }
    }
}
