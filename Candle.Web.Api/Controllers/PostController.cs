using Candle.Business.Abstract;
using Candle.Business.Service;
using Candle.Common.Result;
using Candle.Model.DTOs.RequestDto.Post;
using Candle.Model.DTOs.ResponseDto.PostResponseDto;
using Candle.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Candle.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        IPostService _postService;
        public PostController()
        {
            _postService = new PostService();
        }

        /// <summary>
        /// Herhangi bir post açılmak istendiği zaman bu method çağrılacak
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPost/{id}")]
        public ActionResult<IDataResult<Post>> Get([FromRoute] Guid id)
        {
            var getPost = _postService.GetById(id);
            return Ok(getPost);
        }

        /// <summary>
        /// Profile girildiği zaman bu method çağrılacak
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetByUserName")]
        public ActionResult<IQueryable<GetPostResponseDto>> GetByUserName(GetPostByUserNameDto getPostByUserNameDto)
        {
            var getPost = _postService.GetByUserName(getPostByUserNameDto);
            return Ok(getPost);
        }

        /// <summary>
        /// Ana sayfada postları göstermek için bu method çağrılacak
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMainPost/{userName}")]
        public ActionResult<IDataResult<IQueryable<GetPostResponseDto>>> GetMainPost([FromRoute] string userName)
        {
            var getPosts = _postService.GetMainPost(userName);
            return Ok(getPosts);
        }

        /// <summary>
        /// Ana sayfada postları göstermek için bu method çağrılacak
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDiscoveryPost/{userName}")]
        public ActionResult<IDataResult<IQueryable<GetPostResponseDto>>> GetDiscoveryPost([FromRoute] string userName)
        {
            var getPosts = _postService.GetDiscoveryPost(userName);
            return Ok(getPosts);
        }

        /// <summary>
        /// Post eklemek için çağrılacak
        /// </summary>
        /// <param name="addPostDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddPost")]
        public ActionResult<IDataResult<Post>> AddPost([FromBody] AddPostDto addPostDto)
        {
            var addPost = _postService.AddPost(addPostDto);
            return Ok(addPost);
        }
    }
}
