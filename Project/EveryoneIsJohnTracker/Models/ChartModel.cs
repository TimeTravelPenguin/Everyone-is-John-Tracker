#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: ChartModel.cs
// 
// Current Data:
// 2019-12-13 1:53 AM
// 
// Creation Date:
// 2019-12-12 10:48 PM

#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using EveryoneIsJohnTracker.Base;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace EveryoneIsJohnTracker.Models
{
    internal class ChartModel : PropertyChangedBase
    {
        private SeriesCollection _playerSeriesCollection = new SeriesCollection();
        private Func<int, string> _yFormatter;

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

        public ChartModel(IEnumerable<VoiceModel> voices)
        {
            YFormatter = i => i.ToString(CultureInfo.InvariantCulture) + " pts";
            UpdateValues(voices);
        }

        public void UpdateValues(IEnumerable<VoiceModel> voices)
        {
            PlayerSeriesCollection.Clear();

            foreach (var voiceModel in voices)
            {
                var lineSeries = new LineSeries
                {
                    Title = voiceModel.Name,
                    Values = new ChartValues<ObservablePoint>(),
                    LineSmoothness = 0.5
                };

                foreach (var (turn, score) in voiceModel.ScoreHistory)
                {
                    lineSeries.Values.Add(new ObservablePoint(turn, score));
                }

                PlayerSeriesCollection.Add(lineSeries);
            }

            OnPropertyChanged(nameof(PlayerSeriesCollection));
        }
    }
}