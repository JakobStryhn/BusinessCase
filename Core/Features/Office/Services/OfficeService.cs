using Api.Features.Office.Models;
using Core.Features.Office.DataTransferObjects;
using Core.Features.Office.Interfaces;
using Core.Features.Office.Models;
using Core.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Office.Services
{
    public class OfficeService(IDbContextFactory<DBContext> dbContextFactory) : IOfficeService
    {
        private readonly IDbContextFactory<DBContext> _dbContextFactory = dbContextFactory;

        public async Task<OfficeEntity> CreateOffice(CreateOfficeRequest request)
        {
            OfficeEntity officetoCreate = new()
            {
                Name = request.Name,
                LocationName = request.LocationName,
                MaxOccupancy = request.MaxOccupancy
            };

            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                await dbContext.Offices.AddAsync(officetoCreate);
                await dbContext.SaveChangesAsync();
            }
            return officetoCreate;
        }

        public async Task<bool> DeleteOffice(Guid id)
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

        public async Task<List<OfficeEntity>> GetOffices()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Offices.Include(x => x.Employees).AsNoTracking().ToListAsync();
            }
        }

        public async Task<OfficeEntity> UpdateOffice(UpdateOfficeRequest request)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var officeToUpdate = await dbContext.Offices.FirstOrDefaultAsync(e => e.Id == request.Id);

                if (officeToUpdate == null)
                {
                    throw new InvalidOperationException($"Office with ID {request.Id} not found.");
                }

                officeToUpdate.Name = request.Name;

                await dbContext.SaveChangesAsync();
                return officeToUpdate;
            }
        }
    }
}
