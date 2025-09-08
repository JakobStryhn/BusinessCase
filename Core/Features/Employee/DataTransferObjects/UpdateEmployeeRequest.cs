namespace Core.Features.Employee.DataTransferObjects
{
    public class UpdateEmployeeRequest
    {
        public Guid Id { get; init; }
        public Guid OfficeId { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required DateOnly Birthdate { get; init; }
    }
}
