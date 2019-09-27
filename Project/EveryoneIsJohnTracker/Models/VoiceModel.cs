using System.Collections.ObjectModel;

namespace EveryoneIsJohnTracker.Models
{
    internal class VoiceModel : PropertyChangedBase
    {
        private string _name;
        private ObsessionModel _obsession = new ObsessionModel();
        private ObservableCollection<SkillModel> _skills = new ObservableCollection<SkillModel>();
        private int _willpower;

        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        public int Willpower
        {
            get => _willpower;
            set => SetValue(ref _willpower, value);
        }

        public ObservableCollection<SkillModel> Skills
        {
            get => _skills;
            set => SetValue(ref _skills, value);
        }

        public ObsessionModel Obsession
        {
            get => _obsession;
            set => SetValue(ref _obsession, value);
        }

        public VoiceModel()
        {
            Willpower = 7;
        }

        internal void Clear()
        {
            Name = "";
            Willpower = 7;
            Skills.Clear();
            Obsession.Name = "";
            Obsession.Level = 1;
        }
    }
}