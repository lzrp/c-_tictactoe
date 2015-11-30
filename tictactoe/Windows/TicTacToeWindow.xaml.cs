using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using tictactoe.Classes;

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

            var buttonCollection = GridPlayingField.Children.OfType<Button>();
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

            // Check if the game state has changed
            if (_tictactoe.HasGameStateChanged())
            {
                UpdateStatusLabel();
                return;
            }

            UpdateStatusLabel();

            // Computers turn
            // Skip when the computer oponent is disabled
            if (!Properties.Settings.Default.VsComputer) return;

            // Compute the AIs move and place the marker
            var computerMove = _tictactoe.ComputerPlayerAi.GetMove(_tictactoe.GetCurrentTurnPlayerMark(), Properties.Settings.Default.DifficultySetting);
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
            Properties.Settings.Default.DifficultySetting = 2;
            Properties.Settings.Default.Save();
        }

        private void MenuItemAiEasy_Loaded(object sender, RoutedEventArgs e)
        {
            DisableMenuItemAfterFirstTurn(sender as MenuItem);

            if (Properties.Settings.Default.DifficultySetting != 0) return;

            MenuItemAiEasy.IsChecked = true;
            MenuItemAiMedium.IsChecked = false;
            MenuItemAiImpossible.IsChecked = false;
        }

        private void MenuItemAiMedium_Loaded(object sender, RoutedEventArgs e)
        {
            DisableMenuItemAfterFirstTurn(sender as MenuItem);

            if (Properties.Settings.Default.DifficultySetting != 1) return;

            MenuItemAiEasy.IsChecked = false;
            MenuItemAiMedium.IsChecked = true;
            MenuItemAiImpossible.IsChecked = false;
        }

        /// <summary>
        /// Disables a menuitem after certain number of turns have passed.
        /// </summary>
        /// <param name="menuItem">Menuitem object which to disable.</param>
        private void DisableMenuItemAfterFirstTurn(MenuItem menuItem)
        {
            if (_tictactoe.BoardFieldsLeftCounter < 8)
            {
                menuItem.IsEnabled = false;
                return;
            }
            menuItem.IsEnabled = true;
        }

        private void MenuItemAiImpossible_Loaded(object sender, RoutedEventArgs e)
        {
            DisableMenuItemAfterFirstTurn(sender as MenuItem);

            if (Properties.Settings.Default.DifficultySetting != 2) return;

            MenuItemAiEasy.IsChecked = false;
            MenuItemAiMedium.IsChecked = false;
            MenuItemAiImpossible.IsChecked = true;
        }

        /// <summary>
        /// Displays a messagebox asking to confirm or cancel the change of a setting.
        /// </summary>
        /// <returns>A bool value when the user confirmed the action.</returns>
        private static bool UserConfirmedSettingsChange()
        {
            var messageBoxResult = MessageBox.Show("Changing this setting requires a game restart. Are you sure you want to restart the game?", "Restart game", MessageBoxButton.YesNo);

            return messageBoxResult == MessageBoxResult.Yes;
        }

        private void MenuItemVsComputer_Click(object sender, RoutedEventArgs e)
        {
            // Save the selected option if user clicked Yes
            if (UserConfirmedSettingsChange())
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
            // Save the selected option if user clicked Yes
            if (UserConfirmedSettingsChange())
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
            MenuItemVsComputer.IsChecked = Properties.Settings.Default.VsComputer;
        }

        private void MenuItemPlayerStartsFirst_Loaded(object sender, RoutedEventArgs e)
        {
            MenuItemPlayerStartsFirst.IsChecked = Properties.Settings.Default.PlayerStartsFirst;
            MenuItemPlayerStartsFirst.IsEnabled = MenuItemVsComputer.IsChecked;
        }
    }
}

