#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: BoolInverterConverter.cs
// 
// Current Data:
// 2021-02-19 10:36 PM
// 
// Creation Date:
// 2021-02-19 10:34 PM

#endregion

using System;
using System.Globalization;
using System.Windows.Data;

namespace EIJ.IValueConverters
{
  public class BoolInverterConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is bool b)
      {
        return !b;
      }

      throw new InvalidOperationException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}