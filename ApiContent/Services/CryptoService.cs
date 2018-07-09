using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace ApiContent.Services
{
    public class CryptoService : ICryptoService
    {
        public string CryptPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pdkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pdkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        public bool CheckPassword(string suppliedPwd, string savedPwd)
        {
            byte[] hashBytes = Convert.FromBase64String(savedPwd);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pdkdf2 = new Rfc2898DeriveBytes(suppliedPwd, salt, 10000);
            byte[] hash = pdkdf2.GetBytes(20);
            bool ok = true;
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    ok = false;
                    break;
                }
            }
            return ok;
        }
    }
}