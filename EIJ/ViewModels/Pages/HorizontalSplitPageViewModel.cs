#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: HorizontalSplitPageViewModel.cs
// 
// Current Data:
// 2021-02-13 7:50 PM
// 
// Creation Date:
// 2021-02-13 7:42 PM

#endregion

using System.Windows;
using System.Windows.Controls;

namespace EIJ.ViewModels.Pages
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