using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Helpers.DataStructures
{
    public class DefaultDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : notnull
        where TValue : notnull
    {
        private readonly Dictionary<TKey, TValue> value = new();

        public ICollection<TKey> Keys => this.value.Keys;

        public ICollection<TValue?> Values => this.value.Values!;

        public int Count => this.value.Count;

        public TValue? DefaultValue { get; }

        public TValue? this[TKey key]
        {
            get => this.value.TryGetValue(key, out var v) ? v : DefaultValue;
            set
            {
                if (value is null && DefaultValue is null || value!.Equals(DefaultValue))
                {
                    this.value.Remove(key);
                }
                else
                {
                    this.value[key] = value;
                }
            }
        }

        public DefaultDictionary(TValue? defaultValue = default)
        {
            DefaultValue = defaultValue;
        }

        public void Add(TKey key, TValue? value) => this[key] = value;

        public void Add(KeyValuePair<TKey, TValue?> item) => this[item.Key] = item.Value;

        public void Clear() => this.value.Clear();

        public bool ContainsKey(TKey key) => this.value.ContainsKey(key);
        public bool ContainsValue(TValue value) => this.value.ContainsValue(value);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<TKey, TValue>>)value).GetEnumerator();
        }

        public bool Remove(TKey key) => this.value.Remove(key);

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)value).GetEnumerator();
    }
}