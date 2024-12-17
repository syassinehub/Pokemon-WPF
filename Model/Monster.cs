using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonWpf.Model;

public partial class Monster
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Health { get; set; }

    public string? ImageUrl { get; set; }

    [NotMapped] 
    public int MaxHealth { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual ICollection<Spell> Spells { get; set; } = new List<Spell>();

    [NotMapped]
    public string SpellNames => Spells != null && Spells.Any()
        ? string.Join(", ", Spells.Select(s => s.Name))
        : "Aucun sort";
}
