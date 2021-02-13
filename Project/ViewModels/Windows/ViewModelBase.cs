#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohn
// Project: EveryoneIsJohn
// File Name: ViewModelBase.cs
// 
// Current Data:
// 2021-02-08 5:42 PM
// 
// Creation Date:
// 2021-02-08 5:38 PM

#endregion

using System.Windows;
using System.Windows.Controls;
using EveryoneIsJohn.BaseTypes;
using Microsoft.Expression.Interactivity.Core;

namespace EveryoneIsJohn.ViewModels.Windows
{
  internal class ViewModelBase : PropertyChangedBase
  {
    private Page _currentPage;
    private ResizeMode _resizeMode;
    private string _windowTitle;

    public ResizeMode ResizeMode
    {
      get => _resizeMode;
      set => SetValue(ref _resizeMode, value);
    }

    public string WindowTitle
    {
      get => _windowTitle;
      set => SetValue(ref _windowTitle, value);
    }

    public Page CurrentPage
    {
      get => _currentPage;
      set => SetValue(ref _currentPage, value);
    }

    public ActionCommand CloseWindow { get; }
    public ActionCommand MinimizeWindow { get; }
    public ActionCommand MaximizeWindow { get; }

    public ViewModelBase(Window window, ResizeMode resizeMode = ResizeMode.CanResizeWithGrip)
    {
      CloseWindow = new ActionCommand(window.Close);
      MinimizeWindow = new ActionCommand(() => window.WindowState = WindowState.Minimized);
      MaximizeWindow = new ActionCommand(() => window.WindowState = WindowState.Maximized);
      ResizeMode = resizeMode;
    }
  }
}