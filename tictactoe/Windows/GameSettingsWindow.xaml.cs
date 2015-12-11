using System;
using System.Windows;
using tictactoe.Classes;

namespace tictactoe.Windows
{
    /// <summary>
    /// Interaction logic for GameSettingsWindow.xaml
    /// </summary>
    public partial class GameSettingsWindow
    {
        public GameSettingsWindow()
        {
            InitializeComponent();
        }

        private void ComboBoxDifficulty_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxDifficulty.ItemsSource = Enum.GetValues(typeof (Ai.AiDifficulty));
            ComboBoxDifficulty.SelectedIndex = Properties.Settings.Default.AiDifficultySetting;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxComputerOponentEnabled.IsChecked != null)
                Properties.Settings.Default.ComputerOponentEnabled = (bool) CheckBoxComputerOponentEnabled.IsChecked;

            if (CheckBoxPlayerStartsFirst.IsChecked != null && CheckBoxPlayerStartsFirst.IsEnabled)
                Properties.Settings.Default.PlayerStartsFirst = (bool)CheckBoxPlayerStartsFirst.IsChecked;

            if (ComboBoxDifficulty.SelectedIndex >= 0 && ComboBoxDifficulty.SelectedIndex < 4)
            {
                Properties.Settings.Default.AiDifficultySetting = ComboBoxDifficulty.SelectedIndex;
            }

            Properties.Settings.Default.Save();
            Close();
        }

        private void CheckBoxComputerOponentEnabled_Loaded(object sender, RoutedEventArgs e)
        {
            CheckBoxComputerOponentEnabled.IsChecked = Properties.Settings.Default.ComputerOponentEnabled;
        }

        private void CheckBoxPlayerStartsFirst_Loaded(object sender, RoutedEventArgs e)
        {
            CheckBoxPlayerStartsFirst.IsChecked = Properties.Settings.Default.PlayerStartsFirst;
        }
    }
}
