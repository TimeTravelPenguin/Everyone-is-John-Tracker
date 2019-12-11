#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: NullLogger.cs
// 
// Current Data:
// 2019-12-11 7:02 PM
// 
// Creation Date:
// 2019-09-28 10:29 PM

#endregion

namespace EveryoneIsJohnTracker.Models.OutputLoggers
{
    internal class NullLogger : LoggerBase, ILogger
    {
        public void LogAddItem(ItemModel item)
        {
        }

        public void LogAddVoice(VoiceModel voice)
        {
        }

        public void LogRemoveVoice(VoiceModel voice)
        {
        }

        public void LogClearedVoiceCollection()
        {
        }

        public void LogRemoveInventoryItem(ItemModel item)
        {
        }

        public void LogClearInventory()
        {
        }

        public void LogNameChanged(string oldName, string newName)
        {
        }

        public void LogWillPowerChanged(string name, int oldValue, int newValue)
        {
        }

        public void LogObsessionLevelChanged(string name, string name1, int oldValue, int value)
        {
        }

        public void LogObsessionNameChanged(string oldValue, string value, string value1)
        {
        }

        public void LogObsessionPointsChanged(string voiceName, int oldValue, int value)
        {
        }

        public void LogItemNameChanged(string oldValue, string value)
        {
        }

        public void LogItemCountChanged(string name, int oldValue, int value)
        {
        }

        public void LogItemDescriptionChanged(string name)
        {
        }

        public void LogDataLoad(bool errorFlag)
        {
        }

        public void LogDataSave(string fileName, bool errorFlag)
        {
        }
    }
}