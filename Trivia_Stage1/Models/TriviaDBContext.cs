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
        => optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database=Trivia; Trusted_Connection=true; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Level>(entity =>
        {
            entity.HasKey(e => e.LevelCode).HasName("PK__Levels__BE500FE39C9CE1ED");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Players__2CDA01F157DA24B0");

            entity.HasOne(d => d.LevelCodeNavigation).WithMany(p => p.Players)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Players__levelCo__3C69FB99");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__6238D4B20187383F");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Questions__creat__403A8C7D");

            entity.HasOne(d => d.StatusCodeNavigation).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Questions__statu__412EB0B6");

            entity.HasOne(d => d.SubjectCodeNavigation).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Questions__subje__3F466844");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusCode).HasName("PK__Status__AD4366F7E341B929");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectCode).HasName("PK__Subjects__97E46C96D2C3FE3B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
