using Core.Domain.Models;
using Core.Features.Employee.DataTransferObjects;
using Core.Features.Employee.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Employee
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeService employeeService, IValidator<CreateEmployeeRequest> createValidator,
        IValidator<UpdateEmployeeRequest> updateValidator) : ControllerBase
    {
        private readonly IEmployeeService _employeeService = employeeService;
        private readonly IValidator<CreateEmployeeRequest> _createValidator = createValidator;
        private readonly IValidator<UpdateEmployeeRequest> _updateValidator = updateValidator;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EmployeeEntity>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _employeeService.GetEmployees();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(EmployeeEntity), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateEmployeeRequest request)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }

            EmployeeEntity result = await _employeeService.CreateEmployee(request);
            return CreatedAtAction("Post", result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EmployeeEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateEmployeeRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest("ID in URL and request body do not match.");
            }

            ValidationResult validationResult = await _updateValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }

            var result = await _employeeService.UpdateEmployee(request);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _employeeService.DeleteEmployee(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
