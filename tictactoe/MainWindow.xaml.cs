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

        public MainWindow()
        {
            InitializeComponent();

            _buttonCollection = GridPlayingField.Children.OfType<Button>();

            _tictactoe.NewGame(_buttonCollection);
        }

        private void PlaceMarker(object sender, RoutedEventArgs e)
        {
            _tictactoe.PlaceMarker(sender as Button);

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

            else
            {
                _tictactoe.NextTurn();
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

        }



        //private bool CheckWinner()
        //{
        //    //Check rows
        //    if (button_A1.Content.ToString() == button_A2.Content.ToString() && button_A2.Content.ToString() == button_A3.Content.ToString() && !button_A1.IsEnabled)
        //    {
        //        return true;
        //    }

        //    if (button_B1.Content.ToString() == button_B2.Content.ToString() && button_B2.Content.ToString() == button_B3.Content.ToString() && !button_B1.IsEnabled)
        //    {
        //        return true;
        //    }

        //    if (button_C1.Content.ToString() == button_C2.Content.ToString() && button_C2.Content.ToString() == button_C3.Content.ToString() && !button_C1.IsEnabled)
        //    {
        //        return true;
        //    }

        //    //Check columns
        //    if (button_A1.Content.ToString() == button_B1.Content.ToString() && button_B1.Content.ToString() == button_C1.Content.ToString() && !button_A1.IsEnabled)
        //    {
        //        return true;
        //    }

        //    if (button_A2.Content.ToString() == button_B2.Content.ToString() && button_B2.Content.ToString() == button_C2.Content.ToString() && !button_A2.IsEnabled)
        //    {
        //        return true;
        //    }

        //    if (button_A3.Content.ToString() == button_B3.Content.ToString() && button_B3.Content.ToString() == button_C3.Content.ToString() && !button_A3.IsEnabled)
        //    {
        //        return true;
        //    }

        //    //Check diagonals
        //    if (button_A1.Content.ToString() == button_B2.Content.ToString() && button_B2.Content.ToString() == button_C3.Content.ToString() && !button_A1.IsEnabled)
        //    {
        //        return true;
        //    }

        //    if (button_A3.Content.ToString() == button_B2.Content.ToString() && button_B2.Content.ToString() == button_C1.Content.ToString() && !button_A3.IsEnabled)
        //    {
        //        return true;
        //    }

        //    return false;


        //}

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
            //TODO AI WINDOW
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

