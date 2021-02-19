#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: DiceControlViewModel.cs
// 
// Current Data:
// 2021-02-19 8:18 PM
// 
// Creation Date:
// 2021-02-13 8:51 PM

#endregion

using EIJ.BaseTypes;
using EIJ.Models.DiceRoller;
using EIJ.ViewModels.ChartViewModels;
using Microsoft.Expression.Interactivity.Core;

namespace EIJ.ViewModels.UserControls
{
  public class DiceControlViewModel : PropertyChangedBase
  {
    private DiceRollPattern _currentDiceRollPattern;
    private string _diceRoleRule = "1d6";
    private long _diceRollOutcome;
    public AreaChartViewModel AreaChartViewModel { get; }

    public string DiceRoleRule
    {
      get => _diceRoleRule;
      set
      {
        SetValue(ref _diceRoleRule, value);

        CurrentDiceRollPattern.UpdateValues(value);
        AreaChartViewModel.RefreshChart();
      }
    }

    public long DiceRollOutcome
    {
      get => _diceRollOutcome;
      set => SetValue(ref _diceRollOutcome, value);
    }

    public ActionCommand CommandRollDice { get; }

    public DiceRollPattern CurrentDiceRollPattern
    {
      get => _currentDiceRollPattern;
      set => SetValue(ref _currentDiceRollPattern, value);
    }

    public DiceControlViewModel()
    {
      CommandRollDice = new ActionCommand(RollDice);

      CurrentDiceRollPattern = new DiceRollPattern(DiceRoleRule);
      AreaChartViewModel = new AreaChartViewModel(CurrentDiceRollPattern);
    }

    private void RollDice()
    {
      CurrentDiceRollPattern.UpdateValues(DiceRoleRule);
      DiceRollOutcome = DiceRoller.Roll(CurrentDiceRollPattern);
    }
  }
}