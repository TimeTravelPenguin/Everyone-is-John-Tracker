﻿#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: OutputLogger.cs
// 
// Current Data:
// 2019-12-18 11:45 AM
// 
// Creation Date:
// 2019-09-28 10:29 PM

#endregion

using System;
using System.Linq;

namespace EveryoneIsJohnTracker.Models.Logger
{
    internal class OutputLogger : LoggerBase, ILogger
    {
        public void LogAddItem(ItemModel item)
        {
            LogHistory.Add(new Log($"{item.Name} has been added to the inventory"));
        }

        public void LogAddVoice(VoiceModel voice)
        {
            LogHistory.Add(new Log($"{voice.Name} has been added to the game"));
        }

        public void LogTurnChange(int newTurn, int previousTurn)
        {
            if (newTurn > previousTurn)
            {
                LogHistory.Add(new Log($"Incremented to turn {newTurn}"));
            }
            else if (newTurn < previousTurn)
            {
                LogHistory.Add(new Log($"Decremented to turn {newTurn}"));
            }
        }

        public void LogRemoveVoice(VoiceModel voice)
        {
            LogHistory.Add(new Log($"{voice.Name} has been removed from the game"));
        }

        public void LogClearedVoiceCollection()
        {
            LogHistory.Add(new Log("All voices have been removed from the game"));
        }

        public void LogRemoveInventoryItem(ItemModel item)
        {
            LogHistory.Add(new Log($"{item.Name} has been removed to the inventory"));
        }

        public void LogClearInventory()
        {
            LogHistory.Add(new Log("All items have been removed from the inventory"));
        }

        public void LogNameChanged(string oldName, string newName)
        {
            LogHistory.Add(new Log($"{oldName} has been renamed to {newName}"));
        }

        public void LogWillPowerChanged(string name, int oldValue, int newValue)
        {
            if (oldValue > newValue)
            {
                LogHistory.Add(new Log($"{name}'s Willpower has decremented from {oldValue} to {newValue}"));
            }
            else if (newValue > oldValue)
            {
                LogHistory.Add(new Log($"{name}'s Willpower has incremented from {oldValue} to {newValue}"));
            }
        }

        public void LogObsessionNameChanged(string name, string oldValue, string newValue)
        {
            LogHistory.Add(new Log($"{name}'s Obsession, {oldValue}, has been renamed to {newValue}"));
        }

        public void LogObsessionLevelChanged(string name, int oldValue, int newValue)
        {
            LogHistory.Add(
                new Log($"{name}'s Obsession has been changed from Lv. {oldValue} to Lv. {newValue}"));
        }

        public void LogSkillNameChanged(string name, string oldName, string playerName)
        {
            LogHistory.Add(new Log($"{name}'s Skill \"{oldName}\" has been changed to \"{name}\""));
        }

        public void LogObsessionPointsChanged(string name, int obsOldPts, int obsNewPts)
        {
            if (obsOldPts > obsNewPts)
            {
                LogHistory.Add(new Log(
                    $"{name} has lost {obsOldPts - obsNewPts} Obsession Point{(obsOldPts - obsNewPts == 1 ? "" : "s")}"));
            }
            else if (obsNewPts > obsOldPts)
            {
                LogHistory.Add(new Log(
                    $"{name} has earned {obsNewPts - obsOldPts} Obsession Point{(obsOldPts - obsNewPts == 1 ? "" : "s")}"));
            }
        }

        public void LogItemNameChanged(string oldName, string newName)
        {
            LogHistory.Add(new Log($"Item {oldName} has be renamed to {newName}"));
        }

        public void LogItemCountChanged(string name, int oldCount, int newCount)
        {
            LogHistory.Add(new Log($"Item {name}'s count has been changed from {oldCount} to {newCount}"));
        }

        public void LogItemDescriptionChanged(string name)
        {
            LogHistory.Add(new Log($"Item {name}'s description has been changed"));
        }

        public void LogDataLoad(bool errorFlag)
        {
            LogHistory.Add(new Log(errorFlag
                ? "File has been loaded. There were issues loading some data"
                : "File has been successfully loaded"));
        }

        public void LogDataSave(string fileName, bool errorFlag)
        {
            LogHistory.Add(new Log(errorFlag
                ? "There was an error saving the file. File was not saved..."
                : $"File {fileName} has been successfully saved"));
        }

        public void LogDiceRoll(int[] rolls, int modifier)
        {
            var output = rolls.Length switch
            {
                0 => throw new NotImplementedException(),
                1 => $"{rolls.Length} dice rolled: {rolls.Sum() + modifier}",
                _ => $"{rolls.Length} dice rolled: {string.Join(", ", rolls)} for a total of {rolls.Sum() + modifier}"
            };

            if (modifier != 0)
            {
                output += $" ({modifier:+#;-#} modifier)";
            }

            LogHistory.Add(new Log(output));
        }

        public OutputLogger()
        {
            LogHistory.Add(new Log("This application is ready..."));
            LogHistory.Add(new Log("This log will track all actions you take..."));
        }
    }
}