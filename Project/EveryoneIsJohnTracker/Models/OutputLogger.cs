using System.Collections.ObjectModel;

namespace EveryoneIsJohnTracker.Models
{
    internal class OutputLogger : PropertyChangedBase
    {
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

        public void LogAddItem(ItemModel item)
        {
            LogHistory.Add($"{item.Name} has been added to the inventory");
        }

        public void LogAddWillpower(VoiceModel voice)
        {
            LogHistory.Add($"{voice.Name}'s Willpower has increased to {voice.Willpower}");
        }

        public void LogSubtractWillpower(VoiceModel voice)
        {
            LogHistory.Add($"{voice.Name}'s Willpower has decreased to {voice.Willpower}");
        }

        public void LogAddVoice(VoiceModel voice)
        {
            LogHistory.Add($"{voice.Name} has been added to the game");
        }

        public void LogRemoveVoice(VoiceModel voice)
        {
            LogHistory.Add($"{voice.Name} has been removed from the game");
        }

        public void LogAddObsessionPoint(VoiceModel voice)
        {
            LogHistory.Add($"{voice.Name} has earned 1 Obsession Point");
        }

        public void LogRemoveObsessionPoint(VoiceModel voice)
        {
            LogHistory.Add($"{voice.Name} has lost 1 Obsession Point");
        }
    }
}