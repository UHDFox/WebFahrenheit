using Domain;
using Domain.Domain.Entities.Products;

namespace Repository.WaterBoiler;

internal sealed class WaterBoilerRepository : Repository<WaterBoilerRecord>, IWaterBoilerRepository
{
    public WaterBoilerRepository(FahrenheitContext context) : base(context)
    {
    }
}