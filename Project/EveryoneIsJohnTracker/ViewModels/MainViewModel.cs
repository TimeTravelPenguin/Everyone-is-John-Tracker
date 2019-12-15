#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: MainViewModel.cs
// 
// Current Data:
// 2019-12-16 2:49 AM
// 
// Creation Date:
// 2019-12-14 3:31 PM

#endregion

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using EveryoneIsJohnTracker.Base;
using EveryoneIsJohnTracker.Extensions;
using EveryoneIsJohnTracker.Models;
using EveryoneIsJohnTracker.Models.Logger;
using Microsoft.Expression.Interactivity.Core;
using Newtonsoft.Json;

namespace EveryoneIsJohnTracker.ViewModels
{
    internal class MainViewModel : PropertyChangedBase
    {
        private int _comboboxLevelBinding;
        private string _editableItemName;
        private SkillModel _editableSkillModel = new SkillModel(); // Used to bind to view
        private VoiceModel _editableVoiceModel = new VoiceModel(NullLogger); // Used to add to Voices Collection
        private int _listViewSelectedIndex;
        private SkillModel _selectedSkillModel = new SkillModel(); // Used to bind to view
        private VoiceModel _selectedVoiceModel;
        private ICollectionView _voiceCollectionView;

        [JsonIgnore]
        public VoiceModel SelectedVoiceModel
        {
            get => _selectedVoiceModel;
            set
            {
                SetValue(ref _selectedVoiceModel, value);
                GameMaster.UpdateChart();
            }
        }

        public ICollectionView VoiceCollectionView
        {
            get => _voiceCollectionView;
            set => SetValue(ref _voiceCollectionView, value);
        }

        public VoiceModel EditableVoiceModel
        {
            get => _editableVoiceModel;
            set => SetValue(ref _editableVoiceModel, value);
        }

        public SkillModel EditableSkillModel
        {
            get => _editableSkillModel;
            set => SetValue(ref _editableSkillModel, value);
        }

        public SkillModel SelectedSkillModel
        {
            get => _selectedSkillModel;
            set => SetValue(ref _selectedSkillModel, value);
        }

        public string EditableItemName
        {
            get => _editableItemName;
            set => SetValue(ref _editableItemName, value);
        }

        public ActionCommand CommandOpenRules { get; }
        public ActionCommand CommandAddSelectableSkill { get; }
        public ActionCommand CommandAddEditableSkill { get; }
        public ActionCommand CommandAddVoice { get; }
        public ActionCommand CommandAddWillpower { get; }
        public ActionCommand CommandSubtractWillpower { get; }
        public ActionCommand CommandAddWillpowerAll { get; }
        public ActionCommand CommandSubtractWillpowerAll { get; }
        public ActionCommand CommandRemoveVoice { get; }
        public ActionCommand CommandAddObsessionPoint { get; }
        public ActionCommand CommandRemoveObsessionPoint { get; }
        public ActionCommand CommandAddItem { get; }
        public ActionCommand CommandLoadGame { get; }
        public ActionCommand CommandSaveGame { get; }
        public ActionCommand CommandNextTurn { get; }

        public int ComboboxLevelBinding
        {
            get => _comboboxLevelBinding;
            set => SetValue(ref _comboboxLevelBinding, value);
        }

        public int ListViewSelectedIndex
        {
            get => _listViewSelectedIndex;
            set
            {
                SetValue(ref _listViewSelectedIndex, value);
                OnPropertyChanged(nameof(SelectedVoiceModel));
            }
        }

        // GameMaster holds all the collections stored for the game
        public static GameMasterModel GameMaster { get; set; }

        public static ILogger OutputLogger { get; } = LogFactory.NewLogger(LoggerType.OutputLogger);

        private static ILogger NullLogger { get; } = LogFactory.NewLogger(LoggerType.NullLogger);


        public MainViewModel()
        {
            GameMaster = new GameMasterModel(OutputLogger);
            VoiceCollectionView = CollectionViewSource.GetDefaultView(GameMaster.Voices);

            CommandOpenRules =
                new ActionCommand(() => Process.Start(@"https://rulebook.io/games/everyone-is-john/rules"));

            CommandAddEditableSkill = new ActionCommand(obj => AddSkill(obj, EditableVoiceModel));

            CommandAddSelectableSkill = new ActionCommand(obj => AddSkill(obj, SelectedVoiceModel));

            CommandAddVoice = new ActionCommand(AddVoice);

            CommandAddWillpower = new ActionCommand(() => GameMaster.AddWillpower(ListViewSelectedIndex, 1));

            CommandSubtractWillpower = new ActionCommand(() => GameMaster.AddWillpower(ListViewSelectedIndex, -1));

            CommandAddWillpowerAll = new ActionCommand(() => GameMaster.AddWillpowerAll(1));

            CommandSubtractWillpowerAll = new ActionCommand(() => GameMaster.AddWillpowerAll(-1));

            CommandRemoveVoice = new ActionCommand(RemoveVoice);

            CommandAddObsessionPoint = new ActionCommand(() => GameMaster.AddObsessionPoint(ListViewSelectedIndex, 1));

            CommandRemoveObsessionPoint =
                new ActionCommand(() => GameMaster.AddObsessionPoint(ListViewSelectedIndex, -1));

            CommandAddItem = new ActionCommand(AddItem);

            CommandLoadGame = new ActionCommand(() =>
            {
                var success = GameMaster.FileInput(OutputLogger);
                if (success)
                {
                    SelectedVoiceModel = GameMaster.Voices.FirstOrDefault();
                }
            });

            CommandSaveGame = new ActionCommand(() => GameMaster.FileOutput(OutputLogger));

            CommandNextTurn = new ActionCommand(() => GameMaster.IncrementTurn(1));

            SelectedVoiceModel = GameMaster.Voices.FirstOrDefault();
        }


        private void AddItem()
        {
            if (GameMaster.Inventory.Any(item => EditableItemName == item.Name))
            {
                MessageBox.Show("This item already exists");
                return;
            }

            if (!string.IsNullOrEmpty(EditableItemName))
            {
                // Instantiate with NullLogger to prevent logging, and then pass set variable values
                GameMaster.AddItem(new ItemModel(NullLogger)
                {
                    Name = EditableItemName,
                    Count = 1,
                    Description = ""
                }, OutputLogger);

                EditableItemName = "";
            }
        }

        private void AddSkill(object obj, VoiceModel voice)
        {
            if (voice == null)
            {
                return;
            }

            if (!(obj is SkillModel skill))
            {
                throw new InvalidOperationException(nameof(obj));
            }

            if (!string.IsNullOrEmpty(skill.Name))
            {
                voice.Skills.Add(new SkillModel(voice)
                {
                    Name = skill.Name
                });

                skill.Name = "";
                voice.UpdateSkillsAsString();
            }
        }

        private void AddVoice()
        {
            if (GameMaster.Voices.Any(voice => EditableVoiceModel.Name == voice.Name))
            {
                MessageBox.Show("This voice already exists");
                return;
            }

            if (!string.IsNullOrEmpty(EditableVoiceModel.Name))
            {
                EditableVoiceModel.Obsession.Level = ComboboxLevelBinding + 1;

                GameMaster.AddVoice(EditableVoiceModel, OutputLogger);

                EditableVoiceModel.Clear();
                EditableSkillModel.Name = "";
                ComboboxLevelBinding = 0;
            }

            SelectedVoiceModel = GameMaster.Voices.FirstOrDefault();
        }

        private void RemoveVoice()
        {
            //GameMaster.RemoveVoiceAt(SelectedVoiceIndex);

            GameMaster.Voices.Remove(SelectedVoiceModel);
            SelectedSkillModel.Name = "";
            SelectedVoiceModel = GameMaster.Voices.FirstOrDefault();
        }
    }
}