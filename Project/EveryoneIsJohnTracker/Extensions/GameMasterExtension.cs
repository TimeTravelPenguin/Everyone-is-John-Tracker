#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: GameMasterExtension.cs
// 
// Current Data:
// 2019-12-16 8:39 AM
// 
// Creation Date:
// 2019-09-28 9:56 PM

#endregion

using EveryoneIsJohnTracker.Models;
using EveryoneIsJohnTracker.Models.Logger;
using LiveCharts;
using LiveCharts.Defaults;

namespace EveryoneIsJohnTracker.Extensions
{
    internal static class GameMasterExtension
    {
        public static void AddItem(this GameMasterModel gameMaster, ItemModel item, ILogger logger)
        {
            item.Logger = logger;
            gameMaster.Inventory.Add(item);
        }

        public static void AddVoice(this GameMasterModel gameMaster, VoiceModel voice, ILogger logger)
        {
            if (voice != null)
            {
                var newVoice = new VoiceModel(voice)
                {
                    Logger = logger,
                    ScoreHistory = new ChartValues<ObservablePoint>()
                };

                // Add zero points from turn 1 to current turn
                for (var i = 0; i <= gameMaster.Turn; i++)
                {
                    newVoice.ScoreHistory.Add(new ObservablePoint(i, 0));
                }

                gameMaster.Voices.Add(newVoice);
                gameMaster.ChartModel.UpdateChartValues();
            }
        }

        public static void AddWillpower(this GameMasterModel gameMaster, VoiceModel voice, int value)
        {
            if (voice != null && voice.Willpower + value >= 0)
            {
                voice.Willpower += value;
            }
        }

        public static void AddWillpowerAll(this GameMasterModel gameMaster, int value)
        {
            foreach (var voiceModel in gameMaster.Voices)
            {
                if (voiceModel.Willpower + value >= 0)
                {
                    voiceModel.Willpower += value;
                }
            }
        }

        internal static void AddObsessionPoint(this GameMasterModel gameMaster, VoiceModel voice, int value)
        {
            if (voice != null && voice.ScoreHistory[gameMaster.Turn].Y + value >= 0)
            {
                voice.AddObsessionPoint(gameMaster.Turn, value);
            }
        }

        internal static void LoadGameData(this GameMasterModel gameMaster, GameMasterModel data, ILogger logger)
        {
            // Overwrite gameMaster data with new game data
            GameMasterModel.Logger = LogFactory.NewLogger(LoggerType.NullLogger);

            var errorFlag = false;

            gameMaster.Inventory.Clear();
            foreach (var item in data.Inventory)
            {
                try
                {
                    item.Logger = logger;

                    gameMaster.Inventory.Add(item);
                }
                catch
                {
                    errorFlag = true;
                }
            }

            gameMaster.Voices.Clear();
            foreach (var voice in data.Voices)
            {
                try
                {
                    voice.Logger = logger;
                    voice.Obsession.Logger = logger;

                    gameMaster.Voices.Add(voice);
                }
                catch
                {
                    errorFlag = true;
                }
            }

            gameMaster.Turn = data.Turn;

            GameMasterModel.Logger = logger;
            logger.LogDataLoad(errorFlag);
        }
    }
}