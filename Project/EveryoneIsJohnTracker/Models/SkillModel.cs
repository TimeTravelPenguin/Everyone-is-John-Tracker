#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: SkillModel.cs
// 
// Current Data:
// 2019-12-16 12:05 AM
// 
// Creation Date:
// 2019-09-27 9:12 AM

#endregion

using EveryoneIsJohnTracker.Models.Logger;
using EveryoneIsJohnTracker.Types;
using Newtonsoft.Json;

namespace EveryoneIsJohnTracker.Models
{
    internal class SkillModel : PropertyChangedBase
    {
        private ILogger _logger = LogFactory.NewLogger(LoggerType.NullLogger);
        private string _name;
        private VoiceModel _parentVoiceModel;

        [JsonIgnore]
        public VoiceModel ParentVoiceModel
        {
            get => _parentVoiceModel;
            set => SetValue(ref _parentVoiceModel, value);
        }

        [JsonIgnore]
        public ILogger Logger
        {
            get => _logger;
            set => SetValue(ref _logger, value);
        }

        public string Name
        {
            get => _name;
            set
            {
                var oldName = _name;

                if (oldName != value)
                {
                    SetValue(ref _name, value);
                    Logger.LogSkillNameChanged(_name, oldName, ParentVoiceModel==null ? "" : ParentVoiceModel.Name);
                    ParentVoiceModel?.UpdateSkillsAsString();
                }
            }
        }

        public SkillModel()
        {
            ParentVoiceModel = null;
        }

        public SkillModel(VoiceModel parentVoice)
        {
            ParentVoiceModel = parentVoice;
        }
    }
}