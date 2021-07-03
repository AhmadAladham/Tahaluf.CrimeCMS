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
    [Authorize]
    public class StationsController : ControllerBase
    {
        private readonly IStationService _stationService;

        public StationsController(IStationService stationService)
        {
            _stationService = stationService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Station>), StatusCodes.Status200OK)]
        [Permission(Permissions.ListStations)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _stationService.GetAll();
            return Ok(result);
        }   

        [HttpGet]
        [ProducesResponseType(typeof(Station), StatusCodes.Status200OK)]
        [Route("{id}")]
        [Permission(Permissions.ViewStation)]
        public async Task<IActionResult> GetStationById(int id)
        {
            var result = await _stationService.GetById(id);
            return Ok(result.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Permission(Permissions.AddStation)]
        public async Task<IActionResult> Create([FromBody] Station station)
        {
            var result = await _stationService.Create(station);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Station), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Permission(Permissions.EditStation)]
        public async Task<IActionResult> Edit([FromBody] Station station)
        {
            var result = await _stationService.Edit(station);
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        [Permission(Permissions.DeleteStation)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _stationService.Delete(id);
            return Ok(result);
        }
    }
}
