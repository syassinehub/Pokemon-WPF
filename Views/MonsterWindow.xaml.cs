using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore; // Nécessaire pour Include
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

        // Méthode pour ouvrir la fenêtre des sorts
        private void OnViewSpellsClick(object sender, RoutedEventArgs e)
        {
            var spellWindow = new SpellWindow();
            spellWindow.Show();
            this.Close();
        }

        // Méthode pour aller à la page de combat avec le monstre sélectionné
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
                MessageBox.Show("Veuillez sélectionner un monstre pour le combat.",
                                "Aucun monstre sélectionné",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Méthode pour charger les monstres avec leurs sorts depuis la base de données
        private void LoadMonsters()
        {
            try
            {
                using var context = new ExerciceMonsterContext();
                // Inclure les sorts (Spells) liés aux monstres
                List<Monster> monsters = context.Monsters
                                                .Include(m => m.Spells)
                                                .ToList();

                // Mettre les monstres dans le DataGrid
                MonsterDataGrid.ItemsSource = monsters;
            }
            catch (System.Exception ex)
            {
                // Gestion précise de l'exception
                MessageBox.Show($"Une erreur est survenue lors du chargement des monstres : {ex.Message}",
                                "Erreur",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
