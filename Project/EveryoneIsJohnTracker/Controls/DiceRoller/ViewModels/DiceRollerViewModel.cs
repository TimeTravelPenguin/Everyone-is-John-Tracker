#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: DiceRollerViewModel.cs
// 
// Current Data:
// 2019-12-18 11:23 AM
// 
// Creation Date:
// 2019-12-18 11:21 AM

#endregion

using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using EveryoneIsJohnTracker.Controls.DiceRoller.Models;
using EveryoneIsJohnTracker.Models.Logger;
using EveryoneIsJohnTracker.Types;
using Microsoft.Expression.Interactivity.Core;

namespace EveryoneIsJohnTracker.Controls.DiceRoller.ViewModels
{
    internal class DiceRollerViewModel : PropertyChangedBase
    {
        private static readonly Random Random = new Random();
        private static readonly object SyncLock = new object();

        private readonly ObservableCollection<DiceModel> _commonDice = new ObservableCollection<DiceModel>
        {
            // Add common dice => 1d4, 1d6, 1d8, 1d10, 1d12, 1d20

            new DiceModel(1, 4, 0),
            new DiceModel(1, 6, 0),
            new DiceModel(1, 8, 0),
            new DiceModel(1, 10, 0),
            new DiceModel(1, 12, 0),
            new DiceModel(1, 20, 0)
        };

        private string _customDiceRule = "2d6+3";
        private bool _customDiceRuleEnabled;
        private int _recentRoll;
        private readonly ILogger _logger = LogFactory.NewLogger(LoggerType.NullLogger);

        public SelectedItemDataModel<DiceModel> ComboBoxCommonDice { get; set; }


        public ActionCommand CommandRollDice { get; set; }

        public int RecentRoll
        {
            get => _recentRoll;
            set => SetValue(ref _recentRoll, value);
        }

        public bool CustomDiceRuleEnabled
        {
            get => _customDiceRuleEnabled;
            set => SetValue(ref _customDiceRuleEnabled, value);
        }

        public string CustomDiceRule
        {
            get => _customDiceRule;
            set => SetValue(ref _customDiceRule, value);
        }

        public DiceRollerViewModel()
        {
            ComboBoxCommonDice = new SelectedItemDataModel<DiceModel>
            {
                Data = _commonDice
            };

            CommandRollDice = new ActionCommand(RollDice);
        }

        public DiceRollerViewModel(ILogger logger) :this()
        {
            _logger = logger;
        }

        /// <summary>
        ///     Rolls a dice and stores the result
        /// </summary>
        private void RollDice()
        {
            if (CustomDiceRuleEnabled)
            {
                // Using CultureInfo.InvariantCulture because if parsing int on a system in another culture, there may be errors.
                // For example, 1,234 in US parses to 1234, in Ger it is parsed as 1 due to ',' and '.' being swapped semantics.
                var values = Regex.Matches(CustomDiceRule, @"[+-]?\d+")
                    .Cast<Match>()
                    .Select(m => int.Parse(m.Value, CultureInfo.InvariantCulture))
                    .ToArray();

                RecentRoll = values.Length switch
                {
                    2 => RandomInt(new DiceModel(values[0], values[1], 0)),
                    3 => RandomInt(new DiceModel(values[0], values[1], values[2])),
                    _ => throw new NotImplementedException()
                };
            }
            else if (ComboBoxCommonDice.SelectedIndex >= 0 &&
                     ComboBoxCommonDice.SelectedIndex < ComboBoxCommonDice.Data.Count)
            {
                RecentRoll = RandomInt(_commonDice[ComboBoxCommonDice.SelectedIndex]);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        ///     Returns a random number given a DiceModel
        /// </summary>
        /// <param name="dice">Object containing the number of dice, the number of sides, and the modifier</param>
        /// <returns>Random integer</returns>
        private int RandomInt(DiceModel dice)
        {
            var sum = 0;
            var rolls = new int[dice.TotalDice];

            lock (SyncLock)
            {
                for (var i = 0; i < dice.TotalDice; i++)
                {
                    var roll = Random.Next(1, dice.Sides + 1);
                    rolls[i] = roll;
                    sum += roll;
                }
            }


            _logger.LogDiceRoll(rolls, dice.Modifier);
            return sum + dice.Modifier;
        }
    }
}