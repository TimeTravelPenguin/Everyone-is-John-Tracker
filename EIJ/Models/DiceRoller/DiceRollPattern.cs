#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: DiceRollPattern.cs
// 
// Current Data:
// 2021-02-14 10:40 PM
// 
// Creation Date:
// 2021-02-14 8:34 PM

#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using EIJ.Helpers;

namespace EIJ.Models.DiceRoller
{
  public class DiceRollPattern
  {
    public int DiceCount { get; set; }
    public int SideCount { get; set; }
    public int ModValue { get; set; }

    public DiceRollPattern(string pattern)
    {
      var processed = pattern.ProcessDice();

      DiceCount = AssignIntFromDictionary(processed, nameof(DiceCount));
      SideCount = AssignIntFromDictionary(processed, nameof(SideCount));

      if (!processed.ContainsKey("ModGroup"))
      {
        return;
      }

      ModValue = AssignIntFromDictionary(processed, nameof(ModValue), 0);

      if (processed.TryGetValue("ModSign", out var sign) && sign == "-")
      {
        ModValue *= -1;
      }
      else if (sign != "+")
      {
        throw new InvalidOperationException();
      }
    }

    private static int AssignIntFromDictionary(IDictionary<string, string> dictionary, string key,
      int? defaultValue = null)
    {
      if (dictionary.TryGetValue(key, out var k))
      {
        return ParseInt(k);
      }

      if (defaultValue is not null)
      {
        return (int) defaultValue;
      }

      throw new InvalidOperationException();
    }

    private static int ParseInt(string input)
    {
      if (string.IsNullOrWhiteSpace(input))
      {
        throw new ArgumentException(nameof(input));
      }

      if (int.TryParse(input, NumberStyles.None, CultureInfo.InvariantCulture, out var result))
      {
        return result;
      }

      throw new ArgumentException(nameof(input));
    }
  }
}