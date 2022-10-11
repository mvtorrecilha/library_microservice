using Library.Borrowing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Borrowing.Infrastructure.Data.EntityConfigurations;

public class BorrowingHistoryEntityTypeConfiguration : IEntityTypeConfiguration<BorrowingHistory>
{
    public void Configure(EntityTypeBuilder<BorrowingHistory> builder)
    {
        ArgumentNullException.ThrowIfNull(nameof(builder));

        builder.ToTable("BorrowingHistory");

        builder
          .HasKey(s => s.Id)
          .IsClustered(false)
          .HasName("PK_BorrowHistory");

        builder.Property(c => c.BookId)
            .HasColumnType("uniqueidentifier")
            .IsRequired(true);

        builder.Property(c => c.StudentId)
           .HasColumnType("uniqueidentifier")
           .IsRequired(true);

        builder.Property(b => b.BorrowDate)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(b => b.ReturnDate)
            .HasColumnType("datetime")
            .IsRequired(false);
    }
}
