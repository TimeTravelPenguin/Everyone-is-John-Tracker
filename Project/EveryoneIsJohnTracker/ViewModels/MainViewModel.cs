using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
        private VoiceModel _editableVoiceModel = new VoiceModel(OutputNullLogger); // Used to add to Voices Collection


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

        public static OutputLogger OutputLogger { get; } = new OutputLogger();
        private static readonly OutputNullLogger OutputNullLogger = new OutputNullLogger();

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

            AddDemoData();
        }

        // This is strictly for debugging
        private void AddDemoData()
        {
            GameMaster.AddVoice(new VoiceModel(OutputNullLogger)
                {
                    Name = "TimeTravelPenguin",
                    Willpower = 7,

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
                new ObsessionModel(OutputNullLogger)
                {
                    Name = "To become a Penguin",
                    Level = 3,
                    Points = 0
                }, OutputLogger);

            GameMaster.AddVoice(new VoiceModel(OutputNullLogger)
                {
                    Name = "Caitlin",
                    Willpower = 7,

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
                new ObsessionModel(OutputNullLogger)
                {
                    Name = "To make new friends",
                    Level = 2,
                    Points = 0
                }, OutputLogger);
        }

        private void AddItem()
        {
            GameMaster.AddItem(new ItemModel(OutputNullLogger)
            {
                Name = EditableItemName,
                Count = 1,
                Description = ""
            }, OutputLogger);

            EditableItemName = "";
        }

        private void AddSkill()
        {
            EditableVoiceModel.Skills.Add(new SkillModel
            {
                Name = EditableSkillModel.Name
            });

            EditableSkillModel.Name = "";
        }

        private void AddVoice()
        {
            GameMaster.AddVoice(new VoiceModel(OutputNullLogger)
            {
                Name = EditableVoiceModel.Name,
                Willpower = EditableVoiceModel.Willpower,

                Skills = new ObservableCollection<SkillModel>(EditableVoiceModel.Skills)
            }, new ObsessionModel
            {
                Name = EditableVoiceModel.Obsession.Name,
                Level = ComboboxLevelBinding + 1
            }, OutputLogger);

            EditableVoiceModel.Clear();
            EditableSkillModel.Name = "";
            ComboboxLevelBinding = 0;
        }

        private void RemoveVoice()
        {
            GameMaster.RemoveVoiceAt(ListViewSelectedIndex);
            ListViewSelectedIndex = 0;
        }
    }
}