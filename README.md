Pokémon-Like Game en C# avec WPF
Bienvenue dans le projet Pokémon-Like ! Ce projet consiste à créer une application de type Pokémon, basée sur des combats tour par tour, en utilisant C# et Windows Presentation Foundation (WPF). L'application inclut une interface utilisateur ergonomique, la gestion d'une base de données SQL Server Express pour stocker les données du jeu (monstres, sorts, etc.), ainsi que des fonctionnalités de combat.

Avant de commencer, vous aurez besoin des éléments suivants :

Visual Studio avec prise en charge de WPF et de C#
SQL Server management studio (ou une autre instance de SQL Server compatible)
Entity Framework pour la gestion des données
.NET Framework la version la plus récente
Installation
Clonez ce projet depuis GitHub :

bash
Copier le code
git clone https://github.com/votre-utilisateur/Pokemon-Like-WPF.git
Ouvrez le projet dans Visual Studio.

Créez une base de données SQL Server sur Sql server management studio et configurez-la à l'aide du modèle fourni dans le projet. Assurez-vous que votre base de données soit accessible avec l'URL de connexion définie dans les paramètres du projet.

Compilez et exécutez l'application à partir de Visual Studio.

Configurez l'URL de connexion à la base de données dans l'écran des paramètres de l'application.

Fonctionnalités principales
1. Écran de connexion (Login) 
Permet aux utilisateurs de se connecter avec un nom d'utilisateur et un mot de passe (mot de passe hashé en BASE).
Les informations de connexion sont stockées dans la base de données.
2. Écran des paramètres de la base de données 
Permet de configurer l'URL de connexion à la base de données dans un écran dédié.
3. Fenêtre de gestion des monstres 
Liste tous les monstres disponibles.
Permet de sélectionner un monstre à jouer et affiche ses informations détaillées (nom, HP, sorts associés, etc.).
4. Onglet des sorts 
Liste les sorts existants dans le jeu.
Affiche un détail complet pour chaque sort (nom, dégâts, description).
Permet de trier les sorts en fonction du monstre qui les possède.
5. Onglet Combat 
Simule un combat entre un monstre du joueur et un monstre ennemi.
Utilise les sorts pour infliger des dégâts.
Affiche une barre d'HP visible pour chaque monstre.
Génère un nouveau monstre ennemi avec des statistiques légèrement améliorées à chaque tour de combat.
Permet de relancer un combat avec un nouveau monstre.
Crée un score à chaque monstre vaincu.
Base de données
Le projet utilise une base de données SQL Server Express pour stocker les données du jeu, y compris les monstres, sorts et utilisateurs.

Modèle de base de données :
Le modèle de base de données ne doit pas être modifié. Vous pouvez ajouter des données supplémentaires mais ne devez pas changer la structure de la base de données (ajout de colonnes, tables, etc.).
Chaque monstre possède 4 sorts, et chaque joueur contrôle un seul monstre.
Les informations de connexion sont stockées dans la table Login de la base de données.
Initialisation de la base de données
Une fois l'application lancée, vous devez initialiser la base de données en insérant un jeu de données par défaut (monstres, sorts, utilisateurs, etc.). Ceci se fait dans l'onglet des paramètres de l'application, où vous pouvez définir l'URL de connexion.

Gestion des données avec Entity Framework
L'application utilise Entity Framework pour gérer les opérations de création et de lecture dans la base de données. Le code de gestion des données est organisé dans le dossier Database du projet.

Technologies utilisées
C# - Langage de programmation principal
WPF (Windows Presentation Foundation) - Pour la création de l'interface utilisateur
SQL Server Express - Base de données relationnelle pour stocker les données du jeu
Entity Framework - ORM pour faciliter la gestion des données
MVVM - Architecture utilisée pour séparer la logique métier de l'interface utilisateur
Auteurs
Ce projet a été développé par [Votre Nom]. Si vous avez des questions, des suggestions ou des commentaires, n'hésitez pas à ouvrir un ticket ou à contribuer via une pull request.

Exemple de jeu de données de base (SQL):
sql
Copier le code
INSERT INTO [ExerciceMonster].[dbo].[Monster] (Name, Health, ImageURL)
VALUES 
('Pikachu', 200, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/025.png'),
('Bulbasaur', 250, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/detail/001.png'),
('Charmander', 230, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/detail/004.png'),
('Squirtle', 240, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/detail/007.png'),
('Jigglypuff', 180, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/detail/039.png'),
('Meowth', 210, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/detail/052.png'),
('Gyarados', 350, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/detail/130.png'),
('Onix', 400, 'https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/detail/095.png');

INSERT INTO [ExerciceMonster].[dbo].[PlayerMonster] (PlayerID, MonsterID)
VALUES 
(1, 1), -- Ash possède Pikachu (ID du joueur = 1, ID du monstre = 1)
(1, 2), -- Ash possède Bulbasaur (ID du joueur = 1, ID du monstre = 2)
(2, 4), -- Misty possède Squirtle (ID du joueur = 2, ID du monstre = 4)
(2, 7), -- Misty possède Gyarados (ID du joueur = 2, ID du monstre = 7)
(3, 8), -- Brock possède Onix (ID du joueur = 3, ID du monstre = 8)
(4, 3), -- Gary possède Charmander (ID du joueur = 4, ID du monstre = 3)
(5, 6); -- Jessie possède Meowth (ID du joueur = 5, ID du monstre = 6)

INSERT INTO [ExerciceMonster].[dbo].[Login] (Username, PasswordHash)
VALUES 
('AshKetchum', 'hashed_password_ash'),
('MistyWater', 'hashed_password_misty'),
('BrockRock', 'hashed_password_brock'),
('GaryOak', 'hashed_password_gary'),
('JessieTeamRocket', 'hashed_password_jessie');

INSERT INTO [ExerciceMonster].[dbo].[Player] (Name, LoginID)
VALUES 
('Ash', 1),    
('Misty', 2),  
('Brock', 3),  
('Gary', 4),   
('Jessie', 5); 

INSERT INTO [ExerciceMonster].[dbo].[PlayerMonster] (PlayerID, MonsterID)
VALUES 
(1, 1), 
(1, 2), 
(2, 4), 
(2, 7), 
(3, 8), 
(4, 3), 
(5, 6); 

INSERT INTO [ExerciceMonster].[dbo].[Spell] (Name, Damage, Description)
VALUES
('Coup de foudre', 40, 'Un choc électrique faible qui peut paralyser l’ennemi.'),
('Lianes Fouettées', 45, 'Tranche l’ennemi avec des lianes fines et souples.'),
('Lance-flammes', 90, 'Une attaque de flamme puissante qui peut brûler l’ennemi.'),
('Pistolet à eau', 40, 'Projette de l’eau pour attaquer l’ennemi.'),
('Chanson', 0, 'Une mélodie apaisante qui endort l’ennemi.'),
('Tranche', 70, 'Frappe l’ennemi avec des griffes acérées. Forte probabilité de coup critique.'),
('Hydrocanon', 110, 'Projette de l’eau à haute pression. Dégâts importants mais faible précision.'),
('Jet de pierres', 50, 'Lance des pierres à l’ennemi. Efficace contre les ennemis volants.'),
('Séisme', 100, 'Secoue violemment le sol, infligeant de gros dégâts.'),
('Psyko', 90, 'Utilise des pouvoirs psychiques pour attaquer l’ennemi et peut le confondre.'),
('Laser Glace', 90, 'Un rayon de glace qui peut geler l’ennemi.'),
('Solaire', 120, 'Une puissante attaque solaire qui nécessite un tour de charge.'),
('Ball Ombre', 80, 'Une balle d’énergie sombre qui peut réduire la défense spéciale de l’ennemi.'),
('Hyper Beam', 150, 'Un faisceau d’énergie massif qui nécessite un tour de recharge.'),
('Griffe Dragon', 80, 'Attaque avec des griffes acérées de dragon. Forte probabilité de coup critique.'),
('Tonnerre', 90, 'Une attaque électrique forte qui peut paralyser l’ennemi.'),
('Explosion', 110, 'Une puissante attaque de feu avec une chance de brûler l’ennemi.'),
('Tempête de feuilles', 130, 'Une tempête massive de feuilles qui peut abaisser l’attaque spéciale de l’utilisateur.'),
('Blizzard', 110, 'Une tempête de glace puissante qui peut geler l’ennemi.'),
('Queue de fer', 100, 'Attaque avec une queue ressemblant à du fer. Haute précision mais peut manquer.'),
('Cascade', 80, 'Une attaque d’eau forte qui peut confondre l’ennemi.'),
('Rayon Luminoc', 80, 'Un rayon lumineux éclatant qui affecte tous les ennemis proches.'),
('Fissure', 0, 'Une attaque de terre qui peut éliminer instantanément l’ennemi.');



INSERT INTO [ExerciceMonster].[dbo].[MonsterSpell] (MonsterID, SpellID)
VALUES 
-- Pikachu (ID 1)
(1, 1),  -- Coup de foudre
(1, 16), -- Tonnerre
(1, 17), -- Laser Glace
(1, 18), -- Tempête de feuilles

-- Bulbizarre (ID 2)
(2, 2),  -- Lianes Fouettées
(2, 18), -- Tempête de feuilles
(2, 12), -- Solaire
(2, 19), -- Explosion

-- Salamèche (ID 3)
(3, 3),  -- Lance-flammes
(3, 19), -- Explosion
(3, 20), -- Tranche
(3, 16), -- Tonnerre

-- Carapuce (ID 4)
(4, 4),  -- Pistolet à eau
(4, 21), -- Cascade
(4, 7),  -- Hydrocanon
(4, 18), -- Tempête de feuilles

-- Rondoudou (ID 5)
(5, 5),  -- Chanson
(5, 23), -- Rayon Luminoc
(5, 24), -- Ball Ombre
(5, 25), -- Griffe Dragon

-- Miaouss (ID 6)
(6, 6),  -- Tranche
(6, 7),  -- Hyper Beam
(6, 8),  -- Queue de fer
(6, 9),  -- Jet de pierres

-- Léviator (ID 7)
(7, 7),  -- Hydrocanon
(7, 25), -- Griffe Dragon
(7, 26), -- Séisme
(7, 11), -- Laser Glace

-- Onix (ID 8)
(8, 9),  -- Jet de pierres
(8, 10), -- Queue de fer
(8, 11), -- Laser Glace
(8, 27); -- Fissure

Vous pouvez insérer des données par défaut dans ces tables lors de l'initialisation.
