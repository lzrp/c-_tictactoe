﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using tictactoe.Classes;
using tictactoe.Properties;

namespace tictactoe.Windows
{
    /// <summary>
    /// Interaction logic for TicTacToeWindow.xaml
    /// </summary>
    public partial class TicTacToeWindow
    {
        private readonly Tictactoe _tictactoe;

        public TicTacToeWindow()
        {
            InitializeComponent();

            List<Button> buttonCollection = new List<Button>(GridPlayingField.Children.OfType<Button>());
            _tictactoe = new Tictactoe(buttonCollection);

            // Update the user interface
            _tictactoe.UpdateUi();
            UpdateStatusLabel();

        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // Players turn
            _tictactoe.PlaceMarker(sender as Button);
            _tictactoe.UpdateUi();
            UpdateStatusLabel();

            // Check if the game state has changed
            if (_tictactoe.HasGameStateChanged())
            {
                UpdateStatusLabel();
                return;
            }

            // Computers turn
            // Skip when the computer oponent is disabled
            if (!Settings.Default.ComputerOponentEnabled) return;

            // Compute the AIs move and place the marker
            var computerMove = _tictactoe.ComputerPlayerAi.GetMove(_tictactoe.GetCurrentTurnPlayerMark(), Settings.Default.AiDifficultySetting);
            _tictactoe.PlaceMarker(computerMove.X, computerMove.Y);

            // Check for the game state and update user interface
            _tictactoe.UpdateUi();
            _tictactoe.HasGameStateChanged();
            UpdateStatusLabel();
        }

        /// <summary>
        /// Updates the status label with the current player and game state.
        /// </summary>
        public void UpdateStatusLabel()
        {
            string playerStatus = _tictactoe.IsGameInProgress ? $"Player on move: {_tictactoe.GetCurrentTurnPlayerMark()}" : "Game is over.";

            LabelStatus.Content = $"Fields left:{_tictactoe.BoardFieldsLeftCounter} / {playerStatus}";
        }

        private void MenuItemAiEasy_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.AiDifficultySetting = 0;
            Settings.Default.Save();
        }

        private void MenuItemAiMedium_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.AiDifficultySetting = 1;
            Settings.Default.Save();
        }

        private void MenuItemAiImpossible_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.AiDifficultySetting = 2;
            Settings.Default.Save();
        }

        private void MenuItemAiEasy_Loaded(object sender, RoutedEventArgs e)
        {
            DisableMenuItemAfterFirstTurn(sender as MenuItem);

            if (Settings.Default.AiDifficultySetting != 0) return;

            MenuItemAiEasy.IsChecked = true;
            MenuItemAiMedium.IsChecked = false;
            MenuItemAiImpossible.IsChecked = false;
        }

        private void MenuItemAiMedium_Loaded(object sender, RoutedEventArgs e)
        {
            DisableMenuItemAfterFirstTurn(sender as MenuItem);

            if (Settings.Default.AiDifficultySetting != 1) return;

            MenuItemAiEasy.IsChecked = false;
            MenuItemAiMedium.IsChecked = true;
            MenuItemAiImpossible.IsChecked = false;
        }

        private void MenuItemAiImpossible_Loaded(object sender, RoutedEventArgs e)
        {
            DisableMenuItemAfterFirstTurn(sender as MenuItem);

            if (Settings.Default.AiDifficultySetting != 2) return;

            MenuItemAiEasy.IsChecked = false;
            MenuItemAiMedium.IsChecked = false;
            MenuItemAiImpossible.IsChecked = true;
        }

        /// <summary>
        /// Disables a menuitem after certain number of turns have passed.
        /// </summary>
        /// <param name="menuItem">Menuitem object which to disable.</param>
        private void DisableMenuItemAfterFirstTurn(MenuItem menuItem)
        {
            if (menuItem == null)
            {
                throw new NullReferenceException(nameof(menuItem));
            }

            if (_tictactoe.BoardFieldsLeftCounter < 8 && _tictactoe.IsGameInProgress)
            {
                menuItem.IsEnabled = false;
                return;
            }
            menuItem.IsEnabled = true;
        }

        /// <summary>
        /// Displays a messagebox asking to confirm or cancel the change of a setting.
        /// </summary>
        /// <returns>A bool value when the user confirmed the action.</returns>
        private static bool UserConfirmedSettingsChange()
        {
            var messageBoxResult = MessageBox.Show(Properties.Resources.TicTacToeWindow_UserConfirmedSettingsChangeString, "Restart game", MessageBoxButton.YesNo);

            return messageBoxResult == MessageBoxResult.Yes;
        }

        private void MenuItemVsComputer_Click(object sender, RoutedEventArgs e)
        {
            // Save the selected option if user clicked Yes
            if (UserConfirmedSettingsChange())
            {
                Settings.Default.ComputerOponentEnabled = MenuItemVsComputer.IsChecked;
                Settings.Default.Save();

                // Restart the game
                RestartGameAndUpdate();
                return;
            }

            // Else undo the click
            MenuItemVsComputer.IsChecked = !MenuItemVsComputer.IsChecked;
        }

        private void MenuItemPlayerStartsFirst_Click(object sender, RoutedEventArgs e)
        {
            // Save the selected option if user clicked Yes
            if (UserConfirmedSettingsChange())
            {
                Settings.Default.PlayerStartsFirst = MenuItemPlayerStartsFirst.IsChecked;
                Settings.Default.Save();

                // Restart the game
                RestartGameAndUpdate();
                return;
            }

            // Else undo the click
            MenuItemPlayerStartsFirst.IsChecked = !MenuItemPlayerStartsFirst.IsChecked;
        }

        private void NewGameMenuItemClick(object sender, RoutedEventArgs e)
        {
            RestartGameAndUpdate();
        }

        private void RestartGameAndUpdate()
        {
            _tictactoe.RestartGame();
            UpdateStatusLabel();
        }

        private void MenuItemVsComputer_Loaded(object sender, RoutedEventArgs e)
        {
            MenuItemVsComputer.IsChecked = Settings.Default.ComputerOponentEnabled;
        }

        private void MenuItemPlayerStartsFirst_Loaded(object sender, RoutedEventArgs e)
        {
            MenuItemPlayerStartsFirst.IsChecked = Settings.Default.PlayerStartsFirst;
            MenuItemPlayerStartsFirst.IsEnabled = MenuItemVsComputer.IsChecked;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

