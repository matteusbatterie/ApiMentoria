using Core.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration.Tables
{
    public class UserEntityConfiguration : BaseTableConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.Name)
                .IsRequired();
            builder.Property(user => user.Email)
                .IsRequired();
            builder.Property(user => user.Password)
                .IsRequired();
        }
    }
}
