using System;
using System.Security.Cryptography;
using System.Text;

namespace DP.V2.Core.Common.Ultilities
{
    /// <summary>
    /// MD5 helper functions
    /// </summary>
    public static class Md5Helper
    {
        #region " [ Encrypt Data ] "

        /// <summary>
        /// Encrypts the data.
        /// </summary>
        /// <param name="data">The message.</param>
        /// <returns>
        /// encrypted data
        /// </returns>
        public static string EncryptData(string data)
        {
            return CommonMethodForEncryptData(data, "x2");
        }

        /// <summary>
        /// Encrypts the data.
        /// </summary>
        /// <param name="data">The message.</param>
        /// <param name="lockKey">The lock Key.</param>
        /// <returns>
        /// encrypted data
        /// </returns>
        public static string EncryptData(string data, string lockKey)
        {
            return CommonMethodForEncryptData(data, lockKey);
        }

        /// <summary>
        /// Common Method For Encrypts the data.
        /// </summary>
        /// <param name="data">The message.</param>
        /// <param name="lockKey">The lock Key.</param>
        /// <returns>
        /// encrypted data
        /// </returns>
        private static string CommonMethodForEncryptData(string data, string lockKey)
        {
            byte[] _results;
            var _utf8 = new UTF8Encoding();
            using (var hashProvider = new MD5CryptoServiceProvider())
            {
                var _tdesKey = hashProvider.ComputeHash(_utf8.GetBytes(lockKey));
                using (var tdesAlgorithm = new TripleDESCryptoServiceProvider())
                {
                    tdesAlgorithm.Key = _tdesKey;
                    tdesAlgorithm.Mode = CipherMode.ECB;
                    tdesAlgorithm.Padding = PaddingMode.PKCS7;
                    byte[] dataToEncrypt = _utf8.GetBytes(data);
                    try
                    {
                        var encryptor = tdesAlgorithm.CreateEncryptor();
                        _results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
                    }
                    finally
                    {
                        tdesAlgorithm.Clear();
                        hashProvider.Clear();
                    }
                }
            }

            return Convert.ToBase64String(_results);
        }

        #endregion

        #region " [ Decrypt Data ] "

        /// <summary>
        /// Decrypts the data.
        /// </summary>
        /// <param name="data">The message.</param>
        /// <returns>
        /// decrypted data
        /// </returns>
        public static string DecryptData(string data)
        {
            return CommonMethodForDecryptData(data, "x2");
        }

        /// <summary>
        /// Decrypts the data.
        /// </summary>
        /// <param name="data">The message.</param>
        /// <param name="lockKey">The lock Key.</param>
        /// <returns>
        /// decrypted data
        /// </returns>
        public static string DecryptData(string data, string lockKey)
        {
            return CommonMethodForDecryptData(data, lockKey);
        }

        /// <summary>
        /// Decrypts the data.
        /// </summary>
        /// <param name="data">The message.</param>
        /// <param name="lockKey">The lock Key.</param>
        /// <returns>
        /// decrypted data
        /// </returns>
        private static string CommonMethodForDecryptData(string data, string lockKey)
        {
            byte[] _results;
            var _utf8 = new UTF8Encoding();
            using (var hashProvider = new MD5CryptoServiceProvider())
            {
                var _tdesKey = hashProvider.ComputeHash(_utf8.GetBytes(lockKey));
                using (var tdesAlgorithm = new TripleDESCryptoServiceProvider())
                {
                    tdesAlgorithm.Key = _tdesKey;
                    tdesAlgorithm.Mode = CipherMode.ECB;
                    tdesAlgorithm.Padding = PaddingMode.PKCS7;
                    var dataToDecrypt = Convert.FromBase64String(data);
                    try
                    {
                        ICryptoTransform decryptor = tdesAlgorithm.CreateDecryptor();
                        _results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
                    }
                    finally
                    {
                        tdesAlgorithm.Clear();
                        hashProvider.Clear();
                    }
                }
            }

            return _utf8.GetString(_results);
        }

        #endregion

    }
}
