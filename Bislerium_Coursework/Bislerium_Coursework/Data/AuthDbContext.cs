using Bislerium_Coursework.Model.Authentication.ResetPassword;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bislerium_Coursework.Data
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public DbSet<ResetPasswordCode> ResetPasswordCodes { get; set; } // Ensures ResetPasswordCode is included in the context

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure the primary key and relationships for ResetPasswordCode
            builder.Entity<ResetPasswordCode>(entity =>
            {
                entity.HasKey(rpc => rpc.Id);
                entity.HasOne<IdentityUser>(rpc => rpc.User)
                    .WithMany()
                    .HasForeignKey(rpc => rpc.UserId)  // Ensure this matches the foreign key column name in the database.
                    .IsRequired();
            });

            // Seed roles
            SeedRoles(builder);

            // Seed admin user
            SeedAdminUser(builder);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
            );
        }

        private static void SeedAdminUser(ModelBuilder builder)
        {
            var adminUserId = "admin-userid"; // Consider using a constant GUID here
            var hasher = new PasswordHasher<IdentityUser>();

            var adminUser = new IdentityUser
            {
                Id = adminUserId,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "AdminPassword@123") // Use a strong password!
            };

            builder.Entity<IdentityUser>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "1", // Assuming '1' is the Id of the Admin role seeded earlier
                UserId = adminUserId
            });
        }
    }
}
