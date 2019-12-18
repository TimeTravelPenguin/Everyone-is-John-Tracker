#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: ExportChartViewModel.cs
// 
// Current Data:
// 2019-12-19 2:14 AM
// 
// Creation Date:
// 2019-12-18 9:01 PM

#endregion

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EveryoneIsJohnTracker.Models;
using EveryoneIsJohnTracker.Types;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using Microsoft.Expression.Interactivity.Core;

namespace EveryoneIsJohnTracker.ViewModels
{
    internal class ExportChartViewModel : PropertyChangedBase
    {
        private ChartModel _chartModel = new ChartModel();
        private string _hexColourValue = "#FFFFFF";
        private Image _imageRender = new Image();

        private double _imageZoom = 1;
        private int _renderHeight = 720;
        private int _renderWidth = 1080;

        public Image ImageRender
        {
            get => _imageRender;
            set => SetValue(ref _imageRender, value);
        }

        public ChartModel ChartModel
        {
            get => _chartModel;
            set => SetValue(ref _chartModel, value);
        }

        public bool IsBackgroundTransparent { get; set; }

        public string HexColourValue
        {
            get => _hexColourValue;
            set => SetValue(ref _hexColourValue, value);
        }

        public int RenderHeight
        {
            get => _renderHeight;
            set => SetValue(ref _renderHeight, value);
        }

        public int RenderWidth
        {
            get => _renderWidth;
            set => SetValue(ref _renderWidth, value);
        }

        public ActionCommand CommandChangeOutputPath { get; }
        public ActionCommand CommandRenderImage { get; }
        public ActionCommand CommandSaveImage { get; }
        public ActionCommand CommandMouseWheel { get; }

        public ExportChartViewModel()
        {
        }

        public ExportChartViewModel(ChartModel chartModel)
        {
            _chartModel = new ChartModel(chartModel);

            CommandChangeOutputPath = new ActionCommand(ChangeOutput);

            CommandRenderImage = new ActionCommand(RenderImage);

            CommandSaveImage = new ActionCommand(SaveImage);

            CommandMouseWheel = new ActionCommand(MouseWheel);
        }

        private void MouseWheel(object obj)
        {
            if (obj is MouseWheelEventArgs e)
            {
                if (e.Delta > 0)
                {
                    _imageZoom += 0.1;
                }
                else
                {
                    _imageZoom -= 0.1;
                }

                var scale = new ScaleTransform(_imageZoom, _imageZoom);
                ImageRender.LayoutTransform = scale;
                e.Handled = true;
            }
            else
            {
                throw new ArgumentException("The passed parameter is not of type MouseWheelEventArgs", nameof(obj));
            }
        }

        private Chart GenerateChart()
        {
            var color = IsBackgroundTransparent
                ? Colors.Transparent
                : (Color) ColorConverter.ConvertFromString(HexColourValue);

            return new CartesianChart
            {
                DataContext = ChartModel,
                Series = ChartModel.PlayerSeriesCollection,
                LegendLocation = LegendLocation.Top,
                DisableAnimations = true,
                Width = RenderWidth,
                Height = RenderHeight,
                Background = Brushes.White // new SolidColorBrush(color)
            };
        }

        private void SaveImage()
        {
            throw new NotImplementedException();
        }

        private void RenderImage()
        {
            // Creates a new chart
            var newChart = GenerateChart();

            var viewBox = new Viewbox
            {
                Child = newChart
            };
            viewBox.Measure(newChart.RenderSize);
            viewBox.Arrange(new Rect(new Point(0, 0), newChart.RenderSize));

            newChart.Update(true, true);
            viewBox.UpdateLayout();

            var bitmap = new RenderTargetBitmap((int) newChart.ActualWidth, (int) newChart.ActualHeight, 96, 96,
                PixelFormats.Pbgra32);
            bitmap.Render(newChart);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            var image = new BitmapImage();
            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);

                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }

            ImageRender.Source = image;
            OnPropertyChanged(nameof(ImageRender));
        }

        private void ChangeOutput()
        {
            throw new NotImplementedException();
        }
    }
}