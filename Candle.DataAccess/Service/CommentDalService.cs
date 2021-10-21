using Candle.DataAccess.Abstract;
using Candle.InfraStructure.Persistence;
using Candle.InfraStructure.Persistence.EntityFramework;
using Candle.Model.Entities;

namespace Candle.DataAccess.Service
{
    public class CommentDalService : EfRepository<Comment>, ICommentDal
    {
        public CommentDalService(CandleDbContext contextDb) : base(contextDb)
        {
        }
    }
}
