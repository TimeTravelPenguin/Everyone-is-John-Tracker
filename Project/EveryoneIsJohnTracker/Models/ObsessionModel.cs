#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: ObsessionModel.cs
// 
// Current Data:
// 2019-12-14 9:21 PM
// 
// Creation Date:
// 2019-09-27 9:13 AM

#endregion

using System.Windows;
using EveryoneIsJohnTracker.Base;
using EveryoneIsJohnTracker.Models.OutputLoggers;
using Newtonsoft.Json;

namespace EveryoneIsJohnTracker.Models
{
    internal class ObsessionModel : PropertyChangedBase
    {
        private int _level;
        private ILogger _logger;
        private string _name;
        private int _points;
        private string _voiceName;

        [JsonIgnore]
        public ILogger Logger
        {
            get => _logger;
            set => SetValue(ref _logger, value);
        }

        public string VoiceName
        {
            get => _voiceName;
            set => SetValue(ref _voiceName, value);
        }

        public int LevelIndex
        {
            get => _level - 1;
            set
            {
                SetValue(ref _level, value + 1);
                OnPropertyChanged(nameof(Level));
            }
        }

        public int Level
        {
            get => _level;
            set
            {
                var oldValue = _level;
                CheckLevelValue(ref value);
                SetValue(ref _level, value);
                OnPropertyChanged(nameof(LevelIndex));

                if (oldValue != value)
                {
                    Logger.LogObsessionLevelChanged(VoiceName, Name, oldValue, value);
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                var oldValue = _name;
                SetValue(ref _name, value);

                if (oldValue != value)
                {
                    Logger.LogObsessionNameChanged(VoiceName, oldValue, value);
                }
            }
        }

        public int Points
        {
            get => _points;
            set
            {
                var oldValue = _points;
                SetValue(ref _points, value);

                if (oldValue != value)
                {
                    Logger.LogObsessionPointsChanged(VoiceName, oldValue, value);
                }
            }
        }

        public ObsessionModel(ILogger logger)
        {
            Logger = logger;
        }

        public ObsessionModel()
        {
        }

        private static void CheckLevelValue(ref int level)
        {
            if (level >= 1 && level <= 3)
            {
                return;
            }

            level = 1;

            // TODO: Get rid of this garbo
            MessageBox.Show("Level value must be between 1 and 3", "Error assigning Obsession Level");
        }
    }
}