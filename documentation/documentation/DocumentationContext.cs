using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace documentation;

public partial class DocumentationContext : DbContext
{
    public DocumentationContext()
    {
    }

    public DocumentationContext(DbContextOptions<DocumentationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=documentation;Username=postgres;Password=1qazxsw2");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("documents_pkey");

            entity.ToTable("documents");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Author).HasColumnName("author");
            entity.Property(e => e.Description)
                .HasMaxLength(30)
                .HasColumnName("description");
            entity.Property(e => e.Number)
                .HasMaxLength(30)
                .HasColumnName("number");
            entity.Property(e => e.Recipient).HasColumnName("recipient");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .HasColumnName("title");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Department)
                .HasMaxLength(30)
                .HasColumnName("department");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .HasColumnName("first_name");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(30)
                .HasColumnName("job_title");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(30)
                .HasColumnName("middle_name");
            entity.Property(e => e.Role)
                .HasMaxLength(30)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
