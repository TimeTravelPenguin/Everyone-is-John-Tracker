using System.Collections.ObjectModel;
using EveryoneIsJohnTracker.Base;

namespace EveryoneIsJohnTracker.Models.OutputLoggers
{
    internal class OutputLogger : PropertyChangedBase, IOutputLogger
    {
        public void LogAddItem(ItemModel item)
        {
            LogHistory.Add($"{item.Name} has been added to the inventory");
        }

        public void LogAddVoice(VoiceModel voice)
        {
            LogHistory.Add($"{voice.Name} has been added to the game");
        }

        public void LogRemoveVoice(VoiceModel voice)
        {
            LogHistory.Add($"{voice.Name} has been removed from the game");
        }

        public void LogClearedVoiceCollection()
        {
            LogHistory.Add("All voices have been removed from the game");
        }

        public void LogRemoveInventoryItem(ItemModel item)
        {
            LogHistory.Add($"{item.Name} has been removed to the inventory");
        }

        public void LogClearInventory()
        {
            LogHistory.Add("All items have been removed from the inventory");
        }

        public void LogNameChanged(string oldName, string newName)
        {
            LogHistory.Add($"{oldName} has been renamed to {newName}");
        }

        public void LogWillPowerChanged(string name, int oldValue, int newValue)
        {
            if (oldValue > newValue)
            {
                LogHistory.Add($"{name}'s Willpower has decremented from {oldValue} to {newValue}");
            }
            else if (newValue > oldValue)
            {
                LogHistory.Add($"{name}'s Willpower has incremented from {oldValue} to {newValue}");
            }
        }

        public void LogObsessionNameChanged(string name, string oldValue, string newValue)
        {
            LogHistory.Add($"{name}'s Obsession , {oldValue}, has been renamed to {newValue}");
        }

        public void LogObsessionLevelChanged(string name, string obsName, int oldValue, int newValue)
        {
            LogHistory.Add($"{name}'s Obsession, {obsName}, has been changed from Lv. {oldValue} to Lv. {newValue}");
        }

        public void LogObsessionPointsChanged(string name, int obsOldPts, int obsNewPts)
        {
            if (obsOldPts > obsNewPts)
            {
                LogHistory.Add($"{name} has lost {obsOldPts - obsNewPts} Obsession Points");
            }
            else if (obsNewPts > obsOldPts)
            {
                LogHistory.Add($"{name} has earned {obsNewPts - obsOldPts} Obsession Points");
            }
        }

        public void LogItemNameChanged(string oldName, string newName)
        {
            LogHistory.Add($"Item {oldName} has be renamed to {newName}");
        }

        public void LogItemCountChanged(string name, int oldCount, int newCount)
        {
            LogHistory.Add($"Item {name}'s count has been changed from {oldCount} to {newCount}");
        }

        public void LogItemDescriptionChanged(string name)
        {
            LogHistory.Add($"Item {name}'s description has been changed");
        }

        public void LogDataLoad(bool errorFlag)
        {
            LogHistory.Add(errorFlag
                ? "File has been loaded. There were issues loading some data"
                : "File has been successfully loaded");
        }

        public void LogDataSave(string fileName, bool errorFlag)
        {
            LogHistory.Add(errorFlag
                ? "There was an error saving the file. File was not saved..."
                : $"File {fileName} has been successfully saved");
        }

        private ObservableCollection<string> _logHistory = new ObservableCollection<string>();

        public ObservableCollection<string> LogHistory
        {
            get => _logHistory;
            set => SetValue(ref _logHistory, value);
        }

        public OutputLogger()
        {
            LogHistory.Add("This application is ready...");
            LogHistory.Add("This log will track all actions you take...");
        }
    }
}