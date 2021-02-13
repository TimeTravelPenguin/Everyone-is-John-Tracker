#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohn
// Project: EveryoneIsJohn
// File Name: DateTimeExtensions.cs
// 
// Current Data:
// 2021-02-12 10:12 PM
// 
// Creation Date:
// 2021-02-12 10:00 PM

#endregion

using System;

namespace EveryoneIsJohn.Extensions
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