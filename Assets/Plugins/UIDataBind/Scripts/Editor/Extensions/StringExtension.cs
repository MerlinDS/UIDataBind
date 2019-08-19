using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Plugins.UIDataBind.Editor.Extensions
{
    public static class StringExtension
    {
        private const string PropertyNamePatters = @"^_?(\w+)Property?$";
        private const RegexOptions PropertyOptions = RegexOptions.Singleline;

        public static IEnumerable<string> ConvertToHumanReadtable(this IEnumerable<string> strings) =>
            strings.Select(s => s.ConvertToHumanReadtable());

        public static string ConvertToHumanReadtable(this string @string) => @string
            .RemovePropertyPrefixAndPostfix()
            .FirstCharToUpper();

        private static string RemovePropertyPrefixAndPostfix(this string @string)
        {
            var matches = Regex.Matches(@string, PropertyNamePatters, PropertyOptions);
            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1)
                    return match.Groups[1].Value;
            }

            return @string;
        }

        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }
    }
}