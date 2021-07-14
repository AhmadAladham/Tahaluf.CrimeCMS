using CrimeFile.API.Attributes;
using CrimeFile.Core.Entities;
using CrimeFile.Core.Permissions;
using CrimeFile.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrimeFile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRolesController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<UserRole>), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> GetAll()
        {
            var result = await _userRoleService.GetAll();
            return Ok(result);
        }
    }
}
