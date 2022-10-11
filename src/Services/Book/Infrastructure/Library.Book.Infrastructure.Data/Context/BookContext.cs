using Library.Book.Domain.Entities;
using Library.Book.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Library.Book.Infrastructure.Data.Context;

public class BookContext : DbContext
{
    public BookContext(DbContextOptions<BookContext> options) : base(options)
    {
    }

    public DbSet<BookItem> Books { get; set; }

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(nameof(modelBuilder));

        modelBuilder
            .ApplyConfiguration(new BookEntityTypeConfiguration())
            .ApplyConfiguration(new CategoryEntityTypeConfiguration());
    }

    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<BookContext>
    {
        public BookContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookContext>()
                .UseSqlServer("Server=.;Initial Catalog=Library_BookDB;Integrated Security=true");

            return new BookContext(optionsBuilder.Options);
        }
    }
}
