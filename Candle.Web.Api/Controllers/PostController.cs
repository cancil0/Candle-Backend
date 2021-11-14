using Candle.Application.System;
using Candle.Business.Abstract;
using Candle.Business.Service;
using Candle.Common.Result;
using Candle.Model.DTOs.RequestDto.Post;
using Candle.Model.DTOs.ResponseDto.PostResponseDto;
using Candle.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Candle.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : BaseController
    {
        IPostService _postService;
        public PostController()
        {
            _postService = new PostService();
        }

        /// <summary>
        /// Get Post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPost/{id}")]
        public ActionResult<IDataResult<GetPostResponseDto>> Get([FromRoute] Guid id)
        {
            var getPost = _postService.GetById(id);
            return Ok(getPost);
        }

        /// <summary>
        /// Get Spesific User's Post
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetByUserName")]
        public ActionResult<IQueryable<GetPostResponseDto>> GetByUserName([FromBody] GetPostByUserNameDto getPostByUserNameDto)
        {
            var getPost = _postService.GetByUserName(getPostByUserNameDto);
            return Ok(getPost);
        }

        /// <summary>
        /// Get Main Page Posts
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMainPost")]
        public ActionResult<IDataResult<List<GetPostResponseDto>>> GetMainPost([FromBody] GetPostByUserNameDto getPostByUserNameDto)
        {
            var getPosts = _postService.GetMainPost(getPostByUserNameDto);
            return Ok(getPosts);
        }

        /// <summary>
        /// Get Discovery Page Posts
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDiscoveryPost")]
        public ActionResult<IDataResult<IQueryable<GetPostResponseDto>>> GetDiscoveryPost([FromBody] GetPostByUserNameDto getPostByUserNameDto)
        {
            var getPosts = _postService.GetDiscoveryPost(getPostByUserNameDto);
            return Ok(getPosts);
        }

        /// <summary>
        /// Add Post
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

        /// <summary>
        /// Delete Post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeletePost/{id}")]
        public ActionResult<IResult> DeletePost([FromRoute] Guid id)
        {
            var deletedPost = _postService.DeletePost(id);
            return Ok(deletedPost);
        }
    }
}
