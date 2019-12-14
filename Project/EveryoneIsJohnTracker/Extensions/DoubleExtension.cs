#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: DoubleExtension.cs
// 
// Current Data:
// 2019-12-14 10:48 AM
// 
// Creation Date:
// 2019-09-28 4:35 PM

#endregion

using System;

namespace EveryoneIsJohnTracker.Extensions
{
    internal static class DoubleExtension
    {
        public static bool IsZero(this double value)
        {
            return value.IsEqualTo(0.0);
        }

        public static bool IsEqualTo(this double lhs, double rhs)
        {
            return Math.Abs(lhs - rhs) < double.Epsilon;
        }

        public static bool IsGreaterThanZero(this double lhs)
        {
            return Math.Abs(lhs) > 0;
        }

        public static bool NotEqual(this double lhs, double rhs)
        {
            return Math.Abs(lhs - rhs) > double.Epsilon;
        }
    }
}