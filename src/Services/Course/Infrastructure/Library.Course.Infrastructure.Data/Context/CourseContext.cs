using Library.Course.Domain.Entities;
using Library.Course.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Library.Course.Infrastructure.Data.Context;

public class CourseContext : DbContext
{
    public CourseContext(DbContextOptions<CourseContext> options) : base(options)
    {
    }

    public DbSet<CourseItem> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(nameof(modelBuilder));

        modelBuilder
            .ApplyConfiguration(new CourseEntityTypeConfiguration());
    }

    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<CourseContext>
    {
        public CourseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CourseContext>()
                .UseSqlServer("Server=.;Initial Catalog=Library_CourseDB;Integrated Security=true");

            return new CourseContext(optionsBuilder.Options);
        }
    }
}
