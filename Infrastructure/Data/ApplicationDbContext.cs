using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TaskItem entity configuration
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.ToTable("TaskItem"); // SQL'deki tablo ismi - Singular!
                
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(); // IDENTITY(1,1)
                
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(e => e.Description)
                    .HasColumnType("NVARCHAR(MAX)");
                
                entity.Property(e => e.IsCompleted)
                    .IsRequired()
                    .HasDefaultValue(false);
                
                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.UserId)
                    .IsRequired();

                // Foreign Key Relationship
                entity.HasOne(t => t.User)
                    .WithMany(u => u.TaskItems)
                    .HasForeignKey(t => t.UserId)
                    .HasConstraintName("FK_TaskItem_User")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // User entity configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User"); // SQL'deki tablo ismi
                
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(); // IDENTITY(1,1)
                
                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(e => e.Username)
                    .IsUnique(); // Unique constraint for Username
                
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.Email)
                    .HasMaxLength(100);
            });
        }
    }
}
