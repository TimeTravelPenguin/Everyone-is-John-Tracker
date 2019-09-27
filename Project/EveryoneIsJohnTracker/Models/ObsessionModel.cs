namespace EveryoneIsJohnTracker.Models
{
    internal class ObsessionModel : PropertyChangedBase
    {
        private int _level;
        private string _name;

        public int Level
        {
            get => _level;
            set => SetValue(ref _level, value);
        }

        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }
    }
}