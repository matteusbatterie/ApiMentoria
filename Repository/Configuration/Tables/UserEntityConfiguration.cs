using Core.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration.Tables
{
    public class UserEntityConfiguration : BaseTableConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired();
            builder.Property(x => x.Age)
                .IsRequired();
        }
    }
}
