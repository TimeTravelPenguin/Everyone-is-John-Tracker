#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: NullLogger.cs
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
  public class NullLogger : ILogger
  {
    public IReadOnlyCollection<ILog> LogHistory => new ILog[] { };

    public void AddLog(string logMessage)
    {
#if DEBUG
      System.Diagnostics.Debug.WriteLine($"NULL LOGGING: {logMessage}");
#endif
    }
  }
}