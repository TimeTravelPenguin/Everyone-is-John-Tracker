#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: NullLogger.cs
// 
// Current Data:
// 2019-12-18 11:45 AM
// 
// Creation Date:
// 2019-09-28 10:29 PM

#endregion

namespace EveryoneIsJohnTracker.Models.Logger
{
    internal class NullLogger : LoggerBase, ILogger
    {
        public void LogAddItem(ItemModel item)
        {
        }

        public void LogAddVoice(VoiceModel voice)
        {
        }

        public void LogTurnChange(int newTurn, int previousTurn)
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

        public void LogObsessionLevelChanged(string name, int oldValue, int value)
        {
        }

        public void LogObsessionNameChanged(string oldValue, string value, string value1)
        {
        }

        public void LogSkillNameChanged(string name, string oldName, string playerName)
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

        public void LogDiceRoll(int[] rolls, int modifier)
        {
        }
    }
}