#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: RandomHelpers.cs
// 
// Current Data:
// 2021-02-19 8:18 PM
// 
// Creation Date:
// 2021-02-19 8:18 PM

#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace EIJ.Helpers
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

    public static ulong RandomULong(this Random random, ulong min, ulong max)
    {
      if (min > max)
      {
        throw new ArgumentNullException();
      }

      var buf = new byte[8];
      random.NextBytes(buf);
      var longRand = BitConverter.ToUInt64(buf, 0);

      return longRand % (max - min) + min;
    }
  }
}