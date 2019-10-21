using System.Collections.Generic;

namespace UIDataBind.Utils.Extensions
{
    public static class CollectionsExtension
    {
        public static TValue Replace<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue newValue)
        {
            if(!dictionary.ContainsKey(key))
                dictionary.Add(key, newValue);
            return dictionary[key] = newValue;
        }
    }
}