using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using PokemonWpf.Model;

namespace PokemonWpf.Views
{
    public partial class SpellWindow : Window
    {
        private readonly string connectionString = @"Server=localhost\SQLEXPRESS;Database=ExerciceMonster;Trusted_Connection=True;";

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
            List<Spell> spells = new List<Spell>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id, Name, Damage, Description FROM Spell";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                spells.Add(new Spell
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Damage = reader.GetInt32(2),
                                    Description = reader.IsDBNull(3) ? null : reader.GetString(3)
                                });
                            }
                        }
                    }
                }
                SpellDataGrid.ItemsSource = spells;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading spells: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
