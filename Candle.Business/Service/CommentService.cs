using Candle.Business.Abstract;
using Candle.DataAccess.Abstract;

namespace Candle.Business.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentDal commentDal;

        public CommentService(ICommentDal commentDal)
        {
            this.commentDal = commentDal;
        }
    }
}
