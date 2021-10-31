using Candle.DataAccess.Abstract;
using Candle.InfraStructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candle.DataAccess.Service
{
    public class FileDalService:IFileDal
    {
        private readonly CandleDbContext dbContext;
        public FileDalService()
        {
            dbContext = new CandleDbContext();
        }

        public int GetIndexByUserName()
        {
            return 0;
        }
    }
}
