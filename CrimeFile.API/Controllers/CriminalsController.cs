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
        public async Task<IActionResult> GetAll()
        {
            var result = await _criminalService.GetAll();
            return Ok(result.Data);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Station), StatusCodes.Status200OK)]
        [Route("{id}")]
        //[Permission(Permissions.List)]
        public async Task<IActionResult> GetStationById(int id)
        {
            var result = await _criminalService.GetById(id);
            return Ok(result.Data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Criminal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Criminal criminal)
        {
            var result = await _criminalService.Create(criminal);
            return Ok(result.Data);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Criminal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit([FromBody] Criminal criminal)
        {
            var result = await _criminalService.Edit(criminal);
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _criminalService.Delete(id);
            return Ok(result.Data);
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
