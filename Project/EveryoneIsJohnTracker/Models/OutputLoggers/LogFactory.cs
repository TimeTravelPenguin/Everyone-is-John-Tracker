#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: LogFactory.cs
// 
// Current Data:
// 2019-12-11 7:02 PM
// 
// Creation Date:
// 2019-12-11 6:17 PM

#endregion

using System;
using System.Collections.Generic;

namespace EveryoneIsJohnTracker.Models.OutputLoggers
{
    internal static class LogFactory
    {
        private static readonly Dictionary<LoggerType, Func<ILogger>> LoggerDictionary =
            new Dictionary<LoggerType, Func<ILogger>>
            {
                [LoggerType.OutputLogger] = () => new OutputLogger(),
                [LoggerType.NullLogger] = () => new NullLogger()
            };

        public static ILogger NewLogger(LoggerType loggerType)
        {
            return LoggerDictionary[loggerType].Invoke();
        }
    }
}