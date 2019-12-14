#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: SkillModel.cs
// 
// Current Data:
// 2019-12-14 11:55 AM
// 
// Creation Date:
// 2019-09-27 9:12 AM

#endregion

using EveryoneIsJohnTracker.Base;
using EveryoneIsJohnTracker.Models.OutputLoggers;
using Newtonsoft.Json;

namespace EveryoneIsJohnTracker.Models
{
    internal class SkillModel : PropertyChangedBase
    {
        private ILogger _logger;
        private string _name;

        [JsonIgnore]
        public ILogger Logger
        {
            get => _logger;
            set => SetValue(ref _logger, value);
        }

        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }
    }
}