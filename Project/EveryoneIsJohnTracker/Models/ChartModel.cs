#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: ChartModel.cs
// 
// Current Data:
// 2019-12-16 10:11 AM
// 
// Creation Date:
// 2019-12-12 10:48 PM

#endregion

using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using EveryoneIsJohnTracker.Types;
using LiveCharts;
using LiveCharts.Wpf;

namespace EveryoneIsJohnTracker.Models
{
    internal class ChartModel : PropertyChangedBase
    {
        private SeriesCollection _playerSeriesCollection = new SeriesCollection();
        private Func<int, string> _yFormatter;
        public ObservableCollection<VoiceModel> PlayerData;

        public SeriesCollection PlayerSeriesCollection
        {
            get => _playerSeriesCollection;
            set => SetValue(ref _playerSeriesCollection, value);
        }

        public Func<int, string> YFormatter
        {
            get => _yFormatter;
            set => SetValue(ref _yFormatter, value);
        }

        public ChartModel(ObservableCollection<VoiceModel> voices)
        {
            PlayerData = voices;
            YFormatter = i => i.ToString(CultureInfo.InvariantCulture) + " pts";
            UpdateChartValues();
        }

        public void UpdateChartValues()
        {
            // Check _playerData for voice.Name that aren't previously added to _playerSeriesCollection and add
            foreach (var voiceModel in PlayerData)
            {
                if (_playerSeriesCollection.All(series => series.Title != voiceModel.Name))
                {
                    var lineSeries = new LineSeries
                    {
                        Title = voiceModel.Name,
                        Values = voiceModel.ScoreHistory,
                        LineSmoothness = 0.5
                    };

                    PlayerSeriesCollection.Add(lineSeries);
                }
            }

            // Check PlayerSeriesCollection for names that aren't in _playerData and remove
            foreach (var series in _playerSeriesCollection)
            {
                if (PlayerData.All(player => player.Name != series.Title))
                {
                    PlayerSeriesCollection.Remove(series);
                }
            }
        }
    }
}