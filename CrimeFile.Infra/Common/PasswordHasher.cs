using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CrimeFile.Infra.Common
{
    class PasswordHasher
    {
        public string HashPassword(string password)
        {
            MD5 hash = MD5.Create();
            byte[] data = hash.ComputeHash(Encoding.Default.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("X"));
            }
            string hashedPassword = builder.ToString();
            return hashedPassword;
        }
    }
}
