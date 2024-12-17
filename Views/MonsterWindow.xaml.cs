using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PokemonWpf.Model;

namespace PokemonWpf.Views
{
    public partial class MonsterWindow : Window
    {
        public MonsterWindow()
        {
            InitializeComponent();
            LoadMonsters();
        }

        private void OnViewSpellsClick(object sender, RoutedEventArgs e)
        {
            var spellWindow = new SpellWindow();
            spellWindow.Show();
            this.Close();
        }

        private void GoToBattlePage_Click(object sender, RoutedEventArgs e)
        {
            if (MonsterDataGrid.SelectedItem is Monster selectedMonster)
            {
                var battlePage = new BattlePage(selectedMonster);
                battlePage.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un monstre pour le combat.", "Aucun monstre sélectionné", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LoadMonsters()
        {
            try
            {
                using var context = new ExerciceMonsterContext();
                List<Monster> monsters = context.Monsters.ToList();
                MonsterDataGrid.ItemsSource = monsters;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"An error occurred while loading monsters: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
