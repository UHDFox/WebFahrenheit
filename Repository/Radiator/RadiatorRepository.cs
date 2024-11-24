using Domain;
using Domain.Domain.Entities.Products;

namespace Repository.Radiator;

internal sealed class RadiatorRepository : Repository<RadiatorRecord>, IRadiatorRepository
{
    public RadiatorRepository(FahrenheitContext context) : base(context)
    {
    }
}