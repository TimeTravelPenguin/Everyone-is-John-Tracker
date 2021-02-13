#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohn
// Project: EveryoneIsJohn
// File Name: LoggerControlViewModel.cs
// 
// Current Data:
// 2021-02-13 12:32 AM
// 
// Creation Date:
// 2021-02-12 3:49 PM

#endregion

using EveryoneIsJohn.Models.Logging;

namespace EveryoneIsJohn.ViewModels.UserControls
{
  public class LoggerControlViewModel
  {
    public ILogger Logger { get; }

    public LoggerControlViewModel(ILogger logger)
    {
      Logger = logger;
    }

    public LoggerControlViewModel() : this(new NullLogger())
    {
    }
  }
}