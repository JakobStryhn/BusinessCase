using Core.Domain.Models;
using Core.Features.Employee.DataTransferObjects;
using Core.Features.Employee.Interfaces;
using Core.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Employee.Services
{
    public class EmployeeService(IDbContextFactory<DBContext> dbContextFactory) : IEmployeeService
    {
        private readonly IDbContextFactory<DBContext> _dbContextFactory = dbContextFactory;

        public async Task<EmployeeEntity> CreateEmployee(CreateEmployeeRequest request)
        {
            EmployeeEntity employeeToCreate = new()
            {
                OfficeId = request.OfficeId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Birthdate = request.Birthdate
            };

            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var officeToAssignEmployeeTo = await dbContext.Offices
                    .Include(x => x.Employees)
                    .FirstOrDefaultAsync(x => x.Id == request.OfficeId);

                if (officeToAssignEmployeeTo is null)
                {
                    throw new InvalidOperationException($"Office with ID {request.OfficeId} not found.");
                }

                if (officeToAssignEmployeeTo.Employees.Count >= officeToAssignEmployeeTo.MaxOccupancy)
                {
                    throw new InvalidOperationException($"Max Occupancy reached for {officeToAssignEmployeeTo.Name}");
                }

                await dbContext.Employees.AddAsync(employeeToCreate);
                await dbContext.SaveChangesAsync();
            }

            return employeeToCreate;
        }


        public async Task<bool> DeleteEmployee(Guid id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var employeeToDelete = await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);

                if (employeeToDelete == null)
                {
                    return false;
                }

                dbContext.Employees.Remove(employeeToDelete);
                await dbContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<EmployeeEntity>> GetEmployees()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Employees.AsNoTracking().ToListAsync();
            }
        }

        public async Task<EmployeeEntity> UpdateEmployee(UpdateEmployeeRequest request)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var employeeToUpdate = await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == request.Id);

                if (employeeToUpdate == null)
                {
                    throw new InvalidOperationException($"Employee with ID {request.Id} not found.");
                }

                employeeToUpdate.FirstName = request.FirstName;
                employeeToUpdate.LastName = request.LastName;
                employeeToUpdate.Birthdate = request.Birthdate;

                await dbContext.SaveChangesAsync();
                return employeeToUpdate;
            }
        }
    }
}
