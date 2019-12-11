#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: ItemModel.cs
// 
// Current Data:
// 2019-12-11 7:02 PM
// 
// Creation Date:
// 2019-09-27 3:04 PM

#endregion

using EveryoneIsJohnTracker.Base;
using EveryoneIsJohnTracker.Models.OutputLoggers;
using Newtonsoft.Json;

namespace EveryoneIsJohnTracker.Models
{
    internal class ItemModel : PropertyChangedBase
    {
        private int _count;
        private string _description;
        private string _name;

        [JsonIgnore] public ILogger Logger { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                var oldValue = Name;
                SetValue(ref _name, value);

                if (oldValue != value)
                {
                    Logger.LogItemNameChanged(oldValue, value);
                }
            }
        }

        public int Count
        {
            get => _count;
            set
            {
                var oldValue = Count;
                SetValue(ref _count, value);

                if (oldValue != value)
                {
                    Logger.LogItemCountChanged(Name, oldValue, value);
                }
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                var oldValue = Description;
                SetValue(ref _description, value);

                if (oldValue != value)
                {
                    Logger.LogItemDescriptionChanged(Name);
                }
            }
        }

        public ItemModel()
        {
            Logger = LogFactory.NewLogger(LoggerType.NullLogger);
        }

        public ItemModel(ILogger logger)
        {
            Logger = logger;
        }
    }
}