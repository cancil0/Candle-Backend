using Candle.DataAccess.Abstract;
using Candle.InfraStructure.Persistence;
using Candle.InfraStructure.Persistence.EntityFramework;
using Candle.Model.Entities;

namespace Candle.DataAccess.Service
{
    public class LikeDalService : EfRepository<Like>, ILikeDal
    {
        public LikeDalService(CandleDbContext contextDb) : base(contextDb)
        {
        }
    }
}
