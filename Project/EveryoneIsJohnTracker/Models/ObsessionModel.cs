#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: ObsessionModel.cs
// 
// Current Data:
// 2019-12-16 2:42 AM
// 
// Creation Date:
// 2019-09-27 9:13 AM

#endregion

using System.Windows;
using EveryoneIsJohnTracker.Models.Logger;
using EveryoneIsJohnTracker.Types;
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
            set => Level = value + 1;
        }

        public int Level
        {
            get => _level;
            set
            {
                var oldValue = _level;

                if (oldValue != value)
                {
                    CheckLevelValue(ref value);
                    SetValue(ref _level, value);
                    OnPropertyChanged(nameof(LevelIndex));

                    Logger.LogObsessionLevelChanged(VoiceName, oldValue, value);
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                var oldValue = _name;

                if (oldValue != value)
                {
                    SetValue(ref _name, value);

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

                if (oldValue != value)
                {
                    SetValue(ref _points, value);

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