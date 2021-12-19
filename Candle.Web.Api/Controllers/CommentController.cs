using Candle.Application.System;
using Candle.Business.Abstract;
using Candle.Business.Service;
using Candle.Common.Result;
using Candle.Model.DTOs.RequestDto.Comment;
using Candle.Model.DTOs.ResponseDto.CommentResponseDto;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Candle.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : BaseController
    {
        private readonly ICommentService commentService;
        public CommentController()
        {
            commentService = new CommentService();
        }

        /// <summary>
        /// Add New Comment to Post
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AddComment")]
        public ActionResult<DataResult<AddCommentResponseDto>> AddComment([FromBody]  AddCommentRequestDto commentRequestDto)
        {
            var addNewComment = commentService.AddComment(commentRequestDto);
            return Ok(addNewComment);
        }

        /// <summary>
        /// Delete Comment from Post
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteComment/{id}")]
        public ActionResult<Result> DeleteComment([FromRoute] Guid id)
        {
            var stopFollowing = commentService.DeleteComment(id);
            return Ok(stopFollowing);
        }

        /// <summary>
        /// Get Post Comments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPostComments")]
        public ActionResult<Result> GetPostComments([FromQuery] Guid id)
        {
            var postComments = commentService.GetPostComments(id);
            return Ok(postComments);
        }
    }
}
