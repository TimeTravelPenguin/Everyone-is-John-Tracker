using System.Windows;

namespace EveryoneIsJohnTracker.Models
{
    internal class ObsessionModel : PropertyChangedBase
    {
        private int _level;
        private string _name;
        private int _points;

        public int Level
        {
            get => _level;
            set
            {
                CheckLevelValue(ref value);
                SetValue(ref _level, value);
            }
        }

        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        public int Points
        {
            get => _points;
            set => SetValue(ref _points, value);
        }


        internal void CheckLevelValue(ref int level)
        {
            if (level >= 1 && level <= 3)
            {
                return;
            }

            level = 1;

            // TODO: Get rid of this garbo
            MessageBox.Show("Level value must be between 1 and 3", "Error assigning Obsession Level");
        }
    }
}