using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desk.Domain.Entities;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder
            .ToTable(@"Project")
            .HasKey(x => x.Id)
            .HasName("PK_Project_Id");

        builder
            .Property(x => x.Id)
            .HasColumnName(@"Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.Name)
            .HasColumnName(@"Name")
            .HasColumnType("nvarchar")
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(x => x.Description)
            .HasColumnName(@"Description")
            .HasColumnType("nvarchar")
            .HasMaxLength(1024)
            .IsRequired(false);
    }
}