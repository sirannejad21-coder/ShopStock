using ShopStock.Domain.Contracts;
using ShopStock.Domain.Models.Prouducts;
using ShopStock.Infra.Data.Context;

namespace ShopStock.Infra.Data.Repository
{
    public class ProuductFeatureRepository(EshopDbContext _Contex)
        : GenericRepository<ProuductFeature>(_Contex), IProuductFeatureRepository
    {



    }
}