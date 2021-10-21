using Candle.Business.Abstract;
using Candle.Business.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Candle.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PostController : ControllerBase
    {
        IPostService _postService;
        public PostController()
        {
            _postService = new PostService();
        }
    }
}
