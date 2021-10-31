using Candle.Business.Abstract;
using Candle.Business.Service;
using Candle.Common.Result;
using Candle.Model.DTOs.ResponseDto.FileResponseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Candle.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController()
        {
            _fileService = new FileService();
        }

        [HttpPost]
        [Route("UploadFile")]
        public ActionResult<IDataResult<List<UploadFileResponseDto>>> UploadFile(List<IFormFile> files, [FromQuery] string userName)
        {
            var uploadFile = _fileService.UploadFile(files, userName);
            return Ok(uploadFile);
        }
    }
}
