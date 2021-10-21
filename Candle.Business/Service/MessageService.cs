using Candle.Business.Abstract;
using Candle.DataAccess.Abstract;

namespace Candle.Business.Service
{
    public class MessageService : IMessageService
    {
        private readonly IMessageDal messageDal;

        public MessageService(IMessageDal messageDal)
        {
            this.messageDal = messageDal;
        }
    }
}
