using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class Question
{
    [Key]
    [Column("questionId")]
    public int QuestionId { get; set; }

    [Column("subjectCode")]
    public int SubjectCode { get; set; }

    [Column("text")]
    [StringLength(250)]
    public string Text { get; set; } = null!;

    [Column("correctAns")]
    [StringLength(250)]
    public string CorrectAns { get; set; } = null!;

    [Column("wrongAns1")]
    [StringLength(250)]
    public string WrongAns1 { get; set; } = null!;

    [Column("wrongAns2")]
    [StringLength(250)]
    public string WrongAns2 { get; set; } = null!;

    [Column("wrongAns3")]
    [StringLength(250)]
    public string WrongAns3 { get; set; } = null!;

    [Column("createdBy")]
    [StringLength(250)]
    public string CreatedBy { get; set; } = null!;

    [Column("statusCode")]
    public int StatusCode { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("Questions")]
    public virtual Player CreatedByNavigation { get; set; } = null!;

    [ForeignKey("StatusCode")]
    [InverseProperty("Questions")]
    public virtual Status StatusCodeNavigation { get; set; } = null!;

    [ForeignKey("SubjectCode")]
    [InverseProperty("Questions")]
    public virtual Subject SubjectCodeNavigation { get; set; } = null!;
}
