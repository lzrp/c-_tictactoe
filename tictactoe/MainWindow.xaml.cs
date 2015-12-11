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
            var ticTacToeWindow = new TicTacToeWindow();
            
            ShowNewWindow(ticTacToeWindow);
        }

        private void ButtonExitGame_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonGameSettings_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new GameSettingsWindow();
            
            ShowNewWindow(settingsWindow);
        }

        private void ShowNewWindow(Window window)
        {
            // Hide current window and disable it
            Hide();
            IsEnabled = false;

            // Show new window
            window.ShowDialog();

            // Show the default window after exit
            Show();
            IsEnabled = true;
            Focus();
        }
    }
}