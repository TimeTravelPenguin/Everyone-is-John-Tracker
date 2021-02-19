#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: DistributionFunctions.cs
// 
// Current Data:
// 2021-02-19 7:49 PM
// 
// Creation Date:
// 2021-02-15 11:09 PM

#endregion

using System;

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

      return MathHelper.NChooseK(maxTries, successes) * Math.Pow(successProbability, successes) *
             Math.Pow(1 - successProbability, maxTries - successes);
    }

    public static double SingularUniform(double maxOutComes)
    {
      return Math.Pow(maxOutComes, -1);
    }

    /// <summary>
    ///   Dice mathematics source: <a href="https://mathworld.wolfram.com/Dice.html">Dice</a>.
    /// </summary>
    public static double DiceSumProbability(ulong x, ulong diceCount, ulong diceSides)
    {
      ulong sum = 0;
      for (ulong k = 0; k <= Math.Floor((double) (x - diceCount) / diceSides); ++k)
      {
        sum += (ulong) Math.Pow(-1, k) * MathHelper.NChooseK(diceCount, k) *
               MathHelper.NChooseK(x - diceSides * k - 1, diceCount - 1);
      }

      return Math.Pow(diceSides, -(long) diceCount) * sum;
    }
  }
}