using Library.Student.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Student.Infrastructure.Data.EntityConfigurations;

public class StudentEntityTypeConfiguration : IEntityTypeConfiguration<StudentItem>
{
    public void Configure(EntityTypeBuilder<StudentItem> builder)
    {
        ArgumentNullException.ThrowIfNull(nameof(builder));

        builder.ToTable("StudentItem");

        builder
            .HasKey(c => c.Id)
            .IsClustered(false)
            .HasName("PK_StudentItem");

        builder.Property(c => c.Name)
            .HasColumnType("nvarchar(150)")
            .IsRequired(true);

        builder.Property(c => c.CourseId)
            .HasColumnType("uniqueidentifier")
            .IsRequired(true);
    }
}
