using Core.Domain.Models;

namespace Core.Features.Office.Models
{
    public class OfficeEntity
    {
        public Guid Id { get; init; }
        public required string Name { get; set; }
        public required string LocationName { get; set; }
        public required int MaxOccupancy { get; init; }
        public List<EmployeeEntity> Employees { get; set; } = [];
    }
}
