using System;
using System.Collections.ObjectModel;

namespace EveryoneIsJohnTracker.Models
{
    internal class OutputLogger : PropertyChangedBase
    {
        private ObservableCollection<string> _log = new ObservableCollection<string>();

        public ObservableCollection<string> Log
        {
            get => _log;
            set => SetValue(ref _log, value);
        }

        public string LogAsString => string.Join(Environment.NewLine, Log);

        public OutputLogger()
        {
            Log.Add("Initializing application...");
            Log.Add("Done!");
        }

        public void LogAddItem(ItemModel item)
        {
            Log.Add($"{item.Name} has been added to the inventory");
        }
    }
}