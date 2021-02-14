#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: DiceControlViewModel.cs
// 
// Current Data:
// 2021-02-14 10:41 AM
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
    private int _diceRollOutcome;
    public XyDataSeries<double, double> LineData { get; } = new XyDataSeries<double, double>();
    public XyDataSeries<double, double> ScatterData { get; } = new XyDataSeries<double, double>();

    public int DiceRollOutcome
    {
      get => _diceRollOutcome;
      set => SetValue(ref _diceRollOutcome, value);
    }

    public DiceControlViewModel()
    {
      GenerateData();
    }

    private void GenerateData()
    {
      ScatterData.Clear();
      LineData.Clear();

      for (double i = -1; i < 2 * Math.PI + 1; i += 0.1)
      {
        LineData.Append(i, Math.Sin(i));
        ScatterData.Append(i, Math.Cos(i));
      }
    }
  }
}