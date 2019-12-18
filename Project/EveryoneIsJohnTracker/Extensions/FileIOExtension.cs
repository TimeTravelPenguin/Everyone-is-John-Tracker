#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: FileIOExtension.cs
// 
// Current Data:
// 2019-12-18 7:43 PM
// 
// Creation Date:
// 2019-12-14 3:31 PM

#endregion

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EveryoneIsJohnTracker.Models;
using EveryoneIsJohnTracker.Models.Logger;
using LiveCharts.Wpf;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace EveryoneIsJohnTracker.Extensions
{
    internal static class FileIOExtension
    {
        internal static void FileOutput(this GameMasterModel gameMasterModel, ILogger logger)
        {
            var serializedData = JsonConvert.SerializeObject(gameMasterModel);

            var sfd = new SaveFileDialog
            {
                Filter = "Everyone is John File (*.john)|*.john",
                DefaultExt = "john",
                AddExtension = true
            };


            if (sfd.ShowDialog() == true)
            {
                try
                {
                    var path = Path.GetFullPath(sfd.FileName);

                    File.WriteAllText(path, serializedData);
                    logger.LogDataSave(sfd.FileName, false);
                }
                catch (Exception ex)
                {
                    logger.LogDataSave(sfd.FileName, true);
                    MessageBox.Show("There was an error saving the file" + Environment.NewLine + ex.Message, "Error");
                }
            }
        }

        internal static bool FileInput(this GameMasterModel gameMasterModel, ILogger logger)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Everyone is John File (*.john)|*.john"
            };

            if (ofd.ShowDialog() == true)
            {
                try
                {
                    var filePath = Path.GetFullPath(ofd.FileName);
                    var extension = Path.GetExtension(filePath);

                    if (extension == ".john")
                    {
                        var fileData = File.ReadAllText(filePath);
                        var data = JsonConvert.DeserializeObject<GameMasterModel>(fileData);
                        gameMasterModel.LoadGameData(data, logger);


                        gameMasterModel.ChartModel.PlayerData = gameMasterModel.Voices;
                        gameMasterModel.ChartModel.UpdateChartValues();

                        return true;
                    }

                    MessageBox.Show("Please open a valid .john file", "Error opening file...");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inputting file" + Environment.NewLine + ex.Message, "Error");
                }
            }

            return false;
        }

        internal static void ExportLiveChart(this ChartModel chart)
        {
            var newChart = new CartesianChart
            {
                DataContext = chart,
                Series = chart.PlayerSeriesCollection,
                DisableAnimations = true,
                Width = 1920,
                Height = 1080,
                Background = Brushes.White
            };

            var viewBox = new Viewbox
            {
                Child = newChart
            };
            viewBox.Measure(newChart.RenderSize);
            viewBox.Arrange(new Rect(new Point(0, 0), newChart.RenderSize));

            newChart.Update(true, true);
            viewBox.UpdateLayout();

            SaveToPng(newChart, "Chart.png");
        }

        private static void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            var output = EncodeVisual(visual, fileName, encoder);

            MessageBox.Show($"Image saved to:{Environment.NewLine}{output}", "Saved!");
        }

        private static string EncodeVisual(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            var bitmap = new RenderTargetBitmap((int) visual.ActualWidth, (int) visual.ActualHeight, 96, 96,
                PixelFormats.Pbgra32);
            bitmap.Render(visual);

            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);

            var newDir = Path.Combine(Environment.CurrentDirectory, "ChartExports");
            Directory.CreateDirectory(newDir);
            var filePath = Path.Combine(newDir, fileName);
            var output = CreateUniqueFilename(filePath);

            using var stream = File.Create(output);
            encoder.Save(stream);

            return output;
        }

        // Ensures a filename is unique by generating a unique filename if the specified file already exists.
        // If the provided filename exists this method generates a new name that contains a numerical suffix (starting at 1)
        // while ensuring the new filename also does not exist. This method does not create the file so applications should
        // take extra measures to ensure the returned filename is still unique before using it (the file may have since been created
        // by another process.
        private static string CreateUniqueFilename(string baseFilename)
        {
            if (!File.Exists(baseFilename))
            {
                return baseFilename;
            }

            string destFileName;
            var counter = 1;
            var dirPart = Path.GetDirectoryName(baseFilename);
            var filePart = Path.GetFileNameWithoutExtension(baseFilename);
            var extPart = Path.HasExtension(baseFilename) ? Path.GetExtension(baseFilename) : "";

            do
            {
                destFileName = Path.Combine(dirPart ?? "", $"{filePart}.{counter++}{extPart}");
            } while (File.Exists(destFileName));

            return destFileName;
        }
    }
}