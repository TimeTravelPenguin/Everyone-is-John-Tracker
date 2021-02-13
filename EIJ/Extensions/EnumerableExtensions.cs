#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: EnumerableExtensions.cs
// 
// Current Data:
// 2021-02-13 7:50 PM
// 
// Creation Date:
// 2021-02-13 7:42 PM

#endregion

using System;
using System.Collections.Generic;

namespace EIJ.Extensions
{
  internal static class EnumerableExtensions
  {
    public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> collection, int n)
    {
      // Source: https://stackoverflow.com/a/3453310

      if (collection == null)
      {
        throw new ArgumentNullException(nameof(collection));
      }

      if (n < 0)
      {
        throw new ArgumentOutOfRangeException(nameof(n), $"{nameof(n)} must be 0 or greater");
      }

      var temp = new LinkedList<T>();

      foreach (var value in collection)
      {
        temp.AddLast(value);
        if (temp.Count > n)
        {
          temp.RemoveFirst();
        }
      }

      return temp;
    }
  }
}