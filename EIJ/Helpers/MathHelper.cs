#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: MathHelper.cs
// 
// Current Data:
// 2021-02-19 8:32 PM
// 
// Creation Date:
// 2021-02-16 5:23 PM

#endregion

using System;
using System.Collections.Generic;
using MathNet.Numerics;

namespace EIJ.Helpers
{
  public static class MathHelper
  {
    public static ulong NChooseK(ulong n, ulong k)
    {
      if (k > n - k)
      {
        k = n - k;
      }

      ulong c = 1;
      for (uint i = 0; i < k; i++)
      {
        c *= n - i;
        c /= i + 1;
      }

      return c;
    }

    public static double NChooseK(double n, double k)
    {
      var comb = SpecialFunctions.Gamma(n + 1) / (SpecialFunctions.Gamma(k + 1) * SpecialFunctions.Gamma(n - k + 1));

      if (double.IsNaN(comb) || double.IsInfinity(comb))
      {
        throw new NotFiniteNumberException(comb);
      }

      return comb;
    }

    public static IEnumerable<ulong> Factor(this ulong number)
    {
      var factors = new List<ulong>();
      var max = (ulong) Math.Sqrt(number);

      for (ulong factor = 1; factor <= max; ++factor)
      {
        if (number % factor != 0)
        {
          continue;
        }

        factors.Add(factor);
        if (factor != number / factor)
        {
          factors.Add(number / factor);
        }
      }

      return factors;
    }
  }
}