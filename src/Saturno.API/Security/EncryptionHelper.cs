using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;

namespace Saturno.API.Security
{
    public static class EncryptionHelper
    {
        public static string PasswordEncrypt(string password)
        {
            byte[] salt = Encoding.ASCII.GetBytes("de8435808cbe0fc9b33f7def2d4ecc9c09fb8f3d");

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested: 256 / 8));
        }
    }
}
