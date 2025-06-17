using CRUD_HexagonalArchitecture.Domain.Entities;

namespace CRUD_HexagonalArchitecture.Domain.Interfaces.Repositories
{
    public interface ITaxRepository
    {
        Task<IEnumerable<TaxEntity>> GetAllAsync();
        Task<TaxEntity?> GetByIdAsync(int id);
        Task AddAsync(TaxEntity tax);
        Task UpdateAsync(TaxEntity tax);
        Task DeleteAsync(int id);
    }
}
