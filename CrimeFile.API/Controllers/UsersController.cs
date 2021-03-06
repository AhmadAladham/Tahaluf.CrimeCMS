using CrimeFile.API.Attributes;
using CrimeFile.API.Utility;
using CrimeFile.Core.DTOs;
using CrimeFile.Core.Entities;
using CrimeFile.Core.Permissions;
using CrimeFile.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CrimeFile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    [Authorize]
    
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsersController(IUserService userService, IEmailService emailService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;

        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedList<UserDTO>), StatusCodes.Status200OK)]
        [Permission(Permissions.ListUsers)]

        public async Task<IActionResult> GetAll([FromQuery] UserParameters userParameters)
        {
            var users = await _userService.GetAllPaged(userParameters);
            var metadata = new
            {
                users.Data.TotalCount,
                users.Data.PageSize,
                users.Data.CurrentPage, 
                users.Data.TotalPages,
                users.Data.HasNext,
                users.Data.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            
            return Ok(users);
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegisterResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var result = await _userService.Register(registerDTO);
            await _emailService.SendVerificationCode(result.Data.Email, result.Data.CodeGenerated);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(GenerateCodeDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDTO)
        {
            var result = await _userService.ForgotPassword(forgotPasswordDTO);
            await _emailService.SendVerificationCode(result.Data.Email, result.Data.CodeGenerated); 
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(GenerateCodeDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("ResetPassword")]

        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            var result = await _userService.ResetPassword(resetPasswordDTO);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var token = SecurityUtility.DecodeToken(authorization);
            changePasswordDTO.UserId = Convert.ToInt32(token.Claims.First(c => c.Type == "UserId").Value);
            var result = await _userService.ChangePassword(changePasswordDTO);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail([FromBody] EmailVerificationDTO emailVerification)
        {
            var result = await _userService.VerifyEmail(emailVerification);
            return Ok(result);
        }



        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("SignIn")]
        [AllowAnonymous]
        
        public async Task<IActionResult> SignIn([FromBody] SignInDTO signInDTO)
        {
            var result = await _userService.SignIn(signInDTO);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("Edit")]
        [Permission(Permissions.EditUser)]

        public async Task<IActionResult> Edit([FromBody] EditUserDTO editUserDTO)
        {
            var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var token = SecurityUtility.DecodeToken(authorization);
            editUserDTO.UserId = Convert.ToInt32(token.Claims.First(c => c.Type == "UserId").Value);
            var result = await _userService.Edit(editUserDTO);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("UserSearch")]
        [Permission(Permissions.SearchUser)]
        public async Task<IActionResult> Search([FromBody] SearchUserDTO searchUserDTO)
        {
            var users = await _userService.Search(searchUserDTO);
            if (users.Data != null)
            {
                var metadata = new
                {
                    users.Data.TotalCount,
                    users.Data.PageSize,
                    users.Data.CurrentPage,
                    users.Data.TotalPages,
                    users.Data.HasNext,
                    users.Data.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            }

            return Ok(users);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var oldToken = SecurityUtility.DecodeToken(authorization);
            var userId = Convert.ToInt32(oldToken.Claims.First(c => c.Type == "UserId").Value);
            var data = await _userService.RefreshToken(userId);
            return Ok(data);
        }
    }
}