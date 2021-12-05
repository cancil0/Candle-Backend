using Candle.Model.DTOs.RequestDto.Login;
using Candle.Model.Entities;
using System;

namespace Candle.DataAccess.Abstract
{
    public interface IPinDal
    {
        void RemoveLastPin(Guid userId);

        void AddNewPin(PinForgotPassword pinForgotPassword);

        void GeneratePinMessageFile(User user, string randomNumber);

        bool IsPinCorrect(EnterPinForgotPassRequestDto enterPinForgot);
    }
}
