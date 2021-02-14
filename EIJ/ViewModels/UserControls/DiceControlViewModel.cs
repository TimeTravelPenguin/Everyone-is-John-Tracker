#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: DiceControlViewModel.cs
// 
// Current Data:
// 2021-02-14 10:22 PM
// 
// Creation Date:
// 2021-02-13 8:51 PM

#endregion

using EIJ.BaseTypes;
using EIJ.Models.DiceRoller;
using Microsoft.Expression.Interactivity.Core;

namespace EIJ.ViewModels.UserControls
{
  public class DiceControlViewModel : PropertyChangedBase
  {
    private static readonly DiceRoller DiceRoller = new DiceRoller();
    private string _diceRoleRule = "1d6";
    private int _diceRollOutcome;

    public string DiceRoleRule
    {
      get => _diceRoleRule;
      set => SetValue(ref _diceRoleRule, value);
    }

    public int DiceRollOutcome
    {
      get => _diceRollOutcome;
      set => SetValue(ref _diceRollOutcome, value);
    }

    public ActionCommand CommandRollDice { get; }

    public DiceControlViewModel()
    {
      CommandRollDice = new ActionCommand(RollDice);
    }

    private void RollDice()
    {
      DiceRollOutcome = DiceRoller.Roll(new DiceRollPattern(DiceRoleRule));
    }
  }
}