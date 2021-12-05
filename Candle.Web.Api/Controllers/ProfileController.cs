using Candle.Application.System;
using Candle.Business.Abstract;
using Candle.Business.Service;
using Candle.Common.Result;
using Candle.Model.DTOs.ResponseDto.ProfileResponseDto;
using Microsoft.AspNetCore.Mvc;

namespace Candle.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : BaseController
    {
        IProfileService _profileService;
        public ProfileController()
        {
            _profileService = new ProfileService();
        }

        [HttpGet]
        [Route("GetProfileInfos/{userName}")]
        public ActionResult<IDataResult<GetProfileInfoDto>> GetProfileInfos(string userName)
        {
            var counts = _profileService.GetProfileInfo(userName);
            return Ok(counts);
        }
    }
}
