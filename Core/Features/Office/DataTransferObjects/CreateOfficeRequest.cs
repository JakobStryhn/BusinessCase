namespace Core.Features.Office.DataTransferObjects
{
    public class CreateOfficeRequest
    {
        public required string Name { get; set; }
        public required string LocationName { get; set; }
        public required int MaxOccupancy { get; set; }
    }
}
