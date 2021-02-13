#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohn
// Project: EveryoneIsJohn
// File Name: LogItem.cs
// 
// Current Data:
// 2021-02-12 11:16 PM
// 
// Creation Date:
// 2021-02-12 3:51 PM

#endregion

using System;
using EveryoneIsJohn.Extensions;

namespace EveryoneIsJohn.Models.Logging
{
  public class LogItem : ILog
  {
    public DateTime TimeStamp { get; }
    public string LogMessage { get; }

    public string LogTimeStamp => TimeStamp.ToLogTime();

    public LogItem(string logMessage, DateTime? datetime = null)
    {
      LogMessage = logMessage;
      TimeStamp = datetime ?? DateTime.Now;
    }
  }
}