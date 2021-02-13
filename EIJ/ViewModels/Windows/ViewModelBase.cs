#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: ViewModelBase.cs
// 
// Current Data:
// 2021-02-13 7:50 PM
// 
// Creation Date:
// 2021-02-13 7:42 PM

#endregion

using System.Windows;
using System.Windows.Controls;
using EIJ.BaseTypes;
using Microsoft.Xaml.Behaviors.Core;

namespace EIJ.ViewModels.Windows
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