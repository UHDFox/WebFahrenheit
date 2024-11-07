using Domain.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Domain.EntitiesConfiguration;

public class ClientsConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(client => client.Id);

        builder.HasMany(c => c.Feedbacks)
            .WithOne(f => f.Client);
    }
}