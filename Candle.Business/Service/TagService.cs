using Candle.Business.Abstract;
using Candle.DataAccess.Abstract;

namespace Candle.Business.Service
{
    public class TagService : ITagService
    {
        private readonly ITagDal tagDal;

        public TagService(ITagDal tagDal)
        {
            this.tagDal = tagDal;
        }
    }
}
