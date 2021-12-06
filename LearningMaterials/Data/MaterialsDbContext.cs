using LearningMaterials.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningMaterials.Data
{
    public class MaterialsDbContext : DbContext
    {
        public MaterialsDbContext(DbContextOptions<MaterialsDbContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}