﻿using System;
using EveryoneIsJohnTracker.Models;
using EveryoneIsJohnTracker.Models.OutputLoggers;

namespace EveryoneIsJohnTracker.Extensions
{
    internal static class GameMasterExtension
    {
        public static void AddItem(this GameMasterModel gameMaster, ItemModel item, IOutputLogger logger)
        {
            item.Logger = logger;
            gameMaster.Inventory.Add(item);
        }

        public static void AddVoice(this GameMasterModel gameMaster, VoiceModel voice, IOutputLogger logger)
        {
            gameMaster.Voices.Add(new VoiceModel(voice) {Logger = logger});
        }

        public static void RemoveVoiceAt(this GameMasterModel gameMaster, int index)
        {
            if (gameMaster.Voices.Count > 0 && index >= 0 && index < gameMaster.Voices.Count)
            {
                gameMaster.Voices.RemoveAt(index);
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
            foreach (var voiceModel in gameMaster.Voices)
            {
                if (voiceModel.Willpower + value >= 0)
                {
                    voiceModel.Willpower += value;
                }
            }
        }

        internal static void AddObsessionPoint(this GameMasterModel gameMaster, int index, int value)
        {
            if (gameMaster.Voices.Count > 0 && gameMaster.Voices[index].Obsession.Points + value >= 0)
            {
                gameMaster.Voices[index].Obsession.Points += value;
            }
        }

        internal static void LoadGameData(this GameMasterModel gameMaster, GameMasterModel data, IOutputLogger logger)
        {
            // Overwrite gameMaster data with new game data
            gameMaster.SetLogger(new OutputNullLogger());

            var errorFlag = false;
            
            gameMaster.Inventory.Clear();
            foreach (var item in data.Inventory)
            {
                try
                {
                    item.Logger = logger;

                    gameMaster.Inventory.Add(item);
                }
                catch (Exception ex)
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
                catch (Exception e)
                {
                    errorFlag = true;
                }
            }

            gameMaster.SetLogger(logger);
            logger.LogDataLoad(errorFlag);
        }
    }
}