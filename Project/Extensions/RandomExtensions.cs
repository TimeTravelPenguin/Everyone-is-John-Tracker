#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohn
// Project: EveryoneIsJohn
// File Name: RandomExtensions.cs
// 
// Current Data:
// 2021-02-08 5:42 PM
// 
// Creation Date:
// 2021-02-08 5:28 PM

#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace EveryoneIsJohn.Extensions
{
  internal static class RandomExtensions
  {
    private static readonly Random Random = new Random();
    private static readonly object SyncLock = new object();

    public static T RandomIn<T>(this IEnumerable<T> collection)
    {
      var enumerable = collection as T[] ?? collection.ToArray();
      var length = enumerable.Count();

      lock (SyncLock)
      {
        return enumerable[Random.Next(length)];
      }
    }
  }
}