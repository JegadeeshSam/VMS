using Microsoft.EntityFrameworkCore;
using VisitorManagementSystem.Models;

namespace VisitorManagementSystem.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Visitor> Visitors { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Visit> Visits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Visitor configuration
        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.HasKey(e => e.VisitorId);
            entity.Property(e => e.VisitorName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CompanyName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.MobileNo).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.IDProof).HasMaxLength(50);
        });

        // Employee configuration
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);
            entity.Property(e => e.EmployeeName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Department).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Designation).IsRequired().HasMaxLength(50);
            entity.Property(e => e.MobileNo).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        // Visit configuration
        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => e.VisitId);
            entity.Property(e => e.Purpose).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
            
            entity.HasOne(e => e.Visitor)
                  .WithMany(v => v.Visits)
                  .HasForeignKey(e => e.VisitorId)
                  .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.Employee)
                  .WithMany(v => v.Visits)
                  .HasForeignKey(e => e.EmployeeId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
