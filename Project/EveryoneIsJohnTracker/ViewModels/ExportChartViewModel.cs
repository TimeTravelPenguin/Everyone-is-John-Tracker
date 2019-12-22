#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: ExportChartViewModel.cs
// 
// Current Data:
// 2019-12-22 12:05 PM
// 
// Creation Date:
// 2019-12-22 11:05 AM

#endregion

using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EveryoneIsJohnTracker.Extensions;
using EveryoneIsJohnTracker.Models;
using EveryoneIsJohnTracker.Types;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using Microsoft.Xaml.Behaviors.Core;
using MessageBox = System.Windows.MessageBox;

namespace EveryoneIsJohnTracker.ViewModels
{
    internal class ExportChartViewModel : PropertyChangedBase
    {
        private BitmapImage _bitmapImage = new BitmapImage();
        private ChartModel _chartModel = new ChartModel();
        private string _hexColourValue = "#FFFFFF";
        private double _imageZoom = 1;

        private string _outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
            "Export.png");

        private int _renderHeight = 720;
        private int _renderWidth = 1080;
        private bool _saveEnabled;
        private double _windowHeight = 419;
        private double _windowMinHeight = 419;
        private double _windowMinWidth = 448;
        private double _windowWidth = 448;

        public bool SaveEnabled
        {
            get => _saveEnabled;
            set => SetValue(ref _saveEnabled, value);
        }

        public double WindowHeight
        {
            get => _windowHeight;
            set => SetValue(ref _windowHeight, value);
        }

        public double WindowWidth
        {
            get => _windowWidth;
            set => SetValue(ref _windowWidth, value);
        }

        public double ImageZoom
        {
            get => _imageZoom;
            private set => SetValue(ref _imageZoom, value);
        }

        public BitmapImage BitmapImage
        {
            get => _bitmapImage;
            private set
            {
                ImageZoom = 1;
                SetValue(ref _bitmapImage, value);
            }
        }

        private ChartModel ChartModel
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

        public string OutputPath
        {
            get => _outputPath;
            set => SetValue(ref _outputPath, value);
        }

        public ActionCommand CommandChangeOutputPath { get; }
        public ActionCommand CommandRenderImage { get; }
        public ActionCommand CommandSaveImage { get; }
        public ActionCommand CommandMouseWheel { get; }

        public double WindowMinWidth
        {
            get => _windowMinWidth;
            set => SetValue(ref _windowMinWidth, value);
        }

        public double WindowMinHeight
        {
            get => _windowMinHeight;
            set => SetValue(ref _windowMinHeight, value);
        }

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
                var zoom = e.Delta > 0
                    ? Math.Min(_imageZoom + 0.1, 4)
                    : Math.Max(_imageZoom - 0.1, 0.01);

                ImageZoom = zoom.LimitToRange(0.1, 4);

                if (_imageZoom.IsInfinity())
                {
                    ImageZoom = 1;
                    MessageBox.Show(
                        "An error occured and the zoom somehow became infinity, so it has been reset to 1x zoom.",
                        "Error");
                }

                Debug.WriteLine(ImageZoom);

                e.Handled = true;
            }
            else
            {
                throw new ArgumentException(@"The passed parameter is not of type MouseWheelEventArgs", nameof(obj));
            }
        }

        private Chart GenerateChart()
        {
            if (string.IsNullOrEmpty(HexColourValue))
            {
                throw new NullReferenceException(nameof(HexColourValue));
            }

            var color = IsBackgroundTransparent
                ? new SolidColorBrush(Colors.Transparent)
                : new SolidColorBrush((Color) ColorConverter.ConvertFromString(HexColourValue));

            var chart = new CartesianChart
            {
                DataContext = ChartModel,
                Series = ChartModel.PlayerSeriesCollection,
                LegendLocation = LegendLocation.Top,
                DisableAnimations = true,
                Width = RenderWidth,
                Height = RenderHeight,
                Background = color,
                ChartLegend =
                {
                    Background = color
                }
            };

            chart.AxisX.Clear();
            chart.AxisY.Clear();
            chart.AxisX.Add(new Axis {MinValue = 0});
            chart.AxisY.Add(new Axis {MinValue = 0});

            return chart;
        }


        // TODO: Make process run on own thread
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

            BitmapImage = image;
            SaveEnabled = true;
        }

        private void ChangeOutput()
        {
            if (BitmapImage is null)
            {
                throw new NullReferenceException(nameof(BitmapImage));
            }

            using var sfd = new SaveFileDialog
            {
                Filter = @"png files (*.png)|*.png",
                RestoreDirectory = true
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                OutputPath = Path.GetFullPath(sfd.FileName ?? throw new InvalidOperationException());
            }
        }

        private void SaveImage()
        {
            if (BitmapImage is null)
            {
                MessageBox.Show("You must render an image before saving", "No image rendered");
                return;
            }

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(BitmapImage));

            using var stream = new FileStream(OutputPath, FileMode.Create);
            encoder.Save(stream);

            MessageBox.Show("File saved", "Save successful");
        }
    }
}