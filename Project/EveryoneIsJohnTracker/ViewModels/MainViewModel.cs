#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: MainViewModel.cs
// 
// Current Data:
// 2019-12-14 11:12 AM
// 
// Creation Date:
// 2019-12-13 3:01 PM

#endregion

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using EveryoneIsJohnTracker.Base;
using EveryoneIsJohnTracker.Extensions;
using EveryoneIsJohnTracker.Models;
using EveryoneIsJohnTracker.Models.OutputLoggers;
using Microsoft.Expression.Interactivity.Core;

namespace EveryoneIsJohnTracker.ViewModels
{
    internal class MainViewModel : PropertyChangedBase
    {
        private int _comboboxLevelBinding;
        private string _editableItemName;
        private SkillModel _editableSkillModel = new SkillModel(); // Used to bind to view
        private VoiceModel _editableVoiceModel = new VoiceModel(NullLogger); // Used to add to Voices Collection


        private int _listViewSelectedIndex;
        private ICollectionView _voiceCollectionView;

        public ICollectionView VoiceCollectionView
        {
            get => _voiceCollectionView;
            set => SetValue(ref _voiceCollectionView, value);
        }

        public VoiceModel EditableVoiceModel
        {
            get => _editableVoiceModel;
            set => SetValue(ref _editableVoiceModel, value);
        }

        public SkillModel EditableSkillModel
        {
            get => _editableSkillModel;
            set => SetValue(ref _editableSkillModel, value);
        }

        public string EditableItemName
        {
            get => _editableItemName;
            set => SetValue(ref _editableItemName, value);
        }

        public ActionCommand CommandOpenRules { get; set; }
        public ActionCommand CommandAddSkill { get; set; }
        public ActionCommand CommandAddVoice { get; set; }
        public ActionCommand CommandAddWillpower { get; set; }
        public ActionCommand CommandSubtractWillpower { get; set; }
        public ActionCommand CommandAddWillpowerAll { get; set; }
        public ActionCommand CommandSubtractWillpowerAll { get; set; }
        public ActionCommand CommandRemoveVoice { get; set; }
        public ActionCommand CommandAddObsessionPoint { get; set; }
        public ActionCommand CommandRemoveObsessionPoint { get; set; }
        public ActionCommand CommandAddItem { get; set; }
        public ActionCommand CommandLoadGame { get; set; }
        public ActionCommand CommandSaveGame { get; set; }
        public ActionCommand CommandNextTurn { get; set; }

        public int ComboboxLevelBinding
        {
            get => _comboboxLevelBinding;
            set => SetValue(ref _comboboxLevelBinding, value);
        }

        public int ListViewSelectedIndex
        {
            get => _listViewSelectedIndex;
            set => SetValue(ref _listViewSelectedIndex, value);
        }

        // GameMaster holds all the collections stored for the game
        public static GameMasterModel GameMaster { get; set; }

        public static ILogger OutputLogger { get; } = LogFactory.NewLogger(LoggerType.OutputLogger);
        private static ILogger NullLogger { get; } = LogFactory.NewLogger(LoggerType.NullLogger);

        public MainViewModel()
        {
            GameMaster = new GameMasterModel(OutputLogger);
            VoiceCollectionView = CollectionViewSource.GetDefaultView(GameMaster.Voices);

            CommandOpenRules =
                new ActionCommand(() => Process.Start(@"https://rulebook.io/games/everyone-is-john/rules"));

            CommandAddSkill = new ActionCommand(AddSkill);

            CommandAddVoice = new ActionCommand(AddVoice);

            CommandAddWillpower = new ActionCommand(() => GameMaster.AddWillpower(ListViewSelectedIndex, 1));

            CommandSubtractWillpower = new ActionCommand(() => GameMaster.AddWillpower(ListViewSelectedIndex, -1));

            CommandAddWillpowerAll = new ActionCommand(() => GameMaster.AddWillpowerAll(1));

            CommandSubtractWillpowerAll = new ActionCommand(() => GameMaster.AddWillpowerAll(-1));

            CommandRemoveVoice = new ActionCommand(RemoveVoice);

            CommandAddObsessionPoint = new ActionCommand(() => GameMaster.AddObsessionPoint(ListViewSelectedIndex, 1));

            CommandRemoveObsessionPoint =
                new ActionCommand(() => GameMaster.AddObsessionPoint(ListViewSelectedIndex, -1));

            CommandAddItem = new ActionCommand(AddItem);

            CommandLoadGame = new ActionCommand(() => GameMaster.FileInput(OutputLogger));

            CommandSaveGame = new ActionCommand(() => GameMaster.FileOutput(OutputLogger));

            CommandNextTurn = new ActionCommand(() => GameMaster.Turn++);

            // AddDemoData();
        }

        // This is strictly for debugging
        private void AddDemoData()
        {
            GameMaster.AddVoice(new VoiceModel(NullLogger)
                {
                    Name = "TimeTravelPenguin",
                    Willpower = 7,

                    Obsession = new ObsessionModel(NullLogger)
                    {
                        Name = "To become a Penguin",
                        Level = 3,
                        Points = 0
                    },
                    Skills = new ObservableCollection<SkillModel>
                    {
                        new SkillModel
                        {
                            Name = "Dancing"
                        },
                        new SkillModel
                        {
                            Name = "Cooking"
                        },
                        new SkillModel
                        {
                            Name = "Performing the dark arts"
                        }
                    }
                },
                OutputLogger);

            GameMaster.AddVoice(new VoiceModel(NullLogger)
                {
                    Name = "Caitlin",
                    Willpower = 7,

                    Obsession = new ObsessionModel(NullLogger)
                    {
                        Name = "To make new friends",
                        Level = 2,
                        Points = 0
                    },

                    Skills = new ObservableCollection<SkillModel>
                    {
                        new SkillModel
                        {
                            Name = "Talking"
                        },
                        new SkillModel
                        {
                            Name = "Eating"
                        },
                        new SkillModel
                        {
                            Name = "Hugging"
                        }
                    }
                },
                OutputLogger);
        }

        private void AddItem()
        {
            if (GameMaster.Inventory.Any(item => EditableItemName == item.Name))
            {
                MessageBox.Show("This item already exists");
                return;
            }

            if (!string.IsNullOrEmpty(EditableItemName))
            {
                // Instantiate with NullLogger to prevent logging, and then pass set variable values
                GameMaster.AddItem(new ItemModel(NullLogger)
                {
                    Name = EditableItemName,
                    Count = 1,
                    Description = ""
                }, OutputLogger);

                EditableItemName = "";
            }
        }

        private void AddSkill()
        {
            if (!string.IsNullOrEmpty(EditableSkillModel.Name))
            {
                EditableVoiceModel.Skills.Add(new SkillModel
                {
                    Name = EditableSkillModel.Name
                });

                EditableSkillModel.Name = "";
            }
        }

        private void AddVoice()
        {
            if (GameMaster.Voices.Any(voice => EditableVoiceModel.Name == voice.Name))
            {
                MessageBox.Show("This voice already exists");
                return;
            }

            if (!string.IsNullOrEmpty(EditableVoiceModel.Name))
            {
                EditableVoiceModel.Obsession.Level = ComboboxLevelBinding + 1;

                GameMaster.AddVoice(EditableVoiceModel, OutputLogger);

                EditableVoiceModel.Clear();
                EditableSkillModel.Name = "";
                ComboboxLevelBinding = 0;
            }
        }

        private void RemoveVoice()
        {
            GameMaster.RemoveVoiceAt(ListViewSelectedIndex);
            ListViewSelectedIndex = 0;
        }
    }
}