#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohn
// Project: EveryoneIsJohn
// File Name: ApplicationShell.cs
// 
// Current Data:
// 2021-02-13 12:36 AM
// 
// Creation Date:
// 2021-02-08 5:28 PM

#endregion

using System.Windows;
using EveryoneIsJohn.Debug;
using EveryoneIsJohn.Models.Logging;
using EveryoneIsJohn.UserControls;
using EveryoneIsJohn.ViewModels.Pages;
using EveryoneIsJohn.ViewModels.UserControls;
using EveryoneIsJohn.ViewModels.Windows;
using EveryoneIsJohn.Views.Pages;
using EveryoneIsJohn.Views.Windows;

namespace EveryoneIsJohn
{
  internal static class ApplicationShell
  {
    private static readonly ILogger Logger = new GameLogger();

    public static void Start()
    {
      var mainWindow = NewMainWindow();
      mainWindow.Show();
    }

    private static Window NewMainWindow()
    {
      var window = new ViewBase();

      var loggerControlView = new LoggerControlView
      {
        DataContext = new LoggerControlViewModel(Logger)
      };

      var horizontalSplitPage = new HorizontalSplitPageView
      {
        DataContext =
          new HorizontalSplitPageViewModel(new DebugPageView {DataContext = new DebugPageViewModel {Logger = Logger}},
            loggerControlView)
      };

      var mainPage = new ToolbarPageView
      {
        DataContext = new ToolbarPageViewModel(horizontalSplitPage)
      };

      var windowVm = new ViewModelBase(window)
      {
        WindowTitle = "Everyone is John Tracker",
        CurrentPage = mainPage
      };

      window.DataContext = windowVm;

      return window;
    }
  }
}