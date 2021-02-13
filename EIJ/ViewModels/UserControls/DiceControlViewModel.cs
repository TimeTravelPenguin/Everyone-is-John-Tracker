#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: DiceControlViewModel.cs
// 
// Current Data:
// 2021-02-13 9:00 PM
// 
// Creation Date:
// 2021-02-13 8:51 PM

#endregion

using System;
using EIJ.BaseTypes;
using SciChart.Charting.Model.DataSeries;

namespace EIJ.ViewModels.UserControls
{
  public class DiceControlViewModel : PropertyChangedBase
  {
    private XyDataSeries<double, double> _lineData = new XyDataSeries<double, double>();
    private XyDataSeries<double, double> _scatterData = new XyDataSeries<double, double>();

    public XyDataSeries<double, double> LineData
    {
      get => _lineData;
      set => SetValue(ref _lineData, value);
    }

    public XyDataSeries<double, double> ScatterData
    {
      get => _scatterData;
      set => SetValue(ref _scatterData, value);
    }

    public DiceControlViewModel()
    {
      GenerateData();
    }

    private void GenerateData()
    {
      // Create XyDataSeries to host data for our charts
      ScatterData.Clear();
      LineData.Clear();

      for (double i = -1; i < 2 * Math.PI + 1; i += 0.1)
      {
        LineData.Append(i, Math.Sin(i));
        ScatterData.Append(i, Math.Cos(i));
      }

      //OnPropertyChanged(nameof(ScatterData));
      //OnPropertyChanged(nameof(LineData));
    }
  }
}