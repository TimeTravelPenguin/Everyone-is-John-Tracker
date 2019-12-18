#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: DiceModel.cs
// 
// Current Data:
// 2019-12-18 11:20 AM
// 
// Creation Date:
// 2019-12-18 11:20 AM

#endregion

using EveryoneIsJohnTracker.Types;

namespace EveryoneIsJohnTracker.Controls.DiceRoller.Models
{
    internal class DiceModel : PropertyChangedBase
    {
        private int _modifier;
        private int _sides;
        private int _totalDice;

        public string DiceName =>
            Modifier == 0 ? $"{TotalDice}d{Sides}" : $"{TotalDice}d{Sides}{Modifier:+#;-#}";

        public int Sides
        {
            get => _sides;
            set
            {
                SetValue(ref _sides, value);
                OnPropertyChanged(nameof(DiceName));
            }
        }

        public int TotalDice
        {
            get => _totalDice;
            set
            {
                SetValue(ref _totalDice, value);
                OnPropertyChanged(nameof(DiceName));
            }
        }

        public int Modifier
        {
            get => _modifier;
            set
            {
                SetValue(ref _modifier, value);
                OnPropertyChanged(nameof(DiceName));
            }
        }

        public DiceModel(int totalDice, int sides, int modifier)
        {
            TotalDice = totalDice;
            Sides = sides;
            Modifier = modifier;
        }
    }
}