#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: GameMasterModel.cs
// 
// Current Data:
// 2019-12-11 7:14 PM
// 
// Creation Date:
// 2019-09-28 9:40 PM

#endregion

using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using EveryoneIsJohnTracker.Base;
using EveryoneIsJohnTracker.Models.OutputLoggers;
using Newtonsoft.Json;

namespace EveryoneIsJohnTracker.Models
{
    internal class GameMasterModel : PropertyChangedBase
    {
        private ObservableCollection<ItemModel> _inventory = new ObservableCollection<ItemModel>();

        private ObservableCollection<VoiceModel> _voices = new ObservableCollection<VoiceModel>();

        public ObservableCollection<VoiceModel> Voices
        {
            get => _voices;
            set => SetValue(ref _voices, value);
        }

        public ObservableCollection<ItemModel> Inventory
        {
            get => _inventory;
            set => SetValue(ref _inventory, value);
        }

        [JsonIgnore] public static ILogger Logger { get; set; }

        public GameMasterModel(ILogger logger)
        {
            Logger = logger;

            Voices.CollectionChanged += VoiceCollectionChanged;
            Inventory.CollectionChanged += InventoryCollectionChanged;
        }


        /// <summary>
        ///     Events on modification of the Inventory collection
        /// </summary>
        private static void InventoryCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    foreach (var item in e.NewItems)
                    {
                        Logger.LogAddItem(item as ItemModel);
                    }

                    break;
                }

                case NotifyCollectionChangedAction.Remove:
                {
                    foreach (var item in e.OldItems)
                    {
                        Logger.LogRemoveInventoryItem(item as ItemModel);
                    }

                    break;
                }

                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                {
                    Logger.LogClearInventory();
                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException(nameof(e));
            }
        }

        /// <summary>
        ///     Events on modification of the Voice collection
        /// </summary>
        private static void VoiceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    foreach (var newItem in e.NewItems)
                    {
                        Logger.LogAddVoice(newItem as VoiceModel);
                    }

                    break;
                }

                case NotifyCollectionChangedAction.Remove:
                {
                    foreach (var newItem in e.OldItems)
                    {
                        Logger.LogRemoveVoice(newItem as VoiceModel);
                    }

                    break;
                }

                case NotifyCollectionChangedAction.Replace:
                {
                    break;
                }

                case NotifyCollectionChangedAction.Move:
                {
                    break;
                }

                case NotifyCollectionChangedAction.Reset:
                {
                    Logger.LogClearedVoiceCollection();
                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException(nameof(e));
            }
        }
    }
}