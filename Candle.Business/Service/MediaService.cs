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
            var media = mediaDal.GetMany(x => x.Post.User.UserName == userName);
            bool isIndex = media.Select(x => x.Index).Any();
            if (isIndex)
            {
                return media.Max(x => x.Index);
            }

            return 0;
        }
    }
}
