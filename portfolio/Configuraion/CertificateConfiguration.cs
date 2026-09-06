using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Portfolio.DAL.Configurations;

public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
{
    public void Configure(EntityTypeBuilder<Certificate> builder)
    {
        builder.Property(x => x.Title)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(x => x.Issuer)
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(x => x.ImagePath)
               .HasMaxLength(500);

        builder.HasOne(x => x.ApplicationUser)
               .WithMany(x => x.Certificates)
               .HasForeignKey(x => x.ApplicationUserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}