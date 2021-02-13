#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: App.xaml.cs
// 
// Current Data:
// 2021-02-13 7:54 PM
// 
// Creation Date:
// 2021-02-13 7:42 PM

#endregion

using System.Windows;

namespace EIJ
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