using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
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
        private static Ai _computerAi;
        //private readonly Random _rnd = new Random();


        public MainWindow()
        {
            InitializeComponent();

            //Get the button collection from the UI and initialize a newgame
            _buttonCollection = GridPlayingField.Children.OfType<Button>();

            //Initialize and start the game
            _tictactoe = new Tictactoe(_buttonCollection);

            //Initialize the game and AI if needed
            _computerAi = new Ai(_tictactoe);

            //If AI starts first
            if (!_tictactoe.PlayerStartsFirst)
            {
                _computerAi.PerformMove(_computerAi.ComputeMoveValue(_tictactoe.Board));
                _tictactoe.NextTurn();

                UpdateUi();
            }


        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            //Players turn
            _tictactoe.PlaceMarker(sender as Button);
            UpdateUi();
            if (!_tictactoe.GameStateCheck())
            {
                return;
            }
            _tictactoe.NextTurn();
            
            //Computers turn after the end of players turn
            _computerAi.PerformMove(_computerAi.ComputeMoveValue(_tictactoe.Board));
            UpdateUi();
            if (!_tictactoe.GameStateCheck())
            {
                return;
            }
            _tictactoe.NextTurn();

            
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
            
            LabelStatus.Content = "Fields left: " + _tictactoe.BoardFieldsLeftCounter + " / Player on move: " + _tictactoe.GetCurrentTurnPlayer();
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

        private void MenuItemVsComputer_Loaded(object sender, RoutedEventArgs e)
        {
            MenuItemVsComputer.IsChecked = Properties.Settings.Default.VsComputer;
        }

        private void MenuItemVsComputer_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.VsComputer = MenuItemVsComputer.IsChecked;
            Properties.Settings.Default.Save();
        }

        
    }
}

