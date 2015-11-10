using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using tictactoe.Classes;

namespace tictactoe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IEnumerable<Button> _buttonCollection;
        private readonly Tictactoe _tictactoe;
        //private readonly Random _rnd = new Random();
           
        public MainWindow()
        {
            InitializeComponent();
            //ComponentDispatcher.ThreadIdle += AiTest;
            //Get the button collection from the UI and initialize a newgame
            _buttonCollection = GridPlayingField.Children.OfType<Button>();

            //Initialize and start the game
            _tictactoe = new Tictactoe(_buttonCollection);
            UpdateUi();
            
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            //Players turn
            _tictactoe.PlaceMarker(sender as Button);
            UpdateUi();
            if (!_tictactoe.GameStateCheck())
            {
                UpdateStatusLabel();
                return;
            }

            //Computers turn
            if (!Properties.Settings.Default.VsComputer) return;
            _tictactoe.PlaceMarker(_tictactoe.ComputerPlayerAi.ComputeMoveValue().X, _tictactoe.ComputerPlayerAi.ComputeMoveValue().Y);
            UpdateUi();
            _tictactoe.GameStateCheck();
            UpdateStatusLabel();
        }

        private void RestartGame(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }

        private void RestartGame()
        {
            _tictactoe.NewGame();
            UpdateUi();
        }

        private void UpdateUi()
        {
            //Redraw the board buttons
            foreach (var button in _buttonCollection)
            {
                var x = _tictactoe.GetButtonHorizontalCoordinate(button);
                var y = _tictactoe.GetButtonVerticalCoordinate(button);

                button.Content = _tictactoe.Board[x, y];

                //Disable the button if its marked by a player
                if (button.Content.ToString() != " ")
                {
                    button.IsEnabled = false;
                }
            }

            UpdateStatusLabel();
        }

        private void UpdateStatusLabel()
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
            //TODO Ai WINDOW
        }


        //Menuitem event handlers

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
            var messageBoxResult = MessageBox.Show(MenuItemVsComputer.IsChecked ? "This will start a new game with a computer oponent. Are you sure you want to start a new game?" : "This will start a new game with a human oponent. Are you sure you want to start a new game?", "Restart game", MessageBoxButton.YesNo);

            if (messageBoxResult != MessageBoxResult.Yes) return;
            Properties.Settings.Default.VsComputer = MenuItemVsComputer.IsChecked;
            Properties.Settings.Default.Save();

            RestartGame();
        }

        private void MenuItemPlayerStartsFirst_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.PlayerStartsFirst = MenuItemPlayerStartsFirst.IsChecked;
            Properties.Settings.Default.Save();
        }
    }
}

