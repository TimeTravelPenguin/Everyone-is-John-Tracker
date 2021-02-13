#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: DateTimeExtensions.cs
// 
// Current Data:
// 2021-02-13 7:50 PM
// 
// Creation Date:
// 2021-02-13 7:42 PM

#endregion

using System;

namespace EIJ.Extensions
{
  public static class DateTimeExtensions
  {
    public static bool MinutePrecisionComesAfter(this DateTime after, DateTime before)
    {
      var beforeNumeric = before.Year + before.DayOfYear + before.Hour + before.Minute;
      var afterNumeric = after.Year + after.DayOfYear + after.Hour + after.Minute;

      return after > before;
    }

    public static string ToLogTime(this DateTime dt)
    {
      return $"{dt:ddd hh:mm tt}";
    }
  }
}