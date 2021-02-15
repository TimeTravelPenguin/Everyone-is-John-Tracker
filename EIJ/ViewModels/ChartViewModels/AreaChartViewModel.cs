#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: AreaChartViewModel.cs
// 
// Current Data:
// 2021-02-16 12:51 AM
// 
// Creation Date:
// 2021-02-15 10:14 PM

#endregion

using System;
using System.Collections.Generic;
using EIJ.BaseTypes;
using EIJ.Helpers;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace EIJ.ViewModels.ChartViewModels
{
  public class AreaChartViewModel : PropertyChangedBase
  {
    private Func<double, double> _chartFunction;

    public Func<double, double> ChartFunction
    {
      get => _chartFunction;
      set => SetValue(ref _chartFunction, value);
    }


    public SeriesCollection ChartDataSeries { get; } = new SeriesCollection();


    public AreaChartViewModel() : this(d => DistributionFunctions.Binomial(6, d, 0.167))
    {
    }

    public AreaChartViewModel(Func<double, double> chartFunction)
    {
      ChartFunction = chartFunction;
      CreateChart();
    }

    private void CreateChart()
    {
      ChartDataSeries.Clear();

      var series = new LineSeries
      {
        Values = new ChartValues<ObservablePoint>(GenerateChartData()),
        DataLabels = false,
        StrokeThickness = 2,
        PointGeometrySize = 0,
        LineSmoothness = 0.5
      };

      ChartDataSeries.Add(series);
    }

    private IEnumerable<ObservablePoint> GenerateChartData(double step = 0.01)
    {
      var points = new List<ObservablePoint>();
      double x = 0;
      double y = 1;

      while (y > 0.001 && x <= 6)
      {
        y = ChartFunction(x);
        points.Add(new ObservablePoint(x, y));
        x += step;
      }

      return points;
    }
  }
}