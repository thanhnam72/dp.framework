﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace DP.V2.Core.Common.Ultilities
{
    public class TokenSecurityHelper
    {
        #region " [ Initializations & Declarations ] "

        /// <summary>
        /// The alg
        /// </summary>
        private const string Alg = "HmacSHA256";

        /// <summary>
        /// The expiration minutes. Default 10 minutes
        /// </summary>
        private const int ExpirationMinutes = 10;

        #endregion

        /// <summary>
        /// Generates a token to be used in API calls.
        /// The token is generated by hashing a message with a key, using HMAC SHA256.
        /// The message is: username:ip:userAgent:timeStamp
        /// The key is: password:ip:salt
        /// The resulting token is then concatenated with username:timeStamp and the result base64 encoded.
        /// API calls may then be validated by:
        /// 1. Base64 decode the string, obtaining the token, username, and timeStamp.
        /// 2. Ensure the timestamp is not expired.
        /// 2. Lookup the user's password from the db (cached).
        /// 3. Hash the username:ip:userAgent:timeStamp with the key of password:salt to compute a token.
        /// 4. Compare the computed token with the one supplied and ensure they match.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="ip">The ip.</param>
        /// <param name="userAgent">The user agent.</param>
        /// <param name="ticks">The ticks.</param>
        /// <returns>
        /// System.String.
        /// </returns>
        public static string GenerateToken(string username, string password, string ip, string userAgent, long ticks)
        {
            string _hash = string.Join(":", username, ip, userAgent, ticks.ToString());
            string _hashLeft;
            string _hashRight;

            using (HMAC hmac = HMAC.Create(Alg))
            {
                hmac.Key = Encoding.UTF8.GetBytes(PasswordSecurityHelper.GetHashedPassword(password));
                hmac.ComputeHash(Encoding.UTF8.GetBytes(_hash));

                _hashLeft = Convert.ToBase64String(hmac.Hash);
                _hashRight = string.Join(":", username, ticks.ToString());
            }

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", _hashLeft, _hashRight)));
        }

    }
}
