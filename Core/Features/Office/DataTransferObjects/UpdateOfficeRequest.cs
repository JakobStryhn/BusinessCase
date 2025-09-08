namespace Api.Features.Office.Models
{
    public class UpdateOfficeRequest
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string LocationName { get; set; }
        public required int MaxOccupancyLimit { get; set; }
    }
}
