using Candle.Business.Abstract;
using Candle.DataAccess.Abstract;

namespace Candle.Business.Service
{
    public class LikeService : ILikeService
    {
        private readonly ILikeDal likeDal;

        public LikeService(ILikeDal likeDal)
        {
            this.likeDal = likeDal;
        }
    }
}
