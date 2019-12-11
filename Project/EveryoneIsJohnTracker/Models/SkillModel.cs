#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: SkillModel.cs
// 
// Current Data:
// 2019-12-11 7:02 PM
// 
// Creation Date:
// 2019-09-27 9:12 AM

#endregion

using EveryoneIsJohnTracker.Base;

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