using Candle.Business.Abstract;
using Candle.DataAccess.Abstract;

namespace Candle.Business.Service
{
    public class MediaService : IMediaService
    {
        private readonly IMediaDal mediaDal;

        public MediaService(IMediaDal mediaDal)
        {
            this.mediaDal = mediaDal;
        }
    }
}
