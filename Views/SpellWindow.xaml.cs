using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using PokemonWpf.Model;

namespace PokemonWpf.Views
{
    public partial class SpellWindow : Window
    {
        private List<Spell> allSpells; 
        private List<Monster> allMonsters; 

        public SpellWindow()
        {
            InitializeComponent();
            LoadMonsters();
            LoadSpells();
        }

        private void LoadMonsters()
        {
            try
            {
                using var context = new ExerciceMonsterContext();

                allMonsters = context.Monsters.ToList();

                var allSpellsOption = new Monster { Id = 0, Name = "Tous les sorts" };
                allMonsters.Insert(0, allSpellsOption);

                MonsterComboBox.ItemsSource = allMonsters;
                MonsterComboBox.DisplayMemberPath = "Name";
                MonsterComboBox.SelectedValuePath = "Id";

                MonsterComboBox.SelectedIndex = 0;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des monstres : {ex.Message}",
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadSpells()
        {
            try
            {
                using var context = new ExerciceMonsterContext();
                allSpells = context.Spells
                                  .Include(s => s.Monsters) 
                                  .ToList();

                SpellDataGrid.ItemsSource = allSpells; 
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des sorts : {ex.Message}",
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnMonsterSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MonsterComboBox.SelectedValue is int selectedMonsterId)
            {
                if (selectedMonsterId == 0)
                {
                    SpellDataGrid.ItemsSource = allSpells;
                }
                else
                {
                    var filteredSpells = allSpells
                        .Where(s => s.Monsters.Any(m => m.Id == selectedMonsterId))
                        .ToList();

                    SpellDataGrid.ItemsSource = filteredSpells;
                }
            }
        }

        private void OnViewMonstersClick(object sender, RoutedEventArgs e)
        {
            var monsterWindow = new MonsterWindow();
            monsterWindow.Show();
            this.Close();
        }
    }
}
