using CRUD_HexagonalArchitecture.Application.Dtos.Tax;
using CRUD_HexagonalArchitecture.Application.Ports.In;
using CRUD_HexagonalArchitecture.Domain.Interfaces.Repositories;

namespace CRUD_HexagonalArchitecture.Application.UseCases.Tax
{
    public class UpdateTaxUseCase : IUpdateTaxUseCase
    {
        private readonly ITaxRepository _repository;

        public UpdateTaxUseCase(ITaxRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaxListDto?> ExecuteAsync(TaxListDto dto)
        {
            var existing = await _repository.GetByIdAsync(dto.Id);

            if (existing == null) 
                return null;

            existing.Name = dto.Name;
            existing.Percentage = dto.Percentage;

            var updated = await _repository.UpdateAsync(existing);

            return new TaxListDto 
            { 
                Id = updated.Id,
                Name = updated.Name,
                Percentage = updated.Percentage
            };
        }
    }
}
