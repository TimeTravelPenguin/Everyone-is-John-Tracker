using System;

namespace EveryoneIsJohnTracker.Models
{
    internal class SkillModel : PropertyChangedBase
    {
        private string _skillName;

        public string SkillName
        {
            get => _skillName;
            set => SetValue(ref _skillName, value);
        }
    }
}