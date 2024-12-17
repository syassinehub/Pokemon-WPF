using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PokemonWpf.Model
{
    public partial class ExerciceMonsterContext : DbContext
    {
        public ExerciceMonsterContext()
        {
        }

        public ExerciceMonsterContext(DbContextOptions<ExerciceMonsterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Monster> Monsters { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Spell> Spells { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning Move the connection string to a secure location.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ExerciceMonster;Trusted_Connection=True; TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Login");
                entity.ToTable("Login");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Username).HasMaxLength(50).IsRequired();
                entity.Property(e => e.PasswordHash).HasMaxLength(255).IsRequired();
            });

            modelBuilder.Entity<Monster>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Monster");
                entity.ToTable("Monster");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
                entity.Property(e => e.ImageUrl).HasMaxLength(255).HasColumnName("ImageURL");

                entity.HasMany(d => d.Spells).WithMany(p => p.Monsters)
                    .UsingEntity<Dictionary<string, object>>(
                        "MonsterSpell",
                        r => r.HasOne<Spell>().WithMany().HasForeignKey("SpellId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_MonsterSpell_Spell"),
                        l => l.HasOne<Monster>().WithMany().HasForeignKey("MonsterId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_MonsterSpell_Monster"),
                        j =>
                        {
                            j.HasKey("MonsterId", "SpellId").HasName("PK_MonsterSpell");
                            j.ToTable("MonsterSpell");
                            j.Property<int>("MonsterId").HasColumnName("MonsterID");
                            j.Property<int>("SpellId").HasColumnName("SpellID");
                        });
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Player");
                entity.ToTable("Player");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
                entity.Property(e => e.LoginId).HasColumnName("LoginID");

                entity.HasOne(d => d.Login).WithMany(p => p.Players)
                    .HasForeignKey(d => d.LoginId)
                    .HasConstraintName("FK_Player_Login");

                entity.HasMany(d => d.Monsters).WithMany(p => p.Players)
                    .UsingEntity<Dictionary<string, object>>(
                        "PlayerMonster",
                        r => r.HasOne<Monster>().WithMany().HasForeignKey("MonsterId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_PlayerMonster_Monster"),
                        l => l.HasOne<Player>().WithMany().HasForeignKey("PlayerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_PlayerMonster_Player"),
                        j =>
                        {
                            j.HasKey("PlayerId", "MonsterId").HasName("PK_PlayerMonster");
                            j.ToTable("PlayerMonster");
                            j.Property<int>("PlayerId").HasColumnName("PlayerID");
                            j.Property<int>("MonsterId").HasColumnName("MonsterID");
                        });
            });

            modelBuilder.Entity<Spell>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Spell");
                entity.ToTable("Spell");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Damage).IsRequired();
                entity.Property(e => e.Description).HasColumnType("nvarchar(max)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
