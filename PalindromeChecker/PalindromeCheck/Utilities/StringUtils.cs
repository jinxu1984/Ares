using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PalindromeCheck.Utilities
{
    // Summary:
    //     Exposes utility functions for strings
    public class StringUtils
    {
        public static string RemovePunctuations(string input)
        {
            Regex regex = new Regex("[^a-zA-Z0-9]");
            string result = regex.Replace(input, "");

            return result;
        }

        public static TEnum ToEnum<TEnum>(string value, TEnum defaultValue) where TEnum : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            TEnum result;

            bool suceeded = Enum.TryParse(value, true, out result);

            return suceeded ? result : defaultValue;
        }
    }
}
