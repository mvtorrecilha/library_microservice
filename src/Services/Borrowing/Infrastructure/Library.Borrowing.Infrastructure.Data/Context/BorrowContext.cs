using Library.Borrowing.Domain.Entities;
using Library.Borrowing.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Library.Borrowing.Infrastructure.Data.Context;

public class BorrowContext : DbContext
{
    public BorrowContext(DbContextOptions<BorrowContext> options) : base(options)
    {
    }

    public DbSet<BorrowingHistory> BorrowingHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(nameof(modelBuilder));

        modelBuilder
            .ApplyConfiguration(new BorrowingHistoryEntityTypeConfiguration());
    }

    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<BorrowContext>
    {
        public BorrowContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BorrowContext>()
                .UseSqlServer("Server=.;Initial Catalog=Library_BorrowHistoryDB;Integrated Security=true");

            return new BorrowContext(optionsBuilder.Options);
        }
    }
}