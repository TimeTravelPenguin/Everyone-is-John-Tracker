#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: RandomExtensions.cs
// 
// Current Data:
// 2021-02-13 7:50 PM
// 
// Creation Date:
// 2021-02-13 7:42 PM

#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace EIJ.Extensions
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