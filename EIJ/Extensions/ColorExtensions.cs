#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: ColorExtensions.cs
// 
// Current Data:
// 2021-02-15 10:41 PM
// 
// Creation Date:
// 2021-02-15 10:35 PM

#endregion

using System;
using System.Windows.Media;

namespace EIJ.Extensions
{
  public static class ColorExtensions
  {
    public static Color ColorFromHex(this string hex)
    {
      if (hex is null)
      {
        throw new ArgumentNullException(nameof(hex));
      }

      return (Color) ColorConverter.ConvertFromString(hex);
    }
  }
}