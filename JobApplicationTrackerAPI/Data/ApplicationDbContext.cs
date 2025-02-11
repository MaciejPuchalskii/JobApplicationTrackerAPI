using JobApplicationTrackerAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTrackerAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JobApplication>()
                        .HasOne(j => j.User)
                        .WithMany(u => u.JobApplications)
                        .HasForeignKey(j => j.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobApplication>()
                        .HasOne(j => j.Company)
                        .WithMany(c => c.JobApplications)
                        .HasForeignKey(j => j.CompanyId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Attachment>()
                        .HasOne<JobApplication>()
                        .WithMany(j => j.Attachments)
                        .HasForeignKey(a => a.JobApplicationId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Note>()
                        .HasOne<JobApplication>()
                        .WithMany(j => j.NotesList)
                        .HasForeignKey(n => n.JobApplicationId)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}