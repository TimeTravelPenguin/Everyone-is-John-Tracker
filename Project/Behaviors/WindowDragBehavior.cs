#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohn
// Project: EveryoneIsJohn
// File Name: WindowDragBehavior.cs
// 
// Current Data:
// 2021-02-08 5:42 PM
// 
// Creation Date:
// 2021-02-08 5:28 PM

#endregion

using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace EveryoneIsJohn.Behaviors
{
  internal class WindowDragBehavior : Behavior<UIElement>
  {
    protected override void OnAttached()
    {
      base.OnAttached();

      AssociatedObject.MouseLeftButtonDown += LeftButtonDown;
    }

    protected override void OnDetaching()
    {
      base.OnDetaching();

      AssociatedObject.MouseLeftButtonDown -= LeftButtonDown;
    }

    private static void LeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (e.LeftButton == MouseButtonState.Pressed)
      {
        // Get current window for dragging
        var window = Application.Current.Windows
          .OfType<Window>().SingleOrDefault(x => x.IsActive);

        if (window is null)
        {
          throw new NullReferenceException();
        }

        // TODO: Fix single click on maximized window activating the following if statement. The user needs to drag the window before it detaches.

        // BUG: When the window is maximized, dragging the window does not always place the window on the mouse cursor position. This can cause dragging off-screen.

        // Check if window is maximized and return to normal
        if (window.WindowState == WindowState.Maximized)
        {
          window.WindowState = WindowState.Normal;

          window.Top = 0;
        }

        window.DragMove();
      }
    }
  }
}