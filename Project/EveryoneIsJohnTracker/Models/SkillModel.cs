using EveryoneIsJohnTracker.Base;
using EveryoneIsJohnTracker.Models.OutputLoggers;

namespace EveryoneIsJohnTracker.Models
{
    internal class SkillModel : PropertyChangedBase
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }
    }
}