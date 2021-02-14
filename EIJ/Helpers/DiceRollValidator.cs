#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: DiceRollValidator.cs
// 
// Current Data:
// 2021-02-14 10:41 PM
// 
// Creation Date:
// 2021-02-14 6:13 PM

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EIJ.Extensions;

namespace EIJ.Helpers
{
  public static class DiceRollValidator
  {
    private static readonly Lazy<Regex> DiceFormatRegex = new Lazy<Regex>(() =>
      new Regex(@"^(?<DiceCount>\d+)d(?<SideCount>\d+)(?<ModGroup>(?<ModSign>[+-])(?<ModValue>\d+))?$",
        RegexOptions.IgnoreCase));

    public static bool ValidateDiceRule(this string diceFormat)
    {
      if (diceFormat is null)
      {
        throw new ArgumentNullException(nameof(diceFormat));
      }

      return DiceFormatRegex.Value.IsMatch(diceFormat.RemoveWhitespace());
    }

    public static IDictionary<string, string> ProcessDice(this string diceFormat)
    {
      if (diceFormat is null)
      {
        throw new ArgumentNullException(nameof(diceFormat));
      }

      var match = DiceFormatRegex.Value.Match(diceFormat.RemoveWhitespace());

      return match.Groups
        .Cast<Group>()
        .Where(x => x.Value.Any())
        .ToDictionary(m => m.Name, m => m.Value);
    }
  }
}