using CrimeFile.API.Attributes;
using CrimeFile.API.Utility;
using CrimeFile.Core.DTOs;
using CrimeFile.Core.Entities;
using CrimeFile.Core.Security;
using CrimeFile.Core.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace CrimeFile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class ComplaintsController : ControllerBase
    {
        private readonly IComplaintService _complaintService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ComplaintsController(IComplaintService complaintService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _complaintService = complaintService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<AllComplaintsDTO>), StatusCodes.Status200OK)]
        //[Permission(Permissions.List)]
        public async Task<IActionResult> GetAll([FromQuery] ComplaintParameter complaintParameter)
        {
            var crimes = await _complaintService.GetAllPaged(complaintParameter);
            var metadata = new
            {
                crimes.Data.TotalCount,
                crimes.Data.PageSize,
                crimes.Data.CurrentPage,
                crimes.Data.TotalPages,
                crimes.Data.HasNext,
                crimes.Data.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(crimes);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Complaint), StatusCodes.Status200OK)]
        [Route("{id}")]
        //[Permission(Permissions.List)]
        public async Task<IActionResult> GetComplaintById(int id)
        {
            var result = await _complaintService.GetById(id);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Complaint), StatusCodes.Status200OK)]
        [Route("User/{id}")]
        //[Permission(Permissions.)]
        public async Task<IActionResult> GetComplaintByUserId()
        {
            var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var token = SecurityUtility.DecodeToken(authorization);
            int userId = Convert.ToInt32(token.Claims.First(c => c.Type == "UserId").Value);
            var result = await _complaintService.GetByUserId(userId);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Complaint), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Complaint complaint)
        {
            var result = await _complaintService.Create(complaint);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Complaint), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit([FromBody] Complaint complaint)
        {
            var result = await _complaintService.Edit(complaint);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _complaintService.Delete(id);
            return Ok(result);
        }
    }
}
