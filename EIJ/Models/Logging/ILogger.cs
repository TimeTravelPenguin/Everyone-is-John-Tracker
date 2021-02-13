#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: ILogger.cs
// 
// Current Data:
// 2021-02-13 7:50 PM
// 
// Creation Date:
// 2021-02-13 7:42 PM

#endregion

using System.Collections.Generic;

namespace EIJ.Models.Logging
{
  public interface ILogger
  {
    IReadOnlyCollection<ILog> LogHistory { get; }
    void AddLog(string logMessage);
  }
}