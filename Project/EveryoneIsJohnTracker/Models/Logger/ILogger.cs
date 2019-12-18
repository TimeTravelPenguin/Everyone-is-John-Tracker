#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: ILogger.cs
// 
// Current Data:
// 2019-12-18 11:45 AM
// 
// Creation Date:
// 2019-09-28 10:29 PM

#endregion

using System.Collections.ObjectModel;

namespace EveryoneIsJohnTracker.Models.Logger
{
    internal interface ILogger
    {
        ObservableCollection<Log> LogHistory { get; set; }
        void LogAddItem(ItemModel item);
        void LogAddVoice(VoiceModel voice);
        void LogTurnChange(int newTurn, int previousTurn);
        void LogRemoveVoice(VoiceModel voice);
        void LogClearedVoiceCollection();
        void LogRemoveInventoryItem(ItemModel item);
        void LogItemNameChanged(string oldValue, string value);
        void LogClearInventory();
        void LogNameChanged(string oldName, string value);
        void LogWillPowerChanged(string name, int oldValue, int value);
        void LogObsessionNameChanged(string name, string oldValue, string newValue);
        void LogObsessionLevelChanged(string name, int oldValue, int newValue);
        void LogItemCountChanged(string name, int oldValue, int value);
        void LogSkillNameChanged(string name, string oldName, string playerName);
        void LogObsessionPointsChanged(string voiceName, int oldValue, int value);
        void LogItemDescriptionChanged(string name);
        void LogDataLoad(bool errorFlag);
        void LogDataSave(string fileName, bool errorFlag);
        void LogDiceRoll(int[] rolls, int modifier);
    }
}