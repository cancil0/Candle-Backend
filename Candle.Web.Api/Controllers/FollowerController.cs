using Candle.Application.System;
using Candle.Business.Abstract;
using Candle.Business.Service;
using Candle.Common.Result;
using Candle.Model.DTOs.RequestDto.Follower;
using Candle.Model.DTOs.ResponseDto.FollowerResponseDto;
using Candle.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Candle.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class FollowerController : BaseController
    {
        private readonly IFollowerService _followerService;
        public FollowerController()
        {
            _followerService = new FollowerService();
        }

        /// <summary>
        /// Get All Followers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetFollowers/{userName}")]
        public ActionResult<IDataResult<IQueryable<GetFollowerResponseDto>>> GetFollowers([FromRoute] string userName)
        {
            var getFollowers = _followerService.GetFollowers(userName);
            return Ok(getFollowers);
        }

        /// <summary>
        /// Get All Followings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetFollowings/{userName}")]
        public ActionResult<IDataResult<IQueryable<User>>> GetFollowings([FromRoute] string userName)
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
        /// Get Following List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetFollowingList/{userName}")]
        public ActionResult<IDataResult<List<GetFollowerResponseDto>>> GetFollowingList([FromRoute] string userName)
        {
            var getFollowings = _followerService.GetFollowingList(userName);
            return Ok(getFollowings);
        }

        /// <summary>
        /// Get Follower List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetFollowerList/{userName}")]
        public ActionResult<IDataResult<List<GetFollowerResponseDto>>> GetFollowerList([FromRoute] string userName)
        {
            var getFollowings = _followerService.GetFollowerList(userName);
            return Ok(getFollowings);
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
        /// Stop Following
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("StopFollowing/{id}")]
        public ActionResult<IResult> StopFollowingById([FromRoute] Guid id)
        {
            var stopFollowing = _followerService.StopFollowingById(id);
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
