using CrimeFile.Core.DTOs;
using CrimeFile.Core.Entities;
using CrimeFile.Core.Services;
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
    public class CrimesController : ControllerBase
    {
        private readonly ICrimeService _crimeService;

        public CrimesController(ICrimeService crimeService)
        {
            _crimeService = crimeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Crime>), StatusCodes.Status200OK)]
        //[Permission(Permissions.List)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _crimeService.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Crime), StatusCodes.Status200OK)]
        [Route("{id}")]
        //[Permission(Permissions.List)]
        public async Task<IActionResult> GetStationById(int id)
        {
            var result = await _crimeService.GetById(id);
            return Ok(result.Data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Crime), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Crime crime)
        {
            var result = await _crimeService.Create(crime);
            return Ok(result.Data);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Crime), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit([FromBody] Crime crime)
        {
            var result = await _crimeService.Edit(crime);
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _crimeService.Delete(id);
            return Ok(result.Data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Crime), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CrimeSearch")]
        public async Task<IActionResult> Search([FromBody] CrimeDto crime)
        {
            var result = await _crimeService.Search(crime);
            return Ok(result);
        }
    }
}
