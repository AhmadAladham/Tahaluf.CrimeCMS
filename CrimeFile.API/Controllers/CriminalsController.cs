using CrimeFile.Core.DTOs;
using CrimeFile.Core.Entities;
using CrimeFile.Core.Services;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors]
    public class CriminalsController : ControllerBase
    {
        private readonly ICriminalService _criminalService;

        public CriminalsController(ICriminalService criminalService)
        {
            _criminalService = criminalService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Criminal>), StatusCodes.Status200OK)]
        //[Permission(Permissions.List)]
        public async Task<IActionResult> GetAll([FromQuery] CriminalParameters criminalParameters)
        {
            var criminals = await _criminalService.GetAllPaged(criminalParameters);
            var metadata = new
            {
                criminals.Data.TotalCount,
                criminals.Data.PageSize,
                criminals.Data.CurrentPage,
                criminals.Data.TotalPages,
                criminals.Data.HasNext,
                criminals.Data.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(criminals);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Criminal), StatusCodes.Status200OK)]
        [Route("{id}")]
        //[Permission(Permissions.List)]
        public async Task<IActionResult> GetCriminalById(int id)
        {
            var result = await _criminalService.GetById(id);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(AllCriminalsDTO), StatusCodes.Status200OK)]
        [Route("NationalNumber/{nationalNumber}")]
        //[Permission(Permissions.List)]
        public async Task<IActionResult> GetCriminalByNationalNumber(string nationalNumber)
        {
            var result = await _criminalService.GetByNationalNumber(nationalNumber);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Criminal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Criminal criminal)
        {
            var result = await _criminalService.Create(criminal);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Criminal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit([FromBody] Criminal criminal)
        {
            var result = await _criminalService.Edit(criminal);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _criminalService.Delete(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Criminal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CriminalSearch")]
        public async Task<IActionResult> Search([FromBody] CriminalDto criminal)
        {
            var result = await _criminalService.Search(criminal);
            return Ok(result);
        }
    }
}
