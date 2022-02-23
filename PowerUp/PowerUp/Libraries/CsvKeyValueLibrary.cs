using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerUp.Libraries
{
  public abstract class CsvKeyValueLibrary<TKey, TValue>
    where TKey : struct
    where TValue : struct
  {
    private readonly IDictionary<TKey, TValue> _valuesByKey;
    private readonly IDictionary<TValue, TKey> _keysByValue;

    protected abstract TKey ParseKey(string keyString);
    protected abstract TValue ParseValue(string valueString);

    protected abstract TValue? OnKeyNotFound(TKey key);
    protected abstract TKey? OnValueNotFound(TValue value);

    public CsvKeyValueLibrary(string libraryFilePath)
    {
      var keyValuePairs = File.ReadAllLines(libraryFilePath)
        .Select(l => l.Split(','))
        .Select(l => new KeyValuePair<TKey, TValue>(ParseKey(l[0]), ParseValue(l[1])));

      _valuesByKey = keyValuePairs.ToDictionary(p => p.Key, p => p.Value);
      _keysByValue = keyValuePairs.ToDictionary(p => p.Value, p => p.Key);
    }

    public TValue? this[TKey key] => _valuesByKey.TryGetValue(key, out var value) 
      ? value 
      : OnKeyNotFound(key);

    public TKey? this[TValue value] => _keysByValue.TryGetValue(value, out var key)
      ? key
      : OnValueNotFound(value);

  }
}
