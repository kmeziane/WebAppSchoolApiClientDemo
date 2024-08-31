using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebAppSchoolApiClientDemo.Models;

namespace WebAppSchoolApiClientDemo.Data;

public partial class SchoolDemoContext : DbContext
{
    public SchoolDemoContext()
    {
    }

    public SchoolDemoContext(DbContextOptions<SchoolDemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasMany(d => d.Students).WithMany(p => p.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseStudent",
                    r => r.HasOne<Student>().WithMany().HasForeignKey("StudentsId"),
                    l => l.HasOne<Course>().WithMany().HasForeignKey("CoursesId"),
                    j =>
                    {
                        j.HasKey("CoursesId", "StudentsId");
                        j.ToTable("CourseStudent");
                        j.HasIndex(new[] { "StudentsId" }, "IX_CourseStudent_StudentsId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
