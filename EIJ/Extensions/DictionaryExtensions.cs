#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: DictionaryExtensions.cs
// 
// Current Data:
// 2021-02-14 10:32 PM
// 
// Creation Date:
// 2021-02-14 10:29 PM

#endregion

using System.Collections.Generic;

namespace EIJ.Extensions
{
  public static class DictionaryExtensions
  {
    public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
      TValue defaultValue)
    {
      return dictionary.TryGetValue(key, out var foundValue) ? foundValue : defaultValue;
    }
  }
}