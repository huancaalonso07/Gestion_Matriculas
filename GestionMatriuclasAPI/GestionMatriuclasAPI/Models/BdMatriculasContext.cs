using System;
using System.Collections.Generic;
using GestionMatriuclasAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GestionMatriuclasAPI.Models;

public partial class BdMatriculasContext : DbContext
{
    public BdMatriculasContext()
    {
    }

    public BdMatriculasContext(DbContextOptions<BdMatriculasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Matricula> Matriculas { get; set; }

    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Server=localhost;Database=bd_matriculas;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MatriculaDto>().HasNoKey();

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("PK__curso__5D3F750252EE908A");

            entity.ToTable("curso");

            entity.Property(e => e.IdCurso).HasColumnName("id_curso");
            entity.Property(e => e.Descripcion)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreCurso)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_curso");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante).HasName("PK__estudian__E0B2763C13FE968E");

            entity.ToTable("estudiante");

            entity.HasIndex(e => e.Dni, "UQ__estudian__D87608A70DDA7285").IsUnique();

            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Dni)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("dni");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombres");
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.IdMatricula).HasName("PK__matricul__1D7CF00B3C38C742");

            entity.ToTable("matricula");

            entity.HasIndex(e => new { e.IdEstudiante, e.IdCurso }, "uq_estudiante_curso").IsUnique();

            entity.Property(e => e.IdMatricula).HasColumnName("id_matricula");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Activa")
                .HasColumnName("estado");
            entity.Property(e => e.FechaMatricula).HasColumnName("fecha_matricula");
            entity.Property(e => e.IdCurso).HasColumnName("id_curso");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.IdCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_matricula_curso");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_matricula_estudiante");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
