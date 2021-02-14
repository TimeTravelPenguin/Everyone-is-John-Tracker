#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: StringExtensions.cs
// 
// Current Data:
// 2021-02-14 9:12 PM
// 
// Creation Date:
// 2021-02-14 9:11 PM

#endregion

using System;
using System.Text.RegularExpressions;

namespace EIJ.Extensions
{
  public static class StringExtensions
  {
    public static string RemoveWhitespace(this string input)
    {
      if (input is null)
      {
        throw new ArgumentNullException(nameof(input));
      }

      return Regex.Replace(input, @"\s+", "");
    }
  }
}