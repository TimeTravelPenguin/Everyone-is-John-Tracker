#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: DiceRoller.cs
// 
// Current Data:
// 2021-02-19 8:13 PM
// 
// Creation Date:
// 2021-02-13 11:16 PM

#endregion

using System;
using System.Linq;
using EIJ.Extensions;
using EIJ.Helpers;

namespace EIJ.Models.DiceRoller
{
  public static class DiceRoller
  {
    private static readonly Random Random = new Random();

    private static long RollDie(ulong sides, ulong count = 1, long additionMod = 0, ulong min = 1)
    {
      if (count <= 0)
      {
        throw new ArgumentException($"{nameof(count)} must be a positive integer");
      }

      if (sides <= 0)
      {
        throw new ArgumentException($"{nameof(count)} must be a positive integer");
      }

      ulong sum = 0;
      for (ulong i = 0; i < count; i++)
      {
        sum += Random.RandomULong(min, sides + 1);
      }

      return (long) sum + additionMod;
    }

    public static long Roll(DiceRollPattern pattern, ulong minValue = 1)
    {
      if (pattern is null)
      {
        throw new ArgumentNullException(nameof(pattern));
      }

      return RollDie(pattern.SideCount, pattern.DiceCount, pattern.ModValue, minValue);
    }
  }
}