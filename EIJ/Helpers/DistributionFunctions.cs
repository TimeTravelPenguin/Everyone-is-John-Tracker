#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: DistributionFunctions.cs
// 
// Current Data:
// 2021-02-16 12:46 AM
// 
// Creation Date:
// 2021-02-15 11:09 PM

#endregion

using System;
using MathNet.Numerics;

namespace EIJ.Helpers
{
  public static class DistributionFunctions
  {
    public static double Binomial(double maxTries, double successes, double successProbability)
    {
      if (maxTries < successes)
      {
        throw new ArgumentException($"'{nameof(maxTries)}' cannot be less than '{nameof(successes)}'");
      }

      if (successProbability < 0 || successProbability > 1)
      {
        throw new ArgumentException("Probability must be within the inclusive range [0, 1]",
          nameof(successProbability));
      }

      return nChoosek(maxTries, successes) * Math.Pow(successProbability, successes) *
             Math.Pow(1 - successProbability, maxTries - successes);
    }

    public static double nChoosek(double n, double k)
    {
      return SpecialFunctions.Gamma(n + 1) / (SpecialFunctions.Gamma(k + 1) * SpecialFunctions.Gamma(n - k + 1));
    }
  }
}