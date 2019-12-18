#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: CountGreaterThanZeroConverter.cs
// 
// Current Data:
// 2019-12-18 11:52 AM
// 
// Creation Date:
// 2019-12-18 11:49 AM

#endregion

using System;
using System.Globalization;
using System.Windows.Data;

namespace EveryoneIsJohnTracker.Types.Converters
{
    public class CountGreaterThanZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var count = System.Convert.ToInt32(value, CultureInfo.InvariantCulture);

            return count > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}