using Core.Domain.Models;
using Core.Features.Employee.DataTransferObjects;

namespace Core.Features.Employee.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeEntity>> GetEmployees();
        Task<EmployeeEntity> CreateEmployee(CreateEmployeeRequest request);
        Task<EmployeeEntity> UpdateEmployee(UpdateEmployeeRequest request);
        Task<bool> DeleteEmployee(Guid id);
    }
}
