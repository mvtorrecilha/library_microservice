using Library.Student.Domain.Entities;
using Library.Student.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Library.Student.Infrastructure.Data.Context;

public class StudentContext : DbContext
{
    public StudentContext(DbContextOptions<StudentContext> options) : base(options)
    {
    }

    public DbSet<StudentItem> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(nameof(modelBuilder));

        modelBuilder
            .ApplyConfiguration(new StudentEntityTypeConfiguration());
    }

    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<StudentContext>
    {
        public StudentContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StudentContext>()
                .UseSqlServer("Server=.;Initial Catalog=Library_StudentDB;Integrated Security=true");

            return new StudentContext(optionsBuilder.Options);
        }
    }
}
