#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: AreaChartViewModel.cs
// 
// Current Data:
// 2021-02-19 11:28 PM
// 
// Creation Date:
// 2021-02-15 10:14 PM

#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EIJ.BaseTypes;
using EIJ.Helpers;
using EIJ.Models.DiceRoller;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MathNet.Numerics.Statistics;

namespace EIJ.ViewModels.ChartViewModels
{
  public class AreaChartViewModel : PropertyChangedBase
  {
    private DiceRollPattern _currentDiceRollPattern = new DiceRollPattern("1d6");
    private bool _dataTooLarge;
    private Axis _xAxis;

    public Axis XAxis
    {
      get => _xAxis;
      set => SetValue(ref _xAxis, value);
    }

    public SeriesCollection ChartDataSeries { get; } = new SeriesCollection();

    public AxesCollection XAxesCollection { get; } = new AxesCollection();

    public Func<double, string> XLabelFormatter { get; } = d => Math.Round(d, 3).ToString(CultureInfo.InvariantCulture);
    public Func<double, string> YLabelFormatter { get; } = d => Math.Round(d, 3).ToString(CultureInfo.InvariantCulture);

    public DiceRollPattern CurrentDiceRollPattern
    {
      get => _currentDiceRollPattern;
      set => SetValue(ref _currentDiceRollPattern, value);
    }

    public bool DataTooLarge
    {
      get => _dataTooLarge;
      set => SetValue(ref _dataTooLarge, value);
    }

    public AreaChartViewModel()
    {
      RefreshChart();
    }

    public AreaChartViewModel(DiceRollPattern diceRollPattern) : this()
    {
      CurrentDiceRollPattern = diceRollPattern;
    }

    public void RefreshChart()
    {
      ChartDataSeries.Clear();
      XAxesCollection.Clear();

      if (CurrentDiceRollPattern.SideCount * CurrentDiceRollPattern.DiceCount > 100)
      {
        DataTooLarge = true;
        return;
      }

      // Update data
      var data = GenerateChartData().ToList();

      if (data.Select(p => p.Y).Mean() > 1)
      {
        // Indicates there is an error and the sum of the probabilities is > 1
        DataTooLarge = true;
        return;
      }

      var series = new ColumnSeries
      {
        Title = "Probability dice sum x",
        Values = new ChartValues<ObservablePoint>(data),
        DataLabels = false,
        StrokeThickness = 2,
        MaxColumnWidth = double.PositiveInfinity
      };

      ChartDataSeries.Add(series);

      // Update XAxis Spacing

      XAxis = new Axis
      {
        Title = "Number of rolls",
        LabelFormatter = XLabelFormatter,
        Separator = new Separator {IsEnabled = true}
      };

      if (data.Count < 50)
      {
        XAxis.Separator.Step = 1;
      }

      XAxesCollection.Add(XAxis);

      DataTooLarge = false;
    }

    private IEnumerable<ObservablePoint> GenerateChartData(double step = 0.01)
    {
      var points = new List<ObservablePoint>();

      for (var i = CurrentDiceRollPattern.DiceCount;
        i <= CurrentDiceRollPattern.DiceCount * CurrentDiceRollPattern.SideCount;
        ++i)
      {
        var y = DistributionFunctions.DiceSumProbability(i, CurrentDiceRollPattern.DiceCount,
          CurrentDiceRollPattern.SideCount);
        points.Add(new ObservablePoint((long) i + CurrentDiceRollPattern.ModValue, y));
      }

      return points;
    }
  }
}