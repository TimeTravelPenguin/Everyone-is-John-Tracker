#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: DiceRoller.cs
// 
// Current Data:
// 2021-02-13 11:25 PM
// 
// Creation Date:
// 2021-02-13 11:16 PM

#endregion

using System;
using System.Linq;
using EIJ.BaseTypes;

namespace EIJ.Models.DiceRoller
{
  public class DiceRoller : PropertyChangedBase
  {
    private static readonly Random Random = new Random();

    private static int RollDie(int sides, int count = 1, int additionMod = 0, int min = 1)
    {
      if (count <= 0)
      {
        throw new ArgumentException($"{nameof(count)} must be a positive integer");
      }

      if (sides <= 0)
      {
        throw new ArgumentException($"{nameof(count)} must be a positive integer");
      }

      return Enumerable.Range(0, count)
               .Sum(x => Random.Next(min, sides + 1))
             + additionMod;
    }

    public static int Roll(DiceRollPattern pattern, int minValue = 1)
    {
      if (pattern is null)
      {
        throw new ArgumentNullException(nameof(pattern));
      }

      return RollDie(pattern.SideCount, pattern.DiceCount, pattern.ModValue, minValue);
    }
  }
}