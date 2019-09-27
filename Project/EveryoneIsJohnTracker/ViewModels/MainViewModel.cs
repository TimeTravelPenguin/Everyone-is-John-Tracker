using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Data;
using EveryoneIsJohnTracker.Models;
using Microsoft.Expression.Interactivity.Core;

namespace EveryoneIsJohnTracker.ViewModels
{
    internal class MainViewModel : PropertyChangedBase
    {
        private int _comboboxLevelBinding;
        private SkillModel _editableSkillModel = new SkillModel();
        private VoiceModel _editableVoiceModel = new VoiceModel();

        private int _listViewSelectedIndex;
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

        public ActionCommand CommandOpenRules { get; set; }
        public ActionCommand CommandAddSkill { get; set; }
        public ActionCommand CommandAddVoice { get; set; }
        public ActionCommand CommandAddWillpower { get; set; }
        public ActionCommand CommandSubtractWillpower { get; set; }
        public ActionCommand CommandAddWillpowerAll { get; set; }
        public ActionCommand CommandSubtractWillpowerAll { get; set; }
        public ActionCommand CommandRemoveVoice { get; set; }

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