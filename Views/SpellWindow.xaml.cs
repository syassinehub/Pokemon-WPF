using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PokemonWpf.Model;

namespace PokemonWpf.Views
{
    public partial class SpellWindow : Window
    {
        public SpellWindow()
        {
            InitializeComponent();
            LoadSpells();
        }

        private void OnViewMonstersClick(object sender, RoutedEventArgs e)
        {
            var monsterWindow = new MonsterWindow();
            monsterWindow.Show();
            this.Close();
        }

        private void LoadSpells()
        {
            try
            {
                using var context = new ExerciceMonsterContext();
                List<Spell> spells = context.Spells.ToList();
                SpellDataGrid.ItemsSource = spells;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"An error occurred while loading spells: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
