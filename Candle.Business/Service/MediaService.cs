using Candle.Business.Abstract;
using Candle.DataAccess.Abstract;
using Candle.DataAccess.Service;
using Candle.InfraStructure.Persistence;
using System.Linq;

namespace Candle.Business.Service
{
    public class MediaService : IMediaService
    {
        private readonly IMediaDal mediaDal;
        private readonly CandleDbContext dbContext;
        public MediaService()
        {
            dbContext = new CandleDbContext();
            mediaDal = new MediaDalService(dbContext);
        }

        public int GetMediaMaxIndex(string userName)
        {
            return mediaDal.GetMany(x => x.Post.User.UserName == userName).Max(x => x.Index);
        }
    }
}
