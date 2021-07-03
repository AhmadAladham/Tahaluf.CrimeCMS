using CrimeFile.API.Attributes;
using CrimeFile.Core.Entities;
using CrimeFile.Core.Permissions;
using CrimeFile.Core.Services;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class CrimeCategoriesController : ControllerBase
    {
        private readonly ICrimeCategoryService _crimeCategoryService;

        public CrimeCategoriesController(ICrimeCategoryService crimeCategoryService)
        {
            _crimeCategoryService = crimeCategoryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CrimeCategory>), StatusCodes.Status200OK)]
        [Permission(Permissions.ListCrimeCategories)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _crimeCategoryService.GetAll();
            return Ok(data);
        }

        [HttpGet]
        [ProducesResponseType(typeof(CrimeCategory), StatusCodes.Status200OK)]
        [Route("{id}")]
        [Permission(Permissions.ViewCrimeCategory)]
        public async Task<IActionResult> GetStationById(int id)
        {
            var data = await _crimeCategoryService.GetById(id);
            return Ok(data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CrimeCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Permission(Permissions.AddCrimeCategory)]
        public async Task<IActionResult> Create([FromBody] CrimeCategory crimeCategory)
        {
            var data = await _crimeCategoryService.Create(crimeCategory);
            return Ok(data);
        }

        [HttpPut]
        [ProducesResponseType(typeof(CrimeCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Permission(Permissions.EditCrimeCategory)]
        public async Task<IActionResult> Edit([FromBody] CrimeCategory crimeCategory)
        {
            var data = await _crimeCategoryService.Edit(crimeCategory);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        [Permission(Permissions.DeleteCrimeCategory)]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _crimeCategoryService.Delete(id);
            return Ok(data);
        }
    }
}
