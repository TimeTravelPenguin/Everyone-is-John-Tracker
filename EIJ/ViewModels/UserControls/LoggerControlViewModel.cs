#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: LoggerControlViewModel.cs
// 
// Current Data:
// 2021-02-13 7:50 PM
// 
// Creation Date:
// 2021-02-13 7:42 PM

#endregion

using EIJ.Models.Logging;

namespace EIJ.ViewModels.UserControls
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