using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class TriviaDBContext : DbContext
{
    public TriviaDBContext()
    {
    }

    public TriviaDBContext(DbContextOptions<TriviaDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Level> Levels { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = localhost\\SQLEXPRESS; Database=Trivia; Trusted_Connection=true; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Level>(entity =>
        {
            entity.HasKey(e => e.LevelCode).HasName("PK__Levels__BE500FE3D0D0C522");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerMail).HasName("PK__Players__F50BE709141330EF");

            entity.HasOne(d => d.LevelCodeNavigation).WithMany(p => p.Players)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Players__levelCo__440B1D61");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__6238D4B2A5B3F283");

            entity.Property(e => e.QuestionId).ValueGeneratedNever();

            entity.HasOne(d => d.StatusCodeNavigation).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Questions__statu__412EB0B6");

            entity.HasOne(d => d.SubjectCodeNavigation).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Questions__subje__3F466844");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusCode).HasName("PK__Status__AD4366F7A99A95CF");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectCode).HasName("PK__Subjects__97E46C9660A7FC72");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
