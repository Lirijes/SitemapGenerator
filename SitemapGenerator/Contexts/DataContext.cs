using Microsoft.EntityFrameworkCore;
using SitemapGenerator.Models.Entities;

namespace SitemapGenerator.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
