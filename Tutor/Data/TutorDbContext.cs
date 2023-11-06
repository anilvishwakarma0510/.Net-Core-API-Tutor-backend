using Microsoft.EntityFrameworkCore;
using Tutor.Models.Domain;

namespace Tutor.Data
{
    public class TutorDbContext : DbContext
    {
        public TutorDbContext(DbContextOptions dbContextOptions) : base (dbContextOptions) 
        {

        }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<RoleModel> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().ToTable("users");
            modelBuilder.Entity<UserModel>().Property(u=>u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<UserModel>().HasIndex(u => u.Email).IsUnique();

            //specified user role relation
            modelBuilder.Entity<UserModel>()
            .HasOne(u => u.UserRole)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);
            //modelBuilder.Entity<UserModel>().Property(u => u.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp(6)");
            //modelBuilder.Entity<UserModel>().Property(u => u.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnUpdate().HasColumnType("timestamp(6)");





            modelBuilder.Entity<RoleModel>().ToTable("role");
            modelBuilder.Entity<RoleModel>().HasIndex(u => u.Name).IsUnique();
            //modelBuilder.Entity<RoleModel>().Property(u => u.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp(6)");
            //modelBuilder.Entity<RoleModel>().Property(u => u.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnUpdate().HasColumnType("timestamp(6)");


            modelBuilder.Entity<RoleModel>().HasData(
                new RoleModel { Id = 1, Name = "Administrator", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new RoleModel { Id = 2, Name = "Subadmin", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new RoleModel { Id = 3, Name = "Tutor", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new RoleModel { Id = 4, Name = "Student", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            );

            //InsertAdmin
            modelBuilder.Entity<UserModel>().HasData(
                new UserModel
                {
                    Id = 1,
                    FirstName = "Super",
                    LastName = "Admin",
                    Email = "admin@gmail.com",
                    EmailVerified = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    RoleId = 1,
                    Status = true,
                    Password = BCrypt.Net.BCrypt.HashPassword("12345678"),
                }
                );
        }

    }
}
