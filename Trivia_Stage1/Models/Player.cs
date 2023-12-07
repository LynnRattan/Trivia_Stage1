using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class Player
{
    [Key]
    [Column("playerId")]
    public int PlayerId { get; set; }

    [Column("playerMail")]
    [StringLength(250)]
    public string PlayerMail { get; set; } = null!;

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("password")]
    [StringLength(250)]
    public string Password { get; set; } = null!;

    [Column("levelCode")]
    public int LevelCode { get; set; }

    [Column("points")]
    public int Points { get; set; }

    [ForeignKey("LevelCode")]
    [InverseProperty("Players")]
    public virtual Level LevelCodeNavigation { get; set; } = null!;

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
