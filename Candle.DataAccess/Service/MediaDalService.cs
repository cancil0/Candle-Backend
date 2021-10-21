using Candle.DataAccess.Abstract;
using Candle.InfraStructure.Persistence;
using Candle.InfraStructure.Persistence.EntityFramework;
using Candle.Model.Entities;

namespace Candle.DataAccess.Service
{
    public class MediaDalService : EfRepository<Media>, IMediaDal
    {
        public MediaDalService(CandleDbContext contextDb) : base(contextDb)
        {
        }
    }
}
