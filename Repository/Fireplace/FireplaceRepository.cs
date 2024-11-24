using Domain;
using Domain.Domain.Entities.Products;

namespace Repository.Fireplace;

internal sealed class FireplaceRepository : Repository<FireplaceRecord>, IFireplaceRepository
{
    public FireplaceRepository(FahrenheitContext context) : base(context)
    {
    }
}
