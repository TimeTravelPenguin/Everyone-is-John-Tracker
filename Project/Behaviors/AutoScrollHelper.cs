#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohn
// Project: EveryoneIsJohn
// File Name: AutoScrollHelper.cs
// 
// Current Data:
// 2021-02-13 12:06 AM
// 
// Creation Date:
// 2021-02-12 11:52 PM

#endregion

using System.Windows;
using System.Windows.Controls;
using EveryoneIsJohn.Extensions;

namespace EveryoneIsJohn.Behaviors
{
  public class AutoScrollHelper
  {
    public static DependencyProperty AutoScrollProperty { get; } =
      DependencyProperty.RegisterAttached("AutoScroll", typeof(bool), typeof(AutoScrollHelper),
        new PropertyMetadata(false, AutoScrollPropertyChanged));

    public static void AutoScrollPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
    {
      var scrollViewer = obj as ScrollViewer;
      if (scrollViewer != null && (bool) args.NewValue)
      {
        scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
        scrollViewer.ScrollToEnd();
      }
      else if (scrollViewer != null)
      {
        scrollViewer.ScrollChanged -= ScrollViewer_ScrollChanged;
      }
    }

    private static void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
    {
      // Only scroll to bottom when the extent changed. Otherwise you can't scroll up
      if (e.ExtentHeightChange.IsGreaterThanZero())
      {
        var scrollViewer = sender as ScrollViewer;
        scrollViewer?.ScrollToBottom();
      }
    }

    public static bool GetAutoScroll(DependencyObject obj)
    {
      return (bool) obj.GetValue(AutoScrollProperty);
    }

    public static void SetAutoScroll(DependencyObject obj, bool value)
    {
      obj.SetValue(AutoScrollProperty, value);
    }
  }
}