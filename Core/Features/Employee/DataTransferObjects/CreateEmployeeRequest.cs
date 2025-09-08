namespace Core.Features.Employee.DataTransferObjects
{
    public class CreateEmployeeRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly Birthdate { get; set; }
        public required Guid OfficeId { get; set; }
    }
}
