using System.Windows;
using tictactoe.Windows;

namespace tictactoe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonStartNewGame_Click(object sender, RoutedEventArgs e)
        {
            // Hide startscreen
            Hide();
            IsEnabled = false;

            // Show main game window
            var ticTacToeWindow = new TicTacToeWindow();
            ticTacToeWindow.ShowDialog();

            // Show starscreen
            Show();
            IsEnabled = true;
            Focus();
        }

        private void ButtonExitGame_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonGameSettings_Click(object sender, RoutedEventArgs e)
        {
            // Hide startscreen
            Hide();
            IsEnabled = false;

            // Show main game window
            var settingsWindow = new GameSettingsWindow();
            settingsWindow.ShowDialog();

            // Show starscreen
            Show();
            IsEnabled = true;
            Focus();
        }
    }
}