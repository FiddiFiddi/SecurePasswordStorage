using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Entities.Util
{
    public class CryptoManager
    {
        private const int SALT_SIZE = 12;
        private const int HASH_SIZE = 24;
        private const int ITERATIONS = 10000;
        private static readonly char delimiter = ':';

        // Make this work with hash password to divide code
        //private static byte[] GenerateHash(string input)
        //{
        //    RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        //    byte[] salt = new byte[SALT_SIZE];
        //    provider.GetBytes(salt);

        //    Rfc2898DeriveBytes pdkdf2 = new Rfc2898DeriveBytes(input, salt, ITERATIONS);
        //    return pdkdf2.GetBytes(HASH_SIZE);
        //}

        public static byte[] GenerateSalt()
        {
            var salt = new byte[SALT_SIZE];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        /// <summary>
        /// Hashed a password with a random salt corresponding to the user
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string HashPassword(string password, byte[] salt = null)
        {
            if (salt == null) salt = GenerateSalt(); // Dynamic Salt

            // TODO Change to actually use GenerateHash function ^ works but is ugly
            var hash = Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, ITERATIONS, HASH_SIZE));
            return hash;
        }

        /// <summary>
        /// Splits Password, and gets salt to checksum on new password hash and database password hash
        /// </summary>
        /// <param name="clientInput">Inputted Password eks.</param>
        /// <param name="hashedDBInput">hashed password from database</param>
        /// <returns></returns>
        public static bool CheckPassword(string clientInput, string hashedDBInput)
        {
            // Create a splitter, to get the salt from the beginning of password field
            var splitPassword = hashedDBInput.Split(delimiter);

            var salt = Convert.FromBase64String(splitPassword[0]); // Gets users salt
            //Hash the inputted password
            var hashedClientInput = HashPassword(clientInput, salt);

            if(hashedDBInput == hashedClientInput)
            {
                return true;
            }
            return false;
        }
    }
}
