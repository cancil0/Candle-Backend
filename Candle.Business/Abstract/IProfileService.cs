using Candle.Common.Result;
using Candle.Model.DTOs.ResponseDto.ProfileResponseDto;

namespace Candle.Business.Abstract
{
    public interface IProfileService
    {
        IDataResult<GetProfileCountDto> GetProfileCount(string userName);
    }
}
