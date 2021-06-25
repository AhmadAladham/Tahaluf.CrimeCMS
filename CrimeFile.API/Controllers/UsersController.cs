using CrimeFile.Core.DTOs;
using CrimeFile.Core.Entities;
using CrimeFile.Core.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CrimeFile.API.Controllers
{
    [Route("api/[controller]")]// /[action]
    [ApiController]
    [EnableCors]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        

        public UsersController(IUserService userService, IEmailService emailService )
        {
            _userService = userService;
            _emailService = emailService;
           
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedList<UserDTO>), StatusCodes.Status200OK)]
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
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var data = await _userService.Register(registerDTO);
            await _emailService.SendVerificationCode(data.Email, data.CodeGenerated);
            return Ok(data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(GenerateCodeDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("ForgotPassword")]
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
        public async Task<IActionResult> SignIn([FromBody] SignInDTO signInDTO)
        {
            var result = await _userService.SignIn(signInDTO);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody] EditUserDTO editUserDTO)
        {
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
    }
}