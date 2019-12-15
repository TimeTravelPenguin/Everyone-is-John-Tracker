#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohnTracker
// Project: EveryoneIsJohnTracker
// File Name: MainViewModel.cs
// 
// Current Data:
// 2019-12-15 11:14 PM
// 
// Creation Date:
// 2019-12-14 3:31 PM

#endregion

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using EveryoneIsJohnTracker.Base;
using EveryoneIsJohnTracker.Extensions;
using EveryoneIsJohnTracker.Models;
using EveryoneIsJohnTracker.Models.OutputLoggers;
using Microsoft.Expression.Interactivity.Core;

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
        private int _selectedVoiceIndex;
        private ICollectionView _voiceCollectionView;

        public VoiceModel SelectedVoiceModel
        {
            get => GameMaster.Voices.Count > SelectedVoiceIndex && SelectedVoiceIndex >= 0
                ? GameMaster.Voices[SelectedVoiceIndex]
                : null;
            set
            {
                GameMaster.Voices[SelectedVoiceIndex] = value;
                GameMaster.UpdateChart();
            }
        }

        public int SelectedVoiceIndex
        {
            get => _selectedVoiceIndex;
            set
            {
                SetValue(ref _selectedVoiceIndex, value);
                OnPropertyChanged(nameof(SelectedVoiceModel));
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
        public ActionCommand CommandSelectionChangedEvent { get; }

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

            CommandLoadGame = new ActionCommand(() => GameMaster.FileInput(OutputLogger));

            CommandSaveGame = new ActionCommand(() => GameMaster.FileOutput(OutputLogger));

            CommandNextTurn = new ActionCommand(() => GameMaster.IncrementTurn(1));

            CommandSelectionChangedEvent =
                new ActionCommand(VoicesComboBox_SelectionChanged);
        }

        private void VoicesComboBox_SelectionChanged(object sender)
        {
            if (!(sender is ComboBox combo))
            {
                throw new InvalidOperationException(nameof(sender));
            }


            if (SelectedVoiceIndex < 0 && GameMaster.Voices.Count > 0)
            {
                SelectedVoiceIndex = 0;
                OnPropertyChanged(nameof(SelectedVoiceModel));
            }
            else
            {
                SelectedVoiceIndex = combo.SelectedIndex;
            }
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
                throw new NullReferenceException(nameof(voice));
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

            // Check element in editing combobox is selected
            SelectedVoiceIndex = SelectedVoiceIndex < 0 && GameMaster.Voices.Count > 0
                ? 0
                : SelectedVoiceIndex;
            OnPropertyChanged(nameof(SelectedVoiceModel));
        }

        private void RemoveVoice()
        {
            GameMaster.RemoveVoiceAt(SelectedVoiceIndex);
        }
    }
}