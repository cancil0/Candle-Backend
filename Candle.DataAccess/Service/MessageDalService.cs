using Candle.DataAccess.Abstract;
using Candle.InfraStructure.Persistence;
using Candle.InfraStructure.Persistence.EntityFramework;
using Candle.Model.Entities;

namespace Candle.DataAccess.Service
{
    class MessageDalService : EfRepository<Message>, IMessageDal
    {
        public MessageDalService(CandleDbContext contextDb) : base(contextDb)
        {
        }
    }
}
