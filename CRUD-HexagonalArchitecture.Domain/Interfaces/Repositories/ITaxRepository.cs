using CRUD_HexagonalArchitecture.Domain.Entities;

namespace CRUD_HexagonalArchitecture.Domain.Interfaces.Repositories
{
    public interface ITaxRepository
    {
        Task<IEnumerable<TaxEntity>> GetAllAsync();
        Task<TaxEntity?> GetByIdAsync(int id);
        Task<TaxEntity> CreateAsync(TaxEntity tax);
        Task<TaxEntity> UpdateAsync(TaxEntity tax);
        Task<bool> DeleteAsync(int id);
    }
}
