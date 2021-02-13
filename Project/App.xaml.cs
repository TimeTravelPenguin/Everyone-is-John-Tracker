#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohn
// Project: EveryoneIsJohn
// File Name: App.xaml.cs
// 
// Current Data:
// 2021-02-08 5:42 PM
// 
// Creation Date:
// 2021-02-08 5:28 PM

#endregion

using System.Windows;

namespace EveryoneIsJohn
{
  /// <summary>
  ///   Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private void App_OnStartup(object sender, StartupEventArgs e)
    {
      ApplicationShell.Start();
    }
  }
}