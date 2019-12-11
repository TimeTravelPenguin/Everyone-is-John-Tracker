#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: ILogger.cs
// 
// Current Data:
// 2019-12-11 7:02 PM
// 
// Creation Date:
// 2019-09-28 10:29 PM

#endregion

using System.Collections.ObjectModel;

namespace EveryoneIsJohnTracker.Models.OutputLoggers
{
    internal interface ILogger
    {
        ObservableCollection<Log> LogHistory { get; set; }
        void LogAddItem(ItemModel item);
        void LogAddVoice(VoiceModel voice);
        void LogRemoveVoice(VoiceModel voice);
        void LogClearedVoiceCollection();
        void LogRemoveInventoryItem(ItemModel item);
        void LogObsessionLevelChanged(string name, string name1, int oldValue, int value);
        void LogItemNameChanged(string oldValue, string value);
        void LogClearInventory();
        void LogNameChanged(string oldName, string value);
        void LogWillPowerChanged(string name, int oldValue, int value);
        void LogObsessionNameChanged(string oldValue, string value, string value1);
        void LogItemCountChanged(string name, int oldValue, int value);
        void LogObsessionPointsChanged(string voiceName, int oldValue, int value);
        void LogItemDescriptionChanged(string name);
        void LogDataLoad(bool errorFlag);
        void LogDataSave(string fileName, bool errorFlag);
    }
}