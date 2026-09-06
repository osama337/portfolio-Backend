using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Portfolio.DAL.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(x => x.Title)
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(x => x.Description)
               .HasMaxLength(1000)
               .IsRequired();

        builder.Property(x => x.GithubUrl)
               .HasMaxLength(500);

        builder.Property(x => x.LiveDemoUrl)
               .HasMaxLength(500);

        builder.Property(x => x.ImagePath)
               .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(x => x.ApplicationUser)
               .WithMany(x => x.Projects)
               .HasForeignKey(x => x.ApplicationUserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}