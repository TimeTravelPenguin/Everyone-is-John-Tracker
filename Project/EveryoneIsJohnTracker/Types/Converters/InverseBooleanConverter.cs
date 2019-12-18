#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: InverseBooleanConverter.cs
// 
// Current Data:
// 2019-12-18 11:53 AM
// 
// Creation Date:
// 2019-12-18 11:53 AM

#endregion

using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Data;

namespace EveryoneIsJohnTracker.Types.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        private readonly ResourceManager _resourceManager =
            new ResourceManager("DnDTracker.Resources.Exceptions.ExceptionMessages", Assembly.GetExecutingAssembly());

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool))
            {
                throw new InvalidOperationException(_resourceManager.GetString("TargetMustBeBool",
                    CultureInfo.InvariantCulture));
            }

            if (value == null)
            {
                throw new InvalidOperationException(_resourceManager.GetString("CannotBeNull",
                    CultureInfo.InvariantCulture));
            }

            return !(bool) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}