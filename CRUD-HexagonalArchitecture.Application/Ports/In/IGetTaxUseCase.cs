using CRUD_HexagonalArchitecture.Application.Dtos.Tax;

namespace CRUD_HexagonalArchitecture.Application.Ports.In
{
    public interface IGetTaxUseCase
    {
        Task<TaxListDto?> GetByIdAsync(int id);
        Task<IEnumerable<TaxListDto>> GetAllAsync();
    }
}
