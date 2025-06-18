using CRUD_HexagonalArchitecture.Application.Dtos.Tax;
using CRUD_HexagonalArchitecture.Application.Ports.In;
using CRUD_HexagonalArchitecture.Domain.Interfaces.Repositories;

namespace CRUD_HexagonalArchitecture.Application.UseCases.Tax
{
    public class GetTaxUseCase : IGetTaxUseCase
    {
        private readonly ITaxRepository _repository;

        public GetTaxUseCase(ITaxRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaxListDto?> GetByIdAsync(int id)
        {
            var tax = await _repository.GetByIdAsync(id);

            return tax == null ? null : new TaxListDto 
            { 
                Id = tax.Id,
                Name = tax.Name,
                Percentage = tax.Percentage
            };
        }

        public async Task<IEnumerable<TaxListDto>> GetAllAsync()
        {
            var taxes = await _repository.GetAllAsync();

            return taxes.Select(t => new TaxListDto 
            { 
                Id = t.Id,
                Name = t.Name,
                Percentage = t.Percentage
            });
        }
    }
}
