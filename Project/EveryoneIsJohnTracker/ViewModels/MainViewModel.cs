using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Data;
using EveryoneIsJohnTracker.Models;
using Microsoft.Expression.Interactivity.Core;
using NLog;

namespace EveryoneIsJohnTracker.ViewModels
{
    internal class MainViewModel : PropertyChangedBase
    {
        private int _comboboxLevelBinding;
        private string _editableItemName;
        private SkillModel _editableSkillModel = new SkillModel();
        private VoiceModel _editableVoiceModel = new VoiceModel();
        private ObservableCollection<ItemModel> _inventory = new ObservableCollection<ItemModel>();
        private int _listViewSelectedIndex;
        private OutputLogger _outputLogger = new OutputLogger();
        private ICollectionView _voiceCollectionView;

        private ObservableCollection<VoiceModel> _voices = new ObservableCollection<VoiceModel>
        {
            new VoiceModel
            {
                Name = "TimeTravelPenguin",
                Willpower = 7,
                Obsession = new ObsessionModel
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
            new VoiceModel
            {
                Name = "Caitlin",
                Willpower = 7,
                Obsession = new ObsessionModel
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
            }
        };

        public ObservableCollection<VoiceModel> Voices
        {
            get => _voices;
            set => SetValue(ref _voices, value);
        }

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

        public ObservableCollection<ItemModel> Inventory
        {
            get => _inventory;
            set => SetValue(ref _inventory, value);
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

        public OutputLogger OutputLogger
        {
            get => _outputLogger;
            set => SetValue(ref _outputLogger, value);
        }

        public static Logger Logger;

        public MainViewModel()
        {
            VoiceCollectionView = CollectionViewSource.GetDefaultView(Voices);

            CommandOpenRules =
                new ActionCommand(() => Process.Start(@"https://rulebook.io/games/everyone-is-john/rules"));

            CommandAddSkill = new ActionCommand(AddSkill);

            CommandAddVoice = new ActionCommand(AddVoice);

            CommandAddWillpower = new ActionCommand(() => AddWillPower(ListViewSelectedIndex));

            CommandSubtractWillpower = new ActionCommand(() => SubtractWillPower(ListViewSelectedIndex));

            CommandAddWillpowerAll = new ActionCommand(AddWillPowerAll);

            CommandSubtractWillpowerAll = new ActionCommand(SubtractWillPowerAll);

            CommandRemoveVoice = new ActionCommand(() => RemoveVoice(ListViewSelectedIndex));

            CommandAddObsessionPoint = new ActionCommand(() => AddObsessionPoint(ListViewSelectedIndex));

            CommandRemoveObsessionPoint = new ActionCommand(() => RemoveObsessionPoint(ListViewSelectedIndex));

            CommandAddItem = new ActionCommand(AddItem);

            Logger = LogManager.GetLogger("RichLogger");
            Logger.Info("This is a test");
        }

        private void AddItem()
        {
            var item = new ItemModel
            {
                Name = EditableItemName,
                Count = 1,
                Description = ""
            };

            Inventory.Add(item);

            OutputLogger.LogAddItem(item);

            Logger.Info(item.Name);

            EditableItemName = "";
        }

        private void RemoveObsessionPoint(int index)
        {
            if (Voices[index].Obsession.Points > 0)
            {
                Voices[index].Obsession.Points--;
            }
        }

        private void AddObsessionPoint(int index)
        {
            Voices[index].Obsession.Points++;
        }

        private void AddWillPower(int index)
        {
            Voices[index].Willpower++;
        }

        private void AddWillPowerAll()
        {
            foreach (var voiceModel in Voices)
            {
                voiceModel.Willpower++;
            }
        }

        private void SubtractWillPower(int index)
        {
            if (Voices[index].Willpower > 0)
            {
                Voices[index].Willpower--;
            }
        }

        private void SubtractWillPowerAll()
        {
            foreach (var voiceModel in Voices)
            {
                if (voiceModel.Willpower > 0)
                {
                    voiceModel.Willpower--;
                }
            }
        }

        private void RemoveVoice(int index)
        {
            if (Voices.Count > 0)
            {
                Voices.RemoveAt(index);
            }

            ListViewSelectedIndex = 0;
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
            Voices.Add(new VoiceModel
            {
                Name = EditableVoiceModel.Name,
                Willpower = EditableVoiceModel.Willpower,

                Obsession = new ObsessionModel
                {
                    Name = EditableVoiceModel.Obsession.Name,
                    Level = ComboboxLevelBinding + 1
                },

                Skills = new ObservableCollection<SkillModel>(EditableVoiceModel.Skills)
            });

            EditableVoiceModel.Clear();
            EditableSkillModel.Name = "";
            ComboboxLevelBinding = 0;
        }
    }
}