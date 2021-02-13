#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: ToolbarPageViewModel.cs
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