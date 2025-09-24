using BaMinimalTemplate.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaMinimalTemplate.Data;

public class UserCategoryConfiguration : IEntityTypeConfiguration<UserCategory>
{
    public void Configure(EntityTypeBuilder<UserCategory> builder)
    {
        // Key (BaseEntity Id varsa yine de açıkça yazmak iyi olur)
        builder.HasKey(x => x.Id);

        // Index: aynı kullanıcı aynı kategoriyi 2. kez ekleyemesin
        builder.HasIndex(x => new { x.UserId, x.CategoryId }).IsUnique();

        // User -> UserCategory (Required)
        builder.HasOne(x => x.User)
            .WithMany(u => u.UserCategories)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(); // User zorunlu

        // Category -> UserCategory (Optional)  <-- uyarıyı çözer
        builder.HasOne(x => x.Category)
            .WithMany(c => c.UserCategories)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false); // Category opsiyonel (Category'de global filter var diye)
    }
}