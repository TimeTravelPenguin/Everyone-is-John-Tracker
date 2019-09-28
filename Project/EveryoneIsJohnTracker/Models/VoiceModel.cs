using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using EveryoneIsJohnTracker.Base;
using EveryoneIsJohnTracker.Models.OutputLoggers;

namespace EveryoneIsJohnTracker.Models
{
    internal class VoiceModel : PropertyChangedBase
    {
        private string _name;
        private ObsessionModel _obsession = new ObsessionModel();
        private ObservableCollection<SkillModel> _skills = new ObservableCollection<SkillModel>();
        private string _skillsAsString;
        private int _willpower;
        public IOutputLogger Logger { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                var oldName = Name;
                SetValue(ref _name, value);

                if (oldName != value)
                {
                    Logger.LogNameChanged(oldName, value);
                }

                Obsession.VoiceName = Name;
                
            }
        }

        public int Willpower
        {
            get => _willpower;
            set
            {
                var oldValue = Willpower;
                SetValue(ref _willpower, value);

                if (oldValue != value)
                {
                    Logger.LogWillPowerChanged(Name, oldValue, value);
                }
            }
        }

        public ObservableCollection<SkillModel> Skills
        {
            get => _skills;
            set
            {
                SetValue(ref _skills, value);
                SetValue(ref _skillsAsString, string.Join(", ", Skills.Select(x => x.Name).ToArray()));
            }
        }

        public string SkillsAsString => _skillsAsString;

        public ObsessionModel Obsession
        {
            get => _obsession;
            set => SetValue(ref _obsession, value);
        }

        public VoiceModel(IOutputLogger logger)
        {
            Logger = logger;
            Obsession.Logger = logger;
            Willpower = 7;
            Skills.CollectionChanged += SkillsCollectionChanged;
        }

        private void SkillsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //throw new NotImplementedException();
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