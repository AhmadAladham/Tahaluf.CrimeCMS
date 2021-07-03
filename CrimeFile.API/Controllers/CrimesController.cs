using CrimeFile.API.Attributes;
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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrimeFile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    [Authorize]
    public class CrimesController : ControllerBase
    {
        private readonly ICrimeService _crimeService;

        public CrimesController(ICrimeService crimeService)
        {
            _crimeService = crimeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Crime>), StatusCodes.Status200OK)]
        [Permission(Permissions.ListCrimes)]
        public async Task<IActionResult> GetAll([FromQuery] CrimeParameters crimeParameters)
        {
            var crimes = await _crimeService.GetAllPaged(crimeParameters);
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
        [ProducesResponseType(typeof(Crime), StatusCodes.Status200OK)]
        [Route("{id}")]
        [Permission(Permissions.ViewCrime)]
        public async Task<IActionResult> GetStationById(int id)
        {
            var result = await _crimeService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Crime), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Permission(Permissions.AddCrime)]
        public async Task<IActionResult> Create([FromBody] Crime crime)
        {
            var result = await _crimeService.Create(crime);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Crime), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Permission(Permissions.EditCrime)]
        public async Task<IActionResult> Edit([FromBody] Crime crime)
        {
            var result = await _crimeService.Edit(crime);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Permission(Permissions.DeleteCrime)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _crimeService.Delete(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Complaint), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CrimeSearch")]
        [Permission(Permissions.SearchCrime)]
        public async Task<IActionResult> Search([FromBody] CrimeDto crime)
        {
            var crimes = await _crimeService.Search(crime);
            if(crimes.Data != null)
            {
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
            }
                
            return Ok(crimes);
        }
    }
}
