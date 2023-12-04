using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Table("Status")]
public partial class Status
{
    [Key]
    [Column("statusCode")]
    public int StatusCode { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [InverseProperty("StatusCodeNavigation")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
