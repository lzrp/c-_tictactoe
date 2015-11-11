﻿using System.Linq;
using System.Windows;
using System.Windows.Controls;
using tictactoe.Classes;
using static tictactoe.Classes.Ai;

namespace tictactoe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Tictactoe _tictactoe;
           
        public MainWindow()
        {
            // Initialize the application components
            InitializeComponent();

            // Get the button collection from the UI and initialize a newgame
            var buttonCollection = GridPlayingField.Children.OfType<Button>();

            // Initialize and start the game
            _tictactoe = new Tictactoe(buttonCollection);

            // Update the user interface
            _tictactoe.UpdateUi();
            UpdateStatusLabel();

        }

        // Button click event, gets fired on each board button click
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // Players turn
            // Place the marker and update the user interface
            _tictactoe.PlaceMarker(sender as Button);
            _tictactoe.UpdateUi();

            // Check for the game state
            if (_tictactoe.GameStateCheckChanged())
            {
                UpdateStatusLabel();
                return;
            }

            UpdateStatusLabel();

            // Computers turn
            // Skip when the computer oponent is disabled
            if (!Properties.Settings.Default.VsComputer) return;

            // Compute the AIs move and place the marker
            Move computerMove = _tictactoe.ComputerPlayerAi.PerformMove(_tictactoe.GetCurrentTurnPlayer());
            _tictactoe.PlaceMarker(computerMove.X, computerMove.Y);

            // Check for the game state and update user interface
            _tictactoe.UpdateUi();
            _tictactoe.GameStateCheckChanged();
            UpdateStatusLabel();
        }

        /// <summary>
        /// Updates the status label.
        /// </summary>
        public void UpdateStatusLabel()
        {
            string playerStatus;

            if (_tictactoe.GameInProgress)
            {
                playerStatus = "Player on move: " + _tictactoe.GetCurrentTurnPlayer();
            }

            else
            {
                playerStatus = "Game is over.";
            }

            LabelStatus.Content = "Fields left: " + _tictactoe.BoardFieldsLeftCounter + " / " + playerStatus;
        }

        #region MenuItem event handlers
        private void MenuItemAiEasy_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DifficultySetting = 0;
            Properties.Settings.Default.Save();
        }

        private void MenuItemAiMedium_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DifficultySetting = 1;
            Properties.Settings.Default.Save();
        }

        private void MenuItemAiImpossible_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "You have selected the impossible difficulty. All games played on this difficulty will result in a draw or a loss against the computer player.",
                "Impossible difficulty", MessageBoxButton.OK);
            Properties.Settings.Default.DifficultySetting = 2;
            Properties.Settings.Default.Save();
        }

        private void MenuItemAiEasy_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.DifficultySetting != 0) return;
            MenuItemAiEasy.IsChecked = true;
            MenuItemAiMedium.IsChecked = false;
            MenuItemAiImpossible.IsChecked = false;
        }

        private void MenuItemAiMedium_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.DifficultySetting != 1) return;
            MenuItemAiEasy.IsChecked = false;
            MenuItemAiMedium.IsChecked = true;
            MenuItemAiImpossible.IsChecked = false;
        }

        private void MenuItemAiImpossible_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.DifficultySetting != 2) return;
            MenuItemAiEasy.IsChecked = false;
            MenuItemAiMedium.IsChecked = false;
            MenuItemAiImpossible.IsChecked = true;
        }

        private void MenuItemVsComputer_Click(object sender, RoutedEventArgs e)
        {
            // Ask for confirmation
            var messageBoxResult = MessageBox.Show(MenuItemVsComputer.IsChecked ? "This will start a new game with a computer oponent. Are you sure you want to start a new game?" : "This will start a new game with a human oponent. Are you sure you want to start a new game?", "Restart game", MessageBoxButton.YesNo);

            // Save the selected option if user clicked Yes
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Properties.Settings.Default.VsComputer = MenuItemVsComputer.IsChecked;
                Properties.Settings.Default.Save();

                // Restart the game
                _tictactoe.RestartGame();
                UpdateStatusLabel();
                return;
            }

            // Else undo the click
            MenuItemVsComputer.IsChecked = !MenuItemVsComputer.IsChecked;
        }

        private void MenuItemPlayerStartsFirst_Click(object sender, RoutedEventArgs e)
        {
            // Ask for confirmation
            var messageBoxResult = MessageBox.Show("Changing this setting requires a game restart. Are you sure you want to restart the game?" , "Restart game", MessageBoxButton.YesNo);

            // Save the selected option if user clicked Yes
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Properties.Settings.Default.PlayerStartsFirst = MenuItemPlayerStartsFirst.IsChecked;
                Properties.Settings.Default.Save();

                // Restart the game
                _tictactoe.RestartGame();
                UpdateStatusLabel();
                return;
            }

            // Else undo the click
            MenuItemPlayerStartsFirst.IsChecked = !MenuItemPlayerStartsFirst.IsChecked;
        }

        private void NewGameMenuItemClick(object sender, RoutedEventArgs e)
        {
            _tictactoe.RestartGame();
            UpdateStatusLabel();
        }

        private void MenuItemVsComputer_Loaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.VsComputer = MenuItemVsComputer.IsChecked;
            Properties.Settings.Default.Save();
        }

        private void MenuItemPlayerStartsFirst_Loaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.PlayerStartsFirst = MenuItemPlayerStartsFirst.IsChecked;
            Properties.Settings.Default.Save();
        }
        #endregion


    }
}

