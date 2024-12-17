using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PokemonWpf.Model;

namespace PokemonWpf.Views
{
    public partial class BattlePage : Window, INotifyPropertyChanged
    {
        private MediaPlayer _mediaPlayer;

        public event PropertyChangedEventHandler PropertyChanged;

        private Monster _playerMonster;
        public Monster PlayerMonster
        {
            get => _playerMonster;
            set
            {
                _playerMonster = value;
                OnPropertyChanged();
            }
        }

        private Monster _enemyMonster;
        public Monster EnemyMonster
        {
            get => _enemyMonster;
            set
            {
                _enemyMonster = value;
                OnPropertyChanged();
            }
        }

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
            PlayBackgroundMusic();
        }

        private void PlayBackgroundMusic()
        {
            _mediaPlayer = new MediaPlayer();

            string musicPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Pokemon Emerald Soundtrack #10 - Wild Pokemon Battle!.mp3");

            _mediaPlayer.Open(new Uri(musicPath));
            _mediaPlayer.MediaEnded += (s, e) => _mediaPlayer.Position = TimeSpan.Zero;
            _mediaPlayer.Play();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _mediaPlayer?.Stop();
            _mediaPlayer?.Close();
        }

        private Monster GetRandomEnemyMonster()
        {
            using var context = new ExerciceMonsterContext();
            return context.Monsters.OrderBy(m => Guid.NewGuid()).FirstOrDefault()
                ?? throw new Exception("No enemy monster found.");
        }

        private List<Spell> GetSpellsForMonster(int monsterId)
        {
            using var context = new ExerciceMonsterContext();
            return context.Spells
                          .Where(s => s.Monsters.Any(m => m.Id == monsterId))
                          .ToList();
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
            PlayerMonster.Health = PlayerMonster.MaxHealth;

            EnemyMonster = GetRandomEnemyMonster();
            EnemyMonster.MaxHealth = EnemyMonster.Health;

            UpdateHealthBars();

            MessageBox.Show($"A new enemy Pokémon, {EnemyMonster.Name}, has appeared!");
        }

        private void RestartBattle_Click(object sender, RoutedEventArgs e)
        {
            PlayerMonster.Health = PlayerMonster.MaxHealth;
            EnemyMonster = GetRandomEnemyMonster();
            EnemyMonster.MaxHealth = EnemyMonster.Health;

            MessageBox.Show("A new battle begins!");
            UpdateHealthBars();
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

            OnPropertyChanged(nameof(PlayerMonster));
            OnPropertyChanged(nameof(EnemyMonster));
        }

        private void BackToMonsterList_Click(object sender, RoutedEventArgs e)
        {
            var monsterWindow = new MonsterWindow();
            monsterWindow.Show();
            this.Close();
        }

        private void SpellComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SpellComboBox.SelectedItem is Spell selectedSpell)
            {
                SpellDetailsTextBlock.Text = $"Damage: {selectedSpell.Damage}\nDescription: {selectedSpell.Description}";
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
