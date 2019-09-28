using EveryoneIsJohnTracker.Base;
using EveryoneIsJohnTracker.Models.OutputLoggers;

namespace EveryoneIsJohnTracker.Models
{
    internal class ItemModel : PropertyChangedBase
    {
        private int _count;
        private string _description;
        private string _name;
        public IOutputLogger Logger { get; set; }

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

        public ItemModel(IOutputLogger logger)
        {
            Logger = logger;
        }
    }
}