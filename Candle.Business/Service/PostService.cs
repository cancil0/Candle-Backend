using Candle.Business.Abstract;
using Candle.DataAccess.Abstract;
using Candle.DataAccess.Service;
using Candle.InfraStructure.Persistence;

namespace Candle.Business.Service
{
    public class PostService : IPostService
    {
        private readonly IPostDal postDal;
        private readonly CandleDbContext dbContext;
        public PostService()
        {
            dbContext = new CandleDbContext();
            postDal = new PostDalService(dbContext);
        }

    }
}
