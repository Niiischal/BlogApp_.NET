using Bislerium_Coursework.Model;
using Microsoft.EntityFrameworkCore;

namespace Bislerium_Coursework.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet properties
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }

        // Possibly other DbSets
    }
}
