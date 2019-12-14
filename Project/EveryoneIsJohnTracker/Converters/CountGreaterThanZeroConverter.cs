#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: CountGreaterThanZeroConverter.cs
// 
// Current Data:
// 2019-12-14 4:33 PM
// 
// Creation Date:
// 2019-12-14 4:28 PM

#endregion

using System;
using System.Globalization;
using System.Windows.Data;

namespace EveryoneIsJohnTracker.Converters
{
internal class CountGreaterThanZeroConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var count = System.Convert.ToInt32(value);

        return count > 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
}