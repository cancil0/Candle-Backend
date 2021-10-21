using Candle.Business.Abstract;
using Candle.Business.Service;
using Candle.Common.Result;
using Candle.Model.DTOs.RequestDto.Login;
using Candle.Model.DTOs.RequestDto.User;
using Candle.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Candle.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        IUserService _userService;
        public UserController()
        {
            _userService = new UserService();
        }

        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")]
        public ActionResult<IDataResult<User>> Get([FromRoute] Guid id)
        {
            var getUser = _userService.GetById(id);
            return Ok(getUser);
        }

        /// <summary>
        /// Get All User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public ActionResult<IDataResult<IQueryable<User>>> GetAll()
        {
            var getUsers = _userService.GetAll();
            return Ok(getUsers);
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public ActionResult<IResult> UpdateUser([FromBody] UserRequestDto user)
        {
            var updateUser = _userService.UpdateUser(user);
            return Ok(updateUser);
        }

        /// <summary>
        /// Delete user by set status 0
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        public ActionResult<IResult> DeleteUser([FromBody] UserLoginDto user)
        {
            var deleteUser = _userService.DeleteUser(user);
            return Ok(deleteUser);
        }

        /// <summary>
        /// Activate User by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ActivateUser/{id}")]
        public ActionResult<IResult> ActivateUser([FromRoute] Guid id)
        {
            var activateUser = _userService.ActivateUser(id);
            return Ok(activateUser);
        }
    }
}
