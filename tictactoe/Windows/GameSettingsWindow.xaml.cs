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
using System.Windows.Shapes;
using tictactoe.Classes;

namespace tictactoe.Windows
{
    /// <summary>
    /// Interaction logic for GameSettingsWindow.xaml
    /// </summary>
    public partial class GameSettingsWindow : Window
    {
        public GameSettingsWindow()
        {
            InitializeComponent();
        }

        private void ComboBoxDifficulty_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxDifficulty.ItemsSource = Enum.GetValues(typeof (Ai.AiDifficulty));
            ComboBoxDifficulty.SelectedIndex = Properties.Settings.Default.DifficultySetting;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxVsComputer.IsChecked != null)
                Properties.Settings.Default.VsComputer = (bool) CheckBoxVsComputer.IsChecked;

            if (CheckBoxPlayerStartsFirst.IsChecked != null && CheckBoxPlayerStartsFirst.IsEnabled)
                Properties.Settings.Default.PlayerStartsFirst = (bool)CheckBoxPlayerStartsFirst.IsChecked;

            if (ComboBoxDifficulty.SelectedIndex > 0 && ComboBoxDifficulty.SelectedIndex < 4)
            {
                Properties.Settings.Default.DifficultySetting = ComboBoxDifficulty.SelectedIndex;
            }

            Properties.Settings.Default.Save();
            Close();
        }
    }
}
