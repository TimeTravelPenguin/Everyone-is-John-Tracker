#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohn
// Project: EveryoneIsJohn
// File Name: HorizontalSplitPageViewModel.cs
// 
// Current Data:
// 2021-02-12 7:49 PM
// 
// Creation Date:
// 2021-02-12 7:48 PM

#endregion

using System.Windows;
using System.Windows.Controls;

namespace EveryoneIsJohn.ViewModels.Pages
{
  public class HorizontalSplitPageViewModel
  {
    public FrameworkElement PageUpper { get; }
    public FrameworkElement PageLower { get; }

    public HorizontalSplitPageViewModel(FrameworkElement pageUpper, FrameworkElement pageLower)
    {
      PageUpper = pageUpper;
      PageLower = pageLower;
    }

    public HorizontalSplitPageViewModel()
    {
      PageUpper = new Page();
      PageLower = new Page();
    }
  }
}