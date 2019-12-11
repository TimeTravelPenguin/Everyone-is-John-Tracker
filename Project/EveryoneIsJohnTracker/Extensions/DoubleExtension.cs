#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: DoubleExtension.cs
// 
// Current Data:
// 2019-12-11 7:02 PM
// 
// Creation Date:
// 2019-09-28 4:35 PM

#endregion

using System;

namespace EveryoneIsJohnTracker.Extensions
{
    internal static class DoubleExtension
    {
        private const double Epsilon = 1E-07;

        public static bool IsZero(this double value)
        {
            return value.IsEqualTo(0.0);
        }

        public static bool IsEqualTo(this double lhs, double rhs)
        {
            return Math.Abs(lhs - rhs) < Epsilon;
        }

        public static bool IsGreaterThanZero(this double lhs)
        {
            return Math.Abs(lhs) > Epsilon;
        }
    }
}