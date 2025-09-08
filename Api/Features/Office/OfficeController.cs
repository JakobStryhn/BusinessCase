using Api.Features.Office.Models;
using Core.Domain.Models;
using Core.Features.Office.DataTransferObjects;
using Core.Features.Office.Interfaces;
using Core.Features.Office.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Office
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController(IOfficeService officeService) : ControllerBase
    {
        private readonly IOfficeService _officeService = officeService;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OfficeEntity>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _officeService.GetOffices();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OfficeEntity), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateOfficeRequest request)
        {
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _officeService.CreateOffice(request);
                return CreatedAtAction("Post", result);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EmployeeEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateOfficeRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest("ID in URL and request body do not match.");
            }

            var result = await _officeService.UpdateOffice(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _officeService.DeleteOffice(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
