using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelApp.DataAccess.Models;

namespace TravelApp.DataAccess.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");
        builder.HasKey(r => new {r.UserId, r.AttractionId});
        
        builder.Property(r => r.Rating).HasPrecision(2,1).IsRequired();
        builder.Property(r => r.Title).IsRequired().HasMaxLength(50);
        builder.Property(r => r.Content).IsRequired().HasMaxLength(300);
        
        builder.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId);
        
        builder.HasOne(r => r.Attraction)
            .WithMany(a => a.Reviews)
            .HasForeignKey(r => r.AttractionId);
    }
}