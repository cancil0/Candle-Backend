using Candle.DataAccess.Abstract;
using Candle.InfraStructure.Persistence;

namespace Candle.DataAccess.Service
{
    public class FileDalService:IFileDal
    {
        private readonly CandleDbContext dbContext;
        public FileDalService(CandleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int GetIndexByUserName()
        {
            return 0;
        }
    }
}
