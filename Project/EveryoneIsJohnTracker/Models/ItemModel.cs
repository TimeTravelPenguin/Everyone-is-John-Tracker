namespace EveryoneIsJohnTracker.Models
{
    internal class ItemModel : PropertyChangedBase
    {
        private int _count;
        private string _description;
        private string _name;

        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        public int Count
        {
            get => _count;
            set => SetValue(ref _count, value);
        }

        public string Description
        {
            get => _description;
            set => SetValue(ref _description, value);
        }
    }
}