using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StudentCourseManagement.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

       public override int SaveChanges()
       {
           foreach (var entry in ChangeTracker.Entries<IAuditEntity>())
           {
               if (entry.State == EntityState.Added)
               {
                   entry.Entity.CreatedAt = DateTime.UtcNow;
               }
               else if (entry.State == EntityState.Modified)
               {
                   entry.Entity.ModifiedAt = DateTime.UtcNow;
               }
               else if (entry.State == EntityState.Deleted)
               {
                   entry.State = EntityState.Modified;
                   entry.Entity.IsActive = false;
               }
           }
       
           return base.SaveChanges();
       }
       
       public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
       {
           foreach (var entry in ChangeTracker.Entries<IAuditEntity>())
           {
               if (entry.State == EntityState.Added)
               {
                   entry.Entity.CreatedAt = DateTime.UtcNow;
               }
               else if (entry.State == EntityState.Modified)
               {
                   entry.Entity.ModifiedAt = DateTime.UtcNow;
               }
               else if (entry.State == EntityState.Deleted)
               {
                   entry.State = EntityState.Modified;
                   entry.Entity.IsActive = false;
               }
           }
       
           return await base.SaveChangesAsync(cancellationToken);
       }


       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
           base.OnModelCreating(modelBuilder);

           modelBuilder.Entity<StudentCourse>()
               .HasKey(sc => new { sc.CoursesCourseId, sc.StudentsStudentId });

           modelBuilder.Entity<StudentCourse>()
               .HasOne(sc => sc.Student)
               .WithMany(s => s.StudentCourses)
               .HasForeignKey(sc => sc.StudentsStudentId);

           modelBuilder.Entity<StudentCourse>()
               .HasOne(sc => sc.Course)
               .WithMany(c => c.StudentCourses)
               .HasForeignKey(sc => sc.CoursesCourseId);

           modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
               new IdentityRole { Id = "2", Name = "Öğrenci", NormalizedName = "ÖĞRENCİ" });

           var adminUser = new IdentityUser
           {
               Id = "1",
               UserName = "admin@itb.com",
               NormalizedUserName = "ADMIN@ITB.COM",
               Email = "admin@itb.com",
               NormalizedEmail = "ADMIN@ITB.COM",
               EmailConfirmed = true,
               SecurityStamp = Guid.NewGuid().ToString()
           };

           var passwordHasher = new PasswordHasher<IdentityUser>();
           adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "ITB2024!!");

           modelBuilder.Entity<IdentityUser>().HasData(adminUser);

           modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
           {
               UserId = "1",
               RoleId = "1"
           });
       }

    }
}
