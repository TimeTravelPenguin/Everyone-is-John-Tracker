#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: SelectedItemDataModel.cs
// 
// Current Data:
// 2019-12-18 11:23 AM
// 
// Creation Date:
// 2019-12-18 11:23 AM

#endregion

using System.Collections.ObjectModel;
using EveryoneIsJohnTracker.Types;

namespace EveryoneIsJohnTracker.Controls.DiceRoller.Models
{
    internal class SelectedItemDataModel<T> : PropertyChangedBase
    {
        private ObservableCollection<T> _data = new ObservableCollection<T>();
        private int _selectedIndex;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                SetValue(ref _selectedIndex, value);
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public ObservableCollection<T> Data
        {
            get => _data;
            set => SetValue(ref _data, value);
        }

        public T SelectedItem => Data[SelectedIndex];
    }
}