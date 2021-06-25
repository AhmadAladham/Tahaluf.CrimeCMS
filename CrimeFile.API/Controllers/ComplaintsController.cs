using CrimeFile.Core.DTOs;
using CrimeFile.Core.Entities;
using CrimeFile.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrimeFile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        private readonly IComplaintService _complaintService;

        public ComplaintsController(IComplaintService complaintService)
        {
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
        public async Task<IActionResult> GetStationById(int id)
        {
            var data = await _complaintService.GetById(id);
            return Ok(data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Complaint), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Complaint complaint)
        {
            var data = await _complaintService.Create(complaint);
            return Ok(data);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Complaint), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit([FromBody] Complaint complaint)
        {
            var data = await _complaintService.Edit(complaint);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _complaintService.Delete(id);
            return Ok(data);
        }
    }
}
