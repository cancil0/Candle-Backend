using Candle.Common.Result;
using Candle.Model.DTOs.ResponseDto.FileResponseDto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Candle.Business.Abstract
{
    public interface IFileService
    {
        IDataResult<List<UploadFileResponseDto>>  UploadFile(List<IFormFile> file, string userName);
    }
}
