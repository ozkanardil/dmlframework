using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DmlFramework.Domain.Entities;

namespace DmlFramework.Persistance.Configurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Role).IsRequired().HasMaxLength(50);
        }
    }
}
