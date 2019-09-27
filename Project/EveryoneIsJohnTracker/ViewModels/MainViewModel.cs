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
        private SkillModel _editableSkillModel = new SkillModel();
        private VoiceModel _editableVoiceModel = new VoiceModel();
        private ICollectionView _voiceCollectionView;
        private ObservableCollection<VoiceModel> _voices = new ObservableCollection<VoiceModel>();

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

        public MainViewModel()
        {
            VoiceCollectionView = CollectionViewSource.GetDefaultView(Voices);

            CommandOpenRules =
                new ActionCommand(() => Process.Start(@"https://rulebook.io/games/everyone-is-john/rules"));

            CommandAddSkill = new ActionCommand(AddSkill);

            CommandAddVoice = new ActionCommand(AddVoice);
        }

        private void AddSkill()
        {
            EditableVoiceModel.Skills.Add(new SkillModel
            {
                SkillName = EditableSkillModel.SkillName
            });

            EditableSkillModel.SkillName = "";
        }

        private void AddVoice()
        {
            Voices.Add(new VoiceModel
            {
                Name = EditableVoiceModel.Name,
                Willpower = EditableVoiceModel.Willpower,
                Obsession = EditableVoiceModel.Obsession,
                Skills = EditableVoiceModel.Skills
            });

            EditableVoiceModel.Clear();
            EditableSkillModel.SkillName = "";
        }
    }
}