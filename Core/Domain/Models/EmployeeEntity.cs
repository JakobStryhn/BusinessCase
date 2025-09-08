namespace Core.Domain.Models
{
    public class EmployeeEntity
    {
        public Guid Id { get; init; }
        public Guid OfficeId { get; init; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly Birthdate { get; set; }
    }
}
