using Candle.InfraStructure.Persistence;
using Candle.Model.Entities;

namespace Candle.DataAccess.Abstract
{
    public interface IMessageDal : IRepository<Message> 
    {
    }
}
