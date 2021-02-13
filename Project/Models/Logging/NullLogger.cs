#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohn
// Project: EveryoneIsJohn
// File Name: NullLogger.cs
// 
// Current Data:
// 2021-02-13 12:29 AM
// 
// Creation Date:
// 2021-02-13 12:25 AM

#endregion

using System.Collections.Generic;

namespace EveryoneIsJohn.Models.Logging
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