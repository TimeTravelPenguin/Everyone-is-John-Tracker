#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohn
// Project: EveryoneIsJohn
// File Name: GameLogger.cs
// 
// Current Data:
// 2021-02-13 12:24 AM
// 
// Creation Date:
// 2021-02-12 9:49 PM

#endregion

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EveryoneIsJohn.Models.Logging
{
  public class GameLogger : ILogger
  {
    private readonly ObservableCollection<ILog> _logs = new ObservableCollection<ILog>();

    public IReadOnlyCollection<ILog> LogHistory => _logs;

    public void AddLog(string logMessage)
    {
      _logs.Add(new LogItem(logMessage));
    }
  }
}