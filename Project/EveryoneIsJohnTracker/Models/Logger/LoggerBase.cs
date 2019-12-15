#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: LoggerBase.cs
// 
// Current Data:
// 2019-12-11 7:22 PM
// 
// Creation Date:
// 2019-12-11 6:18 PM

#endregion

using System.Collections.ObjectModel;
using EveryoneIsJohnTracker.Base;

namespace EveryoneIsJohnTracker.Models.Logger
{
    internal class LoggerBase : PropertyChangedBase
    {
        private ILogger _logger;
        private ObservableCollection<Log> _logHistory = new ObservableCollection<Log>();

        public ILogger Logger
        {
            get => _logger;
            set
            {
                if (value != null)
                {
                    value.LogHistory = _logHistory;
                }

                SetValue(ref _logger, value);
            }
        }

        public ObservableCollection<Log> LogHistory
        {
            get => _logHistory;
            set => SetValue(ref _logHistory, value);
        }
    }
}