using System.Collections.Generic;

namespace ReactiveUI.Samples.Wpf.Common
{
    public static class DictionaryExtension
    {
        public static TValue NextValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            int index = new List<TKey>(dictionary.Keys).IndexOf(key);
            if (index >= 0 && index < dictionary.Count - 1)
            {
                var nextKey = new List<TKey>(dictionary.Keys)[index + 1];
                return dictionary[nextKey];
            }
            else
            {
                return default(TValue);
            }
        }

        public static TKey NextKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            int index = new List<TKey>(dictionary.Keys).IndexOf(key);
            if (index >= 0 && index < dictionary.Count - 1)
            {
                var nextKey = new List<TKey>(dictionary.Keys)[index + 1];
                return nextKey;
            }
            else
            {
                return default(TKey);
            }
        }
    }
}