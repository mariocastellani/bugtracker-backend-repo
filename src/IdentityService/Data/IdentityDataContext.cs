using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentityService.Data
{
    public class IdentityDataContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map all tables to specific schema
            modelBuilder.HasDefaultSchema("Identity");

            // Map each entity to specific table

            modelBuilder.Entity<IdentityUser<int>>(b =>
            {
                b.ToTable("Users");
            });

            modelBuilder.Entity<IdentityUserClaim<int>>(b =>
            {
                b.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<int>>(b =>
            {
                b.ToTable("UserLogins");
            });

            modelBuilder.Entity<IdentityUserToken<int>>(b =>
            {
                b.ToTable("UserTokens");
            });

            modelBuilder.Entity<IdentityRole<int>>(b =>
            {
                b.ToTable("Roles");
            });

            modelBuilder.Entity<IdentityRoleClaim<int>>(b =>
            {
                b.ToTable("RoleClaims");
            });

            modelBuilder.Entity<IdentityUserRole<int>>(b =>
            {
                b.ToTable("UserRoles");
            });
        }
    }
}