#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohn
// Project: EveryoneIsJohn
// File Name: ILogger.cs
// 
// Current Data:
// 2021-02-12 10:39 PM
// 
// Creation Date:
// 2021-02-12 10:30 PM

#endregion

using System.Collections.Generic;

namespace EveryoneIsJohn.Models.Logging
{
  public interface ILogger
  {
    IReadOnlyCollection<ILog> LogHistory { get; }
    void AddLog(string logMessage);
  }
}