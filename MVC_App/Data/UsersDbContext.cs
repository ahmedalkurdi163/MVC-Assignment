using System;
using System.Collections.Generic;
using MVC_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;

namespace MVC_App.Data;
// هذا الكلاس من اجل المستخدمين والادوار
public partial class UsersDbContext : DbContext
{
    public UsersDbContext()
    {
    }

    public UsersDbContext(DbContextOptions<UsersDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
    public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Ahmed;Integrated Security=True;Persist Security Info=False;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<AspNetUserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<AspNetUserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AspNetUserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");
        });

        modelBuilder.Entity<AspNetUser>()
            .HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<AspNetRole>()
            .HasMany(r => r.UserRoles)
            .WithOne(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId);

        OnModelCreatingPartial(modelBuilder);
    }


    public static void CreateInitialTestingDatabase(UsersDbContext context)
    {
        context.Database.EnsureCreated();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }

        if (!context.AspNetRoles.Any())
        {
            
           
            var adminRole = new AspNetRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var userRole = new AspNetRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "user",
                NormalizedName = "USER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            context.AspNetRoles.Add(adminRole);
            context.AspNetRoles.Add(userRole);
            context.SaveChanges();
        }

        var passwordHasher = new PasswordHasher<AspNetUser>();

        if (!context.AspNetUsers.Any(u => u.UserName == "admin@gmail.com"))
        {
            var adminUser = new AspNetUser()
            {
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Id = Guid.NewGuid().ToString(),
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                AccessFailedCount = 0,
                NormalizedEmail = "ADMIN@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "P@ssword123");

            context.AspNetUsers.Add(adminUser);
            context.SaveChanges();

            var adminRole = context.AspNetRoles.FirstOrDefault(r => r.Name == "admin");
            if (adminRole != null)
            {
                context.AspNetUserRoles.Add(new AspNetUserRole
                {
                    UserId = adminUser.Id,
                    RoleId = adminRole.Id
                });
                context.SaveChanges();
            }
        }
        if (!context.AspNetUsers.Any(u => u.UserName == "user@gmail.com"))
        {
            var regularUser = new AspNetUser()
            {
                UserName = "user@gmail.com",
                NormalizedUserName = "USER@GMAIL.COM",
                Id = Guid.NewGuid().ToString(),
                Email = "user@gmail.com",
                EmailConfirmed = true,
                AccessFailedCount = 0,
                NormalizedEmail = "USER@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            regularUser.PasswordHash = passwordHasher.HashPassword(regularUser, "P@ssword123");

            context.AspNetUsers.Add(regularUser);
            context.SaveChanges();

            var userRole = context.AspNetRoles.FirstOrDefault(r => r.Name == "user");
            if (userRole != null)
            {
                context.AspNetUserRoles.Add(new AspNetUserRole
                {
                    UserId = regularUser.Id,
                    RoleId = userRole.Id
                });
                context.SaveChanges();
            }
        }
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
