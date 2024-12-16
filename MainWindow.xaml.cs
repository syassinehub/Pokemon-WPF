﻿using System;
using System.Data.SqlClient;
using System.Windows;
using PokemonWpf.Views; 

namespace PokemonWpf
{
    public partial class MainWindow : Window
    {
        // Chaîne de connexion à ta base de données
        private readonly string connectionString = @"Server=localhost\SQLEXPRESS;Database=ExerciceMonster;Trusted_Connection=True;";

        public MainWindow()
        {
            InitializeComponent();
        }


        private void OnLoginClick(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(1) FROM Login WHERE Username = @Username AND PasswordHash = HASHBYTES('SHA2_256', @Password)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        if (count == 1)
                        {
                            MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                            // Ouvrir MonsterWindow
                            var monsterWindow = new Views.MonsterWindow();
                            monsterWindow.Show();
                            this.Close(); // Fermer la fenêtre actuelle
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void OnSignInClick(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Vérifier si le nom d'utilisateur existe déjà
                    string checkQuery = "SELECT COUNT(1) FROM Login WHERE Username = @Username";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Username", username);

                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose another one.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    // Insérer le nouveau compte si le nom d'utilisateur n'existe pas
                    string insertQuery = "INSERT INTO Login (Username, PasswordHash) VALUES (@Username, HASHBYTES('SHA2_256', @Password))";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Username", username);
                        insertCommand.Parameters.AddWithValue("@Password", password);

                        insertCommand.ExecuteNonQuery();
                        MessageBox.Show($"Account created for {username}!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}