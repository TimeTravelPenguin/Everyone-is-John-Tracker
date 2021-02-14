﻿#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: WatermarkService.cs
// 
// Current Data:
// 2021-02-14 10:14 AM
// 
// Creation Date:
// 2021-02-14 10:10 AM

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace EIJ.UserControls.HintTextBox
{
  /// <summary>
  ///   Class that provides the Watermark attached property
  /// </summary>
  public static class WatermarkService
  {
    /// <summary>
    ///   Watermark Attached Dependency Property
    /// </summary>
    public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached(
      "Watermark",
      typeof(object),
      typeof(WatermarkService),
      new FrameworkPropertyMetadata(null, OnWatermarkChanged));

    #region Private Fields

    /// <summary>
    ///   Dictionary of ItemsControls
    /// </summary>
    private static readonly Dictionary<object, ItemsControl> ItemsControls = new Dictionary<object, ItemsControl>();

    #endregion

    /// <summary>
    ///   Gets the Watermark property.  This dependency property indicates the watermark for the control.
    /// </summary>
    /// <param name="d"><see cref="DependencyObject" /> to get the property from</param>
    /// <returns>The value of the Watermark property</returns>
    public static object GetWatermark(DependencyObject d)
    {
      return d.GetValue(WatermarkProperty);
    }

    /// <summary>
    ///   Sets the Watermark property.  This dependency property indicates the watermark for the control.
    /// </summary>
    /// <param name="d"><see cref="DependencyObject" /> to set the property on</param>
    /// <param name="value">value of the property</param>
    public static void SetWatermark(DependencyObject d, object value)
    {
      d.SetValue(WatermarkProperty, value);
    }

    /// <summary>
    ///   Handles changes to the Watermark property.
    /// </summary>
    /// <param name="d"><see cref="DependencyObject" /> that fired the event</param>
    /// <param name="e">A <see cref="DependencyPropertyChangedEventArgs" /> that contains the event data.</param>
    private static void OnWatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var control = (Control) d;
      control.Loaded += Control_Loaded;

      switch (d)
      {
        case ComboBox _:
          control.GotKeyboardFocus += Control_GotKeyboardFocus;
          control.LostKeyboardFocus += Control_Loaded;
          break;
        case TextBox _:
          control.GotKeyboardFocus += Control_GotKeyboardFocus;
          control.LostKeyboardFocus += Control_Loaded;
          ((TextBox) control).TextChanged += Control_GotKeyboardFocus;
          break;
      }

      if (d is ItemsControl i && !(i is ComboBox))
      {
        // for Items property  
        i.ItemContainerGenerator.ItemsChanged += ItemsChanged;
        ItemsControls.Add(i.ItemContainerGenerator, i);

        // for ItemsSource property  
        var prop = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, i.GetType());
        prop.AddValueChanged(i, ItemsSourceChanged);
      }
    }

    #region Event Handlers

    /// <summary>
    ///   Handle the GotFocus event on the control
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="RoutedEventArgs" /> that contains the event data.</param>
    private static void Control_GotKeyboardFocus(object sender, RoutedEventArgs e)
    {
      var c = (Control) sender;
      if (ShouldShowWatermark(c))
      {
        ShowWatermark(c);
      }
      else
      {
        RemoveWatermark(c);
      }
    }

    /// <summary>
    ///   Handle the Loaded and LostFocus event on the control
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="RoutedEventArgs" /> that contains the event data.</param>
    private static void Control_Loaded(object sender, RoutedEventArgs e)
    {
      var control = (Control) sender;
      if (ShouldShowWatermark(control))
      {
        ShowWatermark(control);
      }
    }

    /// <summary>
    ///   Event handler for the items source changed event
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="EventArgs" /> that contains the event data.</param>
    private static void ItemsSourceChanged(object sender, EventArgs e)
    {
      var c = (ItemsControl) sender;
      if (c.ItemsSource != null)
      {
        if (ShouldShowWatermark(c))
        {
          ShowWatermark(c);
        }
        else
        {
          RemoveWatermark(c);
        }
      }
      else
      {
        ShowWatermark(c);
      }
    }

    /// <summary>
    ///   Event handler for the items changed event
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="ItemsChangedEventArgs" /> that contains the event data.</param>
    private static void ItemsChanged(object sender, ItemsChangedEventArgs e)
    {
      if (ItemsControls.TryGetValue(sender, out var control))
      {
        if (ShouldShowWatermark(control))
        {
          ShowWatermark(control);
        }
        else
        {
          RemoveWatermark(control);
        }
      }
    }

    #endregion

    #region Helper Methods

    /// <summary>
    ///   Remove the watermark from the specified element
    /// </summary>
    /// <param name="control">Element to remove the watermark from</param>
    private static void RemoveWatermark(UIElement control)
    {
      var layer = AdornerLayer.GetAdornerLayer(control);

      // layer could be null if control is no longer in the visual tree

      var adorners = layer?.GetAdorners(control);
      if (adorners is null)
      {
        return;
      }

      foreach (var adorner in adorners)
      {
        if (adorner is WatermarkAdorner)
        {
          adorner.Visibility = Visibility.Hidden;
          layer.Remove(adorner);
        }
      }
    }

    /// <summary>
    ///   Show the watermark on the specified control
    /// </summary>
    /// <param name="control">UIElement to show the watermark on</param>
    private static void ShowWatermark(UIElement control)
    {
      var layer = AdornerLayer.GetAdornerLayer(control);

      // layer could be null if control is no longer in the visual tree
      layer?.Add(new WatermarkAdorner(control, GetWatermark(control)));
    }

    /// <summary>
    ///   Indicates whether or not the watermark should be shown on the specified control
    /// </summary>
    /// <param name="c"><see cref="Control" /> to test</param>
    /// <returns>true if the watermark should be shown; false otherwise</returns>
    private static bool ShouldShowWatermark(Control c)
    {
      return c switch
      {
        ComboBox box => box.Text == string.Empty,
        TextBoxBase _ => (c as TextBox)?.Text == string.Empty,
        ItemsControl control => control.Items.Count == 0,
        _ => false
      };
    }

    #endregion
  }
}