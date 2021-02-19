#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: BoolToVisibilityConverter.cs
// 
// Current Data:
// 2021-02-19 10:42 PM
// 
// Creation Date:
// 2021-02-19 10:41 PM

#endregion

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EIJ.IValueConverters
{
  public class BoolToVisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is bool b)
      {
        return b ? Visibility.Visible : Visibility.Hidden;
      }

      throw new InvalidOperationException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}