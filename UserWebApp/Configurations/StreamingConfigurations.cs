using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserWebApp.Extensions;
using UserWebApp.Models;

namespace UserWebApp.Configurations
{
    public class StreamingConfigurations : IEntityTypeConfiguration<Streaming>
    {
        public void Configure(EntityTypeBuilder<Streaming> builder)
        {
            builder.Property(b => b.StreamingId)
                .IsRequired();

            builder.Property(b => b.Channel)
                .IsRequired();

            builder.Property(b => b.Description)
                .HasMaxLength(255);

            builder.Property(b => b.Detail)
                .HasJsonConversion();
        }
    }
}
