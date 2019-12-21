#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: FileIOExtension.cs
// 
// Current Data:
// 2019-12-21 5:56 PM
// 
// Creation Date:
// 2019-12-18 8:21 PM

#endregion

using System;
using System.IO;
using System.Windows;
using EveryoneIsJohnTracker.Models;
using EveryoneIsJohnTracker.Models.Logger;
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
    }
}