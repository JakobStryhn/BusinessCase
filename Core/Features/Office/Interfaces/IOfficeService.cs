using Api.Features.Office.Models;
using Core.Features.Office.DataTransferObjects;
using Core.Features.Office.Models;

namespace Core.Features.Office.Interfaces
{

    public interface IOfficeService
    {
        Task<List<OfficeEntity>> GetOffices();
        Task<OfficeEntity> CreateOffice(CreateOfficeRequest request);
        Task<OfficeEntity> UpdateOffice(UpdateOfficeRequest request);
        Task<bool> DeleteOffice(Guid id);
    }
}
