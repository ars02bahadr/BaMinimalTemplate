using BaMinimalTemplate.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaMinimalTemplate.Data;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<UserProfile>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User - UserType Many-to-One relationship
        builder
            .HasOne(u => u.UserType)
            .WithMany(ut => ut.Users)
            .HasForeignKey(u => u.UserTypeId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}