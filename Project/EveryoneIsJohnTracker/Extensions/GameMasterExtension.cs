#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: GameMasterExtension.cs
// 
// Current Data:
// 2019-12-13 1:44 AM
// 
// Creation Date:
// 2019-09-28 9:56 PM

#endregion

using EveryoneIsJohnTracker.Models;
using EveryoneIsJohnTracker.Models.OutputLoggers;

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
            gameMaster.Voices.Add(new VoiceModel(voice) {Logger = logger});
            gameMaster.ChartModel.UpdateChartValues();
        }

        public static void RemoveVoiceAt(this GameMasterModel gameMaster, int index)
        {
            if (gameMaster.Voices.Count > 0 && index >= 0 && index < gameMaster.Voices.Count)
            {
                gameMaster.Voices.RemoveAt(index);
                gameMaster.ChartModel.UpdateChartValues();
            }
        }

        public static void AddWillpower(this GameMasterModel gameMaster, int index, int value)
        {
            if (gameMaster.Voices.Count > 0 && gameMaster.Voices[index].Willpower + value >= 0)
            {
                gameMaster.Voices[index].Willpower += value;
            }
        }

        public static void AddWillpowerAll(this GameMasterModel gameMaster, int value)
        {
            var change = false;
            foreach (var voiceModel in gameMaster.Voices)
            {
                if (voiceModel.Willpower + value >= 0)
                {
                    voiceModel.Willpower += value;
                    change = true;
                }
            }

            if (change)
            {
                gameMaster.IncrementHistory();
            }
        }

        internal static void AddObsessionPoint(this GameMasterModel gameMaster, int index, int value)
        {
            if (gameMaster.Voices.Count > 0 && gameMaster.Voices[index].Obsession.Points + value >= 0)
            {
                gameMaster.Voices[index].Obsession.Points += value;

                gameMaster.IncrementHistory();
                //gameMaster.UpdateChart();
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