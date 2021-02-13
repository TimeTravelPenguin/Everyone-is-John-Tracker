#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: GameLogger.cs
// 
// Current Data:
// 2021-02-13 7:50 PM
// 
// Creation Date:
// 2021-02-13 7:42 PM

#endregion

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EIJ.Models.Logging
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