#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: ToolbarControlViewModel.cs
// 
// Current Data:
// 2021-02-13 7:50 PM
// 
// Creation Date:
// 2021-02-13 7:42 PM

#endregion

using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace EIJ.ViewModels.UserControls
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