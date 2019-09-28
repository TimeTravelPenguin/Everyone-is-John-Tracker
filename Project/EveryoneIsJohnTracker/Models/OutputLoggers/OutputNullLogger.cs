namespace EveryoneIsJohnTracker.Models.OutputLoggers
{
    internal class OutputNullLogger : IOutputLogger
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
    }
}