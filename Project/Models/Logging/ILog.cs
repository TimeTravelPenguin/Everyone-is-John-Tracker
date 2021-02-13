#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohn
// Project: EveryoneIsJohn
// File Name: ILog.cs
// 
// Current Data:
// 2021-02-12 10:30 PM
// 
// Creation Date:
// 2021-02-12 3:51 PM

#endregion

using System;

namespace EveryoneIsJohn.Models.Logging
{
  public interface ILog
  {
    string LogMessage { get; }
    DateTime TimeStamp { get; }
    string LogTimeStamp { get; }
  }
}