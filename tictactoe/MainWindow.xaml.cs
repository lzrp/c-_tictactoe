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
        readonly Tictactoe _tictactoe = new Tictactoe();
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
                _fieldsLeftCounter--;

                if (CheckWinner())
                {
                    var continueBoxResult = MessageBox.Show(button.Content + " wins! Do you want to start a new game?", "Winner!", MessageBoxButton.YesNo);

                    if (continueBoxResult == MessageBoxResult.Yes)
                    {
                        RestartGame(sender, e);
                    }
                    
                }

                else
                {
                    _tictactoe.NextTurn();
                }

                UpdateUi();
                
            }

            else
            {
            MessageBox.Show("You can't place your marker to an already marked field!");
            }
        }

        private bool CheckWinner()
        {
            //Check rows
            return button_A1.Content.Equals(button_A2.Content);
        }

        private void RestartGame(object sender, RoutedEventArgs e)
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
            //TODO disable all buttons after a winning game
        }
    }
}
