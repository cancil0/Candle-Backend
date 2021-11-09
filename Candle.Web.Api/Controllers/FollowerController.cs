using Candle.Application.System;
using Candle.Business.Abstract;
using Candle.Business.Service;
using Candle.Common.Result;
using Candle.Model.DTOs.RequestDto.Follower;
using Candle.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Candle.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class FollowerController : BaseController
    {
        IFollowerService _followerService;
        public FollowerController()
        {
            _followerService = new FollowerService();
        }

        /// <summary>
        /// Get All Followers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetFollowers")]
        public ActionResult<IDataResult<IQueryable<Follower>>> GetFollowers([FromQuery] string userName)
        {
            var getFollowers = _followerService.GetFollowers(userName);
            return Ok(getFollowers);
        }

        /// <summary>
        /// Get All Followings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetFollowings")]
        public ActionResult<IDataResult<IQueryable<Follower>>> GetFollowings([FromQuery] string userName)
        {
            var getFollowings = _followerService.GetFollowings(userName);
            return Ok(getFollowings);
        }

        /// <summary>
        /// Get 50 People Who Not Followings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetNotFollowings")]
        public ActionResult<IDataResult<IQueryable<Follower>>> GetNotFollowings([FromQuery] string userName)
        {
            var getNotFollowings = _followerService.GetNotFollowings(userName);
            return Ok(getNotFollowings);
        }

        /// <summary>
        /// Stop Following
        /// </summary>
        /// <param name="followerRequestDto"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("StopFollowing")]
        public ActionResult<IResult> StopFollowing([FromBody] FollowerRequestDto followerRequestDto)
        {
            var stopFollowing = _followerService.StopFollowing(followerRequestDto);
            return Ok(stopFollowing);
        }

        /// <summary>
        /// Follow
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Follow")]
        public ActionResult<IResult> Follow(FollowerRequestDto followerRequestDto)
        {
            var follow = _followerService.Follow(followerRequestDto);
            return Ok(follow);
        }
    }
}
