using Domain;
using Domain.Domain.Entities.Products;

namespace Repository.Pump;

internal sealed class PumpRepository : Repository<PumpRecord>, IPumpRepository
{
    public PumpRepository(FahrenheitContext context) : base(context)
    {
    }
}