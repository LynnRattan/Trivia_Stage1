using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class Level
{
    [Key]
    [Column("levelCode")]
    public int LevelCode { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [InverseProperty("LevelCodeNavigation")]
    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
