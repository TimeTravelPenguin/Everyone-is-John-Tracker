#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohn
// Project: EveryoneIsJohn
// File Name: ToolbarPageViewModel.cs
// 
// Current Data:
// 2021-02-12 4:07 PM
// 
// Creation Date:
// 2021-02-12 3:49 PM

#endregion

using System.Windows;
using System.Windows.Controls;

namespace EveryoneIsJohn.ViewModels.Pages
{
  public class ToolbarPageViewModel
  {
    public FrameworkElement PageBody { get; }

    public ToolbarPageViewModel()
    {
      PageBody = new Page();
    }

    public ToolbarPageViewModel(FrameworkElement pageBody)
    {
      PageBody = pageBody;
    }
  }
}