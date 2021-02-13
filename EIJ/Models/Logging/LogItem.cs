#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: LogItem.cs
// 
// Current Data:
// 2021-02-13 7:50 PM
// 
// Creation Date:
// 2021-02-13 7:42 PM

#endregion

using System;
using EIJ.Extensions;

namespace EIJ.Models.Logging
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