using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using tictactoe.Classes;

namespace tictactoe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Tictactoe _tictactoe = new Tictactoe();
        private readonly IEnumerable<Button> _buttonCollection;
        private int _fieldsLeftCounter = 9;
        
        public MainWindow()
        {
            InitializeComponent();

            _buttonCollection = GridPlayingField.Children.OfType<Button>();

            _tictactoe.NewGame(_buttonCollection);
        }

        private void PlaceMarker(object sender, RoutedEventArgs e)
        {
            var currentPlayer = _tictactoe.GetCurrentTurnPlayer();

            var button = (Button) sender;

            if (button.Content.ToString() == "")
            {
                button.Content = currentPlayer;
                button.IsEnabled = false;
                _fieldsLeftCounter--;

                if (CheckWinner())
                {
                    MessageBox.Show("Congratulations! Player " + currentPlayer + " wins!", "Winner!", MessageBoxButton.OK);
                }

                else
                {
                    _tictactoe.NextTurn();
                    if (_fieldsLeftCounter != 0) return;
                    MessageBox.Show("Players draw.", "Draw!", MessageBoxButton.OK);
                }

                UpdateUi();

                if (!AskForNewGame())
                {
                    DisableButtons();
                }
            }

            else
            {
            MessageBox.Show("You can't place your marker to an already marked field!");
            }
        }

        private bool CheckWinner()
        {
            //Check rows
            if (button_A1.Content.ToString() == button_A2.Content.ToString() && button_A2.Content.ToString() == button_A3.Content.ToString() && !button_A1.IsEnabled)
            {
                return true;
            }

            if (button_B1.Content.ToString() == button_B2.Content.ToString() && button_B2.Content.ToString() == button_B3.Content.ToString() && !button_B1.IsEnabled)
            {
                return true;
            }

            if (button_C1.Content.ToString() == button_C2.Content.ToString() && button_C2.Content.ToString() == button_C3.Content.ToString() && !button_C1.IsEnabled)
            {
                return true;
            }

            //Check columns
            if (button_A1.Content.ToString() == button_B1.Content.ToString() && button_B1.Content.ToString() == button_C1.Content.ToString() && !button_A1.IsEnabled)
            {
                return true;
            }

            if (button_A2.Content.ToString() == button_B2.Content.ToString() && button_B2.Content.ToString() == button_C2.Content.ToString() && !button_A2.IsEnabled)
            {
                return true;
            }

            if (button_A3.Content.ToString() == button_B3.Content.ToString() && button_B3.Content.ToString() == button_C3.Content.ToString() && !button_A3.IsEnabled)
            {
                return true;
            }

            //Check diagonals
            if (button_A1.Content.ToString() == button_B2.Content.ToString() && button_B2.Content.ToString() == button_C3.Content.ToString() && !button_A1.IsEnabled)
            {
                return true;
            }

            if (button_A3.Content.ToString() == button_B2.Content.ToString() && button_B2.Content.ToString() == button_C1.Content.ToString() && !button_A3.IsEnabled)
            {
                return true;
            }

            return false;


        }

        private void RestartGame(object sender, RoutedEventArgs e)
        {
            //_tictactoe.NewGame(_buttonCollection);
            //_fieldsLeftCounter = 9;
            //UpdateUi();

            RestartGame();
        }

        private void RestartGame()
        {
            _tictactoe.NewGame(_buttonCollection);
            _fieldsLeftCounter = 9;
            UpdateUi();
        }

        private void UpdateUi()
        {
            LabelStatus.Content = "Fields left: " + _fieldsLeftCounter;
            //TODO AI WINDOW
        }

        private void DisableButtons()
        {
            foreach (var button in _buttonCollection)
            {
                button.IsEnabled = false;
            }
        }

        private bool AskForNewGame()
        {
            var continueBoxResult = MessageBox.Show("Do you want to start a new game?", "New game?" , MessageBoxButton.YesNo);

            if (continueBoxResult != MessageBoxResult.Yes) return false;
            RestartGame();
            return true;
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
