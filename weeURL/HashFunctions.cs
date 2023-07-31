using System;
using System.Security.Cryptography;

namespace weeURL
{
	public static class HashFunctions
	{
        public static string createShortUrl(string longURL)
        {
            string hash;
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(longURL);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                hash = Convert.ToHexString(hashBytes);
            }

            return hash.Substring(0, 7);
        }
	}   
}

