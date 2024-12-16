using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PokemonWpf.Model;

namespace PokemonWpf.Views
{
    public partial class BattlePage : Window
    {
        private readonly string connectionString = @"Server=localhost\SQLEXPRESS;Database=ExerciceMonster;Trusted_Connection=True;";

        public Monster PlayerMonster { get; set; }
        public Monster EnemyMonster { get; set; }
        public List<Spell> PlayerSpells { get; set; }
        private readonly Random random = new Random();

        public BattlePage(Monster playerMonster)
        {
            InitializeComponent();
            PlayerMonster = playerMonster;
            PlayerMonster.MaxHealth = PlayerMonster.Health;

            EnemyMonster = GetRandomEnemyMonster();
            EnemyMonster.MaxHealth = EnemyMonster.Health;

            PlayerSpells = GetSpellsForMonster(PlayerMonster.Id);
            SpellComboBox.ItemsSource = PlayerSpells;

            DataContext = this;

            UpdateHealthBars();
        }

        private Monster GetRandomEnemyMonster()
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "SELECT TOP 1 Id, Name, Health, ImageURL FROM Monster ORDER BY NEWID()";
            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Monster
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Health = reader.GetInt32(2),
                    ImageUrl = reader.IsDBNull(3) ? null : reader.GetString(3) // Récupérer l'URL de l'image
                };
            }

            throw new Exception("No enemy monster found.");
        }


        private List<Spell> GetSpellsForMonster(int monsterId)
        {
            var spells = new List<Spell>();

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            string query = @"SELECT s.Id, s.Name, s.Damage, s.Description
                             FROM Spell s
                             INNER JOIN MonsterSpell ms ON ms.SpellID = s.Id
                             WHERE ms.MonsterID = @MonsterID";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MonsterID", monsterId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                spells.Add(new Spell
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Damage = reader.GetInt32(2),
                    Description = reader.GetString(3)
                });
            }

            return spells;
        }

        private void UseSpell_Click(object sender, RoutedEventArgs e)
        {
            if (SpellComboBox.SelectedItem is not Spell selectedSpell) return;

            EnemyMonster.Health -= selectedSpell.Damage;
            if (EnemyMonster.Health < 0) EnemyMonster.Health = 0;

            MessageBox.Show($"You used {selectedSpell.Name} and dealt {selectedSpell.Damage} damage!");

            UpdateHealthBars();

            if (EnemyMonster.Health > 0)
            {
                EnemyAttack();
            }
            else
            {
                MessageBox.Show("You won the battle!");
                StartNewBattle(); 
            }
        }

        private void StartNewBattle()
        {
            EnemyMonster = GetRandomEnemyMonster();
            EnemyMonster.MaxHealth = EnemyMonster.Health;

            UpdateHealthBars();

            MessageBox.Show($"A new enemy Pokémon, {EnemyMonster.Name}, has appeared!");
        }

        private void EnemyAttack()
        {
            var enemySpells = GetSpellsForMonster(EnemyMonster.Id);
            if (enemySpells.Count == 0) return;

            var randomSpell = enemySpells[random.Next(enemySpells.Count)];
            PlayerMonster.Health -= randomSpell.Damage;
            if (PlayerMonster.Health < 0) PlayerMonster.Health = 0;

            MessageBox.Show($"Enemy used {randomSpell.Name} and dealt {randomSpell.Damage} damage!");

            UpdateHealthBars();

            if (PlayerMonster.Health <= 0)
            {
                MessageBox.Show("You lost the battle!");

                var monsterWindow = new MonsterWindow();
                monsterWindow.Show();
                this.Close(); 
            }
        }


        private void UpdateHealthBars()
        {
            PlayerHealthBar.Value = PlayerMonster.Health;
            EnemyHealthBar.Value = EnemyMonster.Health;
        }

        private void RestartBattle_Click(object sender, RoutedEventArgs e)
        {
            PlayerMonster.Health = PlayerMonster.MaxHealth;
            EnemyMonster = GetRandomEnemyMonster();
            EnemyMonster.MaxHealth = EnemyMonster.Health;

            MessageBox.Show("A new battle begins!");
            UpdateHealthBars();
        }

        private void BackToMonsterList_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SpellComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SpellComboBox.SelectedItem is Spell selectedSpell)
            {
                SpellDetailsTextBlock.Text = $"Damage: {selectedSpell.Damage}\nDescription: {selectedSpell.Description}";
            }
        }
    }
}
