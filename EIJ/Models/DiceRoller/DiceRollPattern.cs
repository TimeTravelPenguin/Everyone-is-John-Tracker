#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: DiceRollPattern.cs
// 
// Current Data:
// 2021-02-19 7:53 PM
// 
// Creation Date:
// 2021-02-14 8:34 PM

#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using EIJ.BaseTypes;
using EIJ.Helpers;

namespace EIJ.Models.DiceRoller
{
  public class DiceRollPattern : PropertyChangedBase
  {
    private ulong _diceCount;
    private long _modValue;
    private ulong _sideCount;

    public ulong DiceCount
    {
      get => _diceCount;
      set => SetValue(ref _diceCount, value);
    }

    public ulong SideCount
    {
      get => _sideCount;
      set => SetValue(ref _sideCount, value);
    }

    public long ModValue
    {
      get => _modValue;
      set => SetValue(ref _modValue, value);
    }

    public DiceRollPattern()
    {
      DiceCount = 1;
      SideCount = 1;
      ModValue = 0;
    }

    public DiceRollPattern(string pattern)
    {
      UpdateValues(pattern);
    }

    public void UpdateValues(string pattern)
    {
      var processed = pattern.ProcessDice();

      DiceCount = AssignULongFromDictionary(processed, nameof(DiceCount));
      SideCount = AssignULongFromDictionary(processed, nameof(SideCount));

      if (!processed.ContainsKey("ModGroup"))
      {
        ModValue = 0;
        return;
      }

      ModValue = (long) AssignULongFromDictionary(processed, nameof(ModValue), 0);

      if (processed.TryGetValue("ModSign", out var sign) && sign == "-")
      {
        ModValue *= -1;
      }
      else if (sign != "+")
      {
        throw new InvalidOperationException();
      }
    }

    private static ulong AssignULongFromDictionary(IDictionary<string, string> dictionary, string key,
      int? defaultValue = null)
    {
      if (dictionary.TryGetValue(key, out var k))
      {
        return ParseULong(k);
      }

      if (defaultValue is not null)
      {
        return (ulong) defaultValue;
      }

      throw new InvalidOperationException();
    }

    private static ulong ParseULong(string input)
    {
      if (string.IsNullOrWhiteSpace(input))
      {
        throw new ArgumentException(nameof(input));
      }

      if (ulong.TryParse(input, NumberStyles.None, CultureInfo.InvariantCulture, out var result))
      {
        return result;
      }

      throw new ArgumentException(nameof(input));
    }
  }
}