using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using PokemonWpf.Model;
using PokemonWpf.Views;

namespace PokemonWpf.Views
{
    public partial class MonsterWindow : Window
    {
        private readonly string connectionString = @"Server=localhost\SQLEXPRESS;Database=ExerciceMonster;Trusted_Connection=True;";

        public MonsterWindow()
        {
            InitializeComponent();
            LoadMonsters();
        }

        private void OnViewSpellsClick(object sender, RoutedEventArgs e)
        {
            var spellWindow = new SpellWindow();
            spellWindow.Show();
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
            List<Monster> monsters = new List<Monster>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id, Name, Health, ImageUrl FROM Monster";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                monsters.Add(new Monster
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Health = reader.GetInt32(2),
                                    ImageUrl = reader.IsDBNull(3) ? null : reader.GetString(3)
                                });
                            }
                        }
                    }
                }
                MonsterDataGrid.ItemsSource = monsters;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading monsters: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
