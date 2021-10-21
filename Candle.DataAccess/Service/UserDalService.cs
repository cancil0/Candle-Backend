using Candle.DataAccess.Abstract;
using Candle.InfraStructure.Persistence;
using Candle.InfraStructure.Persistence.EntityFramework;
using Candle.Model.Entities;

namespace Candle.DataAccess.Service
{
    public class UserDalService : EfRepository<User>, IUserDal
    {
        public UserDalService(CandleDbContext contextDb):base(contextDb)
        {
        }
    }
}
