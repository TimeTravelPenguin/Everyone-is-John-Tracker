#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohn
// Project: EveryoneIsJohn
// File Name: ToolbarControlViewModel.cs
// 
// Current Data:
// 2021-02-09 1:41 PM
// 
// Creation Date:
// 2021-02-08 7:54 PM

#endregion

using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace EveryoneIsJohn.ViewModels.UserControls
{
  /// <summary>
  ///   Interaction logic for ToolbarControlViewModel.xaml
  /// </summary>
  public class ToolbarControlViewModel : UserControl
  {
    private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
    {
      Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
      e.Handled = true;
    }
  }
}