using Candle.Application.System;
using Candle.Business.Abstract;
using Candle.Business.Service;
using Candle.Common.Result;
using Candle.Model.DTOs.RequestDto.Like;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Candle.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : BaseController
    {
        private readonly ILikeService _likeService;
        public LikeController()
        {
            _likeService = new LikeService();
        }

        [HttpPost]
        [Route("LikePost")]
        public ActionResult<IResult> LikePost([FromBody] LikePostRequestDto likePostRequest)
        {
            var like = _likeService.LikePost(likePostRequest);
            return Ok(like);
        }

        [HttpPost]
        [Route("StopLikePost")]
        public ActionResult<IResult> StopLikePost([FromBody] LikePostRequestDto likePostRequest)
        {
            var like = _likeService.StopLikePost(likePostRequest);
            return Ok(like);
        }

        [HttpGet]
        [Route("GetWhoLikedPost")]
        public ActionResult<IDataResult<List<string>>> GetWhoLikedPost([FromQuery] Guid postId)
        {
            var like = _likeService.GetWhoLikedPost(postId);
            return Ok(like);
        }
    }
}
