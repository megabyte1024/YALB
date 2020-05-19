using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YALB
{
    public static class DefaultRawStringParser
    {
        private const string _unrecognizedKey = "Unrecognized";

        public static Dictionary<string, string> ParseWithLinq(string rawData, string keyValueDelimiter = ":", string pairDelimiter = ";")
            => rawData?.Split(pairDelimiter)
            .Select(x => x.Split(keyValueDelimiter, StringSplitOptions.RemoveEmptyEntries))
            .Select(x => x.Length == 2 ? new { Key = x[0].Trim(), Value = x[1].Trim() } : new { Key = _unrecognizedKey, Value = x[0].Trim() })
            .ToDictionary(x => x.Key, x => x.Value)
            ?? new Dictionary<string, string>();

        public static List<KeyValuePair<string, string>> ParseFairWithLinq(string rawData, string keyValueDelimiter = ":", string pairDelimiter = ";")
            => rawData.Split(pairDelimiter)
            .Select(x => x.Split(keyValueDelimiter, StringSplitOptions.RemoveEmptyEntries))
            .Select(x => x.Length == 2 ? new KeyValuePair<string, string>(x[0].Trim(), x[1].Trim()) : new KeyValuePair<string, string>(_unrecognizedKey, x[0].Trim()))
            .ToList();

        public static IEnumerable<KeyValuePair<string, string>> ParseWithLinq2(string rawData, string keyValueDelimiter = ":", string pairDelimiter = ";")
            => rawData.Split(pairDelimiter)
            .Select(x => x.Split(keyValueDelimiter, StringSplitOptions.RemoveEmptyEntries))
            .Select(x => x.Length == 2 ? new KeyValuePair<string, string>(x[0].Trim(), x[1].Trim())
                                       : new KeyValuePair<string, string>(_unrecognizedKey, x[0].Trim()));

        public static Dictionary<string, string> ParseWithoutLinq(string rawData, string keyValueDelimiter = ":", string pairDelimiter = ";")
        {
            var result = new Dictionary<string, string>();
            var splitted = rawData?.Split(pairDelimiter);
            foreach (var item in splitted)
            {
                var pair = item.Split(keyValueDelimiter, StringSplitOptions.RemoveEmptyEntries);
                if (pair.Length == 2)
                    result.Add(pair[0].Trim(), pair[1].Trim());
                else
                    result.Add(_unrecognizedKey, pair[0].Trim());
            }
            return result;
        }
        public static List<KeyValuePair<string, string>> ParseFairWithoutLinq(string rawData, string keyValueDelimiter = ":", string pairDelimiter = ";")
        {
            var result = new List<KeyValuePair<string, string>>();
            var splitted = rawData?.Split(pairDelimiter);
            foreach (var item in splitted)
            {
                var pair = item.Split(keyValueDelimiter, StringSplitOptions.RemoveEmptyEntries);
                if (pair.Length == 2)
                    result.Add(new KeyValuePair<string, string>(pair[0].Trim(), pair[1].Trim()));
                else
                    result.Add(new KeyValuePair<string, string>(_unrecognizedKey, pair[0].Trim()));
            }
            return result;
        }
    }
}
