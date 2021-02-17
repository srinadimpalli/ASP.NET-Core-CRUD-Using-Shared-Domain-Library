using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreFactory.InfraStructure.Extensions
{
    public static class Extensions
    {
        public static string ToCurrency(this decimal? amount)
        {
            if (amount == null) return "";
            return amount.Value.ToCurrency();
        }

        public static string ToCurrency(this decimal amount)
        {
            return string.Format("{0:C0}", amount);
        }

        public static string ToDate(this DateTime? dt)
        {
            if (dt == null) return "";
            return dt.Value.ToDate();
        }

        public static string ToDate(this DateTime dt)
        {
            return string.Format("{0:d}", dt);
        }

        public static int? GetId(this object obj, int? defaultId = null)
        {
            if (obj == null) return defaultId;

            if (int.TryParse(obj.ToString(), out int value))
                return value as int?;

            return defaultId;
        }

        public static int GetInt(this object obj, int defaultId = 0)
        {
            if (obj == null) return defaultId;

            if (int.TryParse(obj.ToString(), out int value))
                return value;

            return defaultId;
        }

        // ** Iterator Pattern

        // foreach iterates over an enumerable collection

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        // Truncates a string and appends ellipsis if beyond a given length

        public static string Ellipsify(this string s, int maxLength)
        {
            if (string.IsNullOrEmpty(s)) return "";
            if (s.Length <= maxLength) return s;

            return s.Substring(0, maxLength) + "...";
        }

        public static bool IsAjax(this HttpRequest request)
        {
            if (request == null) return false;
            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
