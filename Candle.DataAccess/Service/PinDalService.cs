using Candle.DataAccess.Abstract;
using Candle.InfraStructure.Persistence;
using Candle.Model.DTOs.RequestDto.Login;
using Candle.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;

namespace Candle.DataAccess.Service
{
    public class PinDalService : IPinDal
    {
        private readonly CandleDbContext dbContext;
        private readonly DbSet<PinForgotPassword> entities;
        public PinDalService(CandleDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entities = dbContext.Set<PinForgotPassword>();
        }

        public void RemoveLastPin(Guid userId)
        {
            PinForgotPassword lastPin = entities.SingleOrDefault(x => x.UserId == userId);

            if (lastPin != null)
            {
                entities.Remove(lastPin);
                dbContext.SaveChanges();
            }
        }

        public void AddNewPin(PinForgotPassword pinForgotPassword)
        {
            entities.Add(pinForgotPassword);
            dbContext.SaveChanges();
        }

        public void GeneratePinMessageFile(User user, string randomNumber, GeneratePinFileResourcesDto fileResources)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            File.Create(Path.Combine(docPath, user.UserName + "-Pin.txt")).Close();
            using StreamWriter outputFile = new(Path.Combine(docPath, user.UserName + "-Pin.txt"), true);
            outputFile.WriteLine(string.Format("{0}:  {1}", fileResources.UserName, user.UserName));
            outputFile.WriteLine(string.Format("Pin: {0}", randomNumber));
            outputFile.WriteLine(string.Format("{0}:  {1}", fileResources.Date, DateTime.Now.ToString("dd.MM.yyyy")));
            outputFile.WriteLine(string.Format("{0}:  {1}", fileResources.Time, DateTime.Now.ToString("HH:mm")));
            outputFile.WriteLine(fileResources.SecondsLine);
            outputFile.WriteLine(fileResources.CautionLine);
        }

        public bool IsPinCorrect(EnterPinForgotPassRequestDto enterPinForgot)
        {
            PinForgotPassword userPinRequest = (from a in entities
                                                where a.User.UserName == enterPinForgot.UserName && a.Pin == enterPinForgot.Pin
                                                select a).SingleOrDefault();

            return userPinRequest != null;
        }
    }

}
