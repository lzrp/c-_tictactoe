using System;
using System.Collections.Generic;
using System.Linq;
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
using tictactoe.Events;

namespace tictactoe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Tictactoe _tictactoe = new Tictactoe();
        private readonly IEnumerable<Button> _buttonCollection;
        private static readonly ManualResetEvent _mre = new ManualResetEvent(false);
        public event ComputersMoveEventHandler OnAiMove;

        public MainWindow()
        {
            InitializeComponent();

            //Get the button collection from the UI and initialize a newgame
            _buttonCollection = GridPlayingField.Children.OfType<Button>();

            _tictactoe.NewGame(_buttonCollection);

            if (!_tictactoe.PlayerStartsFirst)
            {
                OnAiMove?.Invoke(this, new ComputersMoveEventArgs(_tictactoe.GetCurrentTurnPlayer()));
            }
            
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            _tictactoe.PlaceMarker(sender as Button);

            //Check for a winner
            if (_tictactoe.CheckWinner(_tictactoe.Board))
            {
                _tictactoe.StopGame();
                DisableButtons();
                _tictactoe.AnnounceWinner(_tictactoe.GetCurrentTurnPlayer());
                
                if (_tictactoe.AskForNewGame())
                {
                    _tictactoe.NewGame(_buttonCollection);
                }

            }

            //Else advance to the next turn
            else
            {
                _tictactoe.NextTurn();

                //If there are no empty fields left, end the game
                if (_tictactoe.BoardFieldsLeftCounter == 0)
                {
                    _tictactoe.StopGame();
                    DisableButtons();
                    _tictactoe.AnnounceDraw();
                    
                    if (_tictactoe.AskForNewGame())
                    {
                        _tictactoe.NewGame(_buttonCollection);
                    }
                }
            }
            UpdateUi();

            OnAiMove?.Invoke(this, new ComputersMoveEventArgs(_tictactoe.GetCurrentTurnPlayer()));
        }

        private void RestartGame(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }

        private void RestartGame()
        {
            _tictactoe.NewGame(_buttonCollection);
            UpdateUi();
        }

        private void UpdateUi()
        {
            LabelStatus.Content = "Fields left: " + _tictactoe.BoardFieldsLeftCounter;
            //TODO Ai WINDOW
        }

        private void DisableButtons()
        {
            foreach (var button in _buttonCollection)
            {
                button.IsEnabled = false;
            }
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

