using System;
using System.Security.Cryptography;
using System.Text;

namespace DP.V2.Core.Common.Ultilities
{
    public class PasswordSecurityHelper
    {
        #region " [ Initializations & Declarations ] "

        /// <summary>
        /// The alg
        /// </summary>
        private const string Alg = "HmacSHA256";

        /// <summary>
        /// The salt
        /// </summary>
        private const string Salt = "rz8LuOtFBXphj9WQfvFh";

        #endregion

        /// <summary>
        /// Returns a hashed password + salt, to be used in generating a token.
        /// </summary>
        /// <param name="password">string - user's password</param>
        /// <returns>
        /// string - hashed password
        /// </returns>
        public static string GetHashedPassword(string password)
        {
            string _key = string.Join(":", password, Salt);

            using (HMAC hmac = HMAC.Create(Alg))
            {
                // Hash the key.
                hmac.Key = Encoding.UTF8.GetBytes(Salt);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(_key));

                StringBuilder _builder = new StringBuilder();
                foreach (byte num in hmac.Hash)
                {
                    _builder.AppendFormat("{0:X2}", num);
                }

                var _convertBuilder = Encoding.UTF8.GetBytes(_builder.ToString());

                //return Convert.ToBase64String(hmac.Hash);
                return Convert.ToBase64String(_convertBuilder);
            }
        }

    }
}
