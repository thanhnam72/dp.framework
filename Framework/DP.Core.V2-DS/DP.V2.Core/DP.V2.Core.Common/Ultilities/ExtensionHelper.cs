using System;

namespace DP.V2.Core.Common.Ultilities
{
    /// <summary>
    /// Extension class
    /// </summary>
    public static class ExtensionHelper
    {

        #region " [ Properties ] "

        /// <summary>
        /// Default number format #,###
        /// </summary>
        private static readonly string NUMBER_FORMAT = "#,##0";

        /// <summary>
        /// Default date format dd/MM/yyyy
        /// </summary>
        private static readonly string DATE_FORMAT = "dd/MM/yyyy";

        /// <summary>
        /// Default time format HH:mm:ss
        /// </summary>
        private static readonly string TIME_FORMAT = "HH:mm:ss";

        /// <summary>
        /// Default full time format dd/MM/yyyy HH:mm:ss
        /// </summary>
        private static readonly string FULLTIME_FORMAT = "dd/MM/yyyy HH:mm:ss";

        /// <summary>
        /// Viet nam characters
        /// </summary>
        private static string[] VietNamChar = new string[]
       {
           "aAeEoOuUiIdDyY",
           "áàạảãâấầậẩẫăắằặẳẵ",
           "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
           "éèẹẻẽêếềệểễ",
           "ÉÈẸẺẼÊẾỀỆỂỄ",
           "óòọỏõôốồộổỗơớờợởỡ",
           "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
           "úùụủũưứừựửữ",
           "ÚÙỤỦŨƯỨỪỰỬỮ",
           "íìịỉĩ",
           "ÍÌỊỈĨ",
           "đ",
           "Đ",
           "ýỳỵỷỹ",
           "ÝỲỴỶỸ"
       };

        #endregion

        #region " [ Datetime ] "

        /// <summary>
        /// Convert date to string using default format
        /// </summary>
        /// <param name="date"></param>
        /// <returns>return string value based on default date format dd/MM/yyyy</returns>
        public static string DateToString(this DateTime date)
        {
            return date.ToString(DATE_FORMAT);
        }

        /// <summary>
        /// Convert date to string based on format value input
        /// </summary>
        /// <param name="date">date input</param>
        /// <param name="format">format input</param>
        /// <returns>Return string value based on format inputed</returns>
        public static string DateToString(this DateTime date, string format)
        {
            return date.ToString(format);
        }

        /// <summary>
        /// Get distanct between time happen the action to now.
        /// </summary>
        /// <param name="dt">Time happenned (fulltime)</param>
        /// <param name="actionName">Action name</param>
        /// <returns>String</returns>
        public static string TimeAgo(this DateTime dt, string actionName)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return actionName + String.Format(" cách đây {0} {1}",
                years, years == 1 ? "năm" : "năm");
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return actionName + String.Format(" cách đây {0} {1}",
                months, months == 1 ? "tháng" : "tháng");
            }
            if (span.Days > 0)
                return actionName + String.Format(" cách đây {0} {1}",
                span.Days, span.Days == 1 ? "ngày" : "ngày");
            if (span.Hours > 0)
                return actionName + String.Format(" cách đây {0} {1}",
                span.Hours, span.Hours == 1 ? "giờ" : "giờ");
            if (span.Minutes > 0)
                return actionName + String.Format(" cách đây {0} {1}",
                span.Minutes, span.Minutes == 1 ? "phút" : "phút");
            if (span.Seconds > 5)
                return actionName + String.Format(" cách đây {0} giây", span.Seconds);
            if (span.Seconds <= 5)
                return "vừa " + actionName;
            return string.Empty;
        }

        /// <summary>
        /// Return full time format
        /// </summary>
        /// <param name="date"></param>
        /// <returns>dd/MM/yyyy HH:mm:ss</returns>
        public static string FullTimeString(this DateTime date)
        {
            return date.ToString(FULLTIME_FORMAT);
        }

        /// <summary>
        /// Return full time format
        /// </summary>
        /// <param name="date"></param>
        /// <returns>HH:mm:ss</returns>
        public static string TimeString(this DateTime date)
        {
            return date.ToString(TIME_FORMAT);
        }

        #endregion

        #region " [ Number ] "

        /// <summary>
        /// Convert double value to string using default format
        /// </summary>
        /// <param name="value">Input number value</param>
        /// <returns>Return string value based on number format default #,###</returns>
        public static string NumberToString(this double value)
        {
            return value.ToString(NUMBER_FORMAT);
        }

        /// <summary>
        /// Convert double value to string
        /// </summary>
        /// <param name="value">Input number value</param>
        /// <param name="format">format string</param>
        /// <returns>Return string value based on format inputed</returns>
        public static string NumberToString(this double value, string format)
        {
            return value.ToString(format);
        }

        /// <summary>
        /// Convert integer value to string using default format
        /// </summary>
        /// <param name="value">Input number value</param>
        /// <returns>Return string value based on number format default #,###</returns>
        public static string NumberToString(this int value)
        {
            return value.ToString(NUMBER_FORMAT);
        }

        /// <summary>
        /// Convert integer value to string
        /// </summary>
        /// <param name="value">Input number value</param>
        /// <param name="format">format string</param>
        /// <returns>Return string value based on format inputed</returns>
        public static string NumberToString(this int value, string format)
        {
            return value.ToString(format);
        }

        /// <summary>
        /// Convert float value to string using default format
        /// </summary>
        /// <param name="value">Input number value</param>
        /// <returns>Return string value based on number format default #,###</returns>
        public static string NumberToString(this float value)
        {
            return value.ToString(NUMBER_FORMAT);
        }

        /// <summary>
        /// Convert float value to string
        /// </summary>
        /// <param name="value">Input number value</param>
        /// <param name="format">format string</param>
        /// <returns>Return string value based on format inputed</returns>
        public static string NumberToString(this float value, string format)
        {
            return value.ToString(format);
        }

        /// <summary>
        /// Convert decimal value to string using default format
        /// </summary>
        /// <param name="value">Input number value</param>
        /// <returns>Return string value based on number format default #,###</returns>
        public static string NumberToString(this decimal value)
        {
            return value.ToString(NUMBER_FORMAT);
        }

        /// <summary>
        /// Convert decimal value to string
        /// </summary>
        /// <param name="value">Input number value</param>
        /// <param name="format">format string</param>
        /// <returns>Return string value based on format inputed</returns>
        public static string NumberToString(this decimal value, string format)
        {
            return value.ToString(format);
        }

        #endregion

        #region " [ String ] "

        /// <summary>
        /// Convert title string with special character to latin character.
        /// Only use in case url website, title string convert to alias.
        /// 
        /// </summary>
        /// <param name="str">Title</param>
        /// <returns>Remove all special and unicode characters</returns>
        public static string TitleToAlias(this string str)
        {
            return str.ReplaceUnicode().RemoveSpecialChars().Trim().Replace(' ', '-');
        }

        /// <summary>
        /// Replace unicode in string content
        /// </summary>
        /// <param name="strInput">Input string</param>
        /// <returns>Replace all unicode characters</returns>
        public static string ReplaceUnicode(this string strInput)
        {
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                {
                    strInput = strInput.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
            }
            return strInput;
        }

        /// <summary>
        /// Remove all special characters in string content
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Remove all specia characters</returns>
        public static string RemoveSpecialChars(this string str)
        {
            string[] chars = new string[] { ",", "-", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "(", ")", ":", "|", "[", "]", "?", "+", "=", "{", "}", "`", "~", "<", ">" };

            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }
            return str.Trim();
        }

        /// <summary>
        /// Upper the first character in each word in string content
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>String</returns>
        public static string CapitalizeFirstWords(this string str)
        {
            str = str.Trim().ToLower();

            char[] array = str.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

        /// <summary>
        /// Capitalizer first letter
        /// </summary>
        /// <param name="str">String content</param>
        /// <returns></returns>
        public static string CapitalizeFirstLetter(this string str)
        {
            return str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();
        }

        #endregion
    }
}
