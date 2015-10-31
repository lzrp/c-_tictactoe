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

        public MainWindow()
        {
            InitializeComponent();

            var buttonCollection = GridPlayingField.Children.OfType<Button>();

            _tictactoe.NewGame(buttonCollection);
        }

        private void PlaceMarker(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;

            if (button.Content.ToString() == " ")
            {
                button.Content = _tictactoe.GetCurrentTurnPlayer();
                _tictactoe.NextTurn();
            }

            else
            {
            MessageBox.Show("You can't place your marker to an already marked field!");
            }
        }
    }
}
