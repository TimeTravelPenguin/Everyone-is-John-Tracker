#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: DiceRoleRuleValidationRule.cs
// 
// Current Data:
// 2021-02-14 10:15 PM
// 
// Creation Date:
// 2021-02-14 10:00 PM

#endregion

using System;
using System.Globalization;
using System.Windows.Controls;
using EIJ.Helpers;

namespace EIJ.Validation
{
  public class DiceRoleRuleValidationRule : ValidationRule
  {
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
      var rule = Convert.ToString(value);

      return rule.ValidateDiceRule()
        ? new ValidationResult(true, null)
        : new ValidationResult(false, "Invalid dice rule format");
    }
  }
}