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

        // Audit işlemleri için SaveChanges metodunu override ediyoruz
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
            base.OnModelCreating(modelBuilder); // Identity yapılandırmalarını yap
            
            // İlişkileri tanımla
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students)
                .UsingEntity(j => j.ToTable("StudentCourses"));
        }
    }
}
