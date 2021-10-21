using Candle.DataAccess.Abstract;
using Candle.InfraStructure.Persistence;
using Candle.InfraStructure.Persistence.EntityFramework;
using Candle.Model.Entities;

namespace Candle.DataAccess.Service
{
    public class TagDalService : EfRepository<Tag>, ITagDal
    {
        public TagDalService(CandleDbContext contextDb) : base(contextDb)
        {
        }
    }
}
