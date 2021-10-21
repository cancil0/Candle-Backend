using Candle.DataAccess.Abstract;
using Candle.InfraStructure.Persistence;
using Candle.InfraStructure.Persistence.EntityFramework;
using Candle.Model.Entities;

namespace Candle.DataAccess.Service
{
    public class PostDalService : EfRepository<Post>, IPostDal
    {
        public PostDalService(CandleDbContext contextDb) : base(contextDb)
        {
        }
    }
}
