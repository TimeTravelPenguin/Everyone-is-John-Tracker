#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: RegexValidationRule.cs
// 
// Current Data:
// 2019-12-18 11:48 AM
// 
// Creation Date:
// 2019-12-18 11:48 AM

#endregion

using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace EveryoneIsJohnTracker.Types
{
    internal class RegexValidationRule : ValidationRule
    {
        public string Expression { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return new Regex(Expression).IsMatch(value?.ToString() ?? throw new InvalidOperationException())
                ? ValidationResult.ValidResult
                : new ValidationResult(false, "Invalid input format");
        }
    }
}