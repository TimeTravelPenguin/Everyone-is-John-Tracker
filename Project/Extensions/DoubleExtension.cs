#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohn
// Project: EveryoneIsJohn
// File Name: DoubleExtension.cs
// 
// Current Data:
// 2021-02-12 11:53 PM
// 
// Creation Date:
// 2021-02-12 11:53 PM

#endregion

using System;

namespace EveryoneIsJohn.Extensions
{
  internal static class DoubleExtension
  {
    public static bool IsZero(this double value)
    {
      return value.IsEqualTo(0.0);
    }

    public static bool IsEqualTo(this double lhs, double rhs)
    {
      return Math.Abs(lhs - rhs) < double.Epsilon;
    }

    public static bool IsGreaterThanZero(this double lhs)
    {
      return Math.Abs(lhs) > 0;
    }
  }
}