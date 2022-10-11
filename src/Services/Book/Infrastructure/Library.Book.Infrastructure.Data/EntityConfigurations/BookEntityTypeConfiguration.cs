using Library.Book.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Book.Infrastructure.Data.EntityConfigurations;

public class BookEntityTypeConfiguration : IEntityTypeConfiguration<BookItem>
{
    public void Configure(EntityTypeBuilder<BookItem> builder)
    {
        ArgumentNullException.ThrowIfNull(nameof(builder));

        builder.ToTable("BookItem");

        builder
           .HasKey(b => b.Id)
           .IsClustered(false)
           .HasName("PK_BookItem");

        builder.Property(b => b.Title)
            .HasColumnType("nvarchar(150)")
            .IsRequired(true);

        builder.Property(b => b.Author)
            .HasColumnType("nvarchar(100)")
            .IsRequired(true);

        builder.Property(b => b.Pages)
            .HasColumnType("int")
            .IsRequired(true);

        builder.Property(b => b.Publisher)
            .HasColumnType("nvarchar(300)")
            .IsRequired(true);

        builder.HasOne(b => b.Category)
           .WithMany(p => p.Books)
           .HasForeignKey(b => b.CategoryId)
           .IsRequired()
           .HasConstraintName("FK_Category")
           .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(b => b.ImageUrl)
           .HasColumnType("nvarchar(300)")
           .IsRequired(true);

        builder.Property(b => b.IsAvailable)
           .IsRequired(true)
           .HasDefaultValue(false);
    }
}
