#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: Log.cs
// 
// Current Data:
// 2019-12-11 7:02 PM
// 
// Creation Date:
// 2019-12-11 6:15 PM

#endregion

using System;

namespace EveryoneIsJohnTracker.Models.Logger
{
    internal class Log
    {
        public DateTime TimeStamp { get; }
        public string Message { get; }

        public Log(string message)
        {
            TimeStamp = DateTime.Now;
            Message = message;
        }
    }
}